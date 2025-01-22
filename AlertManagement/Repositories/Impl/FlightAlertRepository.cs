using AlertManagement.Models;
using AlertManagement.Repositories.Interfaces;

namespace AlertManagement.Repositories.Impl
{
    public class FlightAlertRepository : IFlightAlertRepository
    {

        private readonly AppDbContext _context;

        public FlightAlertRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<FlightAlert> CreateFlightAlert(FlightAlert flightAlert)
        {
            await _context.FlightAlerts.AddAsync(flightAlert);
            await _context.SaveChangesAsync();
            return flightAlert;
        }
    }
}
