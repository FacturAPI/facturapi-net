using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class RetentionWrapper : BaseWrapper
    {
        internal RetentionWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListRetentionss(query), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Invoice> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateRetention(), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Invoice> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveRetention(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Invoice> CancelAsync(string id, Dictionary<string, object> query = null)
        {
            var url = Router.CancelRetention(id, query);
            var response = await client.DeleteAsync(url);
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendRetentionByEmail(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
            }
        }

        private async Task<Stream> DownloadAsync(string id, string format, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(Router.DownloadRetention(id, format), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var responseStream = await response.Content.ReadAsStreamAsync();
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory, 81920, cancellationToken);
                memory.Position = 0;
                return memory;
            }
        }

        public Task<Stream> DownloadZipAsync(string id, CancellationToken cancellationToken = default)
        {
            return this.DownloadAsync(id, "zip", cancellationToken);
        }

        public Task<Stream> DownloadPdfAsync(string id, CancellationToken cancellationToken = default)
        {
            return this.DownloadAsync(id, "pdf", cancellationToken);
        }

        public Task<Stream> DownloadXmlAsync(string id, CancellationToken cancellationToken = default)
        {
            return this.DownloadAsync(id, "xml", cancellationToken);
        }
    }
}
