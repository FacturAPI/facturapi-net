using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class Product
    {
        public string Id { get; set; }
        public bool Livemode { get; set; }
        public string ProductKey { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool TaxIncluded { get; set; }
        public List<Tax> Taxes { get; set; }
        public string UnitKey { get; set; }
        public string UnitName { get; set; }
        public string Sku { get; set; }
        public string Taxability { get; set; }
    }
}
