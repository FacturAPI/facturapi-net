using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class WebhookWrapper : BaseWrapper
    {
        public WebhookWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<SearchResult<Webhook>> ListAsync(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.ListWebhooks(query));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Webhook>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Webhook> CreateAsync(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateWebhook(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
            return webhook;
        }

        public async Task<Webhook> RetrieveAsync(string id)
        {
            var response = await client.GetAsync(Router.RetrieveWebhook(id));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
            return webhook;
        }

        public async Task<Webhook> UpdateAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.UpdateWebhook(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
            return webhook;
        }

        public async Task<Webhook> DeleteAsync(string id)
        {
            var response = await client.DeleteAsync(Router.DeleteWebhook(id));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
            return webhook;
        }

        public async Task<Webhook> ValidateSignatureAsync(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.ValidateSignature(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var webhook = JsonConvert.DeserializeObject<Webhook>(resultString, this.jsonSettings);
            return webhook;
        }

    }
}
