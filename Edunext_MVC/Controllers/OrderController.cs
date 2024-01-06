using Microsoft.AspNetCore.Mvc;

namespace Edunext_MVC.Controllers
{
    public class OrderController : Controller
    {
        private string url = "http://localhost:5102/api/Author";
        private HttpClient httpClient;

        public OrderController()
        {
            httpClient = new HttpClient();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
