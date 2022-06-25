using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using BrowserChat.Client.Core.Session;

namespace BrowserChat.Client.Core.Services
{
    public class HttpClientHelper
    {
        private readonly HttpClient _client = null;
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
            HttpResponseMessage response = null;

            response = AsyncHelper.RunSync(() => _client.PostAsync(url, CreateHttpContentJson(obj)));
            response.EnsureSuccessStatusCode();

            return CreateResultObject<T>(response);
        }

        public T GetResponse<U, T>(string url, U obj)
        {
            HttpResponseMessage response = null;

            var properties = from property in obj.GetType().GetProperties()
                             where property.GetValue(obj, null) != null
                             select $"{property.Name}={WebUtility.UrlEncode(property.GetValue(obj, null).ToString())}";

            string queryString = string.Join("&", properties.ToArray());

            response = AsyncHelper.RunSync(() => _client.GetAsync($"{url}?{queryString}"));
            response.EnsureSuccessStatusCode();

            return CreateResultObject<T>(response);
        }

        public T PostResponse<T>(string url, List<KeyValuePair<string, string>> data)
        {
            HttpResponseMessage response = null;

            response = AsyncHelper.RunSync(() => _client.PostAsync(url, new FormUrlEncodedContent(data)));
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
