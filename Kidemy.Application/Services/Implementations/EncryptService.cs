using Kidemy.Application.Services.Interfaces;
using Kidemy.Domain.Statics;
using System.Security.Cryptography;
using System.Text;

namespace Kidemy.Application.Services.Implementations
{
    public class EncryptService : IEncryptService
    {
        public string EncryptText(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText)) return plainText;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(SiteTools.SecretKey)));
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    byte[] encryptedBytes = msEncrypt.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public string DecryptText(string cipherText)
        {
            if (string.IsNullOrWhiteSpace(cipherText)) return cipherText;

            byte[] encryptedBytes = Convert.FromBase64String(cipherText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(SiteTools.SecretKey)));
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
