// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffSST
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>The XLS biff sst.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsBiffRecord"/>
    internal class XlsBiffSST : XlsBiffRecord
    {
        /// <summary>The continues.</summary>
        private readonly List<uint> continues = new();

        /// <summary>The strings.</summary>
        private readonly List<string> m_strings;

        /// <summary>The size.</summary>
        private uint m_size;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffSST"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="reader">The reader.</param>
        internal XlsBiffSST(byte[] bytes, uint offset, ExcelBinaryReader reader) : base(bytes, offset, reader)
        {
            m_size = RecordSize;
            m_strings = new List<string>();
        }

        /// <summary>Gets the number of. </summary>
        /// <value>The count.</value>
        public uint Count => ReadUInt32(0);

        /// <summary>Gets the number of uniques.</summary>
        /// <value>The number of uniques.</value>
        public uint UniqueCount => ReadUInt32(4);

        /// <summary>Appends a fragment.</summary>
        /// <param name="fragment">The fragment to append.</param>
        public void Append(XlsBiffContinue fragment)
        {
            continues.Add((uint)fragment.Offset);
            m_size += (uint)fragment.Size;
        }

        /// <summary>Gets a string.</summary>
        /// <param name="SSTIndex">Zero-based index of the sst.</param>
        /// <returns>The string.</returns>
        public string GetString(uint SSTIndex)
        {
            return (long)SSTIndex < (long)m_strings.Count ? m_strings[(int)SSTIndex] : string.Empty;
        }

        /// <summary>Reads the strings.</summary>
        public void ReadStrings()
        {
            var offset = (uint)(m_readoffset + 8);
            var num1 = (uint)m_readoffset + RecordSize;
            var index = 0;
            var num2 = UniqueCount;
            while (offset < num1)
            {
                var formattedUnicodeString = new XlsFormattedUnicodeString(m_bytes, offset);
                var headSize = formattedUnicodeString.HeadSize;
                var tailSize = formattedUnicodeString.TailSize;
                uint characterCount = formattedUnicodeString.CharacterCount;
                var num3 = (uint)((int)headSize
                    + (int)tailSize
                    + (int)characterCount
                    + (formattedUnicodeString.IsMultiByte ? (int)characterCount : 0));
                if (offset + num3 > num1)
                {
                    if (index >= continues.Count)
                    {
                        break;
                    }
                    var num4 = continues[index];
                    var num5 = Buffer.GetByte(m_bytes, (int)num4 + 4);
                    var bytes1 = new byte[(int)(num3 * 2U)];
                    Buffer.BlockCopy(m_bytes, (int)offset, bytes1, 0, (int)num1 - (int)offset);
                    if (num5 == 0 && formattedUnicodeString.IsMultiByte)
                    {
                        var num6 = characterCount - (num1 - headSize - offset) / 2U;
                        var bytes2 = Encoding.Unicode.GetBytes(
                            Encoding.Default.GetString(m_bytes, (int)num4 + 5, (int)num6));
                        Buffer.BlockCopy(bytes2, 0, bytes1, (int)num1 - (int)offset, bytes2.Length);
                        Buffer.BlockCopy(
                            m_bytes,
                            (int)num4 + 5 + (int)num6,
                            bytes1,
                            (int)num1 - (int)offset + (int)num6 + (int)num6,
                            (int)tailSize);
                        offset = num4 + 5U + num6 + tailSize;
                    }
                    else if (num5 == 1 && !formattedUnicodeString.IsMultiByte)
                    {
                        var num6 = characterCount - (num1 - offset - headSize);
                        var bytes2 = Encoding.Default.GetBytes(
                            Encoding.Unicode.GetString(m_bytes, (int)num4 + 5, (int)num6 + (int)num6));
                        Buffer.BlockCopy(bytes2, 0, bytes1, (int)num1 - (int)offset, bytes2.Length);
                        Buffer.BlockCopy(
                            m_bytes,
                            (int)num4 + 5 + (int)num6 + (int)num6,
                            bytes1,
                            (int)num1 - (int)offset + (int)num6,
                            (int)tailSize);
                        offset = num4 + 5U + num6 + num6 + tailSize;
                    }
                    else
                    {
                        Buffer.BlockCopy(
                            m_bytes,
                            (int)num4 + 5,
                            bytes1,
                            (int)num1 - (int)offset,
                            (int)num3 - (int)num1 + (int)offset);
                        offset = num4 + 5U + num3 - num1 + offset;
                    }
                    num1 = num4 + 4U + BitConverter.ToUInt16(m_bytes, (int)num4 + 2);
                    ++index;
                    formattedUnicodeString = new XlsFormattedUnicodeString(bytes1, 0U);
                }
                else
                {
                    offset += num3;
                    if ((int)offset == (int)num1)
                    {
                        if (index < continues.Count)
                        {
                            var num4 = continues[index];
                            offset = num4 + 4U;
                            num1 = offset + BitConverter.ToUInt16(m_bytes, (int)num4 + 2);
                            ++index;
                        }
                        else
                        {
                            num2 = 1U;
                        }
                    }
                }
                m_strings.Add(formattedUnicodeString.Value);
                --num2;
                if (num2 == 0U)
                {
                    break;
                }
            }
        }
    }
}
