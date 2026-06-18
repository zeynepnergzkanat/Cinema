using Cinema.Presentation.Models;
using Cinema.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinema.Presentation.Controllers;

[Authorize]
public class BiletController(ApiService apiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var biletler = await apiService.GetAsync<List<BiletDto>>($"Bilet/{userId}");
        return View(biletler ?? new List<BiletDto>());
    }
}