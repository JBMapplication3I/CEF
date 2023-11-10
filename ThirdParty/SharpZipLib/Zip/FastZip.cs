// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.FastZip
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;
    using Core;

    /// <summary>A fast zip.</summary>
    public class FastZip : IDisposable
    {
        /// <summary>The buffer.</summary>
        private byte[] buffer_;

        /// <summary>The confirm delegate.</summary>
        private ConfirmOverwriteDelegate confirmDelegate_;

        /// <summary>True to continue running.</summary>
        private bool continueRunning_;

        /// <summary>A filter specifying the directory.</summary>
        private NameFilter directoryFilter_;

        /// <summary>The entry factory.</summary>
        private IEntryFactory entryFactory_ = new ZipEntryFactory();

        /// <summary>The events.</summary>
        private readonly FastZipEvents events_;

        /// <summary>The extract name transform.</summary>
        private INameTransform extractNameTransform_;

        /// <summary>A filter specifying the file.</summary>
        private NameFilter fileFilter_;

        /// <summary>Stream to write data to.</summary>
        private ZipOutputStream outputStream_;

        /// <summary>The overwrite.</summary>
        private Overwrite overwrite_;

        /// <summary>Pathname of the source directory.</summary>
        private string sourceDirectory_;

        /// <summary>The zip file.</summary>
        private ZipFile zipFile_;

        private bool disposed;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.FastZip" /> class.</summary>
        public FastZip() { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.FastZip" /> class.</summary>
        /// <param name="events">The events.</param>
        public FastZip(FastZipEvents events)
        {
            events_ = events;
        }

        /// <summary>Confirm overwrite delegate.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <returns>A bool.</returns>
        public delegate bool ConfirmOverwriteDelegate(string fileName);

        /// <summary>Values that represent overwrites.</summary>
        public enum Overwrite
        {
            /// <summary>An enum constant representing the prompt option.</summary>
            Prompt,

            /// <summary>An enum constant representing the never option.</summary>
            Never,

            /// <summary>An enum constant representing the always option.</summary>
            Always,
        }

        /// <summary>Gets or sets a value indicating whether the create empty directories.</summary>
        /// <value>True if create empty directories, false if not.</value>
        public bool CreateEmptyDirectories { get; set; }

        /// <summary>Gets or sets the entry factory.</summary>
        /// <value>The entry factory.</value>
        public IEntryFactory EntryFactory
        {
            get => entryFactory_;
            set
            {
                if (value == null)
                {
                    entryFactory_ = new ZipEntryFactory();
                }
                else
                {
                    entryFactory_ = value;
                }
            }
        }

        /// <summary>Gets or sets the name transform.</summary>
        /// <value>The name transform.</value>
        public INameTransform NameTransform
        {
            get => entryFactory_.NameTransform;
            set => entryFactory_.NameTransform = value;
        }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>Gets or sets a value indicating whether the restore attributes on extract.</summary>
        /// <value>True if restore attributes on extract, false if not.</value>
        public bool RestoreAttributesOnExtract { get; set; }

        /// <summary>Gets or sets a value indicating whether the restore date time on extract.</summary>
        /// <value>True if restore date time on extract, false if not.</value>
        public bool RestoreDateTimeOnExtract { get; set; }

        /// <summary>Gets or sets the use zip 64.</summary>
        /// <value>The use zip 64.</value>
        public UseZip64 UseZip64 { get; set; } = UseZip64.Dynamic;

        /// <summary>Creates a zip.</summary>
        /// <param name="zipFileName">    Filename of the zip file.</param>
        /// <param name="sourceDirectory">Pathname of the source directory.</param>
        /// <param name="recurse">        True to process recursively, false to process locally only.</param>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        /// <param name="directoryFilter">A filter specifying the directory.</param>
        public void CreateZip(
            string zipFileName,
            string sourceDirectory,
            bool recurse,
            string fileFilter,
            string directoryFilter)
        {
            CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
        }

        /// <summary>Creates a zip.</summary>
        /// <param name="zipFileName">    Filename of the zip file.</param>
        /// <param name="sourceDirectory">Pathname of the source directory.</param>
        /// <param name="recurse">        True to process recursively, false to process locally only.</param>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
        {
            CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
        }

        /// <summary>Creates a zip.</summary>
        /// <param name="outputStream">   Stream to write data to.</param>
        /// <param name="sourceDirectory">Pathname of the source directory.</param>
        /// <param name="recurse">        True to process recursively, false to process locally only.</param>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        /// <param name="directoryFilter">A filter specifying the directory.</param>
        public void CreateZip(
            Stream outputStream,
            string sourceDirectory,
            bool recurse,
            string fileFilter,
            string directoryFilter)
        {
            NameTransform = new ZipNameTransform(sourceDirectory);
            sourceDirectory_ = sourceDirectory;
            using (outputStream_ = new ZipOutputStream(outputStream))
            {
                if (Password != null)
                {
                    outputStream_.Password = Password;
                }
                outputStream_.UseZip64 = UseZip64;
                var fileSystemScanner = new FileSystemScanner(fileFilter, directoryFilter);
                fileSystemScanner.ProcessFile += ProcessFile;
                if (CreateEmptyDirectories)
                {
                    fileSystemScanner.ProcessDirectory += ProcessDirectory;
                }
                if (events_ != null)
                {
                    if (events_.FileFailure != null)
                    {
                        fileSystemScanner.FileFailure += events_.FileFailure;
                    }
                    if (events_.DirectoryFailure != null)
                    {
                        fileSystemScanner.DirectoryFailure += events_.DirectoryFailure;
                    }
                }
                fileSystemScanner.Scan(sourceDirectory, recurse);
            }
        }

        /// <summary>Extracts the zip.</summary>
        /// <param name="zipFileName">    Filename of the zip file.</param>
        /// <param name="targetDirectory">Pathname of the target directory.</param>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
        {
            ExtractZip(
                zipFileName,
                targetDirectory,
                Overwrite.Always,
                null,
                fileFilter,
                null,
                RestoreDateTimeOnExtract);
        }

        /// <summary>Extracts the zip.</summary>
        /// <param name="zipFileName">    Filename of the zip file.</param>
        /// <param name="targetDirectory">Pathname of the target directory.</param>
        /// <param name="overwrite">      The overwrite.</param>
        /// <param name="confirmDelegate">The confirm delegate.</param>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        /// <param name="directoryFilter">A filter specifying the directory.</param>
        /// <param name="restoreDateTime">True to restore date time.</param>
        public void ExtractZip(
            string zipFileName,
            string targetDirectory,
            Overwrite overwrite,
            ConfirmOverwriteDelegate confirmDelegate,
            string fileFilter,
            string directoryFilter,
            bool restoreDateTime)
        {
            ExtractZip(
                File.Open(zipFileName, FileMode.Open, FileAccess.Read, FileShare.Read),
                targetDirectory,
                overwrite,
                confirmDelegate,
                fileFilter,
                directoryFilter,
                restoreDateTime,
                true);
        }

        /// <summary>Extracts the zip.</summary>
        /// <param name="inputStream">    Stream to read data from.</param>
        /// <param name="targetDirectory">Pathname of the target directory.</param>
        /// <param name="overwrite">      The overwrite.</param>
        /// <param name="confirmDelegate">The confirm delegate.</param>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        /// <param name="directoryFilter">A filter specifying the directory.</param>
        /// <param name="restoreDateTime">True to restore date time.</param>
        /// <param name="isStreamOwner">  True if this FastZip is stream owner.</param>
        public void ExtractZip(
            Stream inputStream,
            string targetDirectory,
            Overwrite overwrite,
            ConfirmOverwriteDelegate confirmDelegate,
            string fileFilter,
            string directoryFilter,
            bool restoreDateTime,
            bool isStreamOwner)
        {
            if (overwrite == Overwrite.Prompt && confirmDelegate == null)
            {
                throw new ArgumentNullException(nameof(confirmDelegate));
            }
            continueRunning_ = true;
            overwrite_ = overwrite;
            confirmDelegate_ = confirmDelegate;
            extractNameTransform_ = new WindowsNameTransform(targetDirectory);
            fileFilter_ = new NameFilter(fileFilter);
            directoryFilter_ = new NameFilter(directoryFilter);
            RestoreDateTimeOnExtract = restoreDateTime;
            using (zipFile_ = new ZipFile(inputStream))
            {
                if (Password != null)
                {
                    zipFile_.Password = Password;
                }
                zipFile_.IsStreamOwner = isStreamOwner;
                var enumerator = zipFile_.GetEnumerator();
                while (continueRunning_ && enumerator.MoveNext())
                {
                    var current = (ZipEntry)enumerator.Current;
                    if (current.IsFile)
                    {
                        if (directoryFilter_.IsMatch(Path.GetDirectoryName(current.Name))
                            && fileFilter_.IsMatch(current.Name))
                        {
                            ExtractEntry(current);
                        }
                    }
                    else if (current.IsDirectory && directoryFilter_.IsMatch(current.Name) && CreateEmptyDirectories)
                    {
                        ExtractEntry(current);
                    }
                }
            }
        }

        /// <summary>Makes external attributes.</summary>
        /// <param name="info">The information.</param>
        /// <returns>An int.</returns>
        private static int MakeExternalAttributes(FileInfo info)
        {
            return (int)info.Attributes;
        }

        /// <summary>Name is valid.</summary>
        /// <param name="name">The name.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static bool NameIsValid(string name)
        {
            return name != null && name.Length > 0 && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
        }

        /// <summary>Adds a file contents to 'stream'.</summary>
        /// <param name="name">  The name.</param>
        /// <param name="stream">The stream.</param>
        private void AddFileContents(string name, Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (buffer_ == null)
            {
                buffer_ = new byte[4096];
            }
            if (events_ != null && events_.Progress != null)
            {
                StreamUtils.Copy(
                    stream,
                    outputStream_,
                    buffer_,
                    events_.Progress,
                    events_.ProgressInterval,
                    this,
                    name);
            }
            else
            {
                StreamUtils.Copy(stream, outputStream_, buffer_);
            }
            if (events_ == null)
            {
                return;
            }
            continueRunning_ = events_.OnCompletedFile(name);
        }

        /// <summary>Extracts the entry described by entry.</summary>
        /// <param name="entry">The entry.</param>
        private void ExtractEntry(ZipEntry entry)
        {
            var flag = entry.IsCompressionMethodSupported();
            var str = entry.Name;
            if (flag)
            {
                if (entry.IsFile)
                {
                    str = extractNameTransform_.TransformFile(str);
                }
                else if (entry.IsDirectory)
                {
                    str = extractNameTransform_.TransformDirectory(str);
                }
                flag = str != null && str.Length != 0;
            }
            string path = null;
            if (flag)
            {
                path = !entry.IsDirectory ? Path.GetDirectoryName(Path.GetFullPath(str)) : str;
            }
            if (flag && !Directory.Exists(path))
            {
                if (entry.IsDirectory)
                {
                    if (!CreateEmptyDirectories)
                    {
                        goto label_16;
                    }
                }
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    flag = false;
                    if (events_ != null)
                    {
                        continueRunning_ = !entry.IsDirectory
                            ? events_.OnFileFailure(str, ex)
                            : events_.OnDirectoryFailure(str, ex);
                    }
                    else
                    {
                        continueRunning_ = false;
                        throw;
                    }
                }
            }
        label_16:
            if (!flag || !entry.IsFile)
            {
                return;
            }
            ExtractFileEntry(entry, str);
        }

        /// <summary>Extracts the file entry.</summary>
        /// <param name="entry">     The entry.</param>
        /// <param name="targetName">Name of the target.</param>
        private void ExtractFileEntry(ZipEntry entry, string targetName)
        {
            var flag = true;
            if (overwrite_ != Overwrite.Always && File.Exists(targetName))
            {
                flag = overwrite_ == Overwrite.Prompt && confirmDelegate_ != null && confirmDelegate_(targetName);
            }
            if (!flag)
            {
                return;
            }
            if (events_ != null)
            {
                continueRunning_ = events_.OnProcessFile(entry.Name);
            }
            if (!continueRunning_)
            {
                return;
            }
            try
            {
                using (var fileStream = File.Create(targetName))
                {
                    if (buffer_ == null)
                    {
                        buffer_ = new byte[4096];
                    }
                    if (events_ != null && events_.Progress != null)
                    {
                        StreamUtils.Copy(
                            zipFile_.GetInputStream(entry),
                            fileStream,
                            buffer_,
                            events_.Progress,
                            events_.ProgressInterval,
                            this,
                            entry.Name,
                            entry.Size);
                    }
                    else
                    {
                        StreamUtils.Copy(zipFile_.GetInputStream(entry), fileStream, buffer_);
                    }
                    if (events_ != null)
                    {
                        continueRunning_ = events_.OnCompletedFile(entry.Name);
                    }
                }
                if (RestoreDateTimeOnExtract)
                {
                    File.SetLastWriteTime(targetName, entry.DateTime);
                }
                if (!RestoreAttributesOnExtract || !entry.IsDOSEntry || entry.ExternalFileAttributes == -1)
                {
                    return;
                }
                var fileAttributes = (FileAttributes)(entry.ExternalFileAttributes & 163);
                File.SetAttributes(targetName, fileAttributes);
            }
            catch (Exception ex)
            {
                if (events_ != null)
                {
                    continueRunning_ = events_.OnFileFailure(targetName, ex);
                }
                else
                {
                    continueRunning_ = false;
                    throw;
                }
            }
        }

        /// <summary>Process the directory.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Directory event information.</param>
        private void ProcessDirectory(object sender, DirectoryEventArgs e)
        {
            if (e.HasMatchingFiles || !CreateEmptyDirectories)
            {
                return;
            }
            events_?.OnProcessDirectory(e.Name, e.HasMatchingFiles);
            if (!e.ContinueRunning || !(e.Name != sourceDirectory_))
            {
                return;
            }
            outputStream_.PutNextEntry(entryFactory_.MakeDirectoryEntry(e.Name));
        }

        /// <summary>Process the file.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Scan event information.</param>
        private void ProcessFile(object sender, ScanEventArgs e)
        {
            if (events_ != null)
            {
                events_.ProcessFile?.Invoke(sender, e);
            }
            if (!e.ContinueRunning)
            {
                return;
            }
            try
            {
                using var fileStream = File.Open(e.Name, FileMode.Open, FileAccess.Read, FileShare.Read);
                outputStream_.PutNextEntry(entryFactory_.MakeFileEntry(e.Name));
                AddFileContents(e.Name, fileStream);
            }
            catch (Exception ex)
            {
                if (events_ != null)
                {
                    continueRunning_ = events_.OnFileFailure(e.Name, ex);
                }
                else
                {
                    continueRunning_ = false;
                    throw;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                outputStream_?.Dispose();
                zipFile_?.Dispose();
            }
            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            outputStream_ = null;
            zipFile_ = null;
            disposed = true;
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~FastZip()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
