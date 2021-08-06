using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class CatalogWrapper : BaseWrapper
    {
        public CatalogWrapper(string apiKey) : base(apiKey) { }

        public async Task<SearchResult<CatalogItem>> SearchProducts(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchProductKeys(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchUnits(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchProductKeys(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }
    }
}
