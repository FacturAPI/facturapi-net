using System;

namespace Facturapi
{
    public class SignedDownloadUrl
    {
        public string Url { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public string ContentType { get; set; }
        public string Filename { get; set; }
    }
}
