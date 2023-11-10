// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;
    using System.IO;

    /// <summary>An inflater input stream.</summary>
    /// <seealso cref="System.IO.Stream" />
    public class InflaterInputStream : Stream
    {
        /// <summary>The csize.</summary>
        protected long csize;

        /// <summary>The inf.</summary>
        protected Inflater inf;

        /// <summary>Buffer for input data.</summary>
        protected InflaterInputBuffer inputBuffer;

        /// <summary>The base input stream.</summary>
        private readonly Stream baseInputStream;

        /// <summary>True if this InflaterInputStream is closed.</summary>
        private bool isClosed;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" /> class.
        /// </summary>
        /// <param name="baseInputStream">The base input stream.</param>
        public InflaterInputStream(Stream baseInputStream) : this(baseInputStream, new Inflater(), 4096) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" /> class.
        /// </summary>
        /// <param name="baseInputStream">The base input stream.</param>
        /// <param name="inf">            The inf.</param>
        public InflaterInputStream(Stream baseInputStream, Inflater inf) : this(baseInputStream, inf, 4096) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" /> class.
        /// </summary>
        /// <param name="baseInputStream">The base input stream.</param>
        /// <param name="inflater">       The inflater.</param>
        /// <param name="bufferSize">     Size of the buffer.</param>
        public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }
            this.baseInputStream = baseInputStream ?? throw new ArgumentNullException(nameof(baseInputStream));
            inf = inflater ?? throw new ArgumentNullException(nameof(inflater));
            inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
        }

        /// <summary>Gets the available.</summary>
        /// <value>The available.</value>
        public virtual int Available => !inf.IsFinished ? 1 : 0;

        /// <inheritdoc/>
        public override bool CanRead => baseInputStream.CanRead;

        /// <inheritdoc/>
        public override bool CanSeek => false;

        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <summary>Gets or sets a value indicating whether this InflaterInputStream is stream owner.</summary>
        /// <value>True if this InflaterInputStream is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; } = true;

        /// <inheritdoc/>
        public override long Length => inputBuffer.RawLength;

        /// <inheritdoc/>
        public override long Position
        {
            get => baseInputStream.Position;
            set => throw new NotSupportedException("InflaterInputStream Position not supported");
        }

        /// <inheritdoc/>
        public override IAsyncResult BeginWrite(
            byte[] buffer,
            int offset,
            int count,
            AsyncCallback callback,
            object state)
        {
            throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
        }

        /// <inheritdoc/>
        public override void Close()
        {
            if (isClosed)
            {
                return;
            }
            isClosed = true;
            if (!IsStreamOwner)
            {
                return;
            }
            baseInputStream.Close();
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            baseInputStream.Flush();
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (inf.IsNeedingDictionary)
            {
                throw new SharpZipBaseException("Need a dictionary");
            }
            var count1 = count;
            int num;
            do
            {
                num = inf.Inflate(buffer, offset, count1);
                offset += num;
                count1 -= num;
                if (count1 != 0 && !inf.IsFinished)
                {
                    if (inf.IsNeedingInput)
                    {
                        Fill();
                    }
                }
                else
                {
                    goto label_8;
                }
            }
            while (num != 0);
            throw new ZipException("Dont know what to do");
        label_8:
            return count - count1;
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seek not supported");
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("InflaterInputStream SetLength not supported");
        }

        /// <summary>Skips the given count.</summary>
        /// <param name="count">Number of.</param>
        /// <returns>A long.</returns>
        public long Skip(long count)
        {
            if (count <= 0L)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if (baseInputStream.CanSeek)
            {
                baseInputStream.Seek(count, SeekOrigin.Current);
                return count;
            }
            var count1 = 2048;
            if (count < count1)
            {
                count1 = (int)count;
            }
            var buffer = new byte[count1];
            var num1 = 1;
            long num2;
            for (num2 = count; num2 > 0L && num1 > 0; num2 -= (long)num1)
            {
                if (num2 < count1)
                {
                    count1 = (int)num2;
                }
                num1 = baseInputStream.Read(buffer, 0, count1);
            }
            return count - num2;
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("InflaterInputStream Write not supported");
        }

        /// <inheritdoc/>
        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("InflaterInputStream WriteByte not supported");
        }

        /// <summary>Fills this InflaterInputStream.</summary>
        protected void Fill()
        {
            if (inputBuffer.Available <= 0)
            {
                inputBuffer.Fill();
                if (inputBuffer.Available <= 0)
                {
                    throw new SharpZipBaseException("Unexpected EOF");
                }
            }
            inputBuffer.SetInflaterInput(inf);
        }

        /// <summary>Stops a decrypting.</summary>
        protected void StopDecrypting()
        {
            inputBuffer.CryptoTransform = null;
        }
    }
}
