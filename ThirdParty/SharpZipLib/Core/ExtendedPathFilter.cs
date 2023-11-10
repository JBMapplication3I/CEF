// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ExtendedPathFilter
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>An extended path filter.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Core.PathFilter" />
    public class ExtendedPathFilter : PathFilter
    {
        /// <summary>The maximum date.</summary>
        private DateTime maxDate_ = DateTime.MaxValue;

        /// <summary>Size of the maximum.</summary>
        private long maxSize_ = long.MaxValue;

        /// <summary>The minimum date.</summary>
        private DateTime minDate_ = DateTime.MinValue;

        /// <summary>Size of the minimum.</summary>
        private long minSize_;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.ExtendedPathFilter" />
        ///     class.
        /// </summary>
        /// <param name="filter"> Specifies the filter.</param>
        /// <param name="minSize">Size of the minimum.</param>
        /// <param name="maxSize">Size of the maximum.</param>
        public ExtendedPathFilter(string filter, long minSize, long maxSize)
            : base(filter)
        {
            this.MinSize = minSize;
            this.MaxSize = maxSize;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.ExtendedPathFilter" />
        ///     class.
        /// </summary>
        /// <param name="filter"> Specifies the filter.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        public ExtendedPathFilter(string filter, DateTime minDate, DateTime maxDate)
            : base(filter)
        {
            this.MinDate = minDate;
            this.MaxDate = maxDate;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.ExtendedPathFilter" />
        ///     class.
        /// </summary>
        /// <param name="filter"> Specifies the filter.</param>
        /// <param name="minSize">Size of the minimum.</param>
        /// <param name="maxSize">Size of the maximum.</param>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        public ExtendedPathFilter(string filter, long minSize, long maxSize, DateTime minDate, DateTime maxDate)
            : base(filter)
        {
            this.MinSize = minSize;
            this.MaxSize = maxSize;
            this.MinDate = minDate;
            this.MaxDate = maxDate;
        }

        /// <summary>Gets or sets the maximum date.</summary>
        /// <value>The maximum date.</value>
        public DateTime MaxDate
        {
            get => this.maxDate_;
            set =>
                this.maxDate_ = !(this.minDate_ > value)
                                    ? value
                                    : throw new ArgumentOutOfRangeException(nameof(value), "Exceeds MinDate");
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

        /// <summary>Gets or sets the minimum date.</summary>
        /// <value>The minimum date.</value>
        public DateTime MinDate
        {
            get => this.minDate_;
            set =>
                this.minDate_ = !(value > this.maxDate_)
                                    ? value
                                    : throw new ArgumentOutOfRangeException(nameof(value), "Exceeds MaxDate");
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

        /// <summary>Query if 'name' is match.</summary>
        /// <param name="name">The name.</param>
        /// <returns>True if match, false if not.</returns>
        public override bool IsMatch(string name)
        {
            var flag = base.IsMatch(name);
            if (flag)
            {
                var fileInfo = new FileInfo(name);
                flag = this.MinSize <= fileInfo.Length && this.MaxSize >= fileInfo.Length
                                                       && this.MinDate <= fileInfo.LastWriteTime
                                                       && this.MaxDate >= fileInfo.LastWriteTime;
            }

            return flag;
        }
    }
}