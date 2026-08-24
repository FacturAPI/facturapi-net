using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IProductWrapper
    {
        Task<SearchResult<Product>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Product> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Product> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<Product> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Product> UpdateAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
    }
}
