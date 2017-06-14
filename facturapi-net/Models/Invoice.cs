using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public class Invoice
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Livemode { get; set; }
        public string Status { get; set; }
        public Customer Customer { get; set; }
        public Decimal Total { get; set; }
        public string Uuid { get; set; }
        public long FolioNumber { get; set; }
        public string Series { get; set; }
        public string PaymentForm { get; set; }
        public List<InvoiceItem> Items { get; set; }

        public static Task<SearchResult<Invoice>> ListAsync(Dictionary<string, object> query = null)
        {
            return new Wrapper().ListInvoices(query);
        }

        public static Task<Invoice> CreateAsync(Dictionary<string, object> data)
        {
            return new Wrapper().CreateInvoice(data);
        }

        public static Task<Invoice> RetrieveAsync(string id)
        {
            return new Wrapper().RetrieveInvoice(id);
        }

        public static Task<Invoice> CancelAsync(string id)
        {
            return new Wrapper().CancelInvoice(id);
        }

        public static Task SendByEmailAsync(string id)
        {
            return new Wrapper().SendInvoiceByEmail(id);
        }

        public static Task<Stream> DownloadXmlAsync(string id)
        {
            return new Wrapper().DownloadInvoice(id, "xml");
        }

        public static Task<Stream> DownloadPdfAsync(string id)
        {
            return new Wrapper().DownloadInvoice(id, "pdf");
        }

        public static Task<Stream> DownloadZipAsync(string id)
        {
            return new Wrapper().DownloadInvoice(id, "zip");
        }
    }
}
