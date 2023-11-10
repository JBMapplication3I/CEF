// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.PathFilter
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System.IO;

    /// <summary>A path filter.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Core.IScanFilter" />
    public class PathFilter : IScanFilter
    {
        /// <summary>A filter specifying the name.</summary>
        private readonly NameFilter nameFilter_;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.PathFilter" /> class.</summary>
        /// <param name="filter">Specifies the filter.</param>
        public PathFilter(string filter)
        {
            this.nameFilter_ = new NameFilter(filter);
        }

        /// <inheritdoc/>
        public virtual bool IsMatch(string name)
        {
            var flag = false;
            if (name != null)
            {
                flag = this.nameFilter_.IsMatch(name.Length > 0 ? Path.GetFullPath(name) : string.Empty);
            }

            return flag;
        }
    }
}