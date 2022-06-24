using BrowserChat.Bot;
using BrowserChat.Bot.Application;
using BrowserChat.Bot.AsyncServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient(typeof(Processor));
        services.AddSingleton(typeof(BotRequestSubscriber));
        services.AddSingleton(typeof(BotResponsePublisher));
    })
    .Build();

await host.RunAsync();
