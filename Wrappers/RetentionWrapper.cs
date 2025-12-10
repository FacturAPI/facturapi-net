using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class RetentionWrapper : BaseWrapper
    {
        internal RetentionWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.ListRetentionss(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Invoice> CreateAsync(Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateRetention(), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Invoice> RetrieveAsync(string id)
        {
            using (var response = await client.GetAsync(Router.RetrieveRetention(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Invoice> CancelAsync(string id)
        {
            using (var response = await client.DeleteAsync(Router.CancelRetention(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendRetentionByEmail(id), content))
            {
                await this.ThrowIfErrorAsync(response);
            }
        }

        private async Task<Stream> DownloadAsync(string id, string format)
        {
            using (var response = await client.GetAsync(Router.DownloadRetention(id, format)))
            {
                await this.ThrowIfErrorAsync(response);
                var responseStream = await response.Content.ReadAsStreamAsync();
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory);
                memory.Position = 0;
                return memory;
            }
        }

        public Task<Stream> DownloadZipAsync(string id)
        {
            return this.DownloadAsync(id, "zip");
        }

        public Task<Stream> DownloadPdfAsync(string id)
        {
            return this.DownloadAsync(id, "pdf");
        }

        public Task<Stream> DownloadXmlAsync(string id)
        {
            return this.DownloadAsync(id, "xml");
        }
    }
}
