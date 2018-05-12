using Facturapi.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Facturapi
{
    public class Organization
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsProductionReady { get; set; }
        public List<CompletionStep> PendingSteps { get; set; }
        public Legal Legal { get; set; }
        public Customization Customization { get; set; }
        public Certificate Certificate { get; set; }

        public static Task<SearchResult<Organization>> ListAsync (Dictionary<string, object> query = null)
        {
            return new OrganizationWrapper().ListAsync(query);
        }

        public static Task<Organization> CreateAsync (Dictionary<string, object> data)
        {
            return new OrganizationWrapper().CreateAsync(data);
        }

        public static Task<Organization> RetrieveAsync (string id)
        {
            return new OrganizationWrapper().RetrieveAsync(id);
        }

        public static Task<Organization> DeleteAsync (string id)
        {
            return new OrganizationWrapper().DeleteAsync(id);
        }

        public static Task<Organization> UploadLogoAsync (string id, Stream file)
        {
            return new OrganizationWrapper().UploadLogoAsync(id, file);
        }
        
        public static Task<Organization> UploadCertificateAsync (string id, Stream cerFile, Stream keyFile, string password)
        {
            return new OrganizationWrapper().UploadCertificateAsync(id, cerFile, keyFile, password);
        }
    }
}
