using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class CustomerWrapper : BaseWrapper
    {
        public CustomerWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<SearchResult<Customer>> ListAsync(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.ListCustomers(query));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Customer>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Customer> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> queryParams = null)
        {
            var response = await client.PostAsync(Router.CreateCustomer(queryParams), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Customer> RetrieveAsync(string id)
        {
            var response = await client.GetAsync(Router.RetrieveCustomer(id));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Customer> DeleteAsync(string id)
        {
            var response = await client.DeleteAsync(Router.DeleteCustomer(id));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Customer> UpdateAsync(string id, Dictionary<string, object> data, Dictionary<string, object> queryParams = null)
        {
            var response = await client.PutAsync(Router.UpdateCustomer(id, queryParams), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<TaxInfoValidation> ValidateTaxInfoAsync(string id)
        {
            var response = await client.GetAsync(Router.ValidateCustomerTaxInfo(id));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();
            var validation = JsonConvert.DeserializeObject<TaxInfoValidation>(resultString, this.jsonSettings);
            return validation;
        }

        public async Task SendEditLinkByEmailAsync(string id, Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.SendEditLinkByEmail(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            await this.ThrowIfErrorAsync(response);
        }
    }
}
