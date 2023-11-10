// Decompiled with JetBrains decompiler
// Type: Excel.Exceptions.BiffRecordException
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Exceptions
{
    using System;

    /// <summary>Exception for signalling biff record errors.</summary>
    /// <seealso cref="Exception"/>
    public class BiffRecordException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Exceptions.BiffRecordException"/> class.</summary>
        public BiffRecordException() { }

        /// <summary>Initializes a new instance of the <see cref="Excel.Exceptions.BiffRecordException"/> class.</summary>
        /// <param name="message">The message.</param>
        public BiffRecordException(string message) : base(message) { }

        /// <summary>Initializes a new instance of the <see cref="Excel.Exceptions.BiffRecordException"/> class.</summary>
        /// <param name="message">       The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BiffRecordException(string message, Exception innerException) : base(message, innerException) { }
    }
}
