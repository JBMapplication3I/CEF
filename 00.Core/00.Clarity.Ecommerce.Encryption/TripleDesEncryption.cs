// <copyright file="TripleDesEncryption.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the triple des encryption class</summary>
namespace Clarity.Ecommerce.Encryption
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>A triple description encryption.</summary>
    internal static class TripleDesEncryption
    {
        private const string Key = "x8b0f!@6fW98p76|ILZQ";

        /// <summary>Encrypts the provided string.</summary>
        /// <param name="toEncrypt">to encrypt.</param>
        /// <returns>A string.</returns>
        public static string Encrypt(string toEncrypt)
        {
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            var keyArray = GetKey();
            var provider = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
            };
            var transform = provider.CreateEncryptor();
            var resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            provider.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>Decrypts the provided string.</summary>
        /// <param name="cipherString">The cipher string.</param>
        /// <returns>A string.</returns>
        public static string Decrypt(string cipherString)
        {
            var toEncryptArray = Convert.FromBase64String(cipherString);
            var keyArray = GetKey();
            var provider = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
            };
            var transform = provider.CreateDecryptor();
            var resultArray = transform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
            provider.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

        private static byte[] GetKey()
        {
            var hash = new SHA512CryptoServiceProvider();
            var keyArray = hash.ComputeHash(Encoding.UTF8.GetBytes(Key));
            var trimmedBytes = new byte[24];
            Buffer.BlockCopy(keyArray, 0, trimmedBytes, 0, 24);
            return trimmedBytes;
        }
    }
}
