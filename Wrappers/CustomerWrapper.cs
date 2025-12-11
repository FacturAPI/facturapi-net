using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class CustomerWrapper : BaseWrapper
    {
        internal CustomerWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Customer>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ListCustomers(query), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Customer>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Customer> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> queryParams = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateCustomer(queryParams), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Customer> RetrieveAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.RetrieveCustomer(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Customer> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.DeleteAsync(Router.DeleteCustomer(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Customer> UpdateAsync(string id, Dictionary<string, object> data, Dictionary<string, object> queryParams = null, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PutAsync(Router.UpdateCustomer(id, queryParams), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Customer>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<TaxInfoValidation> ValidateTaxInfoAsync(string id, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ValidateCustomerTaxInfo(id), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var validation = JsonConvert.DeserializeObject<TaxInfoValidation>(resultString, this.jsonSettings);
                return validation;
            }
        }

        public async Task SendEditLinkByEmailAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.SendEditLinkByEmail(id), content, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
            }
        }
    }
}
