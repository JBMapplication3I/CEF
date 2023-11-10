// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.StaticDiskDataSource
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>A static disk data source.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.IStaticDataSource" />
    public class StaticDiskDataSource : IStaticDataSource
    {
        /// <summary>Filename of the file.</summary>
        private readonly string fileName_;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.StaticDiskDataSource" />
        ///     class.
        /// </summary>
        /// <param name="fileName">Filename of the file.</param>
        public StaticDiskDataSource(string fileName)
        {
            fileName_ = fileName;
        }

        /// <inheritdoc/>
        public Stream GetSource()
        {
            return File.Open(fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
