// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Encryption.PkzipClassic
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;
    using System.Security.Cryptography;

    using ICSharpCode.SharpZipLib.Checksums;

    /// <summary>A pkzip classic.</summary>
    /// <seealso cref="System.Security.Cryptography.SymmetricAlgorithm" />
    public abstract class PkzipClassic : SymmetricAlgorithm
    {
        /// <summary>Generates the keys.</summary>
        /// <param name="seed">The seed.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] GenerateKeys(byte[] seed)
        {
            if (seed == null)
            {
                throw new ArgumentNullException(nameof(seed));
            }

            if (seed.Length == 0)
            {
                throw new ArgumentException("Length is zero", nameof(seed));
            }

            var numArray = new uint[3] { 305419896U, 591751049U, 878082192U };
            for (var index = 0; index < seed.Length; ++index)
            {
                numArray[0] = Crc32.ComputeCrc32(numArray[0], seed[index]);
                numArray[1] = numArray[1] + (byte)numArray[0];
                numArray[1] = (uint)((int)numArray[1] * 134775813 + 1);
                numArray[2] = Crc32.ComputeCrc32(numArray[2], (byte)(numArray[1] >> 24));
            }

            return new byte[12]
                       {
                           (byte)(numArray[0] & byte.MaxValue), (byte)((numArray[0] >> 8) & byte.MaxValue),
                           (byte)((numArray[0] >> 16) & byte.MaxValue), (byte)((numArray[0] >> 24) & byte.MaxValue),
                           (byte)(numArray[1] & byte.MaxValue), (byte)((numArray[1] >> 8) & byte.MaxValue),
                           (byte)((numArray[1] >> 16) & byte.MaxValue), (byte)((numArray[1] >> 24) & byte.MaxValue),
                           (byte)(numArray[2] & byte.MaxValue), (byte)((numArray[2] >> 8) & byte.MaxValue),
                           (byte)((numArray[2] >> 16) & byte.MaxValue), (byte)((numArray[2] >> 24) & byte.MaxValue)
                       };
        }
    }
}