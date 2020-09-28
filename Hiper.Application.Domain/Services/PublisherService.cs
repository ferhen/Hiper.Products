using RabbitMQ.Client;
using System.Text;

namespace Hiper.Application.Domain.Services
{
    public class PublisherService
    {
        private readonly ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq" };

        public void Publish(string message)
        {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "productQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "productQueue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
