using BrowserChat.Entity;
using BrowserChat.Value;

namespace BrowserChat.Bot.Application.Strategy
{
    public interface ICommandStrategy
    {
        void ProcessCommand(BotRequest request);
    }
}
