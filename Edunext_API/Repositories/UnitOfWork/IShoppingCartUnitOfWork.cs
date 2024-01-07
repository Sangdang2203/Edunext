using Edunext_API.Repositories.GenericRepository;
using Edunext_Model.Models;

namespace Edunext_API.Repositories.UnitOfWork
{
    public interface IShoppingCartUnitOfWork
    {
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<OrderDetail> OrderDetailRepository { get; }

        Task BeginTransaction();
        Task RollbackTransaction();
        Task Save();
    }
}
