using BrowserChat.Backend.Core.HubConfig;
using BrowserChat.Entity;
using BrowserChat.Value;
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
        private IConnection? _conn;
        private IModel? _channel;

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
                exchange: Constant.QueueService.ConfigurationParams.ExchangeMode,
                type: ExchangeType.Fanout
            );

            _channel.QueueDeclare(
                    queue: Constant.QueueService.QueueName.BotResponse,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

            _channel.QueueBind(
                queue: Constant.QueueService.QueueName.BotResponse,
                exchange: Constant.QueueService.ConfigurationParams.ExchangeMode,
                routingKey: string.Empty
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
                string notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                BotResponse? response = null;
                try
                {
                    response = JsonSerializer.Deserialize<BotResponse>(notificationMessage);
                }
                catch { }

                if (response != null)
                {
                    Task.Run(
                        async () =>
                        {
                            await _hubHelper.PublishPost(
                                Constant.MessagesAndExceptions.Bot.BotName,
                                new Entity.DTO.PostPublishDTO
                                {
                                    Message = response.Message,
                                    RoomId = response.RoomId,
                                    TimeStampStr = DateTime.Now.ToString(Constant.General.ConversionTimeFormat)
                                }
                            );
                        }
                    );
                }
            };

            _channel.BasicConsume(
                queue: Constant.QueueService.QueueName.BotResponse,
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
            if (_channel != null
                && _channel.IsOpen)
            {
                _channel.Close();
                if (_conn != null)
                {
                    _conn.Close();
                }
            }

            base.Dispose();
        }
    }
}
