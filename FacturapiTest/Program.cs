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
    }
}
