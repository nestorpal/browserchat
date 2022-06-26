using BrowserChat.Entity;
using BrowserChat.Value;
using BrowserChat.Util;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BrowserChat.Backend.Core.AsyncServices
{
    public class BotRequestPublisher
    {
        public void Publish(BotRequest request)
        {
            var factory = new ConnectionFactory() { HostName = Util.ConfigurationHelper.config["RabbitMQHost"] };
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

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: Constant.QueueService.QueueName.BotRequest,
                    basicProperties: null,
                    body: body
                );
            }
        }
    }
}
