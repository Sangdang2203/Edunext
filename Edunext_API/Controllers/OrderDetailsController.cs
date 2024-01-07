using Edunext_API.Helpers;
using Edunext_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Edunext_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailService orderDetailServices;

        public OrderDetailsController(OrderDetailService orderDetailServices)
        {
            this.orderDetailServices = orderDetailServices;
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
    }
}
