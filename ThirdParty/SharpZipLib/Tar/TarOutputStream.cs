// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarOutputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;

    /// <summary>A tar output stream.</summary>
    /// <seealso cref="System.IO.Stream" />
    public class TarOutputStream : Stream
    {
        /// <summary>Buffer for assembly data.</summary>
        protected byte[] assemblyBuffer;

        /// <summary>Buffer for block data.</summary>
        protected byte[] blockBuffer;

        /// <summary>The buffer.</summary>
        protected TarBuffer buffer;

        /// <summary>Size of the curr.</summary>
        protected long currSize;

        /// <summary>Stream to write data to.</summary>
        protected Stream outputStream;

        /// <summary>Length of the assembly buffer.</summary>
        private int assemblyBufferLength;

        /// <summary>The curr in bytes.</summary>
        private long currBytes;

        /// <summary>True if this TarOutputStream is closed.</summary>
        private bool isClosed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarOutputStream" />
        ///     class.
        /// </summary>
        /// <param name="outputStream">Stream to write data to.</param>
        public TarOutputStream(Stream outputStream) : this(outputStream, 20) { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarOutputStream" />
        ///     class.
        /// </summary>
        /// <param name="outputStream">Stream to write data to.</param>
        /// <param name="blockFactor"> The block factor.</param>
        public TarOutputStream(Stream outputStream, int blockFactor)
        {
            this.outputStream = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
            buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
            assemblyBuffer = new byte[512];
            blockBuffer = new byte[512];
        }

        /// <inheritdoc/>
        public override bool CanRead => outputStream.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => outputStream.CanSeek;

        /// <inheritdoc/>
        public override bool CanWrite => outputStream.CanWrite;

        /// <summary>Gets or sets a value indicating whether this TarOutputStream is stream owner.</summary>
        /// <value>True if this TarOutputStream is stream owner, false if not.</value>
        public bool IsStreamOwner
        {
            get => buffer.IsStreamOwner;
            set => buffer.IsStreamOwner = value;
        }

        /// <inheritdoc/>
        public override long Length => outputStream.Length;

        /// <inheritdoc/>
        public override long Position
        {
            get => outputStream.Position;
            set => outputStream.Position = value;
        }

        /// <summary>Gets the size of the record.</summary>
        /// <value>The size of the record.</value>
        public int RecordSize => buffer.RecordSize;

        /// <summary>Gets a value indicating whether an entry is open.</summary>
        /// <value>True if an entry is open, false if not.</value>
        private bool IsEntryOpen => currBytes < currSize;

        /// <inheritdoc/>
        public override void Close()
        {
            if (isClosed)
            {
                return;
            }
            isClosed = true;
            Finish();
            buffer.Close();
        }

        /// <summary>Closes the entry.</summary>
        public void CloseEntry()
        {
            if (assemblyBufferLength > 0)
            {
                Array.Clear(assemblyBuffer, assemblyBufferLength, assemblyBuffer.Length - assemblyBufferLength);
                buffer.WriteBlock(assemblyBuffer);
                currBytes += assemblyBufferLength;
                assemblyBufferLength = 0;
            }
            if (currBytes < currSize)
            {
                throw new TarException(
                    string.Format(
                        "Entry closed at '{0}' before the '{1}' bytes specified in the header were written",
                        currBytes,
                        currSize));
            }
        }

        /// <summary>Finishes this TarOutputStream.</summary>
        public void Finish()
        {
            if (IsEntryOpen)
            {
                CloseEntry();
            }
            WriteEofBlock();
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            outputStream.Flush();
        }

        /// <summary>(This method is obsolete) gets record size.</summary>
        /// <returns>The record size.</returns>
        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return buffer.RecordSize;
        }

        /// <summary>Puts next entry.</summary>
        /// <param name="entry">The entry.</param>
        public void PutNextEntry(TarEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (entry.TarHeader.Name.Length >= 100)
            {
                var tarHeader = new TarHeader { TypeFlag = 76 };
                tarHeader.Name += "././@LongLink";
                tarHeader.UserId = 0;
                tarHeader.GroupId = 0;
                tarHeader.GroupName = "";
                tarHeader.UserName = "";
                tarHeader.LinkName = "";
                tarHeader.Size = entry.TarHeader.Name.Length;
                tarHeader.WriteHeader(blockBuffer);
                buffer.WriteBlock(blockBuffer);
                var nameOffset = 0;
                while (nameOffset < entry.TarHeader.Name.Length)
                {
                    Array.Clear(blockBuffer, 0, blockBuffer.Length);
                    TarHeader.GetAsciiBytes(entry.TarHeader.Name, nameOffset, blockBuffer, 0, 512);
                    nameOffset += 512;
                    buffer.WriteBlock(blockBuffer);
                }
            }
            entry.WriteEntryHeader(blockBuffer);
            buffer.WriteBlock(blockBuffer);
            currBytes = 0L;
            currSize = entry.IsDirectory ? 0L : entry.Size;
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return outputStream.Read(buffer, offset, count);
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            return outputStream.ReadByte();
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return outputStream.Seek(offset, origin);
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            outputStream.SetLength(value);
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be negative");
            }
            if (buffer.Length - offset < count)
            {
                throw new ArgumentException("offset and count combination is invalid");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            }
            if (currBytes + count > currSize)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(count),
                    string.Format(
                        "request to write '{0}' bytes exceeds size in header of '{1}' bytes",
                        count,
                        currSize));
            }
            if (assemblyBufferLength > 0)
            {
                if (assemblyBufferLength + count >= blockBuffer.Length)
                {
                    var length = blockBuffer.Length - assemblyBufferLength;
                    Array.Copy(assemblyBuffer, 0, blockBuffer, 0, assemblyBufferLength);
                    Array.Copy(buffer, offset, blockBuffer, assemblyBufferLength, length);
                    this.buffer.WriteBlock(blockBuffer);
                    currBytes += blockBuffer.Length;
                    offset += length;
                    count -= length;
                    assemblyBufferLength = 0;
                }
                else
                {
                    Array.Copy(buffer, offset, assemblyBuffer, assemblyBufferLength, count);
                    offset += count;
                    assemblyBufferLength += count;
                    count -= count;
                }
            }
            while (count > 0)
            {
                if (count < blockBuffer.Length)
                {
                    Array.Copy(buffer, offset, assemblyBuffer, assemblyBufferLength, count);
                    assemblyBufferLength += count;
                    break;
                }
                this.buffer.WriteBlock(buffer, offset);
                var length = blockBuffer.Length;
                currBytes += length;
                count -= length;
                offset += length;
            }
        }

        /// <inheritdoc/>
        public override void WriteByte(byte value)
        {
            Write(new byte[1] { value }, 0, 1);
        }

        /// <summary>Writes the EOF block.</summary>
        private void WriteEofBlock()
        {
            Array.Clear(blockBuffer, 0, blockBuffer.Length);
            buffer.WriteBlock(blockBuffer);
        }
    }
}
