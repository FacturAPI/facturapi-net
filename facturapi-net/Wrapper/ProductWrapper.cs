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
        public async Task<SearchResult<Product>> ListProducts(Dictionary<string, object> query)
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

        public async Task<Product> CreateProduct(Dictionary<string, object> data)
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

        public async Task<Product> RetrieveProduct(string id)
        {
            var response = await client.GetAsync(Router.RetrieveProduct(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
            return customer;
        }

        public async Task<Product> DeleteProduct(string id)
        {
            var response = await client.DeleteAsync(Router.DeleteProduct(id));
            var resultString = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString);
                throw new FacturapiException(error["message"].ToString());
            }
            var customer = JsonConvert.DeserializeObject<Product>(resultString, this.jsonSettings);
            return customer;
        }
    }
}
