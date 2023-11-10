// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ProgressEventArgs
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;

    /// <summary>Additional information for progress events.</summary>
    /// <seealso cref="EventArgs" />
    public class ProgressEventArgs : EventArgs
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.ProgressEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="name">     The name.</param>
        /// <param name="processed">The processed.</param>
        /// <param name="target">   Target for the.</param>
        public ProgressEventArgs(string name, long processed, long target)
        {
            this.Name = name;
            this.Processed = processed;
            this.Target = target;
        }

        /// <summary>Gets or sets a value indicating whether the continue running.</summary>
        /// <value>True if continue running, false if not.</value>
        public bool ContinueRunning { get; set; } = true;

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>Gets the percent complete.</summary>
        /// <value>The percent complete.</value>
        public float PercentComplete =>
            this.Target > 0L ? (float)(this.Processed / (double)this.Target * 100.0) : 0.0f;

        /// <summary>Gets the processed.</summary>
        /// <value>The processed.</value>
        public long Processed { get; }

        /// <summary>Gets the Target for the.</summary>
        /// <value>The target.</value>
        public long Target { get; }
    }
}