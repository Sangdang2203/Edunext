using Edunext_Model.Models;

namespace Edunext_API.Repositories.OrderRepository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByID(int id);
        Task InsertUser(User user);
        void DeleteUser(int id);
        void UpdateUser(User user);
    }
}
