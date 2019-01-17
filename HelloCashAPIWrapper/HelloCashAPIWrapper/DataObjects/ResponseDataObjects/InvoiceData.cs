using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.DataObjects
{
    public class InvoiceData
    {
        public string invoice_id { get; set; }
        public string invoice_timestamp { get; set; }
        public string invoice_number { get; set; }
        public string invoice_cashier { get; set; }
        public string invoice_cashier_id { get; set; }
        public string invoice_mode { get; set; }
        public string invoice_payment { get; set; }
        public string invoice_total { get; set; }
        public string invoice_totalNet { get; set; }
        public string invoice_totalTax { get; set; }
        public string invoice_text { get; set; }
        public string invoice_currency { get; set; }
        public string invoice_cancellation { get; set; }
        public Company company { get; set; }
        public List<InvoiceResponseItem> items { get; set; }
        public List<Tax> taxes { get; set; }
        public Signature signature { get; set; }
    }

    public class Company
    {
        public string name { get; set; }
        public string street { get; set; }
        public string houseNumber { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string phoneNumber { get; set; }
        public string companyRegister { get; set; }
        public string iban { get; set; }
        public string bic { get; set; }
    }

    public class InvoiceResponseItem
    {
        public string item_id { get; set; }
        public string item_quantity { get; set; }
        public string item_name { get; set; }
        public string item_price { get; set; }
        public string item_total { get; set; }
        public string item_taxRate { get; set; }
        public string item_discount { get; set; }
        public string item_service_id { get; set; }
        public string item_article_id { get; set; }
    }

    public class Tax
    {
        public string tax_taxRate { get; set; }
        public double tax_gross { get; set; }
        public double tax_net { get; set; }
        public double tax_tax { get; set; }
    }

    public class Signature
    {
        public string signature_code { get; set; }
        public string signature_text { get; set; }
    }


}
