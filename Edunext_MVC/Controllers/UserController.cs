using Edunext_API.Helpers;
using Edunext_API.Models;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Edunext_MVC.Controllers
{
    public class UserController : Controller
    {
        private string url = "http://localhost:5101/api/Users/";

        private HttpClient _httpClient = new HttpClient();

        private readonly DatabaseContext _context;

        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var res = _httpClient.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<User>>>(data);
                return View(apiResponse.Value);
            }
            return View(null);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                var json = JsonConvert.SerializeObject(user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var res = _httpClient.PostAsync(url, data).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Create successfully";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception e)
            {
                TempData["Error"] = "Error while create";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var res = _httpClient.GetAsync(url + id).Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<User>>(data);
                return View(apiResponse.Value);
            }
            return View(null);
        }

        [HttpPost]
        public IActionResult Edit(int id, User user)
        {
            try
            {
                var json = JsonConvert.SerializeObject(user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var res = _httpClient.PutAsync(url + id, data).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var res = _httpClient.DeleteAsync(url + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                var response = _httpClient.PostAsJsonAsync(url + "login", user).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<User>>(data);
                    var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                    if (dbUser != null)
                    {
                        // Replace the username in the response with the username from the database
                        HttpContext.Session.SetString("UserName", dbUser.Username);
                    }
                    return RedirectToAction("Index", "User");
                }
                TempData["Error"] = "Username or password is incorrect!";
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Logout successfully";
            return RedirectToAction("Login", "User");
        }
    }
}
