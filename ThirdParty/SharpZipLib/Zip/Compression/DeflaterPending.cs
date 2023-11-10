// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflaterPending
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    /// <summary>A deflater pending.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.Compression.PendingBuffer" />
    public class DeflaterPending : PendingBuffer
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.DeflaterPending" /> class.
        /// </summary>
        public DeflaterPending() : base(65536) { }
    }
}
