// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarArchive
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>A tar archive.</summary>
    /// <seealso cref="IDisposable" />
    public class TarArchive : IDisposable
    {
        /// <summary>The tar in.</summary>
        private readonly TarInputStream tarIn;

        /// <summary>The tar out.</summary>
        private readonly TarOutputStream tarOut;

        /// <summary>True to apply user information overrides.</summary>
        private bool applyUserInfoOverrides;

        /// <summary>True to ASCII translate.</summary>
        private bool asciiTranslate;

        /// <summary>Identifier for the group.</summary>
        private int groupId;

        /// <summary>Name of the group.</summary>
        private string groupName = string.Empty;

        /// <summary>True if this TarArchive is disposed.</summary>
        private bool isDisposed;

        /// <summary>True to keep old files.</summary>
        private bool keepOldFiles;

        /// <summary>The path prefix.</summary>
        private string pathPrefix;

        /// <summary>Full pathname of the root file.</summary>
        private string rootPath;

        /// <summary>Identifier for the user.</summary>
        private int userId;

        /// <summary>Name of the user.</summary>
        private string userName = string.Empty;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarArchive" /> class.</summary>
        protected TarArchive() { }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarArchive" /> class.</summary>
        /// <param name="stream">The stream.</param>
        protected TarArchive(TarInputStream stream)
        {
            this.tarIn = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarArchive" /> class.</summary>
        /// <param name="stream">The stream.</param>
        protected TarArchive(TarOutputStream stream)
        {
            this.tarOut = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        /// <summary>Finalizes an instance of the ICSharpCode.SharpZipLib.Tar.TarArchive class.</summary>
        ~TarArchive()
        {
            this.Dispose(false);
        }

        /// <summary>Occurs when progress Message.</summary>
        public event ProgressMessageHandler ProgressMessageEvent;

        /// <summary>Gets or sets a value indicating whether the apply user information overrides.</summary>
        /// <value>True if apply user information overrides, false if not.</value>
        public bool ApplyUserInfoOverrides
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.applyUserInfoOverrides;
            }
            set
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                this.applyUserInfoOverrides = value;
            }
        }

        /// <summary>Gets or sets a value indicating whether the ASCII translate.</summary>
        /// <value>True if ASCII translate, false if not.</value>
        public bool AsciiTranslate
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.asciiTranslate;
            }
            set
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                this.asciiTranslate = value;
            }
        }

        /// <summary>Gets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        public int GroupId
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.groupId;
            }
        }

        /// <summary>Gets the name of the group.</summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.groupName;
            }
        }

        /// <summary>Sets a value indicating whether this TarArchive is stream owner.</summary>
        /// <value>True if this TarArchive is stream owner, false if not.</value>
        public bool IsStreamOwner
        {
            set
            {
                if (this.tarIn != null)
                {
                    this.tarIn.IsStreamOwner = value;
                }
                else
                {
                    this.tarOut.IsStreamOwner = value;
                }
            }
        }

        /// <summary>Gets or sets the path prefix.</summary>
        /// <value>The path prefix.</value>
        public string PathPrefix
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.pathPrefix;
            }
            set
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                this.pathPrefix = value;
            }
        }

        /// <summary>Gets the size of the record.</summary>
        /// <value>The size of the record.</value>
        public int RecordSize
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                if (this.tarIn != null)
                {
                    return this.tarIn.RecordSize;
                }

                return this.tarOut != null ? this.tarOut.RecordSize : 10240;
            }
        }

        /// <summary>Gets or sets the full pathname of the root file.</summary>
        /// <value>The full pathname of the root file.</value>
        public string RootPath
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.rootPath;
            }
            set
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                this.rootPath = value;
            }
        }

        /// <summary>Gets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int UserId
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.userId;
            }
        }

        /// <summary>Gets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get
            {
                if (this.isDisposed)
                {
                    throw new ObjectDisposedException(nameof(TarArchive));
                }

                return this.userName;
            }
        }

        /// <summary>Creates input tar archive.</summary>
        /// <param name="inputStream">Stream to read data from.</param>
        /// <returns>The new input tar archive.</returns>
        public static TarArchive CreateInputTarArchive(Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            return inputStream is not TarInputStream stream
                       ? CreateInputTarArchive(inputStream, 20)
                       : new TarArchive(stream);
        }

        /// <summary>Creates input tar archive.</summary>
        /// <param name="inputStream">Stream to read data from.</param>
        /// <param name="blockFactor">The block factor.</param>
        /// <returns>The new input tar archive.</returns>
        public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }

            return !(inputStream is TarInputStream)
                       ? new TarArchive(new TarInputStream(inputStream, blockFactor))
                       : throw new ArgumentException("TarInputStream not valid");
        }

        /// <summary>Creates output tar archive.</summary>
        /// <param name="outputStream">Stream to write data to.</param>
        /// <returns>The new output tar archive.</returns>
        public static TarArchive CreateOutputTarArchive(Stream outputStream)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }

            return outputStream is not TarOutputStream stream
                       ? CreateOutputTarArchive(outputStream, 20)
                       : new TarArchive(stream);
        }

        /// <summary>Creates output tar archive.</summary>
        /// <param name="outputStream">Stream to write data to.</param>
        /// <param name="blockFactor"> The block factor.</param>
        /// <returns>The new output tar archive.</returns>
        public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException(nameof(outputStream));
            }

            return !(outputStream is TarOutputStream)
                       ? new TarArchive(new TarOutputStream(outputStream, blockFactor))
                       : throw new ArgumentException("TarOutputStream is not valid");
        }

        /// <summary>Closes this TarArchive.</summary>
        public virtual void Close()
        {
            this.Dispose(true);
        }

        /// <summary>(This method is obsolete) closes the archive.</summary>
        [Obsolete("Use Close instead")]
        public void CloseArchive()
        {
            this.Close();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Extracts the contents described by destinationDirectory.</summary>
        /// <param name="destinationDirectory">Pathname of the destination directory.</param>
        public void ExtractContents(string destinationDirectory)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(TarArchive));
            }

            while (true)
            {
                var nextEntry = this.tarIn.GetNextEntry();
                if (nextEntry != null)
                {
                    this.ExtractEntry(destinationDirectory, nextEntry);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>List contents.</summary>
        public void ListContents()
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(TarArchive));
            }

            while (true)
            {
                var nextEntry = this.tarIn.GetNextEntry();
                if (nextEntry != null)
                {
                    this.OnProgressMessageEvent(nextEntry, null);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>(This method is obsolete) sets ASCII translation.</summary>
        /// <param name="translateAsciiFiles">True to translate ASCII files.</param>
        [Obsolete("Use the AsciiTranslate property")]
        public void SetAsciiTranslation(bool translateAsciiFiles)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(TarArchive));
            }

            this.asciiTranslate = translateAsciiFiles;
        }

        /// <summary>Sets keep old files.</summary>
        /// <param name="keepExistingFiles">True to keep existing files.</param>
        public void SetKeepOldFiles(bool keepExistingFiles)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(TarArchive));
            }

            this.keepOldFiles = keepExistingFiles;
        }

        /// <summary>Sets user information.</summary>
        /// <param name="userId">   Identifier for the user.</param>
        /// <param name="userName"> Name of the user.</param>
        /// <param name="groupId">  Identifier for the group.</param>
        /// <param name="groupName">Name of the group.</param>
        public void SetUserInfo(int userId, string userName, int groupId, string groupName)
        {
            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(TarArchive));
            }

            this.userId = userId;
            this.userName = userName;
            this.groupId = groupId;
            this.groupName = groupName;
            this.applyUserInfoOverrides = true;
        }

        /// <summary>Writes an entry.</summary>
        /// <param name="sourceEntry">Source entry.</param>
        /// <param name="recurse">    True to process recursively, false to process locally only.</param>
        public void WriteEntry(TarEntry sourceEntry, bool recurse)
        {
            if (sourceEntry == null)
            {
                throw new ArgumentNullException(nameof(sourceEntry));
            }

            if (this.isDisposed)
            {
                throw new ObjectDisposedException(nameof(TarArchive));
            }

            try
            {
                if (recurse)
                {
                    TarHeader.SetValueDefaults(
                        sourceEntry.UserId,
                        sourceEntry.UserName,
                        sourceEntry.GroupId,
                        sourceEntry.GroupName);
                }

                this.WriteEntryCore(sourceEntry, recurse);
            }
            finally
            {
                if (recurse)
                {
                    TarHeader.RestoreSetValues();
                }
            }
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the ICSharpCode.SharpZipLib.Tar.TarArchive and optionally
        ///     releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            this.isDisposed = true;
            if (!disposing)
            {
                return;
            }

            if (this.tarOut != null)
            {
                this.tarOut.Flush();
                this.tarOut.Close();
            }

            this.tarIn?.Close();
        }

        /// <summary>Executes the progress message event action.</summary>
        /// <param name="entry">  The entry.</param>
        /// <param name="message">The message.</param>
        protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
        {
            var progressMessageEvent = this.ProgressMessageEvent;
            progressMessageEvent?.Invoke(this, entry, message);
        }

        /// <summary>Ensures that directory exists.</summary>
        /// <param name="directoryName">Pathname of the directory.</param>
        private static void EnsureDirectoryExists(string directoryName)
        {
            if (Directory.Exists(directoryName))
            {
                return;
            }

            try
            {
                Directory.CreateDirectory(directoryName);
            }
            catch (Exception ex)
            {
                throw new TarException("Exception creating directory '" + directoryName + "', " + ex.Message, ex);
            }
        }

        /// <summary>Query if 'filename' is binary.</summary>
        /// <param name="filename">Filename of the file.</param>
        /// <returns>True if binary, false if not.</returns>
        private static bool IsBinary(string filename)
        {
            using (var fileStream = File.OpenRead(filename))
            {
                var count = Math.Min(4096, (int)fileStream.Length);
                var buffer = new byte[count];
                var num1 = fileStream.Read(buffer, 0, count);
                for (var index = 0; index < num1; ++index)
                {
                    var num2 = buffer[index];
                    if (num2 < 8 || num2 > 13 && num2 < 32 || num2 == byte.MaxValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>Extracts the entry.</summary>
        /// <param name="destDir">Destination dir.</param>
        /// <param name="entry">  The entry.</param>
        private void ExtractEntry(string destDir, TarEntry entry)
        {
            this.OnProgressMessageEvent(entry, null);
            var path = entry.Name;
            if (Path.IsPathRooted(path))
            {
                path = path[Path.GetPathRoot(path).Length..];
            }

            var path2 = path.Replace('/', Path.DirectorySeparatorChar);
            var str1 = Path.Combine(destDir, path2);
            if (entry.IsDirectory)
            {
                EnsureDirectoryExists(str1);
            }
            else
            {
                EnsureDirectoryExists(Path.GetDirectoryName(str1));
                var flag1 = true;
                var fileInfo = new FileInfo(str1);
                if (fileInfo.Exists)
                {
                    if (this.keepOldFiles)
                    {
                        this.OnProgressMessageEvent(entry, "Destination file already exists");
                        flag1 = false;
                    }
                    else if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
                    {
                        this.OnProgressMessageEvent(entry, "Destination file already exists, and is read-only");
                        flag1 = false;
                    }
                }

                if (!flag1)
                {
                    return;
                }

                var flag2 = false;
                Stream stream = File.Create(str1);
                if (this.asciiTranslate)
                {
                    flag2 = !IsBinary(str1);
                }

                StreamWriter streamWriter = null;
                if (flag2)
                {
                    streamWriter = new StreamWriter(stream);
                }

                var numArray = new byte[32768];
            label_15:
                int count;
                while (true)
                {
                    count = this.tarIn.Read(numArray, 0, numArray.Length);
                    if (count > 0)
                    {
                        if (!flag2)
                        {
                            stream.Write(numArray, 0, count);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        goto label_24;
                    }
                }

                var index1 = 0;
                for (var index2 = 0; index2 < count; ++index2)
                {
                    if (numArray[index2] == 10)
                    {
                        var str2 = Encoding.ASCII.GetString(numArray, index1, index2 - index1);
                        streamWriter.WriteLine(str2);
                        index1 = index2 + 1;
                    }
                }

                goto label_15;
            label_24:
                if (flag2)
                {
                    streamWriter.Close();
                }
                else
                {
                    stream.Close();
                }
            }
        }

        /// <summary>Writes an entry core.</summary>
        /// <param name="sourceEntry">Source entry.</param>
        /// <param name="recurse">    True to process recursively, false to process locally only.</param>
        private void WriteEntryCore(TarEntry sourceEntry, bool recurse)
        {
            string str1 = null;
            var str2 = sourceEntry.File;
            var entry = (TarEntry)sourceEntry.Clone();
            if (this.applyUserInfoOverrides)
            {
                entry.GroupId = this.groupId;
                entry.GroupName = this.groupName;
                entry.UserId = this.userId;
                entry.UserName = this.userName;
            }

            this.OnProgressMessageEvent(entry, null);
            if (this.asciiTranslate && !entry.IsDirectory && !IsBinary(str2))
            {
                str1 = Path.GetTempFileName();
                using (var streamReader = File.OpenText(str2))
                {
                    using Stream stream = File.Create(str1);
                    while (true)
                    {
                        var s = streamReader.ReadLine();
                        if (s != null)
                        {
                            var bytes = Encoding.ASCII.GetBytes(s);
                            stream.Write(bytes, 0, bytes.Length);
                            stream.WriteByte(10);
                        }
                        else
                        {
                            break;
                        }
                    }

                    stream.Flush();
                }

                entry.Size = new FileInfo(str1).Length;
                str2 = str1;
            }

            string str3 = null;
            if (this.rootPath != null && entry.Name.StartsWith(this.rootPath))
            {
                str3 = entry.Name[(this.rootPath.Length + 1)..];
            }

            if (this.pathPrefix != null)
            {
                str3 = str3 == null ? this.pathPrefix + "/" + entry.Name : this.pathPrefix + "/" + str3;
            }

            if (str3 != null)
            {
                entry.Name = str3;
            }

            this.tarOut.PutNextEntry(entry);
            if (entry.IsDirectory)
            {
                if (!recurse)
                {
                    return;
                }

                foreach (var directoryEntry in entry.GetDirectoryEntries())
                {
                    this.WriteEntryCore(directoryEntry, recurse);
                }
            }
            else
            {
                using (Stream stream = File.OpenRead(str2))
                {
                    var buffer = new byte[32768];
                    while (true)
                    {
                        var count = stream.Read(buffer, 0, buffer.Length);
                        if (count > 0)
                        {
                            this.tarOut.Write(buffer, 0, count);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (str1 != null && str1.Length > 0)
                {
                    File.Delete(str1);
                }

                this.tarOut.CloseEntry();
            }
        }
    }
}