# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [4.6.0] - 2024-23-09

### Added

- Add List of Live Api Keys `Organizations.ListAsyncLiveApiKey`
- Add Delete of a Live Api Key `Organization.DeleteAsyncLiveApiKey`

## [4.5.0] - 2024-06-05

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
