using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public class ToolWrapper : BaseWrapper
    {
        internal ToolWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<TaxIdValidation> ValidateTaxId(string taxId)
        {
            using (var response = await client.GetAsync(Router.ValidateTaxId(
                new Dictionary<string, object>()
                {
                    ["tax_id"] = taxId
                }
            )))
            {
                await this.ThrowIfErrorAsync(response);
                var resultString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TaxIdValidation>(resultString, this.jsonSettings);
                return result;
            }
        }
    }
}
