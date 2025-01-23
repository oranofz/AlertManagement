using AlertManagement.Models;
using AlertManagement.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlertManagement.Repositories.Impl
{
    public class UserPreferenceRepository : IUserPreferenceRepository
    {
        private readonly AppDbContext _context;

        public UserPreferenceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserPreference> CreateUserPreferences(UserPreference preference)
        {
            await _context.UserPreferences.AddAsync(preference);
            await _context.SaveChangesAsync();
            return preference;
        }

        public async Task<UserPreference?> GetUserPreferenceById(int id)
        {
            UserPreference? preference = await _context.UserPreferences.FirstOrDefaultAsync(up => up.Id == id);
            return preference;
        }

        public async Task<List<UserPreference>> GetAllUserPreferences(int userId)
        {
            List<UserPreference> preferences = await _context.UserPreferences.Where(up => up.UserId == userId).ToListAsync();
            return preferences;
        }

        public async Task<bool> UpdateUserPreferences(UserPreference updatedPreferences)
        {
            var preferences = await _context.UserPreferences
                                             .FirstOrDefaultAsync(up => up.UserId == updatedPreferences.UserId);
            if (preferences == null)
            {
                return false;
            }

            preferences.MinPrice = updatedPreferences.MinPrice;
            preferences.MaxPrice = updatedPreferences.MaxPrice;
            preferences.Origin = updatedPreferences.Origin;
            preferences.Destination = updatedPreferences.Destination;

            _context.UserPreferences.Update(preferences);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserPreference(int id)
        {
            UserPreference? preferences = await _context.UserPreferences
                                             .FirstOrDefaultAsync(up => up.Id == id);
            if (preferences == null)
            {
                return false;
            }
            _context.UserPreferences.Remove(preferences);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserPreference>> GetRelevantUserPreferences(FlightAlert alert)
        {
            List<UserPreference> relevantPreferences = await _context.UserPreferences
                .Where(up => up.Origin == alert.Origin &&
                       up.Destination == alert.Destination &&
                       up.MaxPrice >= alert.Price &&
                       up.MinPrice <= alert.Price).ToListAsync();
            return relevantPreferences;

        }
    }
}
