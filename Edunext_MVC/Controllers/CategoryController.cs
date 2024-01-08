using Edunext_Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Edunext_MVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly string cateUrl = "http://localhost:5101/api/Categories/";
        private readonly HttpClient client = new HttpClient();
        [HttpGet("categories")]
        public IActionResult Index()
        {
            try
            {
                var category = JsonConvert.DeserializeObject<IEnumerable<Category>>
                     (client.GetStringAsync(cateUrl).Result);
                return View(category);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error get categories: ", ex.Message);
            }
            return View();
        }

        [HttpGet("categories/add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("categories/add")]
        public IActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var createResponse = client.PostAsJsonAsync(cateUrl, category).Result;
                    if(createResponse.IsSuccessStatusCode)
                    {
                        TempData["success"] = "Add new successfully";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }
            return View();
        }

        [HttpGet("categories/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = JsonConvert.DeserializeObject<Category>
                    (client.GetStringAsync(cateUrl + id).Result);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }

        [HttpPost("categories/edit/{id}")]
        public IActionResult Edit(Category category)
        {
            try
            {
                var editResponese = client.PutAsJsonAsync(cateUrl + category.Id, category).Result;
                if (editResponese.IsSuccessStatusCode)
                {
                    TempData["success"] = "Update category successfully.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error failed to update: ", ex.Message);
            }
            return View();
        }
    }
}
