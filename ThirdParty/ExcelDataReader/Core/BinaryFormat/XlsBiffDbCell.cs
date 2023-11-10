// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffDbCell
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System.Collections.Generic;

    /// <summary>The XLS biff database cell.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffDbCell : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffDbCell"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffDbCell(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>Gets the cell addresses.</summary>
        /// <value>The cell addresses.</value>
        public uint[] CellAddresses
        {
            get
            {
                var num = RowAddress - 20;
                var uintList = new List<uint>();
                for (var offset = 4; offset < (int)RecordSize; offset += 4)
                {
                    uintList.Add((uint)num + ReadUInt16(offset));
                }
                return uintList.ToArray();
            }
        }

        /// <summary>Gets the row address.</summary>
        /// <value>The row address.</value>
        public int RowAddress => Offset - ReadInt32(0);
    }
}
