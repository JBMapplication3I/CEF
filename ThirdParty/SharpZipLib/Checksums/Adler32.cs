// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Checksums.Adler32
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Checksums
{
    using System;

    /// <summary>An adler 32. This class cannot be inherited.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Checksums.IChecksum" />
    public sealed class Adler32 : IChecksum
    {
        /// <summary>The base.</summary>
        private const uint BASE = 65521;

        /// <summary>The checksum.</summary>
        private uint checksum;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Checksums.Adler32" /> class.</summary>
        public Adler32()
        {
            this.Reset();
        }

        /// <inheritdoc/>
        public long Value => this.checksum;

        /// <summary>Resets this Adler32.</summary>
        public void Reset()
        {
            this.checksum = 1U;
        }

        /// <summary>Updates the given value.</summary>
        /// <param name="value">The value.</param>
        public void Update(int value)
        {
            var num1 = this.checksum & ushort.MaxValue;
            var num2 = this.checksum >> 16;
            var num3 = (num1 + (uint)(value & byte.MaxValue)) % 65521U;
            this.checksum = (((num3 + num2) % 65521U) << 16) + num3;
        }

        /// <summary>Updates the given buffer.</summary>
        /// <param name="buffer">The buffer.</param>
        public void Update(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            this.Update(buffer, 0, buffer.Length);
        }

        /// <summary>Updates this Adler32.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        public void Update(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "cannot be negative");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "cannot be negative");
            }

            if (offset >= buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "not a valid index into buffer");
            }

            if (offset + count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "exceeds buffer size");
            }

            var num1 = this.checksum & ushort.MaxValue;
            var num2 = this.checksum >> 16;
            while (count > 0)
            {
                var num3 = 3800;
                if (num3 > count)
                {
                    num3 = count;
                }

                count -= num3;
                while (--num3 >= 0)
                {
                    num1 += buffer[offset++] & (uint)byte.MaxValue;
                    num2 += num1;
                }

                num1 %= 65521U;
                num2 %= 65521U;
            }

            this.checksum = (num2 << 16) | num1;
        }
    }
}