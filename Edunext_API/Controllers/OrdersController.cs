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
        public async Task<ActionResult<Orders>> GetOrders()
        {
            Orders orders = await orderServices.GetOrders();
          
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            Order? order = await orderServices.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
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
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            if (order.Id != 0 && order.Id != id)
            {
                return BadRequest();
            }

            try
            {
                order.Id = id;
                order.DateUpdate = DateTime.Now;

                /*databaseContext.Update(order);
                await databaseContext.SaveChangesAsync();*/
                return Ok(order);
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }
        }

        [HttpPut("/status/{id}")]
        public async Task<ActionResult<Order>> UpdateStatus(int id, string status)
        {
            Order order = await orderServices.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            try
            {
                order.Status = status;
                order.DateUpdate = DateTime.Now;

                await orderServices.Save();

                return Ok(order);
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest();
            }
        }
    }
}
