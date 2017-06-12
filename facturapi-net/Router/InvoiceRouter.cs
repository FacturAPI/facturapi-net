using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListInvoices(Dictionary<string, object> query = null)
        {
            return UriWithQuery("invoices", query);
        }

        public static string RetrieveInvoice(string id)
        {
            return $"invoices/{id}";
        }

        public static string CreateInvoice()
        {
            return "invoices";
        }

        public static string CancelInvoice(string id)
        {
            return RetrieveInvoice(id);
        }

        public static string DownloadXml(string id)
        {
            return RetrieveInvoice(id);
        }

        public static string DownloadPdf(string id)
        {
            return RetrieveInvoice(id);
        }

        public static string DownloadZip(string id)
        {
            return RetrieveInvoice(id);
        }

        public static string SendByEmail(string id)
        {
            return RetrieveInvoice(id);
        }
    }
}
