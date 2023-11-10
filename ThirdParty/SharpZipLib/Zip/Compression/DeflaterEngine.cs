// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflaterEngine
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;
    using Checksums;

    /// <summary>A deflater engine.</summary>
    /// <seealso cref="DeflaterConstants" />
    public class DeflaterEngine : DeflaterConstants
    {
        /// <summary>The too far.</summary>
        private const int TooFar = 4096;

        /// <summary>The adler.</summary>
        private readonly Adler32 adler;

        /// <summary>The block start.</summary>
        private int blockStart;

        /// <summary>The compression function.</summary>
        private int compressionFunction;

        /// <summary>Length of the good.</summary>
        private int goodLength;

        /// <summary>The head.</summary>
        private readonly short[] head;

        /// <summary>The huffman.</summary>
        private readonly DeflaterHuffman huffman;

        /// <summary>Buffer for input data.</summary>
        private byte[] inputBuf;

        /// <summary>The input end.</summary>
        private int inputEnd;

        /// <summary>The input off.</summary>
        private int inputOff;

        /// <summary>The insert h.</summary>
        private int ins_h;

        /// <summary>The lookahead.</summary>
        private int lookahead;

        /// <summary>Length of the match.</summary>
        private int matchLen;

        /// <summary>The match start.</summary>
        private int matchStart;

        /// <summary>The maximum chain.</summary>
        private int max_chain;

        /// <summary>The maximum lazy.</summary>
        private int max_lazy;

        /// <summary>Length of the nice.</summary>
        private int niceLength;

        /// <summary>The pending.</summary>
        private readonly DeflaterPending pending;

        /// <summary>The previous.</summary>
        private readonly short[] prev;

        /// <summary>True if previous available.</summary>
        private bool prevAvailable;

        /// <summary>The strstart.</summary>
        private int strstart;

        /// <summary>The window.</summary>
        private readonly byte[] window;

        /// <summary>Initializes a new instance of the <see cref="DeflaterEngine" /> class.</summary>
        /// <param name="pending">The pending.</param>
        public DeflaterEngine(DeflaterPending pending)
        {
            this.pending = pending;
            huffman = new DeflaterHuffman(pending);
            adler = new Adler32();
            window = new byte[65536];
            head = new short[32768];
            prev = new short[32768];
            blockStart = strstart = 1;
        }

        /// <summary>Gets the adler.</summary>
        /// <value>The adler.</value>
        public int Adler => (int)adler.Value;

        /// <summary>Gets or sets the strategy.</summary>
        /// <value>The strategy.</value>
        public DeflateStrategy Strategy { get; set; }

        /// <summary>Gets the total number of in.</summary>
        /// <value>The total number of in.</value>
        public long TotalIn { get; private set; }

        /// <summary>Deflates.</summary>
        /// <param name="flush"> True to flush.</param>
        /// <param name="finish">True to finish.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Deflate(bool flush, bool finish)
        {
            bool flag;
            do
            {
                FillWindow();
                var flush1 = flush && inputOff == inputEnd;
                switch (compressionFunction)
                {
                    case 0:
                    {
                        flag = DeflateStored(flush1, finish);
                        break;
                    }
                    case 1:
                    {
                        flag = DeflateFast(flush1, finish);
                        break;
                    }
                    case 2:
                    {
                        flag = DeflateSlow(flush1, finish);
                        break;
                    }
                    default:
                    {
                        throw new InvalidOperationException("unknown compressionFunction");
                    }
                }
            }
            while (pending.IsFlushed && flag);
            return flag;
        }

        /// <summary>Fill window.</summary>
        public void FillWindow()
        {
            if (strstart >= 65274)
            {
                SlideWindow();
            }
            int num;
            for (; lookahead < 262 && inputOff < inputEnd; lookahead += num)
            {
                num = 65536 - lookahead - strstart;
                if (num > inputEnd - inputOff)
                {
                    num = inputEnd - inputOff;
                }
                Array.Copy(inputBuf, inputOff, window, strstart + lookahead, num);
                adler.Update(inputBuf, inputOff, num);
                inputOff += num;
                TotalIn += num;
            }
            if (lookahead < 3)
            {
                return;
            }
            UpdateHash();
        }

        /// <summary>Determines if we can needs input.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool NeedsInput()
        {
            return inputEnd == inputOff;
        }

        /// <summary>Resets this DeflaterEngine.</summary>
        public void Reset()
        {
            huffman.Reset();
            adler.Reset();
            blockStart = strstart = 1;
            lookahead = 0;
            TotalIn = 0L;
            prevAvailable = false;
            matchLen = 2;
            for (var index = 0; index < 32768; ++index)
            {
                head[index] = 0;
            }
            for (var index = 0; index < 32768; ++index)
            {
                prev[index] = 0;
            }
        }

        /// <summary>Resets the adler.</summary>
        public void ResetAdler()
        {
            adler.Reset();
        }

        /// <summary>Sets a dictionary.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        public void SetDictionary(byte[] buffer, int offset, int length)
        {
            adler.Update(buffer, offset, length);
            if (length < 3)
            {
                return;
            }
            if (length > 32506)
            {
                offset += length - 32506;
                length = 32506;
            }
            Array.Copy(buffer, offset, window, strstart, length);
            UpdateHash();
            --length;
            while (--length > 0)
            {
                InsertString();
                ++strstart;
            }
            strstart += 2;
            blockStart = strstart;
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
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (inputOff < inputEnd)
            {
                throw new InvalidOperationException("Old input was not completely processed");
            }
            var num = offset + count;
            if (offset > num || num > buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            inputBuf = buffer;
            inputOff = offset;
            inputEnd = num;
        }

        /// <summary>Sets a level.</summary>
        /// <param name="level">The level.</param>
        public void SetLevel(int level)
        {
            goodLength = level >= 0 && level <= 9
                ? GOOD_LENGTH[level]
                : throw new ArgumentOutOfRangeException(nameof(level));
            max_lazy = MAX_LAZY[level];
            niceLength = NICE_LENGTH[level];
            max_chain = MAX_CHAIN[level];
            if (COMPR_FUNC[level] == compressionFunction)
            {
                return;
            }
            switch (compressionFunction)
            {
                case 0:
                {
                    if (strstart > blockStart)
                    {
                        huffman.FlushStoredBlock(window, blockStart, strstart - blockStart, false);
                        blockStart = strstart;
                    }
                    UpdateHash();
                    break;
                }
                case 1:
                {
                    if (strstart > blockStart)
                    {
                        huffman.FlushBlock(window, blockStart, strstart - blockStart, false);
                        blockStart = strstart;
                    }
                    break;
                }
                case 2:
                {
                    if (prevAvailable)
                    {
                        huffman.TallyLit(window[strstart - 1] & byte.MaxValue);
                    }
                    if (strstart > blockStart)
                    {
                        huffman.FlushBlock(window, blockStart, strstart - blockStart, false);
                        blockStart = strstart;
                    }
                    prevAvailable = false;
                    matchLen = 2;
                    break;
                }
            }
            compressionFunction = COMPR_FUNC[level];
        }

        /// <summary>Deflate fast.</summary>
        /// <param name="flush"> True to flush.</param>
        /// <param name="finish">True to finish.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool DeflateFast(bool flush, bool finish)
        {
            if (lookahead < 262 && !flush)
            {
                return false;
            }
            while (lookahead >= 262 || flush)
            {
                if (lookahead == 0)
                {
                    huffman.FlushBlock(window, blockStart, strstart - blockStart, finish);
                    blockStart = strstart;
                    return false;
                }
                if (strstart > 65274)
                {
                    SlideWindow();
                }
                int curMatch;
                if (lookahead >= 3
                    && (curMatch = InsertString()) != 0
                    && Strategy != DeflateStrategy.HuffmanOnly
                    && strstart - curMatch <= 32506
                    && FindLongestMatch(curMatch))
                {
                    var flag = huffman.TallyDist(strstart - matchStart, matchLen);
                    lookahead -= matchLen;
                    if (matchLen <= max_lazy && lookahead >= 3)
                    {
                        while (--matchLen > 0)
                        {
                            ++strstart;
                            InsertString();
                        }
                        ++strstart;
                    }
                    else
                    {
                        strstart += matchLen;
                        if (lookahead >= 2)
                        {
                            UpdateHash();
                        }
                    }
                    matchLen = 2;
                    if (!flag)
                    {
                        continue;
                    }
                }
                else
                {
                    huffman.TallyLit(window[strstart] & byte.MaxValue);
                    ++strstart;
                    --lookahead;
                }
                if (huffman.IsFull())
                {
                    var lastBlock = finish && lookahead == 0;
                    huffman.FlushBlock(window, blockStart, strstart - blockStart, lastBlock);
                    blockStart = strstart;
                    return !lastBlock;
                }
            }
            return true;
        }

        /// <summary>Deflate slow.</summary>
        /// <param name="flush"> True to flush.</param>
        /// <param name="finish">True to finish.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool DeflateSlow(bool flush, bool finish)
        {
            if (lookahead < 262 && !flush)
            {
                return false;
            }
            while (lookahead >= 262 || flush)
            {
                if (lookahead == 0)
                {
                    if (prevAvailable)
                    {
                        huffman.TallyLit(window[strstart - 1] & byte.MaxValue);
                    }
                    prevAvailable = false;
                    huffman.FlushBlock(window, blockStart, strstart - blockStart, finish);
                    blockStart = strstart;
                    return false;
                }
                if (strstart >= 65274)
                {
                    SlideWindow();
                }
                var matchStart = this.matchStart;
                var matchLen = this.matchLen;
                if (lookahead >= 3)
                {
                    var curMatch = InsertString();
                    if (Strategy != DeflateStrategy.HuffmanOnly
                        && curMatch != 0
                        && strstart - curMatch <= 32506
                        && FindLongestMatch(curMatch)
                        && this.matchLen <= 5
                        && (Strategy == DeflateStrategy.Filtered
                            || this.matchLen == 3 && strstart - this.matchStart > 4096))
                    {
                        this.matchLen = 2;
                    }
                }
                if (matchLen >= 3 && this.matchLen <= matchLen)
                {
                    huffman.TallyDist(strstart - 1 - matchStart, matchLen);
                    var num = matchLen - 2;
                    do
                    {
                        ++strstart;
                        --lookahead;
                        if (lookahead >= 3)
                        {
                            InsertString();
                        }
                    }
                    while (--num > 0);
                    ++strstart;
                    --lookahead;
                    prevAvailable = false;
                    this.matchLen = 2;
                }
                else
                {
                    if (prevAvailable)
                    {
                        huffman.TallyLit(window[strstart - 1] & byte.MaxValue);
                    }
                    prevAvailable = true;
                    ++strstart;
                    --lookahead;
                }
                if (huffman.IsFull())
                {
                    var storedLength = strstart - blockStart;
                    if (prevAvailable)
                    {
                        --storedLength;
                    }
                    var lastBlock = finish && lookahead == 0 && !prevAvailable;
                    huffman.FlushBlock(window, blockStart, storedLength, lastBlock);
                    blockStart += storedLength;
                    return !lastBlock;
                }
            }
            return true;
        }

        /// <summary>Deflate stored.</summary>
        /// <param name="flush"> True to flush.</param>
        /// <param name="finish">True to finish.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool DeflateStored(bool flush, bool finish)
        {
            if (!flush && lookahead == 0)
            {
                return false;
            }
            strstart += lookahead;
            lookahead = 0;
            var storedLength = strstart - blockStart;
            if (storedLength < MAX_BLOCK_SIZE && (blockStart >= 32768 || storedLength < 32506) && !flush)
            {
                return true;
            }
            var lastBlock = finish;
            if (storedLength > MAX_BLOCK_SIZE)
            {
                storedLength = MAX_BLOCK_SIZE;
                lastBlock = false;
            }
            huffman.FlushStoredBlock(window, blockStart, storedLength, lastBlock);
            blockStart += storedLength;
            return !lastBlock;
        }

        /// <summary>Searches for the first longest match.</summary>
        /// <param name="curMatch">A match specifying the current.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool FindLongestMatch(int curMatch)
        {
            var maxChain = max_chain;
            var num1 = niceLength;
            var prev = this.prev;
            var strstart = this.strstart;
            var index = this.strstart + matchLen;
            var val1 = Math.Max(matchLen, 2);
            var num2 = Math.Max(this.strstart - 32506, 0);
            var num3 = this.strstart + 258 - 1;
            var num4 = window[index - 1];
            var num5 = window[index];
            if (val1 >= goodLength)
            {
                maxChain >>= 2;
            }
            if (num1 > lookahead)
            {
                num1 = lookahead;
            }
            do
            {
                if (window[curMatch + val1] == num5
                    && window[curMatch + val1 - 1] == num4
                    && window[curMatch] == window[strstart]
                    && window[curMatch + 1] == window[strstart + 1])
                {
                    var num6 = curMatch + 2;
                    var num7 = strstart + 2;
                    int num8;
                    int num9;
                    int num10;
                    int num11;
                    int num12;
                    int num13;
                    int num14;
                    do
                    {
                        ;
                    }
                    while (window[++num7] == window[num8 = num6 + 1]
                        && window[++num7] == window[num9 = num8 + 1]
                        && window[++num7] == window[num10 = num9 + 1]
                        && window[++num7] == window[num11 = num10 + 1]
                        && window[++num7] == window[num12 = num11 + 1]
                        && window[++num7] == window[num13 = num12 + 1]
                        && window[++num7] == window[num14 = num13 + 1]
                        && window[++num7] == window[num6 = num14 + 1]
                        && num7 < num3);
                    if (num7 > index)
                    {
                        matchStart = curMatch;
                        index = num7;
                        val1 = num7 - this.strstart;
                        if (val1 < num1)
                        {
                            num4 = window[index - 1];
                            num5 = window[index];
                        }
                        else
                        {
                            break;
                        }
                    }
                    strstart = this.strstart;
                }
            }
            while ((curMatch = prev[curMatch & short.MaxValue] & ushort.MaxValue) > num2 && --maxChain != 0);
            matchLen = Math.Min(val1, lookahead);
            return matchLen >= 3;
        }

        /// <summary>Inserts a string.</summary>
        /// <returns>An int.</returns>
        private int InsertString()
        {
            var index = ((ins_h << 5) ^ window[strstart + 2]) & short.MaxValue;
            short num;
            prev[strstart & short.MaxValue] = num = head[index];
            head[index] = (short)strstart;
            ins_h = index;
            return num & ushort.MaxValue;
        }

        /// <summary>Slide window.</summary>
        private void SlideWindow()
        {
            Array.Copy(window, 32768, window, 0, 32768);
            matchStart -= 32768;
            strstart -= 32768;
            blockStart -= 32768;
            for (var index = 0; index < 32768; ++index)
            {
                var num = head[index] & ushort.MaxValue;
                head[index] = num >= 32768 ? (short)(num - 32768) : (short)0;
            }
            for (var index = 0; index < 32768; ++index)
            {
                var num = prev[index] & ushort.MaxValue;
                prev[index] = num >= 32768 ? (short)(num - 32768) : (short)0;
            }
        }

        /// <summary>Updates the hash.</summary>
        private void UpdateHash()
        {
            ins_h = (window[strstart] << 5) ^ window[strstart + 1];
        }
    }
}
