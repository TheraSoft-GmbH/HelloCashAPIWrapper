using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.Authentication
{
    public class HelloCashAuthenticator
    {
        public HelloCashAuthenticator(string authenticationString, bool isBase64Encoded = true)
        {
            if (isBase64Encoded == false)
            {
                this.AuthenticationString = EncodeToBase64(authenticationString);
            }
            else
            {
                this.AuthenticationString = authenticationString;
            }
        }


        private string AuthenticationString { get; set; }

        public string GetAuthenticationString()
        {
            return AuthenticationString;
        }

        public string EncodeToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
    }
}
