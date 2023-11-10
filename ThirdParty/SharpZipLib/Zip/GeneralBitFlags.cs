// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.GeneralBitFlags
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;

    /// <summary>Values that represent general bit flags.</summary>
    [Flags]
    public enum GeneralBitFlags
    {
        /// <summary>.</summary>
        Encrypted = 1,

        /// <summary>.</summary>
        Method = 6,

        /// <summary>.</summary>
        Descriptor = 8,

        /// <summary>0x00000010.</summary>
        ReservedPKware4 = 16,

        /// <summary>0x00000020.</summary>
        Patched = 32,

        /// <summary>0x00000040.</summary>
        StrongEncryption = 64,

        /// <summary>0x00000080.</summary>
        Unused7 = 128,

        /// <summary>0x00000100.</summary>
        Unused8 = 256,

        /// <summary>0x00000200.</summary>
        Unused9 = 512,

        /// <summary>0x00000400.</summary>
        Unused10 = 1024,

        /// <summary>0x00000800.</summary>
        UnicodeText = 2048,

        /// <summary>0x00001000.</summary>
        EnhancedCompress = 4096,

        /// <summary>0x00002000.</summary>
        HeaderMasked = 8192,

        /// <summary>0x00004000.</summary>
        ReservedPkware14 = 16384,

        /// <summary>0x00008000.</summary>
        ReservedPkware15 = 32768,
    }
}
