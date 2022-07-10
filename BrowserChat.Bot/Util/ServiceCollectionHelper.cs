namespace BrowserChat.Bot.Util
{
    public class ServiceCollectionHelper
    {
        public static IServiceProvider? Provider;

        public static void Initialize(IServiceProvider provider)
        {
            Provider = provider;
        }
    }
}
