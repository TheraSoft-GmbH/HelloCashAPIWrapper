using HelloCashAPIWrapper.APIManagerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.DataObjects
{
    class GetInvoiceRequestData : RequestData
    {
        public GetInvoiceRequestData(string invoice_id)
        {
            RequestType = RequestType.GetInvoice;
            this.invoice_id = invoice_id;
        }

        public string invoice_id { get; set; }

        public override string ToJson()
        {
            return "";
        }

        public override string GetPostRequestLink()
        {
            return RequestTypeLinkManager.GetRequestLink(this.RequestType, invoice_id);
        }
    }
}