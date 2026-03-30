using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IRetentionWrapper
    {
        Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Invoice> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Invoice> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<Invoice> CancelAsync(string id, Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task SendByEmailAsync(string id, Dictionary<string, object> data = null, CancellationToken cancellationToken = default);
        Task<Stream> DownloadZipAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> DownloadPdfAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> DownloadXmlAsync(string id, CancellationToken cancellationToken = default);
    }
}
