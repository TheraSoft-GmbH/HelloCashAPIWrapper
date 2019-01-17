using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HelloCashAPIWrapper.Authentication;
using HelloCashAPIWrapper.DataObjects;
using System.Web.Script.Serialization;

namespace HelloCashAPIWrapper.Request
{
    public class Request : IRequest
    {
        /// <summary>
        /// Sends a HelloCashAPI request defined by the RequestData parameter
        /// <para>Returns the servers object of type T</para>
        /// <para>Returns default(T) if the Deserialization failed</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Authenticator">Authentication token to be used for API call</param>
        /// <param name="RequestData">Defines type and payload of API request</param>
        /// <exception cref="Exception">Incomplete input values | Deserializing failed | Invoice not found</exception>
        /// <exception cref="System.Security.Authentication.AuthenticationException"></exception>
        /// <returns>API response deserialized into Task <typeparam name="T"></typeparam> object</returns>
        public async Task<T> SendRequestAsync<T>(Authentication.HelloCashAuthenticator Authenticator, IRequestData RequestData)
        {
            //Check input values
            if (RequestData == null)
                throw new Exception($"RequestData must not be null at Invocation of {nameof(SendRequestAsync)}");
            if (Authenticator == null)
                throw new Exception($"Authenticator must not be null at Invocation of {nameof(SendRequestAsync)}");

            string responseData = "";

            //Send Request
            using (var httpClient = new HttpClient { BaseAddress = Config.BaseAddress })
            {
                //Authenticate user
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("authorization", $"Basic {Authenticator.GetAuthenticationString()}");

                if (RequestData.ToJson() == "")
                {
                    //Requests without json content to upload
                    using (var response = await httpClient.GetAsync(RequestData.GetPostRequestLink()))
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                }
                else
                {
                    //Requests with json content to upload
                    using (var content = new StringContent(RequestData.ToJson(), System.Text.Encoding.Default, "application/json"))
                    {
                        using (var response = await httpClient.PostAsync(RequestData.GetPostRequestLink(), content))
                        {
                            responseData = await response.Content.ReadAsStringAsync();
                        }
                    }
                }

            }

            //Wrong authentication
            if (responseData.Equals("{\"error\":\"Invalid Basic authenticaten: Benutzername oder Passwort falsch\"}"))
                throw new System.Security.Authentication.AuthenticationException("UserAuthentication failed");
            //return default(T);
            if (responseData.Equals("[\"Rechnung nicht gefunden\"]"))
                throw new Exception("Invoice not found");

            //Attempt to deserialize API response into T
            try
            {
                var returnObject = new JavaScriptSerializer().Deserialize<T>(responseData);
                return returnObject;
            }
            catch (Exception ex)
            {
                throw new Exception($"HelloCash response could not be deserialized into a {typeof(T)} object", ex);
            }

        }
    }
}
