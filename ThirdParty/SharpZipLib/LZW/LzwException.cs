// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.LZW.LzwException
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.LZW
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>(Serializable)exception for signalling lzw errors.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.SharpZipBaseException" />
    [Serializable]
    public class LzwException : SharpZipBaseException
    {
        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.LZW.LzwException" /> class.</summary>
        public LzwException() { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.LZW.LzwException" /> class.</summary>
        /// <param name="message">The message.</param>
        public LzwException(string message)
            : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.LZW.LzwException" /> class.</summary>
        /// <param name="message">       The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public LzwException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.LZW.LzwException" /> class.</summary>
        /// <param name="info">   The information.</param>
        /// <param name="context">The context.</param>
        protected LzwException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}