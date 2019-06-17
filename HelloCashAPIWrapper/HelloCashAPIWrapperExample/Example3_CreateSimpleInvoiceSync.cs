using HelloCashAPIWrapper.Authentication;
using HelloCashAPIWrapper.DataObjects;
using HelloCashAPIWrapper.PdfReceiptGenerator;
using HelloCashAPIWrapper.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapperExample
{
    public class Example3_CreateSimpleInvoiceSync
    {
        public void PerformExampleInvoiceCreationSync(string email, string password)
        {
            //Create an RequestData object and initialize all values
            var requestData = Shared.CreateRequestDataExampleObject();

            //Create an Authenticator object
            //AuthenticationString is "Email:Password" in Base64 encoding
            var authenticator = new HelloCashAuthenticator($"{email}:{password}", false);

            //Make HelloCash API call and get response object
            var response = new Request().SendRequest<CreateInvoiceResponseData>(authenticator, requestData);

            //Generate pdf invoice
            var pdfGenerator = new PdfReceiptGenerator(response, Shared.GetTargetFilePath());
            pdfGenerator.SaveAsPdf(openAfterSuccess: true); //Saves the pdf and opens it after generation

            //Notify user
            Console.WriteLine("An Invoice was added to your HelloCash account");
        }
    }
}
