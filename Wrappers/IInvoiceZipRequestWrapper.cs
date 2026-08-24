using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IInvoiceZipRequestWrapper
    {
        Task<Dictionary<string, object>> CreateZipRequestAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<SearchResult<Dictionary<string, object>>> ListZipRequestsAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Dictionary<string, object>> RetrieveZipRequestAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> DownloadZipRequestAsync(string id, CancellationToken cancellationToken = default);
    }
}
