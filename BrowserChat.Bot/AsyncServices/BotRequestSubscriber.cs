using BrowserChat.Bot.Util;
using BrowserChat.Value;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace BrowserChat.Bot.AsyncServices
{
    public class BotRequestSubscriber
    {
        private readonly ILogger<Worker> _logger;

        public BotRequestSubscriber(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public async void StartSubscription(CancellationToken stoppingToken, Action<string> methodToExecute)
        {
            var factory = new ConnectionFactory() { HostName = ConfigurationHelper.RabbitMQHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: Constant.QueueService.QueueName.BotRequest,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    methodToExecute(message);
                };

                channel.BasicConsume(
                    queue: Constant.QueueService.QueueName.BotRequest,
                    autoAck: true,
                    consumer: consumer
                );

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Listening queue {Constant.QueueService.QueueName.BotRequest} at: {DateTimeOffset.Now}");
                    await Task.Delay(5000, stoppingToken);
                }
            }
        }
    }
}
