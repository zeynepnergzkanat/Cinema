using Cinema.Presentation.Models;
using Cinema.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinema.Presentation.Controllers;

public class SeansController(ApiService apiService) : Controller
{
    public async Task<IActionResult> Index(Guid filmId, string? sehir = null)
    {
        var tumSeanslar = await apiService.GetAsync<List<SeansDto>>($"Seans/film/{filmId}") ?? new List<SeansDto>();

        var sehirler = tumSeanslar.Select(s => s.City).Distinct().OrderBy(c => c).ToList();

        if (sehir == null && sehirler.Any())
            sehir = sehirler.First();

        var filtreliSeanslar = sehir != null
            ? tumSeanslar.Where(s => s.City == sehir).ToList()
            : tumSeanslar;

        ViewBag.FilmId = filmId;
        ViewBag.Sehirler = sehirler;
        ViewBag.SecilenSehir = sehir;

        return View(filtreliSeanslar);
    }

    [Authorize]
    public async Task<IActionResult> KoltukSec(Guid seansId, string biletTipi, int biletAdedi = 0)
    {
        var seans = await apiService.GetAsync<SeansDto>($"Seans/{seansId}");
        if (seans is null)
            return RedirectToAction("Index");

        var doluKoltuklar = await GetDoluKoltuklar(seansId);
        ViewBag.BiletTipi = biletTipi;
        ViewBag.DoluKoltuklar = doluKoltuklar;
        ViewBag.BiletAdedi = biletAdedi;
        return View(seans);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SepeteEkle(Guid seansId, string biletTipi, List<string> koltuklar, int biletAdedi)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (koltuklar == null || !koltuklar.Any())
        {
            TempData["Error"] = $"Lütfen {biletAdedi} koltuk seçin.";
            return RedirectToAction("KoltukSec", new { seansId, biletTipi, biletAdedi });
        }

        if (koltuklar.Count != biletAdedi)
        {
            TempData["Error"] = $"{biletAdedi} koltuk seçmelisiniz. Şu an {koltuklar.Count} koltuk seçtiniz.";
            return RedirectToAction("KoltukSec", new { seansId, biletTipi, biletAdedi });
        }

        var koltukStr = string.Join(",", koltuklar);
        var request = new SepeteEkleRequest(seansId, biletAdedi, biletTipi, koltukStr);
        var success = await apiService.PostAsync($"Sepet/{userId}", request);

        if (!success)
            TempData["Error"] = "Sepete eklenirken bir hata oluştu.";
        else
            TempData["Success"] = "Sepete eklendi!";

        return RedirectToAction("Index", "Sepet");
    }

    private async Task<List<string>> GetDoluKoltuklar(Guid seansId)
    {
        var biletler = await apiService.GetAsync<List<BiletDto>>($"Bilet/seans/{seansId}");
        if (biletler is null) return new List<string>();
        return biletler
            .SelectMany(b => b.KoltukNumaralari.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .ToList();
    }
}