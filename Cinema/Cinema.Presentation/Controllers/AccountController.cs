using Cinema.Presentation.Models;
using Cinema.Presentation.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinema.Presentation.Controllers;

public class AccountController(ApiService apiService) : Controller
{
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var response = await apiService.PostAsync<AuthResponse>("Kullanici/login", new LoginRequest(email, password));
        if (response is null)
        {
            ModelState.AddModelError("", "Geçersiz email veya şifre.");
            return View();
        }

        await SignInUser(response);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(string firstName, string lastName, string email, string password)
    {
        var (success, error) = await apiService.PostWithErrorAsync("Kullanici/register", new RegisterRequest(firstName, lastName, email, password));

        if (!success)
        {
            ModelState.AddModelError("", error ?? "Kayıt sırasında bir hata oluştu.");
            return View();
        }

        var loginResponse = await apiService.PostAsync<AuthResponse>("Kullanici/login", new LoginRequest(email, password));
        if (loginResponse is not null)
            await SignInUser(loginResponse);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<IActionResult> Profil()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var kullanici = await apiService.GetAsync<AuthResponse>($"Kullanici/{userId}");
        if (kullanici is null)
            return RedirectToAction("Index", "Home");
        return View(kullanici);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ProfilGuncelle(string firstName, string lastName)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await apiService.PutAsync($"Kullanici/{userId}", new UpdateProfileRequest(firstName, lastName));
        if (!success)
            TempData["Error"] = "Profil güncellenirken bir hata oluştu.";
        else
            TempData["Success"] = "Profil başarıyla güncellendi.";
        return RedirectToAction("Profil");
    }
    public IActionResult AccessDenied()
    {
        return View();
    }


    private async Task SignInUser(AuthResponse response)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, response.Id),
        new Claim(ClaimTypes.Email, response.Email),
        new Claim(ClaimTypes.Name, $"{response.FirstName} {response.LastName}"),
        new Claim("AccessToken", response.Token)
    };

        foreach (var role in response.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}