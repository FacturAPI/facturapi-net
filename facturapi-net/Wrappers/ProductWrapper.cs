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
    public class ProductWrapper : BaseWrapper
    {
        public ProductWrapper(string apiKey) : base(apiKey) { }

        public async Task<SearchResult<Product>> ListAsync(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.ListProducts(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<Product>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<Product> CreateAsync(Dictionary<string, object> data)
        {
            var response = await client.PostAsync(Router.CreateProduct(), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Product> RetrieveAsync(string id)
        {
            var response = await client.GetAsync(Router.RetrieveProduct(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var product = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
            return product;
        }

        public async Task<Product> DeleteAsync(string id)
        {
            var response = await client.DeleteAsync(Router.DeleteProduct(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var product = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
            return product;
        }

		public async Task<Product> UpdateAsync(string id, Dictionary<string, object> data)
		{
			var response = await client.PutAsync(Router.UpdateProduct(id), new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
			var resultString = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				var error = JsonConvert.DeserializeObject<JObject>(resultString);
				throw new FacturapiException(error["message"].ToString());
			}
			var product = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
			return product;
		}
    }
}
