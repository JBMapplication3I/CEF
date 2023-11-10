// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipInputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.GZip
{
    using System.IO;

    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip.Compression;
    using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

    /// <summary>A zip input stream.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" />
    public class GZipInputStream : InflaterInputStream
    {
        /// <summary>The CRC.</summary>
        protected Crc32 crc;

        /// <summary>True to read gzip header.</summary>
        private bool readGZIPHeader;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipInputStream" />
        ///     class.
        /// </summary>
        /// <param name="baseInputStream">The base input stream.</param>
        public GZipInputStream(Stream baseInputStream)
            : this(baseInputStream, 4096)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipInputStream" />
        ///     class.
        /// </summary>
        /// <param name="baseInputStream">The base input stream.</param>
        /// <param name="size">           The size.</param>
        public GZipInputStream(Stream baseInputStream, int size)
            : base(baseInputStream, new Inflater(true), size)
        {
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            while (this.readGZIPHeader || this.ReadHeader())
            {
                var count1 = base.Read(buffer, offset, count);
                if (count1 > 0)
                {
                    this.crc.Update(buffer, offset, count1);
                }

                if (this.inf.IsFinished)
                {
                    this.ReadFooter();
                }

                if (count1 > 0)
                {
                    return count1;
                }
            }

            return 0;
        }

        /// <summary>Reads the footer.</summary>
        private void ReadFooter()
        {
            var outBuffer = new byte[8];
            var num1 = this.inf.TotalOut & uint.MaxValue;
            this.inputBuffer.Available += this.inf.RemainingInput;
            this.inf.Reset();
            int num2;
            for (var length = 8; length > 0; length -= num2)
            {
                num2 = this.inputBuffer.ReadClearTextBuffer(outBuffer, 8 - length, length);
                if (num2 <= 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP footer");
                }
            }

            var num3 = (outBuffer[0] & byte.MaxValue) | ((outBuffer[1] & byte.MaxValue) << 8)
                                                      | ((outBuffer[2] & byte.MaxValue) << 16) | (outBuffer[3] << 24);
            if (num3 != (int)this.crc.Value)
            {
                throw new GZipException(
                    "GZIP crc sum mismatch, theirs \"" + num3 + "\" and ours \"" + (int)this.crc.Value);
            }

            var num4 = (uint)((outBuffer[4] & byte.MaxValue) | ((outBuffer[5] & byte.MaxValue) << 8)
                                                             | ((outBuffer[6] & byte.MaxValue) << 16)
                                                             | (outBuffer[7] << 24));
            if (num1 != num4)
            {
                throw new GZipException("Number of bytes mismatch in footer");
            }

            this.readGZIPHeader = false;
        }

        /// <summary>Reads the header.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool ReadHeader()
        {
            this.crc = new Crc32();
            if (this.inputBuffer.Available <= 0)
            {
                this.inputBuffer.Fill();
                if (this.inputBuffer.Available <= 0)
                {
                    return false;
                }
            }

            var crc32 = new Crc32();
            var num1 = this.inputBuffer.ReadLeByte();
            if (num1 < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }

            crc32.Update(num1);
            if (num1 != 31)
            {
                throw new GZipException("Error GZIP header, first magic byte doesn't match");
            }

            var num2 = this.inputBuffer.ReadLeByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }

            if (num2 != 139)
            {
                throw new GZipException("Error GZIP header,  second magic byte doesn't match");
            }

            crc32.Update(num2);
            var num3 = this.inputBuffer.ReadLeByte();
            if (num3 < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }

            if (num3 != 8)
            {
                throw new GZipException("Error GZIP header, data not in deflate format");
            }

            crc32.Update(num3);
            var num4 = this.inputBuffer.ReadLeByte();
            if (num4 < 0)
            {
                throw new EndOfStreamException("EOS reading GZIP header");
            }

            crc32.Update(num4);
            if ((num4 & 224) != 0)
            {
                throw new GZipException("Reserved flag bits in GZIP header != 0");
            }

            for (var index = 0; index < 6; ++index)
            {
                var num5 = this.inputBuffer.ReadLeByte();
                if (num5 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }

                crc32.Update(num5);
            }

            if ((num4 & 4) != 0)
            {
                for (var index = 0; index < 2; ++index)
                {
                    var num5 = this.inputBuffer.ReadLeByte();
                    if (num5 < 0)
                    {
                        throw new EndOfStreamException("EOS reading GZIP header");
                    }

                    crc32.Update(num5);
                }

                var num6 = this.inputBuffer.ReadLeByte() >= 0 && this.inputBuffer.ReadLeByte() >= 0
                               ? this.inputBuffer.ReadLeByte()
                               : throw new EndOfStreamException("EOS reading GZIP header");
                var num7 = this.inputBuffer.ReadLeByte();
                if (num6 < 0 || num7 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }

                crc32.Update(num6);
                crc32.Update(num7);
                var num8 = (num6 << 8) | num7;
                for (var index = 0; index < num8; ++index)
                {
                    var num5 = this.inputBuffer.ReadLeByte();
                    if (num5 < 0)
                    {
                        throw new EndOfStreamException("EOS reading GZIP header");
                    }

                    crc32.Update(num5);
                }
            }

            if ((num4 & 8) != 0)
            {
                int num5;
                while ((num5 = this.inputBuffer.ReadLeByte()) > 0)
                {
                    crc32.Update(num5);
                }

                if (num5 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }

                crc32.Update(num5);
            }

            if ((num4 & 16) != 0)
            {
                int num5;
                while ((num5 = this.inputBuffer.ReadLeByte()) > 0)
                {
                    crc32.Update(num5);
                }

                if (num5 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }

                crc32.Update(num5);
            }

            if ((num4 & 2) != 0)
            {
                var num5 = this.inputBuffer.ReadLeByte();
                if (num5 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }

                var num6 = this.inputBuffer.ReadLeByte();
                if (num6 < 0)
                {
                    throw new EndOfStreamException("EOS reading GZIP header");
                }

                if (((num5 << 8) | num6) != ((int)crc32.Value & ushort.MaxValue))
                {
                    throw new GZipException("Header CRC value mismatch");
                }
            }

            this.readGZIPHeader = true;
            return true;
        }
    }
}