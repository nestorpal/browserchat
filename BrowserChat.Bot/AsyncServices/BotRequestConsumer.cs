using BrowserChat.Bot.Application;
using BrowserChat.Bot.Util;
using BrowserChat.Entity;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Bot.AsyncServices
{
    public class BotRequestConsumer : IConsumer<BotRequest>
    {
        public Task Consume(ConsumeContext<BotRequest> context)
        {
            if (ServiceCollectionHelper._provider != null)
            {
                using (var scope = ServiceCollectionHelper._provider.CreateScope())
                {
                    scope.ServiceProvider.GetService<Processor>()?.Process(context.Message);
                }
            }

            return Task.CompletedTask;
        }
    }
}
