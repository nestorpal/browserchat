using BrowserChat.Entity;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrowserChat.Bot.AsyncServices
{
    public class BotResponsePublisher
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "BotResponse";

        public BotResponsePublisher(IConfiguration config)
        {
            _config = config;
        }

        public void Publish(BotResponse request)
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
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request));

                channel.BasicPublish(
                    exchange: "",
                    routingKey: _queueName,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
