using EmailSenderWeb.Lifetimes;
using EmailSenderWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderWeb.Controllers;

public sealed class LifetimesController : Controller
{
    [HttpGet]
    public IActionResult Index(
        [FromServices] TransientProbe transient,
        [FromServices] ScopedProbe scoped,
        [FromServices] SingletonProbe singleton)
    {
        var secondTransient = HttpContext.RequestServices.GetRequiredService<TransientProbe>();
        var secondScoped = HttpContext.RequestServices.GetRequiredService<ScopedProbe>();
        var secondSingleton = HttpContext.RequestServices.GetRequiredService<SingletonProbe>();

        return View(new LifetimeDemoModel
        {
            TransientFirst = transient.Id,
            TransientSecond = secondTransient.Id,
            ScopedFirst = scoped.Id,
            ScopedSecond = secondScoped.Id,
            SingletonFirst = singleton.Id,
            SingletonSecond = secondSingleton.Id
        });
    }
}
