// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipHelperStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>A zip helper stream.</summary>
    /// <seealso cref="System.IO.Stream" />
    internal class ZipHelperStream : Stream
    {

        /// <summary>The stream.</summary>
        private Stream stream_;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipHelperStream" />
        ///     class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ZipHelperStream(string name)
        {
            stream_ = new FileStream(name, FileMode.Open, FileAccess.ReadWrite);
            IsStreamOwner = true;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipHelperStream" />
        ///     class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public ZipHelperStream(Stream stream)
        {
            stream_ = stream;
        }

        /// <inheritdoc/>
        public override bool CanRead => stream_.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => stream_.CanSeek;

        /// <inheritdoc/>
        public override bool CanTimeout => stream_.CanTimeout;

        /// <inheritdoc/>
        public override bool CanWrite => stream_.CanWrite;

        /// <summary>Gets or sets a value indicating whether this ZipHelperStream is stream owner.</summary>
        /// <value>True if this ZipHelperStream is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; }

        /// <inheritdoc/>
        public override long Length => stream_.Length;

        /// <inheritdoc/>
        public override long Position
        {
            get => stream_.Position;
            set => stream_.Position = value;
        }

        /// <inheritdoc/>
        public override void Close()
        {
            var stream = stream_;
            stream_ = null;
            if (!IsStreamOwner || stream == null)
            {
                return;
            }
            IsStreamOwner = false;
            stream.Close();
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            stream_.Flush();
        }

        /// <summary>Locates block with signature.</summary>
        /// <param name="signature">          The signature.</param>
        /// <param name="endLocation">        The end location.</param>
        /// <param name="minimumBlockSize">   Size of the minimum block.</param>
        /// <param name="maximumVariableData">Information describing the maximum variable.</param>
        /// <returns>A long.</returns>
        public long LocateBlockWithSignature(
            int signature,
            long endLocation,
            int minimumBlockSize,
            int maximumVariableData)
        {
            var num1 = endLocation - minimumBlockSize;
            if (num1 < 0L)
            {
                return -1;
            }
            var num2 = Math.Max(num1 - maximumVariableData, 0L);
            while (num1 >= num2)
            {
                Seek(num1--, SeekOrigin.Begin);
                if (ReadLEInt() == signature)
                {
                    return Position;
                }
            }
            return -1;
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return stream_.Read(buffer, offset, count);
        }

        /// <summary>Reads data descriptor.</summary>
        /// <param name="zip64">True to zip 64.</param>
        /// <param name="data"> The data.</param>
        public void ReadDataDescriptor(bool zip64, DescriptorData data)
        {
            if (ReadLEInt() != 134695760)
            {
                throw new ZipException("Data descriptor signature not found");
            }
            data.Crc = ReadLEInt();
            if (zip64)
            {
                data.CompressedSize = ReadLELong();
                data.Size = ReadLELong();
            }
            else
            {
                data.CompressedSize = ReadLEInt();
                data.Size = ReadLEInt();
            }
        }

        /// <summary>Reads le int.</summary>
        /// <returns>The le int.</returns>
        public int ReadLEInt()
        {
            return ReadLEShort() | (ReadLEShort() << 16);
        }

        /// <summary>Reads le long.</summary>
        /// <returns>The le long.</returns>
        public long ReadLELong()
        {
            return (uint)ReadLEInt() | ((long)ReadLEInt() << 32);
        }

        /// <summary>Reads le short.</summary>
        /// <returns>The le short.</returns>
        public int ReadLEShort()
        {
            var num1 = stream_.ReadByte();
            if (num1 < 0)
            {
                throw new EndOfStreamException();
            }
            var num2 = stream_.ReadByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException();
            }
            return num1 | (num2 << 8);
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return stream_.Seek(offset, origin);
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            stream_.SetLength(value);
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            stream_.Write(buffer, offset, count);
        }

        /// <summary>Writes a data descriptor.</summary>
        /// <param name="entry">The entry.</param>
        /// <returns>An int.</returns>
        public int WriteDataDescriptor(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            var num1 = 0;
            if ((entry.Flags & 8) != 0)
            {
                WriteLEInt(134695760);
                WriteLEInt((int)entry.Crc);
                var num2 = num1 + 8;
                if (entry.LocalHeaderRequiresZip64)
                {
                    WriteLELong(entry.CompressedSize);
                    WriteLELong(entry.Size);
                    num1 = num2 + 16;
                }
                else
                {
                    WriteLEInt((int)entry.CompressedSize);
                    WriteLEInt((int)entry.Size);
                    num1 = num2 + 8;
                }
            }
            return num1;
        }

        /// <summary>Writes an end of central directory.</summary>
        /// <param name="noOfEntries">            The no of entries.</param>
        /// <param name="sizeEntries">            The size entries.</param>
        /// <param name="startOfCentralDirectory">Pathname of the start of central directory.</param>
        /// <param name="comment">                The comment.</param>
        public void WriteEndOfCentralDirectory(
            long noOfEntries,
            long sizeEntries,
            long startOfCentralDirectory,
            byte[] comment)
        {
            if (noOfEntries >= ushort.MaxValue
                || startOfCentralDirectory >= uint.MaxValue
                || sizeEntries >= uint.MaxValue)
            {
                WriteZip64EndOfCentralDirectory(noOfEntries, sizeEntries, startOfCentralDirectory);
            }
            WriteLEInt(101010256);
            WriteLEShort(0);
            WriteLEShort(0);
            if (noOfEntries >= ushort.MaxValue)
            {
                WriteLEUshort(ushort.MaxValue);
                WriteLEUshort(ushort.MaxValue);
            }
            else
            {
                WriteLEShort((short)noOfEntries);
                WriteLEShort((short)noOfEntries);
            }
            if (sizeEntries >= uint.MaxValue)
            {
                WriteLEUint(uint.MaxValue);
            }
            else
            {
                WriteLEInt((int)sizeEntries);
            }
            if (startOfCentralDirectory >= uint.MaxValue)
            {
                WriteLEUint(uint.MaxValue);
            }
            else
            {
                WriteLEInt((int)startOfCentralDirectory);
            }
            var num = comment != null ? comment.Length : 0;
            if (num > ushort.MaxValue)
            {
                throw new ZipException(string.Format("Comment length({0}) is too long can only be 64K", num));
            }
            WriteLEShort(num);
            if (num <= 0)
            {
                return;
            }
            Write(comment, 0, comment.Length);
        }

        /// <summary>Writes a le int.</summary>
        /// <param name="value">The value.</param>
        public void WriteLEInt(int value)
        {
            WriteLEShort(value);
            WriteLEShort(value >> 16);
        }

        /// <summary>Writes a le long.</summary>
        /// <param name="value">The value.</param>
        public void WriteLELong(long value)
        {
            WriteLEInt((int)value);
            WriteLEInt((int)(value >> 32));
        }

        /// <summary>Writes a le short.</summary>
        /// <param name="value">The value.</param>
        public void WriteLEShort(int value)
        {
            stream_.WriteByte((byte)(value & byte.MaxValue));
            stream_.WriteByte((byte)((value >> 8) & byte.MaxValue));
        }

        /// <summary>Writes a le uint.</summary>
        /// <param name="value">The value.</param>
        public void WriteLEUint(uint value)
        {
            WriteLEUshort((ushort)(value & ushort.MaxValue));
            WriteLEUshort((ushort)(value >> 16));
        }

        /// <summary>Writes a le ulong.</summary>
        /// <param name="value">The value.</param>
        public void WriteLEUlong(ulong value)
        {
            WriteLEUint((uint)(value & uint.MaxValue));
            WriteLEUint((uint)(value >> 32));
        }

        /// <summary>Writes a le ushort.</summary>
        /// <param name="value">The value.</param>
        public void WriteLEUshort(ushort value)
        {
            stream_.WriteByte((byte)(value & (uint)byte.MaxValue));
            stream_.WriteByte((byte)((uint)value >> 8));
        }

        /// <summary>Writes a zip 64 end of central directory.</summary>
        /// <param name="noOfEntries">     The no of entries.</param>
        /// <param name="sizeEntries">     The size entries.</param>
        /// <param name="centralDirOffset">The central dir offset.</param>
        public void WriteZip64EndOfCentralDirectory(long noOfEntries, long sizeEntries, long centralDirOffset)
        {
            var position = stream_.Position;
            WriteLEInt(101075792);
            WriteLELong(44L);
            WriteLEShort(51);
            WriteLEShort(45);
            WriteLEInt(0);
            WriteLEInt(0);
            WriteLELong(noOfEntries);
            WriteLELong(noOfEntries);
            WriteLELong(sizeEntries);
            WriteLELong(centralDirOffset);
            WriteLEInt(117853008);
            WriteLEInt(0);
            WriteLELong(position);
            WriteLEInt(1);
        }

        /// <summary>Writes a local header.</summary>
        /// <param name="entry">    The entry.</param>
        /// <param name="patchData">Information describing the patch.</param>
        private void WriteLocalHeader(ZipEntry entry, EntryPatchData patchData)
        {
            var compressionMethod = entry.CompressionMethod;
            var flag1 = true;
            var flag2 = false;
            WriteLEInt(67324752);
            WriteLEShort(entry.Version);
            WriteLEShort(entry.Flags);
            WriteLEShort((byte)compressionMethod);
            WriteLEInt((int)entry.DosTime);
            if (flag1)
            {
                WriteLEInt((int)entry.Crc);
                if (entry.LocalHeaderRequiresZip64)
                {
                    WriteLEInt(-1);
                    WriteLEInt(-1);
                }
                else
                {
                    WriteLEInt(entry.IsCrypted ? (int)entry.CompressedSize + 12 : (int)entry.CompressedSize);
                    WriteLEInt((int)entry.Size);
                }
            }
            else
            {
                if (patchData != null)
                {
                    patchData.CrcPatchOffset = stream_.Position;
                }
                WriteLEInt(0);
                if (patchData != null)
                {
                    patchData.SizePatchOffset = stream_.Position;
                }
                if (entry.LocalHeaderRequiresZip64 && flag2)
                {
                    WriteLEInt(-1);
                    WriteLEInt(-1);
                }
                else
                {
                    WriteLEInt(0);
                    WriteLEInt(0);
                }
            }
            var array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
            if (array.Length > ushort.MaxValue)
            {
                throw new ZipException("Entry name too long.");
            }
            var zipExtraData = new ZipExtraData(entry.ExtraData);
            if (entry.LocalHeaderRequiresZip64 && (flag1 || flag2))
            {
                zipExtraData.StartNewEntry();
                if (flag1)
                {
                    zipExtraData.AddLeLong(entry.Size);
                    zipExtraData.AddLeLong(entry.CompressedSize);
                }
                else
                {
                    zipExtraData.AddLeLong(-1L);
                    zipExtraData.AddLeLong(-1L);
                }
                zipExtraData.AddNewEntry(1);
                if (!zipExtraData.Find(1))
                {
                    throw new ZipException("Internal error cant find extra data");
                }
                if (patchData != null)
                {
                    patchData.SizePatchOffset = zipExtraData.CurrentReadIndex;
                }
            }
            else
            {
                zipExtraData.Delete(1);
            }
            var entryData = zipExtraData.GetEntryData();
            WriteLEShort(array.Length);
            WriteLEShort(entryData.Length);
            if (array.Length > 0)
            {
                stream_.Write(array, 0, array.Length);
            }
            if (entry.LocalHeaderRequiresZip64 && flag2)
            {
                patchData.SizePatchOffset += stream_.Position;
            }
            if (entryData.Length <= 0)
            {
                return;
            }
            stream_.Write(entryData, 0, entryData.Length);
        }
    }
}
