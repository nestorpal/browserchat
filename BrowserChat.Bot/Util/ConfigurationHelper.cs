using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Bot.Util
{
    public class ConfigurationHelper
    {
        private static IConfiguration? _config;

        public static void Initialize(IConfiguration Configuration)
        {
            _config = Configuration;
        }

        private static string GetValue(string key)
        {
            string result = string.Empty;
            if (_config != null)
            {
                if (key.Contains("/"))
                {
                    result = _config.GetSection(key.Split('/')[0]).GetValue<string>(key.Split('/')[1]);
                }
                else
                {
                    result = _config[key];
                }
            }
            return result;
        }

        public static string RabbitMQHost { get { return GetValue("RabbitMQHost"); } }
        public static string RabbitMQPort { get { return GetValue("RabbitMQPort"); } }
        public static string StockCommand_Api { get { return GetValue("StockCommand/Api"); } }
        public static string StockCommand_DataKey { get { return GetValue("StockCommand/DataKey"); } }
    }
}
