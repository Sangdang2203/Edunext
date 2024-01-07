using Edunext_API.Models;
using Edunext_Model.DTOs;
using Edunext_Model.Mapper;
using Edunext_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Repositories.OrderRepository
{
    using Orders = IEnumerable<Order>;

    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Orders> GetOrders()
        {
            return await databaseContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByID(int id)
        {
            return await databaseContext.Orders.FindAsync(id);
        }

        public void UpdateOrder(Order order)
        {
            databaseContext.Entry(order).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await databaseContext.SaveChangesAsync();
        }

        public async Task<bool> IsExisted(int id)
        {
            return await databaseContext.Orders.AnyAsync(order => order.Id == id);
        }

        /*        public async Task InsertOrder(Order order)
                {
                    await databaseContext.AddAsync(order);
                }

                public async void DeleteOrder(int id)
                {
                    Order order = await databaseContext.Orders.FindAsync(id);
                    databaseContext.Orders.Remove(order);
                }*/
    }
}
