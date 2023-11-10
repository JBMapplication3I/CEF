// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Encryption.PkzipClassicDecryptCryptoTransform
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;
    using System.Security.Cryptography;

    /// <summary>Form for viewing the pkzip classic decrypt crypto transaction.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Encryption.PkzipClassicCryptoBase" />
    /// <seealso cref="System.Security.Cryptography.ICryptoTransform" />
    /// <seealso cref="IDisposable" />
    internal class PkzipClassicDecryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Encryption.PkzipClassicDecryptCryptoTransform" /> class.
        /// </summary>
        /// <param name="keyBlock">The key block.</param>
        internal PkzipClassicDecryptCryptoTransform(byte[] keyBlock)
        {
            this.SetKeys(keyBlock);
        }

        /// <inheritdoc/>
        public bool CanReuseTransform => true;

        /// <inheritdoc/>
        public bool CanTransformMultipleBlocks => true;

        /// <inheritdoc/>
        public int InputBlockSize => 1;

        /// <inheritdoc/>
        public int OutputBlockSize => 1;

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Reset();
        }

        /// <inheritdoc/>
        public int TransformBlock(
            byte[] inputBuffer,
            int inputOffset,
            int inputCount,
            byte[] outputBuffer,
            int outputOffset)
        {
            for (var index = inputOffset; index < inputOffset + inputCount; ++index)
            {
                var ch = (byte)(inputBuffer[index] ^ (uint)this.TransformByte());
                outputBuffer[outputOffset++] = ch;
                this.UpdateKeys(ch);
            }

            return inputCount;
        }

        /// <inheritdoc/>
        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            var outputBuffer = new byte[inputCount];
            this.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
            return outputBuffer;
        }
    }
}