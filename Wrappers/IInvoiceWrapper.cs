using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IInvoiceWrapper
    {
        Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Invoice> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> options = null, CancellationToken cancellationToken = default);
        Task<Invoice> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<Invoice> CancelAsync(string id, Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task SendByEmailAsync(string id, Dictionary<string, object> data = null, CancellationToken cancellationToken = default);
        Task<Stream> DownloadZipAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> DownloadPdfAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> DownloadXmlAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> DownloadCancellationReceiptXmlAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> DownloadCancellationReceiptPdfAsync(string id, CancellationToken cancellationToken = default);
        Task<Invoice> UpdateStatusAsync(string id, CancellationToken cancellationToken = default);
        Task<Invoice> UpdateDraftAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Invoice> StampDraftAsync(string id, Dictionary<string, object> options = null, CancellationToken cancellationToken = default);
        [Obsolete("Use StampDraftAsync instead.")]
        Task<Invoice> StampDraft(string id, Dictionary<string, object> options = null, CancellationToken cancellationToken = default);
        Task<Invoice> CopyToDraftAsync(string id, CancellationToken cancellationToken = default);
        Task<Stream> PreviewPdfAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
    }
}
