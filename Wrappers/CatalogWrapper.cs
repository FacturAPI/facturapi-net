using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class CatalogWrapper : BaseWrapper
    {
        internal CatalogWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<CatalogItem>> SearchProducts(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchProductKeys(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchUnits(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchUnitKeys(query), cancellationToken);
        }

        private async Task<SearchResult<CatalogItem>> SearchCatalogAsync(string url, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(url, cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();
                var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }
    }
}
