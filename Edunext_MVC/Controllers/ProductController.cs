using Microsoft.AspNetCore.Mvc;

namespace Edunext_MVC.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
