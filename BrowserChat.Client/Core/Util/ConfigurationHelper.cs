namespace BrowserChat.Client.Core.Util
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

        public static string SecurityService { get { return GetValue("SecurityService"); } }
        public static string RestAPIService { get { return GetValue("RestAPIService"); } }
        public static string HubService { get { return GetValue("HubService"); } }
    }
}
