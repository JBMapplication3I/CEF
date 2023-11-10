// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Encryption.ZipAESStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>A zip the es stream.</summary>
    /// <seealso cref="System.Security.Cryptography.CryptoStream" />
    internal class ZipAESStream : CryptoStream
    {
        /// <summary>Length of the authentication code.</summary>
        private const int AUTH_CODE_LENGTH = 10;

        /// <summary>Size of the crypto block.</summary>
        private const int CRYPTO_BLOCK_SIZE = 16;

        /// <summary>The block and authentication.</summary>
        private readonly int _blockAndAuth;

        /// <summary>Buffer for slide data.</summary>
        private readonly byte[] _slideBuffer;

        /// <summary>The stream.</summary>
        private readonly Stream _stream;

        /// <summary>The transform.</summary>
        private readonly ZipAESTransform _transform;

        /// <summary>The slide buffer free position.</summary>
        private int _slideBufFreePos;

        /// <summary>The slide buffer start position.</summary>
        private int _slideBufStartPos;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Encryption.ZipAESStream" />
        ///     class.
        /// </summary>
        /// <param name="stream">   The stream.</param>
        /// <param name="transform">The transform.</param>
        /// <param name="mode">     The mode.</param>
        public ZipAESStream(Stream stream, ZipAESTransform transform, CryptoStreamMode mode)
            : base(stream, transform, mode)
        {
            this._stream = stream;
            this._transform = transform;
            this._slideBuffer = new byte[1024];
            this._blockAndAuth = 26;
            if (mode != CryptoStreamMode.Read)
            {
                throw new Exception("ZipAESStream only for read");
            }
        }

        /// <inheritdoc/>
        public override int Read(byte[] outBuffer, int offset, int count)
        {
            var num1 = 0;
            while (num1 < count)
            {
                var count1 = this._blockAndAuth - (this._slideBufFreePos - this._slideBufStartPos);
                if (this._slideBuffer.Length - this._slideBufFreePos < count1)
                {
                    var index = 0;
                    var slideBufStartPos = this._slideBufStartPos;
                    while (slideBufStartPos < this._slideBufFreePos)
                    {
                        this._slideBuffer[index] = this._slideBuffer[slideBufStartPos];
                        ++slideBufStartPos;
                        ++index;
                    }

                    this._slideBufFreePos -= this._slideBufStartPos;
                    this._slideBufStartPos = 0;
                }

                this._slideBufFreePos += this._stream.Read(this._slideBuffer, this._slideBufFreePos, count1);
                var num2 = this._slideBufFreePos - this._slideBufStartPos;
                if (num2 >= this._blockAndAuth)
                {
                    this._transform.TransformBlock(this._slideBuffer, this._slideBufStartPos, 16, outBuffer, offset);
                    num1 += 16;
                    offset += 16;
                    this._slideBufStartPos += 16;
                }
                else
                {
                    if (num2 > 10)
                    {
                        var inputCount = num2 - 10;
                        this._transform.TransformBlock(
                            this._slideBuffer,
                            this._slideBufStartPos,
                            inputCount,
                            outBuffer,
                            offset);
                        num1 += inputCount;
                        this._slideBufStartPos += inputCount;
                    }
                    else if (num2 < 10)
                    {
                        throw new Exception("Internal error missed auth code");
                    }

                    var authCode = this._transform.GetAuthCode();
                    for (var index = 0; index < 10; ++index)
                    {
                        if (authCode[index] != this._slideBuffer[this._slideBufStartPos + index])
                        {
                            throw new Exception(
                                "AES Authentication Code does not match. This is a super-CRC check on the data in the file after compression and encryption. \r\nThe file may be damaged.");
                        }
                    }

                    break;
                }
            }

            return num1;
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}