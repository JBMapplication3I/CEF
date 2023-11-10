// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DynamicDiskDataSource
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>A dynamic disk data source.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.IDynamicDataSource" />
    public class DynamicDiskDataSource : IDynamicDataSource
    {
        /// <summary>Gets a source.</summary>
        /// <param name="entry">The entry.</param>
        /// <param name="name"> The name.</param>
        /// <returns>The source.</returns>
        public Stream GetSource(ZipEntry entry, string name)
        {
            Stream stream = null;
            if (name != null)
            {
                stream = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            return stream;
        }
    }
}
