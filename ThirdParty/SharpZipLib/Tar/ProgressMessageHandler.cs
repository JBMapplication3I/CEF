// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.ProgressMessageHandler
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    /// <summary>Handler, called when the progress message.</summary>
    /// <param name="archive">The archive.</param>
    /// <param name="entry">  The entry.</param>
    /// <param name="message">The message.</param>
    public delegate void ProgressMessageHandler(TarArchive archive, TarEntry entry, string message);
}