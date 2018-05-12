using System;
using System.Collections.Generic;

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
    }
}
