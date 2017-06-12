using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal partial class Wrapper
    {
        public async Task<SearchResult<Customer>> ListCustomers(Dictionary<string, object> query)
        {
            var response = await client.GetAsync(Router.ListCustomers(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Customer>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Customer> CreateCustomer(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateCustomer(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Customer> RetrieveCustomer(string id)
        {
            var response = await client.GetAsync(Router.RetrieveCustomer(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Customer> DeleteCustomer(string id)
        {
            var response = await client.DeleteAsync(Router.DeleteCustomer(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
            return customer;
        }
    }
}
