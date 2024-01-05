using Microsoft.AspNetCore.Mvc;

namespace Edunext_MVC.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
