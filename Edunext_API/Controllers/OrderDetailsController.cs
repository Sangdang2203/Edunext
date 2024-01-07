using Edunext_API.Helpers;
using Edunext_API.Models;
using Edunext_API.Services;
using Edunext_Model.DTOs.Cart;
using Edunext_Model.Mapper;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailService orderDetailServices;
        private readonly DatabaseContext _context;

        public OrderDetailsController(OrderDetailService orderDetailServices, DatabaseContext context)
        {
            this.orderDetailServices = orderDetailServices;
            _context = context;
        }

        /*        [HttpPost]
                public async Task<ActionResult<ApiResponse<OrderDetailDTO>>> CreateOrderDetail(IDictionary<string, object> requestbody)
                {
                    object orderDTO = new OrderDTO(); 

                    if (requestbody.TryGetValue("order", out orderDTO))
                    {
                        Order order = await orderServices.CreateOrder((OrderDTO) orderDTO);

                        object orderDetailDTO = new List<OrderDetailDTO>();
                        if (requestbody.TryGetValue("cart", out orderDetailDTO))
                        {
                            IEnumerable<OrderDetailDTO> newOrderDetailDTOs = (List<OrderDetailDTO>)orderDetailDTO;

                            foreach (OrderDetailDTO newOrderDetailDTO in newOrderDetailDTOs) {
                                OrderDetail orderDetail = Mapping.Mapper.Map<OrderDetail>(newOrderDetailDTO);
                                orderDetail.
                                order.OrderDetails.Add(orderDetail);
                            }

                        }
                    }

                    return Ok(orderDetailDTO);
                }*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAllOrderDetail()
        {
            try
            {
                var orderDetails = await _context.OrderDetails.ToListAsync();
                if (orderDetails != null)
                {
                    return Ok(orderDetails);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
        // Get OrderDetails by customerID
        [HttpGet("{UserId}")]
        public async Task<ActionResult<List<OrderDetailDTO>>> GetOrderDetailByUserId(int userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user !=null)
                {
                    return NotFound("No user found for the specified ID.");
                }

                var orderDetails = await _context.OrderDetails
                    .Include(od => od.Order)
                    .Include(od => od.Product)
                    .Where(od => od.Order.UserId == userId)
                    .ToListAsync();

                if (orderDetails != null)
                {
                    var orderDetailDTOs = Mapping.Mapper.Map<List<OrderDetailDTO>>(orderDetails);
                    
                    orderDetailDTOs.ForEach(od =>
                    {
                        od.LineTotal = od.UnitPrice * od.Quantity * (1 - od.Discount/100);
                        od.Status = orderDetails.Find(o => o.Id == od.Id).Order.Status;
                        od.ProductName = orderDetails.Find(o => o.Id == od.Id).Product.Name;
                        od.ProductImage = orderDetails.Find(o => o.Id == od.Id).Product.Image;
                    });

                    return Ok(orderDetailDTOs);
                }
                else
                {
                    return NotFound("No order details found for the specified user.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching order details.");
            }
        }

    }
}
