using Microsoft.AspNetCore.Mvc;
using webbir.Interfaces;
using webbir.Models;

namespace webbir.Controllers;

public class ProfileController : Controller
{
    private readonly IAuthService _authService;

    public ProfileController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var user = _authService.GetUserById(userId.Value);
        if (user == null) return RedirectToAction("Login", "Auth");

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(double? heightCm, double? weightKg, string programName, string about)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Login", "Auth");

        var success = _authService.UpdateUserProfile(userId.Value, heightCm, weightKg, programName ?? string.Empty, about ?? string.Empty);
        if (!success)
        {
            ViewBag.Error = "Profil güncellenirken hata oluştu";
            var user = _authService.GetUserById(userId.Value);
            return View(user);
        }

        // Redirect to GET, böylece sayfa yenilendiğinde güncel veri tekrar yüklensin
        TempData["Success"] = "Profiliniz başarıyla güncellendi.";
        return RedirectToAction("Index");
    }
}