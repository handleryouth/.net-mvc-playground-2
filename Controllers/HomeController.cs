using contactForm.Models.CommonModel;
using Microsoft.AspNetCore.Mvc;

namespace contactForm.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var initialModelValue = new RegisterModel()
        {
            QueryType = null,
        };
        return View(initialModelValue);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        return Content("success");

    }

}

