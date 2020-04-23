using System;
using System.Text;
using GSTAPI.Models;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace GSTAPI.Helper
{
    public class Cryptography
    {
        private static X509Certificate2 GSTNPublickey()
        {
            var certificatePath = AppDomain.CurrentDomain.BaseDirectory;
            certificatePath = Path.GetFullPath(Path.Combine(certificatePath, @"..\..\..\"));
            certificatePath = certificatePath + @"Certificate\GSTN_G2B_SANDBOX_UAT_public.cer";
            return new X509Certificate2(certificatePath);
        }
        internal static string DecryptData(string dataToDecrypt, string Key)
        {
            var tdes = new AesManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Key = Encoding.UTF8.GetBytes(Key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            string a = dataToDecrypt.Replace(" ", "+");
            int mod4 = a.Length % 4;
            if (mod4 > 0)
            {
                a += new string('=', 4 - mod4);
            }
            
            var dataToDecryptBytes = Convert.FromBase64String(a);
            var decrypt = tdes.CreateDecryptor();
            var deCipher = decrypt.TransformFinalBlock(dataToDecryptBytes, 0, dataToDecryptBytes.Length);
            tdes.Clear();
            var plainText = Encoding.UTF8.GetString(deCipher);

            return plainText;
        }
        internal static string EncryptData(string plainText, string Key)
        {
            var dataToEncrypt = Encoding.UTF8.GetBytes(plainText);
            var tdes = new AesManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Key = Encoding.UTF8.GetBytes(Key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };


            var crypt = tdes.CreateEncryptor();
            var cipher = crypt.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            tdes.Clear();
            return Convert.ToBase64String(cipher, 0, cipher.Length);
        }
        internal static string EncryptTextWithSessionKey(string plainText, CipherKeys keys)
        {
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(base64String);

            AesManaged tdes = new AesManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Key = GetSessionKeyBytes(keys),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };


            ICryptoTransform crypt = tdes.CreateEncryptor();
            byte[] cipher = crypt.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            tdes.Clear();
            return Convert.ToBase64String(cipher, 0, cipher.Length);
        }
        internal static string EncryptTextWithGSTNPublicKey(string plainText)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            return EncryptTextWithGSTNPublicKey(bytesToBeEncrypted);
        }
        internal static string EncryptTextWithGSTNPublicKey(byte[] bytesToBeEncrypted)
        {
            X509Certificate2 certificate = GSTNPublickey();
            RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;

            byte[] bytesEncrypted = rsa.Encrypt(bytesToBeEncrypted, false);

            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }
        internal static string Hmac(string plainText, CipherKeys keys)
        {
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
            var sessionKeyBytes = GetSessionKeyBytes(keys);
            using (var hmacsha256 = new HMACSHA256(sessionKeyBytes))
            {
                byte[] data = Encoding.UTF8.GetBytes(base64String);
                byte[] hashmessage = hmacsha256.ComputeHash(data);
                return Convert.ToBase64String(hashmessage);
            }
        }
        internal static byte[] GetSessionKeyBytes(CipherKeys keys)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(keys.SessionKey);
            AesManaged tdes = new AesManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Key = Encoding.UTF8.GetBytes(keys.GSTNAppKey),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform decrypt = tdes.CreateDecryptor();
            byte[] deCipher = decrypt.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            tdes.Clear();

            return deCipher;
        }
        internal static byte[] GetRekBytes(string rek, CipherKeys keys)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(rek);
            AesManaged tdes = new AesManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Key = GetSessionKeyBytes(keys),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform decrypt = tdes.CreateDecryptor();
            byte[] deCipher = decrypt.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            tdes.Clear();

            return deCipher;
        }
        internal static string DecryptResponseData(string dataToDecrypt, string rek, CipherKeys keys)
        {
            byte[] dataToDecryptBytes = Convert.FromBase64String(dataToDecrypt);
            AesManaged tdes = new AesManaged
            {
                KeySize = 256,
                BlockSize = 128,
                Key = GetRekBytes(rek, keys),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform decrypt = tdes.CreateDecryptor();
            byte[] deCipher = decrypt.TransformFinalBlock(dataToDecryptBytes, 0, dataToDecryptBytes.Length);
            tdes.Clear();

            var deCipherData = Encoding.UTF8.GetString(deCipher);
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(deCipherData));

            return json;
        }
    }
}
