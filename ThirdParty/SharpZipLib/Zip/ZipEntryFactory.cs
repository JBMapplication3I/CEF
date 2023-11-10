// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipEntryFactory
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;
    using Core;

    /// <summary>A zip entry factory.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.IEntryFactory" />
    public class ZipEntryFactory : IEntryFactory
    {
        /// <summary>The fixed date time.</summary>
        private DateTime fixedDateTime_ = DateTime.Now;

        /// <summary>The name transform.</summary>
        private INameTransform nameTransform_;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipEntryFactory" />
        ///     class.
        /// </summary>
        public ZipEntryFactory()
        {
            nameTransform_ = new ZipNameTransform();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipEntryFactory" />
        ///     class.
        /// </summary>
        /// <param name="timeSetting">The time setting.</param>
        public ZipEntryFactory(TimeSetting timeSetting)
        {
            Setting = timeSetting;
            nameTransform_ = new ZipNameTransform();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipEntryFactory" />
        ///     class.
        /// </summary>
        /// <param name="time">The time Date/Time.</param>
        public ZipEntryFactory(DateTime time)
        {
            Setting = TimeSetting.Fixed;
            FixedDateTime = time;
            nameTransform_ = new ZipNameTransform();
        }

        /// <summary>Values that represent time settings.</summary>
        public enum TimeSetting
        {
            /// <summary>An enum constant representing the last write time option.</summary>
            LastWriteTime,

            /// <summary>An enum constant representing the last write time UTC option.</summary>
            LastWriteTimeUtc,

            /// <summary>An enum constant representing the create time option.</summary>
            CreateTime,

            /// <summary>An enum constant representing the create time UTC option.</summary>
            CreateTimeUtc,

            /// <summary>An enum constant representing the last access time option.</summary>
            LastAccessTime,

            /// <summary>An enum constant representing the last access time UTC option.</summary>
            LastAccessTimeUtc,

            /// <summary>An enum constant representing the fixed option.</summary>
            Fixed,
        }

        /// <summary>Gets or sets the fixed date time.</summary>
        /// <value>The fixed date time.</value>
        public DateTime FixedDateTime
        {
            get => fixedDateTime_;
            set
                => fixedDateTime_ = value.Year >= 1970
                    ? value
                    : throw new ArgumentException("Value is too old to be valid", nameof(value));
        }

        /// <summary>Gets or sets the get attributes.</summary>
        /// <value>The get attributes.</value>
        public int GetAttributes { get; set; } = -1;

        /// <summary>Gets or sets a value indicating whether this ZipEntryFactory is unicode text.</summary>
        /// <value>True if this ZipEntryFactory is unicode text, false if not.</value>
        public bool IsUnicodeText { get; set; }

        /// <inheritdoc/>
        public INameTransform NameTransform
        {
            get => nameTransform_;
            set
            {
                if (value == null)
                {
                    nameTransform_ = new ZipNameTransform();
                }
                else
                {
                    nameTransform_ = value;
                }
            }
        }

        /// <summary>Gets or sets the set attributes.</summary>
        /// <value>The set attributes.</value>
        public int SetAttributes { get; set; }

        /// <summary>Gets or sets the setting.</summary>
        /// <value>The setting.</value>
        public TimeSetting Setting { get; set; }

        /// <inheritdoc/>
        public ZipEntry MakeDirectoryEntry(string directoryName)
        {
            return MakeDirectoryEntry(directoryName, true);
        }

        /// <inheritdoc/>
        public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
        {
            var zipEntry = new ZipEntry(nameTransform_.TransformDirectory(directoryName))
            {
                IsUnicodeText = IsUnicodeText,
                Size = 0L
            };
            var num1 = 0;
            DirectoryInfo directoryInfo = null;
            if (useFileSystem)
            {
                directoryInfo = new DirectoryInfo(directoryName);
            }
            if (directoryInfo != null && directoryInfo.Exists)
            {
                zipEntry.DateTime = Setting switch
                {
                    TimeSetting.LastWriteTime => directoryInfo.LastWriteTime,
                    TimeSetting.LastWriteTimeUtc => directoryInfo.LastWriteTimeUtc,
                    TimeSetting.CreateTime => directoryInfo.CreationTime,
                    TimeSetting.CreateTimeUtc => directoryInfo.CreationTimeUtc,
                    TimeSetting.LastAccessTime => directoryInfo.LastAccessTime,
                    TimeSetting.LastAccessTimeUtc => directoryInfo.LastAccessTimeUtc,
                    TimeSetting.Fixed => fixedDateTime_,
                    _ => throw new ZipException("Unhandled time setting in MakeDirectoryEntry"),
                };
                num1 = (int)(directoryInfo.Attributes & (FileAttributes)GetAttributes);
            }
            else if (Setting == TimeSetting.Fixed)
            {
                zipEntry.DateTime = fixedDateTime_;
            }
            var num2 = num1 | SetAttributes | 16;
            zipEntry.ExternalFileAttributes = num2;
            return zipEntry;
        }

        /// <inheritdoc/>
        public ZipEntry MakeFileEntry(string fileName)
        {
            return MakeFileEntry(fileName, true);
        }

        /// <inheritdoc/>
        public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
        {
            var zipEntry = new ZipEntry(nameTransform_.TransformFile(fileName))
            {
                IsUnicodeText = IsUnicodeText
            };
            var num1 = 0;
            var flag = SetAttributes != 0;
            FileInfo fileInfo = null;
            if (useFileSystem)
            {
                fileInfo = new FileInfo(fileName);
            }
            if (fileInfo != null && fileInfo.Exists)
            {
                zipEntry.DateTime = Setting switch
                {
                    TimeSetting.LastWriteTime => fileInfo.LastWriteTime,
                    TimeSetting.LastWriteTimeUtc => fileInfo.LastWriteTimeUtc,
                    TimeSetting.CreateTime => fileInfo.CreationTime,
                    TimeSetting.CreateTimeUtc => fileInfo.CreationTimeUtc,
                    TimeSetting.LastAccessTime => fileInfo.LastAccessTime,
                    TimeSetting.LastAccessTimeUtc => fileInfo.LastAccessTimeUtc,
                    TimeSetting.Fixed => fixedDateTime_,
                    _ => throw new ZipException("Unhandled time setting in MakeFileEntry"),
                };
                zipEntry.Size = fileInfo.Length;
                flag = true;
                num1 = (int)(fileInfo.Attributes & (FileAttributes)GetAttributes);
            }
            else if (Setting == TimeSetting.Fixed)
            {
                zipEntry.DateTime = fixedDateTime_;
            }
            if (flag)
            {
                var num2 = num1 | SetAttributes;
                zipEntry.ExternalFileAttributes = num2;
            }
            return zipEntry;
        }
    }
}
