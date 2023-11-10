// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflaterConstants
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;

    /// <summary>A deflater constants.</summary>
    public class DeflaterConstants
    {
        /// <summary>The debugging.</summary>
        public const bool DEBUGGING = false;

        /// <summary>The default memory level.</summary>
        public const int DEFAULT_MEM_LEVEL = 8;

        /// <summary>The deflate fast.</summary>
        public const int DEFLATE_FAST = 1;

        /// <summary>The deflate slow.</summary>
        public const int DEFLATE_SLOW = 2;

        /// <summary>The deflate stored.</summary>
        public const int DEFLATE_STORED = 0;

        /// <summary>The dynamic trees.</summary>
        public const int DYN_TREES = 2;

        /// <summary>The hash bits.</summary>
        public const int HASH_BITS = 15;

        /// <summary>The hash mask.</summary>
        public const int HASH_MASK = 32767;

        /// <summary>The hash shift.</summary>
        public const int HASH_SHIFT = 5;

        /// <summary>Size of the hash.</summary>
        public const int HASH_SIZE = 32768;

        /// <summary>The maximum distance.</summary>
        public const int MAX_DIST = 32506;

        /// <summary>A match specifying the maximum.</summary>
        public const int MAX_MATCH = 258;

        /// <summary>The maximum wbits.</summary>
        public const int MAX_WBITS = 15;

        /// <summary>The minimum lookahead.</summary>
        public const int MIN_LOOKAHEAD = 262;

        /// <summary>A match specifying the minimum.</summary>
        public const int MIN_MATCH = 3;

        /// <summary>Size of the pending buffer.</summary>
        public const int PENDING_BUF_SIZE = 65536;

        /// <summary>Dictionary of presets.</summary>
        public const int PRESET_DICT = 32;

        /// <summary>The static trees.</summary>
        public const int STATIC_TREES = 1;

        /// <summary>The stored block.</summary>
        public const int STORED_BLOCK = 0;

        /// <summary>The wmask.</summary>
        public const int WMASK = 32767;

        /// <summary>The wsize.</summary>
        public const int WSIZE = 32768;

        /// <summary>The compr function.</summary>
        public static int[] COMPR_FUNC = new int[10] { 0, 1, 1, 1, 1, 2, 2, 2, 2, 2 };

        /// <summary>Length of the good.</summary>
        public static int[] GOOD_LENGTH = new int[10] { 0, 4, 4, 4, 4, 8, 8, 8, 32, 32 };

        /// <summary>Size of the maximum block.</summary>
        public static int MAX_BLOCK_SIZE = Math.Min((int)ushort.MaxValue, 65531);

        /// <summary>The maximum chain.</summary>
        public static int[] MAX_CHAIN = new int[10] { 0, 4, 8, 32, 16, 32, 128, 256, 1024, 4096 };

        /// <summary>The maximum lazy.</summary>
        public static int[] MAX_LAZY = new int[10] { 0, 4, 5, 6, 4, 16, 16, 32, 128, 258 };

        /// <summary>Length of the nice.</summary>
        public static int[] NICE_LENGTH = new int[10] { 0, 8, 16, 32, 16, 32, 128, 128, 258, 258 };
    }
}
