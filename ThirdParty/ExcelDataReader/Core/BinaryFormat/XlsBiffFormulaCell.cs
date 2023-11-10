// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffFormulaCell
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.Text;

    /// <summary>The XLS biff formula cell.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffNumberCell"/>
    internal class XlsBiffFormulaCell : XlsBiffNumberCell
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffFormulaCell"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffFormulaCell(byte[] bytes, uint offset, ExcelBinaryReader reader)
            : base(bytes, offset, reader)
        {
        }

        /// <summary>A bit-field of flags for specifying formula options.</summary>
        [Flags]
        public enum FormulaFlags : ushort
        {
            /// <summary>A binary constant representing the always Calculate flag.</summary>
            AlwaysCalc = 1,


            /// <summary>A binary constant representing the Calculate on load flag.</summary>
            CalcOnLoad = 2,


            /// <summary>A binary constant representing the shared formula group flag.</summary>
            SharedFormulaGroup = 8,
        }

        /// <summary>Gets the flags.</summary>
        /// <value>The flags.</value>
        public FormulaFlags Flags => (FormulaFlags)ReadUInt16(14);

        /// <summary>Gets the formula.</summary>
        /// <value>The formula.</value>
        public string Formula
        {
            get
            {
                var bytes = ReadArray(16, FormulaLength);
                return Encoding.Default.GetString(bytes, 0, bytes.Length);
            }
        }

        /// <summary>Gets the length of the formula.</summary>
        /// <value>The length of the formula.</value>
        public byte FormulaLength => ReadByte(15);

        /// <summary>Gets or sets the use encoding.</summary>
        /// <value>The use encoding.</value>
        public Encoding UseEncoding { get; set; } = Encoding.Default;

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public new object Value
        {
            get
            {
                var num1 = ReadInt64(6);
                if ((num1 & -281474976710656L) != -281474976710656L)
                {
                    return Helpers.Int64BitsToDouble(num1);
                }
                var num2 = (byte)((ulong)num1 & byte.MaxValue);
                var num3 = (byte)((ulong)(num1 >> 16) & byte.MaxValue);
                switch (num2)
                {
                    case 0:
                    {
                        var record = GetRecord(m_bytes, (uint)(Offset + Size), reader);
                        var biffFormulaString = record.ID != BIFFRECORDTYPE.SHRFMLA
                            ? record as XlsBiffFormulaString
                            : GetRecord(m_bytes, (uint)(Offset + Size + record.Size), reader) as XlsBiffFormulaString;
                        if (biffFormulaString == null)
                        {
                            return string.Empty;
                        }
                        biffFormulaString.UseEncoding = UseEncoding;
                        return biffFormulaString.Value;
                    }
                    case 1:
                    {
                        return num3 != 0;
                    }
                    case 2:
                    {
                        return (FORMULAERROR)num3;
                    }
                    default:
                    {
                        return null;
                    }
                }
            }
        }
    }
}
