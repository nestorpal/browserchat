using BrowserChat.Bot.AsyncServices;
using BrowserChat.Entity;
using BrowserChat.Value;

namespace BrowserChat.Bot.Application.Strategy
{
    public class StockCompanyCommand : CommandBase, ICommandStrategy
    {
        public StockCompanyCommand(BotResponsePublisher publisher) : base(publisher)
        {
        }

        public void ProcessCommand(BotRequest request)
        {
            List<string> result = Constant.Samples.StockCompany; // mock source

            Publish(
                new BotResponse
                {
                    RoomId = request.RoomId,
                    Message = String.Join(", ", result),
                    CommandError = false,
                    CommandErrorType = 0
                }
            );
        }
    }
}
