using Cinema.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KullaniciController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
        var kullanicilar = userManager.Users.Select(u => new
        {
            u.Id,
            u.FirstName,
            u.LastName,
            u.Email,
            u.UserName
        }).ToList();
        return Ok(kullanicilar);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(string id)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != id && !User.IsInRole("Admin"))
            return Forbid();

        var kullanici = await userManager.FindByIdAsync(id);
        if (kullanici is null)
            return NotFound("Kullanıcı bulunamadı.");
        return Ok(new
        {
            kullanici.Id,
            kullanici.FirstName,
            kullanici.LastName,
            kullanici.Email,
            kullanici.UserName
        });
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(string id, UpdateProfileRequest request)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != id)
            return Forbid();

        var kullanici = await userManager.FindByIdAsync(id);
        if (kullanici is null)
            return NotFound("Kullanıcı bulunamadı.");

        kullanici.FirstName = request.FirstName;
        kullanici.LastName = request.LastName;

        var result = await userManager.UpdateAsync(kullanici);
        if (!result.Succeeded)
            return BadRequest(string.Join(" ", result.Errors.Select(e => e.Description)));

        return Ok();
    }

    [HttpPost("{id}/sifreDegistir")]
    [Authorize]
    public async Task<IActionResult> SifreDegistir(string id, SifreDegistirRequest request)
    {
        var tokenUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (tokenUserId != id)
            return Forbid();

        var kullanici = await userManager.FindByIdAsync(id);
        if (kullanici is null)
            return NotFound("Kullanıcı bulunamadı.");

        var result = await userManager.ChangePasswordAsync(kullanici, request.MevcutSifre, request.YeniSifre);
        if (!result.Succeeded)
            return BadRequest(string.Join(" ", result.Errors.Select(e => TurkceHataMesaji(e.Code))));

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return BadRequest("Geçersiz email veya şifre.");
        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return BadRequest("Geçersiz email veya şifre.");
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<System.Security.Claims.Claim>
        {
            new(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id),
            new(System.Security.Claims.ClaimTypes.Name, user.UserName!),
            new(System.Security.Claims.ClaimTypes.Email, user.Email!)
        };
        foreach (var role in roles)
            claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role));
        var token = tokenService.GenerateAccessToken(claims);
        return Ok(new AuthResponse(user.Id, user.Email!, user.FirstName, user.LastName, roles.ToList(), token));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            Email = request.Email
        };
        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var hatalar = result.Errors.Select(e => TurkceHataMesaji(e.Code)).ToList();
            return BadRequest(string.Join(" ", hatalar));
        }
        await userManager.AddToRoleAsync(user, "User");
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<System.Security.Claims.Claim>
        {
            new(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id),
            new(System.Security.Claims.ClaimTypes.Name, user.UserName!),
            new(System.Security.Claims.ClaimTypes.Email, user.Email!)
        };
        foreach (var role in roles)
            claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role));
        var token = tokenService.GenerateAccessToken(claims);
        return Ok(new AuthResponse(user.Id, user.Email!, user.FirstName, user.LastName, roles.ToList(), token));
    }

    private static string TurkceHataMesaji(string code)
    {
        return code switch
        {
            "PasswordTooShort" => "Şifre en az 6 karakter olmalıdır.",
            "PasswordRequiresDigit" => "Şifre en az bir rakam içermelidir.",
            "PasswordRequiresLower" => "Şifre en az bir küçük harf içermelidir.",
            "PasswordRequiresUpper" => "Şifre en az bir büyük harf içermelidir.",
            "PasswordRequiresNonAlphanumeric" => "Şifre en az bir özel karakter içermelidir.",
            "DuplicateUserName" => "Bu email adresi zaten kullanılıyor.",
            "DuplicateEmail" => "Bu email adresi zaten kullanılıyor.",
            "InvalidEmail" => "Geçersiz email adresi.",
            "PasswordMismatch" => "Mevcut şifre yanlış.",
            _ => "Bir hata oluştu."
        };
    }
}