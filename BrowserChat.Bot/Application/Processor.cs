using BrowserChat.Bot.Application.Strategy;
using BrowserChat.Bot.AsyncServices;
using BrowserChat.Entity;
using BrowserChat.Value;
using System.Text.Json;

namespace BrowserChat.Bot.Application
{
    public class Processor
    {
        private readonly BotResponsePublisher _publisher;

        public Processor(BotResponsePublisher publisher)
        {
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
                        case BotCommandType.STOCKCOMPANY:
                            {
                                context.SetStrategy(new StockCompanyCommand(_publisher));
                                break;
                            }
                        case BotCommandType.STOCK:
                            {
                                context.SetStrategy(new StockCommand(_publisher));
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
