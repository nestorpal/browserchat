using BrowserChat.Client.Core.Session;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BrowserChat.Client.Core.Services
{
    public class HttpClientHelper
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpClientHelper(string baseUrl, SessionManagement sessionMgr)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(300)
            };

            if (sessionMgr.IsLoggedIn())
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {sessionMgr.GetSessionToken()}");
            }
            else
            {
                _client.DefaultRequestHeaders.Remove("Authorization");
            }
            

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public T PostResponse<U, T>(string url, U obj)
        {
            HttpResponseMessage response =
                AsyncHelper.RunSync(() => _client.PostAsync(url, CreateHttpContentJson(obj)));

            response.EnsureSuccessStatusCode();

            return CreateResultObject<T>(response);
        }

        public T GetResponse<U, T>(string url, U obj)
        {
            IEnumerable<string> properties = new List<string>();

            if (obj != null)
            {
                properties = from property in obj.GetType().GetProperties()
                             where property.GetValue(obj, null) != null
                             select $"{property.Name}={WebUtility.UrlEncode((property.GetValue(obj, null) ?? string.Empty).ToString() )}";
            }

            string queryString = string.Join("&", properties.ToArray());

            HttpResponseMessage response = AsyncHelper.RunSync(() => _client.GetAsync($"{url}?{queryString}"));
            response.EnsureSuccessStatusCode();

            return CreateResultObject<T>(response);
        }

        public T PostResponse<T>(string url, List<KeyValuePair<string, string>> data)
        {
            HttpResponseMessage response =
                AsyncHelper.RunSync(() => _client.PostAsync(url, new FormUrlEncodedContent(data)));

            response.EnsureSuccessStatusCode();

            return CreateResultObject<T>(response);
        }

        private HttpContent CreateHttpContentJson<T>(T content)
        {
            var json = JsonSerializer.Serialize(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private T CreateResultObject<T>(HttpResponseMessage response)
        {
            T ret = default;

            if (response.IsSuccessStatusCode)
            {
                string resp = AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync());
                return JsonSerializer.Deserialize<T>(resp, _jsonOptions);
            }

            return ret;
        }
    }
}
