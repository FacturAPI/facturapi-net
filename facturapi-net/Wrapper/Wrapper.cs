using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal partial class Wrapper
    {
        private const string BASE_URL = "https://www.facturapi.io/v1/";
        HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri(BASE_URL)
        };
        private JsonSerializerSettings jsonSettings { get; set; }

        public Wrapper()
        {
            var apiKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(Settings.ApiKey + ":"));
            this.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", apiKeyBase64);
            this.jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new SnakeCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
