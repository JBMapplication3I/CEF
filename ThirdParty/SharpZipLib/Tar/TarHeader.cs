// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarHeader
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.Text;

    /// <summary>A tar header.</summary>
    /// <seealso cref="ICloneable" />
    public class TarHeader : ICloneable
    {
        /// <summary>The chksumlen.</summary>
        public const int CHKSUMLEN = 8;

        /// <summary>The chksumofs.</summary>
        public const int CHKSUMOFS = 148;

        /// <summary>The devlen.</summary>
        public const int DEVLEN = 8;

        /// <summary>The gidlen.</summary>
        public const int GIDLEN = 8;

        /// <summary>The gnamelen.</summary>
        public const int GNAMELEN = 32;

        /// <summary>The gnu tmagic.</summary>
        public const string GNU_TMAGIC = "ustar  ";

        /// <summary>The line feed ACL.</summary>
        public const byte LF_ACL = 65;

        /// <summary>The line feed block.</summary>
        public const byte LF_BLK = 52;

        /// <summary>The line feed character.</summary>
        public const byte LF_CHR = 51;

        /// <summary>The line feed contig.</summary>
        public const byte LF_CONTIG = 55;

        /// <summary>The line feed dir.</summary>
        public const byte LF_DIR = 53;

        /// <summary>The line feed extattr.</summary>
        public const byte LF_EXTATTR = 69;

        /// <summary>The line feed FIFO.</summary>
        public const byte LF_FIFO = 54;

        /// <summary>The line feed ghdr.</summary>
        public const byte LF_GHDR = 103;

        /// <summary>The line feed gnu dumpdir.</summary>
        public const byte LF_GNU_DUMPDIR = 68;

        /// <summary>The line feed gnu longlink.</summary>
        public const byte LF_GNU_LONGLINK = 75;

        /// <summary>The line feed gnu longname.</summary>
        public const byte LF_GNU_LONGNAME = 76;

        /// <summary>The line feed gnu multivol.</summary>
        public const byte LF_GNU_MULTIVOL = 77;

        /// <summary>List of names of the line feed gnus.</summary>
        public const byte LF_GNU_NAMES = 78;

        /// <summary>The line feed gnu sparse.</summary>
        public const byte LF_GNU_SPARSE = 83;

        /// <summary>The line feed gnu volhdr.</summary>
        public const byte LF_GNU_VOLHDR = 86;

        /// <summary>The line feed link.</summary>
        public const byte LF_LINK = 49;

        /// <summary>The line feed meta.</summary>
        public const byte LF_META = 73;

        /// <summary>The line feed normal.</summary>
        public const byte LF_NORMAL = 48;

        /// <summary>The line feed oldnorm.</summary>
        public const byte LF_OLDNORM = 0;

        /// <summary>The line feed symlink.</summary>
        public const byte LF_SYMLINK = 50;

        /// <summary>The line feed xhdr.</summary>
        public const byte LF_XHDR = 120;

        /// <summary>The magiclen.</summary>
        public const int MAGICLEN = 6;

        /// <summary>The modelen.</summary>
        public const int MODELEN = 8;

        /// <summary>The modtimelen.</summary>
        public const int MODTIMELEN = 12;

        /// <summary>The namelen.</summary>
        public const int NAMELEN = 100;

        /// <summary>The sizelen.</summary>
        public const int SIZELEN = 12;

        /// <summary>The tmagic.</summary>
        public const string TMAGIC = "ustar ";

        /// <summary>The uidlen.</summary>
        public const int UIDLEN = 8;

        /// <summary>The unamelen.</summary>
        public const int UNAMELEN = 32;

        /// <summary>The versionlen.</summary>
        public const int VERSIONLEN = 2;

        /// <summary>The time conversion factor.</summary>
        private const long timeConversionFactor = 10000000;

        /// <summary>The default group identifier.</summary>
        internal static int defaultGroupId;

        /// <summary>The default group name.</summary>
        internal static string defaultGroupName = "None";

        /// <summary>The default user.</summary>
        internal static string defaultUser;

        /// <summary>The default user identifier.</summary>
        internal static int defaultUserId;

        /// <summary>Set the group identifier as belongs to.</summary>
        internal static int groupIdAsSet;

        /// <summary>Set the group name as belongs to.</summary>
        internal static string groupNameAsSet = "None";

        /// <summary>Set the user identifier as belongs to.</summary>
        internal static int userIdAsSet;

        /// <summary>Set the user name as belongs to.</summary>
        internal static string userNameAsSet;

        /// <summary>The date time 1970.</summary>
        private static readonly DateTime dateTime1970 = new(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>Name of the group.</summary>
        private string groupName;

        /// <summary>Name of the link.</summary>
        private string linkName;

        /// <summary>The magic.</summary>
        private string magic;

        /// <summary>The modifier time.</summary>
        private DateTime modTime;

        /// <summary>The name.</summary>
        private string name;

        /// <summary>The size.</summary>
        private long size;

        /// <summary>Name of the user.</summary>
        private string userName;

        /// <summary>The version.</summary>
        private string version;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarHeader" /> class.</summary>
        public TarHeader()
        {
            Magic = "ustar ";
            Version = " ";
            Name = string.Empty;
            LinkName = string.Empty;
            UserId = defaultUserId;
            GroupId = defaultGroupId;
            UserName = defaultUser;
            GroupName = defaultGroupName;
            Size = 0L;
        }

        /// <summary>Gets the checksum.</summary>
        /// <value>The checksum.</value>
        public int Checksum { get; private set; }

        /// <summary>Gets or sets the development major.</summary>
        /// <value>The development major.</value>
        public int DevMajor { get; set; }

        /// <summary>Gets or sets the development minor.</summary>
        /// <value>The development minor.</value>
        public int DevMinor { get; set; }

        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        public int GroupId { get; set; }

        /// <summary>Gets or sets the name of the group.</summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            get => groupName;
            set
            {
                if (value == null)
                {
                    groupName = "None";
                }
                else
                {
                    groupName = value;
                }
            }
        }

        /// <summary>Gets a value indicating whether this TarHeader is checksum valid.</summary>
        /// <value>True if this TarHeader is checksum valid, false if not.</value>
        public bool IsChecksumValid { get; private set; }

        /// <summary>Gets or sets the name of the link.</summary>
        /// <value>The name of the link.</value>
        public string LinkName
        {
            get => linkName;
            set => linkName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>Gets or sets the magic.</summary>
        /// <value>The magic.</value>
        public string Magic
        {
            get => magic;
            set => magic = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>Gets or sets the mode.</summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        /// <summary>Gets or sets the modifier time.</summary>
        /// <value>The modifier time.</value>
        public DateTime ModTime
        {
            get => modTime;
            set
            {
                if (value < dateTime1970)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "ModTime cannot be before Jan 1st 1970");
                }
                modTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
            }
        }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name
        {
            get => name;
            set => name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>Gets or sets the size.</summary>
        /// <value>The size.</value>
        public long Size
        {
            get => size;
            set
                => size = value >= 0L
                    ? value
                    : throw new ArgumentOutOfRangeException(nameof(value), "Cannot be less than zero");
        }

        /// <summary>Gets or sets the type flag.</summary>
        /// <value>The type flag.</value>
        public byte TypeFlag { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int UserId { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get => userName;
            set
            {
                if (value != null)
                {
                    userName = value.Substring(0, Math.Min(32, value.Length));
                }
                else
                {
                    var str = Environment.UserName;
                    if (str.Length > 32)
                    {
                        str = str.Substring(0, 32);
                    }
                    userName = str;
                }
            }
        }

        /// <summary>Gets or sets the version.</summary>
        /// <value>The version.</value>
        public string Version
        {
            get => version;
            set => version = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>Gets ASCII bytes.</summary>
        /// <param name="toAdd">       to add.</param>
        /// <param name="nameOffset">  The name offset.</param>
        /// <param name="buffer">      The buffer.</param>
        /// <param name="bufferOffset">The buffer offset.</param>
        /// <param name="length">      The length.</param>
        /// <returns>The ASCII bytes.</returns>
        public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (toAdd == null)
            {
                throw new ArgumentNullException(nameof(toAdd));
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            for (var index = 0; index < length && nameOffset + index < toAdd.Length; ++index)
            {
                buffer[bufferOffset + index] = (byte)toAdd[nameOffset + index];
            }
            return bufferOffset + length;
        }

        /// <summary>Gets long octal bytes.</summary>
        /// <param name="value"> The value.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>The long octal bytes.</returns>
        public static int GetLongOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            return GetOctalBytes(value, buffer, offset, length);
        }

        /// <summary>Gets name bytes.</summary>
        /// <param name="name">        The name.</param>
        /// <param name="nameOffset">  The name offset.</param>
        /// <param name="buffer">      The buffer.</param>
        /// <param name="bufferOffset">The buffer offset.</param>
        /// <param name="length">      The length.</param>
        /// <returns>The name bytes.</returns>
        public static int GetNameBytes(StringBuilder name, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            return GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length);
        }

        /// <summary>Gets name bytes.</summary>
        /// <param name="name">        The name.</param>
        /// <param name="nameOffset">  The name offset.</param>
        /// <param name="buffer">      The buffer.</param>
        /// <param name="bufferOffset">The buffer offset.</param>
        /// <param name="length">      The length.</param>
        /// <returns>The name bytes.</returns>
        public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            int num;
            for (num = 0; num < length - 1 && nameOffset + num < name.Length; ++num)
            {
                buffer[bufferOffset + num] = (byte)name[nameOffset + num];
            }
            for (; num < length; ++num)
            {
                buffer[bufferOffset + num] = 0;
            }
            return bufferOffset + length;
        }

        /// <summary>Gets name bytes.</summary>
        /// <param name="name">  The name.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>The name bytes.</returns>
        public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            return GetNameBytes(name.ToString(), 0, buffer, offset, length);
        }

        /// <summary>Gets name bytes.</summary>
        /// <param name="name">  The name.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>The name bytes.</returns>
        public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            return GetNameBytes(name, 0, buffer, offset, length);
        }

        /// <summary>Gets octal bytes.</summary>
        /// <param name="value"> The value.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>The octal bytes.</returns>
        public static int GetOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            var num1 = length - 1;
            buffer[offset + num1] = 0;
            var num2 = num1 - 1;
            if (value > 0L)
            {
                for (var index = value; num2 >= 0 && index > 0L; --num2)
                {
                    buffer[offset + num2] = (byte)(48U + (byte)((ulong)index & 7UL));
                    index >>= 3;
                }
            }
            for (; num2 >= 0; --num2)
            {
                buffer[offset + num2] = 48;
            }
            return offset + length;
        }

        /// <summary>Parse name.</summary>
        /// <param name="header">The header.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>A StringBuilder.</returns>
        public static StringBuilder ParseName(byte[] header, int offset, int length)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be less than zero");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Cannot be less than zero");
            }
            if (offset + length > header.Length)
            {
                throw new ArgumentException("Exceeds header size", nameof(length));
            }
            var stringBuilder = new StringBuilder(length);
            for (var index = offset; index < offset + length && header[index] != (byte)0; ++index)
            {
                stringBuilder.Append((char)header[index]);
            }
            return stringBuilder;
        }

        /// <summary>Parse octal.</summary>
        /// <param name="header">The header.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>A long.</returns>
        public static long ParseOctal(byte[] header, int offset, int length)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }
            long num1 = 0;
            var flag = true;
            var num2 = offset + length;
            for (var index = offset; index < num2 && header[index] != (byte)0; ++index)
            {
                if (header[index] == 32 || header[index] == 48)
                {
                    if (!flag)
                    {
                        if (header[index] == 32)
                        {
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                flag = false;
                num1 = (num1 << 3) + (header[index] - 48);
            }
            return num1;
        }

        /// <inheritdoc/>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is TarHeader tarHeader
                && name == tarHeader.name
                && Mode == tarHeader.Mode
                && UserId == tarHeader.UserId
                && GroupId == tarHeader.GroupId
                && Size == tarHeader.Size
                && ModTime == tarHeader.ModTime
                && Checksum == tarHeader.Checksum
                && TypeFlag == tarHeader.TypeFlag
                && LinkName == tarHeader.LinkName
                && Magic == tarHeader.Magic
                && Version == tarHeader.Version
                && UserName == tarHeader.UserName
                && GroupName == tarHeader.GroupName
                && DevMajor == tarHeader.DevMajor
                && DevMinor == tarHeader.DevMinor;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>(This method is obsolete) gets the name.</summary>
        /// <returns>The name.</returns>
        [Obsolete("Use the Name property instead", true)]
        public string GetName()
        {
            return name;
        }

        /// <summary>Parse buffer.</summary>
        /// <param name="header">The header.</param>
        public void ParseBuffer(byte[] header)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }
            var offset1 = 0;
            name = ParseName(header, offset1, 100).ToString();
            var offset2 = offset1 + 100;
            Mode = (int)ParseOctal(header, offset2, 8);
            var offset3 = offset2 + 8;
            UserId = (int)ParseOctal(header, offset3, 8);
            var offset4 = offset3 + 8;
            GroupId = (int)ParseOctal(header, offset4, 8);
            var offset5 = offset4 + 8;
            Size = ParseOctal(header, offset5, 12);
            var offset6 = offset5 + 12;
            ModTime = GetDateTimeFromCTime(ParseOctal(header, offset6, 12));
            var offset7 = offset6 + 12;
            Checksum = (int)ParseOctal(header, offset7, 8);
            var num = offset7 + 8;
            var numArray = header;
            var index = num;
            var offset8 = index + 1;
            TypeFlag = numArray[index];
            LinkName = ParseName(header, offset8, 100).ToString();
            var offset9 = offset8 + 100;
            Magic = ParseName(header, offset9, 6).ToString();
            var offset10 = offset9 + 6;
            Version = ParseName(header, offset10, 2).ToString();
            var offset11 = offset10 + 2;
            UserName = ParseName(header, offset11, 32).ToString();
            var offset12 = offset11 + 32;
            GroupName = ParseName(header, offset12, 32).ToString();
            var offset13 = offset12 + 32;
            DevMajor = (int)ParseOctal(header, offset13, 8);
            var offset14 = offset13 + 8;
            DevMinor = (int)ParseOctal(header, offset14, 8);
            IsChecksumValid = Checksum == MakeCheckSum(header);
        }

        /// <summary>Writes a header.</summary>
        /// <param name="outBuffer">Buffer for out data.</param>
        public void WriteHeader(byte[] outBuffer)
        {
            if (outBuffer == null)
            {
                throw new ArgumentNullException(nameof(outBuffer));
            }
            var offset1 = 0;
            var nameBytes1 = GetNameBytes(Name, outBuffer, offset1, 100);
            var octalBytes1 = GetOctalBytes(Mode, outBuffer, nameBytes1, 8);
            var octalBytes2 = GetOctalBytes(UserId, outBuffer, octalBytes1, 8);
            var octalBytes3 = GetOctalBytes(GroupId, outBuffer, octalBytes2, 8);
            var longOctalBytes1 = GetLongOctalBytes(Size, outBuffer, octalBytes3, 12);
            var longOctalBytes2 = GetLongOctalBytes(GetCTime(ModTime), outBuffer, longOctalBytes1, 12);
            var offset2 = longOctalBytes2;
            for (var index = 0; index < 8; ++index)
            {
                outBuffer[longOctalBytes2++] = 32;
            }
            var numArray = outBuffer;
            var index1 = longOctalBytes2;
            var offset3 = index1 + 1;
            int typeFlag = TypeFlag;
            numArray[index1] = (byte)typeFlag;
            var nameBytes2 = GetNameBytes(LinkName, outBuffer, offset3, 100);
            var asciiBytes = GetAsciiBytes(Magic, 0, outBuffer, nameBytes2, 6);
            var nameBytes3 = GetNameBytes(Version, outBuffer, asciiBytes, 2);
            var nameBytes4 = GetNameBytes(UserName, outBuffer, nameBytes3, 32);
            var offset4 = GetNameBytes(GroupName, outBuffer, nameBytes4, 32);
            if (TypeFlag == 51 || TypeFlag == 52)
            {
                var octalBytes4 = GetOctalBytes(DevMajor, outBuffer, offset4, 8);
                offset4 = GetOctalBytes(DevMinor, outBuffer, octalBytes4, 8);
            }
            while (offset4 < outBuffer.Length)
            {
                outBuffer[offset4++] = 0;
            }
            Checksum = ComputeCheckSum(outBuffer);
            GetCheckSumOctalBytes(Checksum, outBuffer, offset2, 8);
            IsChecksumValid = true;
        }

        /// <summary>Restore set values.</summary>
        internal static void RestoreSetValues()
        {
            defaultUserId = userIdAsSet;
            defaultUser = userNameAsSet;
            defaultGroupId = groupIdAsSet;
            defaultGroupName = groupNameAsSet;
        }

        /// <summary>Sets value defaults.</summary>
        /// <param name="userId">   Identifier for the user.</param>
        /// <param name="userName"> Name of the user.</param>
        /// <param name="groupId">  Identifier for the group.</param>
        /// <param name="groupName">Name of the group.</param>
        internal static void SetValueDefaults(int userId, string userName, int groupId, string groupName)
        {
            defaultUserId = userIdAsSet = userId;
            defaultUser = userNameAsSet = userName;
            defaultGroupId = groupIdAsSet = groupId;
            defaultGroupName = groupNameAsSet = groupName;
        }

        /// <summary>Calculates the check sum.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The calculated check sum.</returns>
        private static int ComputeCheckSum(byte[] buffer)
        {
            var num = 0;
            for (var index = 0; index < buffer.Length; ++index)
            {
                num += buffer[index];
            }
            return num;
        }

        /// <summary>Gets check sum octal bytes.</summary>
        /// <param name="value"> The value.</param>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>The check sum octal bytes.</returns>
        private static int GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            GetOctalBytes(value, buffer, offset, length - 1);
            return offset + length;
        }

        /// <summary>Gets c time.</summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The c time.</returns>
        private static int GetCTime(DateTime dateTime)
        {
            return (int)((dateTime.Ticks - dateTime1970.Ticks) / 10000000L);
        }

        /// <summary>Gets date time from c time.</summary>
        /// <param name="ticks">The ticks.</param>
        /// <returns>The date time from c time.</returns>
        private static DateTime GetDateTimeFromCTime(long ticks)
        {
            try
            {
                return new DateTime(dateTime1970.Ticks + ticks * 10000000L);
            }
            catch (ArgumentOutOfRangeException)
            {
                return dateTime1970;
            }
        }

        /// <summary>Makes check sum.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>An int.</returns>
        private static int MakeCheckSum(byte[] buffer)
        {
            var num = 0;
            for (var index = 0; index < 148; ++index)
            {
                num += buffer[index];
            }
            for (var index = 0; index < 8; ++index)
            {
                num += 32;
            }
            for (var index = 156; index < buffer.Length; ++index)
            {
                num += buffer[index];
            }
            return num;
        }
    }
}
