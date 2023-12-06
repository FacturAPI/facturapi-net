using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class Stamp
    {
        public string Date { get; set; }
        public string SatSignature { get; set; }
        public string SatCertNumber { get; set; }
        public string Signature { get; set; }
        public string ComplementString { get; set; }
    }
}
