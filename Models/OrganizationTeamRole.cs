using System;
using System.Collections.Generic;

namespace Facturapi
{
    public class OrganizationTeamRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TemplateCode { get; set; }
        public string Scope { get; set; }
        public string Organization { get; set; }
        public List<string> Operations { get; set; }
        public int UsedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Dictionary<string, object> CreatedBy { get; set; }
        public Dictionary<string, object> UpdatedBy { get; set; }
    }
}
