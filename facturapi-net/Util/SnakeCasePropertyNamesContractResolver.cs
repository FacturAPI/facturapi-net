using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi
{
    internal class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            var startUnderscores = System.Text.RegularExpressions.Regex.Match(propertyName, @"^_+");
            return startUnderscores + System.Text.RegularExpressions.Regex
              .Replace(propertyName, @"([A-Z])", "_$1").ToLower().TrimStart('_');
        }
    }
}
