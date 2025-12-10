using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class ProductWrapper : BaseWrapper
    {
        internal ProductWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<Product>> ListAsync(Dictionary<string, object> query = null)
        {
            using (var response = await client.GetAsync(Router.ListProducts(query)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var searchResult = JsonConvert.DeserializeObject<SearchResult<Product>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }

        public async Task<Product> CreateAsync(Dictionary<string, object> data)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
            using (var response = await client.PostAsync(Router.CreateProduct(), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
                return customer;
            }
        }

        public async Task<Product> RetrieveAsync(string id)
        {
            using (var response = await client.GetAsync(Router.RetrieveProduct(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
                return product;
            }
        }

        public async Task<Product> DeleteAsync(string id)
        {
            using (var response = await client.DeleteAsync(Router.DeleteProduct(id)))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
                return product;
            }
        }

		public async Task<Product> UpdateAsync(string id, Dictionary<string, object> data)
		{
			using (var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"))
			using (var response = await client.PutAsync(Router.UpdateProduct(id), content))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
			    var product = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
			    return product;
            }
		}
    }
}
