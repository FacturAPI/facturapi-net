using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal partial class Wrapper
    {
        public async Task<SearchResult<Invoice>> ListInvoices(Dictionary<string, object> query)
        {
            var response = await client.GetAsync(Router.ListInvoices(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Invoice>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Invoice> CreateInvoice(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateInvoice(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Invoice> RetrieveInvoice(string id)
        {
            var response = await client.GetAsync(Router.RetrieveInvoice(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Invoice> CancelInvoice(string id)
        {
            var response = await client.DeleteAsync(Router.CancelInvoice(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Invoice>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task SendInvoiceByEmail(string id)
        {
            var response = await client.PostAsync(Router.SendByEmail(id), null);
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
        }

        public async Task<Stream> DownloadInvoice(string id, string format)
        {
            var response = await client.GetAsync(Router.DownloadInvoice(id, format));
            if (!response.IsSuccessStatusCode)
            {
                var resultString = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }
    }
}
