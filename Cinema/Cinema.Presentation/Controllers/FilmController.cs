using Cinema.Presentation.Services;
using Cinema.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Presentation.Controllers;

public class FilmController(ApiService apiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var filmler = await apiService.GetAsync<List<FilmDto>>("Film");
        return View(filmler ?? new List<FilmDto>());
    }

    public async Task<IActionResult> Detail(Guid id)
    {
        var film = await apiService.GetAsync<FilmDto>($"Film/{id}");
        if (film is null)
            return RedirectToAction("Index");
        return View(film);
    }
}