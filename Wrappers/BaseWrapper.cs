using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public abstract class BaseWrapper
    {
        protected HttpClient client;
        protected JsonSerializerSettings jsonSettings { get; set; }
        public string apiKey { get; set; }
        public string apiVersion {  get; set; }

        public BaseWrapper(string apiKey, string apiVersion, HttpClient httpClient)
        {
            this.apiKey = apiKey;
            this.apiVersion = apiVersion;

            this.client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
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

            var headers = NormalizeResponseHeaders(response);
            return new FacturapiException(
                message,
                status,
                error?["code"]?.Type == JTokenType.String ? error["code"]?.ToString() : null,
                error?["path"]?.Type == JTokenType.String ? error["path"]?.ToString() : null,
                error?["location"]?.Type == JTokenType.String ? error["location"]?.ToString() : null,
                error?["errors"] as JArray,
                headers.TryGetValue("x-facturapi-log-id", out var logId) ? logId : null,
                headers
            );
        }

        private static IReadOnlyDictionary<string, string> NormalizeResponseHeaders(HttpResponseMessage response)
        {
            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (response == null)
            {
                return headers;
            }
            foreach (var header in response.Headers)
            {
                headers[header.Key.ToLowerInvariant()] = string.Join(", ", header.Value);
            }
            if (response.Content != null)
            {
                foreach (var header in response.Content.Headers)
                {
                    headers[header.Key.ToLowerInvariant()] = string.Join(", ", header.Value);
                }
            }
            return headers;
        }

        protected async Task ThrowIfErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw CreateException(resultString, response);
        }
    }
}
