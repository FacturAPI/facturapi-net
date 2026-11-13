Facturapi .NET Library
======================

[![NuGet version](https://badge.fury.io/nu/Facturapi.svg)](https://www.nuget.org/packages/Facturapi/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Facturapi.svg)](https://www.nuget.org/packages/Facturapi/)
[![CI](https://github.com/FacturAPI/facturapi-net/actions/workflows/ci.yml/badge.svg)](https://github.com/FacturAPI/facturapi-net/actions/workflows/ci.yml)

Librería oficial para .NET de https://www.facturapi.io

Facturapi ayuda a generar facturas electrónicas válidas en México (CFDI) de la manera más fácil posible.

Si alguna vez has usado [Stripe](https://stripe.com) o [Conekta](https://conekta.io), verás que Facturapi es igual de sencillo de entender e integrar a tu aplicación.

## Migración a v6

### ¿Cuándo NO necesitas cambiar nada?

No necesitas actualizar tu código si:
- Creas `FacturapiClient` y llamas métodos directamente (por ejemplo `await client.Invoice.CreateAsync(...)`).
- Usas `var` al guardar wrappers (por ejemplo `var invoices = client.Invoice;`).
- No dependes de tipos concretos de wrappers en firmas, propiedades o pruebas.

### ¿Cuándo SÍ necesitas actualizar?

Debes ajustar tu código si:
- Declaras wrappers como clases concretas (`CustomerWrapper`, `InvoiceWrapper`, etc.).
- Mockeas wrappers concretos en pruebas.
- Expones wrappers concretos en tus propias interfaces o APIs públicas.

Antes (v5):

```csharp
CustomerWrapper customers = client.Customer;
```

Después (v6):

```csharp
ICustomerWrapper customers = client.Customer;
```

## Instalación

Puedes instalar Facturapi en tu proyecto usando [Nuget](https://www.nuget.org/)

```bash
PM> Install-Package Facturapi
```

## Antes de comenzar

Asegúrate de haber creado tu cuenta gratuita en [Facturapi](https://www.facturapi.io) y de tener a la mano tus **API Keys**.

### Inicializa la librería usando tus llaves secretas

Empieza por crear una instancia del Wrapper de Facturapi usando tu llave secreta.

```csharp
using Facturapi;

// Esto asegura que puedas usar diferentes ApiKeys en diferentes instancias de Wrapper
var facturapi = new FacturapiClient("TU_API_KEY");
// Después, procede a llamar a los métodos como muestra la documentación.
var invoice = await facturapi.Invoice.CreateAsync(...);
```

### Catálogos de nómina

```csharp
var deductions = await facturapi.NominaCatalog.SearchDeductions(new Dictionary<string, object> { ["q"] = "001" });
var perceptions = await facturapi.NominaCatalog.SearchPerceptions(new Dictionary<string, object> { ["q"] = "001" });
```

### Escenario avanzado: usar tu propio `HttpClient`

Para la mayoría de usuarios, usa el constructor normal de `FacturapiClient`.
Si necesitas un `HttpClient` custom (por ejemplo para un `HttpMessageHandler` propio), usa el factory avanzado:

```csharp
using System.Net.Http;
using Facturapi;

var customHttpClient = new HttpClient();
var facturapi = FacturapiClient.CreateWithCustomHttpClient("TU_API_KEY", customHttpClient);
```

### Métodos asíncronos (async, await)

Esta librería utiliza métodos asíncronos. Si tu aplicación no tiene código asíncrono, puedes convertir un método asíncrono en síncrono de la siguiente manera:

```csharp
// Asíncrono
var customers = await facturapi.Customer.ListAsync();

// Síncrono
var customers = facturapi.Customer.ListAsync().GetAwaiter().GetResult();
```

## Uso de la librería

### Crear un cliente

```csharp
var customer = await facturapi.Customer.CreateAsync(new Dictionary<string, object>
{
  ["legal_name"] = "Walter White",    // Razón social
  ["tax_id"] = "ABCD101010XYZ",       // RFC
  ["email"] = "walter@example.com",   // Email a donde se enviará la factura
  ["address"] = new Dictionary<string, object>
  {
    ["street"] = "Sunset Blvd",       // Calle
    ["exterior"] = "104",             // Número exterior
    ["neighborhood"] = "Roma Norte",  // Colonia
    ["zip"] = "44940"                 // Código postal
    // NOTA: La ciudad, municipio, estado y país se llenan automáticamente
    // a partir del código postal, pero si quieres puedes especificar sus valores.
  }
});

// Recuerda guardar el customer.id en tu base de datos, lo
// necesitarás a la hora de emitirle una factura.
```

### Crear un producto

```csharp
var product = await facturapi.Product.CreateAsync(new Dictionary<string, object>
{
  ["description"] = "iPhone 8",
  ["product_key"] = "4319150114",   // Clave Producto/Servicio del catálogo del SAT. Para obtenerla más fácilmente
                              // utiliza nuestra herramienta de búsqueda de claves en tu dashboard.
  ["price"] = 345.60          // Precio con IVA incluído, a menos que se especifique lo contrario.
  // Por default, se creará un impuesto automáticamente a partir del precio, aplicando el IVA al 16%.
  // Pero puedes sobreescribir los impuestos aplicables a este producto especificando un arreglo de impuestos:
  // taxes: new Dictionary<string,object>[] {
  //   new Dictionary<string, object>
  //   {
  //     ["type"] = Facturapi.TaxType.IVA,
  //     ["rate"] = 0.16
  //   },
  //   new Dictionary<string, object>
  //   {
  //     ["type"] = Facturapi.TaxType.ISR,
  //     ["rate"] = 0.03666,
  //     ["withholding"] = true
  //   }
  // }
});

// Recuerda guardar el product.id para generar facturas que incluyan este producto.
```

### Crear una factura

```csharp
var invoice = await facturapi.Invoice.CreateAsync(new Dictionary<string, object>
{
  ["customer"] = "ID_DEL_CLIENTE",	  // Para clientes no registrados, puedes asignar
									  // un Dictionary con los datos del cliente.
  ["items"] = new Dictionary<string, object>[]
  { // Puedes agregar tantos items como necesites a este arreglo
    new Dictionary<string, object>
    {
      ["quantity"] = 2,               // Opcional. Default: 1.
      ["product"] = "ID_DEL_PRODUCTO" // Para productos no registrados, puedes asignar
                                      // un Dictionary con los datos del producto.
    }
  },
  ["payment_form"] = Facturapi.PaymentForm.DINERO_ELECTRONICO
});
```

#### Descargar factura

```csharp
// Una vez creada la factura, puedes descargar el PDF y el XML comprimidos
// en un archivo ZIP.
var zipStream = await facturapi.Invoice.DownloadZipAsync(invoice.Id);
// O bien, el XML y el PDF por separado
var xmlStream = await facturapi.Invoice.DownloadXmlAsync(invoice.Id);
var pdfStream = await facturapi.Invoice.DownloadPdfAsync(invoice.Id);
// Y luego guardarlo en un archivo del disco duro
using var file = new System.IO.FileStream("C:\\route\\to\\save\\invoice.zip", System.IO.FileMode.Create);
await zipStream.CopyToAsync(file);
```

#### Envía la factura por correo electrónico

```csharp
await facturapi.Invoice.SendByEmailAsync(invoice.Id);
```

## Documentación

Hay muchas más cosas que puedes hacer con esta librería: listar, consultar, actualizar y eliminar clientes, productos y facturas.

Ve la documentación completa en http://docs.facturapi.io.

## Ayuda

### ¿Encontraste un bug?

Por favor repórtalo en el Issue Tracker de este repo.

### ¿Quieres contribuir?

Mándanos un Pull Request! Agradecemos todo tipo de ayuda :)

### Contáctanos

contacto@facturapi.io

## Entradas y respuestas tipadas

Los recursos de clientes, productos, facturas, recibos, retenciones, organizaciones, webhooks, herramientas y catálogos se generan desde la especificación pública de la API. Esto incluye los complementos de pago, nómina, Carta Porte, comercio exterior y leyendas fiscales.

Puedes seguir enviando `Dictionary<string, object>`. Los modelos de entrada ofrecen propiedades, documentación en IntelliSense y constructores con los campos requeridos de cada variante. Sólo se serializan los campos asignados: omitir una propiedad y asignarle `null` conservan sus diferencias.

```csharp
var customer = await facturapi.Customer.CreateNationalAsync(
    new CustomerNationalCreateInput("Dunder Mifflin", "ABC101010111", "601",
        new CustomerNationalAddressInput("86500")));
var product = await facturapi.Product.CreateAsync(
    new ProductCreateInput("Consultoría", "84111506", 100.25m));
var invoice = await facturapi.Invoice.CreateIngresoAsync(
    new InvoiceIngresoInput(new List<InvoiceItemInput>
    {
        new InvoiceItemInput(product.Id) { Quantity = 1 }
    }, PaymentForm.TRANSFERENCIA_ELECTRONICA) { Customer = customer.Id });
```

También puedes elegir las variantes de Egreso, Pago, Nómina o Traslado. Los métodos `CreateDraftIngresoAsync`, `CreateDraftEgresoAsync`, `CreateDraftPagoAsync`, `CreateDraftNominaAsync` y `CreateDraftTrasladoAsync` usan modelos de borrador que permiten datos parciales y seleccionan el tipo y estado correspondientes:

```csharp
var draft = await facturapi.Invoice.CreateDraftIngresoAsync(new InvoiceIngresoDraftInput());
await facturapi.Invoice.CancelAsync(invoice.Id, new CancellationWithSubstitutionInput(
    CancellationMotive.COMPROBANTE_EMITIDO_CON_ERRORES_CON_RELACION, "replacement_id"));
```

`CancellationWithSubstitutionInput` requiere tanto motivo como sustitución, para los motivos 01 y 04. `CancellationQueryInput` conserva la entrada general, incluida la eliminación de borradores sin motivo. Para clientes están disponibles las variantes nacional, extranjera, de RFC genérico y de enlace de edición.

Los filtros tienen modelos propios, como `InvoiceListParams`. Los campos de fecha y hora (`date-time`) de entrada aceptan texto ISO 8601, `DateTime` o `DateTimeOffset`:

```csharp
var page = await facturapi.Invoice.ListAsync(new InvoiceListParams
{
    Pagination = "cursor",
    Date = new DateRangeInput { Gte = DateTime.UtcNow.AddDays(-30) }
});
```

Cuando un campo acepta formas diferentes se usa `OneOf<...>`, por ejemplo ID de cliente u objeto de cliente, y correo individual o lista. Las conversiones implícitas permiten asignar cada alternativa; se envía su valor directamente, sin envoltura JSON. Para inspeccionar una respuesta usa `TryGet<T>(out var value)`.

Los complementos concretos se pueden agregar a `List<InvoiceComplementInput>`. Los complementos personalizados y la addenda conservan su XML como texto. Los objetos abiertos de la API mantienen sus valores dinámicos. El tipado es una ayuda: los catálogos y valores literales se documentan como strings, y las validaciones fiscales y de negocio siguen a cargo de la API.

`Receipt.SummarizeToInvoiceAsync(new ToInvoiceInput(keys))` obtiene el resumen de recibos sin crear una factura. Los métodos de URL firmada devuelven `SignedDownloadUrl`; las descargas siguen devolviendo un `Stream` en memoria que debes disponer. Las cargas de certificados, FIEL y logotipo reciben streams y los disponen al terminar la solicitud.

`FacturapiException.ErrorDetails` expone detalles de error tipados, con `Code`, `Source`, `Path`, `Location` y `Message`. `Errors` conserva el JSON original; no necesitas analizar los mensajes para identificar un código.

### Webhooks

`Webhook.ValidateSignatureAsync(secret, signature, payload)` verifica localmente HMAC SHA-256 y devuelve `ApiEvent`. Pasa el texto JSON original del request, conservando sus bytes UTF-8 y espacios. La sobrecarga con diccionario sigue admitiendo `secret`, `signature` y `payload`.

`ApiEvent.Data.Object` distingue facturas, recibos y clientes usando el tipo indicado por el evento. `Webhook.ValidateSignatureRemotelyAsync` permite usar el endpoint de verificación y conserva su respuesta como string u objeto abierto, según el payload enviado.

## Migración a v8

Las llamadas con diccionarios conservan sus nombres y argumentos. Quienes sólo envían diccionarios, esperan el resultado con `var` y leen propiedades que conservan su tipo normalmente no necesitan cambiar esas llamadas. Revisa estos casos al actualizar:

| Uso anterior | Cambio en v8 |
|---|---|
| Fechas siempre presentes | Las fechas que admiten ausencia o `null`, como `Invoice.Date`, `Customer.SatValidatedAt` y `Customer.EditLinkExpiresAt`, son `DateTime?`. Maneja el caso nulo antes de acceder a `.Value`. |
| Modelos numéricos y campos opcionales | `number` se representa con `decimal`, `integer` con `int`, y los campos opcionales usan tipos anulables. Esto incluye folios y metadatos de respuestas parciales. |
| `Retention.*` devuelve `Invoice` | Devuelve `Retention` o `SearchResult<Retention>`. |
| ZIP requests como diccionarios | Devuelven `InvoiceZipRequest`; usa `.Id`, `.Status`, etc. en lugar de indexadores. |
| Llaves Live o series como objeto individual | `ListLiveApiKeysAsync` y las operaciones de crear, actualizar y eliminar series devuelven listas de los modelos correspondientes. `DeleteCertificateAsync` devuelve `OrganizationDeleteCerts`. |
| Métodos que descartaban el resultado | Envíos por correo y operaciones de recibos exponen las respuestas de la API, incluidos los resultados de facturación y resúmenes. |
| `Invoice.Currecy` y `SeriesGroup.Name` | Usa los nombres corregidos `Currency` y `Series`. |
| Implementaciones manuales de interfaces | Actualiza los miembros y firmas de `I*Wrapper` e incorpora `ComercioExteriorCatalog` a `IFacturapiClient`. |
| Verificación de webhook como `Webhook` | `ValidateSignatureAsync` devuelve `ApiEvent` y verifica localmente. El acceso remoto explícito conserva el payload original. |

Las respuestas incluyen más campos y objetos específicos del contrato; las fechas locales del timbre SAT que la API entrega como texto se conservan como strings. Consulta la documentación XML de cada propiedad cuando adaptes asignaciones a tipos concretos.

## Generación del SDK

Para desarrollar el SDK, instala Node.js 24+ y pnpm 11.9:

```sh
pnpm install --frozen-lockfile
pnpm generate:sdk
pnpm test:codegen
pnpm generate:sdk:check
```

`openapi/source.json` fija una revisión pública del contrato de API v2. `pnpm sync:openapi [public-ref]` actualiza esa revisión; después ejecuta la generación y las pruebas de .NET. La generación lee el spec de esa revisión en memoria. Los builds y los consumidores usan los archivos generados que se incluyen en el repositorio, sin descargar el spec ni instalar Node.js.

El compilador de TypeScript resuelve referencias, intersecciones y variantes después de `openapi-typescript`; los renderers de C# producen modelos, métodos y documentación. Los bindings conservan nombres y firmas del SDK, formatos de descarga y variantes de conveniencia. Toda operación pública debe tener un método asociado y CI verifica que la generación esté actualizada. Las constantes y los helpers locales se mantienen aparte cuando no derivan de un contrato HTTP.
