// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffDimensions
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;

    /// <summary>The XLS biff dimensions.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffDimensions : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffDimensions"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffDimensions(byte[] bytes, uint offset, ExcelBinaryReader reader)
            : base(bytes, offset, reader)
        {
        }

        /// <summary>Gets the first column.</summary>
        /// <value>The first column.</value>
        public ushort FirstColumn => !IsV8 ? ReadUInt16(4) : ReadUInt16(8);

        /// <summary>Gets the first row.</summary>
        /// <value>The first row.</value>
        public uint FirstRow => !IsV8 ? ReadUInt16(0) : ReadUInt32(0);

        /// <summary>Gets or sets a value indicating whether this XlsBiffDimensions is v 8.</summary>
        /// <value>True if this XlsBiffDimensions is v 8, false if not.</value>
        public bool IsV8 { get; set; } = true;

        /// <summary>Gets or sets the last column.</summary>
        /// <value>The last column.</value>
        public ushort LastColumn
        {
            get => !IsV8 ? ReadUInt16(6) : (ushort)((ReadUInt16(9) >> 8) + 1);
            set => throw new NotImplementedException();
        }

        /// <summary>Gets the last row.</summary>
        /// <value>The last row.</value>
        public uint LastRow => !IsV8 ? ReadUInt16(2) : ReadUInt32(4);
    }
}
