// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ScanFailureEventArgs
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;

    /// <summary>Additional information for scan failure events.</summary>
    /// <seealso cref="EventArgs" />
    public class ScanFailureEventArgs : EventArgs
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.ScanFailureEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="e">   The Exception to process.</param>
        public ScanFailureEventArgs(string name, Exception e)
        {
            this.Name = name;
            this.Exception = e;
            this.ContinueRunning = true;
        }

        /// <summary>Gets or sets a value indicating whether the continue running.</summary>
        /// <value>True if continue running, false if not.</value>
        public bool ContinueRunning { get; set; }

        /// <summary>Gets the exception.</summary>
        /// <value>The exception.</value>
        public Exception Exception { get; }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }
    }
}