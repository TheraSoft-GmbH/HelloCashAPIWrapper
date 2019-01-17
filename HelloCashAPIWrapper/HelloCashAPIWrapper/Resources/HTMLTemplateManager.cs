using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.Resources
{
    public class HTMLTemplateManager
    {
        public static string GetHtmlTemplate(HtmlTemplateType type)
        {
            switch (type)
            {
                case HtmlTemplateType.Receipt:
                    return HelloCashAPIWrapper.Properties.Resources.HelloCashReceiptTemplate;

                default:
                    throw new NotImplementedException($"{type} of Type {typeof(HtmlTemplateType)} is not implemented in the GetHtmlTemplate Method");
            }
        }
    }
}
