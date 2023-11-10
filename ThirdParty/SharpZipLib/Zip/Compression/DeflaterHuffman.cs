// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflaterHuffman
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;

    /// <summary>A deflater huffman.</summary>
    public class DeflaterHuffman
    {
        /// <summary>The bitlen number.</summary>
        private const int BITLEN_NUM = 19;

        /// <summary>The bufsize.</summary>
        private const int BUFSIZE = 16384;

        /// <summary>The distance number.</summary>
        private const int DIST_NUM = 30;

        /// <summary>The EOF symbol.</summary>
        private const int EOF_SYMBOL = 256;

        /// <summary>The literal number.</summary>
        private const int LITERAL_NUM = 286;

        /// <summary>The rep 11 138.</summary>
        private const int REP_11_138 = 18;

        /// <summary>The rep 3 10.</summary>
        private const int REP_3_10 = 17;

        /// <summary>The rep 3 6.</summary>
        private const int REP_3_6 = 16;

        /// <summary>The bit 4 reverse.</summary>
        private static readonly byte[] bit4Reverse = new byte[16]
        {
            0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15,
        };

        /// <summary>The bl order.</summary>
        private static readonly int[] BL_ORDER = new int[19]
        {
            16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15,
        };

        /// <summary>The static d codes.</summary>
        private static readonly short[] staticDCodes;

        /// <summary>Length of the static d.</summary>
        private static readonly byte[] staticDLength;

        /// <summary>The static l codes.</summary>
        private static readonly short[] staticLCodes = new short[286];

        /// <summary>Length of the static l.</summary>
        private static readonly byte[] staticLLength = new byte[286];

        /// <summary>The pending.</summary>
        public DeflaterPending pending;

        /// <summary>The bl tree.</summary>
        private readonly Tree blTree;

        /// <summary>The buffer.</summary>
        private readonly short[] d_buf;

        /// <summary>The distance tree.</summary>
        private readonly Tree distTree;

        /// <summary>The extra bits.</summary>
        private int extra_bits;

        /// <summary>The buffer.</summary>
        private readonly byte[] l_buf;

        /// <summary>The last lit.</summary>
        private int last_lit;

        /// <summary>The literal tree.</summary>
        private readonly Tree literalTree;

        /// <summary>Initializes static members of the ICSharpCode.SharpZipLib.Zip.Compression.DeflaterHuffman class.</summary>
        static DeflaterHuffman()
        {
            int index1;
            for (index1 = 0; index1 < 144; staticLLength[index1++] = (byte)8)
            {
                staticLCodes[index1] = BitReverse((48 + index1) << 8);
            }
            for (; index1 < 256; staticLLength[index1++] = (byte)9)
            {
                staticLCodes[index1] = BitReverse((256 + index1) << 7);
            }
            for (; index1 < 280; staticLLength[index1++] = (byte)7)
            {
                staticLCodes[index1] = BitReverse((index1 - 256) << 9);
            }
            for (; index1 < 286; staticLLength[index1++] = (byte)8)
            {
                staticLCodes[index1] = BitReverse((index1 - 88) << 8);
            }
            staticDCodes = new short[30];
            staticDLength = new byte[30];
            for (var index2 = 0; index2 < 30; ++index2)
            {
                staticDCodes[index2] = BitReverse(index2 << 11);
                staticDLength[index2] = 5;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.DeflaterHuffman" /> class.
        /// </summary>
        /// <param name="pending">The pending.</param>
        public DeflaterHuffman(DeflaterPending pending)
        {
            this.pending = pending;
            literalTree = new Tree(this, 286, 257, 15);
            distTree = new Tree(this, 30, 1, 15);
            blTree = new Tree(this, 19, 4, 7);
            d_buf = new short[16384];
            l_buf = new byte[16384];
        }

        /// <summary>Bit reverse.</summary>
        /// <param name="toReverse">to reverse.</param>
        /// <returns>A short.</returns>
        public static short BitReverse(int toReverse)
        {
            return (short)((bit4Reverse[toReverse & 15] << 12)
                | (bit4Reverse[(toReverse >> 4) & 15] << 8)
                | (bit4Reverse[(toReverse >> 8) & 15] << 4)
                | bit4Reverse[toReverse >> 12]);
        }

        /// <summary>Compress block.</summary>
        public void CompressBlock()
        {
            for (var index = 0; index < last_lit; ++index)
            {
                var num1 = l_buf[index] & byte.MaxValue;
                int num2 = d_buf[index];
                var distance = num2 - 1;
                if (num2 != 0)
                {
                    var code1 = Lcode(num1);
                    literalTree.WriteSymbol(code1);
                    var count1 = (code1 - 261) / 4;
                    if (count1 > 0 && count1 <= 5)
                    {
                        pending.WriteBits(num1 & ((1 << count1) - 1), count1);
                    }
                    var code2 = Dcode(distance);
                    distTree.WriteSymbol(code2);
                    var count2 = code2 / 2 - 1;
                    if (count2 > 0)
                    {
                        pending.WriteBits(distance & ((1 << count2) - 1), count2);
                    }
                }
                else
                {
                    literalTree.WriteSymbol(num1);
                }
            }
            literalTree.WriteSymbol(256);
        }

        /// <summary>Flushes the block.</summary>
        /// <param name="stored">      The stored.</param>
        /// <param name="storedOffset">The stored offset.</param>
        /// <param name="storedLength">Length of the stored.</param>
        /// <param name="lastBlock">   True to last block.</param>
        public void FlushBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
        {
            ++literalTree.freqs[256];
            literalTree.BuildTree();
            distTree.BuildTree();
            literalTree.CalcBLFreq(blTree);
            distTree.CalcBLFreq(blTree);
            blTree.BuildTree();
            var blTreeCodes = 4;
            for (var index = 18; index > blTreeCodes; --index)
            {
                if (blTree.length[BL_ORDER[index]] > 0)
                {
                    blTreeCodes = index + 1;
                }
            }
            var num = 14
                + blTreeCodes * 3
                + blTree.GetEncodedLength()
                + literalTree.GetEncodedLength()
                + distTree.GetEncodedLength()
                + extra_bits;
            var extraBits = extra_bits;
            for (var index = 0; index < 286; ++index)
            {
                extraBits += literalTree.freqs[index] * staticLLength[index];
            }
            for (var index = 0; index < 30; ++index)
            {
                extraBits += distTree.freqs[index] * staticDLength[index];
            }
            if (num >= extraBits)
            {
                num = extraBits;
            }
            if (storedOffset >= 0 && storedLength + 4 < num >> 3)
            {
                FlushStoredBlock(stored, storedOffset, storedLength, lastBlock);
            }
            else if (num == extraBits)
            {
                pending.WriteBits(2 + (lastBlock ? 1 : 0), 3);
                literalTree.SetStaticCodes(staticLCodes, staticLLength);
                distTree.SetStaticCodes(staticDCodes, staticDLength);
                CompressBlock();
                Reset();
            }
            else
            {
                pending.WriteBits(4 + (lastBlock ? 1 : 0), 3);
                SendAllTrees(blTreeCodes);
                CompressBlock();
                Reset();
            }
        }

        /// <summary>Flushes the stored block.</summary>
        /// <param name="stored">      The stored.</param>
        /// <param name="storedOffset">The stored offset.</param>
        /// <param name="storedLength">Length of the stored.</param>
        /// <param name="lastBlock">   True to last block.</param>
        public void FlushStoredBlock(byte[] stored, int storedOffset, int storedLength, bool lastBlock)
        {
            pending.WriteBits(lastBlock ? 1 : 0, 3);
            pending.AlignToByte();
            pending.WriteShort(storedLength);
            pending.WriteShort(~storedLength);
            pending.WriteBlock(stored, storedOffset, storedLength);
            Reset();
        }

        /// <summary>Query if this DeflaterHuffman is full.</summary>
        /// <returns>True if full, false if not.</returns>
        public bool IsFull()
        {
            return last_lit >= 16384;
        }

        /// <summary>Resets this DeflaterHuffman.</summary>
        public void Reset()
        {
            last_lit = 0;
            extra_bits = 0;
            literalTree.Reset();
            distTree.Reset();
            blTree.Reset();
        }

        /// <summary>Sends all trees.</summary>
        /// <param name="blTreeCodes">The bl tree codes.</param>
        public void SendAllTrees(int blTreeCodes)
        {
            blTree.BuildCodes();
            literalTree.BuildCodes();
            distTree.BuildCodes();
            pending.WriteBits(literalTree.numCodes - 257, 5);
            pending.WriteBits(distTree.numCodes - 1, 5);
            pending.WriteBits(blTreeCodes - 4, 4);
            for (var index = 0; index < blTreeCodes; ++index)
            {
                pending.WriteBits(blTree.length[BL_ORDER[index]], 3);
            }
            literalTree.WriteTree(blTree);
            distTree.WriteTree(blTree);
        }

        /// <summary>Tally distance.</summary>
        /// <param name="distance">The distance.</param>
        /// <param name="length">  The length.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool TallyDist(int distance, int length)
        {
            d_buf[last_lit] = (short)distance;
            l_buf[last_lit++] = (byte)(length - 3);
            var index1 = Lcode(length - 3);
            ++literalTree.freqs[index1];
            if (index1 >= 265 && index1 < 285)
            {
                extra_bits += (index1 - 261) / 4;
            }
            var index2 = Dcode(distance - 1);
            ++distTree.freqs[index2];
            if (index2 >= 4)
            {
                extra_bits += index2 / 2 - 1;
            }
            return IsFull();
        }

        /// <summary>Tally lit.</summary>
        /// <param name="literal">The literal.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool TallyLit(int literal)
        {
            d_buf[last_lit] = 0;
            l_buf[last_lit++] = (byte)literal;
            ++literalTree.freqs[literal];
            return IsFull();
        }

        /// <summary>Dcodes.</summary>
        /// <param name="distance">The distance.</param>
        /// <returns>An int.</returns>
        private static int Dcode(int distance)
        {
            var num = 0;
            for (; distance >= 4; distance >>= 1)
            {
                num += 2;
            }
            return num + distance;
        }

        /// <summary>Lcodes.</summary>
        /// <param name="length">The length.</param>
        /// <returns>An int.</returns>
        private static int Lcode(int length)
        {
            if (length == byte.MaxValue)
            {
                return 285;
            }
            var num = 257;
            for (; length >= 8; length >>= 1)
            {
                num += 4;
            }
            return num + length;
        }

        /// <summary>A tree.</summary>
        private class Tree
        {
            /// <summary>The freqs.</summary>
            public readonly short[] freqs;

            /// <summary>The length.</summary>
            public byte[] length;

            /// <summary>The minimum number codes.</summary>
            public readonly int minNumCodes;

            /// <summary>Number of codes.</summary>
            public int numCodes;

            /// <summary>The bl counts.</summary>
            private readonly int[] bl_counts;

            /// <summary>The codes.</summary>
            private short[] codes;

            /// <summary>The dh.</summary>
            private readonly DeflaterHuffman dh;

            /// <summary>The maximum length.</summary>
            private readonly int maxLength;

            /// <summary>
            ///     Initializes a new instance of the
            ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.DeflaterHuffman.Tree" /> class.
            /// </summary>
            /// <param name="dh">       The dh.</param>
            /// <param name="elems">    The elements.</param>
            /// <param name="minCodes"> The minimum codes.</param>
            /// <param name="maxLength">The maximum length.</param>
            public Tree(DeflaterHuffman dh, int elems, int minCodes, int maxLength)
            {
                this.dh = dh;
                minNumCodes = minCodes;
                this.maxLength = maxLength;
                freqs = new short[elems];
                bl_counts = new int[maxLength];
            }

            /// <summary>Builds the codes.</summary>
            public void BuildCodes()
            {
                var length = freqs.Length;
                var numArray = new int[maxLength];
                var num1 = 0;
                codes = new short[freqs.Length];
                for (var index = 0; index < maxLength; ++index)
                {
                    numArray[index] = num1;
                    num1 += bl_counts[index] << (15 - index);
                }
                for (var index = 0; index < numCodes; ++index)
                {
                    int num2 = this.length[index];
                    if (num2 > 0)
                    {
                        codes[index] = BitReverse(numArray[num2 - 1]);
                        numArray[num2 - 1] += 1 << (16 - num2);
                    }
                }
            }

            /// <summary>Builds the tree.</summary>
            public void BuildTree()
            {
                var length = freqs.Length;
                var numArray1 = new int[length];
                var num1 = 0;
                var num2 = 0;
                for (var index1 = 0; index1 < length; ++index1)
                {
                    int freq = freqs[index1];
                    if (freq != 0)
                    {
                        int index2;
                        int index3;
                        for (index2 = num1++;
                             index2 > 0 && (int)freqs[numArray1[index3 = (index2 - 1) / 2]] > freq;
                             index2 = index3)
                        {
                            numArray1[index2] = numArray1[index3];
                        }
                        numArray1[index2] = index1;
                        num2 = index1;
                    }
                }
                int num3;
                for (; num1 < 2; numArray1[num1++] = num3)
                {
                    int num4;
                    if (num2 >= 2)
                    {
                        num4 = 0;
                    }
                    else
                    {
                        num2 = num4 = num2 + 1;
                    }
                    num3 = num4;
                }
                numCodes = Math.Max(num2 + 1, minNumCodes);
                var num5 = num1;
                var childs = new int[4 * num1 - 2];
                var numArray2 = new int[2 * num1 - 1];
                var num6 = num5;
                for (var index1 = 0; index1 < num1; ++index1)
                {
                    var index2 = numArray1[index1];
                    childs[2 * index1] = index2;
                    childs[2 * index1 + 1] = -1;
                    numArray2[index1] = freqs[index2] << 8;
                    numArray1[index1] = index1;
                }
                do
                {
                    var index1 = numArray1[0];
                    var index2 = numArray1[--num1];
                    var index3 = 0;
                    for (var index4 = 1; index4 < num1; index4 = index4 * 2 + 1)
                    {
                        if (index4 + 1 < num1 && numArray2[numArray1[index4]] > numArray2[numArray1[index4 + 1]])
                        {
                            ++index4;
                        }
                        numArray1[index3] = numArray1[index4];
                        index3 = index4;
                    }
                    var num4 = numArray2[index2];
                    int index5;
                    while ((index5 = index3) > 0 && numArray2[numArray1[index3 = (index5 - 1) / 2]] > num4)
                    {
                        numArray1[index5] = numArray1[index3];
                    }
                    numArray1[index5] = index2;
                    var index6 = numArray1[0];
                    var index7 = num6++;
                    childs[2 * index7] = index1;
                    childs[2 * index7 + 1] = index6;
                    var num7 = Math.Min(numArray2[index1] & byte.MaxValue, numArray2[index6] & byte.MaxValue);
                    int num8;
                    numArray2[index7] = num8 = numArray2[index1] + numArray2[index6] - num7 + 1;
                    var index8 = 0;
                    for (var index4 = 1; index4 < num1; index4 = index8 * 2 + 1)
                    {
                        if (index4 + 1 < num1 && numArray2[numArray1[index4]] > numArray2[numArray1[index4 + 1]])
                        {
                            ++index4;
                        }
                        numArray1[index8] = numArray1[index4];
                        index8 = index4;
                    }
                    int index9;
                    while ((index9 = index8) > 0 && numArray2[numArray1[index8 = (index9 - 1) / 2]] > num8)
                    {
                        numArray1[index9] = numArray1[index8];
                    }
                    numArray1[index9] = index7;
                }
                while (num1 > 1);
                if (numArray1[0] != childs.Length / 2 - 1)
                {
                    throw new SharpZipBaseException("Heap invariant violated");
                }
                BuildLength(childs);
            }

            /// <summary>Calculates the bl frequency.</summary>
            /// <param name="blTree">The bl tree.</param>
            public void CalcBLFreq(Tree blTree)
            {
                var index1 = -1;
                var index2 = 0;
                while (index2 < numCodes)
                {
                    var num1 = 1;
                    int index3 = length[index2];
                    int num2;
                    int num3;
                    if (index3 == 0)
                    {
                        num2 = 138;
                        num3 = 3;
                    }
                    else
                    {
                        num2 = 6;
                        num3 = 3;
                        if (index1 != index3)
                        {
                            ++blTree.freqs[index3];
                            num1 = 0;
                        }
                    }
                    index1 = index3;
                    ++index2;
                    while (index2 < numCodes && index1 == length[index2])
                    {
                        ++index2;
                        if (++num1 >= num2)
                        {
                            break;
                        }
                    }
                    if (num1 < num3)
                    {
                        blTree.freqs[index1] += (short)num1;
                    }
                    else if (index1 != 0)
                    {
                        ++blTree.freqs[16];
                    }
                    else if (num1 <= 10)
                    {
                        ++blTree.freqs[17];
                    }
                    else
                    {
                        ++blTree.freqs[18];
                    }
                }
            }

            /// <summary>Check empty.</summary>
            public void CheckEmpty()
            {
                var flag = true;
                for (var index = 0; index < freqs.Length; ++index)
                {
                    if (freqs[index] != 0)
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    throw new SharpZipBaseException("!Empty");
                }
            }

            /// <summary>Gets encoded length.</summary>
            /// <returns>The encoded length.</returns>
            public int GetEncodedLength()
            {
                var num = 0;
                for (var index = 0; index < freqs.Length; ++index)
                {
                    num += freqs[index] * length[index];
                }
                return num;
            }

            /// <summary>Resets this Tree.</summary>
            public void Reset()
            {
                for (var index = 0; index < freqs.Length; ++index)
                {
                    freqs[index] = 0;
                }
                codes = null;
                length = null;
            }

            /// <summary>Sets static codes.</summary>
            /// <param name="staticCodes">  The static codes.</param>
            /// <param name="staticLengths">List of lengths of the statics.</param>
            public void SetStaticCodes(short[] staticCodes, byte[] staticLengths)
            {
                codes = staticCodes;
                length = staticLengths;
            }

            /// <summary>Writes a symbol.</summary>
            /// <param name="code">The code.</param>
            public void WriteSymbol(int code)
            {
                dh.pending.WriteBits(codes[code] & ushort.MaxValue, length[code]);
            }

            /// <summary>Writes a tree.</summary>
            /// <param name="blTree">The bl tree.</param>
            public void WriteTree(Tree blTree)
            {
                var code1 = -1;
                var index = 0;
                while (index < numCodes)
                {
                    var num1 = 1;
                    int code2 = length[index];
                    int num2;
                    int num3;
                    if (code2 == 0)
                    {
                        num2 = 138;
                        num3 = 3;
                    }
                    else
                    {
                        num2 = 6;
                        num3 = 3;
                        if (code1 != code2)
                        {
                            blTree.WriteSymbol(code2);
                            num1 = 0;
                        }
                    }
                    code1 = code2;
                    ++index;
                    while (index < numCodes && code1 == length[index])
                    {
                        ++index;
                        if (++num1 >= num2)
                        {
                            break;
                        }
                    }
                    if (num1 < num3)
                    {
                        while (num1-- > 0)
                        {
                            blTree.WriteSymbol(code1);
                        }
                    }
                    else if (code1 != 0)
                    {
                        blTree.WriteSymbol(16);
                        dh.pending.WriteBits(num1 - 3, 2);
                    }
                    else if (num1 <= 10)
                    {
                        blTree.WriteSymbol(17);
                        dh.pending.WriteBits(num1 - 3, 3);
                    }
                    else
                    {
                        blTree.WriteSymbol(18);
                        dh.pending.WriteBits(num1 - 11, 7);
                    }
                }
            }

            /// <summary>Builds a length.</summary>
            /// <param name="childs">The childs.</param>
            private void BuildLength(int[] childs)
            {
                this.length = new byte[freqs.Length];
                var length = childs.Length / 2;
                var num1 = (length + 1) / 2;
                var num2 = 0;
                for (var index = 0; index < this.maxLength; ++index)
                {
                    bl_counts[index] = 0;
                }
                var numArray = new int[length];
                numArray[length - 1] = 0;
                for (var index = length - 1; index >= 0; --index)
                {
                    if (childs[2 * index + 1] != -1)
                    {
                        var num3 = numArray[index] + 1;
                        if (num3 > maxLength)
                        {
                            num3 = maxLength;
                            ++num2;
                        }
                        numArray[childs[2 * index]] = numArray[childs[2 * index + 1]] = num3;
                    }
                    else
                    {
                        ++bl_counts[numArray[index] - 1];
                        this.length[childs[2 * index]] = (byte)numArray[index];
                    }
                }
                if (num2 == 0)
                {
                    return;
                }
                var index1 = this.maxLength - 1;
                do
                {
                    do
                    {
                        ;
                    }
                    while (bl_counts[--index1] == 0);
                    do
                    {
                        --bl_counts[index1];
                        ++bl_counts[++index1];
                        num2 -= 1 << (maxLength - 1 - index1);
                    }
                    while (num2 > 0 && index1 < maxLength - 1);
                }
                while (num2 > 0);
                bl_counts[this.maxLength - 1] += num2;
                bl_counts[this.maxLength - 2] -= num2;
                var num4 = 2 * num1;
                for (var maxLength = this.maxLength; maxLength != 0; --maxLength)
                {
                    var blCount = bl_counts[maxLength - 1];
                    while (blCount > 0)
                    {
                        var index2 = 2 * childs[num4++];
                        if (childs[index2 + 1] == -1)
                        {
                            this.length[childs[index2]] = (byte)maxLength;
                            --blCount;
                        }
                    }
                }
            }
        }
    }
}
