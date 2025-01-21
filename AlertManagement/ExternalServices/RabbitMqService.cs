using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AlertManagement.ExternalServices
{
    public class RabbitMqService
    {
        private readonly string _hostname = "localhost:5672";
        private IConnection _connection;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
        }

        public void PublishMessage<T>(T message, string queueName)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "",
                                    routingKey: queueName,
                                    basicProperties: null,
                                    body: body);
            }
        }
    }

}
