using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.DataObjects
{
    public class CreateInvoiceRequestData : RequestData
    {
        public CreateInvoiceRequestData(bool TestMode)
        {
            RequestType = RequestType.CreateInvoice;
            invoice_testMode = TestMode;
        }

        public int cashier_id { get; set; }
        public bool invoice_testMode { get; private set; }
        public string invoice_text { get; set; }
        public int invoice_reference { get; set; }
        public string invoice_paymentMethod { get; set; }
        public int invoice_discount_percent { get; set; }
        public List<InvoiceRequestItem> items { get; set; }

    }
    public class InvoiceRequestItem
    {
        public string item_name { get; set; }
        public int item_quantity { get; set; }
        public double item_price { get; set; }
        public double item_taxRate { get; set; }
        public string item_discount_unit { get; set; }
        public double item_discount_value { get; set; }
    }
}
