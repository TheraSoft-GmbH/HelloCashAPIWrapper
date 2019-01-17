using HelloCashAPIWrapper.APIManagerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.DataObjects
{
    public class GetInvoicesRequestData : RequestData
    {
        public GetInvoicesRequestData()
        {
            RequestType = RequestType.VerifyUser;
        }


        public override string ToJson()
        {
            return "";
        }

        public override string GetPostRequestLink()
        {
            return RequestTypeLinkManager.GetRequestLink(RequestType);
        }
    }
}
