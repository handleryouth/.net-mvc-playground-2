using Microsoft.AspNetCore.Mvc;

namespace contactForm.Controllers
{
    public class ErrorController : Controller
    {
        [ActionName("404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}

