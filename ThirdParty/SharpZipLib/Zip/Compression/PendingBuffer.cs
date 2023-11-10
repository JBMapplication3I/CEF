// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.PendingBuffer
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;

    /// <summary>Buffer for pending.</summary>
    public class PendingBuffer
    {

        /// <summary>The bits.</summary>
        private uint bits;

        /// <summary>The buffer.</summary>
        private readonly byte[] buffer_;

        /// <summary>The end.</summary>
        private int end;

        /// <summary>The start.</summary>
        private int start;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.PendingBuffer" /> class.
        /// </summary>
        public PendingBuffer() : this(4096) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.PendingBuffer" /> class.
        /// </summary>
        /// <param name="bufferSize">Size of the buffer.</param>
        public PendingBuffer(int bufferSize)
        {
            buffer_ = new byte[bufferSize];
        }

        /// <summary>Gets the number of bits.</summary>
        /// <value>The number of bits.</value>
        public int BitCount { get; private set; }

        /// <summary>Gets a value indicating whether this PendingBuffer is flushed.</summary>
        /// <value>True if this PendingBuffer is flushed, false if not.</value>
        public bool IsFlushed => end == 0;

        /// <summary>Align to byte.</summary>
        public void AlignToByte()
        {
            if (BitCount > 0)
            {
                buffer_[end++] = (byte)bits;
                if (BitCount > 8)
                {
                    buffer_[end++] = (byte)(bits >> 8);
                }
            }
            bits = 0U;
            BitCount = 0;
        }

        /// <summary>Flushes this PendingBuffer.</summary>
        /// <param name="output">The output.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>An int.</returns>
        public int Flush(byte[] output, int offset, int length)
        {
            if (BitCount >= 8)
            {
                buffer_[end++] = (byte)bits;
                bits >>= 8;
                BitCount -= 8;
            }
            if (length > end - start)
            {
                length = end - start;
                Array.Copy(buffer_, start, output, offset, length);
                start = 0;
                end = 0;
            }
            else
            {
                Array.Copy(buffer_, start, output, offset, length);
                start += length;
            }
            return length;
        }

        /// <summary>Resets this PendingBuffer.</summary>
        public void Reset()
        {
            start = end = BitCount = 0;
        }

        /// <summary>Converts this PendingBuffer to a byte array.</summary>
        /// <returns>This PendingBuffer as a byte[].</returns>
        public byte[] ToByteArray()
        {
            var numArray = new byte[end - start];
            Array.Copy(buffer_, start, numArray, 0, numArray.Length);
            start = 0;
            end = 0;
            return numArray;
        }

        /// <summary>Writes the bits.</summary>
        /// <param name="b">    The int to process.</param>
        /// <param name="count">Number of.</param>
        public void WriteBits(int b, int count)
        {
            bits |= (uint)(b << BitCount);
            BitCount += count;
            if (BitCount < 16)
            {
                return;
            }
            buffer_[end++] = (byte)bits;
            buffer_[end++] = (byte)(bits >> 8);
            bits >>= 16;
            BitCount -= 16;
        }

        /// <summary>Writes a block.</summary>
        /// <param name="block"> The block.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public void WriteBlock(byte[] block, int offset, int length)
        {
            Array.Copy(block, offset, buffer_, end, length);
            end += length;
        }

        /// <summary>Writes a byte.</summary>
        /// <param name="value">The value.</param>
        public void WriteByte(int value)
        {
            buffer_[end++] = (byte)value;
        }

        /// <summary>Writes an int.</summary>
        /// <param name="value">The value.</param>
        public void WriteInt(int value)
        {
            buffer_[end++] = (byte)value;
            buffer_[end++] = (byte)(value >> 8);
            buffer_[end++] = (byte)(value >> 16);
            buffer_[end++] = (byte)(value >> 24);
        }

        /// <summary>Writes a short.</summary>
        /// <param name="value">The value.</param>
        public void WriteShort(int value)
        {
            buffer_[end++] = (byte)value;
            buffer_[end++] = (byte)(value >> 8);
        }

        /// <summary>Writes a short MSB.</summary>
        /// <param name="s">The int to process.</param>
        public void WriteShortMSB(int s)
        {
            buffer_[end++] = (byte)(s >> 8);
            buffer_[end++] = (byte)s;
        }
    }
}
