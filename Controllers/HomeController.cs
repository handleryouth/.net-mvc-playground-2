using Microsoft.AspNetCore.Mvc;

namespace contactForm.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}

