using BrowserChat.Entity;
using BrowserChat.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
