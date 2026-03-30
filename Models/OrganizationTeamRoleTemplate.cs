using System.Collections.Generic;

namespace Facturapi
{
    public class OrganizationTeamRoleTemplate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Operations { get; set; }
    }
}
