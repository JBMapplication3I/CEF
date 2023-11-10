// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipException
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.GZip
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>(Serializable)exception for signalling zip errors.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.SharpZipBaseException" />
    [Serializable]
    public class GZipException : SharpZipBaseException
    {
        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipException" /> class.</summary>
        public GZipException() { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipException" /> class.</summary>
        /// <param name="message">The message.</param>
        public GZipException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipException" /> class.</summary>
        /// <param name="message">       The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public GZipException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipException" /> class.</summary>
        /// <param name="info">   The information.</param>
        /// <param name="context">The context.</param>
        protected GZipException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}