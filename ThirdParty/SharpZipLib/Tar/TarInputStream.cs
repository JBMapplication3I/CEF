// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarInputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>A tar input stream.</summary>
    /// <seealso cref="System.IO.Stream" />
    public class TarInputStream : Stream
    {
        /// <summary>The entry factory.</summary>
        protected IEntryFactory entryFactory;

        /// <summary>The entry offset.</summary>
        protected long entryOffset;

        /// <summary>Size of the entry.</summary>
        protected long entrySize;

        /// <summary>True if this TarInputStream has hit EOF.</summary>
        protected bool hasHitEOF;

        /// <summary>Buffer for read data.</summary>
        protected byte[] readBuffer;

        /// <summary>Buffer for tar data.</summary>
        protected TarBuffer tarBuffer;

        /// <summary>Stream to read data from.</summary>
        private readonly Stream inputStream;

        /// <summary>The current entry.</summary>
        private TarEntry currentEntry;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarInputStream" /> class.</summary>
        /// <param name="inputStream">Stream to read data from.</param>
        public TarInputStream(Stream inputStream) : this(inputStream, 20) { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarInputStream" /> class.</summary>
        /// <param name="inputStream">Stream to read data from.</param>
        /// <param name="blockFactor">The block factor.</param>
        public TarInputStream(Stream inputStream, int blockFactor)
        {
            this.inputStream = inputStream;
            tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
        }

        /// <summary>Interface for entry factory.</summary>
        /// <seealso cref="System.IO.Stream" />
        public interface IEntryFactory
        {
            /// <summary>Creates an entry.</summary>
            /// <param name="name">The name.</param>
            /// <returns>The new entry.</returns>
            TarEntry CreateEntry(string name);

            /// <summary>Creates an entry.</summary>
            /// <param name="headerBuffer">Buffer for header data.</param>
            /// <returns>The new entry.</returns>
            TarEntry CreateEntry(byte[] headerBuffer);

            /// <summary>Creates entry from file.</summary>
            /// <param name="fileName">Filename of the file.</param>
            /// <returns>The new entry from file.</returns>
            TarEntry CreateEntryFromFile(string fileName);
        }

        /// <summary>Gets the available.</summary>
        /// <value>The available.</value>
        public long Available => entrySize - entryOffset;

        /// <inheritdoc/>
        public override bool CanRead => inputStream.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => false;

        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <summary>Gets a value indicating whether this TarInputStream is mark supported.</summary>
        /// <value>True if this TarInputStream is mark supported, false if not.</value>
        public bool IsMarkSupported => false;

        /// <summary>Gets or sets a value indicating whether this TarInputStream is stream owner.</summary>
        /// <value>True if this TarInputStream is stream owner, false if not.</value>
        public bool IsStreamOwner
        {
            get => tarBuffer.IsStreamOwner;
            set => tarBuffer.IsStreamOwner = value;
        }

        /// <inheritdoc/>
        public override long Length => inputStream.Length;

        /// <inheritdoc/>
        public override long Position
        {
            get => inputStream.Position;
            set => throw new NotSupportedException("TarInputStream Seek not supported");
        }

        /// <summary>Gets the size of the record.</summary>
        /// <value>The size of the record.</value>
        public int RecordSize => tarBuffer.RecordSize;

        /// <inheritdoc/>
        public override void Close()
        {
            tarBuffer.Close();
            inputStream.Close();
        }

        /// <summary>Copies the entry contents described by outputStream.</summary>
        /// <param name="outputStream">Stream to write data to.</param>
        public void CopyEntryContents(Stream outputStream)
        {
            var buffer = new byte[32768];
            while (true)
            {
                var count = Read(buffer, 0, buffer.Length);
                if (count > 0)
                {
                    outputStream.Write(buffer, 0, count);
                }
                else
                {
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            inputStream.Flush();
        }

        /// <summary>Gets the next entry.</summary>
        /// <returns>The next entry.</returns>
        public TarEntry GetNextEntry()
        {
            if (hasHitEOF)
            {
                return null;
            }
            if (currentEntry != null)
            {
                SkipToNextEntry();
            }
            var numArray1 = tarBuffer.ReadBlock();
            if (numArray1 == null)
            {
                hasHitEOF = true;
            }
            else if (TarBuffer.IsEndOfArchiveBlock(numArray1))
            {
                hasHitEOF = true;
            }
            if (hasHitEOF)
            {
                currentEntry = null;
            }
            else
            {
                try
                {
                    var tarHeader = new TarHeader();
                    tarHeader.ParseBuffer(numArray1);
                    if (!tarHeader.IsChecksumValid)
                    {
                        throw new TarException("Header checksum is invalid");
                    }
                    entryOffset = 0L;
                    this.entrySize = tarHeader.Size;
                    StringBuilder stringBuilder = null;
                    if (tarHeader.TypeFlag == 76)
                    {
                        var numArray2 = new byte[512];
                        var entrySize = this.entrySize;
                        stringBuilder = new StringBuilder();
                        int length;
                        for (; entrySize > 0L; entrySize -= (long)length)
                        {
                            length = Read(
                                numArray2,
                                0,
                                entrySize > (long)numArray2.Length ? numArray2.Length : (int)entrySize);
                            if (length == -1)
                            {
                                throw new InvalidHeaderException("Failed to read long name entry");
                            }
                            stringBuilder.Append(TarHeader.ParseName(numArray2, 0, length));
                        }
                        SkipToNextEntry();
                        numArray1 = tarBuffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag == 103)
                    {
                        SkipToNextEntry();
                        numArray1 = tarBuffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag == 120)
                    {
                        SkipToNextEntry();
                        numArray1 = tarBuffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag == 86)
                    {
                        SkipToNextEntry();
                        numArray1 = tarBuffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag != 48 && tarHeader.TypeFlag != 0 && tarHeader.TypeFlag != 53)
                    {
                        SkipToNextEntry();
                        numArray1 = tarBuffer.ReadBlock();
                    }
                    if (entryFactory == null)
                    {
                        currentEntry = new TarEntry(numArray1);
                        if (stringBuilder != null)
                        {
                            currentEntry.Name = stringBuilder.ToString();
                        }
                    }
                    else
                    {
                        currentEntry = entryFactory.CreateEntry(numArray1);
                    }
                    entryOffset = 0L;
                    this.entrySize = currentEntry.Size;
                }
                catch (InvalidHeaderException ex)
                {
                    entrySize = 0L;
                    entryOffset = 0L;
                    currentEntry = null;
                    throw new InvalidHeaderException(
                        string.Format(
                            "Bad header in record {0} block {1} {2}",
                            tarBuffer.CurrentRecord,
                            tarBuffer.CurrentBlock,
                            ex.Message));
                }
            }
            return currentEntry;
        }

        /// <summary>(This method is obsolete) gets record size.</summary>
        /// <returns>The record size.</returns>
        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return tarBuffer.RecordSize;
        }

        /// <summary>Marks.</summary>
        /// <param name="markLimit">The mark limit.</param>
        public void Mark(int markLimit) { }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            var num1 = 0;
            if (entryOffset >= entrySize)
            {
                return 0;
            }
            long num2 = count;
            if (num2 + entryOffset > entrySize)
            {
                num2 = entrySize - entryOffset;
            }
            if (readBuffer != null)
            {
                var num3 = num2 > (long)readBuffer.Length ? readBuffer.Length : (int)num2;
                Array.Copy(readBuffer, 0, buffer, offset, num3);
                if (num3 >= readBuffer.Length)
                {
                    readBuffer = null;
                }
                else
                {
                    var length = readBuffer.Length - num3;
                    var numArray = new byte[length];
                    Array.Copy(readBuffer, num3, numArray, 0, length);
                    readBuffer = numArray;
                }
                num1 += num3;
                num2 -= num3;
                offset += num3;
            }
            while (num2 > 0L)
            {
                var numArray = tarBuffer.ReadBlock();
                if (numArray == null)
                {
                    throw new TarException("unexpected EOF with " + num2 + " bytes unread");
                }
                var num3 = (int)num2;
                var length = numArray.Length;
                if (length > num3)
                {
                    Array.Copy(numArray, 0, buffer, offset, num3);
                    readBuffer = new byte[length - num3];
                    Array.Copy(numArray, num3, readBuffer, 0, length - num3);
                }
                else
                {
                    num3 = length;
                    Array.Copy(numArray, 0, buffer, offset, length);
                }
                num1 += num3;
                num2 -= num3;
                offset += num3;
            }
            entryOffset += num1;
            return num1;
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            var buffer = new byte[1];
            return Read(buffer, 0, 1) <= 0 ? -1 : buffer[0];
        }

        /// <summary>Resets this TarInputStream.</summary>
        public void Reset() { }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("TarInputStream Seek not supported");
        }

        /// <summary>Sets entry factory.</summary>
        /// <param name="factory">The factory.</param>
        public void SetEntryFactory(IEntryFactory factory)
        {
            entryFactory = factory;
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("TarInputStream SetLength not supported");
        }

        /// <summary>Skips the given skip count.</summary>
        /// <param name="skipCount">Number of skips.</param>
        public void Skip(long skipCount)
        {
            var buffer = new byte[8192];
            int num;
            for (var index = skipCount; index > 0L; index -= (long)num)
            {
                var count = index > (long)buffer.Length ? buffer.Length : (int)index;
                num = Read(buffer, 0, count);
                if (num == -1)
                {
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("TarInputStream Write not supported");
        }

        /// <inheritdoc/>
        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("TarInputStream WriteByte not supported");
        }

        /// <summary>Skip to next entry.</summary>
        private void SkipToNextEntry()
        {
            var skipCount = entrySize - entryOffset;
            if (skipCount > 0L)
            {
                Skip(skipCount);
            }
            readBuffer = null;
        }

        /// <summary>An entry factory adapter.</summary>
        /// <seealso cref="ICSharpCode.SharpZipLib.Tar.TarInputStream.IEntryFactory" />
        public class EntryFactoryAdapter : IEntryFactory
        {
            /// <summary>Creates an entry.</summary>
            /// <param name="name">The name.</param>
            /// <returns>The new entry.</returns>
            public TarEntry CreateEntry(string name)
            {
                return TarEntry.CreateTarEntry(name);
            }

            /// <summary>Creates an entry.</summary>
            /// <param name="headerBuffer">Buffer for header data.</param>
            /// <returns>The new entry.</returns>
            public TarEntry CreateEntry(byte[] headerBuffer)
            {
                return new TarEntry(headerBuffer);
            }

            /// <summary>Creates entry from file.</summary>
            /// <param name="fileName">Filename of the file.</param>
            /// <returns>The new entry from file.</returns>
            public TarEntry CreateEntryFromFile(string fileName)
            {
                return TarEntry.CreateEntryFromFile(fileName);
            }
        }
    }
}
