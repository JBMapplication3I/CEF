// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.MemoryArchiveStorage
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;
    using Core;

    /// <summary>A memory archive storage.</summary>
    /// <seealso cref="BaseArchiveStorage" />
    public class MemoryArchiveStorage : BaseArchiveStorage
    {
        /// <summary>The temporary stream.</summary>
        private MemoryStream temporaryStream_;

        /// <summary>Initializes a new instance of the <see cref="MemoryArchiveStorage" /> class.</summary>
        public MemoryArchiveStorage() : base(FileUpdateMode.Direct) { }

        /// <summary>Initializes a new instance of the <see cref="MemoryArchiveStorage" /> class.</summary>
        /// <param name="updateMode">The update mode.</param>
        public MemoryArchiveStorage(FileUpdateMode updateMode) : base(updateMode) { }

        /// <summary>Gets the final stream.</summary>
        /// <value>The final stream.</value>
        public MemoryStream FinalStream { get; private set; }

        /// <inheritdoc/>
        public override Stream ConvertTemporaryToFinal()
        {
            FinalStream = temporaryStream_ != null
                ? new MemoryStream(temporaryStream_.ToArray())
                : throw new ZipException("No temporary stream has been created");
            return FinalStream;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                temporaryStream_?.Close();
            }
            base.Dispose(disposing);
        }

        /// <inheritdoc/>
        public override Stream GetTemporaryOutput()
        {
            temporaryStream_ = new MemoryStream();
            return temporaryStream_;
        }

        /// <inheritdoc/>
        public override Stream MakeTemporaryCopy(Stream stream)
        {
            temporaryStream_ = new MemoryStream();
            stream.Position = 0L;
            StreamUtils.Copy(stream, temporaryStream_, new byte[4096]);
            return temporaryStream_;
        }

        /// <inheritdoc/>
        public override Stream OpenForDirectUpdate(Stream stream)
        {
            Stream destination;
            if (stream == null || !stream.CanWrite)
            {
                destination = new MemoryStream();
                if (stream != null)
                {
                    stream.Position = 0L;
                    StreamUtils.Copy(stream, destination, new byte[4096]);
                    stream.Close();
                }
            }
            else
            {
                destination = stream;
            }
            return destination;
        }
    }
}
