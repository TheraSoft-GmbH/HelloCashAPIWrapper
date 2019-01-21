using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapperExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int exampleToRun = PrintSelectionMenu();

            
            //Get user's email and password
            Console.Write("\n\nPlease input your email adress: ");
            string email = Console.ReadLine();
            Console.Write("Please input your password adress: ");
            string password = Console.ReadLine();

            //Perform example

            switch (exampleToRun)
            {
                case 1:
                    new Example1_CreateSimpleInvoice().PerformExampleInvoiceCreationAsync(email, password).Wait();
                    break;
                case 2:
                    new Example2_CreateInvoiceWithPDFCustomizer().PerformExampleInvoiceCreationAsync(email, password).Wait();
                    break;
                default:
                    break;
            }

            Console.ReadKey();
        }

        static int PrintSelectionMenu()
        {
            Console.WriteLine("Please choose one example from below:");
            Console.WriteLine("[1] Example1: Creates a simple Invoice and opens it via pdf");
            Console.WriteLine("[2] Example1: Creates a simple Invoice, adds custom transformers and dictionaries and opens it via pdf");
            Console.WriteLine("What example do you want to execute? (Type \"1\" or \"2\" without quotationmarks):");


            string input = "";
            int value = -1;
            do
            {
                input = Console.ReadLine();
                int.TryParse(input, out value);
            } while (value == -1);

            return value;
        }

    }
}
