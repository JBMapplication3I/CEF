// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffIndex
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System.Collections.Generic;

    /// <summary>The XLS biff index.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffIndex : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffIndex"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffIndex(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>Gets the database cell addresses.</summary>
        /// <value>The database cell addresses.</value>
        public uint[] DbCellAddresses
        {
            get
            {
                int recordSize = RecordSize;
                var num = IsV8 ? 16 : 12;
                if (recordSize <= num)
                {
                    return System.Array.Empty<uint>();
                }
                var uintList = new List<uint>((recordSize - num) / 4);
                for (var offset = num; offset < recordSize; offset += 4)
                {
                    uintList.Add(ReadUInt32(offset));
                }
                return uintList.ToArray();
            }
        }

        /// <summary>Gets the first existing row.</summary>
        /// <value>The first existing row.</value>
        public uint FirstExistingRow => !IsV8 ? ReadUInt16(4) : ReadUInt32(4);

        /// <summary>Gets or sets a value indicating whether this XlsBiffIndex is v 8.</summary>
        /// <value>True if this XlsBiffIndex is v 8, false if not.</value>
        public bool IsV8 { get; set; } = true;

        /// <summary>Gets the last existing row.</summary>
        /// <value>The last existing row.</value>
        public uint LastExistingRow => !IsV8 ? ReadUInt16(6) : ReadUInt32(8);
    }
}
