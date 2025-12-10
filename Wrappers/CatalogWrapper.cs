using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class CatalogWrapper : BaseWrapper
    {
        public CatalogWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<SearchResult<CatalogItem>> SearchProducts(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchProductKeys(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchUnits(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchUnitKeys(query));
        }

        // Carta Porte catalogs
        public async Task<SearchResult<CatalogItem>> SearchAirTransportCodes(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteAirTransportCodes(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchTransportConfigs(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteTransportConfigs(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchRightsOfPassage(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteRightsOfPassage(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchCustomsDocuments(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteCustomsDocuments(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchPackagingTypes(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaportePackagingTypes(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchTrailerTypes(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteTrailerTypes(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchHazardousMaterials(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteHazardousMaterials(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchNavalAuthorizations(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteNavalAuthorizations(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchPortStations(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaportePortStations(query));
        }

        public async Task<SearchResult<CatalogItem>> SearchMarineContainers(Dictionary<string, object> query = null)
        {
            return await this.SearchCatalogAsync(Router.SearchCartaporteMarineContainers(query));
        }

        private async Task<SearchResult<CatalogItem>> SearchCatalogAsync(string url)
        {
            using (var response = await client.GetAsync(url))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();
                var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
                return searchResult;
            }
        }
    }
}
