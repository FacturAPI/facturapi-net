using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Facturapi.Wrappers
{
    public class ToolWrapper : BaseWrapper
    {
        internal ToolWrapper(string apiKey, string apiVersion, HttpClient httpClient) : base(apiKey, apiVersion, httpClient)
        {
        }

        public async Task<TaxIdValidation> ValidateTaxId(string taxId, CancellationToken cancellationToken = default)
        {
            using (var response = await client.GetAsync(Router.ValidateTaxId(
                new Dictionary<string, object>()
                {
                    ["tax_id"] = taxId
                }
            ), cancellationToken))
            {
                await this.ThrowIfErrorAsync(response, cancellationToken);
                var resultString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<TaxIdValidation>(resultString, this.jsonSettings);
                return result;
            }
        }
    }
}
