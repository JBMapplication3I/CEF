// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2InputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.BZip2
{
    using System;
    using System.IO;

    using ICSharpCode.SharpZipLib.Checksums;

    /// <summary>A zip 2 input stream.</summary>
    /// <seealso cref="Stream" />
    public class BZip2InputStream : Stream
    {
        /// <summary>State of the no random part a.</summary>
        private const int NO_RAND_PART_A_STATE = 5;

        /// <summary>State of the no random part b.</summary>
        private const int NO_RAND_PART_B_STATE = 6;

        /// <summary>State of the no random part c.</summary>
        private const int NO_RAND_PART_C_STATE = 7;

        /// <summary>State of the random part a.</summary>
        private const int RAND_PART_A_STATE = 2;

        /// <summary>State of the random part b.</summary>
        private const int RAND_PART_B_STATE = 3;

        /// <summary>State of the random part c.</summary>
        private const int RAND_PART_C_STATE = 4;

        /// <summary>The start block state.</summary>
        private const int START_BLOCK_STATE = 1;

        /// <summary>Array of bases.</summary>
        private readonly int[][] baseArray = new int[6][];

        /// <summary>The in use.</summary>
        private readonly bool[] inUse = new bool[256];

        /// <summary>The limit.</summary>
        private readonly int[][] limit = new int[6][];

        /// <summary>The CRC.</summary>
        private readonly IChecksum mCrc = new StrangeCRC();

        /// <summary>The minimum lens.</summary>
        private readonly int[] minLens = new int[6];

        /// <summary>The permission.</summary>
        private readonly int[][] perm = new int[6][];

        /// <summary>The selector.</summary>
        private readonly byte[] selector = new byte[18002];

        /// <summary>The selector mtf.</summary>
        private readonly byte[] selectorMtf = new byte[18002];

        /// <summary>The sequence to unseq.</summary>
        private readonly byte[] seqToUnseq = new byte[256];

        /// <summary>The unseq to sequence.</summary>
        private readonly byte[] unseqToSeq = new byte[256];

        /// <summary>The unzftab.</summary>
        private readonly int[] unzftab = new int[256];

        /// <summary>The base stream.</summary>
        private Stream baseStream;

        /// <summary>True if block randomised.</summary>
        private bool blockRandomised;

        /// <summary>The block size 100k.</summary>
        private int blockSize100k;

        /// <summary>Buffer for bs data.</summary>
        private int bsBuff;

        /// <summary>The bs live.</summary>
        private int bsLive;

        /// <summary>The second ch.</summary>
        private int ch2;

        /// <summary>The previous.</summary>
        private int chPrev;

        /// <summary>The computed block CRC.</summary>
        private int computedBlockCRC;

        /// <summary>The computed combined CRC.</summary>
        private uint computedCombinedCRC;

        /// <summary>Number of.</summary>
        private int count;

        /// <summary>The current character.</summary>
        private int currentChar = -1;

        /// <summary>The current state.</summary>
        private int currentState = 1;

        /// <summary>Zero-based index of the 2.</summary>
        private int i2;

        /// <summary>The second j.</summary>
        private int j2;

        /// <summary>The last.</summary>
        private int last;

        /// <summary>The ll 8.</summary>
        private byte[] ll8;

        /// <summary>The in use.</summary>
        private int nInUse;

        /// <summary>The original pointer.</summary>
        private int origPtr;

        /// <summary>The n to go.</summary>
        private int rNToGo;

        /// <summary>The t position.</summary>
        private int rTPos;

        /// <summary>The stored block CRC.</summary>
        private int storedBlockCRC;

        /// <summary>The stored combined CRC.</summary>
        private int storedCombinedCRC;

        /// <summary>True to stream end.</summary>
        private bool streamEnd;

        /// <summary>The position.</summary>
        private int tPos;

        /// <summary>The tt.</summary>
        private int[] tt;

        /// <summary>The z coordinate.</summary>
        private byte z;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.BZip2.BZip2InputStream" />
        ///     class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public BZip2InputStream(Stream stream)
        {
            for (var i = 0; i < 6; i++)
            {
                this.limit[i] = new int[258];
                this.baseArray[i] = new int[258];
                this.perm[i] = new int[258];
            }

            this.BsSetStream(stream);
            this.Initialize();
            this.InitBlock();
            this.SetupBlock();
        }

        /// <inheritdoc/>
        public override bool CanRead => this.baseStream.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => this.baseStream.CanSeek;

        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <summary>Gets or sets a value indicating whether this BZip2InputStream is stream owner.</summary>
        /// <value>True if this BZip2InputStream is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; } = true;

        /// <inheritdoc/>
        public override long Length => this.baseStream.Length;

        /// <inheritdoc/>
        public override long Position
        {
            get => this.baseStream.Position;
            set => throw new NotSupportedException("BZip2InputStream position cannot be set");
        }

        /// <inheritdoc/>
        public override void Close()
        {
            if (this.IsStreamOwner)
            {
                this.baseStream?.Close();
            }
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            this.baseStream?.Flush();
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            for (var i = 0; i < count; i++)
            {
                var num = this.ReadByte();
                if (num == -1)
                {
                    return i;
                }

                buffer[offset + i] = (byte)num;
            }

            return count;
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            if (this.streamEnd)
            {
                return -1;
            }

            var currentChar = this.currentChar;
            switch (this.currentState)
            {
                case 3:
                {
                    this.SetupRandPartB();
                    break;
                }
                case 4:
                {
                    this.SetupRandPartC();
                    break;
                }
                case 6:
                {
                    this.SetupNoRandPartB();
                    break;
                }
                case 7:
                {
                    this.SetupNoRandPartC();
                    break;
                }
            }

            return currentChar;
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("BZip2InputStream Seek not supported");
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("BZip2InputStream SetLength not supported");
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("BZip2InputStream Write not supported");
        }

        /// <inheritdoc/>
        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("BZip2InputStream WriteByte not supported");
        }

        /// <summary>Bad block header.</summary>
        private static void BadBlockHeader()
        {
            throw new BZip2Exception("BZip2 input stream bad block header");
        }

        /// <summary>Block overrun.</summary>
        private static void BlockOverrun()
        {
            throw new BZip2Exception("BZip2 input stream block overrun");
        }

        /// <summary>Compressed stream EOF.</summary>
        private static void CompressedStreamEOF()
        {
            throw new EndOfStreamException("BZip2 input stream end of compressed stream");
        }

        /// <summary>CRC error.</summary>
        private static void CrcError()
        {
            throw new BZip2Exception("BZip2 input stream crc error");
        }

        /// <summary>Hb create decode tables.</summary>
        /// <param name="limit">    The limit.</param>
        /// <param name="baseArray">Array of bases.</param>
        /// <param name="perm">     The permission.</param>
        /// <param name="length">   The length.</param>
        /// <param name="minLen">   The minimum length.</param>
        /// <param name="maxLen">   The maximum length.</param>
        /// <param name="alphaSize">Size of the alpha.</param>
        private static void HbCreateDecodeTables(
            int[] limit,
            int[] baseArray,
            int[] perm,
            char[] length,
            int minLen,
            int maxLen,
            int alphaSize)
        {
            var index1 = 0;
            for (var index2 = minLen; index2 <= maxLen; ++index2)
            {
                for (var index3 = 0; index3 < alphaSize; ++index3)
                {
                    if (length[index3] == index2)
                    {
                        perm[index1] = index3;
                        ++index1;
                    }
                }
            }

            for (var index2 = 0; index2 < 23; ++index2)
            {
                baseArray[index2] = 0;
            }

            for (var index2 = 0; index2 < alphaSize; ++index2)
            {
                ++baseArray[length[index2] + 1];
            }

            for (var index2 = 1; index2 < 23; ++index2)
            {
                baseArray[index2] += baseArray[index2 - 1];
            }

            for (var index2 = 0; index2 < 23; ++index2)
            {
                limit[index2] = 0;
            }

            var num1 = 0;
            for (var index2 = minLen; index2 <= maxLen; ++index2)
            {
                var num2 = num1 + (baseArray[index2 + 1] - baseArray[index2]);
                limit[index2] = num2 - 1;
                num1 = num2 << 1;
            }

            for (var index2 = minLen + 1; index2 <= maxLen; ++index2)
            {
                baseArray[index2] = ((limit[index2 - 1] + 1) << 1) - baseArray[index2];
            }
        }

        /// <summary>Bs get int 32.</summary>
        /// <returns>An int.</returns>
        private int BsGetInt32()
        {
            return (((((this.BsR(8) << 8) | this.BsR(8)) << 8) | this.BsR(8)) << 8) | this.BsR(8);
        }

        /// <summary>Bs get int vs.</summary>
        /// <param name="numBits">Number of bits.</param>
        /// <returns>An int.</returns>
        private int BsGetIntVS(int numBits)
        {
            return this.BsR(numBits);
        }

        /// <summary>Bs get u character.</summary>
        /// <returns>A char.</returns>
        private char BsGetUChar()
        {
            return (char)this.BsR(8);
        }

        /// <summary>Bs r.</summary>
        /// <param name="n">The int to process.</param>
        /// <returns>An int.</returns>
        private int BsR(int n)
        {
            while (this.bsLive < n)
            {
                this.FillBuffer();
            }

            var num = (this.bsBuff >> (this.bsLive - n)) & ((1 << n) - 1);
            this.bsLive -= n;
            return num;
        }

        /// <summary>Bs set stream.</summary>
        /// <param name="stream">The stream.</param>
        private void BsSetStream(Stream stream)
        {
            this.baseStream = stream;
            this.bsLive = 0;
            this.bsBuff = 0;
        }

        /// <summary>Completes this BZip2InputStream.</summary>
        private void Complete()
        {
            this.storedCombinedCRC = this.BsGetInt32();
            if (this.storedCombinedCRC != (int)this.computedCombinedCRC)
            {
                CrcError();
            }

            this.streamEnd = true;
        }

        /// <summary>Ends a block.</summary>
        private void EndBlock()
        {
            this.computedBlockCRC = (int)this.mCrc.Value;
            if (this.storedBlockCRC != this.computedBlockCRC)
            {
                CrcError();
            }

            this.computedCombinedCRC =
                (uint)(((int)this.computedCombinedCRC << 1) & -1) | (this.computedCombinedCRC >> 31);
            this.computedCombinedCRC ^= (uint)this.computedBlockCRC;
        }

        /// <summary>Fill buffer.</summary>
        private void FillBuffer()
        {
            var num = 0;
            try
            {
                num = this.baseStream.ReadByte();
            }
            catch (Exception)
            {
                CompressedStreamEOF();
            }

            if (num == -1)
            {
                CompressedStreamEOF();
            }

            this.bsBuff = (this.bsBuff << 8) | (num & byte.MaxValue);
            this.bsLive += 8;
        }

        /// <summary>Gets and move to front decode.</summary>
        private void GetAndMoveToFrontDecode()
        {
            var numArray = new byte[256];
            var num1 = 100000 * this.blockSize100k;
            this.origPtr = this.BsGetIntVS(24);
            this.RecvDecodingTables();
            var num2 = this.nInUse + 1;
            var index1 = -1;
            var num3 = 0;
            for (var index2 = 0; index2 <= (int)byte.MaxValue; ++index2)
            {
                this.unzftab[index2] = 0;
            }

            for (var index2 = 0; index2 <= (int)byte.MaxValue; ++index2)
            {
                numArray[index2] = (byte)index2;
            }

            this.last = -1;
            if (num3 == 0)
            {
                ++index1;
                num3 = 50;
            }

            var num4 = num3 - 1;
            int index3 = this.selector[index1];
            var minLen1 = this.minLens[index3];
            int num5;
            int num6;
            for (num5 = this.BsR(minLen1); num5 > this.limit[index3][minLen1]; num5 = (num5 << 1) | num6)
            {
                if (minLen1 > 20)
                {
                    throw new BZip2Exception("Bzip data error");
                }

                ++minLen1;
                while (this.bsLive < 1)
                {
                    this.FillBuffer();
                }

                num6 = (this.bsBuff >> (this.bsLive - 1)) & 1;
                --this.bsLive;
            }

            if (num5 - this.baseArray[index3][minLen1] < 0 || num5 - this.baseArray[index3][minLen1] >= 258)
            {
                throw new BZip2Exception("Bzip data error");
            }

            var num7 = this.perm[index3][num5 - this.baseArray[index3][minLen1]];
            while (num7 != num2)
            {
                if (num7 == 0 || num7 == 1)
                {
                    var num8 = -1;
                    var num9 = 1;
                label_22:
                    if (num7 == 0)
                    {
                        num8 += num9;
                    }
                    else if (num7 == 1)
                    {
                        num8 += 2 * num9;
                    }

                    num9 <<= 1;
                    if (num4 == 0)
                    {
                        ++index1;
                        num4 = 50;
                    }

                    --num4;
                    int index2 = this.selector[index1];
                    var minLen2 = this.minLens[index2];
                    int num10;
                    int num11;
                    for (num10 = this.BsR(minLen2); num10 > this.limit[index2][minLen2]; num10 = (num10 << 1) | num11)
                    {
                        ++minLen2;
                        while (this.bsLive < 1)
                        {
                            this.FillBuffer();
                        }

                        num11 = (this.bsBuff >> (this.bsLive - 1)) & 1;
                        --this.bsLive;
                    }

                    num7 = this.perm[index2][num10 - this.baseArray[index2][minLen2]];
                    switch (num7)
                    {
                        case 0:
                        case 1:
                        goto label_22;
                        default:
                        var num12 = num8 + 1;
                        var num13 = this.seqToUnseq[numArray[0]];
                        this.unzftab[num13] += num12;
                        for (; num12 > 0; --num12)
                        {
                            ++this.last;
                            this.ll8[this.last] = num13;
                        }

                        if (this.last >= num1)
                        {
                            BlockOverrun();
                        }

                        continue;
                    }
                }
                else
                {
                    ++this.last;
                    if (this.last >= num1)
                    {
                        BlockOverrun();
                    }

                    var num8 = numArray[num7 - 1];
                    ++this.unzftab[this.seqToUnseq[num8]];
                    this.ll8[this.last] = this.seqToUnseq[num8];
                    for (var index2 = num7 - 1; index2 > 0; --index2)
                    {
                        numArray[index2] = numArray[index2 - 1];
                    }

                    numArray[0] = num8;
                    if (num4 == 0)
                    {
                        ++index1;
                        num4 = 50;
                    }

                    --num4;
                    int index4 = this.selector[index1];
                    var minLen2 = this.minLens[index4];
                    int num9;
                    int num10;
                    for (num9 = this.BsR(minLen2); num9 > this.limit[index4][minLen2]; num9 = (num9 << 1) | num10)
                    {
                        ++minLen2;
                        while (this.bsLive < 1)
                        {
                            this.FillBuffer();
                        }

                        num10 = (this.bsBuff >> (this.bsLive - 1)) & 1;
                        --this.bsLive;
                    }

                    num7 = this.perm[index4][num9 - this.baseArray[index4][minLen2]];
                }
            }
        }

        /// <summary>Initialises the block.</summary>
        private void InitBlock()
        {
            var uchar1 = this.BsGetUChar();
            var uchar2 = this.BsGetUChar();
            var uchar3 = this.BsGetUChar();
            var uchar4 = this.BsGetUChar();
            var uchar5 = this.BsGetUChar();
            var uchar6 = this.BsGetUChar();
            if (uchar1 == '\x0017' && uchar2 == 'r' && uchar3 == 'E' && uchar4 == '8' && uchar5 == 'P'
                && uchar6 == '\x0090')
            {
                this.Complete();
            }
            else if (uchar1 != '1' || uchar2 != 'A' || uchar3 != 'Y' || uchar4 != '&' || uchar5 != 'S' || uchar6 != 'Y')
            {
                BadBlockHeader();
                this.streamEnd = true;
            }
            else
            {
                this.storedBlockCRC = this.BsGetInt32();
                this.blockRandomised = this.BsR(1) == 1;
                this.GetAndMoveToFrontDecode();
                this.mCrc.Reset();
                this.currentState = 1;
            }
        }

        /// <summary>Initializes this BZip2InputStream.</summary>
        private void Initialize()
        {
            var uchar1 = this.BsGetUChar();
            var uchar2 = this.BsGetUChar();
            var uchar3 = this.BsGetUChar();
            var uchar4 = this.BsGetUChar();
            if (uchar1 != 'B' || uchar2 != 'Z' || uchar3 != 'h' || uchar4 < '1' || uchar4 > '9')
            {
                this.streamEnd = true;
            }
            else
            {
                this.SetDecompressStructureSizes(uchar4 - 48);
                this.computedCombinedCRC = 0U;
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
                    this.seqToUnseq[this.nInUse] = (byte)index;
                    this.unseqToSeq[index] = (byte)this.nInUse;
                    ++this.nInUse;
                }
            }
        }

        /// <summary>Receive decoding tables.</summary>
        private void RecvDecodingTables()
        {
            var chArray = new char[6][];
            for (var index = 0; index < 6; ++index)
            {
                chArray[index] = new char[258];
            }

            var flagArray = new bool[16];
            for (var index = 0; index < 16; ++index)
            {
                flagArray[index] = this.BsR(1) == 1;
            }

            for (var index1 = 0; index1 < 16; ++index1)
            {
                if (flagArray[index1])
                {
                    for (var index2 = 0; index2 < 16; ++index2)
                    {
                        this.inUse[index1 * 16 + index2] = this.BsR(1) == 1;
                    }
                }
                else
                {
                    for (var index2 = 0; index2 < 16; ++index2)
                    {
                        this.inUse[index1 * 16 + index2] = false;
                    }
                }
            }

            this.MakeMaps();
            var alphaSize = this.nInUse + 2;
            var num1 = this.BsR(3);
            var num2 = this.BsR(15);
            for (var index = 0; index < num2; ++index)
            {
                var num3 = 0;
                while (this.BsR(1) == 1)
                {
                    ++num3;
                }

                this.selectorMtf[index] = (byte)num3;
            }

            var numArray = new byte[6];
            for (var index = 0; index < num1; ++index)
            {
                numArray[index] = (byte)index;
            }

            for (var index1 = 0; index1 < num2; ++index1)
            {
                int index2 = this.selectorMtf[index1];
                var num3 = numArray[index2];
                for (; index2 > 0; --index2)
                {
                    numArray[index2] = numArray[index2 - 1];
                }

                numArray[0] = num3;
                this.selector[index1] = num3;
            }

            for (var index1 = 0; index1 < num1; ++index1)
            {
                var num3 = this.BsR(5);
                for (var index2 = 0; index2 < alphaSize; ++index2)
                {
                    while (this.BsR(1) == 1)
                    {
                        if (this.BsR(1) == 0)
                        {
                            ++num3;
                        }
                        else
                        {
                            --num3;
                        }
                    }

                    chArray[index1][index2] = (char)num3;
                }
            }

            for (var index1 = 0; index1 < num1; ++index1)
            {
                var num3 = 32;
                var num4 = 0;
                for (var index2 = 0; index2 < alphaSize; ++index2)
                {
                    num4 = Math.Max(num4, chArray[index1][index2]);
                    num3 = Math.Min(num3, chArray[index1][index2]);
                }

                HbCreateDecodeTables(
                    this.limit[index1],
                    this.baseArray[index1],
                    this.perm[index1],
                    chArray[index1],
                    num3,
                    num4,
                    alphaSize);
                this.minLens[index1] = num3;
            }
        }

        /// <summary>Sets decompress structure sizes.</summary>
        /// <param name="newSize100k">The new size 100k.</param>
        private void SetDecompressStructureSizes(int newSize100k)
        {
            this.blockSize100k =
                0 <= newSize100k && newSize100k <= 9 && 0 <= this.blockSize100k && this.blockSize100k <= 9
                    ? newSize100k
                    : throw new BZip2Exception("Invalid block size");
            if (newSize100k == 0)
            {
                return;
            }

            var length = 100000 * newSize100k;
            this.ll8 = new byte[length];
            this.tt = new int[length];
        }

        /// <summary>Sets up the block.</summary>
        private void SetupBlock()
        {
            var numArray = new int[257];
            numArray[0] = 0;
            Array.Copy(this.unzftab, 0, numArray, 1, 256);
            for (var index = 1; index <= 256; ++index)
            {
                numArray[index] += numArray[index - 1];
            }

            for (var index = 0; index <= this.last; ++index)
            {
                var num = this.ll8[index];
                this.tt[numArray[num]] = index;
                ++numArray[num];
            }

            this.tPos = this.tt[this.origPtr];
            this.count = 0;
            this.i2 = 0;
            this.ch2 = 256;
            if (this.blockRandomised)
            {
                this.rNToGo = 0;
                this.rTPos = 0;
                this.SetupRandPartA();
            }
            else
            {
                this.SetupNoRandPartA();
            }
        }

        /// <summary>Sets up the no random part a.</summary>
        private void SetupNoRandPartA()
        {
            if (this.i2 <= this.last)
            {
                this.chPrev = this.ch2;
                this.ch2 = this.ll8[this.tPos];
                this.tPos = this.tt[this.tPos];
                ++this.i2;
                this.currentChar = this.ch2;
                this.currentState = 6;
                this.mCrc.Update(this.ch2);
            }
            else
            {
                this.EndBlock();
                this.InitBlock();
                this.SetupBlock();
            }
        }

        /// <summary>Sets up the no random part b.</summary>
        private void SetupNoRandPartB()
        {
            if (this.ch2 != this.chPrev)
            {
                this.currentState = 5;
                this.count = 1;
                this.SetupNoRandPartA();
            }
            else
            {
                ++this.count;
                if (this.count >= 4)
                {
                    this.z = this.ll8[this.tPos];
                    this.tPos = this.tt[this.tPos];
                    this.currentState = 7;
                    this.j2 = 0;
                    this.SetupNoRandPartC();
                }
                else
                {
                    this.currentState = 5;
                    this.SetupNoRandPartA();
                }
            }
        }

        /// <summary>Sets up the no random part c.</summary>
        private void SetupNoRandPartC()
        {
            if (this.j2 < this.z)
            {
                this.currentChar = this.ch2;
                this.mCrc.Update(this.ch2);
                ++this.j2;
            }
            else
            {
                this.currentState = 5;
                ++this.i2;
                this.count = 0;
                this.SetupNoRandPartA();
            }
        }

        /// <summary>Sets up the random part a.</summary>
        private void SetupRandPartA()
        {
            if (this.i2 <= this.last)
            {
                this.chPrev = this.ch2;
                this.ch2 = this.ll8[this.tPos];
                this.tPos = this.tt[this.tPos];
                if (this.rNToGo == 0)
                {
                    this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
                    ++this.rTPos;
                    if (this.rTPos == 512)
                    {
                        this.rTPos = 0;
                    }
                }

                --this.rNToGo;
                this.ch2 ^= this.rNToGo == 1 ? 1 : 0;
                ++this.i2;
                this.currentChar = this.ch2;
                this.currentState = 3;
                this.mCrc.Update(this.ch2);
            }
            else
            {
                this.EndBlock();
                this.InitBlock();
                this.SetupBlock();
            }
        }

        /// <summary>Sets up the random part b.</summary>
        private void SetupRandPartB()
        {
            if (this.ch2 != this.chPrev)
            {
                this.currentState = 2;
                this.count = 1;
                this.SetupRandPartA();
            }
            else
            {
                ++this.count;
                if (this.count >= 4)
                {
                    this.z = this.ll8[this.tPos];
                    this.tPos = this.tt[this.tPos];
                    if (this.rNToGo == 0)
                    {
                        this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
                        ++this.rTPos;
                        if (this.rTPos == 512)
                        {
                            this.rTPos = 0;
                        }
                    }

                    --this.rNToGo;
                    this.z ^= this.rNToGo == 1 ? (byte)1 : (byte)0;
                    this.j2 = 0;
                    this.currentState = 4;
                    this.SetupRandPartC();
                }
                else
                {
                    this.currentState = 2;
                    this.SetupRandPartA();
                }
            }
        }

        /// <summary>Sets up the random part c.</summary>
        private void SetupRandPartC()
        {
            if (this.j2 < this.z)
            {
                this.currentChar = this.ch2;
                this.mCrc.Update(this.ch2);
                ++this.j2;
            }
            else
            {
                this.currentState = 2;
                ++this.i2;
                this.count = 0;
                this.SetupRandPartA();
            }
        }
    }
}