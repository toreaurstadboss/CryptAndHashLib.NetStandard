using System;
using System.Configuration;
using System.Security.Cryptography;

namespace CryptAndHashLib.NetStandard
{
    public class Encryptor : IEncryptor
    {
        public string Encrypt(string plainText, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                var encryptor = aes.CreateEncryptor(GetEncryptionKeyFromConfig(), iv ?? aes.IV);
                var encryptedBytes = encryptor.Encrypt(plainText);
                return String.Concat(Convert.ToBase64String(iv ?? aes.IV), " ", Convert.ToBase64String(encryptedBytes));
            }
        }

        public string Encrypt(string plainText)
        {
            byte[] iv = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            return Encrypt(plainText, iv); 
        }

        public string Encrypt(string plainText, string iv)
        {
            return Encrypt(plainText, Convert.FromBase64String(iv));
        }

        public string Decrypt(string encryptedString)
        {
            if (String.IsNullOrEmpty(encryptedString))
            {
                return String.Empty;
            }

            var split = encryptedString.Split(' ');
            var iv = Convert.FromBase64String(split[0]);
            var cipher = Convert.FromBase64String(split[1]);

            using (var aes = Aes.Create())
            {
                var decryptor = aes.CreateDecryptor(GetEncryptionKeyFromConfig(), iv);
                var plainText = decryptor.Decrypt(cipher);

                return plainText;
            }
        }

        public string GenerateKey()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                return Convert.ToBase64String(aes.Key);
            }
        }

        public string GenerateIV()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateIV();
                return Convert.ToBase64String(aes.IV);
            }
        }

        public byte[] GetEncryptionKeyFromConfig()
        {
            try
            {
                return Convert.FromBase64String(ConfigurationManager.AppSettings[Constants.EncryptionKey]);
            }
            catch (ArgumentNullException err)
            {
                Console.WriteLine($"Error, encryption key not specified in configuration file. Make sure you have added application setting EncryptionKey to active configuration file first: {err}");
                throw;
            }
        }

        public byte[] GetIVFromConfig()
        {
            return Convert.FromBase64String(ConfigurationManager.AppSettings[Constants.IV]);
        }

        public string GetIVStringFromBytes(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

    }
}

