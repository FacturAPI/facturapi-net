using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public class InvoiceItem
    {
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Product Product { get; set; }
    }
}
