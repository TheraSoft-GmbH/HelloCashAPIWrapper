using HelloCashAPIWrapper.Authentication;
using HelloCashAPIWrapper.DataObjects;
using HelloCashAPIWrapper.PdfReceiptGenerator;
using HelloCashAPIWrapper.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapperExample
{
    class Example4_ExceptionHandling
    {
        public void PerformExampleInvoiceCreationSync(string email, string password)
        {
            //Create an RequestData object and initialize all values
            var requestData = Shared.CreateRequestDataExampleObject();

            //Create an Authenticator object
            //AuthenticationString is "Email:Password" in Base64 encoding
            var authenticator = new HelloCashAuthenticator($"{email}:{password}", false);

            var response = SendRequestSafely<CreateInvoiceResponseData>(authenticator, requestData);

            if (response == default(CreateInvoiceResponseData)) { return; }

            //Generate pdf invoice
            var pdfGenerator = new PdfReceiptGenerator(response, Shared.GetTargetFilePath());
            pdfGenerator.SaveAsPdf(openAfterSuccess: true); //Saves the pdf and opens it after generation

            //Notify user
            Console.WriteLine("An Invoice was added to your HelloCash account");
        }

        public T SendRequestSafely<T>(HelloCashAuthenticator Authenticator, IRequestData RequestData)
        {
            //Make HelloCash API call and get response object
            try
            {
                var response = new Request().SendRequest<T>(Authenticator, RequestData);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException)
                {
                    Console.WriteLine("Could not connec to the HelloCash API endpoint.");
                }
                if (ex is AuthenticationException)
                {
                    Console.WriteLine("Wrong user name or password");
                }
                if (ex is ArgumentException)
                {
                    Console.WriteLine("An unknown API error occured!");
                }
                if (ex is HelloCashAPIWrapper.Exceptions.InDemoNotAvailableException)
                {
                    Console.WriteLine("The HelloCash account has to at least be in the free mode");
                }

                return default(T);
            }
        }



    }
}
