using BrowserChat.Backend.Core.HubConfig;
using BrowserChat.Backend.Core.Util;
using BrowserChat.Entity;
using BrowserChat.Value;
using MassTransit;

namespace BrowserChat.Backend.Core.AsyncServices
{
    public class BotResponseConsumer : IConsumer<BotResponse>
    {
        public Task Consume(ConsumeContext<BotResponse> context)
        {
            if (ServiceCollectionHelper.Provider != null)
            {
                using (var scope = ServiceCollectionHelper.Provider.ApplicationServices.CreateScope())
                {
                    scope.ServiceProvider.GetService<HubHelper>()?
                        .PublishPost(
                            Constant.MessagesAndExceptions.Bot.BotName,
                            new Entity.DTO.PostPublishDTO
                            {
                                Message = context.Message.Message,
                                RoomId = context.Message.RoomId,
                                TimeStampStr = DateTime.Now.ToString(Constant.General.ConversionTimeFormat)
                            }
                        );
                }
            }

            return Task.CompletedTask;
        }
    }
}
