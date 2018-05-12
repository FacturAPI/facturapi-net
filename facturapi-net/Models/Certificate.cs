using System;

namespace Facturapi
{
    public class Certificate
    {
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool HasCertificate { get; set; }
    }
}