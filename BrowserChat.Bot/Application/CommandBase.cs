using BrowserChat.Bot.AsyncServices;
using BrowserChat.Entity;

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
