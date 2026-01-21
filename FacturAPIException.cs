using System;

namespace Facturapi
{
    public class FacturapiException : Exception
    {
        public int? Status { get; private set; }

        public FacturapiException() : base() { }
        public FacturapiException(string message, int? status = null) : base(message)
        {
            Status = status;
        }
    }
}
