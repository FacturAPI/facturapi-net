# Best-effort typing scope

This audit covers the pinned API v2 contract in `source.json`, comparing the
TypeScript representation with the useful C# equivalents. Endpoint coverage and
compile-time guarantees are different: every operation has a binding, while the
following table records the additional help available for conditional shapes.

| Area | C# representation |
| --- | --- |
| Customers | National, foreign and generic inputs with their required constructor fields; creation overloads and edit-link helpers for incomplete data. |
| Invoice creation | Ingreso, Egreso, Pago, Nómina and Traslado inputs; partial drafts select type and status; typed creation overloads and named helpers. |
| Cancellation | Substitution input requires motive and replacement for the 01/04 family; the general query still supports other motives and draft deletion. |
| Invoice responses | Draft, pending, failed and stamped subtypes; stamped responses narrow UUID, date and stamp. Unknown statuses retain the base model. |
| Stamp responses | Version 1.0/1.1 subtypes; 1.1 narrows the provider RFC. SAT local dates remain strings. |
| Complements and events | Discriminator-selected response subtypes; concrete input families and `OneOf` for overlapping families or heterogeneous shapes. |
| Payroll | Perception 019 has a subtype requiring overtime details; IM has a subtype requiring the own-resource amount. These types work in nested payroll lists and issuer objects. General inputs remain available. |
| Carta Porte | Autotransport constructors require vehicle identification and insurance; insurance constructors require liability insurer and policy. Other transport/nested inputs retain their schema-required fields. |
| Foreign trade and item complements | Typed nested inputs and ID/object/XML alternatives. Untagged response object unions require an unambiguous recognized shape. |
| Receipt invoicing | Typed invoice/summary alternatives and a dedicated summary helper. Explicit receipt selection requires from/to/receipts; period selection has a separate input without a receipt field. Global invoicing returns a nullable invoice. |
| Retentions | Complete and draft input variants; complete requires the customer, retention code, period and totals arguments. Only the explicit draft variant selects draft automatically. |
| Organizations, products, tools and catalogs | Required constructor arguments, typed nested fields, filters, numeric/date types and nullable results. No additional conditional input families were identified in their generated contract types. |
| Downloads, uploads, errors and open JSON | Streams, typed signed URLs, multipart inputs and semantic error details. Open JSON remains dynamic. |

## Generation rules

- TypeScript resolves references, intersections and unions. Required-only `allOf`
  fragments are lifted onto their containing intersection before that step so
  referenced required fields are retained.
- Input selectors must match exactly one alternative using literal values,
  required fields or absent fields. Single required literals are supplied by the
  constructor; optional draft statuses are never inserted automatically.
- Configured canonical property schemas must be mutually assignable to the
  property's non-nullable contract type. This prevents a nested broad shape from
  hiding a useful named variant, and fails generation if the binding drifts.
- Output hierarchies require disjoint string discriminator values; ambiguous
  output shapes retain broad models. `OneOf` preserves heterogeneous JSON values.

## Remaining limits

This is not identical to TypeScript's structural type system. C# string fields do
not reject other string literals, checking a base `Status` string does not narrow
the variable, and base/dictionary inputs remain escape hatches. Use pattern
matching on response subtypes and specific input constructors for stronger help.
Inputs remain mutable; a caller can change fields after construction.

General inputs do not enforce every conditional rule. For example, the general
payroll input still accepts a string perception code without requiring overtime;
the specific subtype supplies that help. Required retention constructor arguments
retain nullable fields where the shared schema exposes null: exclusions expressed
through `not` are not converted into a complete C# constraint system. Neither
prose-only rules nor general `if`/`then`/`not`, numeric ranges, array lengths,
formats or fiscal arithmetic become exhaustive client validation. The API remains
responsible for validating the request.
