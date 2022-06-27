using BrowserChat.Util;
using BrowserChat.Value;

namespace BrowserChat.Test.BrowserChat.Util
{
    public class BotCommandTest
    {
        [Theory]
        [InlineData("/stock=aapl.us")]
        [InlineData("/stock  = aapl.us")]
        [InlineData("/  stock  =aapl.us  ")]
        [InlineData("/stockcompany")]
        [InlineData("/stockcompany  ")]
        public void VerifyIsBotCommand(string input)
        {
            Assert.True(Bot.IsBotCommand(input, out string command, out string value));
            Assert.NotEmpty(command);
        }

        [Theory]
        [InlineData("/+++=aapl.us")]
        [InlineData("/stock999  = aapl.us")]
        [InlineData("/=aapl.us")]
        [InlineData("/=")]
        public void VerifyIsNotBotCommand(string input)
        {
            Assert.False(Bot.IsBotCommand(input, out string command, out string value));
        }

        [Theory]
        [InlineData("stock")]
        [InlineData("stockcompany")]
        public void VerifyIsValidCommand(string commandName)
        {
            Assert.True(Bot.IsValidCommand(commandName, out BotCommandType command));
        }
    }
}
