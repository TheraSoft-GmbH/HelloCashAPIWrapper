using HelloCashAPIWrapper.APIManagerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HelloCashAPIWrapper.DataObjects
{
    class CancelInvoiceRequestData : RequestData
    {
        public CancelInvoiceRequestData(string invoice_id)
        {
            RequestType = RequestType.CancelInvoice;
            this.invoice_id = invoice_id;
        }

        [ScriptIgnore]
        public string invoice_id { get; set; }

        public int cancellation_cashier_id { get; set; } = 1;
        public string cancellation_reason { get; set; }
        public int cancellation_cashBook_enty { get; set; } = 1;

        public override string ToJson()
        {
            return base.ToJson();
        }

        public override string GetPostRequestLink()
        {
            return RequestTypeLinkManager.GetRequestLink(this.RequestType, invoice_id);
        }
    }
}
