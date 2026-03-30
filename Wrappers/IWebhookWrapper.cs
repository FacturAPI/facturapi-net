using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IWebhookWrapper
    {
        Task<SearchResult<Webhook>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Webhook> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Webhook> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<Webhook> UpdateAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Webhook> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Webhook> ValidateSignatureAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
    }
}
