using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Facturapi
{

    public class Tax
    {
        public string Type { get; set; }
        public decimal Rate { get; set; }
        public bool Withholding { get; set; }
    }
}
