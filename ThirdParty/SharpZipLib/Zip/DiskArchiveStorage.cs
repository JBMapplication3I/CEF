// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DiskArchiveStorage
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>A disk archive storage.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage" />
    public class DiskArchiveStorage : BaseArchiveStorage
    {
        /// <summary>Filename of the file.</summary>
        private readonly string fileName_;

        /// <summary>Name of the temporary.</summary>
        private string temporaryName_;

        /// <summary>The temporary stream.</summary>
        private Stream temporaryStream_;

        /// <summary>Initializes a new instance of the <see cref="DiskArchiveStorage" /> class.</summary>
        /// <param name="file">      The file.</param>
        /// <param name="updateMode">The update mode.</param>
        public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode) : base(updateMode)
        {
            fileName_ = file.Name ?? throw new ZipException("Cant handle non file archives");
        }

        /// <summary>Initializes a new instance of the <see cref="DiskArchiveStorage" /> class.</summary>
        /// <param name="file">The file.</param>
        public DiskArchiveStorage(ZipFile file) : this(file, FileUpdateMode.Safe) { }

        /// <inheritdoc/>
        public override Stream ConvertTemporaryToFinal()
        {
            if (temporaryStream_ == null)
            {
                throw new ZipException("No temporary stream has been created");
            }

            var tempFileName = GetTempFileName(fileName_, false);
            var flag = false;
            try
            {
                temporaryStream_.Close();
                File.Move(fileName_, tempFileName);
                File.Move(temporaryName_, fileName_);
                flag = true;
                File.Delete(tempFileName);
                return File.Open(fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                if (!flag)
                {
                    File.Move(tempFileName, fileName_);
                    File.Delete(temporaryName_);
                }
                throw;
            }
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
            if (temporaryName_ != null)
            {
                temporaryName_ = GetTempFileName(temporaryName_, true);
                temporaryStream_ = File.Open(temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            }
            else
            {
                temporaryName_ = Path.GetTempFileName();
                temporaryStream_ = File.Open(temporaryName_, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            }
            return temporaryStream_;
        }

        /// <inheritdoc/>
        public override Stream MakeTemporaryCopy(Stream stream)
        {
            stream.Close();
            temporaryName_ = GetTempFileName(fileName_, true);
            File.Copy(fileName_, temporaryName_, true);
            temporaryStream_ = new FileStream(temporaryName_, FileMode.Open, FileAccess.ReadWrite);
            return temporaryStream_;
        }

        /// <inheritdoc/>
        public override Stream OpenForDirectUpdate(Stream stream)
        {
            Stream stream1;
            if (stream == null || !stream.CanWrite)
            {
                stream?.Close();
                stream1 = new FileStream(fileName_, FileMode.Open, FileAccess.ReadWrite);
            }
            else
            {
                stream1 = stream;
            }
            return stream1;
        }

        /// <summary>Gets temporary file name.</summary>
        /// <param name="original">    The original.</param>
        /// <param name="makeTempFile">True to make temporary file.</param>
        /// <returns>The temporary file name.</returns>
        private static string GetTempFileName(string original, bool makeTempFile)
        {
            string str = null;
            if (original == null)
            {
                str = Path.GetTempFileName();
            }
            else
            {
                var num = 0;
                var second = DateTime.Now.Second;
                while (str == null)
                {
                    ++num;
                    var path = string.Format("{0}.{1}{2}.tmp", original, second, num);
                    if (!File.Exists(path))
                    {
                        if (makeTempFile)
                        {
                            try
                            {
                                using (File.Create(path))
                                {
                                    ;
                                }
                                str = path;
                            }
                            catch
                            {
                                second = DateTime.Now.Second;
                            }
                        }
                        else
                        {
                            str = path;
                        }
                    }
                }
            }
            return str;
        }
    }
}
