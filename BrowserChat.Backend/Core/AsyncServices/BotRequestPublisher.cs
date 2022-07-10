using BrowserChat.Entity;
using MassTransit;

namespace BrowserChat.Backend.Core.AsyncServices
{
    public class BotRequestPublisher
    {
        private readonly IBus _bus;

        public BotRequestPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async void Publish(BotRequest request)
        {
            var endpoint =
                await _bus.GetSendEndpoint(
                    new Uri($"queue:{BrowserChat.Value.Constant.QueueService.QueueName.BotRequest}")
                );

            await endpoint.Send(request);
        }
    }
}
