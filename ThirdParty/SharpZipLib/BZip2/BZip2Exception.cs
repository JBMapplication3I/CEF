// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2Exception
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.BZip2
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>(Serializable)exception for signalling zip 2 errors.</summary>
    /// <seealso cref="SharpZipBaseException" />
    [Serializable]
    public class BZip2Exception : SharpZipBaseException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.BZip2.BZip2Exception" />
        ///     class.
        /// </summary>
        public BZip2Exception() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.BZip2.BZip2Exception" />
        ///     class.
        /// </summary>
        /// <param name="message">The message.</param>
        public BZip2Exception(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.BZip2.BZip2Exception" />
        ///     class.
        /// </summary>
        /// <param name="message">  The message.</param>
        /// <param name="exception">The exception.</param>
        public BZip2Exception(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.BZip2.BZip2Exception" />
        ///     class.
        /// </summary>
        /// <param name="info">   The information.</param>
        /// <param name="context">The context.</param>
        protected BZip2Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}