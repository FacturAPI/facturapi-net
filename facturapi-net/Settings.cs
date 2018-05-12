using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    public static class Settings
    {
        [Obsolete("Setting your ApiKey globally is deprecated and will be removed in the next major release. Create an instance of Facturapi.Wrapper instead.")]
        public static string ApiKey { get; set; }
    }
}
