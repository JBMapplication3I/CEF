// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffLabelSSTCell
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS biff label sst cell.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffBlankCell"/>
    internal class XlsBiffLabelSSTCell : XlsBiffBlankCell
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffLabelSSTCell"/>
        /// class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffLabelSSTCell(byte[] bytes, uint offset, ExcelBinaryReader reader)
            : base(bytes, offset, reader)
        {
        }

        /// <summary>Gets the zero-based index of the sst.</summary>
        /// <value>The sst index.</value>
        public uint SSTIndex => ReadUInt32(6);

        /// <summary>Texts the given sst.</summary>
        /// <param name="sst">The sst.</param>
        /// <returns>A string.</returns>
        public string Text(XlsBiffSST sst)
        {
            return sst.GetString(SSTIndex);
        }
    }
}
