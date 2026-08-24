using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface ICustomerWrapper
    {
        Task<SearchResult<Customer>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Customer> CreateAsync(Dictionary<string, object> data, Dictionary<string, object> queryParams = null, CancellationToken cancellationToken = default);
        Task<Customer> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<Customer> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Customer> UpdateAsync(string id, Dictionary<string, object> data, Dictionary<string, object> queryParams = null, CancellationToken cancellationToken = default);
        Task<TaxInfoValidation> ValidateTaxInfoAsync(string id, CancellationToken cancellationToken = default);
        Task SendEditLinkByEmailAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
    }
}
