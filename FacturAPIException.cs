using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public class FacturapiException : Exception
    {
        public FacturapiException() : base() { }
        public FacturapiException(string message) : base(message) { }
    }
}
