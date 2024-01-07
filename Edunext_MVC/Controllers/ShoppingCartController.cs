using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Edunext_Model.DTOs.Cart;
using Microsoft.Extensions.Options;
using Edunext_MVC.Models;

namespace Edunext_MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly string url;
        private readonly HttpClient httpClient;

        public ShoppingCartController(IOptions<Client> client)
        {
            url = client.Value.Url + "/ShoppingCart";
            httpClient = new();
        }

        [HttpGet]
        public IActionResult Index()
        {
            /*var res = httpClient.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                var shoppingCart = JsonConvert.DeserializeObject<ShoppingCartDTO>(data);
                return View(shoppingCart);
            }*/
            return View();
        }
    }
}
