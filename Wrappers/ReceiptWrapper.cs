using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class ReceiptWrapper : BaseWrapper
    {
        internal ReceiptWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Receipt>> ListAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.ListReceipts(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Receipt>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Receipt> CreateAsync(Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateReceipt(), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Receipt> RetrieveAsync(string id)
        {
            using (var response = await client.GetAsync(Router.RetrieveReceipt(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Receipt> CancelAsync(string id)
        {
            using (var response = await client.DeleteAsync(Router.CancelInvoice(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task InvoiceAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.InvoiceReceipt(id), content))
            {
                await this.ThrowIfErrorAsync(response);
            }
        }

        public async Task CreateGlobalInvoiceAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateGlobalInvoice(id), content))
            {
                await this.ThrowIfErrorAsync(response);
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendReceiptByEmail(id), content))
            {
                await this.ThrowIfErrorAsync(response);
            }
        }

        public async Task<Stream> DownloadPdfAsync(string id)
        {
            using (var response = await client.GetAsync(Router.DownloadReceiptPdf(id)))
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
