// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.NTTaggedData
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>A NT tagged data.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.ITaggedData" />
    public class NTTaggedData : ITaggedData
    {
        /// <summary>The create time.</summary>
        private DateTime _createTime = DateTime.FromFileTime(0L);

        /// <summary>The last access time.</summary>
        private DateTime _lastAccessTime = DateTime.FromFileTime(0L);

        /// <summary>The last modification time.</summary>
        private DateTime _lastModificationTime = DateTime.FromFileTime(0L);

        /// <summary>Gets or sets the create time.</summary>
        /// <value>The create time.</value>
        public DateTime CreateTime
        {
            get => _createTime;
            set => _createTime = IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>Gets or sets the last access time.</summary>
        /// <value>The last access time.</value>
        public DateTime LastAccessTime
        {
            get => _lastAccessTime;
            set => _lastAccessTime = IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>Gets or sets the last modification time.</summary>
        /// <value>The last modification time.</value>
        public DateTime LastModificationTime
        {
            get => _lastModificationTime;
            set
                => _lastModificationTime =
                    IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <inheritdoc/>
        public short TagID => 10;

        /// <summary>Query if 'value' is valid value.</summary>
        /// <param name="value">The value Date/Time.</param>
        /// <returns>True if valid value, false if not.</returns>
        public static bool IsValidValue(DateTime value)
        {
            var flag = true;
            try
            {
                value.ToFileTimeUtc();
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        /// <inheritdoc/>
        public byte[] GetData()
        {
            using var memoryStream = new MemoryStream();
            using var zipHelperStream = new ZipHelperStream(memoryStream);
            zipHelperStream.IsStreamOwner = false;
            zipHelperStream.WriteLEInt(0);
            zipHelperStream.WriteLEShort(1);
            zipHelperStream.WriteLEShort(24);
            zipHelperStream.WriteLELong(_lastModificationTime.ToFileTime());
            zipHelperStream.WriteLELong(_lastAccessTime.ToFileTime());
            zipHelperStream.WriteLELong(_createTime.ToFileTime());
            return memoryStream.ToArray();
        }

        /// <inheritdoc/>
        public void SetData(byte[] data, int index, int count)
        {
            using var memoryStream = new MemoryStream(data, index, count, false);
            using var zipHelperStream = new ZipHelperStream(memoryStream);
            zipHelperStream.ReadLEInt();
            while (zipHelperStream.Position < zipHelperStream.Length)
            {
                var num1 = zipHelperStream.ReadLEShort();
                var num2 = zipHelperStream.ReadLEShort();
                if (num1 == 1)
                {
                    if (num2 < 24)
                    {
                        break;
                    }
                    _lastModificationTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
                    _lastAccessTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
                    _createTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
                    break;
                }
                zipHelperStream.Seek(num2, SeekOrigin.Current);
            }
        }
    }
}
