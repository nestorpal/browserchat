using BrowserChat.Entity;
using MassTransit;

namespace BrowserChat.Bot.AsyncServices
{
    public class BotResponsePublisher
    {
        private readonly IBus _bus;

        public BotResponsePublisher(IBus bus)
        {
            _bus = bus;
        }

        public async void Publish(BotResponse response)
        {
            var endpoint =
                await _bus.GetSendEndpoint(
                    new Uri($"queue:{BrowserChat.Value.Constant.QueueService.QueueName.BotResponse}")
                );

            await endpoint.Send(response);
        }
    }
}
