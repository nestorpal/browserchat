using BrowserChat.Bot.Util;
using BrowserChat.Entity;
using BrowserChat.Value;
using MassTransit;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

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
