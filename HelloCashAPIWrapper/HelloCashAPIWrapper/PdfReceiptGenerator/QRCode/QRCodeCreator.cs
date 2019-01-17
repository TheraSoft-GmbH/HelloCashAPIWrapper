using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCashAPIWrapper.PdfReceiptGenerator.QRCode
{
    public class QRCodeCreator
    {
        /// <summary>
        /// Converts the input string into a Base64 encoded qr code
        /// </summary>
        /// <param name="data">String to be encoded</param>
        /// <returns></returns>
        public string GetQRCodeBase64Encrypted(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            var qrCodeImageAsBase64 = qrCode.GetGraphic(20);
            return qrCodeImageAsBase64;
        }
    }
}
