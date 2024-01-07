using Edunext_API.Repositories.OrderRepository;
using Edunext_Model.Models;

namespace Edunext_API.Services
{
    public class UserServices
    {
        private readonly IUserRepository userRepository;

        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await userRepository.GetUsers();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await userRepository.GetUserByID(id);
        }

        public async Task<User> AddUser (User user)
        {
            await userRepository.InsertUser(user);
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            userRepository.UpdateUser(user);
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            User user = await userRepository.GetUserByID(id);
            userRepository.DeleteUser(id);
            return user;
        }

    }
}
