//using QRCoder;
//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using GeoPet.Models;
//using System.IO;


//namespace GeoPet.Services
//{
//    public class QrCodeGenerator
//    {

//        public Bitmap GenerateImage(string localization)
//        {
//            var qrGenerator = new QRCodeGenerator();
//            var qrCodeData = qrGenerator.CreateQrCode(localization, QRCodeGenerator.ECCLevel.Q);
//            var qrCode = new QRCode(qrCodeData);
//            var qrCodeImage = qrCode.GetGraphic(10);
//            return qrCodeImage;
//        }

//        public byte[] GenerateByteArray(string localization)
//        {
//            var image = GenerateImage(localization);
//            return ImageToByte(image);
//        }

//        private byte[] ImageToByte(Image img)
//        {
//            MemoryStream stream = new MemoryStream();
//            img.Save(stream, ImageFormat.Png);
//            return stream.ToArray();

//        }
//    }
//}

