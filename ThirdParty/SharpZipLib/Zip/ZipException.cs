﻿// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipException
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>(Serializable)exception for signalling zip errors.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.SharpZipBaseException" />
    [Serializable]
    public class ZipException : SharpZipBaseException
    {
        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipException" /> class.</summary>
        public ZipException() { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipException" /> class.</summary>
        /// <param name="message">The message.</param>
        public ZipException(string message) : base(message) { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipException" /> class.</summary>
        /// <param name="message">  The message.</param>
        /// <param name="exception">The exception.</param>
        public ZipException(string message, Exception exception) : base(message, exception) { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipException" /> class.</summary>
        /// <param name="info">   The information.</param>
        /// <param name="context">The context.</param>
        protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
