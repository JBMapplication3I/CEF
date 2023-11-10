// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.StreamManipulator
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;

    /// <summary>A stream manipulator.</summary>
    public class StreamManipulator
    {

        /// <summary>The buffer.</summary>
        private uint buffer_;

        /// <summary>The window.</summary>
        private byte[] window_;

        /// <summary>The window end.</summary>
        private int windowEnd_;

        /// <summary>The window start.</summary>
        private int windowStart_;

        /// <summary>Gets the available bits.</summary>
        /// <value>The available bits.</value>
        public int AvailableBits { get; private set; }

        /// <summary>Gets the available bytes.</summary>
        /// <value>The available bytes.</value>
        public int AvailableBytes => windowEnd_ - windowStart_ + (AvailableBits >> 3);

        /// <summary>Gets a value indicating whether this StreamManipulator is needing input.</summary>
        /// <value>True if this StreamManipulator is needing input, false if not.</value>
        public bool IsNeedingInput => windowStart_ == windowEnd_;

        /// <summary>Copies the bytes.</summary>
        /// <param name="output">The output.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>An int.</returns>
        public int CopyBytes(byte[] output, int offset, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            if ((AvailableBits & 7) != 0)
            {
                throw new InvalidOperationException("Bit buffer is not byte aligned!");
            }
            var num1 = 0;
            while (AvailableBits > 0 && length > 0)
            {
                output[offset++] = (byte)buffer_;
                buffer_ >>= 8;
                AvailableBits -= 8;
                --length;
                ++num1;
            }
            if (length == 0)
            {
                return num1;
            }
            var num2 = windowEnd_ - windowStart_;
            if (length > num2)
            {
                length = num2;
            }
            Array.Copy(window_, windowStart_, output, offset, length);
            windowStart_ += length;
            if (((windowStart_ - windowEnd_) & 1) != 0)
            {
                buffer_ = window_[windowStart_++] & (uint)byte.MaxValue;
                AvailableBits = 8;
            }
            return num1 + length;
        }

        /// <summary>Drop bits.</summary>
        /// <param name="bitCount">Number of bits.</param>
        public void DropBits(int bitCount)
        {
            buffer_ >>= bitCount;
            AvailableBits -= bitCount;
        }

        /// <summary>Gets the bits.</summary>
        /// <param name="bitCount">Number of bits.</param>
        /// <returns>The bits.</returns>
        public int GetBits(int bitCount)
        {
            var num = PeekBits(bitCount);
            if (num >= 0)
            {
                DropBits(bitCount);
            }
            return num;
        }

        /// <summary>Peek bits.</summary>
        /// <param name="bitCount">Number of bits.</param>
        /// <returns>An int.</returns>
        public int PeekBits(int bitCount)
        {
            if (AvailableBits < bitCount)
            {
                if (windowStart_ == windowEnd_)
                {
                    return -1;
                }
                buffer_ |= (uint)(((window_[windowStart_++] & byte.MaxValue)
                        | ((window_[windowStart_++] & byte.MaxValue) << 8))
                    << AvailableBits);
                AvailableBits += 16;
            }
            return (int)(buffer_ & ((1 << bitCount) - 1));
        }

        /// <summary>Resets this StreamManipulator.</summary>
        public void Reset()
        {
            buffer_ = 0U;
            windowStart_ = windowEnd_ = AvailableBits = 0;
        }

        /// <summary>Sets an input.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        public void SetInput(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            }
            if (windowStart_ < windowEnd_)
            {
                throw new InvalidOperationException("Old input was not completely processed");
            }
            var num = offset + count;
            if (offset > num || num > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if ((count & 1) != 0)
            {
                buffer_ |= (uint)((buffer[offset++] & byte.MaxValue) << AvailableBits);
                AvailableBits += 8;
            }
            window_ = buffer;
            windowStart_ = offset;
            windowEnd_ = num;
        }

        /// <summary>Skip to byte boundary.</summary>
        public void SkipToByteBoundary()
        {
            buffer_ >>= AvailableBits & 7;
            AvailableBits &= -8;
        }
    }
}
