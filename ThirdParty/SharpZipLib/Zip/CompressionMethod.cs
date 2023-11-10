// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.CompressionMethod
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>Values that represent compression methods.</summary>
    public enum CompressionMethod
    {
        /// <summary>An enum constant representing the stored option.</summary>
        Stored = 0,

        /// <summary>An enum constant representing the deflated option.</summary>
        Deflated = 8,

        /// <summary>An enum constant representing the deflate 64 option.</summary>
        Deflate64 = 9,

        /// <summary>0x0000000B.</summary>
        BZip2 = 11,

        /// <summary>0x00000063.</summary>
        WinZipAES = 99,
    }
}
