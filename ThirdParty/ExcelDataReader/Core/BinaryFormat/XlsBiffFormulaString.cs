// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffFormulaString
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System.Text;

    /// <summary>The XLS biff formula string.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffFormulaString : XlsBiffRecord
    {
        /// <summary>Number of leading bytes.</summary>
        private const int LEADING_BYTES_COUNT = 3;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffFormulaString"/>
        /// class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffFormulaString(byte[] bytes, uint offset, ExcelBinaryReader reader)
            : base(bytes, offset, reader)
        {
        }

        /// <summary>Gets the length.</summary>
        /// <value>The length.</value>
        public ushort Length => ReadUInt16(0);

        /// <summary>Gets or sets the use encoding.</summary>
        /// <value>The use encoding.</value>
        public Encoding UseEncoding { get; set; } = Encoding.Default;

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public string Value
            => ReadUInt16(1) != (ushort)0
                ? Encoding.Unicode.GetString(m_bytes, m_readoffset + 3, Length * 2)
                : UseEncoding.GetString(m_bytes, m_readoffset + 3, Length);
    }
}
