using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.PdfReceiptGenerator
{
    public class PdfCustomization
    {
        /// <summary>
        /// Key/Value pair.
        /// <para>First Value: KeyWord in html template</para>
        /// <para>Second Value: DataWord to replace KeyWord with</para>
        /// </summary>
        public Dictionary<string, string> CustomValues { get; set; }
        public Dictionary<Func<string, bool>, Func<object, string>> CustomTransformers { get; set; }

        /// <summary>
        /// Returns an empty CustomTransformer Dictionary
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Func<string, bool>, Func<object, string>> GetNewCustomTransformersDictionary() => new Dictionary<Func<string, bool>, Func<object, string>>();
    }
}
