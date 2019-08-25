# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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