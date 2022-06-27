using BrowserChat.Bot.Application;
using BrowserChat.Bot.AsyncServices;
using BrowserChat.Bot.Util;

namespace BrowserChat.Bot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BotRequestSubscriber _subscriber;
        private readonly Processor _processor;

        public Worker(
            ILogger<Worker> logger,
            IConfiguration config,
            BotRequestSubscriber subscriber,
            Processor processor)
        {
            _logger = logger;
            _subscriber = subscriber;
            _processor = processor;

            ConfigurationHelper.Initialize(config);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => _subscriber.StartSubscription(stoppingToken, ProcessBotRequest));
        }

        private void ProcessBotRequest(string payload)
        {
            _processor.Process(payload);
        }
    }
}