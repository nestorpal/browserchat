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

                if (BrowserChat.Util.Bot.IsBotCommand(request.Command, out string verifiedCommand, out string value)
                    && BrowserChat.Util.Bot.IsValidCommand(verifiedCommand, out BotCommandType command))
                {
                    request.Value = value;

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

        public void Process(BotRequest request)
        {
            if (request != null)
            {
                BotContext context = new BotContext();

                if (BrowserChat.Util.Bot.IsBotCommand(request.Command, out string verifiedCommand, out string value)
                    && BrowserChat.Util.Bot.IsValidCommand(verifiedCommand, out BotCommandType command))
                {
                    request.Value = value;

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
    }
}
