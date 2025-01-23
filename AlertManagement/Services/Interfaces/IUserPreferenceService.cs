using AlertManagement.Models;

namespace AlertManagement.Services.Interfaces
{
    public interface IUserPreferenceService
    {
        Task<UserPreference> CreateUserPreference(UserPreference preference);
        Task<bool> UpdateUserPreference(UserPreference preference);
        Task<bool> DeleteUserPreference(int id);
        Task<UserPreference?> GetUserPreference(int id);
        Task<List<UserPreference>> GetAllUserPreferences(int userId);

    }
}
