// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipEntry
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>A zip entry.</summary>
    /// <seealso cref="ICloneable" />
    public class ZipEntry : ICloneable
    {
        /// <summary>The aes encryption strength.</summary>
        private int _aesEncryptionStrength;

        /// <summary>The aes version.</summary>
        private int _aesVer;

        /// <summary>The comment.</summary>
        private string comment;

        /// <summary>Size of the compressed.</summary>
        private ulong compressedSize;

        /// <summary>The CRC.</summary>
        private uint crc;

        /// <summary>The dos time.</summary>
        private uint dosTime;

        /// <summary>The external file attributes.</summary>
        private int externalFileAttributes = -1;

        /// <summary>The extra.</summary>
        private byte[] extra;

        /// <summary>True to force zip 64.</summary>
        private bool forceZip64_;

        /// <summary>The known.</summary>
        private Known known;

        /// <summary>The method.</summary>
        private CompressionMethod method = CompressionMethod.Deflated;

        /// <summary>The size.</summary>
        private ulong size;

        /// <summary>Amount to version made by.</summary>
        private ushort versionMadeBy;

        /// <summary>The version to extract.</summary>
        private ushort versionToExtract;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipEntry" /> class.</summary>
        /// <param name="name">The name.</param>
        public ZipEntry(string name) : this(name, 0, 51, CompressionMethod.Deflated) { }

        /// <summary>
        ///     (This constructor is obsolete) initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.ZipEntry" /> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        [Obsolete("Use Clone instead")]
        public ZipEntry(ZipEntry entry)
        {
            known = entry != null ? entry.known : throw new ArgumentNullException(nameof(entry));
            Name = entry.Name;
            size = entry.size;
            compressedSize = entry.compressedSize;
            crc = entry.crc;
            dosTime = entry.dosTime;
            method = entry.method;
            comment = entry.comment;
            versionToExtract = entry.versionToExtract;
            versionMadeBy = entry.versionMadeBy;
            externalFileAttributes = entry.externalFileAttributes;
            Flags = entry.Flags;
            ZipFileIndex = entry.ZipFileIndex;
            Offset = entry.Offset;
            forceZip64_ = entry.forceZip64_;
            if (entry.extra == null)
            {
                return;
            }
            extra = new byte[entry.extra.Length];
            Array.Copy(entry.extra, 0, extra, 0, entry.extra.Length);
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipEntry" /> class.</summary>
        /// <param name="name">                    The name.</param>
        /// <param name="versionRequiredToExtract">The version required to extract.</param>
        internal ZipEntry(string name, int versionRequiredToExtract)
            : this(name, versionRequiredToExtract, 51, CompressionMethod.Deflated)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipEntry" /> class.</summary>
        /// <param name="name">                    The name.</param>
        /// <param name="versionRequiredToExtract">The version required to extract.</param>
        /// <param name="madeByInfo">              Information describing the made by.</param>
        /// <param name="method">                  The method.</param>
        internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo, CompressionMethod method)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (name.Length > ushort.MaxValue)
            {
                throw new ArgumentException("Name is too long", nameof(name));
            }
            if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(versionRequiredToExtract));
            }
            DateTime = DateTime.Now;
            this.Name = name;
            versionMadeBy = (ushort)madeByInfo;
            versionToExtract = (ushort)versionRequiredToExtract;
            this.method = method;
        }

        /// <summary>Values that represent knowns.</summary>
        [Flags]
        private enum Known : byte
        {
            /// <summary>An enum constant representing the none option.</summary>
            None = 0,

            /// <summary>An enum constant representing the size option.</summary>
            Size = 1,

            /// <summary>An enum constant representing the compressed size option.</summary>
            CompressedSize = 2,

            /// <summary>An enum constant representing the CRC option.</summary>
            Crc = 4,

            /// <summary>An enum constant representing the time option.</summary>
            Time = 8,

            /// <summary>0x10.</summary>
            ExternalAttributes = 16,
        }

        /// <summary>Gets or sets the size of the aes key.</summary>
        /// <value>The size of the aes key.</value>
        public int AESKeySize
        {
            get
            {
                switch (_aesEncryptionStrength)
                {
                    case 0:
                    {
                        return 0;
                    }
                    case 1:
                    {
                        return 128;
                    }
                    case 2:
                    {
                        return 192;
                    }
                    case 3:
                    {
                        return 256;
                    }
                    default:
                    {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                        throw new ZipException("Invalid AESEncryptionStrength " + _aesEncryptionStrength);
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                    }
                }
            }
            set
            {
                switch (value)
                {
                    case 0:
                    {
                        _aesEncryptionStrength = 0;
                        break;
                    }
                    case 128:
                    {
                        _aesEncryptionStrength = 1;
                        break;
                    }
                    case 256:
                    {
                        _aesEncryptionStrength = 3;
                        break;
                    }
                    default:
                    {
                        throw new ZipException("AESKeySize must be 0, 128 or 256: " + value);
                    }
                }
            }
        }

        /// <summary>Gets a value indicating whether we can decompress.</summary>
        /// <value>True if we can decompress, false if not.</value>
        public bool CanDecompress
            => Version <= 51
                && (Version == 10 || Version == 11 || Version == 20 || Version == 45 || Version == 51)
                && IsCompressionMethodSupported();

        /// <summary>Gets a value indicating whether the central header requires zip 64.</summary>
        /// <value>True if central header requires zip 64, false if not.</value>
        public bool CentralHeaderRequiresZip64 => LocalHeaderRequiresZip64 || Offset >= uint.MaxValue;

        /// <summary>Gets or sets the comment.</summary>
        /// <value>The comment.</value>
        public string Comment
        {
            get => comment;
            set
                => comment = value == null || value.Length <= (int)ushort.MaxValue
                    ? value
                    : throw new ArgumentOutOfRangeException(nameof(value), "cannot exceed 65535");
        }

        /// <summary>Gets or sets the size of the compressed.</summary>
        /// <value>The size of the compressed.</value>
        public long CompressedSize
        {
            get => (known & Known.CompressedSize) == Known.None ? -1L : (long)compressedSize;
            set
            {
                compressedSize = (ulong)value;
                known |= Known.CompressedSize;
            }
        }

        /// <summary>Gets or sets the compression method.</summary>
        /// <value>The compression method.</value>
        public CompressionMethod CompressionMethod
        {
            get => method;
            set
                => method = IsCompressionMethodSupported(value)
                    ? value
                    : throw new NotSupportedException("Compression method not supported");
        }

        /// <summary>Gets or sets the CRC.</summary>
        /// <value>The CRC.</value>
        public long Crc
        {
            get => (known & Known.Crc) == Known.None ? -1L : crc & (long)uint.MaxValue;
            set
            {
                crc = ((long)crc & -4294967296L) == 0L
                    ? (uint)value
                    : throw new ArgumentOutOfRangeException(nameof(value));
                known |= Known.Crc;
            }
        }

        /// <summary>Gets or sets the date time.</summary>
        /// <value>The date time.</value>
        public DateTime DateTime
        {
            get
            {
                var num1 = Math.Min(59U, (uint)(2 * ((int)dosTime & 31)));
                var num2 = Math.Min(59U, (dosTime >> 5) & 63U);
                var num3 = Math.Min(23U, (dosTime >> 11) & 31U);
                var num4 = Math.Max(1U, Math.Min(12U, (dosTime >> 21) & 15U));
                var num5 = (uint)(((int)(dosTime >> 25) & sbyte.MaxValue) + 1980);
                var day = Math.Max(1, Math.Min(DateTime.DaysInMonth((int)num5, (int)num4), (int)(dosTime >> 16) & 31));
                return new DateTime((int)num5, (int)num4, day, (int)num3, (int)num2, (int)num1);
            }
            set
            {
                var num1 = (uint)value.Year;
                var num2 = (uint)value.Month;
                var num3 = (uint)value.Day;
                var num4 = (uint)value.Hour;
                var num5 = (uint)value.Minute;
                var num6 = (uint)value.Second;
                if (num1 < 1980U)
                {
                    num1 = 1980U;
                    num2 = 1U;
                    num3 = 1U;
                    num4 = 0U;
                    num5 = 0U;
                    num6 = 0U;
                }
                else if (num1 > 2107U)
                {
                    num1 = 2107U;
                    num2 = 12U;
                    num3 = 31U;
                    num4 = 23U;
                    num5 = 59U;
                    num6 = 59U;
                }
                DosTime = (uint)(((((int)num1 - 1980) & sbyte.MaxValue) << 25)
                        | ((int)num2 << 21)
                        | ((int)num3 << 16)
                        | ((int)num4 << 11)
                        | ((int)num5 << 5))
                    | (num6 >> 1);
            }
        }

        /// <summary>Gets or sets the dos time.</summary>
        /// <value>The dos time.</value>
        public long DosTime
        {
            get => (known & Known.Time) == Known.None ? 0L : dosTime;
            set
            {
                dosTime = (uint)value;
                known |= Known.Time;
            }
        }

        /// <summary>Gets or sets the external file attributes.</summary>
        /// <value>The external file attributes.</value>
        public int ExternalFileAttributes
        {
            get => (known & Known.ExternalAttributes) == Known.None ? -1 : externalFileAttributes;
            set
            {
                externalFileAttributes = value;
                known |= Known.ExternalAttributes;
            }
        }

        /// <summary>Gets or sets information describing the extra.</summary>
        /// <value>Information describing the extra.</value>
        public byte[] ExtraData
        {
            get => extra;
            set
            {
                if (value == null)
                {
                    extra = null;
                }
                else
                {
                    extra = value.Length <= (int)ushort.MaxValue
                        ? new byte[value.Length]
                        : throw new ArgumentOutOfRangeException(nameof(value));
                    Array.Copy(value, 0, extra, 0, value.Length);
                }
            }
        }

        /// <summary>Gets or sets the flags.</summary>
        /// <value>The flags.</value>
        public int Flags { get; set; }

        /// <summary>Gets a value indicating whether this ZipEntry has CRC.</summary>
        /// <value>True if this ZipEntry has crc, false if not.</value>
        public bool HasCrc => (known & Known.Crc) != Known.None;

        /// <summary>Gets or sets the host system.</summary>
        /// <value>The host system.</value>
        public int HostSystem
        {
            get => (versionMadeBy >> 8) & byte.MaxValue;
            set
            {
                versionMadeBy &= byte.MaxValue;
                versionMadeBy |= (ushort)((value & byte.MaxValue) << 8);
            }
        }

        /// <summary>Gets or sets a value indicating whether this ZipEntry is crypted.</summary>
        /// <value>True if this ZipEntry is crypted, false if not.</value>
        public bool IsCrypted
        {
            get => (Flags & 1) != 0;
            set
            {
                if (value)
                {
                    Flags |= 1;
                }
                else
                {
                    Flags &= -2;
                }
            }
        }

        /// <summary>Gets a value indicating whether this ZipEntry is directory.</summary>
        /// <value>True if this ZipEntry is directory, false if not.</value>
        public bool IsDirectory
        {
            get
            {
                var length = Name.Length;
                return length > 0 && (Name[length - 1] == '/' || Name[length - 1] == '\\') || HasDosAttributes(16);
            }
        }

        /// <summary>Gets a value indicating whether this ZipEntry is dos entry.</summary>
        /// <value>True if this ZipEntry is dos entry, false if not.</value>
        public bool IsDOSEntry => HostSystem == 0 || HostSystem == 10;

        /// <summary>Gets a value indicating whether this ZipEntry is file.</summary>
        /// <value>True if this ZipEntry is file, false if not.</value>
        public bool IsFile => !IsDirectory && !HasDosAttributes(8);

        /// <summary>Gets or sets a value indicating whether this ZipEntry is unicode text.</summary>
        /// <value>True if this ZipEntry is unicode text, false if not.</value>
        public bool IsUnicodeText
        {
            get => (Flags & 2048) != 0;
            set
            {
                if (value)
                {
                    Flags |= 2048;
                }
                else
                {
                    Flags &= -2049;
                }
            }
        }

        /// <summary>Gets a value indicating whether the local header requires zip 64.</summary>
        /// <value>True if local header requires zip 64, false if not.</value>
        public bool LocalHeaderRequiresZip64
        {
            get
            {
                var flag = forceZip64_;
                if (!flag)
                {
                    var compressedSize = this.compressedSize;
                    if (versionToExtract == 0 && IsCrypted)
                    {
                        compressedSize += 12UL;
                    }
                    flag = (size >= uint.MaxValue || compressedSize >= uint.MaxValue)
                        && (versionToExtract == 0 || versionToExtract >= 45);
                }
                return flag;
            }
        }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>Gets or sets the offset.</summary>
        /// <value>The offset.</value>
        public long Offset { get; set; }

        /// <summary>Gets or sets the size.</summary>
        /// <value>The size.</value>
        public long Size
        {
            get => (known & Known.Size) == Known.None ? -1L : (long)size;
            set
            {
                size = (ulong)value;
                known |= Known.Size;
            }
        }

        /// <summary>Gets the version.</summary>
        /// <value>The version.</value>
        public int Version
        {
            get
            {
                if (versionToExtract != 0)
                {
                    return versionToExtract;
                }
                var num = 10;
                if (AESKeySize > 0)
                {
                    num = 51;
                }
                else if (CentralHeaderRequiresZip64)
                {
                    num = 45;
                }
                else if (CompressionMethod.Deflated == method)
                {
                    num = 20;
                }
                else if (IsDirectory)
                {
                    num = 20;
                }
                else if (IsCrypted)
                {
                    num = 20;
                }
                else if (HasDosAttributes(8))
                {
                    num = 11;
                }
                return num;
            }
        }

        /// <summary>Gets the amount to version made by.</summary>
        /// <value>Amount to version made by.</value>
        public int VersionMadeBy => versionMadeBy & byte.MaxValue;

        /// <summary>Gets or sets the zero-based index of the zip file.</summary>
        /// <value>The zip file index.</value>
        public long ZipFileIndex { get; set; } = -1;

        /// <summary>Gets the aes encryption strength.</summary>
        /// <value>The aes encryption strength.</value>
        internal byte AESEncryptionStrength => (byte)_aesEncryptionStrength;

        /// <summary>Gets the size of the aes overhead.</summary>
        /// <value>The size of the aes overhead.</value>
        internal int AESOverheadSize => 12 + AESSaltLen;

        /// <summary>Gets the length of the aes salt.</summary>
        /// <value>The length of the aes salt.</value>
        internal int AESSaltLen => AESKeySize / 16;

        /// <summary>Gets the compression method for header.</summary>
        /// <value>The compression method for header.</value>
        internal CompressionMethod CompressionMethodForHeader => AESKeySize <= 0 ? method : CompressionMethod.WinZipAES;

        /// <summary>Gets or sets the crypto check value.</summary>
        /// <value>The crypto check value.</value>
        internal byte CryptoCheckValue { get; set; }

        /// <summary>Clean name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>A string.</returns>
        public static string CleanName(string name)
        {
            if (name == null)
            {
                return string.Empty;
            }
            if (Path.IsPathRooted(name))
            {
                name = name[Path.GetPathRoot(name).Length..];
            }
            name = name.Replace("\\", "/");
            while (name.Length > 0 && name[0] == '/')
            {
                name = name.Remove(0, 1);
            }
            return name;
        }

        /// <summary>Query if 'method' is compression method supported.</summary>
        /// <param name="method">The method.</param>
        /// <returns>True if compression method supported, false if not.</returns>
        public static bool IsCompressionMethodSupported(CompressionMethod method)
        {
            return method == CompressionMethod.Deflated || method == CompressionMethod.Stored;
        }

        /// <inheritdoc/>
        public object Clone()
        {
            var zipEntry = (ZipEntry)MemberwiseClone();
            if (extra != null)
            {
                zipEntry.extra = new byte[extra.Length];
                Array.Copy(extra, 0, zipEntry.extra, 0, extra.Length);
            }
            return zipEntry;
        }

        /// <summary>Force zip 64.</summary>
        public void ForceZip64()
        {
            forceZip64_ = true;
        }

        /// <summary>Query if this ZipEntry is compression method supported.</summary>
        /// <returns>True if compression method supported, false if not.</returns>
        public bool IsCompressionMethodSupported()
        {
            return IsCompressionMethodSupported(CompressionMethod);
        }

        /// <summary>Query if this ZipEntry is zip 64 forced.</summary>
        /// <returns>True if zip 64 forced, false if not.</returns>
        public bool IsZip64Forced()
        {
            return forceZip64_;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>Process the extra data described by localHeader.</summary>
        /// <param name="localHeader">True to local header.</param>
        internal void ProcessExtraData(bool localHeader)
        {
            var extraData = new ZipExtraData(extra);
            if (extraData.Find(1))
            {
                forceZip64_ = true;
                if (extraData.ValueLength < 4)
                {
                    throw new ZipException("Extra data extended Zip64 information length is invalid");
                }
                if (localHeader || size == uint.MaxValue)
                {
                    size = (ulong)extraData.ReadLong();
                }
                if (localHeader || compressedSize == uint.MaxValue)
                {
                    compressedSize = (ulong)extraData.ReadLong();
                }
                if (!localHeader && Offset == uint.MaxValue)
                {
                    Offset = extraData.ReadLong();
                }
            }
            else if ((versionToExtract & byte.MaxValue) >= 45
                && (size == uint.MaxValue || compressedSize == uint.MaxValue))
            {
                throw new ZipException("Zip64 Extended information required but is missing.");
            }
            if (extraData.Find(10))
            {
                if (extraData.ValueLength < 4)
                {
                    throw new ZipException("NTFS Extra data invalid");
                }
                extraData.ReadInt();
                while (extraData.UnreadCount >= 4)
                {
                    var num = extraData.ReadShort();
                    var amount = extraData.ReadShort();
                    if (num == 1)
                    {
                        if (amount >= 24)
                        {
                            var fileTime = extraData.ReadLong();
                            extraData.ReadLong();
                            extraData.ReadLong();
                            DateTime = DateTime.FromFileTime(fileTime);
                        }
                        break;
                    }
                    extraData.Skip(amount);
                }
            }
            else if (extraData.Find(21589))
            {
                var valueLength = extraData.ValueLength;
                if ((extraData.ReadByte() & 1) != 0 && valueLength >= 5)
                {
                    var seconds = extraData.ReadInt();
                    DateTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0))
                        .ToLocalTime();
                }
            }
            if (method != CompressionMethod.WinZipAES)
            {
                return;
            }
            ProcessAESExtraData(extraData);
        }

        /// <summary>Query if 'attributes' has dos attributes.</summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns>True if dos attributes, false if not.</returns>
        private bool HasDosAttributes(int attributes)
        {
            var flag = false;
            if ((known & Known.ExternalAttributes) != Known.None
                && (HostSystem == 0 || HostSystem == 10)
                && (ExternalFileAttributes & attributes) == attributes)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>Process a es extra data described by extraData.</summary>
        /// <param name="extraData">Information describing the extra.</param>
        private void ProcessAESExtraData(ZipExtraData extraData)
        {
            if (!extraData.Find(39169))
            {
                throw new ZipException("AES Extra Data missing");
            }
            versionToExtract = 51;
            Flags |= 64;
            var valueLength = extraData.ValueLength;
            if (valueLength < 7)
            {
                throw new ZipException("AES Extra Data Length " + valueLength + " invalid.");
            }
            var num1 = extraData.ReadShort();
            extraData.ReadShort();
            var num2 = extraData.ReadByte();
            var num3 = extraData.ReadShort();
            _aesVer = num1;
            _aesEncryptionStrength = num2;
            method = (CompressionMethod)num3;
        }
    }
}
