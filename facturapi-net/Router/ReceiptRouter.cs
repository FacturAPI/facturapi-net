using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal static partial class Router
    {
        public static string ListReceipts(Dictionary<string, object> query = null)
        {
            return UriWithQuery("receipts", query);
        }

        public static string RetrieveReceipt(string id)
        {
            return $"receipts/{id}";
        }

        public static string CreateReceipt()
        {
            return "receipts";
        }

        public static string CancelReceipt(string id)
        {
            return RetrieveReceipt(id);
        }

        public static string InvoiceReceipt(string id)
        {
            return $"receipts/(id)/invoice";
        }
        
        public static string CreateGlobalInvoice(string id)
        {
            return $"receipts/global-invoice";
        }
    }
}
