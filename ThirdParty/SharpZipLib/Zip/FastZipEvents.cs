// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.FastZipEvents
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using Core;

    /// <summary>A fast zip events.</summary>
    public class FastZipEvents
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

        /// <summary>The progress.</summary>
        public ProgressHandler Progress;

        /// <summary>Gets or sets the progress interval.</summary>
        /// <value>The progress interval.</value>
        public TimeSpan ProgressInterval { get; set; } = TimeSpan.FromSeconds(3.0);

        /// <summary>Executes the completed file action.</summary>
        /// <param name="file">The file.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool OnCompletedFile(string file)
        {
            var flag = true;
            var completedFile = CompletedFile;
            if (completedFile != null)
            {
                var e = new ScanEventArgs(file);
                completedFile(this, e);
                flag = e.ContinueRunning;
            }
            return flag;
        }

        /// <summary>Executes the directory failure action.</summary>
        /// <param name="directory">Pathname of the directory.</param>
        /// <param name="e">        The Exception to process.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool OnDirectoryFailure(string directory, Exception e)
        {
            var flag = false;
            var directoryFailure = DirectoryFailure;
            if (directoryFailure != null)
            {
                var e1 = new ScanFailureEventArgs(directory, e);
                directoryFailure(this, e1);
                flag = e1.ContinueRunning;
            }
            return flag;
        }

        /// <summary>Executes the file failure action.</summary>
        /// <param name="file">The file.</param>
        /// <param name="e">   The Exception to process.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool OnFileFailure(string file, Exception e)
        {
            var fileFailure = FileFailure;
            var flag = fileFailure != null;
            if (flag)
            {
                var e1 = new ScanFailureEventArgs(file, e);
                fileFailure(this, e1);
                flag = e1.ContinueRunning;
            }
            return flag;
        }

        /// <summary>Executes the process directory action.</summary>
        /// <param name="directory">       Pathname of the directory.</param>
        /// <param name="hasMatchingFiles">True if this FastZipEvents has matching files.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
        {
            var flag = true;
            var processDirectory = ProcessDirectory;
            if (processDirectory != null)
            {
                var e = new DirectoryEventArgs(directory, hasMatchingFiles);
                processDirectory(this, e);
                flag = e.ContinueRunning;
            }
            return flag;
        }

        /// <summary>Executes the process file action.</summary>
        /// <param name="file">The file.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool OnProcessFile(string file)
        {
            var flag = true;
            var processFile = ProcessFile;
            if (processFile != null)
            {
                var e = new ScanEventArgs(file);
                processFile(this, e);
                flag = e.ContinueRunning;
            }
            return flag;
        }
    }
}
