using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace Facturapi.Wrappers
{
    abstract public class BaseWrapper
    {
        protected const string BASE_URL = "https://www.facturapi.io/v1/";
        protected HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri(BASE_URL)
        };
        protected JsonSerializerSettings jsonSettings { get; set; }
        public string apiKey { get; set; }

        public BaseWrapper(string apiKey)
        {
            var apiKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
            this.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", apiKeyBase64);
            this.jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new SnakeCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
