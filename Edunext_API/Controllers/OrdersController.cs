using Edunext_API.Helpers;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Edunext_Model.DTOs;
using Edunext_API.Services;

namespace Edunext_API.Controllers
{
    using Orders = IEnumerable<Order>;

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService orderServices;

        public OrdersController(OrderService orderServices)
        {
            this.orderServices = orderServices;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<Orders>>> GetOrders()
        {
            Orders orders = await orderServices.GetOrders();
            ApiResponse<Orders> res = new(orders, "Get orders successfully");

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Order>>> GetOrderById(int id)
        {
            Order? order = await orderServices.GetOrderById(id);
            ApiResponse<Order?> res = new(order, "Get order successfully");
            if (order == null)
            {
                res.Message = $"Not found this order id: {id}";
                return NotFound(res);
            }
            return Ok(res);
        }

        /*        [HttpPost]
                public async Task<ActionResult<ApiResponse<Order>>> CreateOrder(Order order)
                {
                    ApiResponse<Order> res = new(order, "Create order successfully");
                    if (order.Id != "")
                    {
                        res.Message = "Cannot create this order because id is auto generared!";
                        return BadRequest(res);
                    }
                    try
                    {
                        order.Id = Guid.NewGuid().ToString();
                        order.OrderDate = DateTime.Now;
                        order.DateUpdate = DateTime.Now;

                        await databaseContext.Orders.AddAsync(order);
                        await databaseContext.SaveChangesAsync();
                        return CreatedAtAction("GetOrder", new { id = order.Id }, res);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }*/

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Order>>> UpdateOrder(int id, Order order)
        {
            ApiResponse<Order> res = new(order, "Update order successfully!");
            if (order.Id != 0 && order.Id != id)
            {
                res.Message = "Id param and order id is different!";
                return BadRequest(res);
            }

            try
            {
                order.Id = id;
                order.DateUpdate = DateTime.Now;

                /*databaseContext.Update(order);
                await databaseContext.SaveChangesAsync();*/

                res.Value = order;
                return Ok(res);
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }
        }

        [HttpPut("/status/{id}")]
        public async Task<ActionResult<ApiResponse<Order>>> UpdateStatus(int id, string status)
        {
            Order order = await orderServices.GetOrderById(id);
            ApiResponse<Order> res = new(order, "Update order successfully!");

            if (order == null)
            {
                res.Message = $"Not found this order id: {id}";
                return NotFound(res);
            }

            try
            {
                order.Status = status;
                order.DateUpdate = DateTime.Now;

                await orderServices.Save();

                return Ok(res);
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }
        }
    }
}
