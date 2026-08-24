# SDK Contribution Notes

## Wrapper Interface Compatibility

- `IFacturapiClient` and the `I*Wrapper` interfaces are supported public contracts for injection and mocks.
- Adding an API capability to its existing `I*Wrapper` is an additive minor SDK change, not a breaking major-version change. Manual interface implementations must add the new members.
- Do not introduce capability interfaces solely to avoid extending an existing resource wrapper.
- For SDK tests, prefer injecting an `HttpClient` with a custom `HttpMessageHandler`. Applications that need a smaller dependency should define an abstraction in their own integration layer.
