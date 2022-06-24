using BrowserChat.Backend.Core.HubConfig;
using BrowserChat.Entity;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BrowserChat.Backend.Core.AsyncServices
{
    public class BotResponseSubscriber : BackgroundService
    {
        private readonly IConfiguration _config;
        private readonly HubHelper _hubHelper;
        private IConnection _conn;
        private IModel _channel;
        private readonly string _queueName = "BotResponse";

        public BotResponseSubscriber(
            IConfiguration config,
            HubHelper hubHelper)
        {
            _config = config;
            _hubHelper = hubHelper;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQHost"],
                Port = int.Parse(_config["RabbitMQPort"])
            };

            _conn = factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.ExchangeDeclare(
                exchange: "trigger",
                type: ExchangeType.Fanout
            );

            _channel.QueueDeclare(
                    queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            _channel.QueueBind(
                queue: _queueName,
                exchange: "trigger",
                routingKey: ""
            );

            _conn.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                BotResponse response = JsonSerializer.Deserialize<BotResponse>(notificationMessage);
                Task.Run(
                    async () =>
                    {
                        await _hubHelper.PublishPost(
                            "bot",
                            new Entity.DTO.PostPublishDTO
                            {
                                Message = response.Message,
                                RoomId = response.RoomId,
                                TimeStampStr = DateTime.Now.ToString("HH:mm:ss")
                            }
                        );
                    }
                );
            };

            _channel.BasicConsume(
                queue: _queueName,
                autoAck: true,
                consumer: consumer
            );

            return Task.CompletedTask;
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _conn.Close();
            }

            base.Dispose();
        }
    }
}
