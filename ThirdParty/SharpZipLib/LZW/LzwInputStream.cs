// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.LZW.LzwInputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.LZW
{
    using System;
    using System.IO;

    /// <summary>A lzw input stream.</summary>
    /// <seealso cref="System.IO.Stream" />
    public class LzwInputStream : Stream
    {
        /// <summary>The extra.</summary>
        private const int EXTRA = 64;

        /// <summary>The table clear.</summary>
        private const int TBL_CLEAR = 256;

        /// <summary>The table first.</summary>
        private const int TBL_FIRST = 257;

        /// <summary>The base input stream.</summary>
        private readonly Stream baseInputStream;

        /// <summary>The data.</summary>
        private readonly byte[] data = new byte[8192];

        /// <summary>The one.</summary>
        private readonly byte[] one = new byte[1];

        /// <summary>The zeros.</summary>
        private readonly int[] zeros = new int[256];

        /// <summary>The bit mask.</summary>
        private int bitMask;

        /// <summary>The bit position.</summary>
        private int bitPos;

        /// <summary>True to enable block mode, false to disable it.</summary>
        private bool blockMode;

        /// <summary>The end.</summary>
        private int end;

        /// <summary>True to EOF.</summary>
        private bool eof;

        /// <summary>The fin character.</summary>
        private byte finChar;

        /// <summary>The free ent.</summary>
        private int freeEnt;

        /// <summary>The got.</summary>
        private int got;

        /// <summary>True if header parsed.</summary>
        private bool headerParsed;

        /// <summary>True if this LzwInputStream is closed.</summary>
        private bool isClosed;

        /// <summary>The maximum bits.</summary>
        private int maxBits;

        /// <summary>The maximum code.</summary>
        private int maxCode;

        /// <summary>The maximum code.</summary>
        private int maxMaxCode;

        /// <summary>The bits.</summary>
        private int nBits;

        /// <summary>The old code.</summary>
        private int oldCode;

        /// <summary>The stack.</summary>
        private byte[] stack;

        /// <summary>The stack p.</summary>
        private int stackP;

        /// <summary>The tab prefix.</summary>
        private int[] tabPrefix;

        /// <summary>The tab suffix.</summary>
        private byte[] tabSuffix;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.LZW.LzwInputStream" /> class.</summary>
        /// <param name="baseInputStream">The base input stream.</param>
        public LzwInputStream(Stream baseInputStream)
        {
            this.baseInputStream = baseInputStream;
        }

        /// <inheritdoc/>
        public override bool CanRead => this.baseInputStream.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => false;

        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <summary>Gets or sets a value indicating whether this LzwInputStream is stream owner.</summary>
        /// <value>True if this LzwInputStream is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; } = true;

        /// <inheritdoc/>
        public override long Length => this.got;

        /// <inheritdoc/>
        public override long Position
        {
            get => this.baseInputStream.Position;
            set => throw new NotSupportedException("InflaterInputStream Position not supported");
        }

        /// <inheritdoc/>
        public override IAsyncResult BeginWrite(
            byte[] buffer,
            int offset,
            int count,
            AsyncCallback callback,
            object state)
        {
            throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
        }

        /// <inheritdoc/>
        public override void Close()
        {
            if (this.isClosed)
            {
                return;
            }

            this.isClosed = true;
            if (!this.IsStreamOwner)
            {
                return;
            }

            this.baseInputStream.Close();
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            this.baseInputStream.Flush();
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!this.headerParsed)
            {
                this.ParseHeader();
            }

            if (this.eof)
            {
                return -1;
            }

            var num1 = offset;
            var tabPrefix = this.tabPrefix;
            var tabSuffix = this.tabSuffix;
            var stack = this.stack;
            var num2 = this.nBits;
            var num3 = this.maxCode;
            var maxMaxCode = this.maxMaxCode;
            var num4 = this.bitMask;
            var num5 = this.oldCode;
            var num6 = this.finChar;
            var sourceIndex = this.stackP;
            var index1 = this.freeEnt;
            var data = this.data;
            var bitPosition1 = this.bitPos;
            var num7 = stack.Length - sourceIndex;
            if (num7 > 0)
            {
                var length = num7 >= count ? count : num7;
                Array.Copy(stack, sourceIndex, buffer, offset, length);
                offset += length;
                count -= length;
                sourceIndex += length;
            }
            if (count == 0)
            {
                this.stackP = sourceIndex;
                return offset - num1;
            }
        label_8:
            do
            {
                if (this.end < 64)
                {
                    this.Fill();
                }
                var num8 = this.got > 0 ? (this.end - this.end % num2) << 3 : (this.end << 3) - (num2 - 1);
                while (bitPosition1 < num8)
                {
                    if (count == 0)
                    {
                        this.nBits = num2;
                        this.maxCode = num3;
                        this.maxMaxCode = maxMaxCode;
                        this.bitMask = num4;
                        this.oldCode = num5;
                        this.finChar = num6;
                        this.stackP = sourceIndex;
                        this.freeEnt = index1;
                        this.bitPos = bitPosition1;
                        return offset - num1;
                    }

                    if (index1 > num3)
                    {
                        var num9 = num2 << 3;
                        var bitPosition2 = bitPosition1 - 1 + num9 - (bitPosition1 - 1 + num9) % num9;
                        ++num2;
                        num3 = num2 == this.maxBits ? maxMaxCode : (1 << num2) - 1;
                        num4 = (1 << num2) - 1;
                        bitPosition1 = this.ResetBuf(bitPosition2);
                        goto label_8;
                    }

                    var index2 = bitPosition1 >> 3;
                    var index3 =
                        (((data[index2] & byte.MaxValue) | ((data[index2 + 1] & byte.MaxValue) << 8)
                                                         | ((data[index2 + 2] & byte.MaxValue) << 16))
                         >> (bitPosition1 & 7)) & num4;
                    bitPosition1 += num2;
                    if (num5 == -1)
                    {
                        if (index3 >= 256)
                        {
                            throw new LzwException("corrupt input: " + index3 + " > 255");
                        }

                        num6 = (byte)(num5 = index3);
                        buffer[offset++] = num6;
                        --count;
                    }
                    else if (index3 == 256 && this.blockMode)
                    {
                        Array.Copy(this.zeros, 0, tabPrefix, 0, this.zeros.Length);
                        index1 = 256;
                        var num9 = num2 << 3;
                        var bitPosition2 = bitPosition1 - 1 + num9 - (bitPosition1 - 1 + num9) % num9;
                        num2 = 9;
                        num3 = (1 << num2) - 1;
                        num4 = num3;
                        bitPosition1 = this.ResetBuf(bitPosition2);
                        goto label_8;
                    }
                    else
                    {
                        var num9 = index3;
                        var length1 = stack.Length;
                        if (index3 >= index1)
                        {
                            if (index3 > index1)
                            {
                                throw new LzwException("corrupt input: code=" + index3 + ", freeEnt=" + index1);
                            }

                            stack[--length1] = num6;
                            index3 = num5;
                        }

                        for (; index3 >= 256; index3 = tabPrefix[index3])
                        {
                            stack[--length1] = tabSuffix[index3];
                        }

                        num6 = tabSuffix[index3];
                        buffer[offset++] = num6;
                        --count;
                        var num10 = stack.Length - length1;
                        var length2 = num10 >= count ? count : num10;
                        Array.Copy(stack, length1, buffer, offset, length2);
                        offset += length2;
                        count -= length2;
                        sourceIndex = length1 + length2;
                        if (index1 < maxMaxCode)
                        {
                            tabPrefix[index1] = num5;
                            tabSuffix[index1] = num6;
                            ++index1;
                        }

                        num5 = num9;
                        if (count == 0)
                        {
                            this.nBits = num2;
                            this.maxCode = num3;
                            this.bitMask = num4;
                            this.oldCode = num5;
                            this.finChar = num6;
                            this.stackP = sourceIndex;
                            this.freeEnt = index1;
                            this.bitPos = bitPosition1;
                            return offset - num1;
                        }
                    }
                }

                bitPosition1 = this.ResetBuf(bitPosition1);
            }
            while (this.got > 0);

            this.nBits = num2;
            this.maxCode = num3;
            this.bitMask = num4;
            this.oldCode = num5;
            this.finChar = num6;
            this.stackP = sourceIndex;
            this.freeEnt = index1;
            this.bitPos = bitPosition1;
            this.eof = true;
            return offset - num1;
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            return this.Read(this.one, 0, 1) == 1 ? this.one[0] & byte.MaxValue : -1;
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seek not supported");
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("InflaterInputStream SetLength not supported");
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("InflaterInputStream Write not supported");
        }

        /// <inheritdoc/>
        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("InflaterInputStream WriteByte not supported");
        }

        /// <summary>Fills this LzwInputStream.</summary>
        private void Fill()
        {
            this.got = this.baseInputStream.Read(this.data, this.end, this.data.Length - 1 - this.end);
            if (this.got <= 0)
            {
                return;
            }

            this.end += this.got;
        }

        /// <summary>Parse header.</summary>
        private void ParseHeader()
        {
            this.headerParsed = true;
            var buffer = new byte[3];
            if (this.baseInputStream.Read(buffer, 0, buffer.Length) < 0)
            {
                throw new LzwException("Failed to read LZW header");
            }

            if (buffer[0] != 31 || buffer[1] != 157)
            {
                throw new LzwException(
                    string.Format(
                        "Wrong LZW header. Magic bytes don't match. 0x{0:x2} 0x{1:x2}",
                        buffer[0],
                        buffer[1]));
            }

            this.blockMode = (buffer[2] & 128) > 0;
            this.maxBits = buffer[2] & 31;
            if (this.maxBits > 16)
            {
                throw new LzwException(
                    "Stream compressed with " + this.maxBits + " bits, but decompression can only handle " + 16
                    + " bits.");
            }

            if ((buffer[2] & 96) > 0)
            {
                throw new LzwException("Unsupported bits set in the header.");
            }

            this.maxMaxCode = 1 << this.maxBits;
            this.nBits = 9;
            this.maxCode = (1 << this.nBits) - 1;
            this.bitMask = this.maxCode;
            this.oldCode = -1;
            this.finChar = 0;
            this.freeEnt = this.blockMode ? 257 : 256;
            this.tabPrefix = new int[1 << this.maxBits];
            this.tabSuffix = new byte[1 << this.maxBits];
            this.stack = new byte[1 << this.maxBits];
            this.stackP = this.stack.Length;
            for (int maxValue = byte.MaxValue; maxValue >= 0; --maxValue)
            {
                this.tabSuffix[maxValue] = (byte)maxValue;
            }
        }

        /// <summary>Resets the buffer described by bitPosition.</summary>
        /// <param name="bitPosition">The bit position.</param>
        /// <returns>An int.</returns>
        private int ResetBuf(int bitPosition)
        {
            var sourceIndex = bitPosition >> 3;
            Array.Copy(this.data, sourceIndex, this.data, 0, this.end - sourceIndex);
            this.end -= sourceIndex;
            return 0;
        }
    }
}