// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsBiffStream
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>The XLS biff stream.</summary>
    /// <seealso cref="Excel.Core.BinaryFormat.XlsStream"/>
    internal class XlsBiffStream : XlsStream
    {
        /// <summary>The bytes.</summary>
        private readonly byte[] bytes;

        /// <summary>The reader.</summary>
        private readonly ExcelBinaryReader reader;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsBiffStream"/> class.</summary>
        /// <param name="hdr">        The header.</param>
        /// <param name="streamStart">The stream start.</param>
        /// <param name="isMini">     True if this XlsBiffStream is mini.</param>
        /// <param name="rootDir">    The root dir.</param>
        /// <param name="reader">     The reader.</param>
        public XlsBiffStream(
            XlsHeader hdr,
            uint streamStart,
            bool isMini,
            XlsRootDirectory rootDir,
            ExcelBinaryReader reader) : base(hdr, streamStart, isMini, rootDir)
        {
            this.reader = reader;
            bytes = base.ReadStream();
            Size = bytes.Length;
            Position = 0;
        }

        /// <summary>Gets the position.</summary>
        /// <value>The position.</value>
        public int Position { get; private set; }

        /// <summary>Gets the size.</summary>
        /// <value>The size.</value>
        public int Size { get; }

        /// <summary>Gets the read.</summary>
        /// <returns>An XlsBiffRecord.</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public XlsBiffRecord Read()
        {
            if ((uint)Position >= bytes.Length)
            {
                return null;
            }
            var record = XlsBiffRecord.GetRecord(bytes, (uint)Position, reader);
            Position += record.Size;
            return Position > Size ? null : record;
        }

        /// <summary>Reads at.</summary>
        /// <param name="offset">The offset.</param>
        /// <returns>at.</returns>
        public XlsBiffRecord ReadAt(int offset)
        {
            if ((uint)offset >= bytes.Length)
            {
                return null;
            }
            var record = XlsBiffRecord.GetRecord(bytes, (uint)offset, reader);
            return reader.ReadOption == ReadOption.Strict && Position + record.Size > Size ? null : record;
        }

        /// <summary>(This method is obsolete) reads the stream.</summary>
        /// <returns>An array of byte.</returns>
        [Obsolete("Use BIFF-specific methods for this stream")]
        public new byte[] ReadStream()
        {
            return bytes;
        }

        /// <summary>Seeks.</summary>
        /// <param name="offset">The offset.</param>
        /// <param name="origin">The origin.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Seek(int offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                {
                    Position = offset;
                    break;
                }
                case SeekOrigin.Current:
                {
                    Position += offset;
                    break;
                }
                case SeekOrigin.End:
                {
                    Position = Size - offset;
                    break;
                }
            }
            if (Position < 0)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("{0} On offset={1}", "BIFF Stream error: Moving before stream start.", offset));
            }
            if (Position > Size)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("{0} On offset={1}", "BIFF Stream error: Moving after stream end.", offset));
            }
        }
    }
}
