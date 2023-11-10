// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using Encryption;

    /// <summary>A deflater output stream.</summary>
    /// <seealso cref="System.IO.Stream" />
    public class DeflaterOutputStream : Stream
    {
        /// <summary>The aes random.</summary>
        private static RNGCryptoServiceProvider _aesRnd;

        /// <summary>The aes authentication code.</summary>
        protected byte[] AESAuthCode;

        /// <summary>The base output stream.</summary>
        protected Stream baseOutputStream_;

        /// <summary>The deflater.</summary>
        protected Deflater deflater_;

        /// <summary>The buffer.</summary>
        private readonly byte[] buffer_;

        /// <summary>The crypto transform.</summary>
        private ICryptoTransform cryptoTransform_;

        /// <summary>True if this DeflaterOutputStream is closed.</summary>
        private bool isClosed_;

        /// <summary>The password.</summary>
        private string password;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream" /> class.
        /// </summary>
        /// <param name="baseOutputStream">The base output stream.</param>
        public DeflaterOutputStream(Stream baseOutputStream) : this(baseOutputStream, new Deflater(), 512) { }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream" /> class.
        /// </summary>
        /// <param name="baseOutputStream">The base output stream.</param>
        /// <param name="deflater">        The deflater.</param>
        public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater)
            : this(baseOutputStream, deflater, 512)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream" /> class.
        /// </summary>
        /// <param name="baseOutputStream">The base output stream.</param>
        /// <param name="deflater">        The deflater.</param>
        /// <param name="bufferSize">      Size of the buffer.</param>
        public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize)
        {
            if (baseOutputStream == null)
            {
                throw new ArgumentNullException(nameof(baseOutputStream));
            }
            if (!baseOutputStream.CanWrite)
            {
                throw new ArgumentException("Must support writing", nameof(baseOutputStream));
            }

            if (bufferSize < 512)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }
            baseOutputStream_ = baseOutputStream;
            buffer_ = new byte[bufferSize];
            deflater_ = deflater ?? throw new ArgumentNullException(nameof(deflater));
        }

        /// <summary>Gets a value indicating whether we can patch entries.</summary>
        /// <value>True if we can patch entries, false if not.</value>
        public bool CanPatchEntries => baseOutputStream_.CanSeek;

        /// <inheritdoc/>
        public override bool CanRead => false;

        /// <inheritdoc/>
        public override bool CanSeek => false;

        /// <inheritdoc/>
        public override bool CanWrite => baseOutputStream_.CanWrite;

        /// <summary>Gets or sets a value indicating whether this DeflaterOutputStream is stream owner.</summary>
        /// <value>True if this DeflaterOutputStream is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; } = true;

        /// <inheritdoc/>
        public override long Length => baseOutputStream_.Length;

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password
        {
            get => password;
            set
            {
                switch (value)
                {
                    case "":
                    {
                        password = null;
                        break;
                    }
                    default:
                    {
                        password = value;
                        break;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override long Position
        {
            get => baseOutputStream_.Position;
            set => throw new NotSupportedException("Position property not supported");
        }

        /// <inheritdoc/>
        public override IAsyncResult BeginRead(
            byte[] buffer,
            int offset,
            int count,
            AsyncCallback callback,
            object state)
        {
            throw new NotSupportedException("DeflaterOutputStream BeginRead not currently supported");
        }

        /// <inheritdoc/>
        public override IAsyncResult BeginWrite(
            byte[] buffer,
            int offset,
            int count,
            AsyncCallback callback,
            object state)
        {
            throw new NotSupportedException("BeginWrite is not supported");
        }

        /// <inheritdoc/>
        public override void Close()
        {
            if (isClosed_)
            {
                return;
            }
            isClosed_ = true;
            try
            {
                Finish();
                if (cryptoTransform_ == null)
                {
                    return;
                }
                GetAuthCodeIfAES();
                cryptoTransform_.Dispose();
                cryptoTransform_ = null;
            }
            finally
            {
                if (IsStreamOwner)
                {
                    baseOutputStream_.Close();
                }
            }
        }

        /// <summary>Finishes this DeflaterOutputStream.</summary>
        public virtual void Finish()
        {
            deflater_.Finish();
            while (!deflater_.IsFinished)
            {
                var num = deflater_.Deflate(buffer_, 0, buffer_.Length);
                if (num <= 0)
                {
                    break;
                }
                if (cryptoTransform_ != null)
                {
                    EncryptBlock(buffer_, 0, num);
                }
                baseOutputStream_.Write(buffer_, 0, num);
            }
            if (!deflater_.IsFinished)
            {
                throw new SharpZipBaseException("Can't deflate all input?");
            }
            baseOutputStream_.Flush();
            if (cryptoTransform_ == null)
            {
                return;
            }
            if (cryptoTransform_ is ZipAESTransform transform)
            {
                AESAuthCode = transform.GetAuthCode();
            }
            cryptoTransform_.Dispose();
            cryptoTransform_ = null;
        }

        /// <inheritdoc/>
        public override void Flush()
        {
            deflater_.Flush();
            Deflate();
            baseOutputStream_.Flush();
        }

        /// <inheritdoc/>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("DeflaterOutputStream Read not supported");
        }

        /// <inheritdoc/>
        public override int ReadByte()
        {
            throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
        }

        /// <inheritdoc/>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("DeflaterOutputStream Seek not supported");
        }

        /// <inheritdoc/>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
        }

        /// <inheritdoc/>
        public override void Write(byte[] buffer, int offset, int count)
        {
            deflater_.SetInput(buffer, offset, count);
            Deflate();
        }

        /// <inheritdoc/>
        public override void WriteByte(byte value)
        {
            Write(new byte[1] { value }, 0, 1);
        }

        /// <summary>Deflates this DeflaterOutputStream.</summary>
        protected void Deflate()
        {
            while (!deflater_.IsNeedingInput)
            {
                var num = deflater_.Deflate(buffer_, 0, buffer_.Length);
                if (num > 0)
                {
                    if (cryptoTransform_ != null)
                    {
                        EncryptBlock(buffer_, 0, num);
                    }
                    baseOutputStream_.Write(buffer_, 0, num);
                }
                else
                {
                    break;
                }
            }
            if (!deflater_.IsNeedingInput)
            {
                throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
            }
        }

        /// <summary>Encrypt block.</summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        protected void EncryptBlock(byte[] buffer, int offset, int length)
        {
            cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
        }

        /// <summary>Initializes the aes password.</summary>
        /// <param name="entry">      The entry.</param>
        /// <param name="rawPassword">The raw password.</param>
        /// <param name="salt">       The salt.</param>
        /// <param name="pwdVerifier">The password verifier.</param>
        protected void InitializeAESPassword(
            ZipEntry entry,
            string rawPassword,
            out byte[] salt,
            out byte[] pwdVerifier)
        {
            salt = new byte[entry.AESSaltLen];
            if (_aesRnd == null)
            {
                _aesRnd = new RNGCryptoServiceProvider();
            }
            _aesRnd.GetBytes(salt);
            var blockSize = entry.AESKeySize / 8;
            cryptoTransform_ = new ZipAESTransform(rawPassword, salt, blockSize, true);
            pwdVerifier = ((ZipAESTransform)cryptoTransform_).PwdVerifier;
        }

        /// <summary>Initializes the password.</summary>
        /// <param name="password">The password.</param>
        protected void InitializePassword(string password)
        {
            cryptoTransform_ = new PkzipClassicManaged().CreateEncryptor(
                PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(password)),
                null);
        }

        /// <summary>Gets authentication code if the es.</summary>
        private void GetAuthCodeIfAES()
        {
            if (!(cryptoTransform_ is ZipAESTransform))
            {
                return;
            }
            AESAuthCode = ((ZipAESTransform)cryptoTransform_).GetAuthCode();
        }
    }
}
