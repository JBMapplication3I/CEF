// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.EntryPatchData
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>An entry patch data.</summary>
    internal class EntryPatchData
    {

        /// <summary>Gets or sets the CRC patch offset.</summary>
        /// <value>The CRC patch offset.</value>
        public long CrcPatchOffset { get; set; }

        /// <summary>Gets or sets the size patch offset.</summary>
        /// <value>The size patch offset.</value>
        public long SizePatchOffset { get; set; }
    }
}
