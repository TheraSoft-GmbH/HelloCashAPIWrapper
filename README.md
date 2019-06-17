# HelloCashAPIWrapper
A C# wrapper for the HelloCash (www.hellocash.at) Rest API (https://hellocashapi.docs.apiary.io/)

This library offers classes and methods around the HelloCash API.
Some main features are:
- Request and Response Objects for API Requests
- Objects for Account Authentication
- PDF Receipt generation from Response-Objects
- QR Code Generation for PDF Receipts
- PDF Generation can be customized with custom Transformers and Dictionaries


# Getting Started
Clone this repository

# Prerequisites
This Library makes use of two nuget packages
- QRCoder (https://github.com/codebude/QRCoder/)
- HTMLRenderer (https://www.nuget.org/packages/HtmlRenderer.PdfSharp/1.5.0.6)

# How to use

To send a normal "CreateInvoice" request with this library do the following:



            //Create an RequestData object and initialize all values
            var requestData = new CreateInvoiceRequestData() {...}; 
            
            //Create an Authenticator object
            //AuthenticationString is "Email:Password" in Base64 encoding
            var authenticator = new HelloCashAuthenticator(AuthenticationString);
            
            //Make HelloCash API call and get response object
            var response = await new Request().SendRequestAsync<CreateInvoiceResponseData>(authenticator, requestData);

            //Generate pdf invoice
            var pdfGenerator = new PdfReceiptGenerator(response, new FileInfo(targetPdfPath));
            pdfGenerator.SaveAsPdf(openAfterSuccess : true); //Saves the pdf and opens it after generation
            pdfGenerator.SaveAsHtml(openAfterSuccess: true); //Saves the plain html and opens it with the default viewer

            //Notify user
            Console.WriteLine("An Invoice was added to your HelloCash account");
            
Link to AuthenticationString explanation
//See https://intercom.help/hellocash-faq/faq-deutsch/anleitungen-and-allgemeine-informationen/gibt-es-eine-schnittstelle-zu-hellocash

## PDF Output
The pdf output looks something like this:
![ExamplePDF](https://github.com/luchspeter/HelloCashAPIWrapper/blob/master/README_Resources/ExamplePdf.PNG)

## Examples
The example project contains some examples for creating an invoice with this API wrapper.
- Example 1: demonstrates the "plain" usage of the wrapper.
- Example 2: shows how to use the PDFCustomizer to customize the pdf output. Transformers and a custom Dictionary is used. 
- Example 3: uses a async helper to execute the code synchronsously
- Example 4: shows how to catch and process all commonly thrown exceptions

To run the examples open the ConsoleApplication, build and launch the application. After the example selection a valid HelloCash Email and Password authentication is needed. All examples create an invoice in TestMode and open the pdf output.
