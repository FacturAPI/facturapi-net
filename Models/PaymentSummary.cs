using System.Collections.Generic;

namespace Facturapi
{
    public class PaymentSummary
    {
        public string Uuid { get; set; }
        public decimal? FolioNumber { get; set; }
        public string Series { get; set; }
        public int Installment { get; set; }
        public decimal LastBalance { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public List<PaymentSummaryTax> Taxes { get; set; }
    }

    public class PaymentSummaryTax
    {
        public decimal Base { get; set; }
        public decimal Rate { get; set; }
        public string Type { get; set; }
        public string Factor { get; set; }
        public bool Withholding { get; set; }
    }
}
