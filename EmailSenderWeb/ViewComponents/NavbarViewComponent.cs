using Microsoft.AspNetCore.Mvc;

namespace EmailSenderWeb.ViewComponents;

public sealed class NavbarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
