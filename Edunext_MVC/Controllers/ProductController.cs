using Edunext_Model.DTOs.Product;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Edunext_MVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly string productUrl = "http://localhost:5101/api/Products/";
        private readonly string cateUrl = "http://localhost:5101/api/Categories/";
        private readonly HttpClient client = new HttpClient();

        [HttpGet("products")]
        public IActionResult Index(string search = "")
        {
            try
            {
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductGet>>
                     (client.GetStringAsync(productUrl).Result);
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
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
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>
                     (client.GetStringAsync(cateUrl).Result);
            ViewBag.Categories = new SelectList(categories, "Id", "Name", "Id");

            return View();
        }


        [HttpPost("products/add")]
        public IActionResult Create(ProductPost product)
        {
            try
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>
                     (client.GetStringAsync(cateUrl).Result);
                ViewBag.Categories = new SelectList(categories, "Id", "Name", "Id");

                if (ModelState.IsValid)
                {
                    MultipartFormDataContent formData = new()
                    {
                        { new StringContent(product.Code), "Code" },
                        { new StringContent(product.Name), "Name" },
                        { new StringContent(product.Price.ToString()), "Price" },
                        { new StringContent(product.Quantity.ToString()), "Quantity" },
                        { new StringContent(product.Description), "Description" },
                        { new StreamContent(product.Image.OpenReadStream()), "Image", product.Image.FileName },
                        { new StringContent(product.CategoryId.ToString()), "CategoryId" }
                    };

                    var createResponse = client.PostAsync(productUrl, formData).Result;
                    if (createResponse.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Add new successfully";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error add product: ", ex.Message);
            }
            TempData["error"] = "Failed to add new";
            return RedirectToAction("Index");
        }


        [HttpGet("products/edit/{id}")]
        public IActionResult Edit(int id) 
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>
                     (client.GetStringAsync(cateUrl).Result);
            ViewBag.Categories = new SelectList(categories, "Id", "Name", "Id");
            try
            {
                var res = client.GetAsync(productUrl + "edit/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    var product = JsonConvert.DeserializeObject<ProductPost>(data);

                    return View(product);
                }
                TempData["error"] = "Not found product";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost("products/edit/{id}")]
        public IActionResult Edit(Product product)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>
                     (client.GetStringAsync(cateUrl).Result);
            ViewBag.Categories = new SelectList(categories, "Id", "Name", "Id");
            try
            {
                if(ModelState.IsValid)
                {
                    var editResponse = client.PutAsJsonAsync(productUrl + product.Id, product).Result;
                    if (editResponse.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Update successfully.";
                        return RedirectToAction("Index");
                    }
                    TempData["error"] = "Failed to update.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error edit product: ", ex.Message);
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var response = client.GetStringAsync(productUrl + id).Result;
                var book = JsonConvert.DeserializeObject<ProductGet>(response);
                if (book != null)
                {
                    var deleteResponse = client.DeleteAsync(productUrl + id).Result;
                    if (deleteResponse.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Delete successfully.";
                        return RedirectToAction("Index");
                    }
                }
                return NotFound();
            }
            TempData["error"] = "Delete failed";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Details(int id) 
        {
            var product = JsonConvert.DeserializeObject<Product>(client.GetStringAsync(productUrl + id).Result);
            if(product != null)
            {
                return View(product);
            }
            return NotFound();
        }
    }
}
