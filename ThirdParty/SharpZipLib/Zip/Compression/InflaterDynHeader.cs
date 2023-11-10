// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.InflaterDynHeader
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;
    using Streams;

    /// <summary>An inflater dynamic header.</summary>
    internal class InflaterDynHeader
    {
        /// <summary>The bllens.</summary>
        private const int BLLENS = 3;

        /// <summary>The blnum.</summary>
        private const int BLNUM = 2;

        /// <summary>The dnum.</summary>
        private const int DNUM = 1;

        /// <summary>The lens.</summary>
        private const int LENS = 4;

        /// <summary>The lnum.</summary>
        private const int LNUM = 0;

        /// <summary>The reps.</summary>
        private const int REPS = 5;

        /// <summary>The bl order.</summary>
        private static readonly int[] BL_ORDER = new int[19]
        {
            16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15,
        };

        /// <summary>The rep bits.</summary>
        private static readonly int[] repBits = new int[3] { 2, 3, 7 };

        /// <summary>The rep minimum.</summary>
        private static readonly int[] repMin = new int[3] { 3, 3, 11 };

        /// <summary>The bl lens.</summary>
        private byte[] blLens;

        /// <summary>The blnum.</summary>
        private int blnum;

        /// <summary>The bl tree.</summary>
        private InflaterHuffmanTree blTree;

        /// <summary>The dnum.</summary>
        private int dnum;

        /// <summary>Length of the last.</summary>
        private byte lastLen;

        /// <summary>The litdist lens.</summary>
        private byte[] litdistLens;

        /// <summary>The lnum.</summary>
        private int lnum;

        /// <summary>The mode.</summary>
        private int mode;

        /// <summary>Number of.</summary>
        private int num;

        /// <summary>The pointer.</summary>
        private int ptr;

        /// <summary>The rep symbol.</summary>
        private int repSymbol;

        /// <summary>Builds distance tree.</summary>
        /// <returns>An InflaterHuffmanTree.</returns>
        public InflaterHuffmanTree BuildDistTree()
        {
            var codeLengths = new byte[dnum];
            Array.Copy(litdistLens, lnum, codeLengths, 0, dnum);
            return new InflaterHuffmanTree(codeLengths);
        }

        /// <summary>Builds lit length tree.</summary>
        /// <returns>An InflaterHuffmanTree.</returns>
        public InflaterHuffmanTree BuildLitLenTree()
        {
            var codeLengths = new byte[lnum];
            Array.Copy(litdistLens, 0, codeLengths, 0, lnum);
            return new InflaterHuffmanTree(codeLengths);
        }

        /// <summary>Decodes the given input.</summary>
        /// <param name="input">The input.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Decode(StreamManipulator input)
        {
            while (true)
            {
                switch (mode)
                {
                    case 0:
                    {
                        lnum = input.PeekBits(5);
                        if (lnum >= 0)
                        {
                            lnum += 257;
                            input.DropBits(5);
                            mode = 1;
                            goto case 1;
                        }
                        else
                        {
                            goto label_2;
                        }
                    }
                    case 1:
                    {
                        dnum = input.PeekBits(5);
                        if (dnum >= 0)
                        {
                            ++dnum;
                            input.DropBits(5);
                            num = lnum + dnum;
                            litdistLens = new byte[num];
                            mode = 2;
                            goto case 2;
                        }
                        else
                        {
                            goto label_5;
                        }
                    }
                    case 2:
                    {
                        blnum = input.PeekBits(4);
                        if (blnum >= 0)
                        {
                            blnum += 4;
                            input.DropBits(4);
                            blLens = new byte[19];
                            ptr = 0;
                            mode = 3;
                            goto case 3;
                        }
                        else
                        {
                            goto label_8;
                        }
                    }
                    case 3:
                    {
                        for (; ptr < blnum; ++ptr)
                        {
                            var num = input.PeekBits(3);
                            if (num < 0)
                            {
                                return false;
                            }
                            input.DropBits(3);
                            blLens[BL_ORDER[ptr]] = (byte)num;
                        }
                        blTree = new InflaterHuffmanTree(blLens);
                        blLens = null;
                        ptr = 0;
                        mode = 4;
                        goto case 4;
                    }
                    case 4:
                    {
                        int symbol;
                        while (((symbol = blTree.GetSymbol(input)) & -16) == 0)
                        {
                            litdistLens[ptr++] = lastLen = (byte)symbol;
                            if (ptr == num)
                            {
                                return true;
                            }
                        }
                        if (symbol >= 0)
                        {
                            if (symbol >= 17)
                            {
                                lastLen = 0;
                            }
                            else if (ptr == 0)
                            {
                                goto label_23;
                            }
                            repSymbol = symbol - 16;
                            mode = 5;
                            goto case 5;
                        }
                        else
                        {
                            goto label_19;
                        }
                    }
                    case 5:
                    {
                        var repBit = repBits[repSymbol];
                        var num1 = input.PeekBits(repBit);
                        if (num1 >= 0)
                        {
                            input.DropBits(repBit);
                            var num2 = num1 + repMin[repSymbol];
                            if (ptr + num2 <= num)
                            {
                                while (num2-- > 0)
                                {
                                    litdistLens[ptr++] = lastLen;
                                }
                                if (ptr != num)
                                {
                                    mode = 4;
                                    continue;
                                }
                                goto label_32;
                            }
                            goto label_28;
                        }
                        else
                        {
                            goto label_26;
                        }
                    }
                    default:
                    {
                        continue;
                    }
                }
            }
        label_2:
            return false;
        label_5:
            return false;
        label_8:
            return false;
        label_19:
            return false;
        label_23:
            throw new SharpZipBaseException();
        label_26:
            return false;
        label_28:
            throw new SharpZipBaseException();
        label_32:
            return true;
        }
    }
}
