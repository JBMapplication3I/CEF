// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ExtendedUnixData
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>An extended unix data.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.ITaggedData" />
    public class ExtendedUnixData : ITaggedData
    {
        /// <summary>The create time.</summary>
        private DateTime _createTime = new(1970, 1, 1);

        /// <summary>The last access time.</summary>
        private DateTime _lastAccessTime = new(1970, 1, 1);

        /// <summary>The modification time.</summary>
        private DateTime _modificationTime = new(1970, 1, 1);

        /// <summary>Values that represent flags.</summary>
        [Flags]
        public enum Flags : byte
        {
            /// <summary>An enum constant representing the modification time option.</summary>
            ModificationTime = 1,

            /// <summary>An enum constant representing the access time option.</summary>
            AccessTime = 2,

            /// <summary>An enum constant representing the create time option.</summary>
            CreateTime = 4,
        }

        /// <summary>Gets or sets the access time.</summary>
        /// <value>The access time.</value>
        public DateTime AccessTime
        {
            get => _lastAccessTime;
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                Include |= Flags.AccessTime;
                _lastAccessTime = value;
            }
        }

        /// <summary>Gets or sets the create time.</summary>
        /// <value>The create time.</value>
        public DateTime CreateTime
        {
            get => _createTime;
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                Include |= Flags.CreateTime;
                _createTime = value;
            }
        }

        /// <summary>Gets or sets the modification time.</summary>
        /// <value>The modification time.</value>
        public DateTime ModificationTime
        {
            get => _modificationTime;
            set
            {
                if (!IsValidValue(value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                Include |= Flags.ModificationTime;
                _modificationTime = value;
            }
        }

        /// <inheritdoc/>
        public short TagID => 21589;

        /// <summary>Gets or sets the include.</summary>
        /// <value>The include.</value>
        private Flags Include { get; set; }

        /// <summary>Query if 'value' is valid value.</summary>
        /// <param name="value">The value Date/Time.</param>
        /// <returns>True if valid value, false if not.</returns>
        public static bool IsValidValue(DateTime value)
        {
            return value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);
        }

        /// <summary>Gets the data.</summary>
        /// <returns>An array of byte.</returns>
        public byte[] GetData()
        {
            using var memoryStream = new MemoryStream();
            using var zipHelperStream = new ZipHelperStream(memoryStream);
            zipHelperStream.IsStreamOwner = false;
            zipHelperStream.WriteByte((byte)Include);
            if ((Include & Flags.ModificationTime) != 0)
            {
                var totalSeconds =
                    (int)(_modificationTime.ToUniversalTime()
                        - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
                zipHelperStream.WriteLEInt(totalSeconds);
            }
            if ((Include & Flags.AccessTime) != 0)
            {
                var totalSeconds =
                    (int)(_lastAccessTime.ToUniversalTime()
                        - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
                zipHelperStream.WriteLEInt(totalSeconds);
            }
            if ((Include & Flags.CreateTime) != 0)
            {
                var totalSeconds =
                    (int)(_createTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime())
                    .TotalSeconds;
                zipHelperStream.WriteLEInt(totalSeconds);
            }
            return memoryStream.ToArray();
        }

        /// <summary>Sets a data.</summary>
        /// <param name="data"> The data.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="count">Number of.</param>
        public void SetData(byte[] data, int index, int count)
        {
            using var memoryStream = new MemoryStream(data, index, count, false);
            using var zipHelperStream = new ZipHelperStream(memoryStream);
            Include = (Flags)zipHelperStream.ReadByte();
            if ((Include & Flags.ModificationTime) != 0 && count >= 5)
            {
                var seconds = zipHelperStream.ReadLEInt();
                _modificationTime =
                    (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0))
                    .ToLocalTime();
            }
            if ((Include & Flags.AccessTime) != 0)
            {
                var seconds = zipHelperStream.ReadLEInt();
                _lastAccessTime =
                    (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0))
                    .ToLocalTime();
            }
            if ((Include & Flags.CreateTime) == 0)
            {
                return;
            }
            var seconds1 = zipHelperStream.ReadLEInt();
            _createTime =
                (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds1, 0))
                .ToLocalTime();
        }
    }
}
