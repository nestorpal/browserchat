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

        public static class MessagesAndExceptions
        {
            public static class Bot
            {
                public static class Command
                {
                    public static class Stock
                    {
                        public static readonly string ValidResult = "{0} quote is ${1} per share";
                        public static readonly string QuoteDataNotDefined = "{0} quote data not defined";
                        public static readonly string QuoteNotAvailable = "Could not get quote value for {0}. Data structre may have changed";
                        public static readonly string InvalidValue = "{0} is not a valid value";
                    }
                }
            }

            public static class Client
            {
                public static readonly string IncompleteIncorrectData = "Incorrect or incomplete data";
            }

            public static class Security
            {
                public static readonly string UserNotFound = "User not found";
                public static readonly string IncorrectLogin = "Incorrect Login";
                public static readonly string EmailAlreadyExists = "The email provided already exists";
                public static readonly string UnerNameAlreadyExists = "The username provided already exists";
                public static readonly string CouldNotRegisterUser = "Couldn't register user";
            }
            public static class General
            {
                public static readonly string UnexpectedError = "Unexpected Error. Detail: {0}";
            }
        }
    }
}
