using AlertManagement.Models;
using AlertManagement.Repositories.Interfaces;

namespace AlertManagement.ExternalServices
{
    public class AlertService
    {
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        private readonly RabbitMqService _rabbitMqService;

        public AlertService(IUserPreferenceRepository userPreferenceRepository, RabbitMqService rabbitMqService)
        {
            _userPreferenceRepository = userPreferenceRepository;
            _rabbitMqService = rabbitMqService;
        }

        public async Task ProcessFlightAlert(FlightAlert flightAlert)
        {
            List<UserPreference> preferences = await _userPreferenceRepository.GetRelevantUserPreferences(flightAlert);

            foreach (UserPreference preference in preferences)
            {
                var notification = new NotificationMessage
                {
                    UserId = preference.UserId,
                    FlightDetails = $"{flightAlert.Origin} to {flightAlert.Destination} on {flightAlert.DepartureDate.ToShortDateString()}",
                    Price = flightAlert.Price,
                    AlertTime = DateTime.UtcNow
                };

                _rabbitMqService.PublishMessage(notification, "user_alerts");
            }
        }
    }

}
