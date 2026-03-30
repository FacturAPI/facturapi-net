using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class OrganizationUserAccess
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public string Organization { get; set; }
        public List<string> Operations { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
