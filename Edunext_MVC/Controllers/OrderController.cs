using Edunext_Model.Models;
using Edunext_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using X.PagedList;

namespace Edunext_MVC.Controllers
{
    using Orders = IEnumerable<Order>;
    [Authorize]
    public class OrderController : Controller
    {
        private readonly string url;
        private readonly HttpClient httpClient;

        public OrderController(IOptions<Client> client)
        {
            url = client.Value.Url + "/Orders";
            httpClient = new();
        }
        [HttpGet]
        public IActionResult Index(int userid_search, string collumn = "", string sort_type = "", int page_number = 1)
        {
            var res = httpClient.GetAsync(url).Result;
            if (res.IsSuccessStatusCode)
            {
                var data = res.Content.ReadAsStringAsync().Result;
                var orders = JsonConvert.DeserializeObject<Orders>(data);

                orders = orders.Where(order => userid_search == 0 || order.UserId == userid_search);

                orders = OrderOrders(orders, collumn, sort_type);

                IPagedList<Order> paginatedOrders = orders.ToPagedList(page_number, 5);

                ViewBag.Search = userid_search;
                ViewBag.Sort_type = sort_type;
                ViewBag.Collumn = collumn;

                return View(paginatedOrders);
            }

            return View(null);
        }

        private Orders OrderOrders(Orders orders, string collumn, string sort_type)
        {
            Func<Order, IComparable> keySelector = GetKeySelector(collumn);

            if (keySelector == null)
            {
                return orders;
            }

            switch (sort_type)
            {
                case "asc":
                    return orders.OrderBy(keySelector).ToList();
                case "desc":
                    return orders.OrderByDescending(keySelector).ToList();
                default:
                    return orders;
            }
        }

        private Func<Order, IComparable>? GetKeySelector(string collumn)
        {
            switch (collumn)
            {
                case "status":
                    return order => order.Status;
                case "order_date":
                    return order => order.OrderDate;
                case "shipped_date":
                    return order => order.ShippedDate;
                case "required_date":
                    return order => order.RequiredDate;
                case "date_update":
                    return order => order.DateUpdate;
                default:
                    return null;
            }
        }
    }
}
