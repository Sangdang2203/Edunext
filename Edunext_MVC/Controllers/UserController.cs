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

        [HttpGet]
        public IActionResult Index()
        {
            var res = _httpClient.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                var users = JsonConvert.DeserializeObject<User>(data);
                return View(users);
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
        public IActionResult Edit(int id)
        {
            var res = _httpClient.GetAsync(url + id).Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                var user = JsonConvert.DeserializeObject<User>(data);
                return View(user);
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
        public IActionResult Login ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login (User user)
        {
            try
            {
                var response = _httpClient.PostAsJsonAsync(url + "login", user).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    var userLogin = JsonConvert.DeserializeObject<User>(data);
                    if (userLogin != null)
                    {
                        HttpContext.Session.SetString("user", JsonConvert.SerializeObject(userLogin));
                        return RedirectToAction("Index", "User");
                    }
                    ModelState.AddModelError("", "Username or password is incorrect!");
                    return View();
                }
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View();
            }
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }
    }
}
