﻿// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.InflaterHuffmanTree
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;
    using Streams;

    /// <summary>An inflater huffman tree.</summary>
    public class InflaterHuffmanTree
    {
        /// <summary>The maximum bitlen.</summary>
        private const int MAX_BITLEN = 15;

        /// <summary>The definition distance tree.</summary>
        public static InflaterHuffmanTree defDistTree;

        /// <summary>The definition lit length tree.</summary>
        public static InflaterHuffmanTree defLitLenTree;

        /// <summary>The tree.</summary>
        private short[] tree;

        /// <summary>
        ///     Initializes static members of the ICSharpCode.SharpZipLib.Zip.Compression.InflaterHuffmanTree
        ///     class.
        /// </summary>
        static InflaterHuffmanTree()
        {
            try
            {
                var codeLengths1 = new byte[288];
                var num1 = 0;
                while (num1 < 144)
                {
                    codeLengths1[num1++] = 8;
                }
                while (num1 < 256)
                {
                    codeLengths1[num1++] = 9;
                }
                while (num1 < 280)
                {
                    codeLengths1[num1++] = 7;
                }
                while (num1 < 288)
                {
                    codeLengths1[num1++] = 8;
                }
                defLitLenTree = new InflaterHuffmanTree(codeLengths1);
                var codeLengths2 = new byte[32];
                var num2 = 0;
                while (num2 < 32)
                {
                    codeLengths2[num2++] = 5;
                }
                defDistTree = new InflaterHuffmanTree(codeLengths2);
            }
            catch (Exception)
            {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                throw new SharpZipBaseException("InflaterHuffmanTree: static tree length illegal");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            }
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.InflaterHuffmanTree" /> class.
        /// </summary>
        /// <param name="codeLengths">List of lengths of the codes.</param>
        public InflaterHuffmanTree(byte[] codeLengths)
        {
            BuildTree(codeLengths);
        }

        /// <summary>Gets a symbol.</summary>
        /// <param name="input">The input.</param>
        /// <returns>The symbol.</returns>
        public int GetSymbol(StreamManipulator input)
        {
            int index;
            if ((index = input.PeekBits(9)) >= 0)
            {
                int num1;
                if ((num1 = tree[index]) >= 0)
                {
                    input.DropBits(num1 & 15);
                    return num1 >> 4;
                }
                var num2 = -(num1 >> 4);
                var bitCount = num1 & 15;
                int num3;
                if ((num3 = input.PeekBits(bitCount)) >= 0)
                {
                    int num4 = tree[num2 | (num3 >> 9)];
                    input.DropBits(num4 & 15);
                    return num4 >> 4;
                }
                var availableBits = input.AvailableBits;
                var num5 = input.PeekBits(availableBits);
                int num6 = tree[num2 | (num5 >> 9)];
                if ((num6 & 15) > availableBits)
                {
                    return -1;
                }
                input.DropBits(num6 & 15);
                return num6 >> 4;
            }
            var availableBits1 = input.AvailableBits;
            int num = tree[input.PeekBits(availableBits1)];
            if (num < 0 || (num & 15) > availableBits1)
            {
                return -1;
            }
            input.DropBits(num & 15);
            return num >> 4;
        }

        /// <summary>Builds a tree.</summary>
        /// <param name="codeLengths">List of lengths of the codes.</param>
        private void BuildTree(byte[] codeLengths)
        {
            var numArray1 = new int[16];
            var numArray2 = new int[16];
            for (var index = 0; index < codeLengths.Length; ++index)
            {
                int codeLength = codeLengths[index];
                if (codeLength > 0)
                {
                    ++numArray1[codeLength];
                }
            }
            var num1 = 0;
            var length = 512;
            for (var index = 1; index <= 15; ++index)
            {
                numArray2[index] = num1;
                num1 += numArray1[index] << (16 - index);
                if (index >= 10)
                {
                    var num2 = numArray2[index] & 130944;
                    var num3 = num1 & 130944;
                    length += (num3 - num2) >> (16 - index);
                }
            }
            tree = new short[length];
            var num4 = 512;
            for (var index = 15; index >= 10; --index)
            {
                var num2 = num1 & 130944;
                num1 -= numArray1[index] << (16 - index);
                for (var toReverse = num1 & 130944; toReverse < num2; toReverse += 128)
                {
                    tree[DeflaterHuffman.BitReverse(toReverse)] = (short)((-num4 << 4) | index);
                    num4 += 1 << (index - 9);
                }
            }
            for (var index1 = 0; index1 < codeLengths.Length; ++index1)
            {
                int codeLength = codeLengths[index1];
                if (codeLength != 0)
                {
                    var toReverse = numArray2[codeLength];
                    int index2 = DeflaterHuffman.BitReverse(toReverse);
                    if (codeLength <= 9)
                    {
                        do
                        {
                            tree[index2] = (short)((index1 << 4) | codeLength);
                            index2 += 1 << codeLength;
                        }
                        while (index2 < 512);
                    }
                    else
                    {
                        int num2 = tree[index2 & 511];
                        var num3 = 1 << (num2 & 15);
                        var num5 = -(num2 >> 4);
                        do
                        {
                            tree[num5 | (index2 >> 9)] = (short)((index1 << 4) | codeLength);
                            index2 += 1 << codeLength;
                        }
                        while (index2 < num3);
                    }
                    numArray2[codeLength] = toReverse + (1 << (16 - codeLength));
                }
            }
        }
    }
}
