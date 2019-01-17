using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.DataObjects
{
    public class GetInvoicesResponseData
    {
        public List<Invoice> invoices { get; set; }
        public string count { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
    }
    public class Invoice
    {
        public string invoice_id { get; set; }
        public string invoice_timestamp { get; set; }
        public string invoice_number { get; set; }
        public string invoice_cashier { get; set; }
        public string invoice_mode { get; set; }
        public string invoice_payment { get; set; }
        public string invoice_total { get; set; }
        public string invoice_cancellation { get; set; }
    }
}
