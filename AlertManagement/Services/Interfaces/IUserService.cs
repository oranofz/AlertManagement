using AlertManagement.Models;

namespace AlertManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
        Task<User> GetUser(int id);
        Task<bool> UpdateUser(User updatedUser);
        Task<bool> DeleteUser(int id);
    }
}
