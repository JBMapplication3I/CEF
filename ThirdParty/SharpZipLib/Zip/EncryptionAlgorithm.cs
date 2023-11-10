// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.EncryptionAlgorithm
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>Values that represent encryption algorithms.</summary>
    public enum EncryptionAlgorithm
    {
        /// <summary>.</summary>
        None = 0,

        /// <summary>.</summary>
        PkzipClassic = 1,

        /// <summary>0x00006601.</summary>
        Des = 26113,

        /// <summary>0x00006602.</summary>
        RC2 = 26114,

        /// <summary>0x00006603.</summary>
        TripleDes168 = 26115,

        /// <summary>0x00006609.</summary>
        TripleDes112 = 26121,

        /// <summary>0x0000660E.</summary>
        Aes128 = 26126,

        /// <summary>0x0000660F.</summary>
        Aes192 = 26127,

        /// <summary>0x00006610.</summary>
        Aes256 = 26128,

        /// <summary>0x00006702.</summary>
        RC2Corrected = 26370,

        /// <summary>0x00006720.</summary>
        Blowfish = 26400,

        /// <summary>0x00006721.</summary>
        Twofish = 26401,

        /// <summary>0x00006801.</summary>
        RC4 = 26625,

        /// <summary>0x0000FFFF.</summary>
        Unknown = 65535,
    }
}
