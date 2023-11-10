// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.IEntryFactory
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using Core;

    /// <summary>Interface for entry factory.</summary>
    public interface IEntryFactory
    {
        /// <summary>Gets or sets the name transform.</summary>
        /// <value>The name transform.</value>
        INameTransform NameTransform { get; set; }

        /// <summary>Makes directory entry.</summary>
        /// <param name="directoryName">Pathname of the directory.</param>
        /// <returns>A ZipEntry.</returns>
        ZipEntry MakeDirectoryEntry(string directoryName);

        /// <summary>Makes directory entry.</summary>
        /// <param name="directoryName">Pathname of the directory.</param>
        /// <param name="useFileSystem">True to use file system.</param>
        /// <returns>A ZipEntry.</returns>
        ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

        /// <summary>Makes file entry.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <returns>A ZipEntry.</returns>
        ZipEntry MakeFileEntry(string fileName);

        /// <summary>Makes file entry.</summary>
        /// <param name="fileName">     Filename of the file.</param>
        /// <param name="useFileSystem">True to use file system.</param>
        /// <returns>A ZipEntry.</returns>
        ZipEntry MakeFileEntry(string fileName, bool useFileSystem);
    }
}
