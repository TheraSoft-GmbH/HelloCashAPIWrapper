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
    class Example2_CreateInvoiceWithPDFCustomizer
    {
        public async Task PerformExampleInvoiceCreationAsync(string email, string password)
        {
            //Create an RequestData object and initialize all values
            var requestData = Shared.CreateRequestDataExampleObject();

            //Create an Authenticator object
            //AuthenticationString is "Email:Password" in Base64 encoding
            var authenticator = new HelloCashAuthenticator($"{email}:{password}", false);

            //Make HelloCash API call and get response object
            var response = await new Request().SendRequestAsync<CreateInvoiceResponseData>(authenticator, requestData);

            //Create PDF customizer
            var pdfCustomizer = GetPdfCustomization();

            //Generate pdf invoice
            var pdfGenerator = new PdfReceiptGenerator(response, Shared.GetTargetFilePath(), pdfCustomizer);
            pdfGenerator.SaveAsPdf(openAfterSuccess: true); //Saves the pdf and opens it after generation

            //Notify user
            Console.WriteLine("An Invoice was added to your HelloCash account");
        }

        /// <summary>
        /// Creates a pdf customizers
        /// <para>It converts doubles to int, if the codword for it contains "quantity"</para>
        /// <para>It converts DateTimes to a more readable format</para>
        /// </summary>
        /// <returns></returns>
        private PdfCustomization GetPdfCustomization()
        {
            //Create generator
            var customTransformers = PdfCustomization.GetNewCustomTransformersDictionary();
            customTransformers[IsQuantity] = ConvertToInt;
            customTransformers[IsTimeStamp] = ConvertToEuropeanDate;

            var pdfCustomizer = new PdfCustomization()
            {
                CustomValues = new Dictionary<string, string>() { { "{invoice_reference}", "MyInvoiceRef" } },
                CustomTransformers = customTransformers
            };

            return pdfCustomizer;
        }

        #region ConvertQuantityToInt
        private bool IsQuantity(string propName)
        {
            return propName.Contains("_quantity");
        }
        private string ConvertToInt(object value)
        {
            if (value is string)
            {
                double doubleNumber;
                try
                {
                    doubleNumber = double.Parse((string)value, System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return "";
                }
                return ((int)doubleNumber).ToString();
            }
            return value.ToString();
        }

        private bool IsTimeStamp(string propName)
        {
            return propName.Contains("invoice_timestamp");
        }
        private string ConvertToEuropeanDate(object value)
        {
            if (value is string)
            {
                if (DateTime.TryParse((string)value, out DateTime dt))
                {
                    return $"{dt.ToLongDateString()} {dt.ToShortTimeString()}";
                }
            }
            return value.ToString();
        }
        #endregion


    }
}
