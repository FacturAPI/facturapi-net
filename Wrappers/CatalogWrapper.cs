using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            var response = await client.GetAsync(Router.SearchProductKeys(query));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchUnits(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchUnitKeys(query));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        // Carta Porte catalogs
        public async Task<SearchResult<CatalogItem>> SearchAirTransportCodes(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteAirTransportCodes(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchTransportConfigs(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteTransportConfigs(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchRightsOfPassage(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteRightsOfPassage(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchCustomsDocuments(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteCustomsDocuments(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchPackagingTypes(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaportePackagingTypes(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchTrailerTypes(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteTrailerTypes(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchHazardousMaterials(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteHazardousMaterials(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchNavalAuthorizations(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteNavalAuthorizations(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchPortStations(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaportePortStations(query));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var searchResult = JsonConvert.DeserializeObject<SearchResult<CatalogItem>>(resultString, this.jsonSettings);
            return searchResult;
        }

        public async Task<SearchResult<CatalogItem>> SearchMarineContainers(Dictionary<string, object> query = null)
        {
            var response = await client.GetAsync(Router.SearchCartaporteMarineContainers(query));
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
