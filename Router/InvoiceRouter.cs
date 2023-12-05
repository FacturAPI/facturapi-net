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

        public static string CancelInvoice(string id, Dictionary<string, object> query = null)
        {
            return UriWithQuery(RetrieveInvoice(id), query);
        }

        public static string DownloadInvoice(string id, string format)
        {
            return $"invoices/{id}/{format}";
        }

        public static string DownloadCancellationReceipt(string id, string format)
        {
            return $"invoices/{id}/cancellation_receipt/{format}";
        }

        public static string SendByEmail(string id)
        {
            return $"invoices/{id}/email";
        }
    }
}
