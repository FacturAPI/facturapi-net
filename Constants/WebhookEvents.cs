namespace Facturapi
{
    public static class WebhooksEvents
    {
        public const string INVOICE_CREATED = "invoice.global_invoice_created";
        public const string STATUS_UPDATED = "invoice.status_updated";
        public const string CANCELLATION_STATUS_UPDATED = "invoice.cancellation_status_updated";
        public const string SELF_INVOICE_COMPLETE = "receipt.self_invoice_complete";
        public const string RECEIPT_STATUS_UPDATED = "receipt.status_updated";
        public const string RECEIPT_CANCELLATION_STATUS_UPDATED = "receipt.cancellation_status_updated";
    }
}
