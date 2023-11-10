// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.DirectoryEventArgs
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>Additional information for directory events.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Core.ScanEventArgs" />
    public class DirectoryEventArgs : ScanEventArgs
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.DirectoryEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="name">            The name.</param>
        /// <param name="hasMatchingFiles">True if this DirectoryEventArgs has matching files.</param>
        public DirectoryEventArgs(string name, bool hasMatchingFiles)
            : base(name)
        {
            this.HasMatchingFiles = hasMatchingFiles;
        }

        /// <summary>Gets a value indicating whether this DirectoryEventArgs has matching files.</summary>
        /// <value>True if this DirectoryEventArgs has matching files, false if not.</value>
        public bool HasMatchingFiles { get; }
    }
}