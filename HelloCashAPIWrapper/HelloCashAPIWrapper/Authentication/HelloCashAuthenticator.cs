using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.Authentication
{
    public class HelloCashAuthenticator
    {
        public HelloCashAuthenticator(string authenticationString)
        {
            this.AuthenticationString = authenticationString;
        }

        private string AuthenticationString { get; set; }

        public string GetAuthenticationString()
        {
            return AuthenticationString;
        }
    }
}
