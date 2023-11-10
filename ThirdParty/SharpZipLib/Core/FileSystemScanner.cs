// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.FileSystemScanner
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>A file system scanner.</summary>
    public class FileSystemScanner
    {
        /// <summary>The completed file.</summary>
        public CompletedFileHandler CompletedFile;

        /// <summary>The directory failure.</summary>
        public DirectoryFailureHandler DirectoryFailure;

        /// <summary>The file failure.</summary>
        public FileFailureHandler FileFailure;

        /// <summary>Pathname of the process directory.</summary>
        public ProcessDirectoryHandler ProcessDirectory;

        /// <summary>The process file.</summary>
        public ProcessFileHandler ProcessFile;

        /// <summary>A filter specifying the directory.</summary>
        private readonly IScanFilter directoryFilter_;

        /// <summary>A filter specifying the file.</summary>
        private readonly IScanFilter fileFilter_;

        /// <summary>True to alive.</summary>
        private bool alive_;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.FileSystemScanner" />
        ///     class.
        /// </summary>
        /// <param name="filter">Specifies the filter.</param>
        public FileSystemScanner(string filter)
        {
            this.fileFilter_ = new PathFilter(filter);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.FileSystemScanner" />
        ///     class.
        /// </summary>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        /// <param name="directoryFilter">A filter specifying the directory.</param>
        public FileSystemScanner(string fileFilter, string directoryFilter)
        {
            this.fileFilter_ = new PathFilter(fileFilter);
            this.directoryFilter_ = new PathFilter(directoryFilter);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.FileSystemScanner" />
        ///     class.
        /// </summary>
        /// <param name="fileFilter">A filter specifying the file.</param>
        public FileSystemScanner(IScanFilter fileFilter)
        {
            this.fileFilter_ = fileFilter;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.FileSystemScanner" />
        ///     class.
        /// </summary>
        /// <param name="fileFilter">     A filter specifying the file.</param>
        /// <param name="directoryFilter">A filter specifying the directory.</param>
        public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
        {
            this.fileFilter_ = fileFilter;
            this.directoryFilter_ = directoryFilter;
        }

        /// <summary>Scans.</summary>
        /// <param name="directory">Pathname of the directory.</param>
        /// <param name="recurse">  True to process recursively, false to process locally only.</param>
        public void Scan(string directory, bool recurse)
        {
            this.alive_ = true;
            this.ScanDir(directory, recurse);
        }

        /// <summary>Executes the complete file action.</summary>
        /// <param name="file">The file.</param>
        private void OnCompleteFile(string file)
        {
            var completedFile = this.CompletedFile;
            if (completedFile == null)
            {
                return;
            }

            var e = new ScanEventArgs(file);
            completedFile(this, e);
            this.alive_ = e.ContinueRunning;
        }

        /// <summary>Executes the directory failure action.</summary>
        /// <param name="directory">Pathname of the directory.</param>
        /// <param name="e">        The Exception to process.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool OnDirectoryFailure(string directory, Exception e)
        {
            var directoryFailure = this.DirectoryFailure;
            var flag = directoryFailure != null;
            if (flag)
            {
                var e1 = new ScanFailureEventArgs(directory, e);
                directoryFailure(this, e1);
                this.alive_ = e1.ContinueRunning;
            }

            return flag;
        }

        /// <summary>Executes the file failure action.</summary>
        /// <param name="file">The file.</param>
        /// <param name="e">   The Exception to process.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool OnFileFailure(string file, Exception e)
        {
            var flag = this.FileFailure != null;
            if (flag)
            {
                var e1 = new ScanFailureEventArgs(file, e);
                this.FileFailure(this, e1);
                this.alive_ = e1.ContinueRunning;
            }

            return flag;
        }

        /// <summary>Executes the process directory action.</summary>
        /// <param name="directory">       Pathname of the directory.</param>
        /// <param name="hasMatchingFiles">True if this FileSystemScanner has matching files.</param>
        private void OnProcessDirectory(string directory, bool hasMatchingFiles)
        {
            var processDirectory = this.ProcessDirectory;
            if (processDirectory == null)
            {
                return;
            }

            var e = new DirectoryEventArgs(directory, hasMatchingFiles);
            processDirectory(this, e);
            this.alive_ = e.ContinueRunning;
        }

        /// <summary>Executes the process file action.</summary>
        /// <param name="file">The file.</param>
        private void OnProcessFile(string file)
        {
            var processFile = this.ProcessFile;
            if (processFile == null)
            {
                return;
            }

            var e = new ScanEventArgs(file);
            processFile(this, e);
            this.alive_ = e.ContinueRunning;
        }

        /// <summary>Scans a dir.</summary>
        /// <param name="directory">Pathname of the directory.</param>
        /// <param name="recurse">  True to process recursively, false to process locally only.</param>
        private void ScanDir(string directory, bool recurse)
        {
            try
            {
                var files = Directory.GetFiles(directory);
                var hasMatchingFiles = false;
                for (var index = 0; index < files.Length; ++index)
                {
                    if (!this.fileFilter_.IsMatch(files[index]))
                    {
                        files[index] = null;
                    }
                    else
                    {
                        hasMatchingFiles = true;
                    }
                }

                this.OnProcessDirectory(directory, hasMatchingFiles);
                if (this.alive_)
                {
                    if (hasMatchingFiles)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                if (file != null)
                                {
                                    this.OnProcessFile(file);
                                    if (!this.alive_)
                                    {
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (!this.OnFileFailure(file, ex))
                                {
                                    throw;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.OnDirectoryFailure(directory, ex))
                {
                    throw;
                }
            }

            if (!this.alive_)
            {
                return;
            }

            if (!recurse)
            {
                return;
            }

            try
            {
                foreach (var directory1 in Directory.GetDirectories(directory))
                {
                    if (this.directoryFilter_ == null || this.directoryFilter_.IsMatch(directory1))
                    {
                        this.ScanDir(directory1, true);
                        if (!this.alive_)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.OnDirectoryFailure(directory, ex))
                {
                    return;
                }

                throw;
            }
        }
    }
}