using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class InvoiceWrapper : BaseWrapper
    {
        internal InvoiceWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.ListInvoices(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Invoice> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> options = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateInvoice(options), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> RetrieveAsync(string id)
        {
            using (var response = await client.GetAsync(Router.RetrieveInvoice(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> CancelAsync(string id, Dictionary<string, object> query = null)
        {
            using (var response = await client.DeleteAsync(Router.CancelInvoice(id, query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendByEmail(id), content))
            {
                await this.ThrowIfErrorAsync(response);
            }
        }

        private async Task<Stream> DownloadAsync(string id, string format)
        {
            using (var response = await client.GetAsync(Router.DownloadInvoice(id, format)))
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

        private async Task<Stream> DownloadCancellationReceiptAsync(string id, string format)
        {
            using (var response = await client.GetAsync(Router.DownloadCancellationReceipt(id, format)))
            {
                await this.ThrowIfErrorAsync(response);
                var responseStream = await response.Content.ReadAsStreamAsync();
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory);
                memory.Position = 0;
                return memory;
            }
        }

        public Task<Stream> DownloadCancellationReceiptXmlAsync(string id)
        {
            return DownloadCancellationReceiptAsync(id, "xml");
        }

        public Task<Stream> DownloadCancellationReceiptPdfAsync(string id)
        {
            return DownloadCancellationReceiptAsync(id, "pdf");
        }

        public async Task<Invoice> UpdateStatus(string id)
        {
            using (var content = new StringContent("", Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateStatus(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> UpdateDraftAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateDraftInvoice(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> StampDraft(string id, Dictionary<string, object> options = null)
        {
            using (var content = new StringContent("", Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.StampDraftInvoice(id, options), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Invoice> CopyToDraftAsync(string id)
        {
            using (var content = new StringContent("", Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CopyInvoice(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
                return invoice;
            }
        }

        public async Task<Stream> PreviewPdfAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.PreviewPdf(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var responseStream = await response.Content.ReadAsStreamAsync();
                var memory = new MemoryStream();
                await responseStream.CopyToAsync(memory);
                memory.Position = 0;
                return memory;
            }
        }
    }
}
