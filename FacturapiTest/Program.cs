using Facturapi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturapiTest
{
    class Program
    {
        static FacturapiClient facturapiUser { get; set; }
        static FacturapiClient facturapi { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your user API Key");
            var apiKey = Console.ReadLine();
            facturapiUser = new FacturapiClient(apiKey);
            var organization = facturapiUser.Organization.RetrieveAsync("5d627747cfd03f37574a57d8").GetAwaiter().GetResult();
            Console.WriteLine("Organization created with id " + organization.Id);
            Console.WriteLine("Uploading logo...");

            var fileStream = File.OpenRead(@".\logo.jpg");
            Console.WriteLine("File is of size " + fileStream.Length.ToString());
            organization = facturapiUser.Organization.UploadLogoAsync(organization.Id, fileStream).GetAwaiter().GetResult();
            
            //CreateCustomer();
            //ListCustomers();
            //var productId = CreateProduct();
            //CreateInvoice(productId);
            //Console.WriteLine("Invoice Id?");
            //var invoiceId = Console.ReadLine();
            //DownloadZip(invoiceId);
            //RetrieveInvoice("5940e8a59778e41fc95f294d");
            //var invoice = CreateInvoiceEphimeral();
            // ListInvoices();
            //facturapi.Invoice.SendByEmailAsync(invoice.Id).GetAwaiter().GetResult();
            Console.WriteLine("Done!");
            Console.Read();
        }

        static void ListCustomers ()
        {
            var list = facturapi.Customer.ListAsync().GetAwaiter().GetResult();
            Console.WriteLine($"total: {list.TotalPages}");
            foreach (var c in list.Data)
            {
                Console.WriteLine(c.LegalName);
            }
        }

        static void CreateCustomer ()
        {
            try
            {
                var customer = facturapi.Customer.CreateAsync(new Dictionary<string, object>
                {
                    ["legal_name"] = "María de los Ángeles Buenavista Rodriguez",
                    ["tax_id"] = "ROBJ881103B24",
                    //["email"] = "darkogbk@gmail.com",
                    ["address"] = new Dictionary<string, object>
                    {
                        ["street"] = "Calle Falsa",
                        ["zip"] = "83240"
                    }
                }).GetAwaiter().GetResult();
                Console.WriteLine(customer.LegalName);
                Console.WriteLine(customer.Email);
            }
            catch (FacturapiException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static string CreateProduct ()
        {
            try
            {
                var product = facturapi.Product.CreateAsync(new Dictionary<string, object>
                {
                    ["description"] = "Macbook Pro 15''",
                    ["product_key"] = "43211508",
                    ["price"] = 45000,
                    ["sku"] = "MAC12345"
                }).GetAwaiter().GetResult();
                Console.WriteLine($"Product created with Id {product.Id}");
                return product.Id;
            }
            catch (FacturapiException exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }

        static void CreateInvoice (string productId)
        {
            try
            {
                var invoice = facturapi.Invoice.CreateAsync(new Dictionary<string, object>
                {
                    ["customer"] = "5a7b6739df947d13ad4eea79",
                    ["payment_form"] = Facturapi.PaymentForm.DINERO_ELECTRONICO,
                    ["items"] = new Dictionary<string, object>[]
                    {
                        new Dictionary<string, object>
                        {
                            ["quantity"] = 2,
                            ["product"] = productId
                        }
                    }
                }).GetAwaiter().GetResult();
                Console.WriteLine($"invoice created with Id {invoice.Id}");
            }
            catch (FacturapiException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static void DownloadZip (string invoiceId)
        {
            try
            {
                var stream = facturapi.Invoice.DownloadZipAsync(invoiceId).GetAwaiter().GetResult();
                var file = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\factura.zip", FileMode.Create);
                stream.CopyTo(file);
                file.Close();
                Console.WriteLine("Invoice saved!");
            }
            catch (FacturapiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        static void RetrieveInvoice(string invoiceId)
        {
            try
            {
                var invoice = facturapi.Invoice.RetrieveAsync(invoiceId).GetAwaiter().GetResult();
                Console.WriteLine("Invoice data");
                Console.WriteLine(invoice.Id);
                Console.WriteLine(invoice.Total);
                Console.WriteLine(invoice.PaymentForm);
                Console.WriteLine(invoice.Status);
                Console.WriteLine(invoice.Livemode);
                Console.WriteLine("stop");
            }
            catch (FacturapiException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        static Invoice CreateInvoiceEphimeral()
        {
            try
            {
                var invoice = facturapi.Invoice.CreateAsync(new Dictionary<string, object>
                {
                    ["customer"] = "5a7b6739df947d13ad4eea79",
                    ["payment_form"] = Facturapi.PaymentForm.DINERO_ELECTRONICO,
                    ["items"] = new Dictionary<string, object>[]
                    {
                        new Dictionary<string, object>
                        {
                            ["quantity"] = 2,
                            ["product"] = new Dictionary<string, object>
                            {
                                ["description"] = "iPhone 8",
                                ["product_key"] = "43211508",
                                ["price"] = "30000"
                            }
                        }
                    }
                }).GetAwaiter().GetResult();
                return invoice;
            }
            catch (FacturapiException exception)
            {
                Console.WriteLine(exception.Message);
                return null;
            }
        }

        static void ListInvoices()
        {
            try
            {
                var invoices = facturapi.Invoice.ListAsync(new Dictionary<string, object>
                {
                    ["limit"] = 5,
                    ["page"] = 3
                }
            ).GetAwaiter().GetResult();
                foreach (var invoice in invoices.Data)
                {
                    Console.WriteLine(invoice.Id);
                }
            }
            catch (FacturapiException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
