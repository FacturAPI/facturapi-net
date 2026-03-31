using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface ICartaporteCatalogWrapper
    {
        Task<SearchResult<CatalogItem>> SearchAirTransportCodes(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchTransportConfigs(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchRightsOfPassage(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchCustomsDocuments(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchPackagingTypes(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchTrailerTypes(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchHazardousMaterials(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchNavalAuthorizations(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchPortStations(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchMarineContainers(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
    }
}
