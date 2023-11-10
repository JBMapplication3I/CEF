// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Encryption.ZipAESTransform
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;
    using System.Security.Cryptography;

    /// <summary>Form for viewing the zip the es transaction.</summary>
    /// <seealso cref="System.Security.Cryptography.ICryptoTransform" />
    /// <seealso cref="IDisposable" />
    internal class ZipAESTransform : ICryptoTransform, IDisposable
    {
        /// <summary>The encrypt block.</summary>
        private const int ENCRYPT_BLOCK = 16;

        /// <summary>The key rounds.</summary>
        private const int KEY_ROUNDS = 1000;

        /// <summary>Length of the password version.</summary>
        private const int PWD_VER_LENGTH = 2;

        /// <summary>The counter nonce.</summary>
        private readonly byte[] _counterNonce;

        /// <summary>Buffer for encrypt data.</summary>
        private readonly byte[] _encryptBuffer;

        /// <summary>The encryptor.</summary>
        private readonly ICryptoTransform _encryptor;

        /// <summary>The first hmacsha.</summary>
        private readonly HMACSHA1 _hmacsha1;

        /// <summary>True to enable write mode, false to disable it.</summary>
        private readonly bool _writeMode;

        /// <summary>True if disposed.</summary>
        private bool disposed;

        /// <summary>The encr position.</summary>
        private int _encrPos;

        /// <summary>True if finalised.</summary>
        private bool _finalised;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Encryption.ZipAESTransform" />
        /// class.</summary>
        /// <param name="key">      The key.</param>
        /// <param name="saltBytes">The salt in bytes.</param>
        /// <param name="blockSize">Size of the block.</param>
        /// <param name="writeMode">True to enable write mode, false to disable it.</param>
        public ZipAESTransform(string key, byte[] saltBytes, int blockSize, bool writeMode)
        {
            if (blockSize != 16 && blockSize != 32)
            {
                throw new Exception("Invalid blocksize " + blockSize + ". Must be 16 or 32.");
            }
            if (saltBytes.Length != blockSize / 2)
            {
                throw new Exception("Invalid salt len. Must be " + blockSize / 2 + " for blocksize " + blockSize);
            }
            this.InputBlockSize = blockSize;
            this._encryptBuffer = new byte[this.InputBlockSize];
            this._encrPos = 16;
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, saltBytes, 1000);
            var rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.ECB
            };
            this._counterNonce = new byte[this.InputBlockSize];
            var bytes1 = rfc2898DeriveBytes.GetBytes(this.InputBlockSize);
            var bytes2 = rfc2898DeriveBytes.GetBytes(this.InputBlockSize);
            this._encryptor = rijndaelManaged.CreateEncryptor(bytes1, bytes2);
            this.PwdVerifier = rfc2898DeriveBytes.GetBytes(2);
            this._hmacsha1 = new HMACSHA1(bytes2);
            this._writeMode = writeMode;
        }

        ~ZipAESTransform()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public bool CanReuseTransform => true;

        /// <inheritdoc/>
        public bool CanTransformMultipleBlocks => true;

        /// <inheritdoc/>
        public int InputBlockSize { get; }

        /// <inheritdoc/>
        public int OutputBlockSize => this.InputBlockSize;

        /// <summary>Gets the password verifier.</summary>
        /// <value>The password verifier.</value>
        public byte[] PwdVerifier { get; }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases the unmanaged resources used by the ICSharpCode.SharpZipLib.Encryption.ZipAESTransform and
        /// optionally releases the managed resources.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                _encryptor.Dispose();
                _hmacsha1.Dispose();
            }
            disposed = true;
        }

        /// <summary>Gets authentication code.</summary>
        /// <returns>An array of byte.</returns>
        public byte[] GetAuthCode()
        {
            if (!this._finalised)
            {
                this._hmacsha1.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
                this._finalised = true;
            }

            return this._hmacsha1.Hash;
        }

        /// <inheritdoc/>
        public int TransformBlock(
            byte[] inputBuffer,
            int inputOffset,
            int inputCount,
            byte[] outputBuffer,
            int outputOffset)
        {
            if (!this._writeMode)
            {
                this._hmacsha1.TransformBlock(inputBuffer, inputOffset, inputCount, inputBuffer, inputOffset);
            }

            for (var index1 = 0; index1 < inputCount; ++index1)
            {
                if (this._encrPos == 16)
                {
                    var index2 = 0;
                    while (++this._counterNonce[index2] == 0)
                    {
                        ++index2;
                    }

                    this._encryptor.TransformBlock(this._counterNonce, 0, this.InputBlockSize, this._encryptBuffer, 0);
                    this._encrPos = 0;
                }

                outputBuffer[index1 + outputOffset] =
                    (byte)(inputBuffer[index1 + inputOffset] ^ this._encryptBuffer[this._encrPos++]);
            }

            if (this._writeMode)
            {
                this._hmacsha1.TransformBlock(outputBuffer, outputOffset, inputCount, outputBuffer, outputOffset);
            }

            return inputCount;
        }

        /// <inheritdoc/>
        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            throw new NotImplementedException("ZipAESTransform.TransformFinalBlock");
        }
    }
}