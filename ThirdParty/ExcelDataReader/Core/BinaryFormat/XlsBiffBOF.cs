// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffBOF
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS biff bof.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffBOF : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffBOF"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffBOF(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>Gets the identifier of the creation.</summary>
        /// <value>The identifier of the creation.</value>
        public ushort CreationID => RecordSize < (ushort)6 ? (ushort)0 : ReadUInt16(4);

        /// <summary>Gets the creation year.</summary>
        /// <value>The creation year.</value>
        public ushort CreationYear => RecordSize < (ushort)8 ? (ushort)0 : ReadUInt16(6);

        /// <summary>Gets the history flag.</summary>
        /// <value>The history flag.</value>
        public uint HistoryFlag => RecordSize < (ushort)12 ? 0U : ReadUInt32(8);

        /// <summary>Gets the minimum version to open.</summary>
        /// <value>The minimum version to open.</value>
        public uint MinVersionToOpen => RecordSize < (ushort)16 ? 0U : ReadUInt32(12);

        /// <summary>Gets the type.</summary>
        /// <value>The type.</value>
        public BIFFTYPE Type => (BIFFTYPE)ReadUInt16(2);

        /// <summary>Gets the version.</summary>
        /// <value>The version.</value>
        public ushort Version => ReadUInt16(0);
    }
}
