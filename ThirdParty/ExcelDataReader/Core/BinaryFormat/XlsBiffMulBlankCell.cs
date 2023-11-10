// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffMulBlankCell
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS biff mul blank cell.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffBlankCell"/>
    internal class XlsBiffMulBlankCell : XlsBiffBlankCell
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffMulBlankCell"/>
        /// class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffMulBlankCell(byte[] bytes, uint offset, ExcelBinaryReader reader)
            : base(bytes, offset, reader)
        {
        }

        /// <summary>Gets the zero-based index of the last column.</summary>
        /// <value>The last column index.</value>
        public ushort LastColumnIndex => ReadUInt16(RecordSize - 2);

        /// <summary>Gets an xf.</summary>
        /// <param name="ColumnIdx">Zero-based index of the column.</param>
        /// <returns>The xf.</returns>
        public ushort GetXF(ushort ColumnIdx)
        {
            var offset = 4 + 6 * (ColumnIdx - ColumnIndex);
            return offset > (int)RecordSize - 2 ? (ushort)0 : ReadUInt16(offset);
        }
    }
}
