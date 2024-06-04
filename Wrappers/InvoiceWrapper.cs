using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class InvoiceWrapper : BaseWrapper
    {
        public InvoiceWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.ListInvoices(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Invoice> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> options = null)
        {
            var response = await client.PostAsync(Router.CreateInvoice(options), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return invoice;
        }

        public async Task<Invoice> RetrieveAsync(string id)
        {
            var response = await client.GetAsync(Router.RetrieveInvoice(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return invoice;
        }

        public async Task<Invoice> CancelAsync(string id, Dictionary<string, object> query = null)
        {
            var response = await client.DeleteAsync(Router.CancelInvoice(id, query));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return invoice;
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.SendByEmail(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
        }

        private async Task<Stream> DownloadAsync(string id, string format)
        {
            var response = await client.GetAsync(Router.DownloadInvoice(id, format));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
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
            var response = await client.GetAsync(Router.DownloadCancellationReceipt(id, format));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
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
            var response = await client.PutAsync(Router.UpdateStatus(id), new StringContent("", Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return invoice;
        }

        public async Task<Invoice> UpdateDraftAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.UpdateDraftInvoice(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return invoice;
        }

        public async Task<Invoice> StampDraft(string id, Dictionary<string, object> options = null)
        {
            var response = await client.PostAsync(Router.StampDraftInvoice(id, options), new StringContent("", Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return invoice;
        }

        public async Task<Invoice> CopyToDraftAsync(string id)
        {
            var response = await client.PostAsync(Router.CopyInvoice(id), new StringContent("", Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var invoice = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return invoice;
        }
    }
}
