using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class NominaCatalogWrapper : BaseWrapper, INominaCatalogWrapper
    {
        internal NominaCatalogWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public Task<SearchResult<CatalogItem>> SearchDeductions(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return SearchCatalogAsync(Router.SearchNominaDeductions(query), cancellationToken);
        }

        public Task<SearchResult<CatalogItem>> SearchPerceptions(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return SearchCatalogAsync(Router.SearchNominaPerceptions(query), cancellationToken);
        }

        private async Task<SearchResult<CatalogItem>> SearchCatalogAsync(string url, CancellationToken cancellationToken)
        {
            using (var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false))
            {
                await ThrowIfErrorAsync(response, cancellationToken).ConfigureAwait(false);
                var resultString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, jsonSettings);
            }
        }
    }
}
