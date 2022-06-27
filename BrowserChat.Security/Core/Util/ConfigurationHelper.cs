namespace BrowserChat.Security.Core.Util
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

        private static string GetConnectionString(string key)
        {
            string result = string.Empty;
            if (_config != null)
            {
                result = _config.GetConnectionString(key);
            }
            return result;
        }

        public static string JWTKey { get { return GetValue("JWTKey"); } }
        public static string DbConnection { get { return GetConnectionString("DbConnection"); } }
    }
}
