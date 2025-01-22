using AlertManagement.Models;

namespace AlertManagement.Repositories.Interfaces
{
    public interface IFlightAlertRepository
    {
        Task<FlightAlert> CreateFlightAlert(FlightAlert flightAlert);
    }
}
