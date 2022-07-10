using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
