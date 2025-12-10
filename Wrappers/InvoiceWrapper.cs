using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class InvoiceWrapper : BaseWrapper
    {
        internal InvoiceWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListInvoices(query), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Invoice> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateInvoice(options), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveInvoice(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> CancelAsync(string id, Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.CancelInvoice(id, query), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendByEmail(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
            }
        }

        private async Task<Stream> DownloadAsync(string id, string format, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(Router.DownloadInvoice(id, format), cancellationToken))
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

        private async Task<Stream> DownloadCancellationReceiptAsync(string id, string format, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(Router.DownloadCancellationReceipt(id, format), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var responseStream = await response.Content.ReadAsStreamAsync();
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory, 81920, cancellationToken);
                memory.Position = 0;
                return memory;
            }
        }

        public Task<Stream> DownloadCancellationReceiptXmlAsync(string id, CancellationToken cancellationToken = default)
        {
            return DownloadCancellationReceiptAsync(id, "xml", cancellationToken);
        }

        public Task<Stream> DownloadCancellationReceiptPdfAsync(string id, CancellationToken cancellationToken = default)
        {
            return DownloadCancellationReceiptAsync(id, "pdf", cancellationToken);
        }

        public async Task<Invoice> UpdateStatus(string id, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent("", Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateStatus(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> UpdateDraftAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateDraftInvoice(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> StampDraft(string id, Dictionary<string, object> options = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent("", Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.StampDraftInvoice(id, options), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> CopyToDraftAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent("", Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CopyInvoice(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Stream> PreviewPdfAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.PreviewPdf(), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var responseStream = await response.Content.ReadAsStreamAsync();
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory, 81920, cancellationToken);
                memory.Position = 0;
                return memory;
            }
        }
    }
}
