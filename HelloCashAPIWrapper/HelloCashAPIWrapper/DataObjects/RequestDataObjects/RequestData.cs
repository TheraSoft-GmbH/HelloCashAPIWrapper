using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

using HelloCashAPIWrapper.APIManagerClasses;

namespace HelloCashAPIWrapper.DataObjects
{
    public class RequestData : IRequestData
    {
        public RequestType RequestType { get; set; }


        public virtual string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public virtual string GetPostRequestLink()
        {
            return RequestTypeLinkManager.GetRequestLink(RequestType);
        }
    }
}
