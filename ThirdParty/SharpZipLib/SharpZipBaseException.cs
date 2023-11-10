// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.SharpZipBaseException
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>(Serializable)exception for signalling sharp zip base errors.</summary>
    /// <seealso cref="ApplicationException" />
    [Serializable]
    public class SharpZipBaseException : ApplicationException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.SharpZipBaseException" />
        ///     class.
        /// </summary>
        public SharpZipBaseException() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.SharpZipBaseException" />
        ///     class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SharpZipBaseException(string message) : base(message) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.SharpZipBaseException" />
        ///     class.
        /// </summary>
        /// <param name="message">       The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SharpZipBaseException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.SharpZipBaseException" />
        ///     class.
        /// </summary>
        /// <param name="info">   The information.</param>
        /// <param name="context">The context.</param>
        protected SharpZipBaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
