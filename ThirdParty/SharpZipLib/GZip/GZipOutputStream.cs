// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipOutputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.GZip
{
    using System;
    using System.IO;

    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip.Compression;
    using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

    /// <summary>A zip output stream.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream" />
    public class GZipOutputStream : DeflaterOutputStream
    {
        /// <summary>The CRC.</summary>
        protected Crc32 crc = new();

        /// <summary>The state.</summary>
        private OutputState state_;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipOutputStream" />
        ///     class.
        /// </summary>
        /// <param name="baseOutputStream">The base output stream.</param>
        public GZipOutputStream(Stream baseOutputStream)
            : this(baseOutputStream, 4096)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.GZip.GZipOutputStream" />
        ///     class.
        /// </summary>
        /// <param name="baseOutputStream">The base output stream.</param>
        /// <param name="size">            The size.</param>
        public GZipOutputStream(Stream baseOutputStream, int size)
            : base(baseOutputStream, new Deflater(-1, true), size)
        {
        }

        /// <summary>Values that represent output states.</summary>
        private enum OutputState
        {
            /// <summary>An enum constant representing the header option.</summary>
            Header,

            /// <summary>An enum constant representing the footer option.</summary>
            Footer,

            /// <summary>An enum constant representing the finished option.</summary>
            Finished,

            /// <summary>An enum constant representing the closed option.</summary>
            Closed
        }

        /// <inheritdoc/>
        public override void Close()
        {
            try
            {
                this.Finish();
            }
            finally
            {
                if (this.state_ != OutputState.Closed)
                {
                    this.state_ = OutputState.Closed;
                    if (this.IsStreamOwner)
                    {
                        this.baseOutputStream_.Close();
                    }
                }
            }
        }

        /// <summary>Finishes this GZipOutputStream.</summary>
        public override void Finish()
        {
            if (this.state_ == OutputState.Header)
            {
                this.WriteHeader();
            }

            if (this.state_ != OutputState.Footer)
            {
                return;
            }

            this.state_ = OutputState.Finished;
            base.Finish();
            var num1 = (uint)((ulong)this.deflater_.TotalIn & uint.MaxValue);
            var num2 = (uint)((ulong)this.crc.Value & uint.MaxValue);
            var buffer = new byte[8]
                             {
                                 (byte)num2, (byte)(num2 >> 8), (byte)(num2 >> 16), (byte)(num2 >> 24), (byte)num1,
                                 (byte)(num1 >> 8), (byte)(num1 >> 16), (byte)(num1 >> 24)
                             };
            this.baseOutputStream_.Write(buffer, 0, buffer.Length);
        }

        /// <summary>Gets the level.</summary>
        /// <returns>The level.</returns>
        public int GetLevel()
        {
            return this.deflater_.GetLevel();
        }

        /// <summary>Sets a level.</summary>
        /// <param name="level">The level.</param>
        public void SetLevel(int level)
        {
            if (level < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(level));
            }

            this.deflater_.SetLevel(level);
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.state_ == OutputState.Header)
            {
                this.WriteHeader();
            }

            if (this.state_ != OutputState.Footer)
            {
                throw new InvalidOperationException("Write not permitted in current state");
            }

            this.crc.Update(buffer, offset, count);
            base.Write(buffer, offset, count);
        }

        /// <summary>Writes the header.</summary>
        private void WriteHeader()
        {
            if (this.state_ != OutputState.Header)
            {
                return;
            }

            this.state_ = OutputState.Footer;
            var num = (int)((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000L);
            var buffer = new byte[10]
                             {
                                 31, 139, 8, 0, (byte)num, (byte)(num >> 8), (byte)(num >> 16), (byte)(num >> 24), 0,
                                 byte.MaxValue
                             };
            this.baseOutputStream_.Write(buffer, 0, buffer.Length);
        }
    }
}