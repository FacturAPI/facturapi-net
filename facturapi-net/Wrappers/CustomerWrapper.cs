using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class CustomerWrapper : BaseWrapper
    {
        public CustomerWrapper(string apiKey) : base (apiKey) { }

        public async Task<SearchResult<Customer>> ListAsync(Dictionary<string, object> query = null)
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

        public async Task<Customer> CreateAsync(Dictionary<string, object> data)
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

        public async Task<Customer> RetrieveAsync(string id)
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

        public async Task<Customer> DeleteAsync(string id)
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

        public async Task<Customer> UpdateAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PutAsync(Router.UpdateCustomer(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
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
