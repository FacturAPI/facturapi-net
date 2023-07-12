using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class ReceiptWrapper : BaseWrapper
    {
        public ReceiptWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<SearchResult<Receipt>> ListAsync(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.ListReceipts(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Receipt>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Receipt> CreateAsync(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateReceipt(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Receipt> RetrieveAsync(string id)
        {
            var response = await client.GetAsync(Router.RetrieveReceipt(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Receipt> CancelAsync(string id)
        {
            var response = await client.DeleteAsync(Router.CancelInvoice(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Receipt>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task InvoiceAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.InvoiceReceipt(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
        }

        public async Task CreateGlobalInvoiceAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateGlobalInvoice(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
        }

        public async Task SendByEmailAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.SendReceiptByEmail(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
        }

        public async Task<Stream> DownloadPdfAsync(string id)
        {
            var response = await client.GetAsync(Router.DownloadReceiptPdf(id));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }
    }
}
