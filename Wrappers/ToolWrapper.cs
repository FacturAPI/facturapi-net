using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class ToolWrapper : BaseWrapper
    {
        public ToolWrapper(string apiKey, string apiVersion = "v2") : base(apiKey, apiVersion)
        {
        }

        public async Task<TaxIdValidation> ValidateTaxId(string taxId)
        {
            var response = await client.GetAsync(Router.ValidateTaxId(
                new Dictionary<string, object>()
                {
                    ["tax_id"] = taxId
                }
            ));
            await this.ThrowIfErrorAsync(response);
            var resultString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TaxIdValidation>(resultString, this.jsonSettings);
            return result;
        }
    }
}
