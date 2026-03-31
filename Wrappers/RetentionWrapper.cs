using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class RetentionWrapper : BaseWrapper, IRetentionWrapper
    {
        internal RetentionWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListRetentions(query), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Invoice> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateRetention(), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Invoice> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveRetention(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Invoice> CancelAsync(string id, Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.CancelRetention(id, query), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var retention = JsonConvert.DeserializeObject<Invoice>(responseString, this.jsonSettings);
                return retention;
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendRetentionByEmail(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task<Stream> DownloadAsync(string id, string format, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(Router.DownloadRetention(id, format), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory, 81920, cancellationToken).ConfigureAwait(false);
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
