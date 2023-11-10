// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.IScanFilter
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>Interface for scan filter.</summary>
    public interface IScanFilter
    {
        /// <summary>Query if 'name' is match.</summary>
        /// <param name="name">The name.</param>
        /// <returns>True if match, false if not.</returns>
        bool IsMatch(string name);
    }
}