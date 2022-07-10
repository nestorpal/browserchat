using BrowserChat.Bot;
using BrowserChat.Bot.Application;
using BrowserChat.Bot.AsyncServices;
using BrowserChat.Bot.Util;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        ConfigurationHelper.Initialize(hostContext.Configuration);

        services.AddTransient(typeof(Processor));
        services.AddTransient(typeof(BotResponsePublisher));

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(ConfigurationHelper.RabbitMQHost, "/");

                cfg.ReceiveEndpoint(BrowserChat.Value.Constant.QueueService.QueueName.BotRequest, e =>
                {
                    e.Durable = true;
                    e.Consumer<BotRequestConsumer>();
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
