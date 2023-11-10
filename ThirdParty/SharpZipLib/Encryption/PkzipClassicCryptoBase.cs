// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Encryption.PkzipClassicCryptoBase
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;

    using ICSharpCode.SharpZipLib.Checksums;

    /// <summary>A pkzip classic crypto base.</summary>
    internal class PkzipClassicCryptoBase
    {
        /// <summary>The keys.</summary>
        private uint[] keys;

        /// <summary>Resets this PkzipClassicCryptoBase.</summary>
        protected void Reset()
        {
            this.keys[0] = 0U;
            this.keys[1] = 0U;
            this.keys[2] = 0U;
        }

        /// <summary>Sets the keys.</summary>
        /// <param name="keyData">Information describing the key.</param>
        protected void SetKeys(byte[] keyData)
        {
            if (keyData == null)
            {
                throw new ArgumentNullException(nameof(keyData));
            }

            if (keyData.Length != 12)
            {
                throw new InvalidOperationException("Key length is not valid");
            }

            this.keys = new uint[3];
            this.keys[0] = (uint)((keyData[3] << 24) | (keyData[2] << 16) | (keyData[1] << 8)) | keyData[0];
            this.keys[1] = (uint)((keyData[7] << 24) | (keyData[6] << 16) | (keyData[5] << 8)) | keyData[4];
            this.keys[2] = (uint)((keyData[11] << 24) | (keyData[10] << 16) | (keyData[9] << 8)) | keyData[8];
        }

        /// <summary>Transform byte.</summary>
        /// <returns>A byte.</returns>
        protected byte TransformByte()
        {
            var num = (uint)(((int)this.keys[2] & ushort.MaxValue) | 2);
            return (byte)((num * (num ^ 1U)) >> 8);
        }

        /// <summary>Updates the keys described by ch.</summary>
        /// <param name="ch">The ch.</param>
        protected void UpdateKeys(byte ch)
        {
            this.keys[0] = Crc32.ComputeCrc32(this.keys[0], ch);
            this.keys[1] = this.keys[1] + (byte)this.keys[0];
            this.keys[1] = (uint)((int)this.keys[1] * 134775813 + 1);
            this.keys[2] = Crc32.ComputeCrc32(this.keys[2], (byte)(this.keys[1] >> 24));
        }
    }
}