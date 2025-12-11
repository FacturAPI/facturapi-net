using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public abstract class BaseWrapper
    {
        protected const string BASE_URL = "https://www.facturapi.io/";
        protected HttpClient client;
        protected JsonSerializerSettings jsonSettings { get; set; }
        public string apiKey { get; set; }
        public string apiVersion {  get; set; }

        public BaseWrapper(string apiKey, string apiVersion = "v2")
        {
            var apiKeyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(apiKey + ":"));
            this.client = new HttpClient()
            {
                BaseAddress = new Uri($"{BASE_URL}/{apiVersion}/")
            };
            this.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", apiKeyBase64);
            this.jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new SnakeCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        protected FacturapiException CreateException(string resultString, HttpResponseMessage response)
        {
            JObject error = null;

            try
            {
                error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
            }
            catch (JsonException)
            {
                // Intentionally swallow exception: resultString is not valid JSON.
                // This allows graceful handling of non-JSON error responses.
            }

            var message = error?["message"]?.ToString() ?? "An error occurred";

            int? status = null;
            var statusToken = error?["status"];
            if (statusToken != null)
            {
                if (statusToken.Type == JTokenType.Integer)
                {
                    status = statusToken.Value<int>();
                }
                else if (statusToken.Type == JTokenType.Float)
                {
                    status = Convert.ToInt32(statusToken.Value<double>());
                }
                else if (int.TryParse(statusToken.ToString(), out var parsedStatus))
                {
                    status = parsedStatus;
                }
            }

            if (status == null && response != null)
            {
                status = (int)response.StatusCode;
            }

            return new FacturapiException(message, status);
        }

        protected async Task ThrowIfErrorAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var resultString = await response.Content.ReadAsStringAsync();
            throw CreateException(resultString, response);
        }
    }
}
