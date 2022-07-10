using BrowserChat.Bot.Application;
using BrowserChat.Bot.AsyncServices;
using BrowserChat.Bot.Util;
using BrowserChat.Value;

namespace BrowserChat.Bot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(
            IServiceProvider serviceProvider,
            ILogger<Worker> logger,
            IConfiguration config)
        {
            _logger = logger;
            ConfigurationHelper.Initialize(config);
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