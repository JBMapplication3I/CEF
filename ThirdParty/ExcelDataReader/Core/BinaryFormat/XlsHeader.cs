// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsHeader
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Exceptions;

    /// <summary>The XLS header.</summary>
    internal class XlsHeader
    {
        /// <summary>The bytes.</summary>
        private readonly byte[] m_bytes;

        /// <summary>The fat.</summary>
        private XlsFat m_fat;

        /// <summary>The minifat.</summary>
        private XlsFat m_minifat;

        private readonly object padLock = new();

        private static readonly object staticPadLock = new();

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsHeader"/> class.</summary>
        /// <param name="file">The file.</param>
        private XlsHeader(Stream file)
        {
            m_bytes = new byte[512];
            FileStream = file;
        }

        /// <summary>Gets the byte order.</summary>
        /// <value>The byte order.</value>
        public ushort ByteOrder => BitConverter.ToUInt16(m_bytes, 28);

        /// <summary>Gets the identifier of the class.</summary>
        /// <value>The identifier of the class.</value>
        public Guid ClassId
        {
            get
            {
                var b = new byte[16];
                Buffer.BlockCopy(m_bytes, 8, b, 0, 16);
                return new Guid(b);
            }
        }

        /// <summary>Gets the dif first sector.</summary>
        /// <value>The dif first sector.</value>
        public uint DifFirstSector => BitConverter.ToUInt32(m_bytes, 68);

        /// <summary>Gets the number of dif sectors.</summary>
        /// <value>The number of dif sectors.</value>
        public int DifSectorCount => BitConverter.ToInt32(m_bytes, 72);

        /// <summary>Gets the DLL version.</summary>
        /// <value>The DLL version.</value>
        public ushort DllVersion => BitConverter.ToUInt16(m_bytes, 26);

        /// <summary>Gets the fat.</summary>
        /// <value>The fat.</value>
        public XlsFat FAT
        {
            get
            {
                if (m_fat != null)
                {
                    return m_fat;
                }
                var sectorSize = SectorSize;
                var sectors = new List<uint>(FatSectorCount);
                for (var startIndex = 76; startIndex < sectorSize; startIndex += 4)
                {
                    var uint32 = BitConverter.ToUInt32(m_bytes, startIndex);
                    if (uint32 != uint.MaxValue)
                    {
                        sectors.Add(uint32);
                    }
                    else
                    {
                        goto label_23;
                    }
                }
                int difSectorCount;
                if ((difSectorCount = DifSectorCount) != 0)
                {
                    lock (padLock)
                    {
                        var num1 = DifFirstSector;
                        var buffer = new byte[sectorSize];
                        uint num2 = 0;
                        while (difSectorCount > 0)
                        {
                            sectors.Capacity += 128;
                            if (num2 == 0U || (int)num1 - (int)num2 != 1)
                            {
                                FileStream.Seek((num1 + 1U) * sectorSize, SeekOrigin.Begin);
                            }
                            num2 = num1;
                            FileStream.Read(buffer, 0, sectorSize);
                            for (var startIndex = 0; startIndex < 508; startIndex += 4)
                            {
                                var uint32 = BitConverter.ToUInt32(buffer, startIndex);
                                if (uint32 != uint.MaxValue)
                                {
                                    sectors.Add(uint32);
                                }
                                else
                                {
                                    goto label_23;
                                }
                            }
                            var uint32_1 = BitConverter.ToUInt32(buffer, 508);
                            if (uint32_1 != uint.MaxValue)
                            {
                                if (difSectorCount-- > 1)
                                {
                                    num1 = uint32_1;
                                }
                                else
                                {
                                    sectors.Add(uint32_1);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            label_23:
                m_fat = new XlsFat(this, sectors, SectorSize, false, null);
                return m_fat;
            }
        }

        /// <summary>Gets the number of fat sectors.</summary>
        /// <value>The number of fat sectors.</value>
        public int FatSectorCount => BitConverter.ToInt32(m_bytes, 44);

        /// <summary>Gets the file stream.</summary>
        /// <value>The file stream.</value>
        public Stream FileStream { get; }

        /// <summary>Gets a value indicating whether this XlsHeader is signature valid.</summary>
        /// <value>True if this XlsHeader is signature valid, false if not.</value>
        public bool IsSignatureValid => Signature == 16220472316735377360UL;

        /// <summary>Gets the mini fat first sector.</summary>
        /// <value>The mini fat first sector.</value>
        public uint MiniFatFirstSector => BitConverter.ToUInt32(m_bytes, 60);

        /// <summary>Gets the number of mini fat sectors.</summary>
        /// <value>The number of mini fat sectors.</value>
        public int MiniFatSectorCount => BitConverter.ToInt32(m_bytes, 64);

        /// <summary>Gets the size of the mini sector.</summary>
        /// <value>The size of the mini sector.</value>
        public int MiniSectorSize => 1 << BitConverter.ToUInt16(m_bytes, 32);

        /// <summary>Gets the mini stream cutoff.</summary>
        /// <value>The mini stream cutoff.</value>
        public uint MiniStreamCutoff => BitConverter.ToUInt32(m_bytes, 56);

        /// <summary>Gets the root directory entry start.</summary>
        /// <value>The root directory entry start.</value>
        public uint RootDirectoryEntryStart => BitConverter.ToUInt32(m_bytes, 48);

        /// <summary>Gets the size of the sector.</summary>
        /// <value>The size of the sector.</value>
        public int SectorSize => 1 << BitConverter.ToUInt16(m_bytes, 30);

        /// <summary>Gets the signature.</summary>
        /// <value>The signature.</value>
        public ulong Signature => BitConverter.ToUInt64(m_bytes, 0);

        /// <summary>Gets the transaction signature.</summary>
        /// <value>The transaction signature.</value>
        public uint TransactionSignature => BitConverter.ToUInt32(m_bytes, 52);

        /// <summary>Gets the version.</summary>
        /// <value>The version.</value>
        public ushort Version => BitConverter.ToUInt16(m_bytes, 24);

        /// <summary>Reads a header.</summary>
        /// <param name="file">The file.</param>
        /// <returns>The header.</returns>
        public static XlsHeader ReadHeader(Stream file)
        {
            var xlsHeader = new XlsHeader(file);
            lock (staticPadLock)
            {
                file.Seek(0L, SeekOrigin.Begin);
                file.Read(xlsHeader.m_bytes, 0, 512);
            }
            if (!xlsHeader.IsSignatureValid)
            {
                throw new HeaderException("Error: Invalid file signature.");
            }
            return xlsHeader.ByteOrder == (ushort)65534
                ? xlsHeader
                : throw new FormatException("Error: Invalid byte order specified in header.");
        }

        /// <summary>Gets mini fat.</summary>
        /// <param name="rootDir">The root dir.</param>
        /// <returns>The mini fat.</returns>
        public XlsFat GetMiniFAT(XlsRootDirectory rootDir)
        {
            if (m_minifat != null)
            {
                return m_minifat;
            }
            if (MiniFatSectorCount == 0 || MiniSectorSize == -2)
            {
                return null;
            }
            var miniSectorSize = MiniSectorSize;
            m_minifat = new XlsFat(
                this,
                new List<uint>(MiniFatSectorCount) { BitConverter.ToUInt32(m_bytes, 60) },
                MiniSectorSize,
                true,
                rootDir);
            return m_minifat;
        }
    }
}
