using Edunext_API.Models;
using Edunext_API.Repositories.OrderRepository;
using Edunext_Model.DTOs;
using Edunext_Model.Mapper;
using Edunext_Model.Models;

namespace Edunext_API.Services
{
    using Orders = IEnumerable<Order>;

    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Orders> GetOrders()
        {
            return await orderRepository.GetOrders();
        }

        public async Task<Order?> GetOrderById(int id)
        {
            return await orderRepository.GetOrderByID(id);
        }

        public async Task Save()
        {
            await orderRepository.Save();
        }

        public async Task<bool> IsExisted(int id)
        {
            return await orderRepository.IsExisted(id);
        }
    }
}
