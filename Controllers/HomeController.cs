using contactForm.Models.CommonModel;
using contactForm.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace contactForm.Controllers;

public class HomeController : Controller
{

    private readonly IRegisterServices _registerServices;
    public HomeController(IRegisterServices registerServices)
    {
        _registerServices = registerServices;
    }

    public IActionResult Index()
    {
        var initialModelValue = new RegisterModel()
        {
            QueryType = null,
        };
        return View(initialModelValue);
    }


    [HttpPost]
    // [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _registerServices.Create(model);
        return Ok(new { message = "Test" });

    }

}

