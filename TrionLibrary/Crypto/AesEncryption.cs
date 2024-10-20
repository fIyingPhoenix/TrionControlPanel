using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace TrionLibrary.Crypto
{
    public class AesEncryption
    {
        // Define a 16-byte (128-bit) secret key and 16-byte initialization vector (IV)
        private static readonly string key = "A1B2C3D4E5F6G7H8"; // 16 bytes for AES-128
        private static readonly string iv = "1234567890123456"; // 16 bytes for AES-128-CBC

        // Encrypt function
        public static string Encrypt(string plaintext)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new())
                {
                    using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new(csEncrypt))
                        {
                            swEncrypt.Write(plaintext);
                        }
                        byte[] encrypted = msEncrypt.ToArray();
                        return Convert.ToBase64String(encrypted); // Encode in base64 for easier transmission/storage
                    }
                }
            }
        }
       
    }

}
