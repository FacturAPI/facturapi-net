using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class ToolWrapper : BaseWrapper
    {
        public ToolWrapper(string apiKey) : base(apiKey) { }

        public async Task<TaxIdValidation> ValidateTaxId(string taxId)
        {
            var response = await client.GetAsync(Router.ValidateTaxId(
                new Dictionary<string, object>()
                {
                    ["tax_id"] = taxId
                }
            ));
            var resultString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<JObject>(resultString, this.jsonSettings);
                throw new FacturapiException(error["message"].ToString());
            }

            var result = JsonConvert.DeserializeObject<TaxIdValidation>(resultString, this.jsonSettings);
            return result;
        }
    }
}
