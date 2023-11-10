// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffBlankCell
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS biff blank cell.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffBlankCell : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffBlankCell"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffBlankCell(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>Gets the zero-based index of the column.</summary>
        /// <value>The column index.</value>
        public ushort ColumnIndex => ReadUInt16(2);

        /// <summary>Gets the zero-based index of the row.</summary>
        /// <value>The row index.</value>
        public ushort RowIndex => ReadUInt16(0);

        /// <summary>Gets the format to use.</summary>
        /// <value>The x coordinate format.</value>
        public ushort XFormat => ReadUInt16(4);
    }
}
