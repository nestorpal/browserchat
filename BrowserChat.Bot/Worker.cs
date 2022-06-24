using BrowserChat.Bot.Application;
using BrowserChat.Bot.AsyncServices;

namespace BrowserChat.Bot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly BotRequestSubscriber _subscriber;
        private readonly Processor _processor;

        public Worker(
            ILogger<Worker> logger,
            BotRequestSubscriber subscriber,
            Processor processor)
        {
            _logger = logger;
            _subscriber = subscriber;
            _processor = processor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => _subscriber.StartSubscription(stoppingToken, ProcessBotRequest));
        }

        private void ProcessBotRequest(string payload)
        {
            _processor.Process(payload);
        }
    }
}