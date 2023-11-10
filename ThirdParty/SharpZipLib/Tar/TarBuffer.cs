// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarBuffer
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;

    /// <summary>Buffer for tar.</summary>
    public class TarBuffer
    {
        /// <summary>Size of the block.</summary>
        public const int BlockSize = 512;

        /// <summary>The default block factor.</summary>
        public const int DefaultBlockFactor = 20;

        /// <summary>The default record size.</summary>
        public const int DefaultRecordSize = 10240;

        /// <summary>Stream to read data from.</summary>
        private Stream inputStream;

        /// <summary>Stream to write data to.</summary>
        private Stream outputStream;

        /// <summary>Buffer for record data.</summary>
        private byte[] recordBuffer;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarBuffer" /> class.</summary>
        protected TarBuffer() { }

        /// <summary>Gets the block factor.</summary>
        /// <value>The block factor.</value>
        public int BlockFactor { get; private set; } = 20;

        /// <summary>Gets the current block.</summary>
        /// <value>The current block.</value>
        public int CurrentBlock { get; private set; }

        /// <summary>Gets the current record.</summary>
        /// <value>The current record.</value>
        public int CurrentRecord { get; private set; }

        /// <summary>Gets or sets a value indicating whether this TarBuffer is stream owner.</summary>
        /// <value>True if this TarBuffer is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; } = true;

        /// <summary>Gets the size of the record.</summary>
        /// <value>The size of the record.</value>
        public int RecordSize { get; private set; } = 10240;

        /// <summary>Creates input tar buffer.</summary>
        /// <param name="inputStream">Stream to read data from.</param>
        /// <returns>The new input tar buffer.</returns>
        public static TarBuffer CreateInputTarBuffer(Stream inputStream)
        {
            return inputStream != null
                ? CreateInputTarBuffer(inputStream, 20)
                : throw new ArgumentNullException(nameof(inputStream));
        }

        /// <summary>Creates input tar buffer.</summary>
        /// <param name="inputStream">Stream to read data from.</param>
        /// <param name="blockFactor">The block factor.</param>
        /// <returns>The new input tar buffer.</returns>
        public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
        {
            if (blockFactor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockFactor), "Factor cannot be negative");
            }
            var tarBuffer = new TarBuffer
            {
                inputStream = inputStream ?? throw new ArgumentNullException(nameof(inputStream)),
                outputStream = null
            };
            tarBuffer.Initialize(blockFactor);
            return tarBuffer;
        }

        /// <summary>Creates output tar buffer.</summary>
        /// <param name="outputStream">Stream to write data to.</param>
        /// <returns>The new output tar buffer.</returns>
        public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
        {
            return outputStream != null
                ? CreateOutputTarBuffer(outputStream, 20)
                : throw new ArgumentNullException(nameof(outputStream));
        }

        /// <summary>Creates output tar buffer.</summary>
        /// <param name="outputStream">Stream to write data to.</param>
        /// <param name="blockFactor"> The block factor.</param>
        /// <returns>The new output tar buffer.</returns>
        public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
        {
            if (blockFactor <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockFactor), "Factor cannot be negative");
            }
            var tarBuffer = new TarBuffer
            {
                inputStream = null,
                outputStream = outputStream ?? throw new ArgumentNullException(nameof(outputStream))
            };
            tarBuffer.Initialize(blockFactor);
            return tarBuffer;
        }

        /// <summary>Query if 'block' is end of archive block.</summary>
        /// <param name="block">The block.</param>
        /// <returns>True if end of archive block, false if not.</returns>
        public static bool IsEndOfArchiveBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
            }
            if (block.Length != 512)
            {
                throw new ArgumentException("block length is invalid");
            }
            for (var index = 0; index < 512; ++index)
            {
                if (block[index] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Closes this TarBuffer.</summary>
        public void Close()
        {
            if (outputStream != null)
            {
                WriteFinalRecord();
                if (IsStreamOwner)
                {
                    outputStream.Close();
                }
                outputStream = null;
            }
            else
            {
                if (inputStream == null)
                {
                    return;
                }
                if (IsStreamOwner)
                {
                    inputStream.Close();
                }
                inputStream = null;
            }
        }

        /// <summary>(This method is obsolete) gets block factor.</summary>
        /// <returns>The block factor.</returns>
        [Obsolete("Use BlockFactor property instead")]
        public int GetBlockFactor()
        {
            return BlockFactor;
        }

        /// <summary>(This method is obsolete) gets current block number.</summary>
        /// <returns>The current block number.</returns>
        [Obsolete("Use CurrentBlock property instead")]
        public int GetCurrentBlockNum()
        {
            return CurrentBlock;
        }

        /// <summary>(This method is obsolete) gets current record number.</summary>
        /// <returns>The current record number.</returns>
        [Obsolete("Use CurrentRecord property instead")]
        public int GetCurrentRecordNum()
        {
            return CurrentRecord;
        }

        /// <summary>(This method is obsolete) gets record size.</summary>
        /// <returns>The record size.</returns>
        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return RecordSize;
        }

        /// <summary>(This method is obsolete) query if 'block' is EOF block.</summary>
        /// <param name="block">The block.</param>
        /// <returns>True if EOF block, false if not.</returns>
        [Obsolete("Use IsEndOfArchiveBlock instead")]
        public bool IsEOFBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
            }
            if (block.Length != 512)
            {
                throw new ArgumentException("block length is invalid");
            }
            for (var index = 0; index < 512; ++index)
            {
                if (block[index] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Reads the block.</summary>
        /// <returns>An array of byte.</returns>
        public byte[] ReadBlock()
        {
            if (inputStream == null)
            {
                throw new TarException("TarBuffer.ReadBlock - no input stream defined");
            }
            if (CurrentBlock >= BlockFactor && !ReadRecord())
            {
                throw new TarException("Failed to read a record");
            }
            var numArray = new byte[512];
            Array.Copy(recordBuffer, CurrentBlock * 512, numArray, 0, 512);
            ++CurrentBlock;
            return numArray;
        }

        /// <summary>Skip block.</summary>
        public void SkipBlock()
        {
            if (inputStream == null)
            {
                throw new TarException("no input stream defined");
            }
            if (CurrentBlock >= BlockFactor && !ReadRecord())
            {
                throw new TarException("Failed to read a record");
            }
            ++CurrentBlock;
        }

        /// <summary>Writes a block.</summary>
        /// <param name="block">The block.</param>
        public void WriteBlock(byte[] block)
        {
            if (block == null)
            {
                throw new ArgumentNullException(nameof(block));
            }
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteBlock - no output stream defined");
            }
            if (block.Length != 512)
            {
                throw new TarException(
                    string.Format(
                        "TarBuffer.WriteBlock - block to write has length '{0}' which is not the block size of '{1}'",
                        block.Length,
                        512));
            }
            if (CurrentBlock >= BlockFactor)
            {
                WriteRecord();
            }
            Array.Copy(block, 0, recordBuffer, CurrentBlock * 512, 512);
            ++CurrentBlock;
        }

        /// <summary>Writes a block.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        public void WriteBlock(byte[] buffer, int offset)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteBlock - no output stream stream defined");
            }
            if (offset < 0 || offset >= buffer.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }
            if (offset + 512 > buffer.Length)
            {
                throw new TarException(
                    string.Format(
                        "TarBuffer.WriteBlock - record has length '{0}' with offset '{1}' which is less than the record size of '{2}'",
                        buffer.Length,
                        offset,
                        RecordSize));
            }
            if (CurrentBlock >= BlockFactor)
            {
                WriteRecord();
            }
            Array.Copy(buffer, offset, recordBuffer, CurrentBlock * 512, 512);
            ++CurrentBlock;
        }

        /// <summary>Initializes this TarBuffer.</summary>
        /// <param name="archiveBlockFactor">The archive block factor.</param>
        private void Initialize(int archiveBlockFactor)
        {
            BlockFactor = archiveBlockFactor;
            RecordSize = archiveBlockFactor * 512;
            recordBuffer = new byte[RecordSize];
            if (inputStream != null)
            {
                CurrentRecord = -1;
                CurrentBlock = BlockFactor;
            }
            else
            {
                CurrentRecord = 0;
                CurrentBlock = 0;
            }
        }

        /// <summary>Reads the record.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool ReadRecord()
        {
            if (inputStream == null)
            {
                throw new TarException("no input stream stream defined");
            }
            CurrentBlock = 0;
            var offset = 0;
            long num;
            for (var recordSize = RecordSize; recordSize > 0; recordSize -= (int)num)
            {
                num = inputStream.Read(recordBuffer, offset, recordSize);
                if (num > 0L)
                {
                    offset += (int)num;
                }
                else
                {
                    break;
                }
            }
            ++CurrentRecord;
            return true;
        }

        /// <summary>Writes the final record.</summary>
        private void WriteFinalRecord()
        {
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteFinalRecord no output stream defined");
            }
            if (CurrentBlock > 0)
            {
                var index = CurrentBlock * 512;
                Array.Clear(recordBuffer, index, RecordSize - index);
                WriteRecord();
            }
            outputStream.Flush();
        }

        /// <summary>Writes the record.</summary>
        private void WriteRecord()
        {
            if (outputStream == null)
            {
                throw new TarException("TarBuffer.WriteRecord no output stream defined");
            }
            outputStream.Write(recordBuffer, 0, RecordSize);
            outputStream.Flush();
            CurrentBlock = 0;
            ++CurrentRecord;
        }
    }
}
