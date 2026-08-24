using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IToolWrapper
    {
        Task<TaxIdValidation> ValidateTaxIdAsync(string taxId, CancellationToken cancellationToken = default);
        Task<bool> HealthCheckAsync(CancellationToken cancellationToken = default);
    }
}
