// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsFat
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>The XLS fat.</summary>
    internal class XlsFat
    {
        /// <summary>The fat.</summary>
        private readonly List<uint> m_fat;

        /// <summary>True if this XlsFat is mini.</summary>
        private readonly bool m_isMini;

        /// <summary>The root dir.</summary>
        private readonly XlsRootDirectory m_rootDir;

        private readonly object padLock = new();

        /// <summary>Initializes a new instance of the <see cref="XlsFat"/> class.</summary>
        /// <param name="hdr">         The header.</param>
        /// <param name="sectors">     The sectors.</param>
        /// <param name="sizeOfSector">Size of the sector.</param>
        /// <param name="isMini">      True if this XlsFat is mini.</param>
        /// <param name="rootDir">     The root dir.</param>
        public XlsFat(XlsHeader hdr, List<uint> sectors, int sizeOfSector, bool isMini, XlsRootDirectory rootDir)
        {
            m_isMini = isMini;
            m_rootDir = rootDir;
            Header = hdr;
            SectorsForFat = sectors.Count;
            sizeOfSector = hdr.SectorSize;
            uint num = 0;
            var buffer = new byte[sizeOfSector];
            var fileStream = hdr.FileStream;
            using var memoryStream = new MemoryStream(sizeOfSector * SectorsForFat);
            lock (padLock)
            {
                for (var index = 0; index < sectors.Count; ++index)
                {
                    var sector = sectors[index];
                    if (num == 0U || (int)sector - (int)num != 1)
                    {
                        fileStream.Seek((sector + 1U) * sizeOfSector, SeekOrigin.Begin);
                    }
                    num = sector;
                    fileStream.Read(buffer, 0, sizeOfSector);
                    memoryStream.Write(buffer, 0, sizeOfSector);
                }
            }
            memoryStream.Seek(0L, SeekOrigin.Begin);
            var binaryReader = new BinaryReader(memoryStream);
            SectorsCount = (int)memoryStream.Length / 4;
            m_fat = new List<uint>(SectorsCount);
            for (var index = 0; index < SectorsCount; ++index)
            {
                m_fat.Add(binaryReader.ReadUInt32());
            }
            binaryReader.Close();
            memoryStream.Close();
        }

        /// <summary>Gets the header.</summary>
        /// <value>The header.</value>
        public XlsHeader Header { get; }

        /// <summary>Gets the number of sectors.</summary>
        /// <value>The number of sectors.</value>
        public int SectorsCount { get; }

        /// <summary>Gets the sectors for fat.</summary>
        /// <value>The sectors for fat.</value>
        public int SectorsForFat { get; }

        /// <summary>Gets the next sector.</summary>
        /// <param name="sector">The sector.</param>
        /// <returns>The next sector.</returns>
        public uint GetNextSector(uint sector)
        {
            if (m_fat.Count <= sector)
            {
                throw new ArgumentException("Error reading as FAT table : There's no such sector in FAT.");
            }
            var num = m_fat[(int)sector];
            return num switch
            {
                4294967292 or 4294967293 => throw new InvalidOperationException("Error reading stream from FAT area."),
                _ => num,
            };
        }
    }
}
