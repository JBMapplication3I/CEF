// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll
namespace ICSharpCode.SharpZipLib.BZip2
{
    using System;
    using System.IO;

    using ICSharpCode.SharpZipLib.Checksums;

    /// <summary>A zip 2 output stream.</summary>
    /// <seealso cref="Stream" />
    public class BZip2OutputStream : Stream
    {
        /// <summary>The clearmask.</summary>
        private const int CLEARMASK = -2097153;

        /// <summary>The depth thresh.</summary>
        private const int DEPTH_THRESH = 10;

        /// <summary>The greater icost.</summary>
        private const int GREATER_ICOST = 15;

        /// <summary>The lesser icost.</summary>
        private const int LESSER_ICOST = 0;

        /// <summary>Size of the qsort stack.</summary>
        private const int QSORT_STACK_SIZE = 1000;

        /// <summary>The setmask.</summary>
        private const int SETMASK = 2097152;

        /// <summary>The small thresh.</summary>
        private const int SMALL_THRESH = 20;

        /// <summary>The block size 100k.</summary>
        private readonly int blockSize100k;

        /// <summary>The increments.</summary>
        private readonly int[] increments = new[] { 1, 4, 13, 40, 121, 364, 1093, 3280, 9841, 29524, 88573, 265720, 797161, 2391484, };

        /// <summary>The in use.</summary>
        private readonly bool[] inUse = new bool[256];

        /// <summary>The CRC.</summary>
        private readonly IChecksum mCrc = new StrangeCRC();

        /// <summary>The mtf frequency.</summary>
        private readonly int[] mtfFreq = new int[258];

        /// <summary>The selector.</summary>
        private readonly char[] selector = new char[18002];

        /// <summary>The selector mtf.</summary>
        private readonly char[] selectorMtf = new char[18002];

        /// <summary>The sequence to unseq.</summary>
        private readonly char[] seqToUnseq = new char[256];

        /// <summary>The unseq to sequence.</summary>
        private readonly char[] unseqToSeq = new char[256];

        /// <summary>The work factor.</summary>
        private readonly int workFactor;

        /// <summary>Size of the allowable block.</summary>
        private int allowableBlockSize;

        /// <summary>The base stream.</summary>
        private Stream baseStream;

        /// <summary>The block.</summary>
        private byte[] block;

        /// <summary>The block CRC.</summary>
        private uint blockCRC;

        /// <summary>True if block randomised.</summary>
        private bool blockRandomised;

        /// <summary>Buffer for bs data.</summary>
        private int bsBuff;

        /// <summary>The bs live.</summary>
        private int bsLive;

        /// <summary>The combined CRC.</summary>
        private uint combinedCRC;

        /// <summary>The current character.</summary>
        private int currentChar = -1;

        /// <summary>True to disposed.</summary>
        private bool disposed_;

        /// <summary>True to first attempt.</summary>
        private bool firstAttempt;

        /// <summary>The ftab.</summary>
        private int[] ftab;

        /// <summary>The last.</summary>
        private int last;

        /// <summary>The blocks randomised.</summary>
        private int nBlocksRandomised;

        /// <summary>The in use.</summary>
        private int nInUse;

        /// <summary>The mtf.</summary>
        private int nMTF;

        /// <summary>The original pointer.</summary>
        private int origPtr;

        /// <summary>The quadrant.</summary>
        private int[] quadrant;

        /// <summary>Length of the run.</summary>
        private int runLength;

        /// <summary>The szptr.</summary>
        private short[] szptr;

        /// <summary>The work done.</summary>
        private int workDone;

        /// <summary>The work limit.</summary>
        private int workLimit;

        /// <summary>The zptr.</summary>
        private int[] zptr;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream" />
        ///     class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public BZip2OutputStream(Stream stream)
            : this(stream, 9)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream" />
        ///     class.
        /// </summary>
        /// <param name="stream">   The stream.</param>
        /// <param name="blockSize">Size of the block.</param>
        public BZip2OutputStream(Stream stream, int blockSize)
        {
            this.BsSetStream(stream);
            this.workFactor = 50;
            if (blockSize > 9)
            {
                blockSize = 9;
            }

            if (blockSize < 1)
            {
                blockSize = 1;
            }

            this.blockSize100k = blockSize;
            this.AllocateCompressStructures();
            this.Initialize();
            this.InitBlock();
        }

        /// <summary>Finalizes an instance of the ICSharpCode.SharpZipLib.BZip2.BZip2OutputStream class.</summary>
        ~BZip2OutputStream()
        {
            this.Dispose(false);
        }

        /// <summary>Gets the bytes written.</summary>
        /// <value>The bytes written.</value>
        public int BytesWritten { get; private set; }

        /// <inheritdoc/>
        public override bool CanRead => false;

        /// <inheritdoc/>
        public override bool CanSeek => false;

        /// <inheritdoc/>
        public override bool CanWrite => this.baseStream.CanWrite;

        /// <summary>Gets or sets a value indicating whether this BZip2OutputStream is stream owner.</summary>
        /// <value>True if this BZip2OutputStream is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; } = true;

        /// <inheritdoc/>
        public override long Length => this.baseStream.Length;

        /// <inheritdoc/>
        public override long Position
        {
            get => this.baseStream.Position;
            set => throw new NotSupportedException("BZip2OutputStream position cannot be set");
        }

        /// <inheritdoc/>
        public override void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            this.baseStream.Flush();
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("BZip2OutputStream Read not supported");
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            throw new NotSupportedException("BZip2OutputStream ReadByte not supported");
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("BZip2OutputStream Seek not supported");
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("BZip2OutputStream SetLength not supported");
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (buffer.Length - offset < count)
            {
                throw new ArgumentException("Offset/count out of range");
            }

            for (var index = 0; index < count; ++index)
            {
                this.WriteByte(buffer[offset + index]);
            }
        }

        /// <inheritdoc/>
        public override void WriteByte(byte value)
        {
            var num = (256 + value) % 256;
            if (this.currentChar != -1)
            {
                if (this.currentChar == num)
                {
                    ++this.runLength;
                    if (this.runLength <= 254)
                    {
                        return;
                    }

                    this.WriteRun();
                    this.currentChar = -1;
                    this.runLength = 0;
                }
                else
                {
                    this.WriteRun();
                    this.runLength = 1;
                    this.currentChar = num;
                }
            }
            else
            {
                this.currentChar = num;
                ++this.runLength;
            }
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
                if (this.disposed_)
                {
                    return;
                }

                this.disposed_ = true;
                if (this.runLength > 0)
                {
                    this.WriteRun();
                }

                this.currentChar = -1;
                this.EndBlock();
                this.EndCompression();
                this.Flush();
            }
            finally
            {
                if (disposing && this.IsStreamOwner)
                {
                    this.baseStream.Close();
                }
            }
        }

        /// <summary>Hb assign codes.</summary>
        /// <param name="code">     The code.</param>
        /// <param name="length">   The length.</param>
        /// <param name="minLen">   The minimum length.</param>
        /// <param name="maxLen">   The maximum length.</param>
        /// <param name="alphaSize">Size of the alpha.</param>
        private static void HbAssignCodes(int[] code, char[] length, int minLen, int maxLen, int alphaSize)
        {
            var num = 0;
            for (var index1 = minLen; index1 <= maxLen; ++index1)
            {
                for (var index2 = 0; index2 < alphaSize; ++index2)
                {
                    if (length[index2] == index1)
                    {
                        code[index2] = num;
                        ++num;
                    }
                }

                num <<= 1;
            }
        }

        /// <summary>Hb make code lengths.</summary>
        /// <param name="len">      The length.</param>
        /// <param name="freq">     The frequency.</param>
        /// <param name="alphaSize">Size of the alpha.</param>
        /// <param name="maxLen">   The maximum length.</param>
        private static void HbMakeCodeLengths(char[] len, int[] freq, int alphaSize, int maxLen)
        {
            var numArray1 = new int[260];
            var numArray2 = new int[516];
            var numArray3 = new int[516];
            for (var index = 0; index < alphaSize; ++index)
            {
                numArray2[index + 1] = (freq[index] == 0 ? 1 : freq[index]) << 8;
            }

        label_3:
            var index1 = alphaSize;
            var index2 = 0;
            numArray1[0] = 0;
            numArray2[0] = 0;
            numArray3[0] = -2;
            for (var index3 = 1; index3 <= alphaSize; ++index3)
            {
                numArray3[index3] = -1;
                ++index2;
                numArray1[index2] = index3;
                var index4 = index2;
                int index5;
                for (index5 = numArray1[index4]; numArray2[index5] < numArray2[numArray1[index4 >> 1]]; index4 >>= 1)
                {
                    numArray1[index4] = numArray1[index4 >> 1];
                }

                numArray1[index4] = index5;
            }

            if (index2 >= 260)
            {
                Panic();
            }

            while (index2 > 1)
            {
                var index3 = numArray1[1];
                numArray1[1] = numArray1[index2];
                var index4 = index2 - 1;
                var index5 = 1;
                var index6 = numArray1[index5];
                while (true)
                {
                    var index7 = index5 << 1;
                    if (index7 <= index4)
                    {
                        if (index7 < index4 && numArray2[numArray1[index7 + 1]] < numArray2[numArray1[index7]])
                        {
                            ++index7;
                        }

                        if (numArray2[index6] >= numArray2[numArray1[index7]])
                        {
                            numArray1[index5] = numArray1[index7];
                            index5 = index7;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                numArray1[index5] = index6;
                var index8 = numArray1[1];
                numArray1[1] = numArray1[index4];
                var num2 = index4 - 1;
                var index9 = 1;
                var index10 = numArray1[index9];
                while (true)
                {
                    var index7 = index9 << 1;
                    if (index7 <= num2)
                    {
                        if (index7 < num2 && numArray2[numArray1[index7 + 1]] < numArray2[numArray1[index7]])
                        {
                            ++index7;
                        }

                        if (numArray2[index10] >= numArray2[numArray1[index7]])
                        {
                            numArray1[index9] = numArray1[index7];
                            index9 = index7;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                numArray1[index9] = index10;
                ++index1;
                numArray3[index3] = numArray3[index8] = index1;
                numArray2[index1] = (int)((numArray2[index3] & 4294967040L) + (numArray2[index8] & 4294967040L))
                                    | (1 + ((numArray2[index3] & (int)byte.MaxValue)
                                            > (numArray2[index8] & (int)byte.MaxValue)
                                                ? numArray2[index3] & byte.MaxValue
                                                : numArray2[index8] & byte.MaxValue));
                numArray3[index1] = -1;
                index2 = num2 + 1;
                numArray1[index2] = index1;
                var index11 = index2;
                int index12;
                for (index12 = numArray1[index11];
                     numArray2[index12] < numArray2[numArray1[index11 >> 1]];
                     index11 >>= 1)
                {
                    numArray1[index11] = numArray1[index11 >> 1];
                }

                numArray1[index11] = index12;
            }

            if (index1 >= 516)
            {
                Panic();
            }

            var flag = false;
            for (var index3 = 1; index3 <= alphaSize; ++index3)
            {
                var num = 0;
                var index4 = index3;
                while (numArray3[index4] >= 0)
                {
                    index4 = numArray3[index4];
                    ++num;
                }

                len[index3 - 1] = (char)num;
                if (num > maxLen)
                {
                    flag = true;
                }
            }

            if (!flag)
            {
                return;
            }

            for (var index3 = 1; index3 < alphaSize; ++index3)
            {
                var num = 1 + (numArray2[index3] >> 8) / 2;
                numArray2[index3] = num << 8;
            }

            goto label_3;
        }

        /// <summary>Median 3.</summary>
        /// <param name="a">The alpha component.</param>
        /// <param name="b">The byte value.</param>
        /// <param name="c">The character.</param>
        /// <returns>A byte.</returns>
        private static byte Med3(byte a, byte b, byte c)
        {
            if (a > b)
            {
                var num = a;
                a = b;
                b = num;
            }
            if (b > c)
            {
                var num = b;
                b = c;
                c = num;
            }
            if (a > b)
            {
                b = a;
            }
            return b;
        }

        /// <summary>Panics this BZip2OutputStream.</summary>
        private static void Panic()
        {
            throw new BZip2Exception("BZip2 output stream panic");
        }

        /// <summary>Allocate compress structures.</summary>
        private void AllocateCompressStructures()
        {
            var length = 100000 * this.blockSize100k;
            this.block = new byte[length + 1 + 20];
            this.quadrant = new int[length + 20];
            this.zptr = new int[length];
            this.ftab = new int[65537];
            if (this.block != null && this.quadrant != null && this.zptr != null)
            {
                var ftab = this.ftab;
            }
            this.szptr = new short[2 * length];
        }

        /// <summary>Bs finished with stream.</summary>
        private void BsFinishedWithStream()
        {
            while (this.bsLive > 0)
            {
                this.baseStream.WriteByte((byte)(this.bsBuff >> 24));
                this.bsBuff <<= 8;
                this.bsLive -= 8;
                ++this.BytesWritten;
            }
        }

        /// <summary>Bs putint.</summary>
        /// <param name="u">The int to process.</param>
        private void BsPutint(int u)
        {
            this.BsW(8, (u >> 24) & byte.MaxValue);
            this.BsW(8, (u >> 16) & byte.MaxValue);
            this.BsW(8, (u >> 8) & byte.MaxValue);
            this.BsW(8, u & byte.MaxValue);
        }

        /// <summary>Bs put int vs.</summary>
        /// <param name="numBits">Number of bits.</param>
        /// <param name="c">      The int to process.</param>
        private void BsPutIntVS(int numBits, int c)
        {
            this.BsW(numBits, c);
        }

        /// <summary>Bs put u character.</summary>
        /// <param name="c">The int to process.</param>
        private void BsPutUChar(int c)
        {
            this.BsW(8, c);
        }

        /// <summary>Bs set stream.</summary>
        /// <param name="stream">The stream.</param>
        private void BsSetStream(Stream stream)
        {
            this.baseStream = stream;
            this.bsLive = 0;
            this.bsBuff = 0;
            this.BytesWritten = 0;
        }

        /// <summary>Bs w.</summary>
        /// <param name="n">The int to process.</param>
        /// <param name="v">The int to process.</param>
        private void BsW(int n, int v)
        {
            while (this.bsLive >= 8)
            {
                this.baseStream.WriteByte((byte)(this.bsBuff >> 24));
                this.bsBuff <<= 8;
                this.bsLive -= 8;
                ++this.BytesWritten;
            }

            this.bsBuff |= v << (32 - this.bsLive - n);
            this.bsLive += n;
        }

        /// <summary>Executes the reversible transformation operation.</summary>
        private void DoReversibleTransformation()
        {
            this.workLimit = this.workFactor * this.last;
            this.workDone = 0;
            this.blockRandomised = false;
            this.firstAttempt = true;
            this.MainSort();
            if (this.workDone > this.workLimit && this.firstAttempt)
            {
                this.RandomiseBlock();
                this.workLimit = this.workDone = 0;
                this.blockRandomised = true;
                this.firstAttempt = false;
                this.MainSort();
            }

            this.origPtr = -1;
            for (var index = 0; index <= this.last; ++index)
            {
                if (this.zptr[index] == 0)
                {
                    this.origPtr = index;
                    break;
                }
            }

            if (this.origPtr != -1)
            {
                return;
            }

            Panic();
        }

        /// <summary>Ends a block.</summary>
        private void EndBlock()
        {
            if (this.last < 0)
            {
                return;
            }

            this.blockCRC = (uint)this.mCrc.Value;
            this.combinedCRC = (this.combinedCRC << 1) | (this.combinedCRC >> 31);
            this.combinedCRC ^= this.blockCRC;
            this.DoReversibleTransformation();
            this.BsPutUChar(49);
            this.BsPutUChar(65);
            this.BsPutUChar(89);
            this.BsPutUChar(38);
            this.BsPutUChar(83);
            this.BsPutUChar(89);
            this.BsPutint((int)this.blockCRC);
            if (this.blockRandomised)
            {
                this.BsW(1, 1);
                ++this.nBlocksRandomised;
            }
            else
            {
                this.BsW(1, 0);
            }

            this.MoveToFrontCodeAndSend();
        }

        /// <summary>Ends a compression.</summary>
        private void EndCompression()
        {
            this.BsPutUChar(23);
            this.BsPutUChar(114);
            this.BsPutUChar(69);
            this.BsPutUChar(56);
            this.BsPutUChar(80);
            this.BsPutUChar(144);
            this.BsPutint((int)this.combinedCRC);
            this.BsFinishedWithStream();
        }

        /// <summary>Full gt u.</summary>
        /// <param name="i1">Zero-based index of the 1.</param>
        /// <param name="i2">Zero-based index of the 2.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool FullGtU(int i1, int i2)
        {
            var num1 = this.block[i1 + 1];
            var num2 = this.block[i2 + 1];
            if (num1 != num2)
            {
                return num1 > num2;
            }

            ++i1;
            ++i2;
            var num3 = this.block[i1 + 1];
            var num4 = this.block[i2 + 1];
            if (num3 != num4)
            {
                return num3 > num4;
            }

            ++i1;
            ++i2;
            var num5 = this.block[i1 + 1];
            var num6 = this.block[i2 + 1];
            if (num5 != num6)
            {
                return num5 > num6;
            }

            ++i1;
            ++i2;
            var num7 = this.block[i1 + 1];
            var num8 = this.block[i2 + 1];
            if (num7 != num8)
            {
                return num7 > num8;
            }

            ++i1;
            ++i2;
            var num9 = this.block[i1 + 1];
            var num10 = this.block[i2 + 1];
            if (num9 != num10)
            {
                return num9 > num10;
            }

            ++i1;
            ++i2;
            var num11 = this.block[i1 + 1];
            var num12 = this.block[i2 + 1];
            if (num11 != num12)
            {
                return num11 > num12;
            }

            ++i1;
            ++i2;
            var num13 = this.last + 1;
            do
            {
                var num14 = this.block[i1 + 1];
                var num15 = this.block[i2 + 1];
                if (num14 != num15)
                {
                    return num14 > num15;
                }

                var num16 = this.quadrant[i1];
                var num17 = this.quadrant[i2];
                if (num16 != num17)
                {
                    return num16 > num17;
                }

                ++i1;
                ++i2;
                var num18 = this.block[i1 + 1];
                var num19 = this.block[i2 + 1];
                if (num18 != num19)
                {
                    return num18 > num19;
                }

                var num20 = this.quadrant[i1];
                var num21 = this.quadrant[i2];
                if (num20 != num21)
                {
                    return num20 > num21;
                }

                ++i1;
                ++i2;
                var num22 = this.block[i1 + 1];
                var num23 = this.block[i2 + 1];
                if (num22 != num23)
                {
                    return num22 > num23;
                }

                var num24 = this.quadrant[i1];
                var num25 = this.quadrant[i2];
                if (num24 != num25)
                {
                    return num24 > num25;
                }

                ++i1;
                ++i2;
                var num26 = this.block[i1 + 1];
                var num27 = this.block[i2 + 1];
                if (num26 != num27)
                {
                    return num26 > num27;
                }

                var num28 = this.quadrant[i1];
                var num29 = this.quadrant[i2];
                if (num28 != num29)
                {
                    return num28 > num29;
                }

                ++i1;
                ++i2;
                if (i1 > this.last)
                {
                    i1 -= this.last;
                    --i1;
                }

                if (i2 > this.last)
                {
                    i2 -= this.last;
                    --i2;
                }

                num13 -= 4;
                ++this.workDone;
            }
            while (num13 >= 0);

            return false;
        }

        /// <summary>Generates a mtf values.</summary>
        private void GenerateMTFValues()
        {
            var chArray = new char[256];
            this.MakeMaps();
            var index1 = this.nInUse + 1;
            for (var index2 = 0; index2 <= index1; ++index2)
            {
                this.mtfFreq[index2] = 0;
            }
            var index3 = 0;
            var num1 = 0;
            for (var index2 = 0; index2 < this.nInUse; ++index2)
            {
                chArray[index2] = (char)index2;
            }
            for (var index2 = 0; index2 <= this.last; ++index2)
            {
                var ch1 = this.unseqToSeq[this.block[this.zptr[index2]]];
                var index4 = 0;
                var ch2 = chArray[index4];
                while (ch1 != ch2)
                {
                    ++index4;
                    var ch3 = ch2;
                    ch2 = chArray[index4];
                    chArray[index4] = ch3;
                }
                chArray[0] = ch2;
                if (index4 == 0)
                {
                    ++num1;
                }
                else
                {
                    if (num1 > 0)
                    {
                        var num2 = num1 - 1;
                        while (true)
                        {
                            switch (num2 % 2)
                            {
                                case 0:
                                {
                                    this.szptr[index3] = 0;
                                    ++index3;
                                    ++this.mtfFreq[0];
                                    break;
                                }
                                case 1:
                                {
                                    this.szptr[index3] = 1;
                                    ++index3;
                                    ++this.mtfFreq[1];
                                    break;
                                }
                            }
                            if (num2 >= 2)
                            {
                                num2 = (num2 - 2) / 2;
                            }
                            else
                            {
                                break;
                            }
                        }
                        num1 = 0;
                    }
                    this.szptr[index3] = (short)(index4 + 1);
                    ++index3;
                    ++this.mtfFreq[index4 + 1];
                }
            }
            if (num1 > 0)
            {
                var num2 = num1 - 1;
                while (true)
                {
                    switch (num2 % 2)
                    {
                        case 0:
                        {
                            this.szptr[index3] = 0;
                            ++index3;
                            ++this.mtfFreq[0];
                            break;
                        }
                        case 1:
                        {
                            this.szptr[index3] = 1;
                            ++index3;
                            ++this.mtfFreq[1];
                            break;
                        }
                    }
                    if (num2 >= 2)
                    {
                        num2 = (num2 - 2) / 2;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            this.szptr[index3] = (short)index1;
            var num3 = index3 + 1;
            ++this.mtfFreq[index1];
            this.nMTF = num3;
        }

        /// <summary>Initialises the block.</summary>
        private void InitBlock()
        {
            this.mCrc.Reset();
            this.last = -1;
            for (var index = 0; index < 256; ++index)
            {
                this.inUse[index] = false;
            }
            this.allowableBlockSize = 100000 * this.blockSize100k - 20;
        }

        /// <summary>Initializes this BZip2OutputStream.</summary>
        private void Initialize()
        {
            this.BytesWritten = 0;
            this.nBlocksRandomised = 0;
            this.BsPutUChar(66);
            this.BsPutUChar(90);
            this.BsPutUChar(104);
            this.BsPutUChar(48 + this.blockSize100k);
            this.combinedCRC = 0U;
        }

        /// <summary>Main sort.</summary>
        private void MainSort()
        {
            var numArray1 = new int[256];
            var numArray2 = new int[256];
            var flagArray = new bool[256];
            for (var index = 0; index < 20; ++index)
            {
                this.block[this.last + index + 2] = this.block[index % (this.last + 1) + 1];
            }

            for (var index = 0; index <= this.last + 20; ++index)
            {
                this.quadrant[index] = 0;
            }

            this.block[0] = this.block[this.last + 1];
            if (this.last < 4000)
            {
                for (var index = 0; index <= this.last; ++index)
                {
                    this.zptr[index] = index;
                }

                this.firstAttempt = false;
                this.workDone = this.workLimit = 0;
                this.SimpleSort(0, this.last, 0);
            }
            else
            {
                var num1 = 0;
                for (var index = 0; index <= (int)byte.MaxValue; ++index)
                {
                    flagArray[index] = false;
                }

                for (var index = 0; index <= 65536; ++index)
                {
                    this.ftab[index] = 0;
                }

                int num2 = this.block[0];
                for (var index = 0; index <= this.last; ++index)
                {
                    int num3 = this.block[index + 1];
                    ++this.ftab[(num2 << 8) + num3];
                    num2 = num3;
                }

                for (var index = 1; index <= 65536; ++index)
                {
                    this.ftab[index] += this.ftab[index - 1];
                }

                int num4 = this.block[1];
                for (var index1 = 0; index1 < this.last; ++index1)
                {
                    int num3 = this.block[index1 + 2];
                    var index2 = (num4 << 8) + num3;
                    num4 = num3;
                    --this.ftab[index2];
                    this.zptr[this.ftab[index2]] = index1;
                }

                var index3 = (this.block[this.last + 1] << 8) + this.block[1];
                --this.ftab[index3];
                this.zptr[this.ftab[index3]] = this.last;
                for (var index1 = 0; index1 <= (int)byte.MaxValue; ++index1)
                {
                    numArray1[index1] = index1;
                }

                var num5 = 1;
                do
                {
                    num5 = 3 * num5 + 1;
                }
                while (num5 <= 256);

                do
                {
                    num5 /= 3;
                    for (var index1 = num5; index1 <= (int)byte.MaxValue; ++index1)
                    {
                        var num3 = numArray1[index1];
                        var index2 = index1;
                        while (this.ftab[(numArray1[index2 - num5] + 1) << 8] - this.ftab[numArray1[index2 - num5] << 8]
                               > this.ftab[(num3 + 1) << 8] - this.ftab[num3 << 8])
                        {
                            numArray1[index2] = numArray1[index2 - num5];
                            index2 -= num5;
                            if (index2 <= num5 - 1)
                            {
                                break;
                            }
                        }

                        numArray1[index2] = num3;
                    }
                }
                while (num5 != 1);

                for (var index1 = 0; index1 <= (int)byte.MaxValue; ++index1)
                {
                    var index2 = numArray1[index1];
                    for (var index4 = 0; index4 <= (int)byte.MaxValue; ++index4)
                    {
                        var index5 = (index2 << 8) + index4;
                        if ((this.ftab[index5] & 2097152) != 2097152)
                        {
                            var loSt = this.ftab[index5] & -2097153;
                            var hiSt = (this.ftab[index5 + 1] & -2097153) - 1;
                            if (hiSt > loSt)
                            {
                                this.QSort3(loSt, hiSt, 2);
                                num1 += hiSt - loSt + 1;
                                if (this.workDone > this.workLimit && this.firstAttempt)
                                {
                                    return;
                                }
                            }

                            this.ftab[index5] |= 2097152;
                        }
                    }

                    flagArray[index2] = true;
                    if (index1 < byte.MaxValue)
                    {
                        var num3 = this.ftab[index2 << 8] & -2097153;
                        var num6 = (this.ftab[(index2 + 1) << 8] & -2097153) - num3;
                        var num7 = 0;
                        while (num6 >> num7 > 65534)
                        {
                            ++num7;
                        }

                        for (var index4 = 0; index4 < num6; ++index4)
                        {
                            var index5 = this.zptr[num3 + index4];
                            var num8 = index4 >> num7;
                            this.quadrant[index5] = num8;
                            if (index5 < 20)
                            {
                                this.quadrant[index5 + this.last + 1] = num8;
                            }
                        }

                        if ((num6 - 1) >> num7 > ushort.MaxValue)
                        {
                            Panic();
                        }
                    }

                    for (var index4 = 0; index4 <= (int)byte.MaxValue; ++index4)
                    {
                        numArray2[index4] = this.ftab[(index4 << 8) + index2] & -2097153;
                    }

                    for (var index4 = this.ftab[index2 << 8] & -2097153;
                         index4 < (this.ftab[(index2 + 1) << 8] & -2097153);
                         ++index4)
                    {
                        int index5 = this.block[this.zptr[index4]];
                        if (!flagArray[index5])
                        {
                            this.zptr[numArray2[index5]] = this.zptr[index4] == 0 ? this.last : this.zptr[index4] - 1;
                            ++numArray2[index5];
                        }
                    }

                    for (var index4 = 0; index4 <= (int)byte.MaxValue; ++index4)
                    {
                        this.ftab[(index4 << 8) + index2] |= 2097152;
                    }
                }
            }
        }

        /// <summary>Makes the maps.</summary>
        private void MakeMaps()
        {
            this.nInUse = 0;
            for (var index = 0; index < 256; ++index)
            {
                if (this.inUse[index])
                {
                    this.seqToUnseq[this.nInUse] = (char)index;
                    this.unseqToSeq[index] = (char)this.nInUse;
                    ++this.nInUse;
                }
            }
        }

        /// <summary>Move to front code and send.</summary>
        private void MoveToFrontCodeAndSend()
        {
            this.BsPutIntVS(24, this.origPtr);
            this.GenerateMTFValues();
            this.SendMTFValues();
        }

        /// <summary>Sort 3.</summary>
        /// <param name="loSt">The lower st.</param>
        /// <param name="hiSt">The higher st.</param>
        /// <param name="dSt"> The st.</param>
        private void QSort3(int loSt, int hiSt, int dSt)
        {
            var stackElementArray = new StackElement[1000];
            var index1 = 0;
            stackElementArray[index1].ll = loSt;
            stackElementArray[index1].hh = hiSt;
            stackElementArray[index1].dd = dSt;
            var index2 = index1 + 1;
            while (index2 > 0)
            {
                if (index2 >= 1000)
                {
                    Panic();
                }

                --index2;
                var ll = stackElementArray[index2].ll;
                var hh = stackElementArray[index2].hh;
                var dd = stackElementArray[index2].dd;
                if (hh - ll < 20 || dd > 10)
                {
                    this.SimpleSort(ll, hh, dd);
                    if (this.workDone > this.workLimit && this.firstAttempt)
                    {
                        break;
                    }
                }
                else
                {
                    int num1 = Med3(
                        this.block[this.zptr[ll] + dd + 1],
                        this.block[this.zptr[hh] + dd + 1],
                        this.block[this.zptr[(ll + hh) >> 1] + dd + 1]);
                    int index3;
                    var p1 = index3 = ll;
                    int index4;
                    var index5 = index4 = hh;
                    while (true)
                    {
                        while (p1 <= index5)
                        {
                            var num2 = this.block[this.zptr[p1] + dd + 1] - num1;
                            if (num2 == 0)
                            {
                                var num3 = this.zptr[p1];
                                this.zptr[p1] = this.zptr[index3];
                                this.zptr[index3] = num3;
                                ++index3;
                                ++p1;
                            }
                            else if (num2 <= 0)
                            {
                                ++p1;
                            }
                            else
                            {
                                break;
                            }
                        }

                        while (p1 <= index5)
                        {
                            var num2 = this.block[this.zptr[index5] + dd + 1] - num1;
                            if (num2 == 0)
                            {
                                var num3 = this.zptr[index5];
                                this.zptr[index5] = this.zptr[index4];
                                this.zptr[index4] = num3;
                                --index4;
                                --index5;
                            }
                            else if (num2 >= 0)
                            {
                                --index5;
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (p1 <= index5)
                        {
                            var num2 = this.zptr[p1];
                            this.zptr[p1] = this.zptr[index5];
                            this.zptr[index5] = num2;
                            ++p1;
                            --index5;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (index4 < index3)
                    {
                        stackElementArray[index2].ll = ll;
                        stackElementArray[index2].hh = hh;
                        stackElementArray[index2].dd = dd + 1;
                        ++index2;
                    }
                    else
                    {
                        var n1 = index3 - ll < p1 - index3 ? index3 - ll : p1 - index3;
                        this.Vswap(ll, p1 - n1, n1);
                        var n2 = hh - index4 < index4 - index5 ? hh - index4 : index4 - index5;
                        this.Vswap(p1, hh - n2 + 1, n2);
                        var num2 = ll + p1 - index3 - 1;
                        var num3 = hh - (index4 - index5) + 1;
                        stackElementArray[index2].ll = ll;
                        stackElementArray[index2].hh = num2;
                        stackElementArray[index2].dd = dd;
                        var index6 = index2 + 1;
                        stackElementArray[index6].ll = num2 + 1;
                        stackElementArray[index6].hh = num3 - 1;
                        stackElementArray[index6].dd = dd + 1;
                        var index7 = index6 + 1;
                        stackElementArray[index7].ll = num3;
                        stackElementArray[index7].hh = hh;
                        stackElementArray[index7].dd = dd;
                        index2 = index7 + 1;
                    }
                }
            }
        }

        /// <summary>Randomise block.</summary>
        private void RandomiseBlock()
        {
            var num = 0;
            var index1 = 0;
            for (var index2 = 0; index2 < 256; ++index2)
            {
                this.inUse[index2] = false;
            }

            for (var index2 = 0; index2 <= this.last; ++index2)
            {
                if (num == 0)
                {
                    num = BZip2Constants.RandomNumbers[index1];
                    ++index1;
                    if (index1 == 512)
                    {
                        index1 = 0;
                    }
                }

                --num;
                this.block[index2 + 1] ^= num == 1 ? (byte)1 : (byte)0;
                this.block[index2 + 1] &= byte.MaxValue;
                this.inUse[this.block[index2 + 1]] = true;
            }
        }

        /// <summary>Sends the mtf values.</summary>
        private void SendMTFValues()
        {
            var chArray1 = new char[6][];
            for (var index = 0; index < 6; ++index)
            {
                chArray1[index] = new char[258];
            }

            var v1 = 0;
            var alphaSize = this.nInUse + 2;
            for (var index1 = 0; index1 < 6; ++index1)
            {
                for (var index2 = 0; index2 < alphaSize; ++index2)
                {
                    chArray1[index1][index2] = '\x000F';
                }
            }

            if (this.nMTF <= 0)
            {
                Panic();
            }

            var v2 = this.nMTF >= 200 ? this.nMTF >= 600 ? this.nMTF >= 1200 ? this.nMTF >= 2400 ? 6 : 5 : 4 : 3 : 2;
            var num1 = v2;
            var nMtf = this.nMTF;
            var num2 = 0;
            while (num1 > 0)
            {
                var num3 = nMtf / num1;
                var num4 = 0;
                int index1;
                for (index1 = num2 - 1; num4 < num3 && index1 < alphaSize - 1; num4 += this.mtfFreq[index1])
                {
                    ++index1;
                }

                if (index1 > num2 && num1 != v2 && num1 != 1 && (v2 - num1) % 2 == 1)
                {
                    num4 -= this.mtfFreq[index1];
                    --index1;
                }

                for (var index2 = 0; index2 < alphaSize; ++index2)
                {
                    chArray1[num1 - 1][index2] = index2 < num2 || index2 > index1 ? '\x000F' : char.MinValue;
                }

                --num1;
                num2 = index1 + 1;
                nMtf -= num4;
            }

            var numArray1 = new int[6][];
            for (var index = 0; index < 6; ++index)
            {
                numArray1[index] = new int[258];
            }

            var numArray2 = new int[6];
            var numArray3 = new short[6];
            for (var index1 = 0; index1 < 4; ++index1)
            {
                for (var index2 = 0; index2 < v2; ++index2)
                {
                    numArray2[index2] = 0;
                }

                for (var index2 = 0; index2 < v2; ++index2)
                {
                    for (var index3 = 0; index3 < alphaSize; ++index3)
                    {
                        numArray1[index2][index3] = 0;
                    }
                }

                v1 = 0;
                var num3 = 0;
                int num4;
                for (var index2 = 0; index2 < this.nMTF; index2 = num4 + 1)
                {
                    num4 = index2 + 50 - 1;
                    if (num4 >= this.nMTF)
                    {
                        num4 = this.nMTF - 1;
                    }

                    for (var index3 = 0; index3 < v2; ++index3)
                    {
                        numArray3[index3] = 0;
                    }

                    if (v2 == 6)
                    {
                        int num5;
                        var num6 = (short)(num5 = 0);
                        var num7 = (short)num5;
                        var num8 = (short)num5;
                        var num9 = (short)num5;
                        var num10 = (short)num5;
                        var num11 = (short)num5;
                        for (var index3 = index2; index3 <= num4; ++index3)
                        {
                            var num12 = this.szptr[index3];
                            num11 += (short)chArray1[0][num12];
                            num10 += (short)chArray1[1][num12];
                            num9 += (short)chArray1[2][num12];
                            num8 += (short)chArray1[3][num12];
                            num7 += (short)chArray1[4][num12];
                            num6 += (short)chArray1[5][num12];
                        }

                        numArray3[0] = num11;
                        numArray3[1] = num10;
                        numArray3[2] = num9;
                        numArray3[3] = num8;
                        numArray3[4] = num7;
                        numArray3[5] = num6;
                    }
                    else
                    {
                        for (var index3 = index2; index3 <= num4; ++index3)
                        {
                            var num5 = this.szptr[index3];
                            for (var index4 = 0; index4 < v2; ++index4)
                            {
                                numArray3[index4] += (short)chArray1[index4][num5];
                            }
                        }
                    }

                    var num13 = 999999999;
                    var index5 = -1;
                    for (var index3 = 0; index3 < v2; ++index3)
                    {
                        if (numArray3[index3] < num13)
                        {
                            num13 = numArray3[index3];
                            index5 = index3;
                        }
                    }

                    num3 += num13;
                    ++numArray2[index5];
                    this.selector[v1] = (char)index5;
                    ++v1;
                    for (var index3 = index2; index3 <= num4; ++index3)
                    {
                        ++numArray1[index5][this.szptr[index3]];
                    }
                }

                for (var index2 = 0; index2 < v2; ++index2)
                {
                    HbMakeCodeLengths(chArray1[index2], numArray1[index2], alphaSize, 20);
                }
            }

            if (v2 >= 8)
            {
                Panic();
            }

            if (v1 >= 32768 || v1 > 18002)
            {
                Panic();
            }

            var chArray2 = new char[6];
            for (var index = 0; index < v2; ++index)
            {
                chArray2[index] = (char)index;
            }

            for (var index1 = 0; index1 < v1; ++index1)
            {
                var ch1 = this.selector[index1];
                var index2 = 0;
                var ch2 = chArray2[index2];
                while (ch1 != ch2)
                {
                    ++index2;
                    var ch3 = ch2;
                    ch2 = chArray2[index2];
                    chArray2[index2] = ch3;
                }

                chArray2[0] = ch2;
                this.selectorMtf[index1] = (char)index2;
            }

            var numArray4 = new int[6][];
            for (var index = 0; index < 6; ++index)
            {
                numArray4[index] = new int[258];
            }

            for (var index1 = 0; index1 < v2; ++index1)
            {
                var minLen = 32;
                var maxLen = 0;
                for (var index2 = 0; index2 < alphaSize; ++index2)
                {
                    if (chArray1[index1][index2] > maxLen)
                    {
                        maxLen = chArray1[index1][index2];
                    }

                    if (chArray1[index1][index2] < minLen)
                    {
                        minLen = chArray1[index1][index2];
                    }
                }

                if (maxLen > 20)
                {
                    Panic();
                }

                if (minLen < 1)
                {
                    Panic();
                }

                HbAssignCodes(numArray4[index1], chArray1[index1], minLen, maxLen, alphaSize);
            }

            var flagArray = new bool[16];
            for (var index1 = 0; index1 < 16; ++index1)
            {
                flagArray[index1] = false;
                for (var index2 = 0; index2 < 16; ++index2)
                {
                    if (this.inUse[index1 * 16 + index2])
                    {
                        flagArray[index1] = true;
                    }
                }
            }

            for (var index = 0; index < 16; ++index)
            {
                if (flagArray[index])
                {
                    this.BsW(1, 1);
                }
                else
                {
                    this.BsW(1, 0);
                }
            }

            for (var index1 = 0; index1 < 16; ++index1)
            {
                if (flagArray[index1])
                {
                    for (var index2 = 0; index2 < 16; ++index2)
                    {
                        if (this.inUse[index1 * 16 + index2])
                        {
                            this.BsW(1, 1);
                        }
                        else
                        {
                            this.BsW(1, 0);
                        }
                    }
                }
            }

            this.BsW(3, v2);
            this.BsW(15, v1);
            for (var index1 = 0; index1 < v1; ++index1)
            {
                for (var index2 = 0; index2 < (int)this.selectorMtf[index1]; ++index2)
                {
                    this.BsW(1, 1);
                }

                this.BsW(1, 0);
            }

            for (var index1 = 0; index1 < v2; ++index1)
            {
                int v3 = chArray1[index1][0];
                this.BsW(5, v3);
                for (var index2 = 0; index2 < alphaSize; ++index2)
                {
                    for (; v3 < (int)chArray1[index1][index2]; ++v3)
                    {
                        this.BsW(2, 2);
                    }

                    for (; v3 > (int)chArray1[index1][index2]; --v3)
                    {
                        this.BsW(2, 3);
                    }

                    this.BsW(1, 0);
                }
            }

            var index6 = 0;
            var num14 = 0;
            while (num14 < this.nMTF)
            {
                var num3 = num14 + 50 - 1;
                if (num3 >= this.nMTF)
                {
                    num3 = this.nMTF - 1;
                }

                for (var index1 = num14; index1 <= num3; ++index1)
                {
                    this.BsW(
                        chArray1[this.selector[index6]][this.szptr[index1]],
                        numArray4[this.selector[index6]][this.szptr[index1]]);
                }

                num14 = num3 + 1;
                ++index6;
            }

            if (index6 == v1)
            {
                return;
            }

            Panic();
        }

        /// <summary>Simple sort.</summary>
        /// <param name="lo">The lower.</param>
        /// <param name="hi">The higher.</param>
        /// <param name="d"> The int to process.</param>
        private void SimpleSort(int lo, int hi, int d)
        {
            var num1 = hi - lo + 1;
            if (num1 < 2)
            {
                return;
            }

            var index1 = 0;
            while (this.increments[index1] < num1)
            {
                ++index1;
            }

            for (var index2 = index1 - 1; index2 >= 0; --index2)
            {
                var increment = this.increments[index2];
                var index3 = lo + increment;
                while (index3 <= hi)
                {
                    var num2 = this.zptr[index3];
                    var index4 = index3;
                    while (this.FullGtU(this.zptr[index4 - increment] + d, num2 + d))
                    {
                        this.zptr[index4] = this.zptr[index4 - increment];
                        index4 -= increment;
                        if (index4 <= lo + increment - 1)
                        {
                            break;
                        }
                    }

                    this.zptr[index4] = num2;
                    var index5 = index3 + 1;
                    if (index5 <= hi)
                    {
                        var num3 = this.zptr[index5];
                        var index6 = index5;
                        while (this.FullGtU(this.zptr[index6 - increment] + d, num3 + d))
                        {
                            this.zptr[index6] = this.zptr[index6 - increment];
                            index6 -= increment;
                            if (index6 <= lo + increment - 1)
                            {
                                break;
                            }
                        }

                        this.zptr[index6] = num3;
                        var index7 = index5 + 1;
                        if (index7 <= hi)
                        {
                            var num4 = this.zptr[index7];
                            var index8 = index7;
                            while (this.FullGtU(this.zptr[index8 - increment] + d, num4 + d))
                            {
                                this.zptr[index8] = this.zptr[index8 - increment];
                                index8 -= increment;
                                if (index8 <= lo + increment - 1)
                                {
                                    break;
                                }
                            }

                            this.zptr[index8] = num4;
                            index3 = index7 + 1;
                            if (this.workDone > this.workLimit && this.firstAttempt)
                            {
                                return;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>Vswaps.</summary>
        /// <param name="p1">The first int.</param>
        /// <param name="p2">The second int.</param>
        /// <param name="n"> The int to process.</param>
        private void Vswap(int p1, int p2, int n)
        {
            for (; n > 0; --n)
            {
                var num = this.zptr[p1];
                this.zptr[p1] = this.zptr[p2];
                this.zptr[p2] = num;
                ++p1;
                ++p2;
            }
        }

        /// <summary>Writes the run.</summary>
        private void WriteRun()
        {
            if (this.last >= this.allowableBlockSize)
            {
                this.EndBlock();
                this.InitBlock();
                this.WriteRun();
                return;
            }
            this.inUse[this.currentChar] = true;
            for (var index = 0; index < this.runLength; ++index)
            {
                this.mCrc.Update(this.currentChar);
            }
            switch (this.runLength)
            {
                case 1:
                {
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    break;
                }
                case 2:
                {
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    break;
                }
                case 3:
                {
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    break;
                }
                default:
                {
                    this.inUse[this.runLength - 4] = true;
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    ++this.last;
                    this.block[this.last + 1] = (byte)this.currentChar;
                    ++this.last;
                    this.block[this.last + 1] = (byte)(this.runLength - 4);
                    break;
                }
            }
        }

        /// <summary>A stack element.</summary>
        /// <seealso cref="System.IO.Stream" />
        private struct StackElement
        {
            /// <summary>The ll.</summary>
            public int ll;

            /// <summary>The hh.</summary>
            public int hh;

            /// <summary>The dd.</summary>
            public int dd;
        }
    }
}