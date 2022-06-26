using BrowserChat.Util;

namespace BrowserChat.Backend.Core.Util
{
    public static class General
    {

        public static string EncryptString(string str)
        {
            return Encrypt(str);
        }

        public static string DecryptString(string str)
        {
            return Decrypt(str);
        }

        public static string EncryptStringEncoded(string str)
        {
            return System.Web.HttpUtility.UrlEncode(Encrypt(str));
        }

        public static string DecryptStringEncoded(string str)
        {
            return Decrypt(System.Web.HttpUtility.UrlDecode(str));
            
        }

        private static string Encrypt(string str)
        {
            return BrowserChat.Util.StringCipher.Encrypt(ConfigurationHelper.EncryptionKey, str);
        }

        private static string Decrypt(string str)
        {
            return BrowserChat.Util.StringCipher.Decrypt(ConfigurationHelper.EncryptionKey, str);
        }
    }
}
