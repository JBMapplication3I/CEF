// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ScanEventArgs
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;

    /// <summary>Additional information for scan events.</summary>
    /// <seealso cref="EventArgs" />
    public class ScanEventArgs : EventArgs
    {

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.ScanEventArgs" /> class.</summary>
        /// <param name="name">The name.</param>
        public ScanEventArgs(string name)
        {
            this.Name = name;
        }

        /// <summary>Gets or sets a value indicating whether the continue running.</summary>
        /// <value>True if continue running, false if not.</value>
        public bool ContinueRunning { get; set; } = true;

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }
    }
}