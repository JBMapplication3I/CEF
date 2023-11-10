// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipConstants
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.GZip
{
    /// <summary>A zip constants. This class cannot be inherited.</summary>
    public sealed class GZipConstants
    {
        /// <summary>The fcomment.</summary>
        public const int FCOMMENT = 16;

        /// <summary>The fextra.</summary>
        public const int FEXTRA = 4;

        /// <summary>The fhcrc.</summary>
        public const int FHCRC = 2;

        /// <summary>Filename of the file.</summary>
        public const int FNAME = 8;

        /// <summary>The ftext.</summary>
        public const int FTEXT = 1;

        /// <summary>The gzip magic.</summary>
        public const int GZIP_MAGIC = 8075;

        /// <summary>
        ///     Prevents a default instance of the ICSharpCode.SharpZipLib.GZip.GZipConstants class from being
        ///     created.
        /// </summary>
        private GZipConstants() { }
    }
}