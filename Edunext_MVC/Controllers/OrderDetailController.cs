using Edunext_Model.DTOs.Cart;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Edunext_MVC.Controllers
{
    [Authorize]
    public class OrderDetailController : Controller
    {
        private readonly string url = "http://localhost:5101/api/OrderDetails/";
        private readonly HttpClient client = new HttpClient();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var testDTO = new List<OrderDetailDTO>()
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
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");

            if (!string.IsNullOrEmpty(userName))
            {
                var orderDetails = await GetOrderDetailsAsync((int)userId);

                ViewBag.User = userName;
                if (orderDetails == null)
                {
                    return View(testDTO);
                }
                else
                {
                    return View(orderDetails);
                }
            }
            else
            {
                ViewBag.User = "Guest";
                return View(testDTO);
            }
        }

        private async Task<IEnumerable<OrderDetailDTO>> GetOrderDetailsAsync(int userId)
        {
            var model = await client.GetAsync(url + userId);
            if (!model.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await model.Content.ReadAsStringAsync();
            var orderDetails = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(result);
            return orderDetails;
        }
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> Statistics()
        {
            var testDTO = new List<OrderDetailDTO>()
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
            var model = await client.GetAsync(url);
            if (model.IsSuccessStatusCode)
            {
                var result = await model.Content.ReadAsStringAsync();
                var AllOrderDetails = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(result);
                if (AllOrderDetails.Count > 0)
                {
                    return View(AllOrderDetails);
                }
                else
                {
                    return View(testDTO);
                }
            }
            return View(testDTO);
        }
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetTop10Seller()
        {
            var model = await client.GetAsync(url + "GetTop10SellingProducts");
            if (!model.IsSuccessStatusCode)
            {
                return new List<OrderDetailDTO>
                {
                    new OrderDetailDTO
                    {
                        Id = 1,
                        OrderID = 1,
                        ProductID = 1,
                        Quantity = 10,
                        UnitPrice = 1000,
                        Discount = 0,
                        LineTotal = 10000,
                        CreatedDate = DateTime.Now,
                        ProductName = "Sản phẩm 1",
                        ProductImage = "https://example.com/product-1.jpg",
                        Status = "Đã giao hàng"
                    },
                    new OrderDetailDTO
                    {
                        Id = 2,
                        OrderID = 2,
                        ProductID = 2,
                        Quantity = 9,
                        UnitPrice = 900,
                        Discount = 0,
                        LineTotal = 8100,
                        CreatedDate = DateTime.Now,
                        ProductName = "Sản phẩm 2",
                        ProductImage = "https://example.com/product-2.jpg",
                        Status = "Đang vận chuyển"
                    },
                    new OrderDetailDTO
                    {
                        Id = 3,
                        OrderID = 3,
                        ProductID = 3,
                        Quantity = 8,
                        UnitPrice = 800,
                        Discount = 0,
                        LineTotal = 6400,
                        CreatedDate = DateTime.Now,
                        ProductName = "Sản phẩm 3",
                        ProductImage = "https://example.com/product-3.jpg",
                        Status = "Đã thanh toán"
                    } };
            }

            var result = await model.Content.ReadAsStringAsync();
            var Top10seller = JsonConvert.DeserializeObject<List<OrderDetailDTO>>(result);
            return Top10seller;
        }
        public async Task<IActionResult> ProductChartTotalQuantityByMonth()
        {
            var labels = new string[] { "January", "February", "March", "April", "May" };
            var totalQuantity = new int[] { 10, 20, 15, 30, 25 };

            var getModel = await client.GetAsync(url + "ProductChartTotalQuantityByMonth");
           
            if (getModel.IsSuccessStatusCode)
            {
                var result = await getModel.Content.ReadAsStringAsync();

                try
                {
                    var jsonData = JsonConvert.DeserializeObject<dynamic>(result);
                    var jsonDataLabels = jsonData.labels;
                    var jsonDataTotalQuantity = jsonData.totalQuantity;

                    if (jsonDataLabels.Count == 0 || jsonDataTotalQuantity.Count == 0)
                    {
                        return Json(new { labels, totalQuantity });
                    }
                    else
                    {
                        return Json(result);
                    }
                }
                catch (JsonReaderException)
                {
                    return Json(new { labels, totalQuantity });
                }
            }
            return Json(new { labels, totalQuantity });
        }
    }
}
