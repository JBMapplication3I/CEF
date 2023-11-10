// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffBoundSheet
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System.Text;

    /// <summary>The XLS biff bound sheet.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffBoundSheet : XlsBiffRecord
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffBoundSheet"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffBoundSheet(byte[] bytes, uint offset, ExcelBinaryReader reader)
            : base(bytes, offset, reader)
        {
        }

        /// <summary>Values that represent sheet types.</summary>
        public enum SheetType : byte
        {
            /// <summary>An enum constant representing the worksheet option.</summary>
            Worksheet = 0,

            /// <summary>An enum constant representing the macro sheet option.</summary>
            MacroSheet = 1,

            /// <summary>An enum constant representing the chart option.</summary>
            Chart = 2,

            /// <summary>An enum constant representing the VB module option.</summary>
            VBModule = 6,
        }

        /// <summary>Values that represent sheet visibilities.</summary>
        public enum SheetVisibility : byte
        {
            /// <summary>An enum constant representing the visible option.</summary>
            Visible,

            /// <summary>An enum constant representing the hidden option.</summary>
            Hidden,

            /// <summary>An enum constant representing the very hidden option.</summary>
            VeryHidden,
        }

        /// <summary>Gets or sets a value indicating whether this XlsBiffBoundSheet is v 8.</summary>
        /// <value>True if this XlsBiffBoundSheet is v 8, false if not.</value>
        public bool IsV8 { get; set; } = true;

        /// <summary>Gets the name of the sheet.</summary>
        /// <value>The name of the sheet.</value>
        public string SheetName
        {
            get
            {
                ushort num1 = ReadByte(6);
                var num2 = 8;
                if (!IsV8)
                {
                    return Encoding.Default.GetString(m_bytes, m_readoffset + num2 - 1, num1);
                }
                return ReadByte(7) == (byte)0
                    ? Encoding.Default.GetString(m_bytes, m_readoffset + num2, num1)
                    : UseEncoding.GetString(
                        m_bytes,
                        m_readoffset + num2,
                        Helpers.IsSingleByteEncoding(UseEncoding) ? num1 : num1 * 2);
            }
        }

        /// <summary>Gets the start offset.</summary>
        /// <value>The start offset.</value>
        public uint StartOffset => ReadUInt32(0);

        /// <summary>Gets the type.</summary>
        /// <value>The type.</value>
        public SheetType Type => (SheetType)ReadByte(4);

        /// <summary>Gets or sets the use encoding.</summary>
        /// <value>The use encoding.</value>
        public Encoding UseEncoding { get; set; } = Encoding.Default;

        /// <summary>Gets the state of the visible.</summary>
        /// <value>The visible state.</value>
        public SheetVisibility VisibleState => (SheetVisibility)(ReadByte(5) & 3U);
    }
}
