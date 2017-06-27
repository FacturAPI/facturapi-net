Facturapi .NET Library
======================

[![NuGet version](https://badge.fury.io/nu/Facturapi.svg)](https://www.nuget.org/packages/Facturapi/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Facturapi.svg)](https://www.nuget.org/packages/Facturapi/)

Librería oficial para .NET de https://www.facturapi.io

Facturapi ayuda a generar facturas electrónicas válidas en México (CFDI) de la manera más fácil posible.

Si alguna vez has usado [Stripe](https://stripe.com) o [Conekta](https://conekta.io), verás que Facturapi es igual de sencillo de entender e implementar en tu servidor.

## Instalación

Puedes instalar Facturapi en tu proyecto usando [Nuget](https://www.nuget.org/)

```bash
PM> Install-Package Facturapi
```

## Antes de comenzar

Asegúrate de haber creado tu cuenta gratuita en [Facturapi](https://www.facturapi.io) y de tener a la mano tus **API Keys**.

### Inicializa la librería usando tus llaves secretas

#### Para una sola organización

Coloca este código en tu bloque de inicialización (para ASP.NET MVC, sería en el método `Application_Start`, en el archivo `Global.asax`):

```csharp
Facturapi.Settings.ApiKey = "TU_API_KEY";
```

#### Para multi-organizaciones

Puedes usar múltiples Api Keys creando instancias del wrapper correspondiente antes de usarlo y pasando como parámetro la ApiKey de la organización que va a facturar:

```csharp
var customerWrapperForOrgOne = new Facturapi.Wrappers.CustomerWrapper('API_KEY_ORG_1');
var customerWrapperForOrgTwo = new Facturapi.Wrappers.CustomerWrapper('API_KEY_ORG_2');
// Esto asegura que puedas usar diferentes ApiKeys para obtener y crear datos para diferentes organizaciones
var customersInOrgOne = await customerWrapperForOrgOne.List();
var customersInOrgTwo = await customerWrapperForOrgTwo.List();
```

Si usas este método de identificación, no necesitas asignar un valor a `Facturapi.Settings.ApiKey`.

### Métodos asíncronos (async, await)

Esta librería utiliza métodos asíncronos. Si tu aplicación no tiene código asíncrono, puedes convertir un método asíncrono en síncrono de la siguiente manera:

```csharp
// Asíncrono
var customers = await Facturapi.Customer.List();

// Síncrono
var customers = Facturapi.Customer.List().GetAwaiter().GetResult();
```

## Uso de la librería

### Crear un cliente

```csharp
var customer = await Facturapi.Customer.CreateAsync(new Dictionary<string, object>
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
var product = await Facturapi.Product.CreateAsync(new Dictionary<string, object>
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
var invoice = await Facturapi.Product.CreateAsync(new Dictionary<string, object>
{
  ["customer"] = "ID_DEL_CLIENTE",
  ["items"] = new Dictionary<string, object>[]
  { // Puedes agregar tantos items como necesites a este arreglo
    new Dictionary<string, object>
    {
      ["quantity"] = 2,               // Opcional. Default: 1.
      ["product"] = "ID_DEL_PRODUCTO" // Para productos no registrados, puedes asignar
                                      // un Dictionary con los datos del producto
    }
  }
  ["payment_form"] = Facturapi.PaymentForm.DINERO_ELECTRONICO
});
```

#### Descargar factura

```csharp
// Una vez creada la factura, puedes descargar el PDF y el XML comprimidos
// en un archivo ZIP.
var zipStream = await Facturapi.Invoice.DownloadZipAsync("58e93bd8e86eb318b019743d");
// O bien, el XML y el PDF por separado
var xmlStream = await Facturapi.Invoice.DownloadXmlAsync("58e93bd8e86eb318b019743d");
var pdfStream = await Facturapi.Invoice.DownloadPdfAsync("58e93bd8e86eb318b019743d");
// Y luego guardarlo en un archivo del disco duro
var file = new System.IO.FileStrem("C:\\route\\to\\save\\invoice.zip", FileMode.Create);
zipStream.CopyTo(file);
file.Close();
```

#### Envía la factura por correo electrónico

```csharp
await Facturapi.Invoice.SendByEmailAsync("ID_DEL_CLIENTE");
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