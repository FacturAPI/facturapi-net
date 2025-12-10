using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class WebhookWrapper : BaseWrapper
    {
        internal WebhookWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Webhook>> ListAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.ListWebhooks(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Webhook>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Webhook> CreateAsync(Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateWebhook(), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
                return webhook;
            }
        }

        public async Task<Webhook> RetrieveAsync(string id)
        {
            using (var response = await client.GetAsync(Router.RetrieveWebhook(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
                return webhook;
            }
        }

        public async Task<Webhook> UpdateAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateWebhook(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
                return webhook;
            }
        }

        public async Task<Webhook> DeleteAsync(string id)
        {
            using (var response = await client.DeleteAsync(Router.DeleteWebhook(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
                return webhook;
            }
        }

        public async Task<Webhook> ValidateSignatureAsync(Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.ValidateSignature(), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
                return webhook;
            }
        }

    }
}
