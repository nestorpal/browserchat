namespace BrowserChat.Backend.Core.Util
{
    public class ServiceCollectionHelper
    {
        public static IApplicationBuilder? Provider;

        public static void Initialize(IApplicationBuilder builder)
        {
            Provider = builder;
        }
    }
}
