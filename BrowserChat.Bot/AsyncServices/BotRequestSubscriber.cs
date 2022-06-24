using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Bot.AsyncServices
{
    public class BotRequestSubscriber
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly string _queueName = "BotRequest";

        public BotRequestSubscriber(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async void StartSubscription(CancellationToken stoppingToken, Action<string> methodToExecute)
        {
            var factory = new ConnectionFactory() { HostName = _config["RabbitMQHost"] };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _queueName,
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
                    queue: _queueName,
                    autoAck: true,
                    consumer: consumer
                );

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"Listening queue {_queueName} at: {DateTimeOffset.Now}");
                    await Task.Delay(5000, stoppingToken);
                }
            }
        }
    }
}
