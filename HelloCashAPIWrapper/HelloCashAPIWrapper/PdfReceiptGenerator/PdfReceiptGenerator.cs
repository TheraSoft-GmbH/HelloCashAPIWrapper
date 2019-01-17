using HelloCashAPIWrapper.DataObjects;
using HelloCashAPIWrapper.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.PdfReceiptGenerator
{
    public class PdfReceiptGenerator
    {
        private InvoiceData ResponseData { get; set; }
        private string HtmlTemplate { get; set; }
        private string HtmlDocument { get; set; }
        private FileInfo TargetPath { get; set; }

        /// <summary>
        /// Creates the html template and fills in the given data
        /// </summary>
        /// <param name="responseData">data object from HelloCash</param>
        /// <param name="targetPath">Path where to store the resulting pdf</param>
        /// <param name="pdfCustomization">PDFCustomization object to customize pdf generation</param>
        public PdfReceiptGenerator(InvoiceData responseData, FileInfo targetPath, PdfCustomization pdfCustomization = null)
        {
            this.ResponseData = responseData;
            this.HtmlTemplate = HTMLTemplateManager.GetHtmlTemplate(HtmlTemplateType.Receipt);
            this.TargetPath = targetPath;
            this.FillTemplate(pdfCustomization);
        }

        private void FillTemplate(PdfCustomization pdfCustomization)
        {
            var templateTextReplacer = new TemplateTextReplacer(HtmlTemplate, pdfCustomization);
            HtmlDocument = templateTextReplacer.ReplaceAll(ResponseData);

            if (pdfCustomization != null)
                if (pdfCustomization.CustomValues != null)
                    HtmlDocument = templateTextReplacer.ReplaceCustomValues(pdfCustomization.CustomValues);
        }

        /// <summary>
        /// Saves the receipt as html file
        /// </summary>
        public void SaveAsHtml(bool openAfterSuccess = false)
        {
            File.WriteAllText(TargetPath.FullName, HtmlDocument);
            if (openAfterSuccess)
                System.Diagnostics.Process.Start(TargetPath.FullName);
        }

        /// <summary>
        /// Converts the html template into a pdf file and saves it
        /// </summary>
        public void SaveAsPdf(bool openAfterSuccess = false)
        {
            HtmlToPdf.HtmlToPdfConverter.CreateFromHtml(HtmlDocument, TargetPath.FullName, null);
            if (openAfterSuccess)
                System.Diagnostics.Process.Start(TargetPath.FullName);
        }
    }
}
