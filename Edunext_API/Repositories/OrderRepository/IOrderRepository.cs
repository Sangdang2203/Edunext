using Edunext_Model.Models;

namespace Edunext_API.Repositories.OrderRepository
{
    using Orders = IEnumerable<Order>;

    public interface IOrderRepository
    {
        Task<Orders> GetOrders();
        Task<Order> GetOrderByID(int id);
        void UpdateOrder(Order order);
        Task Save();
        Task<bool> IsExisted(int id);
/*        Task InsertOrder(Order order);
        void DeleteOrder(int id);*/
    }
}
