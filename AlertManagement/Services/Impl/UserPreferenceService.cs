using AlertManagement.Models;
using AlertManagement.Repositories.Interfaces;
using AlertManagement.Services.Interfaces;

namespace AlertManagement.Services.Impl
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;

        public UserPreferenceService(IUserPreferenceRepository userPreferenceRepository)
        {
            _userPreferenceRepository = userPreferenceRepository;
        }
        public async Task<UserPreference> CreateUserPreference(UserPreference preference)
        {
            UserPreference userPreference = await _userPreferenceRepository.CreateUserPreferences(preference);
            return userPreference;
        }

        public async Task<bool> DeleteUserPreference(int id)
        {
            bool isDeletedPreference = await _userPreferenceRepository.DeleteUserPreference(id);
            return isDeletedPreference;
        }

        public async Task<bool> UpdateUserPreference(UserPreference preference)
        {
            bool isUpdatedPreference = await _userPreferenceRepository.UpdateUserPreferences(preference);
            return isUpdatedPreference;
        }

        public async Task<List<UserPreference>> GetAllUserPreferences(int userId) {
            List<UserPreference> preferences = await _userPreferenceRepository.GetAllUserPreferences(userId);
            return preferences;
        }

        public async Task<UserPreference> GetUserPreference(int id)
        {
            UserPreference preference = await _userPreferenceRepository.GetUserPreferenceById(id);
            return preference;
        }
    }
}
