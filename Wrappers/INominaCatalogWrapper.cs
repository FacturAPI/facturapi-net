using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface INominaCatalogWrapper
    {
        Task<SearchResult<CatalogItem>> SearchDeductions(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<SearchResult<CatalogItem>> SearchPerceptions(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
    }
}
