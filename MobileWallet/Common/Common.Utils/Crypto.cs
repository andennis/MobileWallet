﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils
{
    public static class Crypto
    {
        public static string  EncryptString(string textToEncrypt, string key, string iv)
        {
            if (string.IsNullOrEmpty(textToEncrypt))
                throw new ArgumentNullException("textToEncrypt");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (string.IsNullOrEmpty(iv))
                throw new ArgumentNullException("iv");

            using (var aesAlg = new AesManaged())
            {
                //aesAlg.Padding = PaddingMode.;
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (var msEncrypt = new MemoryStream())
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(textToEncrypt);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
        public static string DecryptString(string textToDecrypt, string key, string iv)
        {
            if (string.IsNullOrEmpty(textToDecrypt))
                throw new ArgumentNullException("textToDecrypt");
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (string.IsNullOrEmpty(iv))
                throw new ArgumentNullException("iv");

            byte[] dataToDecrypt = Convert.FromBase64String(textToDecrypt);
            using (var aesAlg = new AesManaged())
            {
                //aesAlg.Padding = PaddingMode.None;
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = Encoding.UTF8.GetBytes(iv);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                try
                {
                    using (var msDecrypt = new MemoryStream(dataToDecrypt))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
                catch (CryptographicException) //TODO exception massage “Padding is invalid and cannot be removed"
                {
                    return null;
                }
            }
        }

        public static string CalculateHash(byte[] data)
        {
            var hashText = new StringBuilder();
            byte[] hashData = SHA1.Create().ComputeHash(data);

            foreach (byte b in hashData)
            {
                string hexValue = b.ToString("X2").ToLower();
                hashText.Append(hexValue);
            }

            return hashText.ToString();
        }

    }
}
