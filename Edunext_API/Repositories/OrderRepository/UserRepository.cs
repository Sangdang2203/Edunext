using Edunext_API.Models;
using Edunext_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Edunext_API.Repositories.OrderRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext databaseContext;
        public UserRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await databaseContext.Users.ToListAsync();
            return users;
        }
        public async Task<User> GetUserByID(int id)
        {
            var user = await databaseContext.Users.FindAsync(id);
            return user;
        }
        public async Task InsertUser(User user)
        {
            await databaseContext.Users.AddAsync(user);
        }
        public async void DeleteUser(int id)
        {
            User user = await databaseContext.Users.FindAsync(id);
            databaseContext.Users.Remove(user);
        }
        public void UpdateUser(User user)
        {
            databaseContext.Entry(user).State = EntityState.Modified;
        }
    }
}
