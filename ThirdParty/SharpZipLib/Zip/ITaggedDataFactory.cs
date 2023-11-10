// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ITaggedDataFactory
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>Interface for tagged data factory.</summary>
    internal interface ITaggedDataFactory
    {
        /// <summary>Creates a new ITaggedData.</summary>
        /// <param name="tag">   The tag.</param>
        /// <param name="data">  The data.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        /// <returns>An ITaggedData.</returns>
        ITaggedData Create(short tag, byte[] data, int offset, int count);
    }
}
