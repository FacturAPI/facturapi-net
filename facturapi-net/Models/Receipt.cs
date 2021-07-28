using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class Receipt
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public bool Livemode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string SelfInvoiceUrl { get; set; }
        public string FolioNumber { get; set; }
        public string Key { get; set; }
        public string Branch { get; set; }
        public string Status { get; set; }
        public string Invoice { get; set; }
        public string PaymentForm { get; set; }
        public string Currency { get; set; }
        public Decimal Exchange { get; set; }
        public Decimal Total { get; set; }
        public List<InvoiceItem> Items { get; set; }

    }
}
