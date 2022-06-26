using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Value
{
    public static class Constant
    {
        public static class General
        {
            public static readonly string ConversionTimeFormat = "HH:mm:ss";
        }

        public static class Samples
        {
            public static class BaseUser
            {
                public static readonly string Name = "Nestor";
                public static readonly string Surname = "Palacios";
                public static readonly string Email = "nestor.panu@gmail.com";
                public static readonly string UserName = "nestor.panu";
                public static readonly string Password = "Password!123";
            }

            public static readonly List<string> Rooms = new List<string> { "Programming", "Music", "Literature", "Science" };
            public static readonly string MessageTemplate = "Message #";
        }

        public static class QueueService
        {
            public static class ConfigurationParams
            {
                public static readonly string ExchangeMode = "trigger";
            }

            public static class QueueName
            {
                public static readonly string BotRequest = "BotRequest";
                public static readonly string BotResponse = "BotResponse";
            }
        }

        public static class HubService
        {
            public static class Events
            {
                public static readonly string AddToRoom = "AddToRoom";
                public static readonly string RemoveFromRoom = "RemoveFromRoom";
                public static readonly string ReceiveMessage = "ReceiveMessage";
            }
        }

        public static class ExceptionMessage
        {
            
        }
    }
}
