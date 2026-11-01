using EmailSenderWeb.Models;
using EmailSenderWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace EmailSenderWeb.Controllers;

public sealed class HomeController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public HomeController(
        IEmailSender emailSender,
        ILogger<HomeController> logger,
        IStringLocalizer<SharedResource> localizer)
    {
        _emailSender = emailSender;
        _logger = logger;
        _localizer = localizer;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Notice = TempData["Notice"];
        return View(new EmailFormModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetLanguage(string culture, string? returnUrl)
    {
        var supportedCultures = new[] { "uk-UA", "en-US" };
        if (!supportedCultures.Contains(culture, StringComparer.OrdinalIgnoreCase))
        {
            culture = "uk-UA";
        }

        culture = CultureInfo.GetCultureInfo(culture).Name;
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), IsEssential = true });

        return Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl!) : RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(EmailFormModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _emailSender.SendEmailAsync(model.Recipient, model.Message, cancellationToken);
            TempData["Notice"] = "EmailSentSuccessfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Не вдалося надіслати лист одержувачу {Recipient}.", model.Recipient);
            ModelState.AddModelError(string.Empty,
                _localizer["EmailSendError"].Value);
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }
}
