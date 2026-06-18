using Cinema.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Presentation.ViewComponents;

public class FilmCardViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(FilmDto film)
    {
        return View(film);
    }
}