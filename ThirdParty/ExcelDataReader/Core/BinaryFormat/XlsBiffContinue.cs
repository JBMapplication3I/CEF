﻿// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffContinue
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS biff continue.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffContinue : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffContinue"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffContinue(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }
    }
}
