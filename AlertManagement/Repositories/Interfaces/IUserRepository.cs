using AlertManagement.Models;

namespace AlertManagement.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUserById(int id);
        Task<bool> UpdateUser(User updatedUser);
        Task<bool> DeleteUser(int id);
    }
}
