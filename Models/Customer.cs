using System;

namespace Facturapi
{
    public class Customer
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Livemode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public string LegalName { get; set; }
        public string TaxId { get; set; }
        public string TaxSystem { get; set; }
        public DateTime SatValidatedAt { get; set; }
        public string EditLink { get; set; }
        public DateTime EditLinkExpiresAt { get; set; }
    }
}
