using System;

namespace Facturapi
{
    public class WebhookValidateSignature

    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool Livemode { get; set; }

        public string Organization { get; set; }

        public string Type { get; set; }

        public object Data { get; set; }

    }
}