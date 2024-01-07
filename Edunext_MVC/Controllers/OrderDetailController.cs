using Edunext_Model.DTOs.Cart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Edunext_MVC.Controllers
{
    public class OrderDetailController : Controller
    {
        private string url = "http://localhost:5101/api/OrderDetails/";
        HttpClient client = new HttpClient();
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> Index()
        {
            /*var UserId = HttpContext.Session.GetString("UserId");*/
            var UserId = "Tester";
            IEnumerable<OrderDetailDTO> TestDTO = new List<OrderDetailDTO>()
            {
                new OrderDetailDTO
                {
                    Id = 1,
                    OrderID = 10,
                    ProductID = 2,
                    Quantity = 3,
                    UnitPrice = 10.99m,
                    Discount = 5m,
                    LineTotal = 15.98m,
                    Status = "Pending",
                    CreatedDate = DateTime.Now,
                    ProductName = "Product A",
                    ProductImage = "https://example.com/product-a.jpg"
                },
                new OrderDetailDTO
                {
                    Id = 2,
                    OrderID = 11,
                    ProductID = 3,
                    Quantity = 2,
                    UnitPrice = 20.99m,
                    Discount = 2m,
                    LineTotal = 40.78m,
                    Status = "Pending",
                    CreatedDate = DateTime.Now,
                    ProductName = "Product B",
                    ProductImage = "https://example.com/product-b.jpg"
                },
                new OrderDetailDTO
                {
                    Id = 3,
                    OrderID = 12,
                    ProductID = 1,
                    Quantity = 1,
                    UnitPrice = 5.99m,
                    Discount = 0m,
                    LineTotal = 5.99m,
                    Status = "Pending",
                    CreatedDate = DateTime.Now,
                    ProductName = "Product C",
                    ProductImage = "https://example.com/product-c.jpg"
                }
            };

            if (!string.IsNullOrEmpty(UserId))
            {
                var model = await client.GetAsync(url+UserId);
                if (model.IsSuccessStatusCode)
                {
                    var result = model.Content.ReadAsStringAsync().Result;
                    var orderDetails = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(result);
                    return View(orderDetails);
                }
                else
                {
                    ViewBag.User = UserId;
                    return View(TestDTO);
                   /* return RedirectToAction("Index", "Home");*/
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }
    }
}
