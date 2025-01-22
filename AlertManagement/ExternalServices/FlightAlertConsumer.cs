using AlertManagement.Models;
using AlertManagement.Repositories.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AlertManagement.ExternalServices
{
    public class FlightAlertConsumer : BackgroundService
    {
        private readonly string _hostname = "localhost";
        private readonly int _port = 5672;
        private readonly string _flightAlertsQueue = "flight_alerts";
        private readonly IServiceProvider _serviceProvider;

        public FlightAlertConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = _hostname, Port = _port };

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
                            using (var scope = _serviceProvider.CreateScope())
                            {
                                IFlightAlertRepository flightAlertRepository = scope.ServiceProvider.GetRequiredService<IFlightAlertRepository>();
                                flightAlert = await flightAlertRepository.CreateFlightAlert(flightAlert);
                                AlertService alertService = scope.ServiceProvider.GetRequiredService<AlertService>();
                                await alertService.ProcessFlightAlert(flightAlert);
                            }
                        }
                    };

                    model.BasicConsume(queue: _flightAlertsQueue, autoAck: true, consumer: consumer);

                    await Task.Delay(-1, stoppingToken);
                }
            }
        }
    }
}
