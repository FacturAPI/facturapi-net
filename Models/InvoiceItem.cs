using System;

using System.Collections.Generic;

namespace Facturapi
{
    public class InvoiceItem
    {
        public Decimal Quantity { get; set; }
        public Decimal Discount { get; set; }
        public string Description { get; set; }
        public Product Product { get; set; }
        public List<string> PropertyTaxAccount { get; set; }
    }
}
