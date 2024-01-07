using Edunext_API.Repositories.UnitOfWork;
using Edunext_Model.DTOs.Cart;
using Edunext_Model.Mapper;
using Edunext_Model.Models;

namespace Edunext_API.Services
{
    public class ShoppingCartService
    {
        private readonly IShoppingCartUnitOfWork uow;

        public ShoppingCartService(IShoppingCartUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task ConfirmOrder(ShoppingCartDTO shoppingCart)
        {
            await uow.BeginTransaction();
            
            Order order = Mapping.Mapper.Map<Order>(shoppingCart.Order);
            await uow.OrderRepository.InsertEntity(order);
            await uow.Save();

            IEnumerable<OrderDetail> orderDetails = Mapping.Mapper.Map<IEnumerable<OrderDetail>>(shoppingCart.OrderDetails);

            foreach (OrderDetail orderdetail in orderDetails)
            {
                orderdetail.OrderId = order.Id;
                await uow.OrderDetailRepository.InsertEntity(orderdetail);
            }
            await uow.Save();
        }
    }
}
