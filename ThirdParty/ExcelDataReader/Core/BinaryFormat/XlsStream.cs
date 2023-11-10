// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsStream
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.IO;

    /// <summary>The XLS stream.</summary>
    internal class XlsStream
    {
        private readonly object padLock = new();

        /// <summary>The fat.</summary>
        protected XlsFat m_fat;

        /// <summary>The file stream.</summary>
        protected Stream m_fileStream;

        /// <summary>The header.</summary>
        protected XlsHeader m_hdr;

        /// <summary>True if this XlsStream is mini.</summary>
        protected bool m_isMini;

        /// <summary>The minifat.</summary>
        protected XlsFat m_minifat;

        /// <summary>The root dir.</summary>
        protected XlsRootDirectory m_rootDir;

        /// <summary>The start sector.</summary>
        protected uint m_startSector;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsStream"/> class.</summary>
        /// <param name="hdr">        The header.</param>
        /// <param name="startSector">The start sector.</param>
        /// <param name="isMini">     True if this XlsStream is mini.</param>
        /// <param name="rootDir">    The root dir.</param>
        public XlsStream(XlsHeader hdr, uint startSector, bool isMini, XlsRootDirectory rootDir)
        {
            m_fileStream = hdr.FileStream;
            m_fat = hdr.FAT;
            m_hdr = hdr;
            m_startSector = startSector;
            m_isMini = isMini;
            m_rootDir = rootDir;
            CalculateMiniFat(rootDir);
        }

        /// <summary>Gets the base offset.</summary>
        /// <value>The base offset.</value>
        public uint BaseOffset => (uint)((m_startSector + 1U) * (ulong)m_hdr.SectorSize);

        /// <summary>Gets the base sector.</summary>
        /// <value>The base sector.</value>
        public uint BaseSector => m_startSector;

        /// <summary>Calculates the mini fat.</summary>
        /// <param name="rootDir">The root dir.</param>
        public void CalculateMiniFat(XlsRootDirectory rootDir)
        {
            m_minifat = m_hdr.GetMiniFAT(rootDir);
        }

        /// <summary>Reads the stream.</summary>
        /// <returns>An array of byte.</returns>
        public byte[] ReadStream()
        {
            var sector = m_startSector;
            uint num1 = 0;
            var count = m_isMini ? m_hdr.MiniSectorSize : m_hdr.SectorSize;
            var xlsFat = m_isMini ? m_minifat : m_fat;
            long num2 = 0;
            if (m_isMini && m_rootDir != null)
            {
                num2 = (m_rootDir.RootEntry.StreamFirstSector + 1U) * m_hdr.SectorSize;
            }
            var buffer = new byte[count];
            using var memoryStream = new MemoryStream(count * 8);
            lock (padLock)
            {
            label_5:
                if (num1 == 0U || (int)sector - (int)num1 != 1)
                {
                    m_fileStream.Seek((m_isMini ? sector : (long)(sector + 1U)) * count + num2, SeekOrigin.Begin);
                }
                num1 = num1 == 0U || (int)num1 != (int)sector
                    ? sector
                    : throw new InvalidOperationException("The excel file may be corrupt. We appear to be stuck");
                m_fileStream.Read(buffer, 0, count);
                memoryStream.Write(buffer, 0, count);
                sector = xlsFat.GetNextSector(sector);
                switch (sector)
                {
                    case 0:
                    throw new InvalidOperationException("Next sector cannot be 0. Possibly corrupt excel file");
                    case 4294967294:
                    break;
                    default:
                    goto label_5;
                }
            }
            return memoryStream.ToArray();
        }
    }
}
