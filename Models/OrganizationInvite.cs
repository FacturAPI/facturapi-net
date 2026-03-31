using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class OrganizationInvite
    {
        public string Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Email { get; set; }
        public string OrganizationName { get; set; }
        public string Role { get; set; }
        public string RoleName { get; set; }
        public List<string> Roles { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
