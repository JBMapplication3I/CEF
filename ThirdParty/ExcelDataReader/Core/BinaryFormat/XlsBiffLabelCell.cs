// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffLabelCell
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System.Text;

    /// <summary>The XLS biff label cell.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffBlankCell"/>
    internal class XlsBiffLabelCell : XlsBiffBlankCell
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffLabelCell"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffLabelCell(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader) { }

        /// <summary>Gets the length.</summary>
        /// <value>The length.</value>
        public ushort Length => ReadUInt16(6);

        /// <summary>Gets or sets the use encoding.</summary>
        /// <value>The use encoding.</value>
        public Encoding UseEncoding { get; set; } = Encoding.Default;

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                var bytes = !reader.IsV8()
                    ? ReadArray(2, Length * (Helpers.IsSingleByteEncoding(UseEncoding) ? 1 : 2))
                    : ReadArray(9, Length * (Helpers.IsSingleByteEncoding(UseEncoding) ? 1 : 2));
                return UseEncoding.GetString(bytes, 0, bytes.Length);
            }
        }
    }
}
