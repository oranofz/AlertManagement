using AlertManagement.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AlertManagement.ExternalServices
{
    public class FlightAlertConsumer : BackgroundService
    {
        private readonly string _hostname = "localhost";
        private readonly string _flightAlertsQueue = "flight_alerts";
        private readonly AlertService _alertService;

        public FlightAlertConsumer(AlertService alertService)
        {
            _alertService = alertService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = _hostname };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel model = connection.CreateModel())
                {
                    model.QueueDeclare(queue: _flightAlertsQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(model);
                    consumer.Received += async (model, ea) =>
                    {
                        if (stoppingToken.IsCancellationRequested)
                        {
                            return;
                        }

                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var flightAlert = JsonConvert.DeserializeObject<FlightAlert>(message);

                        if (flightAlert != null)
                        {
                            await _alertService.ProcessFlightAlert(flightAlert);
                        }
                    };

                    model.BasicConsume(queue: _flightAlertsQueue, autoAck: true, consumer: consumer);

                    await Task.Delay(-1, stoppingToken);
                }
            }
        }
    }
}
