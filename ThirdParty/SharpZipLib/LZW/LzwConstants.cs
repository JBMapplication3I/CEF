// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.LZW.LzwConstants
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.LZW
{
    /// <summary>A lzw constants. This class cannot be inherited.</summary>
    public sealed class LzwConstants
    {
        /// <summary>The bit mask.</summary>
        public const int BIT_MASK = 31;

        /// <summary>The block mode mask.</summary>
        public const int BLOCK_MODE_MASK = 128;

        /// <summary>The extended mask.</summary>
        public const int EXTENDED_MASK = 32;

        /// <summary>Size of the header.</summary>
        public const int HDR_SIZE = 3;

        /// <summary>The initialise bits.</summary>
        public const int INIT_BITS = 9;

        /// <summary>The magic.</summary>
        public const int MAGIC = 8093;

        /// <summary>The maximum bits.</summary>
        public const int MAX_BITS = 16;

        /// <summary>The reserved mask.</summary>
        public const int RESERVED_MASK = 96;

        /// <summary>
        ///     Prevents a default instance of the ICSharpCode.SharpZipLib.LZW.LzwConstants class from being
        ///     created.
        /// </summary>
        private LzwConstants() { }
    }
}