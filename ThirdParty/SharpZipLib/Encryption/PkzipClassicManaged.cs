// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Encryption.PkzipClassicManaged
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;
    using System.Security.Cryptography;

    /// <summary>A pkzip classic managed. This class cannot be inherited.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Encryption.PkzipClassic" />
    public sealed class PkzipClassicManaged : PkzipClassic
    {
        /// <summary>The key.</summary>
        private byte[] key_;

        /// <inheritdoc/>
        public override int BlockSize
        {
            get => 8;
            set
            {
                if (value != 8)
                {
                    throw new CryptographicException("Block size is invalid");
                }
            }
        }

        /// <inheritdoc/>
        public override byte[] Key
        {
            get
            {
                if (this.key_ == null)
                {
                    this.GenerateKey();
                }

                return (byte[])this.key_.Clone();
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.key_ = value.Length == 12
                                ? (byte[])value.Clone()
                                : throw new CryptographicException("Key size is illegal");
            }
        }

        /// <inheritdoc/>
        public override KeySizes[] LegalBlockSizes => new KeySizes[1] { new(8, 8, 0) };

        /// <inheritdoc/>
        public override KeySizes[] LegalKeySizes => new KeySizes[1] { new(96, 96, 0) };

        /// <inheritdoc/>
        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            this.key_ = rgbKey;
            return new PkzipClassicDecryptCryptoTransform(this.Key);
        }

        /// <inheritdoc/>
        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            this.key_ = rgbKey;
            return new PkzipClassicEncryptCryptoTransform(this.Key);
        }

        /// <inheritdoc/>
        public override void GenerateIV() { }

        /// <inheritdoc/>
        public override void GenerateKey()
        {
            this.key_ = new byte[12];
            new Random().NextBytes(this.key_);
        }
    }
}