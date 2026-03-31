using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IReceiptWrapper
    {
        Task<SearchResult<Receipt>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Receipt> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Receipt> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<Receipt> CancelAsync(string id, CancellationToken cancellationToken = default);
        Task InvoiceAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task CreateGlobalInvoiceAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task SendByEmailAsync(string id, Dictionary<string, object> data = null, CancellationToken cancellationToken = default);
        Task<Stream> DownloadPdfAsync(string id, CancellationToken cancellationToken = default);
    }
}
