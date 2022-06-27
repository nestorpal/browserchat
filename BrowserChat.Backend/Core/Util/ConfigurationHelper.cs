namespace BrowserChat.Backend.Core.Util
{
    public static class ConfigurationHelper
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
                result = _config[key];
            }
            return result;
        }

        private static string[] GetValues(string key)
        {
            List<string> result = new List<string>();
            if (_config != null)
            {
                result =
                    _config
                    .GetSection(key)?
                    .GetChildren()?
                    .Select(x => x.Value)?.ToList() ?? new List<string>();
            }
            return result.ToArray();
        }

        private static string GetConnectionString(string key)
        {
            string result = string.Empty;
            if (_config != null)
            {
                result = _config.GetConnectionString(key);
            }
            return result;
        }

        public static string DbConnection { get { return GetConnectionString("DbConnection"); } }
        public static string RabbitMQHost { get { return GetValue("RabbitMQHost"); } }
        public static string RabbitMQPort { get { return GetValue("RabbitMQPort"); } }
        public static string[] ClientDomain { get { return GetValues("ClientDomain"); } }
        public static string EncryptionKey { get { return GetValue("EncryptionKey"); } }
        public static string JWTKey { get { return GetValue("JWTKey"); } }
    }
}
