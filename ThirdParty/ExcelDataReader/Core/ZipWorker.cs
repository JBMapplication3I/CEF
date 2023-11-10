// Decompiled with JetBrains decompiler
// Type: Excel.Core.ZipWorker
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core
{
    using System;
    using System.IO;
    using ICSharpCode.SharpZipLib.Zip;
    using Log;

    /// <summary>A zip worker.</summary>
    /// <seealso cref="IDisposable"/>
    public class ZipWorker : IDisposable
    {
        /// <summary>The file rels.</summary>
        private const string FILE_rels = "workbook.{0}.rels";

        /// <summary>The file shared strings.</summary>
        private const string FILE_sharedStrings = "sharedStrings.{0}";

        /// <summary>The file sheet.</summary>
        private const string FILE_sheet = "sheet{0}.{1}";

        /// <summary>The file styles.</summary>
        private const string FILE_styles = "styles.{0}";

        /// <summary>The file workbook.</summary>
        private const string FILE_workbook = "workbook.{0}";

        /// <summary>The folder rels.</summary>
        private const string FOLDER_rels = "_rels";

        /// <summary>The folder worksheets.</summary>
        private const string FOLDER_worksheets = "worksheets";

        /// <summary>The folder xl.</summary>
        private const string FOLDER_xl = "xl";

        /// <summary>The temporary.</summary>
        private const string TMP = "TMP_Z";

        /// <summary>Describes the format to use.</summary>
        private readonly string _format = "xml";

        /// <summary>True if this ZipWorker is cleaned.</summary>
        private bool _isCleaned;

        /// <summary>The temporary environment.</summary>
        private readonly string _tempEnv;

        /// <summary>Full pathname of the xl file.</summary>
        private string _xlPath;

        /// <summary>The buffer.</summary>
        private byte[] buffer;

        /// <summary>True if disposed.</summary>
        private bool disposed;

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.ZipWorker"/> class.</summary>
        public ZipWorker()
        {
            _tempEnv = Path.GetTempPath();
        }

        /// <summary>Finalizes an instance of the Excel.Core.ZipWorker class.</summary>
        ~ZipWorker()
        {
            Dispose(false);
        }

        /// <summary>Gets a message describing the exception.</summary>
        /// <value>A message describing the exception.</value>
        public string ExceptionMessage { get; private set; }

        /// <summary>Gets a value indicating whether this ZipWorker is valid.</summary>
        /// <value>True if this ZipWorker is valid, false if not.</value>
        public bool IsValid { get; private set; }

        /// <summary>Gets the full pathname of the temporary file.</summary>
        /// <value>The full pathname of the temporary file.</value>
        public string TempPath { get; private set; }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Extracts the given fileStream.</summary>
        /// <param name="fileStream">The file stream.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Extract(Stream fileStream)
        {
            if (fileStream == null)
            {
                return false;
            }
            CleanFromTemp(false);
            NewTempPath();
            IsValid = true;
            ZipFile zipFile = null;
            try
            {
                try
                {
                    zipFile = new ZipFile(fileStream);
                    var enumerator = zipFile.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        ExtractZipEntry(zipFile, (ZipEntry)enumerator.Current);
                    }
                }
                catch (Exception exception)
                {
                    var ex = exception;
                    IsValid = false;
                    ExceptionMessage = ex.Message;
                    CleanFromTemp(true);
                }
            }
            finally
            {
                fileStream.Close();
                if (zipFile != null)
                {
                    zipFile.Close();
                }
            }
            if (!IsValid)
            {
                return false;
            }
            return CheckFolderTree();
        }

        /// <summary>Gets shared strings stream.</summary>
        /// <returns>The shared strings stream.</returns>
        public Stream GetSharedStringsStream()
        {
            return GetStream(Path.Combine(_xlPath, string.Format("sharedStrings.{0}", _format)));
        }

        /// <summary>Gets styles stream.</summary>
        /// <returns>The styles stream.</returns>
        public Stream GetStylesStream()
        {
            return GetStream(Path.Combine(_xlPath, string.Format("styles.{0}", _format)));
        }

        /// <summary>Gets workbook rels stream.</summary>
        /// <returns>The workbook rels stream.</returns>
        public Stream GetWorkbookRelsStream()
        {
            return GetStream(Path.Combine(_xlPath, Path.Combine("_rels", string.Format("workbook.{0}.rels", _format))));
        }

        /// <summary>Gets workbook stream.</summary>
        /// <returns>The workbook stream.</returns>
        public Stream GetWorkbookStream()
        {
            return GetStream(Path.Combine(_xlPath, string.Format("workbook.{0}", _format)));
        }

        /// <summary>Gets worksheet stream.</summary>
        /// <param name="sheetId">Identifier for the sheet.</param>
        /// <returns>The worksheet stream.</returns>
        public Stream GetWorksheetStream(int sheetId)
        {
            return GetStream(
                Path.Combine(Path.Combine(_xlPath, "worksheets"), string.Format("sheet{0}.{1}", sheetId, _format)));
        }

        /// <summary>Gets worksheet stream.</summary>
        /// <param name="sheetPath">Full pathname of the sheet file.</param>
        /// <returns>The worksheet stream.</returns>
        public Stream GetWorksheetStream(string sheetPath)
        {
            if (sheetPath.StartsWith("/xl/"))
            {
                sheetPath = sheetPath[4..];
            }
            return GetStream(Path.Combine(_xlPath, sheetPath));
        }

        /// <summary>Gets a stream.</summary>
        /// <param name="filePath">Full pathname of the file.</param>
        /// <returns>The stream.</returns>
        private static Stream GetStream(string filePath)
        {
            return File.Exists(filePath) ? File.Open(filePath, FileMode.Open, FileAccess.Read) : (Stream)null;
        }

        /// <summary>Determines if we can check folder tree.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool CheckFolderTree()
        {
            _xlPath = Path.Combine(TempPath, "xl");
            return Directory.Exists(_xlPath)
                && Directory.Exists(Path.Combine(_xlPath, "worksheets"))
                && File.Exists(Path.Combine(_xlPath, "workbook.{0}"))
                && File.Exists(Path.Combine(_xlPath, "styles.{0}"));
        }

        /// <summary>Clean from temporary.</summary>
        /// <param name="catchIoError">True to catch i/o error.</param>
        private void CleanFromTemp(bool catchIoError)
        {
            if (string.IsNullOrEmpty(TempPath))
            {
                return;
            }
            _isCleaned = true;
            try
            {
                if (!Directory.Exists(TempPath))
                {
                    return;
                }
                Directory.Delete(TempPath, true);
            }
            catch (IOException)
            {
                if (catchIoError)
                {
                    return;
                }
                throw;
            }
        }

        /// <summary>Releases the unmanaged resources used by the Excel.Core.ZipWorker and optionally releases the
        /// managed resources.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing && !_isCleaned)
            {
                CleanFromTemp(false);
            }
            buffer = null;
            disposed = true;
        }

        /// <summary>Extracts the zip entry.</summary>
        /// <param name="zipFile">The zip file.</param>
        /// <param name="entry">  The entry.</param>
        private void ExtractZipEntry(ZipFile zipFile, ZipEntry entry)
        {
            if (!entry.IsCompressionMethodSupported() || string.IsNullOrEmpty(entry.Name))
            {
                return;
            }
            var path1 = Path.Combine(TempPath, entry.Name);
            var path2 = entry.IsDirectory ? path1 : Path.GetDirectoryName(Path.GetFullPath(path1));
            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }
            if (!entry.IsFile)
            {
                return;
            }
            using var fileStream = File.Create(path1);
            if (buffer == null)
            {
                buffer = new byte[4096];
            }
            using (var inputStream = zipFile.GetInputStream(entry))
            {
                int count;
                while ((count = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, 0, count);
                }
            }
            fileStream.Flush();
        }

        /// <summary>Creates a new temporary path.</summary>
        private void NewTempPath()
        {
            var str = Guid.NewGuid().ToString("N");
            TempPath = Path.Combine(_tempEnv, "TMP_Z" + DateTime.Now.ToFileTimeUtc() + str);
            _isCleaned = false;
            LogManager.Log(this).Debug("Using temp path {0}", (object)TempPath);
            Directory.CreateDirectory(TempPath);
        }
    }
}
