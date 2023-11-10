// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Inflater
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;
    using Checksums;
    using Streams;

    /// <summary>An inflater.</summary>
    public class Inflater
    {
        /// <summary>The decode blocks.</summary>
        private const int DECODE_BLOCKS = 2;

        /// <summary>The decode chksum.</summary>
        private const int DECODE_CHKSUM = 11;

        /// <summary>Dictionary of decodes.</summary>
        private const int DECODE_DICT = 1;

        /// <summary>The decode dynamic header.</summary>
        private const int DECODE_DYN_HEADER = 6;

        /// <summary>The decode header.</summary>
        private const int DECODE_HEADER = 0;

        /// <summary>The decode huffman.</summary>
        private const int DECODE_HUFFMAN = 7;

        /// <summary>The decode huffman distance.</summary>
        private const int DECODE_HUFFMAN_DIST = 9;

        /// <summary>The decode huffman distbits.</summary>
        private const int DECODE_HUFFMAN_DISTBITS = 10;

        /// <summary>The decode huffman lenbits.</summary>
        private const int DECODE_HUFFMAN_LENBITS = 8;

        /// <summary>The decode stored.</summary>
        private const int DECODE_STORED = 5;

        /// <summary>The first decode stored length.</summary>
        private const int DECODE_STORED_LEN1 = 3;

        /// <summary>The second decode stored length.</summary>
        private const int DECODE_STORED_LEN2 = 4;

        /// <summary>The finished.</summary>
        private const int FINISHED = 12;

        /// <summary>The cpdext.</summary>
        private static readonly int[] CPDEXT = new int[30]
        {
            0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12, 13, 13,
        };

        /// <summary>The cpdist.</summary>
        private static readonly int[] CPDIST = new int[30]
        {
            1,
            2,
            3,
            4,
            5,
            7,
            9,
            13,
            17,
            25,
            33,
            49,
            65,
            97,
            129,
            193,
            257,
            385,
            513,
            769,
            1025,
            1537,
            2049,
            3073,
            4097,
            6145,
            8193,
            12289,
            16385,
            24577,
        };

        /// <summary>The cplens.</summary>
        private static readonly int[] CPLENS = new int[29]
        {
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            13,
            15,
            17,
            19,
            23,
            27,
            31,
            35,
            43,
            51,
            59,
            67,
            83,
            99,
            115,
            131,
            163,
            195,
            227,
            258,
        };

        /// <summary>The cplext.</summary>
        private static readonly int[] CPLEXT = new int[29]
        {
            0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 0,
        };

        /// <summary>The adler.</summary>
        private readonly Adler32 adler;

        /// <summary>The distance tree.</summary>
        private InflaterHuffmanTree distTree;

        /// <summary>The dynamic header.</summary>
        private InflaterDynHeader dynHeader;

        /// <summary>The input.</summary>
        private readonly StreamManipulator input;

        /// <summary>True if this Inflater is last block.</summary>
        private bool isLastBlock;

        /// <summary>The litlen tree.</summary>
        private InflaterHuffmanTree litlenTree;

        /// <summary>The mode.</summary>
        private int mode;

        /// <summary>The needed bits.</summary>
        private int neededBits;

        /// <summary>True to no header.</summary>
        private readonly bool noHeader;

        /// <summary>The output window.</summary>
        private readonly OutputWindow outputWindow;

        /// <summary>The read adler.</summary>
        private int readAdler;

        /// <summary>The rep distance.</summary>
        private int repDist;

        /// <summary>Length of the rep.</summary>
        private int repLength;

        /// <summary>The total in.</summary>
        private long totalIn;

        /// <summary>Length of the uncompr.</summary>
        private int uncomprLen;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Inflater" />
        ///     class.
        /// </summary>
        public Inflater() : this(false) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Inflater" />
        ///     class.
        /// </summary>
        /// <param name="noHeader">True to no header.</param>
        public Inflater(bool noHeader)
        {
            this.noHeader = noHeader;
            adler = new Adler32();
            input = new StreamManipulator();
            outputWindow = new OutputWindow();
            mode = noHeader ? 2 : 0;
        }

        /// <summary>Gets the adler.</summary>
        /// <value>The adler.</value>
        public int Adler => !IsNeedingDictionary ? (int)adler.Value : readAdler;

        /// <summary>Gets a value indicating whether this Inflater is finished.</summary>
        /// <value>True if this Inflater is finished, false if not.</value>
        public bool IsFinished => mode == 12 && outputWindow.GetAvailable() == 0;

        /// <summary>Gets a value indicating whether this Inflater is needing dictionary.</summary>
        /// <value>True if this Inflater is needing dictionary, false if not.</value>
        public bool IsNeedingDictionary => mode == 1 && neededBits == 0;

        /// <summary>Gets a value indicating whether this Inflater is needing input.</summary>
        /// <value>True if this Inflater is needing input, false if not.</value>
        public bool IsNeedingInput => input.IsNeedingInput;

        /// <summary>Gets the remaining input.</summary>
        /// <value>The remaining input.</value>
        public int RemainingInput => input.AvailableBytes;

        /// <summary>Gets the total number of in.</summary>
        /// <value>The total number of in.</value>
        public long TotalIn => totalIn - RemainingInput;

        /// <summary>Gets the total number of out.</summary>
        /// <value>The total number of out.</value>
        public long TotalOut { get; private set; }

        /// <summary>Inflates the given buffer.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>An int.</returns>
        public int Inflate(byte[] buffer)
        {
            return buffer != null ? Inflate(buffer, 0, buffer.Length) : throw new ArgumentNullException(nameof(buffer));
        }

        /// <summary>Inflates.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        /// <returns>An int.</returns>
        public int Inflate(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "count cannot be negative");
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be negative");
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentException("count exceeds buffer bounds");
            }
            if (count == 0)
            {
                if (!IsFinished)
                {
                    Decode();
                }
                return 0;
            }
            var num = 0;
            do
            {
                if (mode != 11)
                {
                    var count1 = outputWindow.CopyOutput(buffer, offset, count);
                    if (count1 > 0)
                    {
                        adler.Update(buffer, offset, count1);
                        offset += count1;
                        num += count1;
                        TotalOut += count1;
                        count -= count1;
                        if (count == 0)
                        {
                            return num;
                        }
                    }
                }
            }
            while (Decode() || outputWindow.GetAvailable() > 0 && mode != 11);
            return num;
        }

        /// <summary>Resets this Inflater.</summary>
        public void Reset()
        {
            mode = noHeader ? 2 : 0;
            totalIn = 0L;
            TotalOut = 0L;
            input.Reset();
            outputWindow.Reset();
            dynHeader = null;
            litlenTree = null;
            distTree = null;
            isLastBlock = false;
            adler.Reset();
        }

        /// <summary>Sets a dictionary.</summary>
        /// <param name="buffer">The buffer.</param>
        public void SetDictionary(byte[] buffer)
        {
            SetDictionary(buffer, 0, buffer.Length);
        }

        /// <summary>Sets a dictionary.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="index"> Zero-based index of the.</param>
        /// <param name="count"> Number of.</param>
        public void SetDictionary(byte[] buffer, int index, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (!IsNeedingDictionary)
            {
                throw new InvalidOperationException("Dictionary is not needed");
            }
            adler.Update(buffer, index, count);
            if ((int)adler.Value != readAdler)
            {
                throw new SharpZipBaseException("Wrong adler checksum");
            }
            adler.Reset();
            outputWindow.CopyDict(buffer, index, count);
            mode = 2;
        }

        /// <summary>Sets an input.</summary>
        /// <param name="buffer">The buffer.</param>
        public void SetInput(byte[] buffer)
        {
            SetInput(buffer, 0, buffer.Length);
        }

        /// <summary>Sets an input.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="index"> Zero-based index of the.</param>
        /// <param name="count"> Number of.</param>
        public void SetInput(byte[] buffer, int index, int count)
        {
            input.SetInput(buffer, index, count);
            totalIn += count;
        }

        /// <summary>Decodes this Inflater.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool Decode()
        {
            switch (mode)
            {
                case 0:
                {
                    return DecodeHeader();
                }
                case 1:
                {
                    return DecodeDict();
                }
                case 2:
                {
                    if (isLastBlock)
                    {
                        if (noHeader)
                        {
                            mode = 12;
                            return false;
                        }
                        input.SkipToByteBoundary();
                        neededBits = 32;
                        mode = 11;
                        return true;
                    }
                    var num1 = input.PeekBits(3);
                    if (num1 < 0)
                    {
                        return false;
                    }
                    input.DropBits(3);
                    if ((num1 & 1) != 0)
                    {
                        isLastBlock = true;
                    }
                    switch (num1 >> 1)
                    {
                        case 0:
                        {
                            input.SkipToByteBoundary();
                            mode = 3;
                            break;
                        }
                        case 1:
                        {
                            litlenTree = InflaterHuffmanTree.defLitLenTree;
                            distTree = InflaterHuffmanTree.defDistTree;
                            mode = 7;
                            break;
                        }
                        case 2:
                        {
                            dynHeader = new InflaterDynHeader();
                            mode = 6;
                            break;
                        }
                        default:
                        {
                            throw new SharpZipBaseException("Unknown block type " + num1);
                        }
                    }
                    return true;
                }
                case 3:
                {
                    if ((uncomprLen = input.PeekBits(16)) < 0)
                    {
                        return false;
                    }
                    input.DropBits(16);
                    mode = 4;
                    goto case 4;
                }
                case 4:
                {
                    var num2 = input.PeekBits(16);
                    if (num2 < 0)
                    {
                        return false;
                    }
                    input.DropBits(16);
                    if (num2 != (uncomprLen ^ ushort.MaxValue))
                    {
                        throw new SharpZipBaseException("broken uncompressed block");
                    }
                    mode = 5;
                    goto case 5;
                }
                case 5:
                {
                    uncomprLen -= outputWindow.CopyStored(input, uncomprLen);
                    if (uncomprLen != 0)
                    {
                        return !input.IsNeedingInput;
                    }
                    mode = 2;
                    return true;
                }
                case 6:
                {
                    if (!dynHeader.Decode(input))
                    {
                        return false;
                    }
                    litlenTree = dynHeader.BuildLitLenTree();
                    distTree = dynHeader.BuildDistTree();
                    mode = 7;
                    goto case 7;
                }
                case 7:
                case 8:
                case 9:
                case 10:
                {
                    return DecodeHuffman();
                }
                case 11:
                {
                    return DecodeChksum();
                }
                case 12:
                {
                    return false;
                }
                default:
                {
                    throw new SharpZipBaseException("Inflater.Decode unknown mode");
                }
            }
        }

        /// <summary>Determines if we can decode chksum.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool DecodeChksum()
        {
            for (; neededBits > 0; neededBits -= 8)
            {
                var num = input.PeekBits(8);
                if (num < 0)
                {
                    return false;
                }
                input.DropBits(8);
                readAdler = (readAdler << 8) | num;
            }
            if ((int)adler.Value != readAdler)
            {
                throw new SharpZipBaseException(
                    "Adler chksum doesn't match: " + (int)adler.Value + " vs. " + readAdler);
            }
            mode = 12;
            return false;
        }

        /// <summary>Determines if we can decode dictionary.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool DecodeDict()
        {
            for (; neededBits > 0; neededBits -= 8)
            {
                var num = input.PeekBits(8);
                if (num < 0)
                {
                    return false;
                }
                input.DropBits(8);
                readAdler = (readAdler << 8) | num;
            }
            return false;
        }

        /// <summary>Determines if we can decode header.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool DecodeHeader()
        {
            var num1 = input.PeekBits(16);
            if (num1 < 0)
            {
                return false;
            }
            input.DropBits(16);
            var num2 = ((num1 << 8) | (num1 >> 8)) & ushort.MaxValue;
            if (num2 % 31 != 0)
            {
                throw new SharpZipBaseException("Header checksum illegal");
            }
            if ((num2 & 3840) != 2048)
            {
                throw new SharpZipBaseException("Compression Method unknown");
            }
            if ((num2 & 32) == 0)
            {
                mode = 2;
            }
            else
            {
                mode = 1;
                neededBits = 32;
            }
            return true;
        }

        /// <summary>Determines if we can decode huffman.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool DecodeHuffman()
        {
            var freeSpace = outputWindow.GetFreeSpace();
            while (freeSpace >= 258)
            {
                switch (mode)
                {
                    case 7:
                    {
                        int symbol1;
                        while (((symbol1 = litlenTree.GetSymbol(input)) & -256) == 0)
                        {
                            outputWindow.Write(symbol1);
                            if (--freeSpace < 258)
                            {
                                return true;
                            }
                        }
                        if (symbol1 < 257)
                        {
                            if (symbol1 < 0)
                            {
                                return false;
                            }
                            distTree = null;
                            litlenTree = null;
                            mode = 2;
                            return true;
                        }
                        try
                        {
                            repLength = CPLENS[symbol1 - 257];
                            neededBits = CPLEXT[symbol1 - 257];
                            goto case 8;
                        }
                        catch (Exception)
                        {
                            throw new SharpZipBaseException("Illegal rep length code");
                        }
                    }
                    case 8:
                    {
                        if (neededBits > 0)
                        {
                            mode = 8;
                            var num = input.PeekBits(neededBits);
                            if (num < 0)
                            {
                                return false;
                            }
                            input.DropBits(neededBits);
                            repLength += num;
                        }
                        mode = 9;
                        goto case 9;
                    }
                    case 9:
                    {
                        var symbol2 = distTree.GetSymbol(input);
                        if (symbol2 < 0)
                        {
                            return false;
                        }
                        try
                        {
                            repDist = CPDIST[symbol2];
                            neededBits = CPDEXT[symbol2];
                            goto case 10;
                        }
                        catch (Exception)
                        {
                            throw new SharpZipBaseException("Illegal rep dist code");
                        }
                    }
                    case 10:
                    {
                        if (neededBits > 0)
                        {
                            mode = 10;
                            var num = input.PeekBits(neededBits);
                            if (num < 0)
                            {
                                return false;
                            }
                            input.DropBits(neededBits);
                            repDist += num;
                        }
                        outputWindow.Repeat(repLength, repDist);
                        freeSpace -= repLength;
                        mode = 7;
                        continue;
                    }
                    default:
                    {
                        throw new SharpZipBaseException("Inflater unknown mode");
                    }
                }
            }
            return true;
        }
    }
}
