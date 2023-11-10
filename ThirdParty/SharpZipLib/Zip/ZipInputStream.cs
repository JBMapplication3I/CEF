// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipInputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;
    using Checksums;
    using Compression;
    using Compression.Streams;
    using Encryption;

    /// <summary>A zip input stream.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" />
    public class ZipInputStream : InflaterInputStream
    {
        /// <summary>The CRC.</summary>
        private Crc32 crc = new();

        /// <summary>The entry.</summary>
        private ZipEntry entry;

        /// <summary>The flags.</summary>
        private int flags;

        /// <summary>The internal reader.</summary>
        private ReadDataHandler internalReader;

        /// <summary>The method.</summary>
        private int method;

        /// <summary>The size.</summary>
        private long size;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipInputStream" /> class.</summary>
        /// <param name="baseInputStream">The base input stream.</param>
        public ZipInputStream(Stream baseInputStream) : base(baseInputStream, new Inflater(true))
        {
            internalReader = ReadingNotAvailable;
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipInputStream" /> class.</summary>
        /// <param name="baseInputStream">The base input stream.</param>
        /// <param name="bufferSize">     Size of the buffer.</param>
        public ZipInputStream(Stream baseInputStream, int bufferSize) : base(
            baseInputStream,
            new Inflater(true),
            bufferSize)
        {
            internalReader = ReadingNotAvailable;
        }

        /// <summary>Handler, called when the read data.</summary>
        /// <param name="b">     The byte[] to process.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns>An int.</returns>
        private delegate int ReadDataHandler(byte[] b, int offset, int length);

        /// <inheritdoc/>
        public override int Available => entry == null ? 0 : 1;

        /// <summary>Gets a value indicating whether we can decompress entry.</summary>
        /// <value>True if we can decompress entry, false if not.</value>
        public bool CanDecompressEntry => entry != null && entry.CanDecompress;

        /// <inheritdoc/>
        public override long Length
        {
            get
            {
                if (entry == null)
                {
                    throw new InvalidOperationException("No current entry");
                }
                return entry.Size >= 0L
                    ? entry.Size
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    : throw new ZipException("Length not available for the current entry");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
            }
        }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <inheritdoc/>
        public override void Close()
        {
            internalReader = ReadingNotAvailable;
            crc = null;
            entry = null;
            base.Close();
        }

        /// <summary>Closes the entry.</summary>
        public void CloseEntry()
        {
            if (crc == null)
            {
                throw new InvalidOperationException("Closed");
            }
            if (entry == null)
            {
                return;
            }
            if (method == 8)
            {
                if ((flags & 8) != 0)
                {
                    var buffer = new byte[4096];
                    do
                    {
                        ;
                    }
                    while (Read(buffer, 0, buffer.Length) > 0);
                    return;
                }
                csize -= inf.TotalIn;
                inputBuffer.Available += inf.RemainingInput;
            }
            if (inputBuffer.Available > csize && csize >= 0L)
            {
                inputBuffer.Available = (int)(inputBuffer.Available - csize);
            }
            else
            {
                csize -= inputBuffer.Available;
                inputBuffer.Available = 0;
                long num;
                ZipInputStream zipInputStream;
                for (; csize != 0L; zipInputStream.csize -= num)
                {
                    num = Skip(csize);
                    if (num <= 0L)
                    {
                        throw new ZipException("Zip archive ends early.");
                    }
                    zipInputStream = this;
                }
            }
            CompleteCloseEntry(false);
        }

        /// <summary>Gets the next entry.</summary>
        /// <returns>The next entry.</returns>
        public ZipEntry GetNextEntry()
        {
            if (crc == null)
            {
                throw new InvalidOperationException("Closed.");
            }
            if (entry != null)
            {
                CloseEntry();
            }
            var num1 = inputBuffer.ReadLeInt();
            if (num1 == 33639248 || num1 == 101010256 || num1 == 84233040 || num1 == 117853008 || num1 == 101075792)
            {
                Close();
                return null;
            }
            if (num1 == 808471376 || num1 == 134695760)
            {
                num1 = inputBuffer.ReadLeInt();
            }
            if (num1 != 67324752)
            {
                throw new ZipException("Wrong Local header signature: 0x" + string.Format("{0:X}", num1));
            }
            var num2 = (short)inputBuffer.ReadLeShort();
            flags = inputBuffer.ReadLeShort();
            method = inputBuffer.ReadLeShort();
            var num3 = (uint)inputBuffer.ReadLeInt();
            var num4 = inputBuffer.ReadLeInt();
            csize = inputBuffer.ReadLeInt();
            size = inputBuffer.ReadLeInt();
            var length1 = inputBuffer.ReadLeShort();
            var length2 = inputBuffer.ReadLeShort();
            var flag = (flags & 1) == 1;
            var numArray = new byte[length1];
            inputBuffer.ReadRawBuffer(numArray);
            entry = new ZipEntry(ZipConstants.ConvertToStringExt(flags, numArray), num2)
            {
                Flags = flags,
                CompressionMethod = (CompressionMethod)method
            };
            if ((flags & 8) == 0)
            {
                entry.Crc = num4 & uint.MaxValue;
                entry.Size = size & uint.MaxValue;
                entry.CompressedSize = csize & uint.MaxValue;
                entry.CryptoCheckValue = (byte)((num4 >> 24) & byte.MaxValue);
            }
            else
            {
                if (num4 != 0)
                {
                    entry.Crc = num4 & uint.MaxValue;
                }
                if (size != 0L)
                {
                    entry.Size = size & uint.MaxValue;
                }
                if (csize != 0L)
                {
                    entry.CompressedSize = csize & uint.MaxValue;
                }
                entry.CryptoCheckValue = (byte)((num3 >> 8) & byte.MaxValue);
            }
            entry.DosTime = num3;
            if (length2 > 0)
            {
                var buffer = new byte[length2];
                inputBuffer.ReadRawBuffer(buffer);
                entry.ExtraData = buffer;
            }
            entry.ProcessExtraData(true);
            if (entry.CompressedSize >= 0L)
            {
                csize = entry.CompressedSize;
            }
            if (entry.Size >= 0L)
            {
                size = entry.Size;
            }
            if (method == 0 && (!flag && csize != size || flag && csize - 12L != size))
            {
                throw new ZipException("Stored, but compressed != uncompressed");
            }
            internalReader = !entry.IsCompressionMethodSupported()
                ? ReadingNotSupported
                : new ReadDataHandler(InitialRead);
            return entry;
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
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
            return internalReader(buffer, offset, count);
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            var buffer = new byte[1];
            return Read(buffer, 0, 1) <= 0 ? -1 : buffer[0] & byte.MaxValue;
        }

        /// <summary>Body read.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        /// <returns>An int.</returns>
        private int BodyRead(byte[] buffer, int offset, int count)
        {
            if (crc == null)
            {
                throw new InvalidOperationException("Closed");
            }
            if (entry == null || count <= 0)
            {
                return 0;
            }
            if (offset + count > buffer.Length)
            {
                throw new ArgumentException("Offset + count exceeds buffer size");
            }
            var flag = false;
            switch (method)
            {
                case 0:
                {
                    if (count > csize && csize >= 0L)
                    {
                        count = (int)csize;
                    }
                    if (count > 0)
                    {
                        count = inputBuffer.ReadClearTextBuffer(buffer, offset, count);
                        if (count > 0)
                        {
                            csize -= count;
                            size -= count;
                        }
                    }
                    if (csize == 0L)
                    {
                        flag = true;
                        break;
                    }
                    if (count < 0)
                    {
                        throw new ZipException("EOF in stored block");
                    }
                    break;
                }
                case 8:
                {
                    count = base.Read(buffer, offset, count);
                    if (count <= 0)
                    {
                        inputBuffer.Available = inf.IsFinished
                            ? inf.RemainingInput
                            : throw new ZipException("Inflater not finished!");
                        if ((flags & 8) == 0
                            && (inf.TotalIn != csize && csize != uint.MaxValue && csize != -1L || inf.TotalOut != size))
                        {
                            throw new ZipException(
                                "Size mismatch: " + csize + ";" + size + " <-> " + inf.TotalIn + ";" + inf.TotalOut);
                        }
                        inf.Reset();
                        flag = true;
                    }
                    break;
                }
            }
            if (count > 0)
            {
                crc.Update(buffer, offset, count);
            }
            if (flag)
            {
                CompleteCloseEntry(true);
            }
            return count;
        }

        /// <summary>Complete close entry.</summary>
        /// <param name="testCrc">True to test CRC.</param>
        private void CompleteCloseEntry(bool testCrc)
        {
            StopDecrypting();
            if ((flags & 8) != 0)
            {
                ReadDataDescriptor();
            }
            size = 0L;
            if (testCrc && (crc.Value & uint.MaxValue) != entry.Crc && entry.Crc != -1L)
            {
                throw new ZipException("CRC mismatch");
            }
            crc.Reset();
            if (method == 8)
            {
                inf.Reset();
            }
            entry = null;
        }

        /// <summary>Initial read.</summary>
        /// <param name="destination">Destination for the.</param>
        /// <param name="offset">     The offset.</param>
        /// <param name="count">      Number of.</param>
        /// <returns>An int.</returns>
        private int InitialRead(byte[] destination, int offset, int count)
        {
            if (!CanDecompressEntry)
            {
                throw new ZipException(
                    "Library cannot extract this entry. Version required is (" + entry.Version + ")");
            }
            if (entry.IsCrypted)
            {
                inputBuffer.CryptoTransform = Password != null
                    ? new PkzipClassicManaged().CreateDecryptor(
                        PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(Password)),
                        null)
                    : throw new ZipException("No password set.");
                var outBuffer = new byte[12];
                inputBuffer.ReadClearTextBuffer(outBuffer, 0, 12);
                if (outBuffer[11] != entry.CryptoCheckValue)
                {
                    throw new ZipException("Invalid password");
                }
                if (csize >= 12L)
                {
                    csize -= 12L;
                }
                else if ((entry.Flags & 8) == 0)
                {
                    throw new ZipException(string.Format("Entry compressed size {0} too small for encryption", csize));
                }
            }
            else
            {
                inputBuffer.CryptoTransform = null;
            }
            if (csize > 0L || (flags & 8) != 0)
            {
                if (method == 8 && inputBuffer.Available > 0)
                {
                    inputBuffer.SetInflaterInput(inf);
                }
                internalReader = BodyRead;
                return BodyRead(destination, offset, count);
            }
            internalReader = ReadingNotAvailable;
            return 0;
        }

        /// <summary>Reads data descriptor.</summary>
        private void ReadDataDescriptor()
        {
            if (inputBuffer.ReadLeInt() != 134695760)
            {
                throw new ZipException("Data descriptor signature not found");
            }
            entry.Crc = inputBuffer.ReadLeInt() & uint.MaxValue;
            if (entry.LocalHeaderRequiresZip64)
            {
                csize = inputBuffer.ReadLeLong();
                size = inputBuffer.ReadLeLong();
            }
            else
            {
                csize = inputBuffer.ReadLeInt();
                size = inputBuffer.ReadLeInt();
            }
            entry.CompressedSize = csize;
            entry.Size = size;
        }

        /// <summary>Reading not available.</summary>
        /// <param name="destination">Destination for the.</param>
        /// <param name="offset">     The offset.</param>
        /// <param name="count">      Number of.</param>
        /// <returns>An int.</returns>
        private int ReadingNotAvailable(byte[] destination, int offset, int count)
        {
            throw new InvalidOperationException("Unable to read from this stream");
        }

        /// <summary>Reading not supported.</summary>
        /// <param name="destination">Destination for the.</param>
        /// <param name="offset">     The offset.</param>
        /// <param name="count">      Number of.</param>
        /// <returns>An int.</returns>
        private int ReadingNotSupported(byte[] destination, int offset, int count)
        {
            throw new ZipException("The compression method for this entry is not supported");
        }
    }
}
