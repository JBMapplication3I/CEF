// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipOutputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.Collections;
    using System.IO;
    using Checksums;
    using Compression;
    using Compression.Streams;

    /// <summary>A zip output stream.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream" />
    public class ZipOutputStream : DeflaterOutputStream
    {
        /// <summary>The CRC.</summary>
        private readonly Crc32 crc = new();

        /// <summary>The CRC patch position.</summary>
        private long crcPatchPos = -1;

        /// <summary>The current entry.</summary>
        private ZipEntry curEntry;

        /// <summary>The current method.</summary>
        private CompressionMethod curMethod = CompressionMethod.Deflated;

        /// <summary>The default compression level.</summary>
        private int defaultCompressionLevel = -1;

        /// <summary>The entries.</summary>
        private ArrayList entries = new();

        /// <summary>The offset.</summary>
        private long offset;

        /// <summary>True to patch entry header.</summary>
        private bool patchEntryHeader;

        /// <summary>The size.</summary>
        private long size;

        /// <summary>The size patch position.</summary>
        private long sizePatchPos = -1;

        /// <summary>The zip comment.</summary>
        private byte[] zipComment = Array.Empty<byte>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipOutputStream" />
        ///     class.
        /// </summary>
        /// <param name="baseOutputStream">The base output stream.</param>
        public ZipOutputStream(Stream baseOutputStream)
            : base(baseOutputStream, new Deflater(-1, true))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipOutputStream" />
        ///     class.
        /// </summary>
        /// <param name="baseOutputStream">The base output stream.</param>
        /// <param name="bufferSize">      Size of the buffer.</param>
        public ZipOutputStream(Stream baseOutputStream, int bufferSize)
            : base(baseOutputStream, new Deflater(-1, true), bufferSize)
        {
        }

        /// <summary>Gets a value indicating whether this ZipOutputStream is finished.</summary>
        /// <value>True if this ZipOutputStream is finished, false if not.</value>
        public bool IsFinished => entries == null;

        /// <summary>Gets or sets the use zip 64.</summary>
        /// <value>The use zip 64.</value>
        public UseZip64 UseZip64 { get; set; } = UseZip64.Dynamic;

        /// <summary>Closes the entry.</summary>
        public void CloseEntry()
        {
            if (curEntry == null)
            {
                throw new InvalidOperationException("No open entry");
            }
            var num = size;
            if (curMethod == CompressionMethod.Deflated)
            {
                if (size >= 0L)
                {
                    base.Finish();
                    num = deflater_.TotalOut;
                }
                else
                {
                    deflater_.Reset();
                }
            }
            if (curEntry.AESKeySize > 0)
            {
                baseOutputStream_.Write(AESAuthCode, 0, 10);
            }
            if (curEntry.Size < 0L)
            {
                curEntry.Size = size;
            }
            else if (curEntry.Size != size)
            {
                throw new ZipException("size was " + size + ", but I expected " + curEntry.Size);
            }
            if (curEntry.CompressedSize < 0L)
            {
                curEntry.CompressedSize = num;
            }
            else if (curEntry.CompressedSize != num)
            {
                throw new ZipException("compressed size was " + num + ", but I expected " + curEntry.CompressedSize);
            }
            if (curEntry.Crc < 0L)
            {
                curEntry.Crc = crc.Value;
            }
            else if (curEntry.Crc != crc.Value)
            {
                throw new ZipException("crc was " + crc.Value + ", but I expected " + curEntry.Crc);
            }
            offset += num;
            if (curEntry.IsCrypted)
            {
                if (curEntry.AESKeySize > 0)
                {
                    curEntry.CompressedSize += curEntry.AESOverheadSize;
                }
                else
                {
                    curEntry.CompressedSize += 12L;
                }
            }
            if (patchEntryHeader)
            {
                patchEntryHeader = false;
                var position = baseOutputStream_.Position;
                baseOutputStream_.Seek(crcPatchPos, SeekOrigin.Begin);
                WriteLeInt((int)curEntry.Crc);
                if (curEntry.LocalHeaderRequiresZip64)
                {
                    if (sizePatchPos == -1L)
                    {
                        throw new ZipException("Entry requires zip64 but this has been turned off");
                    }
                    baseOutputStream_.Seek(sizePatchPos, SeekOrigin.Begin);
                    WriteLeLong(curEntry.Size);
                    WriteLeLong(curEntry.CompressedSize);
                }
                else
                {
                    WriteLeInt((int)curEntry.CompressedSize);
                    WriteLeInt((int)curEntry.Size);
                }
                baseOutputStream_.Seek(position, SeekOrigin.Begin);
            }
            if ((curEntry.Flags & 8) != 0)
            {
                WriteLeInt(134695760);
                WriteLeInt((int)curEntry.Crc);
                if (curEntry.LocalHeaderRequiresZip64)
                {
                    WriteLeLong(curEntry.CompressedSize);
                    WriteLeLong(curEntry.Size);
                    offset += 24L;
                }
                else
                {
                    WriteLeInt((int)curEntry.CompressedSize);
                    WriteLeInt((int)curEntry.Size);
                    offset += 16L;
                }
            }
            entries.Add(curEntry);
            curEntry = null;
        }

        /// <inheritdoc/>
        public override void Finish()
        {
            if (entries == null)
            {
                return;
            }
            if (curEntry != null)
            {
                CloseEntry();
            }
            long count = entries.Count;
            long sizeEntries = 0;
            foreach (ZipEntry entry in entries)
            {
                WriteLeInt(33639248);
                WriteLeShort(51);
                WriteLeShort(entry.Version);
                WriteLeShort(entry.Flags);
                WriteLeShort((short)entry.CompressionMethodForHeader);
                WriteLeInt((int)entry.DosTime);
                WriteLeInt((int)entry.Crc);
                if (entry.IsZip64Forced() || entry.CompressedSize >= uint.MaxValue)
                {
                    WriteLeInt(-1);
                }
                else
                {
                    WriteLeInt((int)entry.CompressedSize);
                }
                if (entry.IsZip64Forced() || entry.Size >= uint.MaxValue)
                {
                    WriteLeInt(-1);
                }
                else
                {
                    WriteLeInt((int)entry.Size);
                }
                var array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
                if (array.Length > ushort.MaxValue)
                {
                    throw new ZipException("Name too long.");
                }
                var extraData = new ZipExtraData(entry.ExtraData);
                if (entry.CentralHeaderRequiresZip64)
                {
                    extraData.StartNewEntry();
                    if (entry.IsZip64Forced() || entry.Size >= uint.MaxValue)
                    {
                        extraData.AddLeLong(entry.Size);
                    }
                    if (entry.IsZip64Forced() || entry.CompressedSize >= uint.MaxValue)
                    {
                        extraData.AddLeLong(entry.CompressedSize);
                    }
                    if (entry.Offset >= uint.MaxValue)
                    {
                        extraData.AddLeLong(entry.Offset);
                    }
                    extraData.AddNewEntry(1);
                }
                else
                {
                    extraData.Delete(1);
                }
                if (entry.AESKeySize > 0)
                {
                    AddExtraDataAES(entry, extraData);
                }
                var entryData = extraData.GetEntryData();
                var buffer = entry.Comment != null
                    ? ZipConstants.ConvertToArray(entry.Flags, entry.Comment)
                    : Array.Empty<byte>();
                if (buffer.Length > ushort.MaxValue)
                {
                    throw new ZipException("Comment too long.");
                }
                WriteLeShort(array.Length);
                WriteLeShort(entryData.Length);
                WriteLeShort(buffer.Length);
                WriteLeShort(0);
                WriteLeShort(0);
                if (entry.ExternalFileAttributes != -1)
                {
                    WriteLeInt(entry.ExternalFileAttributes);
                }
                else if (entry.IsDirectory)
                {
                    WriteLeInt(16);
                }
                else
                {
                    WriteLeInt(0);
                }
                if (entry.Offset >= uint.MaxValue)
                {
                    WriteLeInt(-1);
                }
                else
                {
                    WriteLeInt((int)entry.Offset);
                }
                if (array.Length > 0)
                {
                    baseOutputStream_.Write(array, 0, array.Length);
                }
                if (entryData.Length > 0)
                {
                    baseOutputStream_.Write(entryData, 0, entryData.Length);
                }
                if (buffer.Length > 0)
                {
                    baseOutputStream_.Write(buffer, 0, buffer.Length);
                }
                sizeEntries += 46 + array.Length + entryData.Length + buffer.Length;
            }
            using (var zipHelperStream = new ZipHelperStream(baseOutputStream_))
            {
                zipHelperStream.WriteEndOfCentralDirectory(count, sizeEntries, offset, zipComment);
            }
            entries = null;
        }

        /// <summary>Gets the level.</summary>
        /// <returns>The level.</returns>
        public int GetLevel()
        {
            return deflater_.GetLevel();
        }

        /// <summary>Puts next entry.</summary>
        /// <param name="entry">The entry.</param>
        public void PutNextEntry(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (entries == null)
            {
                throw new InvalidOperationException("ZipOutputStream was finished");
            }
            if (curEntry != null)
            {
                CloseEntry();
            }
            if (entries.Count == int.MaxValue)
            {
                throw new ZipException("Too many entries for Zip file");
            }
            var compressionMethod = entry.CompressionMethod;
            var level = defaultCompressionLevel;
            entry.Flags &= 2048;
            patchEntryHeader = false;
            bool flag;
            if (entry.Size == 0L)
            {
                entry.CompressedSize = entry.Size;
                entry.Crc = 0L;
                compressionMethod = CompressionMethod.Stored;
                flag = true;
            }
            else
            {
                flag = entry.Size >= 0L && entry.HasCrc;
                if (compressionMethod == CompressionMethod.Stored)
                {
                    if (!flag)
                    {
                        if (!CanPatchEntries)
                        {
                            compressionMethod = CompressionMethod.Deflated;
                            level = 0;
                        }
                    }
                    else
                    {
                        entry.CompressedSize = entry.Size;
                        flag = entry.HasCrc;
                    }
                }
            }
            if (!flag)
            {
                if (!CanPatchEntries)
                {
                    entry.Flags |= 8;
                }
                else
                {
                    patchEntryHeader = true;
                }
            }
            if (Password != null)
            {
                entry.IsCrypted = true;
                if (entry.Crc < 0L)
                {
                    entry.Flags |= 8;
                }
            }
            entry.Offset = offset;
            entry.CompressionMethod = compressionMethod;
            curMethod = compressionMethod;
            sizePatchPos = -1L;
            if (UseZip64 == UseZip64.On || entry.Size < 0L && UseZip64 == UseZip64.Dynamic)
            {
                entry.ForceZip64();
            }
            WriteLeInt(67324752);
            WriteLeShort(entry.Version);
            WriteLeShort(entry.Flags);
            WriteLeShort((byte)entry.CompressionMethodForHeader);
            WriteLeInt((int)entry.DosTime);
            if (flag)
            {
                WriteLeInt((int)entry.Crc);
                if (entry.LocalHeaderRequiresZip64)
                {
                    WriteLeInt(-1);
                    WriteLeInt(-1);
                }
                else
                {
                    WriteLeInt(entry.IsCrypted ? (int)entry.CompressedSize + 12 : (int)entry.CompressedSize);
                    WriteLeInt((int)entry.Size);
                }
            }
            else
            {
                if (patchEntryHeader)
                {
                    crcPatchPos = baseOutputStream_.Position;
                }
                WriteLeInt(0);
                if (patchEntryHeader)
                {
                    sizePatchPos = baseOutputStream_.Position;
                }
                if (entry.LocalHeaderRequiresZip64 || patchEntryHeader)
                {
                    WriteLeInt(-1);
                    WriteLeInt(-1);
                }
                else
                {
                    WriteLeInt(0);
                    WriteLeInt(0);
                }
            }
            var array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
            if (array.Length > ushort.MaxValue)
            {
                throw new ZipException("Entry name too long.");
            }
            var extraData = new ZipExtraData(entry.ExtraData);
            if (entry.LocalHeaderRequiresZip64)
            {
                extraData.StartNewEntry();
                if (flag)
                {
                    extraData.AddLeLong(entry.Size);
                    extraData.AddLeLong(entry.CompressedSize);
                }
                else
                {
                    extraData.AddLeLong(-1L);
                    extraData.AddLeLong(-1L);
                }
                extraData.AddNewEntry(1);
                if (!extraData.Find(1))
                {
                    throw new ZipException("Internal error cant find extra data");
                }
                if (patchEntryHeader)
                {
                    sizePatchPos = extraData.CurrentReadIndex;
                }
            }
            else
            {
                extraData.Delete(1);
            }
            if (entry.AESKeySize > 0)
            {
                AddExtraDataAES(entry, extraData);
            }
            var entryData = extraData.GetEntryData();
            WriteLeShort(array.Length);
            WriteLeShort(entryData.Length);
            if (array.Length > 0)
            {
                baseOutputStream_.Write(array, 0, array.Length);
            }
            if (entry.LocalHeaderRequiresZip64 && patchEntryHeader)
            {
                sizePatchPos += baseOutputStream_.Position;
            }
            if (entryData.Length > 0)
            {
                baseOutputStream_.Write(entryData, 0, entryData.Length);
            }
            offset += 30 + array.Length + entryData.Length;
            if (entry.AESKeySize > 0)
            {
                offset += entry.AESOverheadSize;
            }
            curEntry = entry;
            crc.Reset();
            if (compressionMethod == CompressionMethod.Deflated)
            {
                deflater_.Reset();
                deflater_.SetLevel(level);
            }
            size = 0L;
            if (!entry.IsCrypted)
            {
                return;
            }
            if (entry.AESKeySize > 0)
            {
                WriteAESHeader(entry);
            }
            else if (entry.Crc < 0L)
            {
                WriteEncryptionHeader(entry.DosTime << 16);
            }
            else
            {
                WriteEncryptionHeader(entry.Crc);
            }
        }

        /// <summary>Sets a comment.</summary>
        /// <param name="comment">The comment.</param>
        public void SetComment(string comment)
        {
            var array = ZipConstants.ConvertToArray(comment);
            zipComment = array.Length <= (int)ushort.MaxValue
                ? array
                : throw new ArgumentOutOfRangeException(nameof(comment));
        }

        /// <summary>Sets a level.</summary>
        /// <param name="level">The level.</param>
        public void SetLevel(int level)
        {
            deflater_.SetLevel(level);
            defaultCompressionLevel = level;
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (curEntry == null)
            {
                throw new InvalidOperationException("No open entry.");
            }
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be negative");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            }
            if (buffer.Length - offset < count)
            {
                throw new ArgumentException("Invalid offset/count combination");
            }
            crc.Update(buffer, offset, count);
            size += count;
            switch (curMethod)
            {
                case CompressionMethod.Stored:
                {
                    if (Password != null)
                    {
                        CopyAndEncrypt(buffer, offset, count);
                        break;
                    }
                    baseOutputStream_.Write(buffer, offset, count);
                    break;
                }
                case CompressionMethod.Deflated:
                {
                    base.Write(buffer, offset, count);
                    break;
                }
            }
        }

        /// <summary>Adds an extra data es to 'extraData'.</summary>
        /// <param name="entry">    The entry.</param>
        /// <param name="extraData">Information describing the extra.</param>
        private static void AddExtraDataAES(ZipEntry entry, ZipExtraData extraData)
        {
            extraData.StartNewEntry();
            extraData.AddLeShort(2);
            extraData.AddLeShort(17729);
            extraData.AddData(entry.AESEncryptionStrength);
            extraData.AddLeShort((int)entry.CompressionMethod);
            extraData.AddNewEntry(39169);
        }

        /// <summary>Copies the and encrypt.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        private void CopyAndEncrypt(byte[] buffer, int offset, int count)
        {
            var buffer1 = new byte[4096];
            while (count > 0)
            {
                var num = count < 4096 ? count : 4096;
                Array.Copy(buffer, offset, buffer1, 0, num);
                EncryptBlock(buffer1, 0, num);
                baseOutputStream_.Write(buffer1, 0, num);
                count -= num;
                offset += num;
            }
        }

        /// <summary>Writes a es header.</summary>
        /// <param name="entry">The entry.</param>
        private void WriteAESHeader(ZipEntry entry)
        {
            InitializeAESPassword(entry, Password, out var salt, out var pwdVerifier);
            baseOutputStream_.Write(salt, 0, salt.Length);
            baseOutputStream_.Write(pwdVerifier, 0, pwdVerifier.Length);
        }

        /// <summary>Writes an encryption header.</summary>
        /// <param name="crcValue">The CRC value.</param>
        private void WriteEncryptionHeader(long crcValue)
        {
            offset += 12L;
            InitializePassword(Password);
            var buffer = new byte[12];
            new Random().NextBytes(buffer);
            buffer[11] = (byte)(crcValue >> 24);
            EncryptBlock(buffer, 0, buffer.Length);
            baseOutputStream_.Write(buffer, 0, buffer.Length);
        }

        /// <summary>Writes a le int.</summary>
        /// <param name="value">The value.</param>
        private void WriteLeInt(int value)
        {
            WriteLeShort(value);
            WriteLeShort(value >> 16);
        }

        /// <summary>Writes a le long.</summary>
        /// <param name="value">The value.</param>
        private void WriteLeLong(long value)
        {
            WriteLeInt((int)value);
            WriteLeInt((int)(value >> 32));
        }

        /// <summary>Writes a le short.</summary>
        /// <param name="value">The value.</param>
        private void WriteLeShort(int value)
        {
            baseOutputStream_.WriteByte((byte)(value & byte.MaxValue));
            baseOutputStream_.WriteByte((byte)((value >> 8) & byte.MaxValue));
        }
    }
}
