using Edunext_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Edunext_MVC.Controllers
{
    public class OrderDetailController : Controller
    {
        private string url = "http://localhost:5101/api/OrderDetails/";
        HttpClient _client = new HttpClient();
        
        public OrderDetailController( HttpClient client)
        {
            _client = client;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _client.GetAsync(url);
            if (model.IsSuccessStatusCode)
            {
                var result = model.Content.ReadAsStringAsync().Result;
                var orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(result);
                return View(orderDetails);
            }
            return View();
        }
        public async Task<IActionResult> GetByUserId(int UserId)
        {
            var model = await _client.GetAsync(url + UserId);
            if (model.IsSuccessStatusCode)
            {
                var result = model.Content.ReadAsStringAsync().Result;
                var orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(result);
                return View(orderDetails);
            }
            return View();
        }
        public async Task<IActionResult> GetByOrderId(string orderId)
        {
            var model = await _client.GetAsync(url + orderId);
            if (model.IsSuccessStatusCode)
            {
                var result = model.Content.ReadAsStringAsync().Result;
                var orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(result);
                return View(orderDetails);
            }
            return View();
        }
        public async Task<IActionResult> GetByCreatedDate (DateTime createdDate)
        {
            var model = await _client.GetAsync(url + createdDate);
            if (model.IsSuccessStatusCode)
            {
                var result = model.Content.ReadAsStringAsync().Result;
                var orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(result);
                return View(orderDetails);
            }
            return View();
        }
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var model = await _client.GetAsync(url + productId);
            if (model.IsSuccessStatusCode)
            {
                var result = model.Content.ReadAsStringAsync().Result;
                var orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(result);
                return View(orderDetails);
            }
            return View();
        }
    }
}
