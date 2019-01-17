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
            new PdfReceiptGenerator(response, new FileInfo(targetPdfPath));

            //Notify user
            Console.WriteLine("An Invoice was added to your HelloCash account");
            
Link to AuthenticationString explanation
//See https://intercom.help/hellocash-faq/faq-deutsch/anleitungen-and-allgemeine-informationen/gibt-es-eine-schnittstelle-zu-hellocash

A more detailled explanation and/or example projects will be added here in the future
