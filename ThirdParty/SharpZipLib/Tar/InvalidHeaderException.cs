// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.InvalidHeaderException
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>(Serializable)exception for signalling invalid header errors.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Tar.TarException" />
    [Serializable]
    public class InvalidHeaderException : TarException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.InvalidHeaderException" />
        ///     class.
        /// </summary>
        public InvalidHeaderException() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.InvalidHeaderException" />
        ///     class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidHeaderException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.InvalidHeaderException" />
        ///     class.
        /// </summary>
        /// <param name="message">  The message.</param>
        /// <param name="exception">The exception.</param>
        public InvalidHeaderException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.InvalidHeaderException" />
        ///     class.
        /// </summary>
        /// <param name="information">The information.</param>
        /// <param name="context">    The context.</param>
        protected InvalidHeaderException(SerializationInfo information, StreamingContext context)
            : base(information, context)
        {
        }
    }
}