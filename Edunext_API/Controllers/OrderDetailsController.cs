using Edunext_API.Models;
using Edunext_Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public OrderDetailsController(DatabaseContext context)
        {
            _context = context;
        }
        // Get all OrderDetails
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var orderDetails = await _context.OrderDetails.ToListAsync();
                if (orderDetails == null)
                {
                    return NotFound();
                }
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // Get OrderDetails by UserId
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetByUserId(int UserId)
        {
            try
            {
                var orderDetails = await _context.OrderDetails
                    .Include(od => od.Order)
                    .Where(od => od.Order.UserId == UserId)
                    .ToListAsync();

                if (orderDetails == null)
                {
                    return NotFound();
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // Get OrderDetails by OrderId
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            try
            {
                var orderDetails = await _context.OrderDetails
                    .Where(od => od.OrderID == orderId)
                    .ToListAsync();

                if (orderDetails == null)
                {
                    return NotFound();
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // Get OrderDetails by CreatedDate
        [HttpGet("{createdDate}")]
        public async Task<IActionResult> GetByCreatedDate(DateTime createdDate)
        {
            try
            {
                var orderDetails = await _context.OrderDetails
                    .Where(od => od.CreatedDate >= createdDate)
                    .ToListAsync();

                if (orderDetails == null)
                {
                    return NotFound();
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // Get OrderDetails by ProductId
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            try
            {
                var orderDetails = await _context.OrderDetails
                    .Where(od => od.ProductID == productId)
                    .ToListAsync();

                if (orderDetails == null)
                {
                    return NotFound();
                }

                return Ok(orderDetails);
            }

            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        // Get OrderDetails by CategoryId (assuming a relationship with Product and Category)
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            try
            {
                var orderDetails = await _context.OrderDetails
                    .Include(od => od.Product) // Include Product to access CategoryID
                    .Where(od => od.Product.CategoryID == categoryId)
                    .ToListAsync();

                if (orderDetails == null)
                {
                    return NotFound();
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
