using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.Request
{
    interface IRequest
    {
        Task<T> SendRequestAsync<T>(Authentication.HelloCashAuthenticator Authenticator, DataObjects.IRequestData RequestData);
    }
}
