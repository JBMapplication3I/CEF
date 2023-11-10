// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsFormattedUnicodeString
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.Text;

    /// <summary>The XLS formatted unicode string.</summary>
    internal class XlsFormattedUnicodeString
    {
        /// <summary>The bytes.</summary>
        protected byte[] m_bytes;

        /// <summary>The offset.</summary>
        protected uint m_offset;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsFormattedUnicodeString"/>
        /// class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="offset">The offset.</param>
        public XlsFormattedUnicodeString(byte[] bytes, uint offset)
        {
            m_bytes = bytes;
            m_offset = offset;
        }

        /// <summary>A bit-field of flags for specifying formatted unicode string options.</summary>
        [Flags]
        public enum FormattedUnicodeStringFlags : byte
        {
            /// <summary>A binary constant representing the multi byte flag.</summary>
            MultiByte = 1,


            /// <summary>A binary constant representing the has extended string flag.</summary>
            HasExtendedString = 4,


            /// <summary>A binary constant representing the has formatting flag.</summary>
            HasFormatting = 8,
        }

        /// <summary>Gets the number of characters.</summary>
        /// <value>The number of characters.</value>
        public ushort CharacterCount => BitConverter.ToUInt16(m_bytes, (int)m_offset);

        /// <summary>Gets the size of the extended string.</summary>
        /// <value>The size of the extended string.</value>
        public uint ExtendedStringSize
            => !HasExtString ? 0U : BitConverter.ToUInt16(m_bytes, (int)m_offset + (HasFormatting ? 5 : 3));

        /// <summary>Gets the flags.</summary>
        /// <value>The flags.</value>
        public FormattedUnicodeStringFlags Flags
            => (FormattedUnicodeStringFlags)Buffer.GetByte(m_bytes, (int)m_offset + 2);

        /// <summary>Gets the number of formats.</summary>
        /// <value>The number of formats.</value>
        public ushort FormatCount => !HasFormatting ? (ushort)0 : BitConverter.ToUInt16(m_bytes, (int)m_offset + 3);

        /// <summary>Gets a value indicating whether this XlsFormattedUnicodeString has extent string.</summary>
        /// <value>True if this XlsFormattedUnicodeString has extent string, false if not.</value>
        public bool HasExtString => false;

        /// <summary>Gets a value indicating whether this XlsFormattedUnicodeString has formatting.</summary>
        /// <value>True if this XlsFormattedUnicodeString has formatting, false if not.</value>
        public bool HasFormatting
            => (Flags & FormattedUnicodeStringFlags.HasFormatting) == FormattedUnicodeStringFlags.HasFormatting;

        /// <summary>Gets the size of the head.</summary>
        /// <value>The size of the head.</value>
        public uint HeadSize => (uint)((HasFormatting ? 2 : 0) + (HasExtString ? 4 : 0) + 3);

        /// <summary>Gets a value indicating whether this XlsFormattedUnicodeString is multi byte.</summary>
        /// <value>True if this XlsFormattedUnicodeString is multi byte, false if not.</value>
        public bool IsMultiByte
            => (Flags & FormattedUnicodeStringFlags.MultiByte) == FormattedUnicodeStringFlags.MultiByte;

        /// <summary>Gets the size.</summary>
        /// <value>The size.</value>
        public uint Size
        {
            get
            {
                var num = (uint)((HasFormatting ? 2 + FormatCount * 4 : 0)
                    + (HasExtString ? 4 + (int)ExtendedStringSize : 0)
                    + 3);
                return !IsMultiByte ? num + CharacterCount : num + CharacterCount * 2U;
            }
        }

        /// <summary>Gets the size of the tail.</summary>
        /// <value>The size of the tail.</value>
        public uint TailSize => (uint)((HasFormatting ? 4 * FormatCount : 0) + (HasExtString ? (int)ExtendedStringSize : 0));

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        public string Value
            => !IsMultiByte
                ? Encoding.Default.GetString(m_bytes, (int)m_offset + (int)HeadSize, (int)ByteCount)
                : Encoding.Unicode.GetString(m_bytes, (int)m_offset + (int)HeadSize, (int)ByteCount);

        /// <summary>Gets the number of bytes.</summary>
        /// <value>The number of bytes.</value>
        private uint ByteCount => CharacterCount * (IsMultiByte ? 2U : 1U);
    }
}
