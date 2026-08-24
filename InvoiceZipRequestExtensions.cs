using Facturapi.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi
{
    public static class InvoiceZipRequestExtensions
    {
        public static Task<Dictionary<string, object>> CreateZipRequestAsync(this IInvoiceWrapper invoice, Dictionary<string, object> data, CancellationToken cancellationToken = default)
        {
            return GetZipRequestWrapper(invoice).CreateZipRequestAsync(data, cancellationToken);
        }

        public static Task<SearchResult<Dictionary<string, object>>> ListZipRequestsAsync(this IInvoiceWrapper invoice, Dictionary<string, object> query = null, CancellationToken cancellationToken = default)
        {
            return GetZipRequestWrapper(invoice).ListZipRequestsAsync(query, cancellationToken);
        }

        public static Task<Dictionary<string, object>> RetrieveZipRequestAsync(this IInvoiceWrapper invoice, string id, CancellationToken cancellationToken = default)
        {
            return GetZipRequestWrapper(invoice).RetrieveZipRequestAsync(id, cancellationToken);
        }

        public static Task<Stream> DownloadZipRequestAsync(this IInvoiceWrapper invoice, string id, CancellationToken cancellationToken = default)
        {
            return GetZipRequestWrapper(invoice).DownloadZipRequestAsync(id, cancellationToken);
        }

        private static IInvoiceZipRequestWrapper GetZipRequestWrapper(IInvoiceWrapper invoice)
        {
            if (invoice is IInvoiceZipRequestWrapper zipRequestWrapper)
            {
                return zipRequestWrapper;
            }

            throw new NotSupportedException("The invoice wrapper does not support ZIP requests.");
        }
    }
}
