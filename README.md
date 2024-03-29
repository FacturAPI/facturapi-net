Facturapi .NET Library
======================

[![NuGet version](https://badge.fury.io/nu/Facturapi.svg)](https://www.nuget.org/packages/Facturapi/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Facturapi.svg)](https://www.nuget.org/packages/Facturapi/)

Librería oficial para .NET de https://www.facturapi.io

Facturapi ayuda a generar facturas electrónicas válidas en México (CFDI) de la manera más fácil posible.

Si alguna vez has usado [Stripe](https://stripe.com) o [Conekta](https://conekta.io), verás que Facturapi es igual de sencillo de entender e integrar a tu aplicación.

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
var facturapi = new FacturapiClient('TU_API_KEY');
// Después, procede a llamar a los métodos como muestra la documentación.
var invoice = await facturapi.Invoice.Create(...);
```

### Métodos asíncronos (async, await)

Esta librería utiliza métodos asíncronos. Si tu aplicación no tiene código asíncrono, puedes convertir un método asíncrono en síncrono de la siguiente manera:

```csharp
// Asíncrono
var customers = await facturapi.Customer.List();

// Síncrono
var customers = facturapi.Customer.List().GetAwaiter().GetResult();
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
var invoice = await facturapi.Product.CreateAsync(new Dictionary<string, object>
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
  }
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
var file = new System.IO.FileStrem("C:\\route\\to\\save\\invoice.zip", FileMode.Create);
zipStream.CopyTo(file);
file.Close();
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
