using Cinema.Presentation.Models;
using Cinema.Presentation.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Presentation.Controllers;

public class HomeController(ApiService apiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var filmler = await apiService.GetAsync<List<FilmDto>>("Film");
        return View(filmler ?? new List<FilmDto>());
    }
}