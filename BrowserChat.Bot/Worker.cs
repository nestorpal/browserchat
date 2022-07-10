using BrowserChat.Bot.Util;

namespace BrowserChat.Bot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(
            IServiceProvider serviceProvider,
            ILogger<Worker> logger)
        {
            _logger = logger;
            ServiceCollectionHelper.Initialize(serviceProvider);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}