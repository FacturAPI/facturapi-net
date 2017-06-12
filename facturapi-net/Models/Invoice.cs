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
        public Customer Customer { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public string PaymentForm { get; set; }

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

        public static Task SendByEmail(string id)
        {
            return new Wrapper().SendInvoiceByEmail(id);
        }

        public static Task<Stream> DownloadXml(string id)
        {
            return new Wrapper().DownloadXml(id);
        }

        public static Task<Stream> DownloadPdf(string id)
        {
            return new Wrapper().DownloadPdf(id);
        }

        public static Task<Stream> DownloadZip(string id)
        {
            return new Wrapper().DownloadZip(id);
        }
    }
}
