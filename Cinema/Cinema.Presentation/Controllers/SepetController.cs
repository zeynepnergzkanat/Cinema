using Cinema.Presentation.Models;
using Cinema.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cinema.Presentation.Controllers;

[Authorize]
public class SepetController(ApiService apiService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var sepetler = await apiService.GetAsync<List<SepetDto>>($"Sepet/{userId}");
        return View(sepetler ?? new List<SepetDto>());
    }

    [HttpPost]
    public async Task<IActionResult> Sil(Guid sepetId)
    {
        await apiService.DeleteAsync($"Sepet/{sepetId}");
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Odeme()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var sepetler = await apiService.GetAsync<List<SepetDto>>($"Sepet/{userId}");
        if (sepetler is null || !sepetler.Any())
            return RedirectToAction("Index");
        return View(sepetler);
    }

    [HttpPost]
    public async Task<IActionResult> OdemeYap(string cardHolderName, string cardNumber, string expiryDate, string cvv)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var request = new OdemeRequest(cardHolderName, cardNumber, expiryDate, cvv);
        var success = await apiService.PostAsync($"Bilet/satinal/{userId}", request);

        if (!success)
        {
            TempData["Error"] = "Ödeme sırasında bir hata oluştu.";
            return RedirectToAction("Odeme");
        }

        TempData["Success"] = "Biletleriniz başarıyla satın alındı!";
        return RedirectToAction("Index", "Bilet");
    }
}