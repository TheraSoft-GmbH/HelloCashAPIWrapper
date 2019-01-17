using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper
{
    public class Config
    {
        public static bool TestMode = true;
        public static Authentication.HelloCashAuthenticator AuthenticaterToken;
        public static Uri BaseAddress = new Uri("https://api.hellocash.business/api/v1/");
    }
}
