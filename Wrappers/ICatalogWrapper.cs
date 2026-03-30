using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface ICatalogWrapper
    {
        Task<SearchResult<CatalogItem>> SearchProducts(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchUnits(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
    }
}
