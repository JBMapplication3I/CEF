// <copyright file="CMSApiEncoder.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CMS API encoder class</summary>
namespace Clarity.Ecommerce.Encryption
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>The CMS API encoder.</summary>
    public static class CMSApiEncoder
    {
        private const string PasswordHash = "qpzm9731";
        private const string SaltKey = "Cl4r1tyV3ntur3s";
        private const string VIKey = "46g7HeDF@52c831B";

        /// <summary>Encrypts a string.</summary>
        /// <param name="plainText">   The plain text.</param>
        /// <param name="isUrlEncoded">true if this object is URL encoded.</param>
        /// <returns>A string.</returns>
        /// <remarks>This function is still used by a ShipCarrier Mapping process that isn't async.</remarks>
        [Obsolete("2019.3: Async version available")]
        public static string Encrypt(string plainText, bool isUrlEncoded = false)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                throw new ArgumentNullException(nameof(plainText), "The plainText parameter must not be null.");
            }
            using var derivedBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey));
            using var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            using var encryptor = symmetricKey.CreateEncryptor(derivedBytes.GetBytes(256 / 8), Encoding.ASCII.GetBytes(VIKey));
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            var output = Convert.ToBase64String(cipherTextBytes);
            if (isUrlEncoded)
            {
                output = HttpUtility.UrlEncode(output);
            }
            return output;
        }

        /// <summary>Encrypts a string.</summary>
        /// <param name="plainText">   The plain text.</param>
        /// <param name="isUrlEncoded">true if this object is URL encoded.</param>
        /// <returns>A string.</returns>
        public static async Task<string> EncryptAsync(string plainText, bool isUrlEncoded = false)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                throw new ArgumentNullException(nameof(plainText), "The plainText parameter must not be null.");
            }
            using var derivedBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey));
            using var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            using var encryptor = symmetricKey.CreateEncryptor(derivedBytes.GetBytes(256 / 8), Encoding.ASCII.GetBytes(VIKey));
#if NET5_0_OR_GREATER
            await using var memoryStream = new MemoryStream();
            await using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            await cryptoStream.WriteAsync(Encoding.UTF8.GetBytes(plainText).AsMemory()).ConfigureAwait(false);
            await cryptoStream.FlushFinalBlockAsync();
#else
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            await cryptoStream.WriteAsync(plainTextBytes, 0, plainTextBytes.Length).ConfigureAwait(false);
            cryptoStream.FlushFinalBlock();
#endif
            var cipherTextBytes = memoryStream.ToArray();
            var output = Convert.ToBase64String(cipherTextBytes);
            if (isUrlEncoded)
            {
                output = HttpUtility.UrlEncode(output);
            }
            return output;
        }

        /// <summary>Decrypts a string.</summary>
        /// <remarks>This function is still used by a ShipCarrier Mapping process that isn't async.</remarks>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <param name="isUrlEncoded"> true if this object is URL encoded.</param>
        /// <returns>A string.</returns>
        [Obsolete("2019.3: Async version available")]
        public static string Decrypt(string encryptedText, bool isUrlEncoded = false)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
            {
                throw new ArgumentNullException(nameof(encryptedText), "The encryptedText parameter must not be null.");
            }
            if (isUrlEncoded)
            {
                encryptedText = HttpUtility.UrlDecode(encryptedText);
            }
            using var derivedBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey));
            using var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            using var decryptor = symmetricKey.CreateDecryptor(derivedBytes.GetBytes(256 / 8), Encoding.ASCII.GetBytes(VIKey));
            var cipherTextBytes = Convert.FromBase64String(encryptedText);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            return Encoding.UTF8.GetString(
                    plainTextBytes,
                    0,
                    cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length))
                .TrimEnd("\0".ToCharArray());
        }

        /// <summary>Decrypts a string.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <param name="isUrlEncoded"> true if this object is URL encoded.</param>
        /// <returns>A string.</returns>
        public static async Task<string> DecryptAsync(string encryptedText, bool isUrlEncoded = false)
        {
            if (string.IsNullOrWhiteSpace(encryptedText))
            {
                throw new ArgumentNullException(nameof(encryptedText), "The encryptedText parameter must not be null.");
            }
            if (isUrlEncoded)
            {
                encryptedText = HttpUtility.UrlDecode(encryptedText);
            }
            using var derivedBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey));
            using var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            using var decryptor = symmetricKey.CreateDecryptor(derivedBytes.GetBytes(256 / 8), Encoding.ASCII.GetBytes(VIKey));
            var cipherTextBytes = Convert.FromBase64String(encryptedText);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
#if NET5_0_OR_GREATER
            var plainTextBytes = new byte[cipherTextBytes.Length].AsMemory();
            var count = await cryptoStream.ReadAsync(plainTextBytes).ConfigureAwait(false);
            return Encoding.UTF8.GetString(bytes: plainTextBytes.ToArray(), index: 0, count: count).TrimEnd("\0".ToCharArray());
#else
            var plainTextBytes = new byte[cipherTextBytes.Length];
            return Encoding.UTF8.GetString(
                    bytes: plainTextBytes,
                    index: 0,
                    count: await cryptoStream.ReadAsync(plainTextBytes, 0, plainTextBytes.Length).ConfigureAwait(false))
                .TrimEnd("\0".ToCharArray());
#endif
        }
    }
}
