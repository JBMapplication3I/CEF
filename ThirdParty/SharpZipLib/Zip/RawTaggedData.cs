// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.RawTaggedData
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;

    /// <summary>A raw tagged data.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.ITaggedData" />
    public class RawTaggedData : ITaggedData
    {

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.RawTaggedData" /> class.</summary>
        /// <param name="tag">The tag.</param>
        public RawTaggedData(short tag)
        {
            TagID = tag;
        }

        /// <summary>Gets or sets the data.</summary>
        /// <value>The data.</value>
        public byte[] Data { get; set; }

        /// <inheritdoc/>
        public short TagID { get; set; }

        /// <inheritdoc/>
        public byte[] GetData()
        {
            return Data;
        }

        /// <inheritdoc/>
        public void SetData(byte[] data, int offset, int count)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            Data = new byte[count];
            Array.Copy(data, offset, Data, 0, count);
        }
    }
}
