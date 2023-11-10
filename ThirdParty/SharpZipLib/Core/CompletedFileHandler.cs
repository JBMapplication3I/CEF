// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.CompletedFileHandler
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>Handler, called when the completed file.</summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">     Scan event information.</param>
    public delegate void CompletedFileHandler(object sender, ScanEventArgs e);
}