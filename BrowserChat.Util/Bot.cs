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

            try
            {
                Regex? regRule = null;
                Match? match = null;

                // command value format
                regRule = new Regex("^/([a-zA-Z\\s?]+)=(.*)");
                match = regRule.Match(message);
                if (match.Success)
                {
                    command = match.Groups[^2].Value.Trim();
                    value = match.Groups.Count > 2 ? ((match.Groups[^1].Value ?? string.Empty).Trim()) : string.Empty;
                    result = true;
                    return result;
                }

                // simple command format
                regRule = new Regex("^/([a-zA-Z\\s?]+)$");
                match = regRule.Match(message);
                if (match.Success)
                {
                    command = match.Groups.Values.Where(v => !string.IsNullOrEmpty(v.Value)).Select(v => v.Value).Last();
                    value = string.Empty;
                    result = true;
                    return result;
                }
            }
            catch { }

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
