// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffRow
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS biff row.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffRow : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffRow"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffRow(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>Gets the first defined column.</summary>
        /// <value>The first defined column.</value>
        public ushort FirstDefinedColumn => ReadUInt16(2);

        /// <summary>Gets the flags.</summary>
        /// <value>The flags.</value>
        public ushort Flags => ReadUInt16(12);

        /// <summary>Gets the last defined column.</summary>
        /// <value>The last defined column.</value>
        public ushort LastDefinedColumn => ReadUInt16(4);

        /// <summary>Gets the height of the row.</summary>
        /// <value>The height of the row.</value>
        public uint RowHeight => ReadUInt16(6);

        /// <summary>Gets the zero-based index of the row.</summary>
        /// <value>The row index.</value>
        public ushort RowIndex => ReadUInt16(0);

        /// <summary>Gets the format to use.</summary>
        /// <value>The x coordinate format.</value>
        public ushort XFormat => ReadUInt16(14);
    }
}
