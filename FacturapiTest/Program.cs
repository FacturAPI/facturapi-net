using Facturapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturapiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your test API Key");
            var apiKey = Console.ReadLine();
            Facturapi.Settings.ApiKey = apiKey;
            //CreateCustomer();
            ListCustomers();
            //CreateProduct();
            Console.WriteLine("Done fetching!");
            Console.Read();
        }

        static void ListCustomers ()
        {
            var list = Facturapi.Customer.ListAsync().GetAwaiter().GetResult();
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
                var customer = Facturapi.Customer.CreateAsync(new Dictionary<string, object>
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

        static void CreateProduct ()
        {
            try
            {
                var product = Facturapi.Product.CreateAsync(new Dictionary<string, object>
                {
                    ["description"] = "Macbook Pro 15''",
                    ["product_key"] = "43211508",
                    ["price"] = 45000,
                    ["sku"] = "MAC12345"
                }).GetAwaiter().GetResult();
                Console.WriteLine($"Product created with Id {product.Id}");
                CreateInvoice(product.Id);
            }
            catch (FacturapiException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        static void CreateInvoice (string productId)
        {
            try
            {
                var invoice = Facturapi.Invoice.CreateAsync(new Dictionary<string, object>
                {
                    ["customer"] = "592deb5ce815c1296c950d02",
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
    }
}
