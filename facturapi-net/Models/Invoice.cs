using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class Invoice
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Livemode { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string VerificationUrl { get; set; }
        public string CancellationStatus { get; set; }
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public string Uuid { get; set; }
        public string FolioNumber { get; set; }
        public string Series { get; set; }
        public string Use { get; set; }
        public string PaymentForm { get; set; }
        public string PaymentMethod { get; set; }
        public string Currecy { get; set; }
        public decimal Exchange { get; set; }
        public string CancellationReceipt { get; set; }
        public List<string> Related { get; set; }
        public string Relation { get; set; }
        public string Conditions { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Namespace> Namespaces { get; set; }
        public Stamp Stamp { get; set; }
    }

    public class Namespace
    {
        public string Prefix { get; set; }
        public string Uri { get; set; }
        public string SchemaLocation { get; set; }
    }
}
