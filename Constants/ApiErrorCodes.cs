#pragma warning disable CS1591

namespace Facturapi
{
    /// <summary>
    /// Documented Facturapi API error codes. External PAC and SAT codes are returned in an error's Errors details and are not included here.
    /// </summary>
    public static class ApiErrorCodes
    {
        public static class CommonErrorCode
        {
            public const string CONFLICT = "conflict";
            public const string FORBIDDEN = "forbidden";
            public const string INTERNAL_ERROR = "internal_error";
            public const string NOT_FOUND = "not_found";
            public const string UNAUTHORIZED = "unauthorized";
        }

        public static class AuthErrorCode
        {
            public const string API_KEY_INVALID = "api_key_invalid";
            public const string API_KEY_NOT_ALLOWED = "api_key_not_allowed";
            public const string FEATURE_NOT_AVAILABLE = "feature_not_available";
            public const string LIVE_API_KEY_REQUIRED = "live_api_key_required";
            public const string MCP_PERMISSION_DENIED = "mcp_permission_denied";
            public const string MISSING_CREDENTIALS = "missing_credentials";
            public const string ORGANIZATION_INCOMPLETE = "organization_incomplete";
            public const string SUBSCRIPTION_REQUIRED = "subscription_required";
            public const string SUBSCRIPTION_LIVE_ACCESS_REQUIRED = "subscription_live_access_required";
            public const string USER_KEY_INVALID = "user_key_invalid";
            public const string USER_SUSPENDED = "user_suspended";
        }

        public static class RequestErrorCode
        {
            public const string IDEMPOTENCY_KEY_IN_USE = "idempotency_key_in_use";
            public const string RATE_LIMIT_EXCEEDED = "rate_limit_exceeded";
            public const string DATE_RANGE_TOO_LARGE = "date_range_too_large";
            public const string IMAGE_FILE_REQUIRED = "image_file_required";
            public const string INVALID_COUNTRY_CODE = "invalid_country_code";
            public const string INVALID_DATE = "invalid_date";
            public const string INVALID_DATE_RANGE = "invalid_date_range";
            public const string INVALID_IMAGE_FILE = "invalid_image_file";
            public const string INVALID_JSON = "invalid_json";
            public const string INVALID_REQUEST = "invalid_request";
            public const string INVALID_MULTIPART_FORM_DATA = "invalid_multipart_form_data";
            public const string INVALID_STATE_CODE = "invalid_state_code";
            public const string INVALID_TIMEZONE = "invalid_timezone";
            public const string MULTIPART_LIMIT_EXCEEDED = "multipart_limit_exceeded";
            public const string PAGE_TOO_LARGE = "page_too_large";
            public const string PAYLOAD_TOO_LARGE = "payload_too_large";
        }

        public static class CustomerErrorCode
        {
            public const string CUSTOMER_COULD_NOT_BE_RESOLVED = "customer_could_not_be_resolved";
            public const string CUSTOMER_EDIT_LINK_NOT_FOUND = "customer_edit_link_not_found";
            public const string CUSTOMER_EDIT_LINK_UNAVAILABLE = "customer_edit_link_unavailable";
            public const string CUSTOMER_EMAIL_REQUIRED = "customer_email_required";
            public const string CUSTOMER_HAS_INVOICES = "customer_has_invoices";
            public const string CUSTOMER_NOT_FOUND = "customer_not_found";
            public const string CUSTOMER_TAX_INFO_UNAVAILABLE = "customer_tax_info_unavailable";
        }

        public static class TaxInfoValidationCode
        {
            public const string LEGAL_NAME_MISMATCH = "legal_name_mismatch";
            public const string TAX_ADDRESS_ZIP_MISMATCH = "tax_address_zip_mismatch";
            public const string TAX_ID_NOT_FOUND = "tax_id_not_found";
            public const string TAX_SYSTEM_NOT_ALLOWED_FOR_TAX_ID = "tax_system_not_allowed_for_tax_id";
            public const string TAX_SYSTEM_NOT_IN_CATALOG = "tax_system_not_in_catalog";
        }

        public static class ProductErrorCode { public const string PRODUCT_NOT_FOUND = "product_not_found"; }
        public static class SupplierErrorCode { public const string SUPPLIER_NOT_FOUND = "supplier_not_found"; }

        public static class CatalogErrorCode
        {
            public const string PRODUCT_KEY_NOT_FOUND = "product_key_not_found";
            public const string TARIFF_CODE_NOT_FOUND = "tariff_code_not_found";
            public const string UNIT_KEY_NOT_FOUND = "unit_key_not_found";
        }

        public static class InvoiceErrorCode
        {
            public const string INVOICE_ALREADY_STAMPED = "invoice_already_stamped";
            public const string INVOICE_NOT_DRAFT = "invoice_not_draft";
            public const string INVOICE_NOT_FOUND = "invoice_not_found";
            public const string INVOICE_NOT_STAMPED = "invoice_not_stamped";
        }

        public static class InvoiceDraftErrorCode
        {
            public const string DRAFT_NOT_READY_TO_STAMP = "draft_not_ready_to_stamp";
            public const string DRAFT_UPDATE_IN_PROGRESS = "draft_update_in_progress";
        }

        public static class InvoiceStampingErrorCode
        {
            public const string INVOICE_STAMPING_FAILED = "invoice_stamping_failed";
            public const string INVOICE_STAMPING_SERVICE_UNAVAILABLE = "invoice_stamping_service_unavailable";
            public const string INVOICE_STAMPING_VALIDATION_ERROR = "invoice_stamping_validation_error";
            public const string MANIFESTO_SIGNATURE_FAILED = "manifesto_signature_failed";
            public const string STAMPING_IN_PROGRESS = "stamping_in_progress";
        }

        public static class InvoiceDeliveryErrorCode
        {
            public const string INVOICE_EMAIL_DELIVERY_FAILED = "invoice_email_delivery_failed";
            public const string INVOICE_EMAIL_RECIPIENT_REQUIRED = "invoice_email_recipient_required";
            public const string INVOICE_EMAIL_STATUS_NOT_ALLOWED = "invoice_email_status_not_allowed";
        }

        public static class InvoiceCancellationErrorCode
        {
            public const string INVOICE_CANCELLATION_IN_PROGRESS = "invoice_cancellation_in_progress";
            public const string INVOICE_CANCELLATION_RECEIPT_UNAVAILABLE = "invoice_cancellation_receipt_unavailable";
            public const string INVOICE_CANCELLATION_FAILED = "invoice_cancellation_failed";
            public const string INVOICE_CANCELLATION_NOT_ALLOWED = "invoice_cancellation_not_allowed";
            public const string INVOICE_CANCELLATION_NOT_FOUND = "invoice_cancellation_not_found";
            public const string INVOICE_CANCELLATION_RFC_MISMATCH = "invoice_cancellation_rfc_mismatch";
            public const string INVOICE_CANCELLATION_SERVICE_UNAVAILABLE = "invoice_cancellation_service_unavailable";
            public const string INVOICE_NOT_CANCELABLE = "invoice_not_cancelable";
            public const string INVOICE_NOT_CANCELABLE_BY_SAT = "invoice_not_cancelable_by_sat";
            public const string SUBSTITUTION_INVOICE_CANCELED = "substitution_invoice_canceled";
            public const string SUBSTITUTION_INVOICE_NOT_FOUND = "substitution_invoice_not_found";
            public const string SUBSTITUTION_INVOICE_REQUIRED = "substitution_invoice_required";
            public const string SUBSTITUTION_INVOICE_STATUS_NOT_ALLOWED = "substitution_invoice_status_not_allowed";
        }

        public static class ReceiptErrorCode
        {
            public const string RECEIPT_EXPIRED = "receipt_expired";
            public const string RECEIPT_NOT_FOUND = "receipt_not_found";
            public const string RECEIPT_NOT_OPEN = "receipt_not_open";
        }

        public static class ReceiptInvoicingErrorCode
        {
            public const string RECEIPT_INVOICING_ADDRESS_MISMATCH = "receipt_invoicing_address_mismatch";
            public const string RECEIPT_INVOICING_CUSTOMER_MISMATCH = "receipt_invoicing_customer_mismatch";
            public const string RECEIPT_INVOICING_TOO_MANY_ITEMS = "receipt_invoicing_too_many_items";
            public const string RECEIPT_KEYS_NOT_FOUND = "receipt_keys_not_found";
        }

        public static class ReceiptGlobalInvoiceErrorCode
        {
            public const string GLOBAL_INVOICE_TOO_MANY_ITEMS = "global_invoice_too_many_items";
            public const string INVALID_GLOBAL_INVOICE_PERIOD = "invalid_global_invoice_period";
        }

        public static class RetentionErrorCode
        {
            public const string INVALID_RETENTION_COMPLEMENT = "invalid_retention_complement";
            public const string RETENTION_NOT_FOUND = "retention_not_found";
            public const string RETENTION_NOT_STAMPED = "retention_not_stamped";
            public const string RETENTION_NOT_CANCELABLE = "retention_not_cancelable";
        }

        public static class RetentionDeliveryErrorCode
        {
            public const string RETENTION_EMAIL_DELIVERY_FAILED = "retention_email_delivery_failed";
            public const string RETENTION_EMAIL_RECIPIENT_REQUIRED = "retention_email_recipient_required";
            public const string RETENTION_EMAIL_STATUS_NOT_ALLOWED = "retention_email_status_not_allowed";
        }

        public static class RetentionCancellationErrorCode
        {
            public const string RETENTION_CANCELLATION_FAILED = "retention_cancellation_failed";
            public const string RETENTION_CANCELLATION_SERVICE_UNAVAILABLE = "retention_cancellation_service_unavailable";
        }

        public static class RetentionStampingErrorCode
        {
            public const string RETENTION_STAMPING_FAILED = "retention_stamping_failed";
            public const string RETENTION_STAMPING_SERVICE_UNAVAILABLE = "retention_stamping_service_unavailable";
            public const string RETENTION_STAMPING_VALIDATION_ERROR = "retention_stamping_validation_error";
        }

        public static class OrganizationErrorCode
        {
            public const string INVALID_OPERATION = "invalid_operation";
            public const string INVALID_USER_ID = "invalid_user_id";
            public const string ORGANIZATION_NOT_FOUND = "organization_not_found";
            public const string SUBSCRIPTION_ACTIVE_REQUIRED = "subscription_active_required";
        }

        public static class OrganizationSettingsErrorCode
        {
            public const string CERTIFICATE_EXPIRED = "certificate_expired";
            public const string CERTIFICATE_FILE_REQUIRED = "certificate_file_required";
            public const string CERTIFICATE_FILES_INVALID = "certificate_files_invalid";
            public const string CERTIFICATE_FILES_REQUIRED = "certificate_files_required";
            public const string CERTIFICATE_FIEL_RFC_MISMATCH = "certificate_fiel_rfc_mismatch";
            public const string CERTIFICATE_INVALID = "certificate_invalid";
            public const string CERTIFICATE_NOT_YET_VALID = "certificate_not_yet_valid";
            public const string CERTIFICATE_PREVIOUS_RFC_MISMATCH = "certificate_previous_rfc_mismatch";
            public const string CSD_REQUIRED = "csd_required";
            public const string FIEL_INVALID = "fiel_invalid";
            public const string FIEL_RFC_MISMATCH = "fiel_rfc_mismatch";
            public const string FIEL_REQUIRED = "fiel_required";
            public const string ORGANIZATION_DOMAIN_CHANGE_NOT_ALLOWED = "organization_domain_change_not_allowed";
            public const string ORGANIZATION_DOMAIN_UNAVAILABLE = "organization_domain_unavailable";
            public const string ORGANIZATION_SETTINGS_INVALID = "organization_settings_invalid";
            public const string ORGANIZATION_SUPPORT_EMAIL_REQUIRED = "organization_support_email_required";
            public const string ORGANIZATION_TAX_INFO_INVALID = "organization_tax_info_invalid";
            public const string PRIVATE_KEY_CERTIFICATE_MISMATCH = "private_key_certificate_mismatch";
            public const string PRIVATE_KEY_FILE_REQUIRED = "private_key_file_required";
            public const string PRIVATE_KEY_PASSWORD_INCORRECT = "private_key_password_incorrect";
        }

        public static class OrganizationInviteErrorCode
        {
            public const string INVITE_EMAIL_DELIVERY_FAILED = "invite_email_delivery_failed";
            public const string INVITE_EMAIL_MISMATCH = "invite_email_mismatch";
            public const string INVITE_EXPIRED = "invite_expired";
            public const string INVITE_NOT_FOUND = "invite_not_found";
            public const string INVITE_ROLE_UNAVAILABLE = "invite_role_unavailable";
            public const string USER_ALREADY_IN_ORGANIZATION = "user_already_in_organization";
        }

        public static class OrganizationAccessErrorCode
        {
            public const string ORGANIZATION_ADMIN_ACCESS_CANNOT_BE_REMOVED = "organization_admin_access_cannot_be_removed";
            public const string ORGANIZATION_ADMIN_ASSIGNMENT_CANNOT_BE_EDITED = "organization_admin_assignment_cannot_be_edited";
            public const string ORGANIZATION_ADMIN_ROLE_CANNOT_BE_DELETED = "organization_admin_role_cannot_be_deleted";
            public const string ORGANIZATION_ADMIN_ROLE_CANNOT_BE_EDITED = "organization_admin_role_cannot_be_edited";
            public const string ORGANIZATION_ADMIN_ROLE_REQUIRED = "organization_admin_role_required";
            public const string ORGANIZATION_ID_NOT_ALLOWED = "organization_id_not_allowed";
            public const string ORGANIZATION_ID_REQUIRED = "organization_id_required";
            public const string OWNER_ACCESS_CANNOT_BE_REASSIGNED = "owner_access_cannot_be_reassigned";
            public const string OWNER_ACCESS_CANNOT_BE_REMOVED = "owner_access_cannot_be_removed";
            public const string ROLE_HAS_ASSIGNED_USERS = "role_has_assigned_users";
            public const string ROLE_TEMPLATE_NOT_FOUND = "role_template_not_found";
            public const string ROLE_NOT_FOUND = "role_not_found";
            public const string USER_ACCESS_NOT_FOUND = "user_access_not_found";
        }

        public static class OrganizationSeriesErrorCode
        {
            public const string SERIES_ALREADY_EXISTS = "series_already_exists";
            public const string SERIES_NOT_FOUND = "series_not_found";
        }

        public static class WebhookErrorCode
        {
            public const string WEBHOOK_DELIVERY_ATTEMPT_NOT_FOUND = "webhook_delivery_attempt_not_found";
            public const string WEBHOOK_NOT_FOUND = "webhook_not_found";
            public const string WEBHOOK_SIGNATURE_INVALID = "webhook_signature_invalid";
        }

        public static class ToolErrorCode
        {
            public const string TAX_ID_VALIDATION_FAILED = "tax_id_validation_failed";
            public const string TAX_ID_VALIDATION_SERVICE_UNAVAILABLE = "tax_id_validation_service_unavailable";
        }

        public static class ValidationErrorCode
        {
            public const string AMOUNT_EXCEEDS_RELATED_DOCUMENT_BALANCE = "amount_exceeds_related_document_balance";
            public const string EXCHANGE_RATE_TOO_LARGE = "exchange_rate_too_large";
            public const string EXCHANGE_RATE_TOO_SMALL = "exchange_rate_too_small";
            public const string INVALID_FORMAT = "invalid_format";
            public const string INVALID_LENGTH = "invalid_length";
            public const string INVALID_TYPE = "invalid_type";
            public const string NOT_FOUND = "not_found";
            public const string NOT_ALLOWED = "not_allowed";
            public const string REQUIRED = "required";
            public const string TOO_LARGE = "too_large";
            public const string TOO_SMALL = "too_small";
            public const string UNKNOWN_FIELD = "unknown_field";
            public const string INVALID_VALUE = "invalid_value";
        }
    }
}

#pragma warning restore CS1591
