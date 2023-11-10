// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.NameAndSizeFilter
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>(This class is obsolete) a name and size filter.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Core.PathFilter" />
    [Obsolete("Use ExtendedPathFilter instead")]
    public class NameAndSizeFilter : PathFilter
    {
        /// <summary>Size of the maximum.</summary>
        private long maxSize_ = long.MaxValue;

        /// <summary>Size of the minimum.</summary>
        private long minSize_;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.NameAndSizeFilter" />
        ///     class.
        /// </summary>
        /// <param name="filter"> Specifies the filter.</param>
        /// <param name="minSize">Size of the minimum.</param>
        /// <param name="maxSize">Size of the maximum.</param>
        public NameAndSizeFilter(string filter, long minSize, long maxSize)
            : base(filter)
        {
            this.MinSize = minSize;
            this.MaxSize = maxSize;
        }

        /// <summary>Gets or sets the size of the maximum.</summary>
        /// <value>The size of the maximum.</value>
        public long MaxSize
        {
            get => this.maxSize_;
            set =>
                this.maxSize_ = value >= 0L && this.minSize_ <= value
                                    ? value
                                    : throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>Gets or sets the size of the minimum.</summary>
        /// <value>The size of the minimum.</value>
        public long MinSize
        {
            get => this.minSize_;
            set =>
                this.minSize_ = value >= 0L && this.maxSize_ >= value
                                    ? value
                                    : throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <inheritdoc/>
        public override bool IsMatch(string name)
        {
            var flag = base.IsMatch(name);
            if (flag)
            {
                var length = new FileInfo(name).Length;
                flag = this.MinSize <= length && this.MaxSize >= length;
            }

            return flag;
        }
    }
}