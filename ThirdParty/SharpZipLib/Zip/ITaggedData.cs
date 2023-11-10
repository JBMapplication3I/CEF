// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ITaggedData
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>Interface for tagged data.</summary>
    public interface ITaggedData
    {
        /// <summary>Gets the identifier of the tag.</summary>
        /// <value>The identifier of the tag.</value>
        short TagID { get; }

        /// <summary>Gets the data.</summary>
        /// <returns>An array of byte.</returns>
        byte[] GetData();

        /// <summary>Sets a data.</summary>
        /// <param name="data">  The data.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        void SetData(byte[] data, int offset, int count);
    }
}
