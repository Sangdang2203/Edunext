using Edunext_Model.DTOs.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SelectPdf;
using System.Net.Http.Headers;

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
        public async Task<IActionResult> HtmlToPdfConverter()
        {
            string currentUrl = Request.GetEncodedUrl();
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertUrl(currentUrl);
            /*doc.Save("test.pdf");
            doc.Close();
            return RedirectToAction("Index");*/
            byte[] pdfData = await converter.ConvertAsync(currentUrl);
            return File(pdfData, "application/pdf", "test.pdf");
        }
    }
}
