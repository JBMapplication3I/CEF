// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffFormatString
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System.Text;

    /// <summary>The XLS biff format string.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffFormatString : XlsBiffRecord
    {
        /// <summary>The value.</summary>
        private string m_value;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffFormatString"/>
        /// class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffFormatString(byte[] bytes, uint offset, ExcelBinaryReader reader)
            : base(bytes, offset, reader)
        {
        }

        /// <summary>Gets the zero-based index of this XlsBiffFormatString.</summary>
        /// <value>The index.</value>
        public ushort Index => ID == BIFFRECORDTYPE.FORMAT_V23 ? (ushort)0 : ReadUInt16(0);

        /// <summary>Gets the length.</summary>
        /// <value>The length.</value>
        public ushort Length => ID == BIFFRECORDTYPE.FORMAT_V23 ? ReadByte(0) : ReadUInt16(2);

        /// <summary>Gets or sets the use encoding.</summary>
        /// <value>The use encoding.</value>
        public Encoding UseEncoding { get; set; } = Encoding.Default;

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                if (m_value == null)
                {
                    switch (ID)
                    {
                        case BIFFRECORDTYPE.FORMAT_V23:
                        {
                            m_value = UseEncoding.GetString(m_bytes, m_readoffset + 1, Length);
                            break;
                        }
                        case BIFFRECORDTYPE.FORMAT:
                        {
                            var index = m_readoffset + 5;
                            var num = ReadByte(3);
                            UseEncoding = ((int)num & 1) == 1 ? Encoding.Unicode : Encoding.Default;
                            if ((num & 4) == 1)
                            {
                                index += 4;
                            }
                            if ((num & 8) == 1)
                            {
                                index += 2;
                            }
                            m_value = UseEncoding.IsSingleByte
                                ? UseEncoding.GetString(m_bytes, index, Length)
                                : UseEncoding.GetString(m_bytes, index, Length * 2);
                            break;
                        }
                    }
                }
                return m_value;
            }
        }
    }
}
