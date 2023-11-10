// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DescriptorData
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>A descriptor data.</summary>
    public class DescriptorData
    {

        /// <summary>The CRC.</summary>
        private long crc;

        /// <summary>Gets or sets the size of the compressed.</summary>
        /// <value>The size of the compressed.</value>
        public long CompressedSize { get; set; }

        /// <summary>Gets or sets the CRC.</summary>
        /// <value>The CRC.</value>
        public long Crc
        {
            get => crc;
            set => crc = value & uint.MaxValue;
        }

        /// <summary>Gets or sets the size.</summary>
        /// <value>The size.</value>
        public long Size { get; set; }
    }
}
