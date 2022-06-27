using BrowserChat.Bot.Util;
using BrowserChat.Entity;
using BrowserChat.Value;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BrowserChat.Bot.AsyncServices
{
    public class BotResponsePublisher
    {
        public void Publish(BotResponse request)
        {
            var factory = new ConnectionFactory() { HostName = ConfigurationHelper.RabbitMQHost };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: Constant.QueueService.QueueName.BotResponse,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));

                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: Constant.QueueService.QueueName.BotResponse,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}
