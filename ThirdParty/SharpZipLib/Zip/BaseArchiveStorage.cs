// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>A base archive storage.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.IArchiveStorage" />
    public abstract class BaseArchiveStorage : IArchiveStorage
    {
        /// <summary>True if disposed.</summary>
        protected bool disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage" />
        ///     class.
        /// </summary>
        /// <param name="updateMode">The update mode.</param>
        protected BaseArchiveStorage(FileUpdateMode updateMode)
        {
            UpdateMode = updateMode;
        }

        /// <inheritdoc/>
        public FileUpdateMode UpdateMode { get; }

        /// <summary>Convert temporary to final.</summary>
        /// <returns>BaseArchiveStorage converted to a temporary to final.</returns>
        public abstract Stream ConvertTemporaryToFinal();

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases the unmanaged resources used by the ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage and
        /// optionally releases the managed resources.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;
        }

        /// <summary>Gets temporary output.</summary>
        /// <returns>The temporary output.</returns>
        public abstract Stream GetTemporaryOutput();

        /// <summary>Makes temporary copy.</summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A Stream.</returns>
        public abstract Stream MakeTemporaryCopy(Stream stream);

        /// <summary>Opens for direct update.</summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A Stream.</returns>
        public abstract Stream OpenForDirectUpdate(Stream stream);
    }
}
