using HelloCashAPIWrapper.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapperExample
{
    public static class Shared
    {
        /// <summary>
        /// Creates example CreateInvoiceRequestData objects
        /// Replace these methods with your business logic
        /// </summary>
        /// <returns></returns>
        public static CreateInvoiceRequestData CreateRequestDataExampleObject()
        {
            List<InvoiceRequestItem> RequestItems = CreateExampleInvoiceItems();


            //Create RequestData object
            var RequestData = new CreateInvoiceRequestData(TestMode: true)
            {
                cashier_id = 1,
                invoice_text = "Bar Bezahlung",
                invoice_reference = 1,
                invoice_paymentMethod = "Bar",
                invoice_discount_percent = 0,
                items = RequestItems
            };

            return RequestData;
        }

        public static List<InvoiceRequestItem> CreateExampleInvoiceItems()
        {
            List<InvoiceRequestItem> items = new List<InvoiceRequestItem>();

            var random = new Random();

            for (int i = 0; i < 5; i++)
            {
                var requestItem = new InvoiceRequestItem()
                {
                    item_name = $"Some service number {i}",
                    item_quantity = random.Next(1, 3),
                    item_price = random.NextDouble() * 100,
                    item_taxRate = 15,
                    item_discount_unit = null,
                    item_discount_value = 0
                };
                items.Add(requestItem);
            }

            return items;
        }

        /// <summary>
        /// Returns a file path to store the pdf in the bin directory of this project
        /// </summary>
        /// <returns></returns>
        public static FileInfo GetTargetFilePath()
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);
            path = Path.Combine(path, "ExampleInfoice.pdf");
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch { throw; }

            return new FileInfo(path);
        }
    }
}
