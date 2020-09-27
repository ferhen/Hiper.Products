using Hiper.SynchronizationAPI.Presentation.DTO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hiper.SynchronizationAPI.Domain.Services
{
    public class ConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IConnection _connection;
        private IModel _channel;

        public ConsumerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            //var factory = new ConnectionFactory { HostName = "rabbitmq" };
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "productQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                await HandleMessage(content);
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(queue: "productQueue",
                                         autoAck: true,
                                         consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task HandleMessage(string content)
        {
            var productSnapshotDTO = JsonConvert.DeserializeObject<ProductSnapshotDTO>(content);

            using var scope = _serviceScopeFactory.CreateScope();
            var productSnapshotService = scope.ServiceProvider.GetRequiredService<ProductSnapshotService>();

            await productSnapshotService.Save(productSnapshotDTO);
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
