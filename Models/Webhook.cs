using System;

namespace Facturapi
{
    public class Webhook

    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public string[] EnabledEvents { get; set; }

        public bool Livemode { get; set; }

        public Organization Organization { get; set; }

        public string Url { get; set; }

        public string Status { get; set; }
    }
}