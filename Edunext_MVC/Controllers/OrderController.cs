using Microsoft.AspNetCore.Mvc;

namespace Edunext_MVC.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
