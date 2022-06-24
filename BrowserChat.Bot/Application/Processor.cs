using BrowserChat.Bot.Application.Strategy;
using BrowserChat.Bot.AsyncServices;
using BrowserChat.Entity;
using BrowserChat.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrowserChat.Bot.Application
{
    public class Processor
    {
        private readonly IConfiguration _config;
        private readonly BotResponsePublisher _publisher;

        public Processor(IConfiguration config, BotResponsePublisher publisher)
        {
            _config = config;
            _publisher = publisher;
        }

        public void Process(string payload)
        {
            var request = JsonSerializer.Deserialize<BotRequest>(payload);
            if (request != null)
            {
                BotContext context = new BotContext();

                if (ValidateCommand(request.Command, out BotCommandType command))
                {
                    switch (command)
                    {
                        case BotCommandType.STOCK:
                            {
                                context.SetStrategy(new StockCommand(_config, _publisher));
                                break;
                            }
                    }
                }
                else
                {
                    context.SetStrategy(new InvalidCommand(_publisher));
                }

                context.ExecuteStrategy(request);
            }
        }

        private bool ValidateCommand(string commandName, out BotCommandType command)
        {
            bool result = false;
            command = 0;
            try
            {
                command = (BotCommandType)(int)Enum.Parse(typeof(BotCommandType), PreProcessCommandString(commandName));
                result = true;
            }
            catch { }
            return result;
        }

        private string PreProcessCommandString(string commandName)
        {
            return commandName.Trim().ToUpper();
        }
    }
}
