
using Edunext_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Edunext_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly string productUrl = "";
        private readonly HttpClient client = new HttpClient();

        [HttpGet("products")]
        public IActionResult Index()
        {
            try
            {
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>
                     (client.GetStringAsync(productUrl).Result);
                return View(products);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error get products: ", ex.Message);
            }
            return View();
        }
            
        [HttpGet("products/add")]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost("products/add")]
        public IActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createResponse = client.PostAsJsonAsync<Product>(productUrl, product).Result;
                    if (createResponse.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Add new successfully";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error add product: ", ex.Message);
            }
            TempData["error"] = "Add new successfully";
            return View();
        }

        [HttpGet("products/edit/code")]
        public IActionResult Edit(string code) 
        {
            try
            {
                var updatedProduct = JsonConvert.DeserializeObject<Product>
                    (client.GetStringAsync(productUrl + code).Result); 
                if(updatedProduct != null)
                {
                    return View(updatedProduct);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error get updatedProduct: ", ex.Message);
            }
            return View();
        }


        [HttpPost("products/edit/code")]
        public IActionResult Edit(Product product)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var editResponse = client.PutAsJsonAsync<Product>(productUrl, product).Result;
                    if (editResponse.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Update successfully.";
                        return RedirectToAction("Index");
                    }
                    TempData["success"] = "Update successfully.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error edit product: ", ex.Message);
            }
            return View();
        }

        [HttpGet("products/delete/code")]
        public IActionResult Delete(string code) 
        {
            if(code != null)
            {
                var product = JsonConvert.DeserializeObject<Product>(client.GetStringAsync(productUrl + code).Result);
                if (product != null)
                {
                    var deleteResponse = client.DeleteAsync(productUrl + code).Result;
                    if (deleteResponse.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Remove successfully";
                        return RedirectToAction("Index");
                    }
                }
            }
            TempData["error"] = "Failed to remove.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            return View();
        }
    }
}
