using Microsoft.AspNetCore.Mvc;

namespace Edunext_MVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
