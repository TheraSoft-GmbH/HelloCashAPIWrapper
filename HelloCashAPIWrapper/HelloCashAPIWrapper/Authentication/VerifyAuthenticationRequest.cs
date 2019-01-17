using HelloCashAPIWrapper.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.Authentication
{
    public class VerifyAuthenticationRequest
    {
        /// <summary>
        /// Verifies the given string via a GetInvoiceRequest
        /// </summary>
        /// <param name="base64EncodedHelloCashAuthentication"></param>
        /// <returns></returns>
        public async Task<bool> Verify(string base64EncodedHelloCashAuthentication)
        {
            var RequestData = new GetInvoicesRequestData();
            var Authenticate = new HelloCashAuthenticator(base64EncodedHelloCashAuthentication);
            try
            {
                var resonse = await new Request.Request().SendRequestAsync<GetInvoicesResponseData>(Authenticate, RequestData);
                return true;
            }
            catch (System.Net.Http.HttpRequestException)
            {
                return false;
            }
            catch (System.Security.Authentication.AuthenticationException)
            {
                return false;
            }
        }
    }
}
