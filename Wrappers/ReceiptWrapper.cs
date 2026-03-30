using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class ReceiptWrapper : BaseWrapper, IReceiptWrapper
    {
        internal ReceiptWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Receipt>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListReceipts(query), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Receipt>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Receipt> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateReceipt(), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Receipt> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveReceipt(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Receipt> CancelAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.CancelReceipt(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task InvoiceAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.InvoiceReceipt(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task CreateGlobalInvoiceAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateGlobalInvoice(), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendReceiptByEmail(id), content, cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<Stream> DownloadPdfAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.DownloadReceiptPdf(id), cancellationToken).ConfigureAwait(false))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory, 81920, cancellationToken).ConfigureAwait(false);
                memory.Position = 0;
                return memory;
            }
        }
    }
}
