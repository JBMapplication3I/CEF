// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>Buffer for inflater input.</summary>
    public class InflaterInputBuffer
    {

        /// <summary>The crypto transform.</summary>
        private ICryptoTransform cryptoTransform;

        /// <summary>Stream to read data from.</summary>
        private readonly Stream inputStream;

        /// <summary>The internal clear text.</summary>
        private byte[] internalClearText;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public InflaterInputBuffer(Stream stream) : this(stream, 4096) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer" /> class.
        /// </summary>
        /// <param name="stream">    The stream.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        public InflaterInputBuffer(Stream stream, int bufferSize)
        {
            inputStream = stream;
            if (bufferSize < 1024)
            {
                bufferSize = 1024;
            }
            RawData = new byte[bufferSize];
            ClearText = RawData;
        }

        /// <summary>Gets or sets the available.</summary>
        /// <value>The available.</value>
        public int Available { get; set; }

        /// <summary>Gets the clear text.</summary>
        /// <value>The clear text.</value>
        public byte[] ClearText { get; private set; }

        /// <summary>Gets the length of the clear text.</summary>
        /// <value>The length of the clear text.</value>
        public int ClearTextLength { get; private set; }

        /// <summary>Sets the crypto transform.</summary>
        /// <value>The crypto transform.</value>
        public ICryptoTransform CryptoTransform
        {
            set
            {
                cryptoTransform = value;
                if (cryptoTransform != null)
                {
                    if (RawData == ClearText)
                    {
                        if (internalClearText == null)
                        {
                            internalClearText = new byte[RawData.Length];
                        }
                        ClearText = internalClearText;
                    }
                    ClearTextLength = RawLength;
                    if (Available <= 0)
                    {
                        return;
                    }
                    cryptoTransform.TransformBlock(
                        RawData,
                        RawLength - Available,
                        Available,
                        ClearText,
                        RawLength - Available);
                }
                else
                {
                    ClearText = RawData;
                    ClearTextLength = RawLength;
                }
            }
        }

        /// <summary>Gets information describing the raw.</summary>
        /// <value>Information describing the raw.</value>
        public byte[] RawData { get; }

        /// <summary>Gets the length of the raw.</summary>
        /// <value>The length of the raw.</value>
        public int RawLength { get; private set; }

        /// <summary>Fills this InflaterInputBuffer.</summary>
        public void Fill()
        {
            RawLength = 0;
            int num;
            for (var length = RawData.Length; length > 0; length -= num)
            {
                num = inputStream.Read(RawData, RawLength, length);
                if (num > 0)
                {
                    RawLength += num;
                }
                else
                {
                    break;
                }
            }
            ClearTextLength = cryptoTransform == null
                ? RawLength
                : cryptoTransform.TransformBlock(RawData, 0, RawLength, ClearText, 0);
            Available = ClearTextLength;
        }

        /// <summary>Reads clear text buffer.</summary>
        /// <param name="outBuffer">Buffer for out data.</param>
        /// <param name="offset">   The offset.</param>
        /// <param name="length">   The length.</param>
        /// <returns>The clear text buffer.</returns>
        public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            var destinationIndex = offset;
            var val1 = length;
            while (val1 > 0)
            {
                if (Available <= 0)
                {
                    Fill();
                    if (Available <= 0)
                    {
                        return 0;
                    }
                }
                var length1 = Math.Min(val1, Available);
                Array.Copy(ClearText, ClearTextLength - Available, outBuffer, destinationIndex, length1);
                destinationIndex += length1;
                val1 -= length1;
                Available -= length1;
            }
            return length;
        }

        /// <summary>Reads le byte.</summary>
        /// <returns>The le byte.</returns>
        public int ReadLeByte()
        {
            if (Available <= 0)
            {
                Fill();
                if (Available <= 0)
                {
                    throw new ZipException("EOF in header");
                }
            }
            var num = RawData[RawLength - Available];
            --Available;
            return num;
        }

        /// <summary>Reads le int.</summary>
        /// <returns>The le int.</returns>
        public int ReadLeInt()
        {
            return ReadLeShort() | (ReadLeShort() << 16);
        }

        /// <summary>Reads le long.</summary>
        /// <returns>The le long.</returns>
        public long ReadLeLong()
        {
            return (uint)ReadLeInt() | ((long)ReadLeInt() << 32);
        }

        /// <summary>Reads le short.</summary>
        /// <returns>The le short.</returns>
        public int ReadLeShort()
        {
            return ReadLeByte() | (ReadLeByte() << 8);
        }

        /// <summary>Reads raw buffer.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The raw buffer.</returns>
        public int ReadRawBuffer(byte[] buffer)
        {
            return ReadRawBuffer(buffer, 0, buffer.Length);
        }

        /// <summary>Reads raw buffer.</summary>
        /// <param name="outBuffer">Buffer for out data.</param>
        /// <param name="offset">   The offset.</param>
        /// <param name="length">   The length.</param>
        /// <returns>The raw buffer.</returns>
        public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            var destinationIndex = offset;
            var val1 = length;
            while (val1 > 0)
            {
                if (Available <= 0)
                {
                    Fill();
                    if (Available <= 0)
                    {
                        return 0;
                    }
                }
                var length1 = Math.Min(val1, Available);
                Array.Copy(RawData, RawLength - Available, outBuffer, destinationIndex, length1);
                destinationIndex += length1;
                val1 -= length1;
                Available -= length1;
            }
            return length;
        }

        /// <summary>Sets inflater input.</summary>
        /// <param name="inflater">The inflater.</param>
        public void SetInflaterInput(Inflater inflater)
        {
            if (Available <= 0)
            {
                return;
            }
            inflater.SetInput(ClearText, ClearTextLength - Available, Available);
            Available = 0;
        }
    }
}
