using Edunext_API.Helpers;
using Edunext_API.Services;
using Edunext_Model.DTOs.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Edunext_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ShoppingCartService service;

        public ShoppingCartController(ShoppingCartService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> CreateOrder(ShoppingCartDTO shoppingCart)
        {
            await service.ConfirmOrder(shoppingCart);

            return Ok(null);
        }

//        [HttpGet]
        //public async Task<IEnumerable<>>
    }
}
