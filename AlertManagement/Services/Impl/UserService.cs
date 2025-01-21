using AlertManagement.Models;
using AlertManagement.Repositories.Interfaces;
using AlertManagement.Services.Interfaces;

namespace AlertManagement.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(User user)
        {
            User createdUser = await _userRepository.CreateUser(user);
            return createdUser;
        }

        public async Task<User> GetUser(int id)
        {
            User user = await _userRepository.GetUserById(id);
            return user;
        }

        public async Task<bool> UpdateUser(User updatedUser)
        {
            bool isUpdatedUser = await _userRepository.UpdateUser(updatedUser);
            return isUpdatedUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            bool isDeletedUser = await _userRepository.DeleteUser(id);
            return isDeletedUser;
        }
    }
}

