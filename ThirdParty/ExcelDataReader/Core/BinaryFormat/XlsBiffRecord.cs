// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffRecord
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;

    /// <summary>Information about the XLS biff.</summary>
    internal class XlsBiffRecord
    {
        /// <summary>The reader.</summary>
        protected readonly ExcelBinaryReader reader;

        /// <summary>The bytes.</summary>
        protected byte[] m_bytes;

        /// <summary>The readoffset.</summary>
        protected int m_readoffset;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffRecord"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        protected XlsBiffRecord(byte[] bytes, uint offset, ExcelBinaryReader reader)
        {
            if (bytes.Length - offset < 4L)
            {
                throw new ArgumentException("Error: Buffer size is less than minimum BIFF record size.");
            }
            m_bytes = bytes;
            this.reader = reader;
            m_readoffset = 4 + (int)offset;
            if (reader.ReadOption == ReadOption.Strict && bytes.Length < offset + Size)
            {
                throw new ArgumentException("BIFF Stream error: Buffer size is less than entry length.");
            }
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public BIFFRECORDTYPE ID => (BIFFRECORDTYPE)BitConverter.ToUInt16(m_bytes, m_readoffset - 4);

        /// <summary>Gets a value indicating whether this XlsBiffRecord is cell.</summary>
        /// <value>True if this XlsBiffRecord is cell, false if not.</value>
        public bool IsCell
        {
            get
            {
                var flag = false;
                var id = ID;
                if ((uint)id <= 253U)
                {
                    switch (id)
                    {
                        case BIFFRECORDTYPE.MULRK:
                        case BIFFRECORDTYPE.MULBLANK:
                        case BIFFRECORDTYPE.LABELSST:
                        {
                            break;
                        }
                        default:
                        {
                            goto label_4;
                        }
                    }
                }
                else
                {
                    switch (id)
                    {
                        case BIFFRECORDTYPE.BLANK:
                        case BIFFRECORDTYPE.NUMBER:
                        case BIFFRECORDTYPE.BOOLERR:
                        case BIFFRECORDTYPE.RK:
                        case BIFFRECORDTYPE.FORMULA:
                        {
                            break;
                        }
                        default:
                        {
                            goto label_4;
                        }
                    }
                }
                flag = true;
            label_4:
                return flag;
            }
        }

        /// <summary>Gets the size of the record.</summary>
        /// <value>The size of the record.</value>
        public ushort RecordSize => BitConverter.ToUInt16(m_bytes, m_readoffset - 2);

        /// <summary>Gets the size.</summary>
        /// <value>The size.</value>
        public int Size => 4 + RecordSize;

        /// <summary>Gets the bytes.</summary>
        /// <value>The bytes.</value>
        internal byte[] Bytes => m_bytes;

        /// <summary>Gets the offset.</summary>
        /// <value>The offset.</value>
        internal int Offset => m_readoffset - 4;

        /// <summary>Gets a record.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>The record.</returns>
        public static XlsBiffRecord GetRecord(byte[] bytes, uint offset, ExcelBinaryReader reader)
        {
            if (offset >= bytes.Length)
            {
                return null;
            }
            var uint16 = (BIFFRECORDTYPE)BitConverter.ToUInt16(bytes, (int)offset);
            if ((uint)uint16 <= 218U)
            {
                if ((uint)uint16 <= 94U)
                {
                    if ((uint)uint16 <= 30U)
                    {
                        switch (uint16)
                        {
                            case BIFFRECORDTYPE.BLANK_OLD:
                            case BIFFRECORDTYPE.BOOLERR_OLD:
                            {
                                goto label_26;
                            }
                            case BIFFRECORDTYPE.INTEGER_OLD:
                            {
                                goto label_30;
                            }
                            case BIFFRECORDTYPE.NUMBER_OLD:
                            {
                                goto label_31;
                            }
                            case BIFFRECORDTYPE.LABEL_OLD:
                            {
                                goto label_28;
                            }
                            case BIFFRECORDTYPE.FORMULA_OLD:
                            {
                                goto label_34;
                            }
                            case BIFFRECORDTYPE.BOF_V2:
                            {
                                break;
                            }
                            case BIFFRECORDTYPE.EOF:
                            {
                                return new XlsBiffEOF(bytes, offset, reader);
                            }
                            case BIFFRECORDTYPE.FORMAT_V23:
                            {
                                goto label_35;
                            }
                            default:
                            {
                                goto label_50;
                            }
                        }
                    }
                    else
                    {
                        switch (uint16)
                        {
                            case BIFFRECORDTYPE.RECORD1904:
                            {
                                return new XlsBiffSimpleValueRecord(bytes, offset, reader);
                            }
                            case BIFFRECORDTYPE.CONTINUE:
                            {
                                return new XlsBiffContinue(bytes, offset, reader);
                            }
                            case BIFFRECORDTYPE.WINDOW1:
                            {
                                return new XlsBiffWindow1(bytes, offset, reader);
                            }
                            case BIFFRECORDTYPE.BACKUP:
                            {
                                return new XlsBiffSimpleValueRecord(bytes, offset, reader);
                            }
                            case BIFFRECORDTYPE.CODEPAGE:
                            {
                                return new XlsBiffSimpleValueRecord(bytes, offset, reader);
                            }
                            case BIFFRECORDTYPE.UNCALCED:
                            {
                                return new XlsBiffUncalced(bytes, offset, reader);
                            }
                            default:
                            {
                                goto label_50;
                            }
                        }
                    }
                }
                else if ((uint)uint16 <= 141U)
                {
                    if (uint16 == BIFFRECORDTYPE.BOUNDSHEET)
                    {
                        return new XlsBiffBoundSheet(bytes, offset, reader);
                    }
                    if (uint16 == BIFFRECORDTYPE.HIDEOBJ)
                    {
                        return new XlsBiffSimpleValueRecord(bytes, offset, reader);
                    }
                    goto label_50;
                }
                else
                {
                    switch (uint16)
                    {
                        case BIFFRECORDTYPE.FNGROUPCOUNT:
                        {
                            return new XlsBiffSimpleValueRecord(bytes, offset, reader);
                        }
                        case BIFFRECORDTYPE.MULRK:
                        {
                            return new XlsBiffMulRKCell(bytes, offset, reader);
                        }
                        case BIFFRECORDTYPE.MULBLANK:
                        {
                            return new XlsBiffMulBlankCell(bytes, offset, reader);
                        }
                        case BIFFRECORDTYPE.RSTRING:
                        {
                            goto label_28;
                        }
                        case BIFFRECORDTYPE.DBCELL:
                        {
                            return new XlsBiffDbCell(bytes, offset, reader);
                        }
                        case BIFFRECORDTYPE.BOOKBOOL:
                        {
                            return new XlsBiffSimpleValueRecord(bytes, offset, reader);
                        }
                        default:
                        {
                            goto label_50;
                        }
                    }
                }
            }
            else if ((uint)uint16 <= 638U)
            {
                if ((uint)uint16 <= 253U)
                {
                    switch (uint16)
                    {
                        case BIFFRECORDTYPE.INTERFACEHDR:
                        {
                            return new XlsBiffInterfaceHdr(bytes, offset, reader);
                        }
                        case BIFFRECORDTYPE.SST:
                        {
                            return new XlsBiffSST(bytes, offset, reader);
                        }
                        case BIFFRECORDTYPE.LABELSST:
                        {
                            return new XlsBiffLabelSSTCell(bytes, offset, reader);
                        }
                        default:
                        {
                            goto label_50;
                        }
                    }
                }
                switch (uint16)
                {
                    case BIFFRECORDTYPE.USESELFS:
                    {
                        return new XlsBiffSimpleValueRecord(bytes, offset, reader);
                    }
                    case BIFFRECORDTYPE.DIMENSIONS:
                    {
                        return new XlsBiffDimensions(bytes, offset, reader);
                    }
                    case BIFFRECORDTYPE.BLANK:
                    case BIFFRECORDTYPE.BOOLERR:
                    {
                        goto label_26;
                    }
                    case BIFFRECORDTYPE.INTEGER:
                    {
                        goto label_30;
                    }
                    case BIFFRECORDTYPE.NUMBER:
                    {
                        goto label_31;
                    }
                    case BIFFRECORDTYPE.LABEL:
                    {
                        goto label_28;
                    }
                    case BIFFRECORDTYPE.STRING:
                    {
                        return new XlsBiffFormulaString(bytes, offset, reader);
                    }
                    case BIFFRECORDTYPE.ROW:
                    {
                        return new XlsBiffRow(bytes, offset, reader);
                    }
                    case BIFFRECORDTYPE.BOF_V3:
                    {
                        break;
                    }
                    case BIFFRECORDTYPE.INDEX:
                    {
                        return new XlsBiffIndex(bytes, offset, reader);
                    }
                    case BIFFRECORDTYPE.RK:
                    {
                        return new XlsBiffRKCell(bytes, offset, reader);
                    }
                    default:
                    {
                        goto label_50;
                    }
                }
            }
            else if ((uint)uint16 <= 1033U)
            {
                if (uint16 != BIFFRECORDTYPE.FORMULA)
                {
                    if (uint16 != BIFFRECORDTYPE.BOF_V4)
                    {
                        goto label_50;
                    }
                }
                else
                {
                    goto label_34;
                }
            }
            else
            {
                switch (uint16)
                {
                    case BIFFRECORDTYPE.FORMAT:
                    {
                        goto label_35;
                    }
                    case BIFFRECORDTYPE.QUICKTIP:
                    {
                        return new XlsBiffQuickTip(bytes, offset, reader);
                    }
                    case BIFFRECORDTYPE.BOF:
                    {
                        break;
                    }
                    default:
                    {
                        goto label_50;
                    }
                }
            }
            return new XlsBiffBOF(bytes, offset, reader);
        label_26:
            return new XlsBiffBlankCell(bytes, offset, reader);
        label_28:
            return new XlsBiffLabelCell(bytes, offset, reader);
        label_30:
            return new XlsBiffIntegerCell(bytes, offset, reader);
        label_31:
            return new XlsBiffNumberCell(bytes, offset, reader);
        label_34:
            return new XlsBiffFormulaCell(bytes, offset, reader);
        label_35:
            return new XlsBiffFormatString(bytes, offset, reader);
        label_50:
            return new XlsBiffRecord(bytes, offset, reader);
        }

        /// <summary>Reads an array.</summary>
        /// <param name="offset">The offset.</param>
        /// <param name="size">  The size.</param>
        /// <returns>An array of byte.</returns>
        public byte[] ReadArray(int offset, int size)
        {
            var numArray = new byte[size];
            Buffer.BlockCopy(m_bytes, m_readoffset + offset, numArray, 0, size);
            return numArray;
        }

        /// <summary>Reads a byte.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The byte.</returns>
        public byte ReadByte(int offset)
        {
            return Buffer.GetByte(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads a double.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The double.</returns>
        public double ReadDouble(int offset)
        {
            return BitConverter.ToDouble(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads a float.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The float.</returns>
        public float ReadFloat(int offset)
        {
            return BitConverter.ToSingle(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads int 16.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The int 16.</returns>
        public short ReadInt16(int offset)
        {
            return BitConverter.ToInt16(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads int 32.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The int 32.</returns>
        public int ReadInt32(int offset)
        {
            return BitConverter.ToInt32(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads int 64.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The int 64.</returns>
        public long ReadInt64(int offset)
        {
            return BitConverter.ToInt64(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads u int 16.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The u int 16.</returns>
        public ushort ReadUInt16(int offset)
        {
            return BitConverter.ToUInt16(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads u int 32.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The u int 32.</returns>
        public uint ReadUInt32(int offset)
        {
            return BitConverter.ToUInt32(m_bytes, m_readoffset + offset);
        }

        /// <summary>Reads u int 64.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>The u int 64.</returns>
        public ulong ReadUInt64(int offset)
        {
            return BitConverter.ToUInt64(m_bytes, m_readoffset + offset);
        }
    }
}
