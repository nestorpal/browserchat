using BrowserChat.Value;
using System.Text.RegularExpressions;

namespace BrowserChat.Util
{
    public static class Bot
    {
        public static bool IsBotCommand(string message, out string command, out string value)
        {
            bool result = false;
            command = string.Empty;
            value = string.Empty;

            Regex regRule = new Regex("^/([a-zA-Z]+)=?(.*)");
            var match = regRule.Match(message);
            if (match.Success)
            {
                command = match.Groups[1].Value;
                value = match.Groups.Count > 2 ? match.Groups[2].Value : string.Empty;
                result = true;
            }

            return result;
        }

        public static bool IsValidCommand(string commandName, out BotCommandType command)
        {
            bool result = false;
            command = 0;
            try
            {
                command = (BotCommandType)(int)Enum.Parse(typeof(BotCommandType), commandName.Trim().ToUpper());
                result = true;
            }
            catch { }
            return result;
        }
    }
}
