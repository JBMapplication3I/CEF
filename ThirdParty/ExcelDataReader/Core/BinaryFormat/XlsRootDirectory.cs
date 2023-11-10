// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsRootDirectory
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>The XLS root directory.</summary>
    internal class XlsRootDirectory
    {
        /// <summary>The entries.</summary>
        private readonly List<XlsDirectoryEntry> m_entries;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsRootDirectory"/> class.</summary>
        /// <param name="hdr">The header.</param>
        public XlsRootDirectory(XlsHeader hdr)
        {
            var xlsStream = new XlsStream(hdr, hdr.RootDirectoryEntryStart, false, null);
            var numArray = xlsStream.ReadStream();
            var xlsDirectoryEntryList = new List<XlsDirectoryEntry>();
            for (var srcOffset = 0; srcOffset < numArray.Length; srcOffset += 128)
            {
                var bytes = new byte[128];
                Buffer.BlockCopy(numArray, srcOffset, bytes, 0, bytes.Length);
                xlsDirectoryEntryList.Add(new XlsDirectoryEntry(bytes, hdr));
            }
            m_entries = xlsDirectoryEntryList;
            for (var index = 0; index < xlsDirectoryEntryList.Count; ++index)
            {
                var xlsDirectoryEntry = xlsDirectoryEntryList[index];
                if (RootEntry == null && xlsDirectoryEntry.EntryType == STGTY.STGTY_ROOT)
                {
                    RootEntry = xlsDirectoryEntry;
                }
                if (xlsDirectoryEntry.ChildSid != uint.MaxValue)
                {
                    xlsDirectoryEntry.Child = xlsDirectoryEntryList[(int)xlsDirectoryEntry.ChildSid];
                }
                if (xlsDirectoryEntry.LeftSiblingSid != uint.MaxValue)
                {
                    xlsDirectoryEntry.LeftSibling = xlsDirectoryEntryList[(int)xlsDirectoryEntry.LeftSiblingSid];
                }
                if (xlsDirectoryEntry.RightSiblingSid != uint.MaxValue)
                {
                    xlsDirectoryEntry.RightSibling = xlsDirectoryEntryList[(int)xlsDirectoryEntry.RightSiblingSid];
                }
            }
            xlsStream.CalculateMiniFat(this);
        }

        /// <summary>Gets the entries.</summary>
        /// <value>The entries.</value>
        public ReadOnlyCollection<XlsDirectoryEntry> Entries => m_entries.AsReadOnly();

        /// <summary>Gets the root entry.</summary>
        /// <value>The root entry.</value>
        public XlsDirectoryEntry RootEntry { get; }

        /// <summary>Searches for the first entry.</summary>
        /// <param name="EntryName">Name of the entry.</param>
        /// <returns>The found entry.</returns>
        public XlsDirectoryEntry FindEntry(string EntryName)
        {
            foreach (var entry in m_entries)
            {
                if (string.Equals(entry.EntryName, EntryName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return entry;
                }
            }
            return null;
        }
    }
}
