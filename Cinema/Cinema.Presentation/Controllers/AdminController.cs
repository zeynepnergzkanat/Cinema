using Cinema.Presentation.Models;
using Cinema.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Presentation.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(ApiService apiService) : Controller
{
    public IActionResult Index() => View();

    // Film işlemleri
    public async Task<IActionResult> Filmler()
    {
        var filmler = await apiService.GetAsync<List<FilmDto>>("Film");
        return View(filmler ?? new List<FilmDto>());
    }

    public IActionResult FilmEkle() => View();

    [HttpPost]
    public async Task<IActionResult> FilmEkle(CreateFilmRequest request)
    {
        var result = await apiService.PostAsync<FilmDto>("Film", request);
        if (result is null)
        {
            TempData["Error"] = "Film eklenirken bir hata oluştu.";
            return View();
        }
        return RedirectToAction("Filmler");
    }

    public async Task<IActionResult> FilmDuzenle(Guid id)
    {
        var film = await apiService.GetAsync<FilmDto>($"Film/{id}");
        if (film is null)
            return RedirectToAction("Filmler");
        return View(film);
    }

    [HttpPost]
    public async Task<IActionResult> FilmDuzenle(UpdateFilmRequest request)
    {
        var success = await apiService.PutAsync("Film", request);
        if (!success)
        {
            TempData["Error"] = "Film güncellenirken bir hata oluştu.";
            return View();
        }
        return RedirectToAction("Filmler");
    }

    [HttpPost]
    public async Task<IActionResult> FilmSil(Guid id)
    {
        await apiService.DeleteAsync($"Film/{id}");
        return RedirectToAction("Filmler");
    }

    // Seans işlemleri
    public async Task<IActionResult> Seanslar()
    {
        var seanslar = await apiService.GetAsync<List<SeansDto>>("Seans");
        return View(seanslar ?? new List<SeansDto>());
    }

    public async Task<IActionResult> SeansEkle()
    {
        var filmler = await apiService.GetAsync<List<FilmDto>>("Film");
        var salonlar = await apiService.GetAsync<List<SalonDto>>("Salon");
        ViewBag.Filmler = filmler ?? new List<FilmDto>();
        ViewBag.Salonlar = salonlar ?? new List<SalonDto>();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SeansEkle(CreateSeansRequest request)
    {
        var result = await apiService.PostAsync<SeansDto>("Seans", request);
        if (result is null)
        {
            TempData["Error"] = "Seans eklenirken bir hata oluştu.";
            return View();
        }
        return RedirectToAction("Seanslar");
    }

    [HttpPost]
    public async Task<IActionResult> SeansSil(Guid id)
    {
        await apiService.DeleteAsync($"Seans/{id}");
        return RedirectToAction("Seanslar");
    }

    // Salon işlemleri
    public async Task<IActionResult> Salonlar()
    {
        var salonlar = await apiService.GetAsync<List<SalonDto>>("Salon");
        return View(salonlar ?? new List<SalonDto>());
    }

    public IActionResult SalonEkle() => View();

    [HttpPost]
    public async Task<IActionResult> SalonEkle(CreateSalonRequest request)
    {
        var result = await apiService.PostAsync<SalonDto>("Salon", request);
        if (result is null)
        {
            TempData["Error"] = "Salon eklenirken bir hata oluştu.";
            return View();
        }
        return RedirectToAction("Salonlar");
    }

    [HttpPost]
    public async Task<IActionResult> SalonSil(Guid id)
    {
        await apiService.DeleteAsync($"Salon/{id}");
        return RedirectToAction("Salonlar");
    }
}