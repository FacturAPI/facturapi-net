# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [6.4.0]
### Added
- Added `facturapi.Receipt.ToInvoiceAsync(Dictionary<string, object> data)` to call `POST /receipts/multi-invoice`.
- Added `facturapi.Receipt.PreviewToInvoicePdfAsync(Dictionary<string, object> data)` to call `POST /receipts/multi-invoice/preview/pdf`.

## [6.3.0] - 2026-04-23
### Added
- Added Carta Porte constants and description catalogs: customs regimes, transport keys, station types, SCT permits, COFEPRIS sectors, pharmaceutical forms, special transport conditions, material types, customs document types, transport unit/figure types, Istmo records, loading keys, maritime configuration, rail traffic, container types, maritime container types, rail car/service types, transfer motives, incoterms, and customs units.

## [6.2.0] - 2026-04-13
### Added
- Added method `facturapi.Organization.UpdateDefaultSeriesAsync` for updating default series

## [6.1.0] - 2026-04-01

### Added

- Added `TaxFactor` and `IepsMode` constants for tax payload fields.
- Added `CancellationMotive` and `CancellationStatus` constants for cancellation flows.
- Added `GlobalPeriodicity`, `GlobalMonths`, and `ExportCode` constants for global invoice payload fields.
- Added `InvoiceStatus` and `ReceiptStatus` constants for common document status handling.
- Added `receipt.cancellation_status_updated` to webhook event constants.

### Changed

- Updated `TaxSystem` constants to include missing SAT regime code `625`.
- Expanded `Taxability` constants to include codes `06`, `07`, and `08`.

## [6.0.0] - 2026-03-30

### Migration Guide

No changes are required if your code:
- Instantiates `FacturapiClient` and calls wrapper methods directly (for example `await client.Invoice.CreateAsync(...)`).
- Uses `var` when storing wrapper properties (for example `var invoices = client.Invoice;`).
- Does not depend on concrete wrapper types in method signatures, fields, or tests.

You need to update your code if you:
- Type wrapper properties as concrete classes (`CustomerWrapper`, `InvoiceWrapper`, etc.).
- Mock or inject concrete wrapper classes.
- Expose concrete wrappers in your own interfaces or public APIs.

Before (v5):
```csharp
CustomerWrapper customers = client.Customer;
```

After (v6):
```csharp
ICustomerWrapper customers = client.Customer;
```

### Breaking

- `IFacturapiClient` now exposes wrapper interfaces instead of concrete wrapper classes:
  - `ICustomerWrapper`, `IProductWrapper`, `IInvoiceWrapper`, `IOrganizationWrapper`, `IReceiptWrapper`, `IRetentionWrapper`, `ICatalogWrapper`, `ICartaporteCatalogWrapper`, `IToolWrapper`, `IWebhookWrapper`.
- `FacturapiClient` properties now return those interface types as part of the public contract.
- Dropped `net452` target framework support. The package now targets `netstandard2.0`, `net6.0`, and `net8.0`.

### Added

- New public interfaces for all wrapper surfaces to improve dependency injection and mocking in tests.
- `Invoice.StampDraftAsync` and legacy `StampDraft` now co-exist; `StampDraft` is marked as obsolete.
- Added initial `FacturapiTest` test project with regression coverage for query building and wrapper behavior.
- Added `FacturapiClient.CreateWithCustomHttpClient(...)` for advanced scenarios where consumers need to provide their own `HttpClient` without changing the default constructor.
- Added organization team-management endpoints to `Organization` / `IOrganizationWrapper`: access listing and retrieval, invite send/cancel/respond flows, role listing/templates/operations, role CRUD, and role reassignment for team members.

### Fixed

- `Retention.CancelAsync` is now consistent with the rest of the SDK: supports `CancellationToken`, disposes HTTP responses, and uses shared error handling (`ThrowIfErrorAsync`).
- Query string generation now handles null values and empty query dictionaries safely.
- README examples were corrected to align with the current async API surface and valid C# snippets.
- Internal async calls in wrapper implementations now consistently use `ConfigureAwait(false)`.

## [5.1.0] - 2026-03-12

### Fix

- Import for retention wrapper

## [5.1.0] - 2026-01-21

### Changed

- Method for cancel retentions must send motive and substitution

## [5.0.0] - 2025-12-10

### Breaking

- Wrappers can no longer be constructed directly; their constructors are internal and they are intended to be used only through `FacturapiClient`.
- Renamed existing methods to match documented C# surface: `Organization.List/Create/Update/DeleteSeriesAsync` (were `*SeriesGroupAsync`), `Invoice.UpdateStatusAsync` (was `UpdateStatus`), and `Tool.ValidateTaxIdAsync` (was `ValidateTaxId`).
- Carta Porte catalog methods moved to a dedicated `CartaporteCatalogWrapper`; `CartaporteCatalog` is now of that type, and `CatalogWrapper` retains only product/unit catalogs.

### Added

- Expose webhook methods through `FacturapiClient`/`IFacturapiClient`.
- New organization endpoints: `MeAsync` (`/organizations/me`), `CheckDomainIsAvailableAsync`, `UpdateReceiptSettingsAsync`, and `UpdateDomainAsync`.
- New `CartaporteCatalogWrapper` for Carta Porte catalog searches.
- Added `DomainAvailability` model for domain check responses.
- Added `Tool.HealthCheckAsync` for `/check`.
- `FacturapiException.Status` now surfaces the HTTP status code when available.
- Introduced `IFacturapiClient` so consumers can mock the client surface in tests.
- Optional `CancellationToken` parameters on client methods to allow request cancellation from callers.

### Changed

- `FacturapiClient` now implements `IDisposable`; call `Dispose()` when finished (or wrap in `using`) to release HTTP resources. If not disposed, garbage collection will eventually clean up, but explicit disposal avoids lingering HTTP connections.

### Fixed

- `Invoices.PreviewPdfAsync` now calls the documented POST endpoint with a JSON body (breaking change to the method signature).
- `Receipts.CreateGlobalInvoiceAsync` posts directly to `/receipts/global-invoice` and no longer requires an id (breaking change to the signature).
- Receipt routes now hit `/receipts/{id}` for cancel, invoice, email, and PDF download instead of invoice endpoints.
- `Organizations.CreateSeriesAsync` uses POST (not PUT) to `/organizations/{id}/series-group`, matching the API.

## [4.11.0] - 2025-12-10

### Added

- Methods to carta porte catalogs
- `SearchAirTranportCodes`, `SearchTransportConfigs`,`SearchRightsOfPassage`, `SearchCustomsDocuments`, `SearchPackagingTypes` `SearchTrailerTypes`, `SearchHazardousMaterials`, `SearchNavalAuthorizations`, `SearchPortStations`, `SearchMarineContainers`

## [4.10.1] - 2025-10-23

### Added

- Added fields to `Customer` model: `DefaultInvoiceUse`.

## [4.10.0] - 2025-09-04

### Added

- Add `Invoices.PreviewPdfAsync` method to preview invoice PDF before stamping.

## [4.9.1] - 2025-07-21

### Fixed

- Fix `Invoices.SendByEmailAsync`, `Receipts.SendByEmailAsync`, `Retentions.SendByEmailAsync` method and mark the `data` parameter as optional.

### Added

- Add `AddOns` property to `Organization` model to support Facturapi add-ons.
- Add missing properties to `Organization.Customization.PdfExtra` model: `AddressCodes`, `RoundUnitPrice`, `TaxBreakdown`, `IepsBreakdown`, `RenderCartaPorte`, and `RepeatSignature`.

## [4.9.0] - 2025-06-16

### Added

- Add `Organizations.UpdateSelfInvoiceSettingsAsync` method to update self invoice settings.

## [4.8.0] - 2025-04-22

### Added

- Add Create Webhook `Webhooks.CreateAsync`
- Add Update Webhook `Webhooks.UpdateAsync`
- Add Retrieve Webhook `Webhooks.RetrieveAsync`
- Add Delete Webhook `Webhooks.DeleteAsync`
- Add List Webhooks `Webhooks.ListAsync`
- Add Validate Signature Webhook `Webhooks.ValidateSignatureAsync`

## [4.7.1] - 2025-04-16

### Added

- Type IepsMode for Tax model
- Type Factor for Tax model

## [4.7.0] - 2025-02-25

### Added

- Support sending query params to `Customers.CreateAsync` and `Customers.UpdateAsync` methods.
- Added fields to `Customer` model: `SatValidatedAt`, `EditLink` and `EditLinkExpiresAt`.
- Added targets for .NET 6.0 and .NET 7.0.

## [4.6.0] - 2024-09-23

### Added

- Add List of Live Api Keys `Organizations.ListAsyncLiveApiKey`
- Add Delete of a Live Api Key `Organization.DeleteAsyncLiveApiKey`

## [4.5.0] - 2024-05-06

### Added

- New methods to manage Series: `Organizations.ListSeriesGroupAsync`, `Organizations.CreateSeriesGroupAsync`, `Organizations.UpdateSeriesGroupAsync`, `Organizations.DeleteSeriesGroupAsync`.

## [4.4.0] - 2024-05-27

### Added

- New methods for invoice drafts: `Invoices.UpdateStatusAsync`, `Invoices.UpdateDraftAsync`, `Invoices.StampDraftAsync` and `Invoices.CopyToDraftAsync`.

## [4.3.0] - 2024-04-17

### Added

- Global constants of taxability
- Taxability type in product type

## [4.2.0] - 2024-03-12

### Added

- Global types in invoice model
- Update invoice uses constants
- Update payment form constants
- Add new method for delete certs in organization

## [4.1.0] - 2023-12-06

### Added

- Support for more .NET versions: .NET Core, .NET 6 and .NET 4.5.2
- Allow passing options object to invoice creation

## [4.0.0] - 2023-12-05

### Breaking changes

- We now target .NET standard 2.0 instead of .NET framework 4.5.

### Added

- RESICO Tax system
- Download cancellation receipt XML and PDF

## [3.2.0] - 2023-07-12

### Added

- Download receipt PDF
- Send receipt by email

## [3.1.0] - 2022-03-14

### Added

- Tax Validation endpoint
- Customer `TaxSystem` property
- Invoice `Address` property

## [3.0.0] - 2022-01-31

- Added support for CFDI 4.0

## [2.1.1] - 2021-09-03

### Fixed

- Unit keys catalog was pointing to product keys catalog

## [2.1.0] - 2021-08-06

### Added

- Catalogs API
- Endpoint for validating tax id

## [2.0.1] - 2021-07-28

### Added

- Missing property on Stamp object: ComplementString

## [2.0.0] - 2021-04-19

### Breaking changes

- Removed deprecated property `Invoice.ForeignTrade`.
- Removed deprecated `Wrapper` class.

### Added

- Support for Receipts API
- Support for Retentions API
- Added missing properties on `Invoice` class: `ExternalId`, `Type`, `Stamp` and `Complements`.

## [1.0.3] - 2020-08-19

### Fixed

- `Invoice.Payments[].Related` should be a List

## [1.0.2] - 2019-09-12

### Fixed

- Bug at uploading certificates.

## [1.0.1] - 2019-08-25

### Fixed

- Problem uploading logo and certs for an organization.

## [1.0.0] - 2019-02-21

### Added

- Bunch of properties missing from the Invoice class like Payments, ForeignTrade, Related, etc.
- `TotalResults` property in search results.
- `FacturapiClient` replaces `Wrapper` and is now the preferred way of instanciating the API client. Usage is exactly the same. Actually this is more a renaming than a replacement.
- Now you can send parameters to the `Invoice.SendByEmail` method, which means you can specify the email address to send the invoice to.

### Deprecated

- Wrapper class. We're replacing it with the more descriptive `FacturapiClient`. `Wrapper` will be available during this version for keeping backwards compatibility, but will be removed in the next major release.

### Removed

- Deprecated static methods.
