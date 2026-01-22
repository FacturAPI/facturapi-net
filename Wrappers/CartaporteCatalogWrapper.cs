using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class CartaporteCatalogWrapper : BaseWrapper
    {
        internal CartaporteCatalogWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<SearchResult<CatalogItem>> SearchAirTransportCodes(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteAirTransportCodes(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchTransportConfigs(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteTransportConfigs(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchRightsOfPassage(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteRightsOfPassage(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchCustomsDocuments(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteCustomsDocuments(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchPackagingTypes(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaportePackagingTypes(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchTrailerTypes(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteTrailerTypes(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchHazardousMaterials(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteHazardousMaterials(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchNavalAuthorizations(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteNavalAuthorizations(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchPortStations(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaportePortStations(query), cancellationToken);
        }

        public async Task<SearchResult<CatalogItem>> SearchMarineContainers(Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteMarineContainers(query), cancellationToken);
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
