using BrowserChat.Entity;

namespace BrowserChat.Bot.Application.Strategy
{
    public interface ICommandStrategy
    {
        /// <summary>
        /// Processes a request based on a command
        /// </summary>
        /// <param name="request"></param>
        void ProcessCommand(BotRequest request);
    }
}
