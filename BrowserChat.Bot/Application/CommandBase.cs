using BrowserChat.Bot.AsyncServices;
using BrowserChat.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Bot.Application
{
    public class CommandBase
    {
        private readonly BotResponsePublisher _publisher;

        public CommandBase(BotResponsePublisher publisher)
        {
            _publisher = publisher;
        }

        public void Publish(BotResponse response)
        {
            _publisher.Publish(response);
        }
    }
}
