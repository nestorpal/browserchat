using BrowserChat.Entity;
using BrowserChat.Value;
using MassTransit;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

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
