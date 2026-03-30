using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Facturapi.Wrappers
{
    public interface IOrganizationWrapper
    {
        Task<SearchResult<Organization>> ListAsync(Dictionary<string, object> query = null, CancellationToken cancellationToken = default);
        Task<Organization> MeAsync(CancellationToken cancellationToken = default);
        Task<DomainAvailability> CheckDomainIsAvailableAsync(string domain, CancellationToken cancellationToken = default);
        Task<Organization> CreateAsync(Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Organization> RetrieveAsync(string id, CancellationToken cancellationToken = default);
        Task<Organization> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Organization> UploadLogoAsync(string id, Stream file, CancellationToken cancellationToken = default);
        Task<Organization> UploadCertificateAsync(string id, Stream cerFile, Stream keyFile, string password, CancellationToken cancellationToken = default);
        Task<Certificate> DeleteCertificateAsync(string id, CancellationToken cancellationToken = default);
        Task<Organization> UpdateLegalAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Organization> UpdateReceiptSettingsAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Organization> UpdateCustomizationAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<Organization> UpdateDomainAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<string> GetTestApiKeyAsync(string id, CancellationToken cancellationToken = default);
        Task<string> RenewTestApiKeyAsync(string id, CancellationToken cancellationToken = default);
        Task<LiveApiKey> ListLiveApiKeysAsync(string id, CancellationToken cancellationToken = default);
        Task<string> RenewLiveApiKeyAsync(string id, CancellationToken cancellationToken = default);
        Task<List<SeriesGroup>> ListSeriesAsync(string id, CancellationToken cancellationToken = default);
        Task<SeriesGroup> CreateSeriesAsync(string id, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<SeriesGroup> UpdateSeriesAsync(string id, string seriesName, Dictionary<string, object> data, CancellationToken cancellationToken = default);
        Task<SeriesGroup> DeleteSeriesAsync(string id, string seriesName, CancellationToken cancellationToken = default);
        Task<List<LiveApiKey>> DeleteLiveApiKeyAsync(string id, string apiKeyId, CancellationToken cancellationToken = default);
        Task<Organization> UpdateSelfInvoiceSettingsAsync(string organizationId, Dictionary<string, object> data, CancellationToken cancellationToken = default);
    }
}
