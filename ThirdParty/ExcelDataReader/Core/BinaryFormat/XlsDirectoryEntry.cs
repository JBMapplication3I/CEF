// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsDirectoryEntry
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.Text;
    using Exceptions;

    /// <summary>The XLS directory entry.</summary>
    internal class XlsDirectoryEntry
    {
        /// <summary>The length.</summary>
        public const int Length = 128;

        /// <summary>The bytes.</summary>
        private readonly byte[] m_bytes;

        /// <summary>The child.</summary>
        private XlsDirectoryEntry m_child;

        /// <summary>The header.</summary>
        private readonly XlsHeader m_header;

        /// <summary>The left sibling.</summary>
        private XlsDirectoryEntry m_leftSibling;

        /// <summary>The right sibling.</summary>
        private XlsDirectoryEntry m_rightSibling;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsDirectoryEntry"/> class.</summary>
        /// <param name="bytes"> The bytes.</param>
        /// <param name="header">The header.</param>
        public XlsDirectoryEntry(byte[] bytes, XlsHeader header)
        {
            m_bytes = bytes.Length >= 128
                ? bytes
                : throw new BiffRecordException("Directory Entry error: Array is too small.");
            m_header = header;
        }

        /// <summary>Gets or sets the child.</summary>
        /// <value>The child.</value>
        public XlsDirectoryEntry Child
        {
            get => m_child;
            set
            {
                if (m_child != null)
                {
                    return;
                }
                m_child = value;
            }
        }

        /// <summary>Gets the child SID.</summary>
        /// <value>The child SID.</value>
        public uint ChildSid => BitConverter.ToUInt32(m_bytes, 76);

        /// <summary>Gets the identifier of the class.</summary>
        /// <value>The identifier of the class.</value>
        public Guid ClassId
        {
            get
            {
                var b = new byte[16];
                Buffer.BlockCopy(m_bytes, 80, b, 0, 16);
                return new Guid(b);
            }
        }

        /// <summary>Gets the creation time.</summary>
        /// <value>The creation time.</value>
        public DateTime CreationTime => DateTime.FromFileTime(BitConverter.ToInt64(m_bytes, 100));

        /// <summary>Gets the color of the entry.</summary>
        /// <value>The color of the entry.</value>
        public DECOLOR EntryColor => (DECOLOR)Buffer.GetByte(m_bytes, 67);

        /// <summary>Gets the length of the entry.</summary>
        /// <value>The length of the entry.</value>
        public ushort EntryLength => BitConverter.ToUInt16(m_bytes, 64);

        /// <summary>Gets the name of the entry.</summary>
        /// <value>The name of the entry.</value>
        public string EntryName => Encoding.Unicode.GetString(m_bytes, 0, EntryLength).TrimEnd(new char[1]);

        /// <summary>Gets the type of the entry.</summary>
        /// <value>The type of the entry.</value>
        public STGTY EntryType => (STGTY)Buffer.GetByte(m_bytes, 66);

        /// <summary>Gets a value indicating whether this XlsDirectoryEntry is entry mini stream.</summary>
        /// <value>True if this XlsDirectoryEntry is entry mini stream, false if not.</value>
        public bool IsEntryMiniStream => StreamSize < m_header.MiniStreamCutoff;

        /// <summary>Gets the last write time.</summary>
        /// <value>The last write time.</value>
        public DateTime LastWriteTime => DateTime.FromFileTime(BitConverter.ToInt64(m_bytes, 108));

        /// <summary>Gets or sets the left sibling.</summary>
        /// <value>The left sibling.</value>
        public XlsDirectoryEntry LeftSibling
        {
            get => m_leftSibling;
            set
            {
                if (m_leftSibling != null)
                {
                    return;
                }
                m_leftSibling = value;
            }
        }

        /// <summary>Gets the left sibling SID.</summary>
        /// <value>The left sibling SID.</value>
        public uint LeftSiblingSid => BitConverter.ToUInt32(m_bytes, 68);

        /// <summary>Gets the type of the property.</summary>
        /// <value>The type of the property.</value>
        public uint PropType => BitConverter.ToUInt32(m_bytes, 124);

        /// <summary>Gets or sets the right sibling.</summary>
        /// <value>The right sibling.</value>
        public XlsDirectoryEntry RightSibling
        {
            get => m_rightSibling;
            set
            {
                if (m_rightSibling != null)
                {
                    return;
                }
                m_rightSibling = value;
            }
        }

        /// <summary>Gets the right sibling SID.</summary>
        /// <value>The right sibling SID.</value>
        public uint RightSiblingSid => BitConverter.ToUInt32(m_bytes, 72);

        /// <summary>Gets the stream first sector.</summary>
        /// <value>The stream first sector.</value>
        public uint StreamFirstSector => BitConverter.ToUInt32(m_bytes, 116);

        /// <summary>Gets the size of the stream.</summary>
        /// <value>The size of the stream.</value>
        public uint StreamSize => BitConverter.ToUInt32(m_bytes, 120);

        /// <summary>Gets the user flags.</summary>
        /// <value>The user flags.</value>
        public uint UserFlags => BitConverter.ToUInt32(m_bytes, 96);
    }
}
