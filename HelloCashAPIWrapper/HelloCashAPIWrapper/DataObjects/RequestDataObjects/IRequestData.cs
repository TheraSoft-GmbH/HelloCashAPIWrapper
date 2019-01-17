using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.DataObjects
{
    public interface IRequestData
    {
        RequestType RequestType { get; set; }
        string ToJson();
        string GetPostRequestLink();
    }
}
