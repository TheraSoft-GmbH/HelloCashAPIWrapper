using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace HelloCashAPIWrapper.PdfReceiptGenerator.HtmlToPdf
{
    public static class HtmlToPdfConverter
    {
        /// <summary>
        /// Converts the input html to a pdf file and saves it to "path"
        /// </summary>
        /// <param name="html">Html to be converted</param>
        /// <param name="path">Path to store the pdf document</param>
        /// <param name="orientation">Orientation of pdf (landscape or something else)</param>
        public static void CreateFromHtml(string html, string path, string orientation)
        {
            PdfDocument pdf = PdfGenerator.GeneratePdf(html, PageSize.A4);

            if (orientation == "landscape")
            {
                foreach (PdfPage page in pdf.Pages)
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
            }

            pdf.Save(path);
            return;
        }
    }
}
