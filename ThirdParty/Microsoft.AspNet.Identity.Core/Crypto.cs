// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.Crypto
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;

    /// <summary>A crypto.</summary>
    internal static class Crypto
    {
        /// <summary>Number of pbkdf 2 iterators.</summary>
        private const int PBKDF2IterCount = 1000;

        /// <summary>Length of the pbkdf 2 subkey.</summary>
        private const int PBKDF2SubkeyLength = 32;

        /// <summary>Size of the salt.</summary>
        private const int SaltSize = 16;

        /// <summary>Hash password.</summary>
        /// <param name="password">The password.</param>
        /// <returns>A string.</returns>
        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            byte[] salt;
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            var inArray = new byte[49];
            Buffer.BlockCopy(salt, 0, inArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, inArray, 17, 32);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>Verify hashed password.</summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="password">      The password.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            var numArray = Convert.FromBase64String(hashedPassword);
            if (numArray.Length != 49 || numArray[0] != 0)
            {
                return false;
            }
            var salt = new byte[16];
            Buffer.BlockCopy(numArray, 1, salt, 0, 16);
            var a = new byte[32];
            Buffer.BlockCopy(numArray, 17, a, 0, 32);
            byte[] bytes;
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 1000))
            {
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            return ByteArraysEqual(a, bytes);
        }

        /// <summary>Byte arrays equal.</summary>
        /// <param name="a">The byte[] to process.</param>
        /// <param name="b">The byte[] to process.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == b)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var flag = true;
            for (var index = 0; index < a.Length; ++index)
            {
                flag &= a[index] == b[index];
            }
            return flag;
        }
    }
}
