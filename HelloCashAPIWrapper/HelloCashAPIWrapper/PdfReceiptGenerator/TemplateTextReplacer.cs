using HelloCashAPIWrapper.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.PdfReceiptGenerator
{
    class TemplateTextReplacer
    {
        private string HtmlTemplate { get; set; }
        private PdfCustomization PdfCustomization { get; set; }
        public TemplateTextReplacer(string htmlTemplate, PdfCustomization pdfCustomization = null)
        {
            HtmlTemplate = htmlTemplate;
            PdfCustomization = pdfCustomization;
        }

        public string ReplaceAll(InvoiceData responseData)
        {
            HtmlTemplate = InsertQRCodeAsBase64(HtmlTemplate, responseData);
            HtmlTemplate = ReplaceLoops(HtmlTemplate, responseData.items);
            HtmlTemplate = ReplaceKeywords(HtmlTemplate, responseData);

            return HtmlTemplate;
        }


        /// <summary>
        /// Replaces the templates Item part with the actual data, accordingly to the sub-template in between the Keywords {BelegElemente{ }BelegElemente}
        /// </summary>
        /// <param name="text"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        private string ReplaceLoops(string text, IEnumerable<object> items)
        {
            var output = text;


            Regex ItemRegex = new Regex("\\{BelegElemente\\{([\\s\\S]+?)\\}BelegElemente\\}", RegexOptions.Compiled);

            //In the default template there is one Match only
            foreach (Match ItemMatch in ItemRegex.Matches(output))
            {
                var subOutput = "";
                var subTemplate = ItemMatch.Value.Replace("{BelegElemente{", "").Replace("}BelegElemente}", "");
                //Insert each item on its own
                foreach (var item in items)
                {
                    subOutput += ReplaceKeywords(subTemplate, item, SkipInvoiceResponseItem: false);
                }

                //replace keyword with html-items
                output = output.Replace(ItemMatch.Value, subOutput);
            }
            return output;
        }

        internal string ReplaceCustomValues(Dictionary<string, string> customValues)
        {
            return InsertDictionaryIntoTemplate(HtmlTemplate, customValues);
        }

        /// <summary>
        /// Iterates over all properties from objecTtoInsert and replaces all same-named {Name} Keywords in the template
        /// </summary>
        /// <param name="HtmlTemplate">Html template that should be filled</param>
        /// <param name="objectToInsert">Object that will be iterated over</param>
        /// <param name="SkipInvoiceResponseItem">If true, InvoiceResponseItems will not be iterated over since they are handled seperatley</param>
        /// <returns>Fille html template</returns>
        private string ReplaceKeywords(string HtmlTemplate, object objectToInsert, bool SkipInvoiceResponseItem = true)
        {
            var htmlTemplate = HtmlTemplate;
            var propertiesAndValues = GetPropertyDictionary(objectToInsert, SkipInvoiceResponseItem);

            htmlTemplate = InsertDictionaryIntoTemplate(htmlTemplate, propertiesAndValues);

            return htmlTemplate;
        }

        /// <summary>
        /// Gets a dictionary of all propertynames and their value
        /// </summary>
        /// <param name="ObjectToRecurse"></param>
        /// <param name="SkipInvoiceResponseItem"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetPropertyDictionary(Object ObjectToRecurse, bool SkipInvoiceResponseItem = true)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (ObjectToRecurse == null) return dic;
            if (ObjectToRecurse.GetType().IsGenericType) return dic;
            if (SkipInvoiceResponseItem == true && ObjectToRecurse.GetType() == typeof(InvoiceResponseItem)) return dic;

            var props = ObjectToRecurse.GetType().GetProperties();
            foreach (PropertyInfo pi in props)
            {
                if (pi.PropertyType.IsPrimitive == true || (pi.PropertyType == typeof(string)))
                {
                    dic[$"{{{pi.Name}}}"] = $"{pi.GetValue(ObjectToRecurse, null)}";
                }
                else
                {
                    var subDictionary = GetPropertyDictionary(pi.GetValue(ObjectToRecurse, null));
                    AddDictionaryRange(ref dic, subDictionary);
                }
            }
            return dic;
        }

        /// <summary>
        /// Merge two dictionaries
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="subDic"></param>
        public void AddDictionaryRange(ref Dictionary<string, string> dic, Dictionary<string, string> subDic)
        {
            foreach (var key in subDic.Keys)
            {
                if (dic.ContainsKey(key) == false)
                    dic[key] = subDic[key];
                else throw new Exception("Duplicate propertynames in the underlying object");
            }
        }

        /// <summary>
        /// Generates QRCode and inserts it Base64 encoded
        /// </summary>
        /// <param name="htmlTemplate"></param>
        /// <param name="signiture_code"></param>
        /// <returns></returns>
        public string InsertQRCodeAsBase64(string htmlTemplate, InvoiceData response)
        {
            var output = htmlTemplate;

            if (response.signature == null)
                response.signature = new Signature() { signature_code = "NoSignitureCode", signature_text = "Kein Signaturtext" };

            var signiture_code = response.signature.signature_code;
            string base64QRCode = new QRCode.QRCodeCreator().GetQRCodeBase64Encrypted(signiture_code);
            output = output.Replace("{Base64EncodedQRCode}", $"data:image/png;base64,{base64QRCode}");

            return output;
        }

        /// <summary>
        /// Looks for all keys and replaces the key with the value in the htmlTemplate if it is found
        /// </summary>
        /// <param name="HtmlTemplate"></param>
        /// <param name="KeyValuePairs"></param>
        /// <returns></returns>
        public string InsertDictionaryIntoTemplate(string HtmlTemplate, Dictionary<string, string> KeyValuePairs)
        {
            var htmlTemplate = HtmlTemplate;

            Regex ItemRegex = new Regex("\\{([a-zA-Z_]+)([^\\}]?)\\}", RegexOptions.Compiled);

            //In the default template there is one Match only
            foreach (Match ItemMatch in ItemRegex.Matches(htmlTemplate))
            {
                if (KeyValuePairs.ContainsKey(ItemMatch.Value))
                {
                    var valueToSearch = ItemMatch.Value;
                    var valueToSet = KeyValuePairs[ItemMatch.Value];

                    //Check if PDFCustomization is used
                    if (PdfCustomization != null && PdfCustomization.CustomTransformers != null)
                    {
                        //Loop through all Transformer
                        foreach (var evaluater in PdfCustomization.CustomTransformers.Keys)
                        {
                            //If TransformEvaluator returns true, Invoke TransformMethod with value
                            if (evaluater(valueToSearch))
                                valueToSet = PdfCustomization.CustomTransformers[evaluater](valueToSet);
                        }
                    }

                    htmlTemplate = htmlTemplate.Replace(valueToSearch, valueToSet);
                }
            }

            return htmlTemplate;
        }
    }
}
