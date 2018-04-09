using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Web;
using OtpSharp;
using QRCoder;
using Wiry.Base32;

namespace Gico.Common
{
    public class OtpUtility
    {
        public const int OtpStep = 30;
        public const int OtpSize = 6;
        public const OtpHashMode OtpHashMode = OtpSharp.OtpHashMode.Sha1;
        public string GenerateRandomKey(OtpHashMode otpHashMode = OtpHashMode)
        {
            byte[] secretKey = KeyGeneration.GenerateRandomKey(otpHashMode);
            return Base32Encoding.Standard.GetString(secretKey);
        }

        public string GenerateOtp(string secretKey, int otpStep = OtpStep, OtpHashMode otpHashMode = OtpHashMode, int otpSize = OtpSize)
        {
            var otp = new Totp(Base32Encoding.Standard.ToBytes(secretKey), otpStep, otpHashMode, otpSize);
            var date = DateTime.UtcNow;
            var code = otp.ComputeTotp(date);
            return code;
        }
        public bool VerifyOtp(string secretKey, string otpCode, int otpStep = OtpStep, OtpHashMode otpHashMode = OtpHashMode, int otpSize = OtpSize)
        {
            var otp = new Totp(Base32Encoding.Standard.ToBytes(secretKey), otpStep, otpHashMode, otpSize);
            var date = DateTime.UtcNow;
            bool valid = otp.VerifyTotp(date, otpCode, out long timeStepMatched, new VerificationWindow(2, 2));
            return valid;
        }

        public string GenerateBarcodeUrl(string secretKey, string emailOrMobile, int otpStep = OtpStep, OtpHashMode otpHashMode = OtpHashMode, int otpSize = OtpSize)
        {
            string barcodeUrl =
                $"{KeyUrl.GetTotpUrl(Base32Encoding.Standard.ToBytes(secretKey), emailOrMobile, otpStep, otpHashMode, otpSize)}&issuer=gico.vn";
            return HttpUtility.UrlEncode(barcodeUrl);
        }

        public byte[] GenerateQRCode(string secretKey, string emailOrMobile)
        {
            string barcodeUrl = GenerateBarcodeUrl(secretKey, emailOrMobile);
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(barcodeUrl, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return ImageToByte(qrCodeImage);
        }
        public static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}