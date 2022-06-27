using BrowserChat.Entity;

namespace BrowserChat.Bot.Application.Strategy
{
    public interface ICommandStrategy
    {
        void ProcessCommand(BotRequest request);
    }
}
