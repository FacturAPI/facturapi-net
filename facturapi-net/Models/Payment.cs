using System;

namespace Facturapi
{
    public class Payment
    {
        public string PaymentForm { get; set; }
        public string Currency { get; set; }
        public Decimal Exchange { get; set; }
        public DateTime Date { get; set; }
        public PaymentRelated Related {get;set;}
    }

    public class PaymentRelated
    {
        public string Uuid { get; set; }
        public int Installment { get; set; }
        public Decimal LastBalance { get; set; }
        public Decimal Amount { get; set; }
        public string Currency { get; set; }
        public Decimal Exchange { get; set; }
    }
}
