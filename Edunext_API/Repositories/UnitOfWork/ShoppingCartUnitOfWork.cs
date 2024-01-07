using Edunext_API.Models;
using Edunext_API.Repositories.GenericRepository;
using Edunext_Model.Models;

namespace Edunext_API.Repositories.UnitOfWork
{
    public class ShoppingCartUnitOfWork : IShoppingCartUnitOfWork
    {
        private readonly DatabaseContext databaseContext;

        public ShoppingCartUnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            OrderRepository = new GenericRepository<Order>(databaseContext);
            OrderDetailRepository = new GenericRepository<OrderDetail>(databaseContext);
        }

        public async Task BeginTransaction()
        {
            await databaseContext.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransaction()
        {
            await databaseContext.Database.RollbackTransactionAsync();
        }

        public async Task Save()
        {
            await databaseContext.SaveChangesAsync();
        }

        public IGenericRepository<Order> OrderRepository { get; }

        public IGenericRepository<OrderDetail> OrderDetailRepository { get; }

    }
}
