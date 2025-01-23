using AlertManagement.Models;

namespace AlertManagement.Repositories.Interfaces
{
    public interface IUserPreferenceRepository
    {
        Task<UserPreference> CreateUserPreferences(UserPreference preference);
        Task<UserPreference?> GetUserPreferenceById(int id);
        Task<List<UserPreference>> GetAllUserPreferences(int userId);
        Task<bool> UpdateUserPreferences(UserPreference updatedPreference);
        Task<bool> DeleteUserPreference(int id);
        Task<List<UserPreference>> GetRelevantUserPreferences(FlightAlert alert);
    }
}
