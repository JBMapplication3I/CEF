// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipConstants
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.Text;
    using System.Threading;

    /// <summary>A zip constants. This class cannot be inherited.</summary>
    public sealed class ZipConstants
    {
        /// <summary>The archive extra data signature.</summary>
        public const int ArchiveExtraDataSignature = 117853008;

        /// <summary>The cendigitalsig.</summary>
        [Obsolete("Use CentralHeaderDigitalSignaure instead")]
        public const int CENDIGITALSIG = 84233040;

        /// <summary>The cenhdr.</summary>
        [Obsolete("Use CentralHeaderBaseSize instead")]
        public const int CENHDR = 46;

        /// <summary>The censig.</summary>
        [Obsolete("Use CentralHeaderSignature instead")]
        public const int CENSIG = 33639248;

        /// <summary>The fourth censig 6.</summary>
        [Obsolete("Use Zip64CentralFileHeaderSignature instead")]
        public const int CENSIG64 = 101075792;

        /// <summary>Size of the central header base.</summary>
        public const int CentralHeaderBaseSize = 46;

        /// <summary>The central header digital signature.</summary>
        public const int CentralHeaderDigitalSignature = 84233040;

        /// <summary>The central header signature.</summary>
        public const int CentralHeaderSignature = 33639248;

        /// <summary>Size of the crypto header.</summary>
        [Obsolete("Use CryptoHeaderSize instead")]
        public const int CRYPTO_HEADER_SIZE = 12;

        /// <summary>Size of the crypto header.</summary>
        public const int CryptoHeaderSize = 12;

        /// <summary>The data descriptor signature.</summary>
        public const int DataDescriptorSignature = 134695760;

        /// <summary>Size of the data descriptor.</summary>
        public const int DataDescriptorSize = 16;

        /// <summary>The endhdr.</summary>
        [Obsolete("Use EndOfCentralRecordBaseSize instead")]
        public const int ENDHDR = 22;

        /// <summary>The end of central directory signature.</summary>
        public const int EndOfCentralDirectorySignature = 101010256;

        /// <summary>Size of the end of central record base.</summary>
        public const int EndOfCentralRecordBaseSize = 22;

        /// <summary>The endsig.</summary>
        [Obsolete("Use EndOfCentralDirectorySignature instead")]
        public const int ENDSIG = 101010256;

        /// <summary>The exthdr.</summary>
        [Obsolete("Use DataDescriptorSize instead")]
        public const int EXTHDR = 16;

        /// <summary>The extsig.</summary>
        [Obsolete("Use DataDescriptorSignature instead")]
        public const int EXTSIG = 134695760;

        /// <summary>Size of the local header base.</summary>
        public const int LocalHeaderBaseSize = 30;

        /// <summary>The local header signature.</summary>
        public const int LocalHeaderSignature = 67324752;

        /// <summary>The lochdr.</summary>
        [Obsolete("Use LocalHeaderBaseSize instead")]
        public const int LOCHDR = 30;

        /// <summary>The locsig.</summary>
        [Obsolete("Use LocalHeaderSignature instead")]
        public const int LOCSIG = 67324752;

        /// <summary>The spanningsig.</summary>
        [Obsolete("Use SpanningSignature instead")]
        public const int SPANNINGSIG = 134695760;

        /// <summary>The spanning signature.</summary>
        public const int SpanningSignature = 134695760;

        /// <summary>The spanning temporary signature.</summary>
        public const int SpanningTempSignature = 808471376;

        /// <summary>The spantempsig.</summary>
        [Obsolete("Use SpanningTempSignature instead")]
        public const int SPANTEMPSIG = 808471376;

        /// <summary>The version aes.</summary>
        public const int VERSION_AES = 51;

        /// <summary>Amount to version made by.</summary>
        [Obsolete("Use VersionMadeBy instead")]
        public const int VERSION_MADE_BY = 51;

        /// <summary>The version strong encryption.</summary>
        [Obsolete("Use VersionStrongEncryption instead")]
        public const int VERSION_STRONG_ENCRYPTION = 50;

        /// <summary>Amount to version made by.</summary>
        public const int VersionMadeBy = 51;

        /// <summary>The version strong encryption.</summary>
        public const int VersionStrongEncryption = 50;

        /// <summary>The fourth version zip 6.</summary>
        public const int VersionZip64 = 45;

        /// <summary>The zip 64 central dir locator signature.</summary>
        public const int Zip64CentralDirLocatorSignature = 117853008;

        /// <summary>The zip 64 central file header signature.</summary>
        public const int Zip64CentralFileHeaderSignature = 101075792;

        /// <summary>Size of the zip 64 data descriptor.</summary>
        public const int Zip64DataDescriptorSize = 24;

        /// <summary>
        ///     Prevents a default instance of the ICSharpCode.SharpZipLib.Zip.ZipConstants class from being
        ///     created.
        /// </summary>
        private ZipConstants() { }

        /// <summary>Gets or sets the default code page.</summary>
        /// <value>The default code page.</value>
        public static int DefaultCodePage { get; set; } = Thread.CurrentThread.CurrentCulture.TextInfo.OEMCodePage;

        /// <summary>Converts a str to an array.</summary>
        /// <param name="str">The string.</param>
        /// <returns>The given data converted to an array.</returns>
        public static byte[] ConvertToArray(string str)
        {
            return str == null ? Array.Empty<byte>() : Encoding.GetEncoding(DefaultCodePage).GetBytes(str);
        }

        /// <summary>Converts this ZipConstants to an array.</summary>
        /// <param name="flags">The flags.</param>
        /// <param name="str">  The string.</param>
        /// <returns>The given data converted to an array.</returns>
        public static byte[] ConvertToArray(int flags, string str)
        {
            if (str == null)
            {
                return Array.Empty<byte>();
            }
            return (flags & 2048) != 0 ? Encoding.UTF8.GetBytes(str) : ConvertToArray(str);
        }

        /// <summary>Converts this ZipConstants to a string.</summary>
        /// <param name="data"> The data.</param>
        /// <param name="count">Number of.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ConvertToString(byte[] data, int count)
        {
            return data == null ? string.Empty : Encoding.GetEncoding(DefaultCodePage).GetString(data, 0, count);
        }

        /// <summary>Converts a data to a string.</summary>
        /// <param name="data">The data.</param>
        /// <returns>The given data converted to a string.</returns>
        public static string ConvertToString(byte[] data)
        {
            return data == null ? string.Empty : ConvertToString(data, data.Length);
        }

        /// <summary>Converts this ZipConstants to a string extent.</summary>
        /// <param name="flags">The flags.</param>
        /// <param name="data"> The data.</param>
        /// <param name="count">Number of.</param>
        /// <returns>The given data converted to a string extent.</returns>
        public static string ConvertToStringExt(int flags, byte[] data, int count)
        {
            if (data == null)
            {
                return string.Empty;
            }
            return (flags & 2048) != 0 ? Encoding.UTF8.GetString(data, 0, count) : ConvertToString(data, count);
        }

        /// <summary>Converts this ZipConstants to a string extent.</summary>
        /// <param name="flags">The flags.</param>
        /// <param name="data"> The data.</param>
        /// <returns>The given data converted to a string extent.</returns>
        public static string ConvertToStringExt(int flags, byte[] data)
        {
            if (data == null)
            {
                return string.Empty;
            }
            return (flags & 2048) != 0
                ? Encoding.UTF8.GetString(data, 0, data.Length)
                : ConvertToString(data, data.Length);
        }
    }
}
