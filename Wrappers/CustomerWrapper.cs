using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class CustomerWrapper : BaseWrapper
    {
        internal CustomerWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Customer>> ListAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.ListCustomers(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Customer>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Customer> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> queryParams = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateCustomer(queryParams), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Customer> RetrieveAsync(string id)
        {
            using (var response = await client.GetAsync(Router.RetrieveCustomer(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Customer> DeleteAsync(string id)
        {
            using (var response = await client.DeleteAsync(Router.DeleteCustomer(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Customer> UpdateAsync(string id, Dictionary<string, object> data, Dictionary<string, object> queryParams = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateCustomer(id, queryParams), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<TaxInfoValidation> ValidateTaxInfoAsync(string id)
        {
            using (var response = await client.GetAsync(Router.ValidateCustomerTaxInfo(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var validation = JsonConvert.DeserializeObject<TaxInfoValidation>(resultString, this.jsonSettings);
                return validation;
            }
        }

        public async Task SendEditLinkByEmailAsync(string id, Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendEditLinkByEmail(id), content))
            {
                await this.ThrowIfErrorAsync(response);
            }
        }
    }
}
