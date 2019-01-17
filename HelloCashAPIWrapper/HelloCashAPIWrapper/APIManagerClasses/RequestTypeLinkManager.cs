using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.APIManagerClasses
{
    public static class RequestTypeLinkManager
    {
        private static Dictionary<RequestType, string> requestTypeLinkBook;
        private static Dictionary<RequestType, string> RequestTypeLinkBook
        {
            get
            {
                if (requestTypeLinkBook == null)
                {
                    requestTypeLinkBook = new Dictionary<RequestType, string>()
                        {
                            { RequestType.CreateInvoice, "Invoices" },
                            { RequestType.CancelInvoice, "invoices/{InvoiceId}/cancellation" },
                            { RequestType.GetInvoice, "invoices/{InvoiceId}" },
                            { RequestType.VerifyUser, "Invoices"},
                        };
                }
                return requestTypeLinkBook;
            }
        }


        public static string GetRequestLink(RequestType requestType, string InvoiceId = null)
        {
            if ((requestType == RequestType.CancelInvoice || requestType == RequestType.GetInvoice) && InvoiceId == null)
                throw new ArgumentNullException("InvoiceId must be set to get a valid link");

            var link = RequestTypeLinkBook[requestType];

            if (link.Contains("{InvoiceId}"))
                link = link.Replace("{InvoiceId}", InvoiceId);
            return link;
        }
    }
}
