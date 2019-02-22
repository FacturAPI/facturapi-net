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
    }
}
