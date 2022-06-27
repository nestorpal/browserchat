using BrowserChat.Entity;

namespace BrowserChat.Bot.Application.Strategy
{
    public class BotContext
    {
        private ICommandStrategy? _strategy;

        public void SetStrategy(ICommandStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecuteStrategy(BotRequest request)
        {
            if (_strategy != null)
            {
                _strategy.ProcessCommand(request);
            }
        }
    }
}
