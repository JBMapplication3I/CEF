// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipFile
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using Checksums;
    using Compression;
    using Compression.Streams;
    using Core;
    using Encryption;

    /// <summary>A zip file.</summary>
    /// <seealso cref="System.Collections.IEnumerable" />
    /// <seealso cref="IDisposable" />
    public class ZipFile : IEnumerable, IDisposable
    {
        /// <summary>The default buffer size.</summary>
        private const int DefaultBufferSize = 4096;

        /// <summary>The keys required.</summary>
        public KeysRequiredEventHandler KeysRequired;

        /// <summary>The archive storage.</summary>
        private IArchiveStorage archiveStorage_;

        /// <summary>The base stream.</summary>
        private Stream baseStream_;

        private readonly object padLock = new();

        /// <summary>Size of the buffer.</summary>
        private int bufferSize_ = 4096;

        /// <summary>True to comment edited.</summary>
        private bool commentEdited_;

        /// <summary>True to contents edited.</summary>
        private bool contentsEdited_;

        /// <summary>Buffer for copy data.</summary>
        private byte[] copyBuffer_;

        /// <summary>The entries.</summary>
        private ZipEntry[] entries_;

        /// <summary>True if this ZipFile is disposed.</summary>
        private bool disposed;

        /// <summary>The new comment.</summary>
        private ZipString newComment_;

        /// <summary>The offset of first entry.</summary>
        private long offsetOfFirstEntry;

        /// <summary>The raw password.</summary>
        private string rawPassword_;

        /// <summary>Number of updates.</summary>
        private long updateCount_;

        /// <summary>The update data source.</summary>
        private IDynamicDataSource updateDataSource_;

        /// <summary>The update entry factory.</summary>
        private IEntryFactory updateEntryFactory_ = new ZipEntryFactory();

        /// <summary>Zero-based index of the update.</summary>
        private Hashtable updateIndex_;

        /// <summary>The updates.</summary>
        private ArrayList updates_;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile" /> class.</summary>
        /// <param name="name">The name.</param>
        public ZipFile(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            baseStream_ = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            IsStreamOwner = true;
            try
            {
                ReadEntries();
            }
            catch
            {
#pragma warning disable CA2214 // Do not call overridable methods in constructors
                Dispose(true);
#pragma warning restore CA2214 // Do not call overridable methods in constructors
                throw;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile" /> class.</summary>
        /// <param name="file">The file.</param>
        public ZipFile(FileStream file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            baseStream_ = file.CanSeek
                ? (Stream)file
                : throw new ArgumentException("Stream is not seekable", nameof(file));
            Name = file.Name;
            IsStreamOwner = true;
            try
            {
                ReadEntries();
            }
            catch
            {
#pragma warning disable CA2214 // Do not call overridable methods in constructors
                Dispose(true);
#pragma warning restore CA2214 // Do not call overridable methods in constructors
                throw;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile" /> class.</summary>
        /// <param name="stream">The stream.</param>
        public ZipFile(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            baseStream_ = stream.CanSeek
                ? stream
                : throw new ArgumentException("Stream is not seekable", nameof(stream));
            IsStreamOwner = true;
            if (baseStream_.Length > 0L)
            {
                try
                {
                    ReadEntries();
                }
                catch
                {
#pragma warning disable CA2214 // Do not call overridable methods in constructors
                    Dispose(true);
#pragma warning restore CA2214 // Do not call overridable methods in constructors
                    throw;
                }
            }
            else
            {
                entries_ = Array.Empty<ZipEntry>();
                IsNewArchive = true;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile" /> class.</summary>
        internal ZipFile()
        {
            entries_ = Array.Empty<ZipEntry>();
            IsNewArchive = true;
        }

        /// <summary>Finalizes an instance of the ICSharpCode.SharpZipLib.Zip.ZipFile class.</summary>
        ~ZipFile()
        {
            Dispose(false);
        }

        /// <summary>Delegate for handling KeysRequired events.</summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">     Keys required event information.</param>
        public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

        /// <summary>Values that represent header tests.</summary>
        [Flags]
        private enum HeaderTest
        {
            /// <summary>An enum constant representing the extract option.</summary>
            Extract = 1,

            /// <summary>An enum constant representing the header option.</summary>
            Header = 2,
        }

        /// <summary>Values that represent update commands.</summary>
        private enum UpdateCommand
        {
            /// <summary>An enum constant representing the copy option.</summary>
            Copy,

            /// <summary>An enum constant representing the modify option.</summary>
            Modify,

            /// <summary>An enum constant representing the add option.</summary>
            Add,
        }

        /// <summary>Gets or sets the size of the buffer.</summary>
        /// <value>The size of the buffer.</value>
        public int BufferSize
        {
            get => bufferSize_;
            set
            {
                if (value < 1024)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "cannot be below 1024");
                }
                if (bufferSize_ == value)
                {
                    return;
                }
                bufferSize_ = value;
                copyBuffer_ = null;
            }
        }

        /// <summary>Gets the number of. </summary>
        /// <value>The count.</value>
        public long Count => entries_.Length;

        /// <summary>Gets or sets the entry factory.</summary>
        /// <value>The entry factory.</value>
        public IEntryFactory EntryFactory
        {
            get => updateEntryFactory_;
            set
            {
                if (value == null)
                {
                    updateEntryFactory_ = new ZipEntryFactory();
                }
                else
                {
                    updateEntryFactory_ = value;
                }
            }
        }

        /// <summary>Gets a value indicating whether this ZipFile is embedded archive.</summary>
        /// <value>True if this ZipFile is embedded archive, false if not.</value>
        public bool IsEmbeddedArchive => offsetOfFirstEntry > 0L;

        /// <summary>Gets a value indicating whether this ZipFile is new archive.</summary>
        /// <value>True if this ZipFile is new archive, false if not.</value>
        public bool IsNewArchive { get; private set; }

        /// <summary>Gets or sets a value indicating whether this ZipFile is stream owner.</summary>
        /// <value>True if this ZipFile is stream owner, false if not.</value>
        public bool IsStreamOwner { get; set; }

        /// <summary>Gets a value indicating whether this ZipFile is updating.</summary>
        /// <value>True if this ZipFile is updating, false if not.</value>
        public bool IsUpdating => updates_ != null;

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>Gets or sets the name transform.</summary>
        /// <value>The name transform.</value>
        public INameTransform NameTransform
        {
            get => updateEntryFactory_.NameTransform;
            set => updateEntryFactory_.NameTransform = value;
        }

        /// <summary>Sets the password.</summary>
        /// <value>The password.</value>
        public string Password
        {
            set
            {
                switch (value)
                {
                    case "":
                    case null:
                    {
                        Key = null;
                        break;
                    }
                    default:
                    {
                        rawPassword_ = value;
                        Key = PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(value));
                        break;
                    }
                }
            }
        }

        /// <summary>(This property is obsolete) gets the size.</summary>
        /// <value>The size.</value>
        [Obsolete("Use the Count property instead")]
        public int Size => entries_.Length;

        /// <summary>Gets or sets the use zip 64.</summary>
        /// <value>The use zip 64.</value>
        public UseZip64 UseZip64 { get; set; } = UseZip64.Dynamic;

        /// <summary>Gets the zip file comment.</summary>
        /// <value>The zip file comment.</value>
        public string ZipFileComment { get; private set; }

        /// <summary>Gets a value indicating whether the have keys.</summary>
        /// <value>True if have keys, false if not.</value>
        private bool HaveKeys => Key != null;

        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        private byte[] Key { get; set; }

        /// <summary>Indexer to get items within this collection using array index syntax.</summary>
        /// <param name="index">Zero-based index of the entry to access.</param>
        /// <returns>The indexed item.</returns>
        [IndexerName("EntryByIndex")]
        public ZipEntry this[int index] => (ZipEntry)entries_[index].Clone();

        /// <summary>Creates a new ZipFile.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <returns>A ZipFile.</returns>
        public static ZipFile Create(string fileName)
        {
            var fileStream =
                fileName != null ? File.Create(fileName) : throw new ArgumentNullException(nameof(fileName));
            return new ZipFile { Name = fileName, baseStream_ = fileStream, IsStreamOwner = true };
        }

        /// <summary>Creates a new ZipFile.</summary>
        /// <param name="outStream">Stream to write data to.</param>
        /// <returns>A ZipFile.</returns>
        public static ZipFile Create(Stream outStream)
        {
            if (outStream == null)
            {
                throw new ArgumentNullException(nameof(outStream));
            }
            if (!outStream.CanWrite)
            {
                throw new ArgumentException("Stream is not writeable", nameof(outStream));
            }
            return outStream.CanSeek
                ? new ZipFile { baseStream_ = outStream }
                : throw new ArgumentException("Stream is not seekable", nameof(outStream));
        }

        /// <summary>Abort update.</summary>
        public void AbortUpdate()
        {
            PostUpdateCleanup();
        }

        /// <summary>Adds fileName.</summary>
        /// <param name="fileName">         Filename of the file.</param>
        /// <param name="compressionMethod">The compression method.</param>
        /// <param name="useUnicodeText">   True to use unicode text.</param>
        public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
            {
                throw new ArgumentOutOfRangeException(nameof(compressionMethod));
            }
            CheckUpdating();
            contentsEdited_ = true;
            var entry = EntryFactory.MakeFileEntry(fileName);
            entry.IsUnicodeText = useUnicodeText;
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(fileName, entry));
        }

        /// <summary>Adds fileName.</summary>
        /// <param name="fileName">         Filename of the file.</param>
        /// <param name="compressionMethod">The compression method.</param>
        public void Add(string fileName, CompressionMethod compressionMethod)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
            {
                throw new ArgumentOutOfRangeException(nameof(compressionMethod));
            }
            CheckUpdating();
            contentsEdited_ = true;
            var entry = EntryFactory.MakeFileEntry(fileName);
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(fileName, entry));
        }

        /// <summary>Adds fileName.</summary>
        /// <param name="fileName">The file name to add.</param>
        public void Add(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(fileName)));
        }

        /// <summary>Adds fileName.</summary>
        /// <param name="fileName"> Filename of the file.</param>
        /// <param name="entryName">Name of the entry.</param>
        public void Add(string fileName, string entryName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(fileName, EntryFactory.MakeFileEntry(entryName)));
        }

        /// <summary>Adds dataSource.</summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="entryName"> Name of the entry.</param>
        public void Add(IStaticDataSource dataSource, string entryName)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(dataSource, EntryFactory.MakeFileEntry(entryName, false)));
        }

        /// <summary>Adds dataSource.</summary>
        /// <param name="dataSource">       The data source.</param>
        /// <param name="entryName">        Name of the entry.</param>
        /// <param name="compressionMethod">The compression method.</param>
        public void Add(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            var entry = EntryFactory.MakeFileEntry(entryName, false);
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(dataSource, entry));
        }

        /// <summary>Adds dataSource.</summary>
        /// <param name="dataSource">       The data source.</param>
        /// <param name="entryName">        Name of the entry.</param>
        /// <param name="compressionMethod">The compression method.</param>
        /// <param name="useUnicodeText">   True to use unicode text.</param>
        public void Add(
            IStaticDataSource dataSource,
            string entryName,
            CompressionMethod compressionMethod,
            bool useUnicodeText)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(nameof(dataSource));
            }
            if (entryName == null)
            {
                throw new ArgumentNullException(nameof(entryName));
            }
            CheckUpdating();
            var entry = EntryFactory.MakeFileEntry(entryName, false);
            entry.IsUnicodeText = useUnicodeText;
            entry.CompressionMethod = compressionMethod;
            AddUpdate(new ZipUpdate(dataSource, entry));
        }

        /// <summary>Adds entry.</summary>
        /// <param name="entry">The entry to add.</param>
        public void Add(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            CheckUpdating();
            if (entry.Size != 0L || entry.CompressedSize != 0L)
            {
                throw new ZipException("Entry cannot have any data");
            }
            AddUpdate(new ZipUpdate(UpdateCommand.Add, entry));
        }

        /// <summary>Adds a directory.</summary>
        /// <param name="directoryName">Pathname of the directory.</param>
        public void AddDirectory(string directoryName)
        {
            if (directoryName == null)
            {
                throw new ArgumentNullException(nameof(directoryName));
            }
            CheckUpdating();
            AddUpdate(new ZipUpdate(UpdateCommand.Add, EntryFactory.MakeDirectoryEntry(directoryName)));
        }

        /// <summary>Begins an update.</summary>
        /// <param name="archiveStorage">The archive storage.</param>
        /// <param name="dataSource">    The data source.</param>
        public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            if (IsEmbeddedArchive)
            {
                throw new ZipException("Cannot update embedded/SFX archives");
            }
            archiveStorage_ = archiveStorage ?? throw new ArgumentNullException(nameof(archiveStorage));
            updateDataSource_ = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
            updateIndex_ = new Hashtable();
            updates_ = new ArrayList(entries_.Length);
            foreach (var entry in entries_)
            {
                var num = updates_.Add(new ZipUpdate(entry));
                updateIndex_.Add(entry.Name, num);
            }
            updates_.Sort(new UpdateComparer());
            var num1 = 0;
            foreach (ZipUpdate update in updates_)
            {
                if (num1 != updates_.Count - 1)
                {
                    update.OffsetBasedSize = ((ZipUpdate)updates_[num1 + 1]).Entry.Offset - update.Entry.Offset;
                    ++num1;
                }
                else
                {
                    break;
                }
            }
            updateCount_ = updates_.Count;
            contentsEdited_ = false;
            commentEdited_ = false;
            newComment_ = null;
        }

        /// <summary>Begins an update.</summary>
        /// <param name="archiveStorage">The archive storage.</param>
        public void BeginUpdate(IArchiveStorage archiveStorage)
        {
            BeginUpdate(archiveStorage, new DynamicDiskDataSource());
        }

        /// <summary>Begins an update.</summary>
        public void BeginUpdate()
        {
            if (Name == null)
            {
                BeginUpdate(new MemoryArchiveStorage(), new DynamicDiskDataSource());
            }
            else
            {
                BeginUpdate(new DiskArchiveStorage(this), new DynamicDiskDataSource());
            }
        }

        /// <summary>Commits an update.</summary>
        public void CommitUpdate()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            CheckUpdating();
            try
            {
                updateIndex_.Clear();
                updateIndex_ = null;
                if (contentsEdited_)
                {
                    RunUpdates();
                }
                else if (commentEdited_)
                {
                    UpdateCommentOnly();
                }
                else
                {
                    if (entries_.Length != 0)
                    {
                        return;
                    }
                    var comment = newComment_ != null ? newComment_.RawComment : ZipConstants.ConvertToArray(ZipFileComment);
                    using var zipHelperStream = new ZipHelperStream(baseStream_);
                    zipHelperStream.WriteEndOfCentralDirectory(0L, 0L, 0L, comment);
                }
            }
            finally
            {
                PostUpdateCleanup();
            }
        }

        /// <summary>Deletes the given fileName.</summary>
        /// <param name="fileName">The file name to delete.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Delete(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            CheckUpdating();
            var existingUpdate = FindExistingUpdate(fileName);
            if (existingUpdate < 0 || updates_[existingUpdate] == null)
            {
                throw new ZipException("Cannot find entry to delete");
            }
            var flag = true;
            contentsEdited_ = true;
            updates_[existingUpdate] = null;
            --updateCount_;
            return flag;
        }

        /// <summary>Deletes the given entry.</summary>
        /// <param name="entry">The entry to delete.</param>
        public void Delete(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            CheckUpdating();
            var existingUpdate = FindExistingUpdate(entry);
            if (existingUpdate < 0)
            {
                throw new ZipException("Cannot find entry to delete");
            }
            contentsEdited_ = true;
            updates_[existingUpdate] = null;
            --updateCount_;
        }

        /// <summary>Searches for the first entry.</summary>
        /// <param name="name">      The name.</param>
        /// <param name="ignoreCase">True to ignore case.</param>
        /// <returns>The found entry.</returns>
        public int FindEntry(string name, bool ignoreCase)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            for (var index = 0; index < entries_.Length; ++index)
            {
                if (string.Compare(name, entries_[index].Name, ignoreCase, CultureInfo.InvariantCulture) == 0)
                {
                    return index;
                }
            }
            return -1;
        }

        /// <summary>Gets an entry.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The entry.</returns>
        public ZipEntry GetEntry(string name)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            var entry = FindEntry(name, true);
            return entry < 0 ? null : (ZipEntry)entries_[entry].Clone();
        }

        /// <inheritdoc/>
        public IEnumerator GetEnumerator()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            return new ZipEntryEnumerator(entries_);
        }

        /// <summary>Gets input stream.</summary>
        /// <param name="entry">The entry.</param>
        /// <returns>The input stream.</returns>
        public Stream GetInputStream(ZipEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            var entryIndex = entry.ZipFileIndex;
            if (entryIndex < 0L || entryIndex >= entries_.Length || entries_[entryIndex].Name != entry.Name)
            {
                entryIndex = FindEntry(entry.Name, true);
                if (entryIndex < 0L)
                {
                    throw new ZipException("Entry cannot be found");
                }
            }
            return GetInputStream(entryIndex);
        }

        /// <summary>Gets input stream.</summary>
        /// <param name="entryIndex">Zero-based index of the entry.</param>
        /// <returns>The input stream.</returns>
        public Stream GetInputStream(long entryIndex)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            var start = LocateEntry(entries_[entryIndex]);
            var compressionMethod = entries_[entryIndex].CompressionMethod;
            Stream stream = new PartialInputStream(this, start, entries_[entryIndex].CompressedSize);
            if (entries_[entryIndex].IsCrypted)
            {
                stream = CreateAndInitDecryptionStream(stream, entries_[entryIndex]);
                if (stream == null)
                {
                    throw new ZipException("Unable to decrypt this entry");
                }
            }
            switch (compressionMethod)
            {
                case CompressionMethod.Stored:
                {
                    return stream;
                }
                case CompressionMethod.Deflated:
                {
                    stream = new InflaterInputStream(stream, new Inflater(true));
                    goto case CompressionMethod.Stored;
                }
                default:
                {
                    throw new ZipException("Unsupported compression method " + compressionMethod);
                }
            }
        }

        /// <summary>Sets a comment.</summary>
        /// <param name="comment">The comment.</param>
        public void SetComment(string comment)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            CheckUpdating();
            newComment_ = new ZipString(comment);
            if (newComment_.RawLength > ushort.MaxValue)
            {
                newComment_ = null;
                throw new ZipException("Comment length exceeds maximum - 65535");
            }
            commentEdited_ = true;
        }

        /// <summary>Tests archive.</summary>
        /// <param name="testData">True to test data.</param>
        /// <returns>True if the test passes, false if the test fails.</returns>
        public bool TestArchive(bool testData)
        {
            return TestArchive(testData, TestStrategy.FindFirstError, null);
        }

        /// <summary>Tests archive.</summary>
        /// <param name="testData">     True to test data.</param>
        /// <param name="strategy">     The strategy.</param>
        /// <param name="resultHandler">The result handler.</param>
        /// <returns>True if the test passes, false if the test fails.</returns>
        public bool TestArchive(bool testData, TestStrategy strategy, ZipTestResultHandler resultHandler)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(ZipFile));
            }
            var status = new TestStatus(this);
            resultHandler?.Invoke(status, null);
            var tests = testData ? HeaderTest.Extract | HeaderTest.Header : HeaderTest.Header;
            var flag = true;
            try
            {
                for (var index = 0; flag && (long)index < Count; ++index)
                {
                    if (resultHandler != null)
                    {
                        status.SetEntry(this[index]);
                        status.SetOperation(TestOperation.EntryHeader);
                        resultHandler(status, null);
                    }
                    try
                    {
                        TestLocalHeader(this[index], tests);
                    }
                    catch (ZipException ex)
                    {
                        status.AddError();
                        resultHandler?.Invoke(status, string.Format("Exception during test - '{0}'", ex.Message));
                        if (strategy == TestStrategy.FindFirstError)
                        {
                            flag = false;
                        }
                    }
                    if (flag && testData && this[index].IsFile)
                    {
                        if (resultHandler != null)
                        {
                            status.SetOperation(TestOperation.EntryData);
                            resultHandler(status, null);
                        }
                        var crc32 = new Crc32();
                        using (var inputStream = GetInputStream(this[index]))
                        {
                            var buffer = new byte[4096];
                            long num = 0;
                            int count;
                            while ((count = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                crc32.Update(buffer, 0, count);
                                if (resultHandler != null)
                                {
                                    num += count;
                                    status.SetBytesTested(num);
                                    resultHandler(status, null);
                                }
                            }
                        }
                        if (this[index].Crc != crc32.Value)
                        {
                            status.AddError();
                            resultHandler?.Invoke(status, "CRC mismatch");
                            if (strategy == TestStrategy.FindFirstError)
                            {
                                flag = false;
                            }
                        }
                        if ((this[index].Flags & 8) != 0)
                        {
                            var zipHelperStream = new ZipHelperStream(baseStream_);
                            var data = new DescriptorData();
                            zipHelperStream.ReadDataDescriptor(this[index].LocalHeaderRequiresZip64, data);
                            if (this[index].Crc != data.Crc)
                            {
                                status.AddError();
                            }
                            if (this[index].CompressedSize != data.CompressedSize)
                            {
                                status.AddError();
                            }
                            if (this[index].Size != data.Size)
                            {
                                status.AddError();
                            }
                        }
                    }
                    if (resultHandler != null)
                    {
                        status.SetOperation(TestOperation.EntryComplete);
                        resultHandler(status, null);
                    }
                }
                if (resultHandler != null)
                {
                    status.SetOperation(TestOperation.MiscellaneousTests);
                    resultHandler(status, null);
                }
            }
            catch (Exception ex)
            {
                status.AddError();
                resultHandler?.Invoke(status, string.Format("Exception during test - '{0}'", ex.Message));
            }
            if (resultHandler != null)
            {
                status.SetOperation(TestOperation.Complete);
                status.SetEntry(null);
                resultHandler(status, null);
            }
            return status.ErrorCount == 0;
        }

        /// <summary>Closes this ZipFile.</summary>
        public void Close()
        {
            Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the ICSharpCode.SharpZipLib.Zip.ZipFile and optionally
        ///     releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;
            entries_ = Array.Empty<ZipEntry>();
            if (IsStreamOwner && baseStream_ != null)
            {
                lock (padLock)
                {
                    baseStream_.Close();
                }
            }
            PostUpdateCleanup();
        }

        /// <summary>Check classic password.</summary>
        /// <param name="classicCryptoStream">The classic crypto stream.</param>
        /// <param name="entry">              The entry.</param>
        private static void CheckClassicPassword(CryptoStream classicCryptoStream, ZipEntry entry)
        {
            var buffer = new byte[12];
            StreamUtils.ReadFully(classicCryptoStream, buffer);
            if (buffer[11] != entry.CryptoCheckValue)
            {
                throw new ZipException("Invalid password");
            }
        }

        /// <summary>Writes an encryption header.</summary>
        /// <param name="stream">  The stream.</param>
        /// <param name="crcValue">The CRC value.</param>
        private static void WriteEncryptionHeader(Stream stream, long crcValue)
        {
            var buffer = new byte[12];
            new Random().NextBytes(buffer);
            buffer[11] = (byte)(crcValue >> 24);
            stream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>Adds an entry to 'update'.</summary>
        /// <param name="workFile">The work file.</param>
        /// <param name="update">  The update.</param>
        private void AddEntry(ZipFile workFile, ZipUpdate update)
        {
            Stream source = null;
            if (update.Entry.IsFile)
            {
                source = update.GetSource() ?? updateDataSource_.GetSource(update.Entry, update.Filename);
            }
            if (source != null)
            {
                using (source)
                {
                    var length = source.Length;
                    if (update.OutEntry.Size < 0L)
                    {
                        update.OutEntry.Size = length;
                    }
                    else if (update.OutEntry.Size != length)
                    {
                        throw new ZipException("Entry size/stream size mismatch");
                    }
                    workFile.WriteLocalEntryHeader(update);
                    var position1 = workFile.baseStream_.Position;
                    using (var outputStream = workFile.GetOutputStream(update.OutEntry))
                    {
                        CopyBytes(update, outputStream, source, length, true);
                    }
                    var position2 = workFile.baseStream_.Position;
                    update.OutEntry.CompressedSize = position2 - position1;
                    if ((update.OutEntry.Flags & 8) != 8)
                    {
                        return;
                    }
                    new ZipHelperStream(workFile.baseStream_).WriteDataDescriptor(update.OutEntry);
                }
            }
            else
            {
                workFile.WriteLocalEntryHeader(update);
                update.OutEntry.CompressedSize = 0L;
            }
        }

        /// <summary>Adds an update.</summary>
        /// <param name="update">The update.</param>
        private void AddUpdate(ZipUpdate update)
        {
            contentsEdited_ = true;
            var existingUpdate = FindExistingUpdate(update.Entry.Name);
            if (existingUpdate >= 0)
            {
                if (updates_[existingUpdate] == null)
                {
                    ++updateCount_;
                }
                updates_[existingUpdate] = update;
            }
            else
            {
                var num = updates_.Add(update);
                ++updateCount_;
                updateIndex_.Add(update.Entry.Name, num);
            }
        }

        /// <summary>Check updating.</summary>
        private void CheckUpdating()
        {
            if (updates_ == null)
            {
                throw new InvalidOperationException("BeginUpdate has not been called");
            }
        }

        /// <summary>Copies the bytes.</summary>
        /// <param name="update">     The update.</param>
        /// <param name="destination">Destination for the.</param>
        /// <param name="source">     Another instance to copy.</param>
        /// <param name="bytesToCopy">The bytes to copy.</param>
        /// <param name="updateCrc">  True to update CRC.</param>
        private void CopyBytes(ZipUpdate update, Stream destination, Stream source, long bytesToCopy, bool updateCrc)
        {
            if (destination == source)
            {
                throw new InvalidOperationException("Destination and source are the same");
            }
            var crc32 = new Crc32();
            var buffer = GetBuffer();
            var num1 = bytesToCopy;
            long num2 = 0;
            int count1;
            do
            {
                var count2 = buffer.Length;
                if (bytesToCopy < count2)
                {
                    count2 = (int)bytesToCopy;
                }
                count1 = source.Read(buffer, 0, count2);
                if (count1 > 0)
                {
                    if (updateCrc)
                    {
                        crc32.Update(buffer, 0, count1);
                    }
                    destination.Write(buffer, 0, count1);
                    bytesToCopy -= count1;
                    num2 += count1;
                }
            }
            while (count1 > 0 && bytesToCopy > 0L);
            if (num2 != num1)
            {
                throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num1, num2));
            }
            if (!updateCrc)
            {
                return;
            }
            update.OutEntry.Crc = crc32.Value;
        }

        /// <summary>Copies the descriptor bytes.</summary>
        /// <param name="update">The update.</param>
        /// <param name="dest">  Destination for the.</param>
        /// <param name="source">Another instance to copy.</param>
        private void CopyDescriptorBytes(ZipUpdate update, Stream dest, Stream source)
        {
            var descriptorSize = GetDescriptorSize(update);
            if (descriptorSize <= 0)
            {
                return;
            }
            var buffer = GetBuffer();
            int count1;
            for (; descriptorSize > 0; descriptorSize -= count1)
            {
                var count2 = Math.Min(buffer.Length, descriptorSize);
                count1 = source.Read(buffer, 0, count2);
                if (count1 <= 0)
                {
                    throw new ZipException("Unxpected end of stream");
                }
                dest.Write(buffer, 0, count1);
            }
        }

        /// <summary>Copies the descriptor bytes direct.</summary>
        /// <param name="update">             The update.</param>
        /// <param name="stream">             The stream.</param>
        /// <param name="destinationPosition">Destination position.</param>
        /// <param name="sourcePosition">     Source position.</param>
        private void CopyDescriptorBytesDirect(
            ZipUpdate update,
            Stream stream,
            ref long destinationPosition,
            long sourcePosition)
        {
            var descriptorSize = GetDescriptorSize(update);
            while (descriptorSize > 0)
            {
                var count1 = descriptorSize;
                var buffer = GetBuffer();
                stream.Position = sourcePosition;
                var count2 = stream.Read(buffer, 0, count1);
                if (count2 <= 0)
                {
                    throw new ZipException("Unxpected end of stream");
                }
                stream.Position = destinationPosition;
                stream.Write(buffer, 0, count2);
                descriptorSize -= count2;
                destinationPosition += count2;
                sourcePosition += count2;
            }
        }

        /// <summary>Copies the entry.</summary>
        /// <param name="workFile">The work file.</param>
        /// <param name="update">  The update.</param>
        private void CopyEntry(ZipFile workFile, ZipUpdate update)
        {
            workFile.WriteLocalEntryHeader(update);
            if (update.Entry.CompressedSize > 0L)
            {
                baseStream_.Seek(update.Entry.Offset + 26L, SeekOrigin.Begin);
                baseStream_.Seek(ReadLEUshort() + (uint)ReadLEUshort(), SeekOrigin.Current);
                CopyBytes(update, workFile.baseStream_, baseStream_, update.Entry.CompressedSize, false);
            }
            CopyDescriptorBytes(update, workFile.baseStream_, baseStream_);
        }

        /// <summary>Copies the entry data direct.</summary>
        /// <param name="update">             The update.</param>
        /// <param name="stream">             The stream.</param>
        /// <param name="updateCrc">          True to update CRC.</param>
        /// <param name="destinationPosition">Destination position.</param>
        /// <param name="sourcePosition">     Source position.</param>
        private void CopyEntryDataDirect(
            ZipUpdate update,
            Stream stream,
            bool updateCrc,
            ref long destinationPosition,
            ref long sourcePosition)
        {
            var compressedSize = update.Entry.CompressedSize;
            var crc32 = new Crc32();
            var buffer = GetBuffer();
            var num1 = compressedSize;
            long num2 = 0;
            int count1;
            do
            {
                var count2 = buffer.Length;
                if (compressedSize < count2)
                {
                    count2 = (int)compressedSize;
                }
                stream.Position = sourcePosition;
                count1 = stream.Read(buffer, 0, count2);
                if (count1 > 0)
                {
                    if (updateCrc)
                    {
                        crc32.Update(buffer, 0, count1);
                    }
                    stream.Position = destinationPosition;
                    stream.Write(buffer, 0, count1);
                    destinationPosition += count1;
                    sourcePosition += count1;
                    compressedSize -= count1;
                    num2 += count1;
                }
            }
            while (count1 > 0 && compressedSize > 0L);
            if (num2 != num1)
            {
                throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", num1, num2));
            }
            if (!updateCrc)
            {
                return;
            }
            update.OutEntry.Crc = crc32.Value;
        }

        /// <summary>Copies the entry direct.</summary>
        /// <param name="workFile">           The work file.</param>
        /// <param name="update">             The update.</param>
        /// <param name="destinationPosition">Destination position.</param>
        private void CopyEntryDirect(ZipFile workFile, ZipUpdate update, ref long destinationPosition)
        {
            var flag = false;
            if (update.Entry.Offset == destinationPosition)
            {
                flag = true;
            }
            if (!flag)
            {
                baseStream_.Position = destinationPosition;
                workFile.WriteLocalEntryHeader(update);
                destinationPosition = baseStream_.Position;
            }
            var offset = update.Entry.Offset + 26L;
            baseStream_.Seek(offset, SeekOrigin.Begin);
            var sourcePosition = baseStream_.Position + ReadLEUshort() + ReadLEUshort();
            if (flag)
            {
                if (update.OffsetBasedSize != -1L)
                {
                    destinationPosition += update.OffsetBasedSize;
                }
                else
                {
                    destinationPosition += sourcePosition
                        - offset
                        + 26L
                        + update.Entry.CompressedSize
                        + GetDescriptorSize(update);
                }
            }
            else
            {
                if (update.Entry.CompressedSize > 0L)
                {
                    CopyEntryDataDirect(update, baseStream_, false, ref destinationPosition, ref sourcePosition);
                }
                CopyDescriptorBytesDirect(update, baseStream_, ref destinationPosition, sourcePosition);
            }
        }

        /// <summary>Creates and initialise decryption stream.</summary>
        /// <param name="baseStream">The base stream.</param>
        /// <param name="entry">     The entry.</param>
        /// <returns>The new and initialise decryption stream.</returns>
        private Stream CreateAndInitDecryptionStream(Stream baseStream, ZipEntry entry)
        {
            CryptoStream classicCryptoStream;
            if (entry.Version < 50 || (entry.Flags & 64) == 0)
            {
                var pkzipClassicManaged = new PkzipClassicManaged();
                OnKeysRequired(entry.Name);
                if (!HaveKeys)
                {
                    throw new ZipException("No password available for encrypted stream");
                }
                classicCryptoStream = new CryptoStream(
                    baseStream,
                    pkzipClassicManaged.CreateDecryptor(Key, null),
                    CryptoStreamMode.Read);
                CheckClassicPassword(classicCryptoStream, entry);
            }
            else
            {
                if (entry.Version != 51)
                {
                    throw new ZipException("Decryption method not supported");
                }
                OnKeysRequired(entry.Name);
                if (!HaveKeys)
                {
                    throw new ZipException("No password available for AES encrypted stream");
                }
                var aesSaltLen = entry.AESSaltLen;
                var numArray = new byte[aesSaltLen];
                var num = baseStream.Read(numArray, 0, aesSaltLen);
                if (num != aesSaltLen)
                {
                    throw new ZipException("AES Salt expected " + aesSaltLen + " got " + num);
                }
                var buffer = new byte[2];
                baseStream.Read(buffer, 0, 2);
                var blockSize = entry.AESKeySize / 8;
                var transform = new ZipAESTransform(rawPassword_, numArray, blockSize, false);
                var pwdVerifier = transform.PwdVerifier;
                if (pwdVerifier[0] != buffer[0] || pwdVerifier[1] != buffer[1])
                {
                    throw new Exception("Invalid password for AES");
                }
                classicCryptoStream = new ZipAESStream(baseStream, transform, CryptoStreamMode.Read);
            }
            return classicCryptoStream;
        }

        /// <summary>Creates and initialise encryption stream.</summary>
        /// <param name="baseStream">The base stream.</param>
        /// <param name="entry">     The entry.</param>
        /// <returns>The new and initialise encryption stream.</returns>
        private Stream CreateAndInitEncryptionStream(Stream baseStream, ZipEntry entry)
        {
            CryptoStream cryptoStream = null;
            if (entry.Version < 50 || (entry.Flags & 64) == 0)
            {
                var pkzipClassicManaged = new PkzipClassicManaged();
                OnKeysRequired(entry.Name);
                if (!HaveKeys)
                {
                    throw new ZipException("No password available for encrypted stream");
                }
                cryptoStream = new CryptoStream(
                    new UncompressedStream(baseStream),
                    pkzipClassicManaged.CreateEncryptor(Key, null),
                    CryptoStreamMode.Write);
                if (entry.Crc < 0L || (entry.Flags & 8) != 0)
                {
                    WriteEncryptionHeader(cryptoStream, entry.DosTime << 16);
                }
                else
                {
                    WriteEncryptionHeader(cryptoStream, entry.Crc);
                }
            }
            return cryptoStream;
        }

        /// <summary>Searches for the first existing update.</summary>
        /// <param name="entry">The entry.</param>
        /// <returns>The found existing update.</returns>
        private int FindExistingUpdate(ZipEntry entry)
        {
            var num = -1;
            var transformedFileName = GetTransformedFileName(entry.Name);
            if (updateIndex_.ContainsKey(transformedFileName))
            {
                num = (int)updateIndex_[transformedFileName];
            }
            return num;
        }

        /// <summary>Searches for the first existing update.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <returns>The found existing update.</returns>
        private int FindExistingUpdate(string fileName)
        {
            var num = -1;
            var transformedFileName = GetTransformedFileName(fileName);
            if (updateIndex_.ContainsKey(transformedFileName))
            {
                num = (int)updateIndex_[transformedFileName];
            }
            return num;
        }

        /// <summary>Gets the buffer.</summary>
        /// <returns>An array of byte.</returns>
        private byte[] GetBuffer()
        {
            if (copyBuffer_ == null)
            {
                copyBuffer_ = new byte[bufferSize_];
            }
            return copyBuffer_;
        }

        /// <summary>Gets descriptor size.</summary>
        /// <param name="update">The update.</param>
        /// <returns>The descriptor size.</returns>
        private int GetDescriptorSize(ZipUpdate update)
        {
            var num = 0;
            if ((update.Entry.Flags & 8) != 0)
            {
                num = 12;
                if (update.Entry.LocalHeaderRequiresZip64)
                {
                    num = 20;
                }
            }
            return num;
        }

        /// <summary>Gets output stream.</summary>
        /// <param name="entry">The entry.</param>
        /// <returns>The output stream.</returns>
        private Stream GetOutputStream(ZipEntry entry)
        {
            var stream = baseStream_;
            if (entry.IsCrypted)
            {
                stream = CreateAndInitEncryptionStream(stream, entry);
            }
            return entry.CompressionMethod switch
            {
                CompressionMethod.Stored => new UncompressedStream(stream),
                CompressionMethod.Deflated => new DeflaterOutputStream(stream, new Deflater(9, true)) { IsStreamOwner = false },
                _ => throw new ZipException("Unknown compression method " + entry.CompressionMethod),
            };
        }

        /// <summary>Gets transformed directory name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The transformed directory name.</returns>
        private string GetTransformedDirectoryName(string name)
        {
            var nameTransform = NameTransform;
            return nameTransform == null ? name : nameTransform.TransformDirectory(name);
        }

        /// <summary>Gets transformed file name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The transformed file name.</returns>
        private string GetTransformedFileName(string name)
        {
            var nameTransform = NameTransform;
            return nameTransform == null ? name : nameTransform.TransformFile(name);
        }

        /// <summary>Locates block with signature.</summary>
        /// <param name="signature">          The signature.</param>
        /// <param name="endLocation">        The end location.</param>
        /// <param name="minimumBlockSize">   Size of the minimum block.</param>
        /// <param name="maximumVariableData">Information describing the maximum variable.</param>
        /// <returns>A long.</returns>
        private long LocateBlockWithSignature(
            int signature,
            long endLocation,
            int minimumBlockSize,
            int maximumVariableData)
        {
            using var zipHelperStream = new ZipHelperStream(baseStream_);
            return zipHelperStream.LocateBlockWithSignature(
                signature,
                endLocation,
                minimumBlockSize,
                maximumVariableData);
        }

        /// <summary>Locates an entry.</summary>
        /// <param name="entry">The entry.</param>
        /// <returns>A long.</returns>
        private long LocateEntry(ZipEntry entry)
        {
            return TestLocalHeader(entry, HeaderTest.Extract);
        }

        /// <summary>Modify entry.</summary>
        /// <param name="workFile">The work file.</param>
        /// <param name="update">  The update.</param>
        private void ModifyEntry(ZipFile workFile, ZipUpdate update)
        {
            workFile.WriteLocalEntryHeader(update);
            var position1 = workFile.baseStream_.Position;
            if (update.Entry.IsFile && update.Filename != null)
            {
                using var outputStream = workFile.GetOutputStream(update.OutEntry);
                using var inputStream = GetInputStream(update.Entry);
                CopyBytes(update, outputStream, inputStream, inputStream.Length, true);
            }
            var position2 = workFile.baseStream_.Position;
            update.Entry.CompressedSize = position2 - position1;
        }

        /// <summary>Executes the keys required action.</summary>
        /// <param name="fileName">Filename of the file.</param>
        private void OnKeysRequired(string fileName)
        {
            if (KeysRequired == null)
            {
                return;
            }
            var e = new KeysRequiredEventArgs(fileName, Key);
            KeysRequired(this, e);
            Key = e.Key;
        }

        /// <summary>Posts the update cleanup.</summary>
        private void PostUpdateCleanup()
        {
            updateDataSource_ = null;
            updates_ = null;
            updateIndex_ = null;
            if (archiveStorage_ == null)
            {
                return;
            }
            archiveStorage_.Dispose();
            archiveStorage_ = null;
        }

        /// <summary>Reads the entries.</summary>
        private void ReadEntries()
        {
            var endLocation = baseStream_.CanSeek
                ? LocateBlockWithSignature(101010256, baseStream_.Length, 22, ushort.MaxValue)
                : throw new ZipException("ZipFile stream must be seekable");
            if (endLocation < 0L)
            {
                throw new ZipException("Cannot find central directory");
            }
            var num1 = ReadLEUshort();
            var num2 = ReadLEUshort();
            ulong length1 = ReadLEUshort();
            ulong num3 = ReadLEUshort();
            ulong num4 = ReadLEUint();
            long num5 = ReadLEUint();
            uint num6 = ReadLEUshort();
            if (num6 > 0U)
            {
                var numArray = new byte[(int)num6];
                StreamUtils.ReadFully(baseStream_, numArray);
                ZipFileComment = ZipConstants.ConvertToString(numArray);
            }
            else
            {
                ZipFileComment = string.Empty;
            }
            var flag = false;
            if (num1 == ushort.MaxValue
                || num2 == ushort.MaxValue
                || length1 == ushort.MaxValue
                || num3 == ushort.MaxValue
                || num4 == uint.MaxValue
                || num5 == uint.MaxValue)
            {
                flag = true;
                if (LocateBlockWithSignature(117853008, endLocation, 0, 4096) < 0L)
                {
                    throw new ZipException("Cannot find Zip64 locator");
                }
                var num7 = (int)ReadLEUint();
                var num8 = ReadLEUlong();
                var num9 = (int)ReadLEUint();
                baseStream_.Position = (long)num8;
                if (ReadLEUint() != 101075792U)
                {
                    throw new ZipException(string.Format("Invalid Zip64 Central directory signature at {0:X}", num8));
                }
                var num10 = (long)ReadLEUlong();
                int num11 = ReadLEUshort();
                int num12 = ReadLEUshort();
                var num13 = (int)ReadLEUint();
                var num14 = (int)ReadLEUint();
                length1 = ReadLEUlong();
                ReadLEUlong();
                num4 = ReadLEUlong();
                num5 = (long)ReadLEUlong();
            }
            entries_ = new ZipEntry[length1];
            if (!flag && num5 < endLocation - (4L + (long)num4))
            {
                offsetOfFirstEntry = endLocation - (4L + (long)num4 + num5);
                if (offsetOfFirstEntry <= 0L)
                {
                    throw new ZipException("Invalid embedded zip archive");
                }
            }
            baseStream_.Seek(offsetOfFirstEntry + num5, SeekOrigin.Begin);
            for (ulong index = 0; index < length1; ++index)
            {
                if (ReadLEUint() != 33639248U)
                {
                    throw new ZipException("Wrong Central Directory signature");
                }
                int madeByInfo = ReadLEUshort();
                int versionRequiredToExtract = ReadLEUshort();
                int flags = ReadLEUshort();
                int num7 = ReadLEUshort();
                var num8 = ReadLEUint();
                var num9 = ReadLEUint();
                long num10 = ReadLEUint();
                long num11 = ReadLEUint();
                int num12 = ReadLEUshort();
                int length2 = ReadLEUshort();
                int num13 = ReadLEUshort();
                int num14 = ReadLEUshort();
                int num15 = ReadLEUshort();
                var num16 = ReadLEUint();
                long num17 = ReadLEUint();
                var numArray = new byte[Math.Max(num12, num13)];
                StreamUtils.ReadFully(baseStream_, numArray, 0, num12);
                var zipEntry = new ZipEntry(
                    ZipConstants.ConvertToStringExt(flags, numArray, num12),
                    versionRequiredToExtract,
                    madeByInfo,
                    (CompressionMethod)num7)
                {
                    Crc = num9 & (long)uint.MaxValue,
                    Size = num11 & uint.MaxValue,
                    CompressedSize = num10 & uint.MaxValue,
                    Flags = flags,
                    DosTime = num8,
                    ZipFileIndex = (long)index,
                    Offset = num17,
                    ExternalFileAttributes = (int)num16,
                    CryptoCheckValue = (flags & 8) != 0 ? (byte)((num8 >> 8) & byte.MaxValue) : (byte)(num9 >> 24)
                };
                if (length2 > 0)
                {
                    var buffer = new byte[length2];
                    StreamUtils.ReadFully(baseStream_, buffer);
                    zipEntry.ExtraData = buffer;
                }
                zipEntry.ProcessExtraData(false);
                if (num13 > 0)
                {
                    StreamUtils.ReadFully(baseStream_, numArray, 0, num13);
                    zipEntry.Comment = ZipConstants.ConvertToStringExt(flags, numArray, num13);
                }
                entries_[index] = zipEntry;
            }
        }

        /// <summary>Reads le uint.</summary>
        /// <returns>The le uint.</returns>
        private uint ReadLEUint()
        {
            return ReadLEUshort() | ((uint)ReadLEUshort() << 16);
        }

        /// <summary>Reads le ulong.</summary>
        /// <returns>The le ulong.</returns>
        private ulong ReadLEUlong()
        {
            return ReadLEUint() | ((ulong)ReadLEUint() << 32);
        }

        /// <summary>Reads le ushort.</summary>
        /// <returns>The le ushort.</returns>
        private ushort ReadLEUshort()
        {
            var num1 = baseStream_.ReadByte();
            if (num1 < 0)
            {
                throw new EndOfStreamException("End of stream");
            }
            var num2 = baseStream_.ReadByte();
            if (num2 < 0)
            {
                throw new EndOfStreamException("End of stream");
            }
            return (ushort)((ushort)num1 | (uint)(ushort)(num2 << 8));
        }

        /// <summary>Reopens the given source.</summary>
        /// <param name="source">Source for the.</param>
        private void Reopen(Stream source)
        {
            IsNewArchive = false;
            baseStream_ = source ?? throw new ZipException("Failed to reopen archive - no source");
            ReadEntries();
        }

        /// <summary>Reopens this ZipFile.</summary>
        private void Reopen()
        {
            if (Name == null)
            {
                throw new InvalidOperationException("Name is not known cannot Reopen");
            }
            Reopen(File.Open(Name, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        /// <summary>Executes the updates operation.</summary>
        private void RunUpdates()
        {
            long sizeEntries = 0;
            var flag = false;
            long destinationPosition = 0;
            ZipFile workFile;
            if (IsNewArchive)
            {
                workFile = this;
                workFile.baseStream_.Position = 0L;
                flag = true;
            }
            else if (archiveStorage_.UpdateMode == FileUpdateMode.Direct)
            {
                workFile = this;
                workFile.baseStream_.Position = 0L;
                flag = true;
                updates_.Sort(new UpdateComparer());
            }
            else
            {
                workFile = Create(archiveStorage_.GetTemporaryOutput());
                workFile.UseZip64 = UseZip64;
                if (Key != null)
                {
                    workFile.Key = (byte[])Key.Clone();
                }
            }
            long position1;
            try
            {
                foreach (ZipUpdate update in updates_)
                {
                    if (update == null)
                    {
                        continue;
                    }
                    switch (update.Command)
                    {
                        case UpdateCommand.Copy:
                        {
                            if (flag)
                            {
                                CopyEntryDirect(workFile, update, ref destinationPosition);
                                continue;
                            }
                            CopyEntry(workFile, update);
                            continue;
                        }
                        case UpdateCommand.Modify:
                        {
                            ModifyEntry(workFile, update);
                            continue;
                        }
                        case UpdateCommand.Add:
                        {
                            if (!IsNewArchive && flag)
                            {
                                workFile.baseStream_.Position = destinationPosition;
                            }
                            AddEntry(workFile, update);
                            if (flag)
                            {
                                destinationPosition = workFile.baseStream_.Position;
                            }
                            continue;
                        }
                        default:
                        {
                            continue;
                        }
                    }
                }
                if (!IsNewArchive && flag)
                {
                    workFile.baseStream_.Position = destinationPosition;
                }
                var position2 = workFile.baseStream_.Position;
                foreach (ZipUpdate update in updates_)
                {
                    if (update != null)
                    {
                        sizeEntries += workFile.WriteCentralDirectoryHeader(update.OutEntry);
                    }
                }
                var comment = newComment_ != null ? newComment_.RawComment : ZipConstants.ConvertToArray(ZipFileComment);
                using (var zipHelperStream = new ZipHelperStream(workFile.baseStream_))
                {
                    zipHelperStream.WriteEndOfCentralDirectory(updateCount_, sizeEntries, position2, comment);
                }
                position1 = workFile.baseStream_.Position;
                foreach (ZipUpdate update in updates_)
                {
                    if (update == null)
                    {
                        continue;
                    }
                    if (update.CrcPatchOffset > 0L && update.OutEntry.CompressedSize > 0L)
                    {
                        workFile.baseStream_.Position = update.CrcPatchOffset;
                        workFile.WriteLEInt((int)update.OutEntry.Crc);
                    }
                    if (update.SizePatchOffset <= 0L)
                    {
                        continue;
                    }
                    workFile.baseStream_.Position = update.SizePatchOffset;
                    if (update.OutEntry.LocalHeaderRequiresZip64)
                    {
                        workFile.WriteLeLong(update.OutEntry.Size);
                        workFile.WriteLeLong(update.OutEntry.CompressedSize);
                    }
                    else
                    {
                        workFile.WriteLEInt((int)update.OutEntry.CompressedSize);
                        workFile.WriteLEInt((int)update.OutEntry.Size);
                    }
                }
            }
            catch
            {
                workFile.Close();
                if (!flag && workFile.Name != null)
                {
                    File.Delete(workFile.Name);
                }
                throw;
            }
            if (flag)
            {
                workFile.baseStream_.SetLength(position1);
                workFile.baseStream_.Flush();
                IsNewArchive = false;
                ReadEntries();
            }
            else
            {
                baseStream_.Close();
                Reopen(archiveStorage_.ConvertTemporaryToFinal());
            }
        }

        /// <summary>Tests local header.</summary>
        /// <param name="entry">The entry.</param>
        /// <param name="tests">The tests.</param>
        /// <returns>A long.</returns>
        private long TestLocalHeader(ZipEntry entry, HeaderTest tests)
        {
            lock (padLock)
            {
                var flag1 = (tests & HeaderTest.Header) != 0;
                var flag2 = (tests & HeaderTest.Extract) != 0;
                baseStream_.Seek(offsetOfFirstEntry + entry.Offset, SeekOrigin.Begin);
                if (ReadLEUint() != 67324752U)
                {
                    throw new ZipException(
                        string.Format("Wrong local header signature @{0:X}", offsetOfFirstEntry + entry.Offset));
                }
                var num1 = (short)ReadLEUshort();
                var num2 = (short)ReadLEUshort();
                var num3 = (short)ReadLEUshort();
                var num4 = (short)ReadLEUshort();
                var num5 = (short)ReadLEUshort();
                var num6 = ReadLEUint();
                long num7 = ReadLEUint();
                long num8 = ReadLEUint();
                int length1 = ReadLEUshort();
                int length2 = ReadLEUshort();
                var numArray1 = new byte[length1];
                StreamUtils.ReadFully(baseStream_, numArray1);
                var numArray2 = new byte[length2];
                StreamUtils.ReadFully(baseStream_, numArray2);
                var zipExtraData = new ZipExtraData(numArray2);
                if (zipExtraData.Find(1))
                {
                    num8 = zipExtraData.ReadLong();
                    num7 = zipExtraData.ReadLong();
                    if ((num2 & 8) != 0)
                    {
                        if (num8 != -1L && num8 != entry.Size)
                        {
                            throw new ZipException("Size invalid for descriptor");
                        }
                        if (num7 != -1L && num7 != entry.CompressedSize)
                        {
                            throw new ZipException("Compressed size invalid for descriptor");
                        }
                    }
                }
                else if (num1 >= 45 && ((uint)num8 == uint.MaxValue || (uint)num7 == uint.MaxValue))
                {
                    throw new ZipException("Required Zip64 extended information missing");
                }
                if (flag2 && entry.IsFile)
                {
                    if (!entry.IsCompressionMethodSupported())
                    {
                        throw new ZipException("Compression method not supported");
                    }
                    if (num1 > 51 || num1 > 20 && num1 < 45)
                    {
                        throw new ZipException(
                            string.Format("Version required to extract this entry not supported ({0})", num1));
                    }
                    if ((num2 & 12384) != 0)
                    {
                        throw new ZipException(
                            "The library does not support the zip version required to extract this entry");
                    }
                }
                if (flag1)
                {
                    if (num1 <= 63
                        && num1 != 10
                        && num1 != 11
                        && num1 != 20
                        && num1 != 21
                        && num1 != 25
                        && num1 != 27
                        && num1 != 45
                        && num1 != 46
                        && num1 != 50
                        && num1 != 51
                        && num1 != 52
                        && num1 != 61
                        && num1 != 62
                        && num1 != 63)
                    {
                        throw new ZipException(
                            string.Format("Version required to extract this entry is invalid ({0})", num1));
                    }
                    if ((num2 & 49168) != 0)
                    {
                        throw new ZipException("Reserved bit flags cannot be set.");
                    }
                    if ((num2 & 1) != 0 && num1 < 20)
                    {
                        throw new ZipException(
                            string.Format(
                                "Version required to extract this entry is too low for encryption ({0})",
                                num1));
                    }
                    if ((num2 & 64) != 0)
                    {
                        if ((num2 & 1) == 0)
                        {
                            throw new ZipException("Strong encryption flag set but encryption flag is not set");
                        }
                        if (num1 < 50)
                        {
                            throw new ZipException(
                                string.Format(
                                    "Version required to extract this entry is too low for encryption ({0})",
                                    num1));
                        }
                    }
                    if ((num2 & 32) != 0 && num1 < 27)
                    {
                        throw new ZipException(string.Format("Patched data requires higher version than ({0})", num1));
                    }
                    if (num2 != entry.Flags)
                    {
                        throw new ZipException("Central header/local header flags mismatch");
                    }
                    if (entry.CompressionMethod != (CompressionMethod)num3)
                    {
                        throw new ZipException("Central header/local header compression method mismatch");
                    }
                    if (entry.Version != num1)
                    {
                        throw new ZipException("Extract version mismatch");
                    }
                    if ((num2 & 64) != 0 && num1 < 62)
                    {
                        throw new ZipException("Strong encryption flag set but version not high enough");
                    }
                    if ((num2 & 8192) != 0 && (num4 != 0 || num5 != 0))
                    {
                        throw new ZipException("Header masked set but date/time values non-zero");
                    }
                    if ((num2 & 8) == 0 && (int)num6 != (int)(uint)entry.Crc)
                    {
                        throw new ZipException("Central header/local header crc mismatch");
                    }
                    if (num8 == 0L && num7 == 0L && num6 != 0U)
                    {
                        throw new ZipException("Invalid CRC for empty entry");
                    }
                    if (entry.Name.Length > length1)
                    {
                        throw new ZipException("File name length mismatch");
                    }
                    var stringExt = ZipConstants.ConvertToStringExt(num2, numArray1);
                    if (stringExt != entry.Name)
                    {
                        throw new ZipException("Central header and local header file name mismatch");
                    }
                    if (entry.IsDirectory)
                    {
                        if (num8 > 0L)
                        {
                            throw new ZipException("Directory cannot have size");
                        }
                        if (entry.IsCrypted)
                        {
                            if (num7 > 14L)
                            {
                                throw new ZipException("Directory compressed size invalid");
                            }
                        }
                        else if (num7 > 2L)
                        {
                            throw new ZipException("Directory compressed size invalid");
                        }
                    }
                    if (!ZipNameTransform.IsValidName(stringExt, true))
                    {
                        throw new ZipException("Name is invalid");
                    }
                }
                if ((num2 & 8) == 0 || num8 > 0L || num7 > 0L)
                {
                    if (num8 != entry.Size)
                    {
                        throw new ZipException(
                            string.Format(
                                "Size mismatch between central header({0}) and local header({1})",
                                entry.Size,
                                num8));
                    }
                    if (num7 != entry.CompressedSize && num7 != uint.MaxValue && num7 != -1L)
                    {
                        throw new ZipException(
                            string.Format(
                                "Compressed size mismatch between central header({0}) and local header({1})",
                                entry.CompressedSize,
                                num7));
                    }
                }
                var num9 = length1 + length2;
                return offsetOfFirstEntry + entry.Offset + 30L + num9;
            }
        }

        /// <summary>Updates the comment only.</summary>
        private void UpdateCommentOnly()
        {
            var length = baseStream_.Length;
            ZipHelperStream zipHelperStream;
            if (archiveStorage_.UpdateMode == FileUpdateMode.Safe)
            {
                zipHelperStream = new ZipHelperStream(archiveStorage_.MakeTemporaryCopy(baseStream_))
                {
                    IsStreamOwner = true
                };
                baseStream_.Close();
                baseStream_ = null;
            }
            else if (archiveStorage_.UpdateMode == FileUpdateMode.Direct)
            {
                baseStream_ = archiveStorage_.OpenForDirectUpdate(baseStream_);
                zipHelperStream = new ZipHelperStream(baseStream_);
            }
            else
            {
                baseStream_.Close();
                baseStream_ = null;
                zipHelperStream = new ZipHelperStream(Name);
            }
            using (zipHelperStream)
            {
                if (zipHelperStream.LocateBlockWithSignature(101010256, length, 22, ushort.MaxValue) < 0L)
                {
                    throw new ZipException("Cannot find central directory");
                }
                zipHelperStream.Position += 16L;
                var rawComment = newComment_.RawComment;
                zipHelperStream.WriteLEShort(rawComment.Length);
                zipHelperStream.Write(rawComment, 0, rawComment.Length);
                zipHelperStream.SetLength(zipHelperStream.Position);
            }
            if (archiveStorage_.UpdateMode == FileUpdateMode.Safe)
            {
                Reopen(archiveStorage_.ConvertTemporaryToFinal());
            }
            else
            {
                ReadEntries();
            }
        }

        /// <summary>Writes a central directory header.</summary>
        /// <param name="entry">The entry.</param>
        /// <returns>An int.</returns>
        private int WriteCentralDirectoryHeader(ZipEntry entry)
        {
            if (entry.CompressedSize < 0L)
            {
                throw new ZipException("Attempt to write central directory entry with unknown csize");
            }
            if (entry.Size < 0L)
            {
                throw new ZipException("Attempt to write central directory entry with unknown size");
            }
            if (entry.Crc < 0L)
            {
                throw new ZipException("Attempt to write central directory entry with unknown crc");
            }
            WriteLEInt(33639248);
            WriteLEShort(51);
            WriteLEShort(entry.Version);
            WriteLEShort(entry.Flags);
            WriteLEShort((byte)entry.CompressionMethod);
            WriteLEInt((int)entry.DosTime);
            WriteLEInt((int)entry.Crc);
            if (entry.IsZip64Forced() || entry.CompressedSize >= uint.MaxValue)
            {
                WriteLEInt(-1);
            }
            else
            {
                WriteLEInt((int)(entry.CompressedSize & uint.MaxValue));
            }
            if (entry.IsZip64Forced() || entry.Size >= uint.MaxValue)
            {
                WriteLEInt(-1);
            }
            else
            {
                WriteLEInt((int)entry.Size);
            }
            var array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
            if (array.Length > ushort.MaxValue)
            {
                throw new ZipException("Entry name is too long.");
            }
            WriteLEShort(array.Length);
            var zipExtraData = new ZipExtraData(entry.ExtraData);
            if (entry.CentralHeaderRequiresZip64)
            {
                zipExtraData.StartNewEntry();
                if (entry.Size >= uint.MaxValue || UseZip64 == UseZip64.On)
                {
                    zipExtraData.AddLeLong(entry.Size);
                }
                if (entry.CompressedSize >= uint.MaxValue || UseZip64 == UseZip64.On)
                {
                    zipExtraData.AddLeLong(entry.CompressedSize);
                }
                if (entry.Offset >= uint.MaxValue)
                {
                    zipExtraData.AddLeLong(entry.Offset);
                }
                zipExtraData.AddNewEntry(1);
            }
            else
            {
                zipExtraData.Delete(1);
            }
            var entryData = zipExtraData.GetEntryData();
            WriteLEShort(entryData.Length);
            WriteLEShort(entry.Comment != null ? entry.Comment.Length : 0);
            WriteLEShort(0);
            WriteLEShort(0);
            if (entry.ExternalFileAttributes != -1)
            {
                WriteLEInt(entry.ExternalFileAttributes);
            }
            else if (entry.IsDirectory)
            {
                WriteLEUint(16U);
            }
            else
            {
                WriteLEUint(0U);
            }
            if (entry.Offset >= uint.MaxValue)
            {
                WriteLEUint(uint.MaxValue);
            }
            else
            {
                WriteLEUint((uint)(int)entry.Offset);
            }
            if (array.Length > 0)
            {
                baseStream_.Write(array, 0, array.Length);
            }
            if (entryData.Length > 0)
            {
                baseStream_.Write(entryData, 0, entryData.Length);
            }
            var buffer = entry.Comment != null ? Encoding.ASCII.GetBytes(entry.Comment) : Array.Empty<byte>();
            if (buffer.Length > 0)
            {
                baseStream_.Write(buffer, 0, buffer.Length);
            }
            return 46 + array.Length + entryData.Length + buffer.Length;
        }

        /// <summary>Writes a le int.</summary>
        /// <param name="value">The value.</param>
        private void WriteLEInt(int value)
        {
            WriteLEShort(value & ushort.MaxValue);
            WriteLEShort(value >> 16);
        }

        /// <summary>Writes a le long.</summary>
        /// <param name="value">The value.</param>
        private void WriteLeLong(long value)
        {
            WriteLEInt((int)(value & uint.MaxValue));
            WriteLEInt((int)(value >> 32));
        }

        /// <summary>Writes a le short.</summary>
        /// <param name="value">The value.</param>
        private void WriteLEShort(int value)
        {
            baseStream_.WriteByte((byte)(value & byte.MaxValue));
            baseStream_.WriteByte((byte)((value >> 8) & byte.MaxValue));
        }

        /// <summary>Writes a le uint.</summary>
        /// <param name="value">The value.</param>
        private void WriteLEUint(uint value)
        {
            WriteLEUshort((ushort)(value & ushort.MaxValue));
            WriteLEUshort((ushort)(value >> 16));
        }

        /// <summary>Writes a le ulong.</summary>
        /// <param name="value">The value.</param>
        private void WriteLEUlong(ulong value)
        {
            WriteLEUint((uint)(value & uint.MaxValue));
            WriteLEUint((uint)(value >> 32));
        }

        /// <summary>Writes a le ushort.</summary>
        /// <param name="value">The value.</param>
        private void WriteLEUshort(ushort value)
        {
            baseStream_.WriteByte((byte)(value & (uint)byte.MaxValue));
            baseStream_.WriteByte((byte)((uint)value >> 8));
        }

        /// <summary>Writes a local entry header.</summary>
        /// <param name="update">The update.</param>
        private void WriteLocalEntryHeader(ZipUpdate update)
        {
            var outEntry = update.OutEntry;
            outEntry.Offset = baseStream_.Position;
            if (update.Command != UpdateCommand.Copy)
            {
                if (outEntry.CompressionMethod == CompressionMethod.Deflated)
                {
                    if (outEntry.Size == 0L)
                    {
                        outEntry.CompressedSize = outEntry.Size;
                        outEntry.Crc = 0L;
                        outEntry.CompressionMethod = CompressionMethod.Stored;
                    }
                }
                else if (outEntry.CompressionMethod == CompressionMethod.Stored)
                {
                    outEntry.Flags &= -9;
                }
                if (HaveKeys)
                {
                    outEntry.IsCrypted = true;
                    if (outEntry.Crc < 0L)
                    {
                        outEntry.Flags |= 8;
                    }
                }
                else
                {
                    outEntry.IsCrypted = false;
                }
                switch (UseZip64)
                {
                    case UseZip64.On:
                    {
                        outEntry.ForceZip64();
                        break;
                    }
                    case UseZip64.Dynamic when outEntry.Size < 0L:
                    {
                        outEntry.ForceZip64();
                        break;
                    }
                }
            }
            WriteLEInt(67324752);
            WriteLEShort(outEntry.Version);
            WriteLEShort(outEntry.Flags);
            WriteLEShort((byte)outEntry.CompressionMethod);
            WriteLEInt((int)outEntry.DosTime);
            if (!outEntry.HasCrc)
            {
                update.CrcPatchOffset = baseStream_.Position;
                WriteLEInt(0);
            }
            else
            {
                WriteLEInt((int)outEntry.Crc);
            }
            if (outEntry.LocalHeaderRequiresZip64)
            {
                WriteLEInt(-1);
                WriteLEInt(-1);
            }
            else
            {
                if (outEntry.CompressedSize < 0L || outEntry.Size < 0L)
                {
                    update.SizePatchOffset = baseStream_.Position;
                }
                WriteLEInt((int)outEntry.CompressedSize);
                WriteLEInt((int)outEntry.Size);
            }
            var array = ZipConstants.ConvertToArray(outEntry.Flags, outEntry.Name);
            if (array.Length > ushort.MaxValue)
            {
                throw new ZipException("Entry name too long.");
            }
            var zipExtraData = new ZipExtraData(outEntry.ExtraData);
            if (outEntry.LocalHeaderRequiresZip64)
            {
                zipExtraData.StartNewEntry();
                zipExtraData.AddLeLong(outEntry.Size);
                zipExtraData.AddLeLong(outEntry.CompressedSize);
                zipExtraData.AddNewEntry(1);
            }
            else
            {
                zipExtraData.Delete(1);
            }
            outEntry.ExtraData = zipExtraData.GetEntryData();
            WriteLEShort(array.Length);
            WriteLEShort(outEntry.ExtraData.Length);
            if (array.Length > 0)
            {
                baseStream_.Write(array, 0, array.Length);
            }
            if (outEntry.LocalHeaderRequiresZip64)
            {
                if (!zipExtraData.Find(1))
                {
                    throw new ZipException("Internal error cannot find extra data");
                }
                update.SizePatchOffset = baseStream_.Position + zipExtraData.CurrentReadIndex;
            }
            if (outEntry.ExtraData.Length <= 0)
            {
                return;
            }
            baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
        }

        /// <summary>A partial input stream.</summary>
        /// <seealso cref="System.IO.Stream" />
        private class PartialInputStream : Stream
        {
            /// <summary>The base stream.</summary>
            private readonly Stream baseStream_;

            private readonly object padLock = new();

            /// <summary>The end.</summary>
            private readonly long end_;

            /// <summary>The length.</summary>
            private readonly long length_;

            /// <summary>The read position.</summary>
            private long readPos_;

            /// <summary>The start.</summary>
            private readonly long start_;

            /// <summary>The zip file.</summary>
            private readonly ZipFile zipFile_;

            /// <summary>
            ///     Initializes a new instance of the
            ///     <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.PartialInputStream" /> class.
            /// </summary>
            /// <param name="zipFile">The zip file.</param>
            /// <param name="start">  The start.</param>
            /// <param name="length"> The length.</param>
            public PartialInputStream(ZipFile zipFile, long start, long length)
            {
                start_ = start;
                length_ = length;
                zipFile_ = zipFile;
                baseStream_ = zipFile_.baseStream_;
                readPos_ = start;
                end_ = start + length;
            }

            /// <inheritdoc/>
            public override bool CanRead => true;

            /// <inheritdoc/>
            public override bool CanSeek => true;

            /// <inheritdoc/>
            public override bool CanTimeout => baseStream_.CanTimeout;

            /// <inheritdoc/>
            public override bool CanWrite => false;

            /// <inheritdoc/>
            public override long Length => length_;

            /// <inheritdoc/>
            public override long Position
            {
                get => readPos_ - start_;
                set
                {
                    var num = start_ + value;
                    if (num < start_)
                    {
                        throw new ArgumentException("Negative position is invalid");
                    }
                    readPos_ = num < end_ ? num : throw new InvalidOperationException("Cannot seek past end");
                }
            }

            /// <inheritdoc/>
            public override void Close() { }

            /// <inheritdoc/>
            public override void Flush() { }

            /// <inheritdoc/>
            public override int Read(byte[] buffer, int offset, int count)
            {
                lock (padLock)
                {
                    if (count > end_ - readPos_)
                    {
                        count = (int)(end_ - readPos_);
                        if (count == 0)
                        {
                            return 0;
                        }
                    }
                    baseStream_.Seek(readPos_, SeekOrigin.Begin);
                    var num = baseStream_.Read(buffer, offset, count);
                    if (num > 0)
                    {
                        readPos_ += num;
                    }
                    return num;
                }
            }

            /// <inheritdoc/>
            public override int ReadByte()
            {
                if (readPos_ >= end_)
                {
                    return -1;
                }
                lock (padLock)
                {
                    baseStream_.Seek(readPos_++, SeekOrigin.Begin);
                    return baseStream_.ReadByte();
                }
            }

            /// <inheritdoc/>
            public override long Seek(long offset, SeekOrigin origin)
            {
                var num = readPos_;
                switch (origin)
                {
                    case SeekOrigin.Begin:
                    {
                        num = start_ + offset;
                        break;
                    }
                    case SeekOrigin.Current:
                    {
                        num = readPos_ + offset;
                        break;
                    }
                    case SeekOrigin.End:
                    {
                        num = end_ + offset;
                        break;
                    }
                }
                if (num < start_)
                {
                    throw new ArgumentException("Negative position is invalid");
                }
                readPos_ = num < end_ ? num : throw new IOException("Cannot seek past end");
                return readPos_;
            }

            /// <inheritdoc/>
            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            /// <inheritdoc/>
            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>An uncompressed stream.</summary>
        /// <seealso cref="System.IO.Stream" />
        private class UncompressedStream : Stream
        {
            /// <summary>The base stream.</summary>
            private readonly Stream baseStream_;

            /// <summary>
            ///     Initializes a new instance of the
            ///     <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.UncompressedStream" /> class.
            /// </summary>
            /// <param name="baseStream">The base stream.</param>
            public UncompressedStream(Stream baseStream)
            {
                baseStream_ = baseStream;
            }

            /// <inheritdoc/>
            public override bool CanRead => false;

            /// <inheritdoc/>
            public override bool CanSeek => false;

            /// <inheritdoc/>
            public override bool CanWrite => baseStream_.CanWrite;

            /// <inheritdoc/>
            public override long Length => 0;

            /// <inheritdoc/>
            public override long Position
            {
                get => baseStream_.Position;
                set { }
            }

            /// <inheritdoc/>
            public override void Close()
            {
                baseStream_?.Dispose();
            }

            /// <inheritdoc/>
            public override void Flush()
            {
                baseStream_.Flush();
            }

            /// <inheritdoc/>
            public override int Read(byte[] buffer, int offset, int count)
            {
                return 0;
            }

            /// <inheritdoc/>
            public override long Seek(long offset, SeekOrigin origin)
            {
                return 0;
            }

            /// <inheritdoc/>
            public override void SetLength(long value) { }

            /// <inheritdoc/>
            public override void Write(byte[] buffer, int offset, int count)
            {
                baseStream_.Write(buffer, offset, count);
            }
        }

        /// <summary>An update comparer.</summary>
        /// <seealso cref="System.Collections.IComparer" />
        private class UpdateComparer : IComparer
        {
            /// <inheritdoc/>
            public int Compare(object x, object y)
            {
                var zipUpdate2 = y as ZipUpdate;
                int num1;
                if (x is not ZipUpdate zipUpdate1)
                {
                    num1 = zipUpdate2 != null ? -1 : 0;
                }
                else if (zipUpdate2 == null)
                {
                    num1 = 1;
                }
                else
                {
                    num1 = (zipUpdate1.Command == UpdateCommand.Copy || zipUpdate1.Command == UpdateCommand.Modify
                            ? 0
                            : 1)
                        - (zipUpdate2.Command == UpdateCommand.Copy || zipUpdate2.Command == UpdateCommand.Modify
                            ? 0
                            : 1);
                    if (num1 == 0)
                    {
                        var num2 = zipUpdate1.Entry.Offset - zipUpdate2.Entry.Offset;
                        num1 = num2 >= 0L
                            ? num2 != 0L
                                ? 1
                                : 0
                            : -1;
                    }
                }
                return num1;
            }
        }

        /// <summary>A zip entry enumerator.</summary>
        /// <seealso cref="System.Collections.IEnumerator" />
        private class ZipEntryEnumerator : IEnumerator
        {
            /// <summary>The array.</summary>
            private readonly ZipEntry[] array;

            /// <summary>Zero-based index of the.</summary>
            private int index = -1;

            /// <summary>
            ///     Initializes a new instance of the
            ///     <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipEntryEnumerator" /> class.
            /// </summary>
            /// <param name="entries">The entries.</param>
            public ZipEntryEnumerator(ZipEntry[] entries)
            {
                array = entries;
            }

            /// <inheritdoc/>
            public object Current => array[index];

            /// <inheritdoc/>
            public bool MoveNext()
            {
                return ++index < array.Length;
            }

            /// <inheritdoc/>
            public void Reset()
            {
                index = -1;
            }
        }

        /// <summary>A zip string.</summary>
        private class ZipString
        {
            /// <summary>The comment.</summary>
            private string comment_;

            /// <summary>The raw comment.</summary>
            private byte[] rawComment_;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipString" />
            ///     class.
            /// </summary>
            /// <param name="comment">The comment.</param>
            public ZipString(string comment)
            {
                comment_ = comment;
                IsSourceString = true;
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipString" />
            ///     class.
            /// </summary>
            /// <param name="rawString">The raw string.</param>
            public ZipString(byte[] rawString)
            {
                rawComment_ = rawString;
            }

            /// <summary>Gets a value indicating whether this ZipString is source string.</summary>
            /// <value>True if this ZipString is source string, false if not.</value>
            public bool IsSourceString { get; }

            /// <summary>Gets the raw comment.</summary>
            /// <value>The raw comment.</value>
            public byte[] RawComment
            {
                get
                {
                    MakeBytesAvailable();
                    return (byte[])rawComment_.Clone();
                }
            }

            /// <summary>Gets the length of the raw.</summary>
            /// <value>The length of the raw.</value>
            public int RawLength
            {
                get
                {
                    MakeBytesAvailable();
                    return rawComment_.Length;
                }
            }

            /// <summary>Implicit cast that converts the given ZipFile.ZipString to a string.</summary>
            /// <param name="zipString">The zip string.</param>
            /// <returns>The result of the operation.</returns>
            public static implicit operator string(ZipString zipString)
            {
                zipString.MakeTextAvailable();
                return zipString.comment_;
            }

            /// <summary>Resets this ZipString.</summary>
            public void Reset()
            {
                if (IsSourceString)
                {
                    rawComment_ = null;
                }
                else
                {
                    comment_ = null;
                }
            }

            /// <summary>Makes bytes available.</summary>
            private void MakeBytesAvailable()
            {
                if (rawComment_ != null)
                {
                    return;
                }
                rawComment_ = ZipConstants.ConvertToArray(comment_);
            }

            /// <summary>Makes text available.</summary>
            private void MakeTextAvailable()
            {
                if (comment_ != null)
                {
                    return;
                }
                comment_ = ZipConstants.ConvertToString(rawComment_);
            }
        }

        /// <summary>A zip update.</summary>
        private class ZipUpdate
        {
            /// <summary>The data source.</summary>
            private readonly IStaticDataSource dataSource_;

            /// <summary>The out entry.</summary>
            private ZipEntry outEntry_;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" />
            ///     class.
            /// </summary>
            /// <param name="fileName">Filename of the file.</param>
            /// <param name="entry">   The entry.</param>
            public ZipUpdate(string fileName, ZipEntry entry)
            {
                Command = UpdateCommand.Add;
                Entry = entry;
                Filename = fileName;
            }

            /// <summary>
            ///     (This constructor is obsolete) initializes a new instance of the
            ///     <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" /> class.
            /// </summary>
            /// <param name="fileName">         Filename of the file.</param>
            /// <param name="entryName">        Name of the entry.</param>
            /// <param name="compressionMethod">The compression method.</param>
            [Obsolete]
            public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
            {
                Command = UpdateCommand.Add;
                Entry = new ZipEntry(entryName)
                {
                    CompressionMethod = compressionMethod
                };
                Filename = fileName;
            }

            /// <summary>
            ///     (This constructor is obsolete) initializes a new instance of the
            ///     <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" /> class.
            /// </summary>
            /// <param name="fileName"> Filename of the file.</param>
            /// <param name="entryName">Name of the entry.</param>
            [Obsolete]
            public ZipUpdate(string fileName, string entryName)
                : this(fileName, entryName, CompressionMethod.Deflated)
            {
            }

            /// <summary>
            ///     (This constructor is obsolete) initializes a new instance of the
            ///     <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" /> class.
            /// </summary>
            /// <param name="dataSource">       The data source.</param>
            /// <param name="entryName">        Name of the entry.</param>
            /// <param name="compressionMethod">The compression method.</param>
            [Obsolete]
            public ZipUpdate(IStaticDataSource dataSource, string entryName, CompressionMethod compressionMethod)
            {
                Command = UpdateCommand.Add;
                Entry = new ZipEntry(entryName)
                {
                    CompressionMethod = compressionMethod
                };
                dataSource_ = dataSource;
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" />
            ///     class.
            /// </summary>
            /// <param name="dataSource">The data source.</param>
            /// <param name="entry">     The entry.</param>
            public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
            {
                Command = UpdateCommand.Add;
                Entry = entry;
                dataSource_ = dataSource;
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" />
            ///     class.
            /// </summary>
            /// <param name="original">The original.</param>
            /// <param name="updated"> The updated.</param>
            public ZipUpdate(ZipEntry original, ZipEntry updated)
            {
                throw new ZipException("Modify not currently supported");
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" />
            ///     class.
            /// </summary>
            /// <param name="command">The command.</param>
            /// <param name="entry">  The entry.</param>
            public ZipUpdate(UpdateCommand command, ZipEntry entry)
            {
                Command = command;
                Entry = (ZipEntry)entry.Clone();
            }

            /// <summary>
            ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" />
            ///     class.
            /// </summary>
            /// <param name="entry">The entry.</param>
            public ZipUpdate(ZipEntry entry) : this(UpdateCommand.Copy, entry) { }

            /// <summary>Gets the command.</summary>
            /// <value>The command.</value>
            public UpdateCommand Command { get; }

            /// <summary>Gets or sets the CRC patch offset.</summary>
            /// <value>The CRC patch offset.</value>
            public long CrcPatchOffset { get; set; } = -1;

            /// <summary>Gets the entry.</summary>
            /// <value>The entry.</value>
            public ZipEntry Entry { get; }

            /// <summary>Gets the filename of the file.</summary>
            /// <value>The filename.</value>
            public string Filename { get; }

            /// <summary>Gets or sets the size of the offset based.</summary>
            /// <value>The size of the offset based.</value>
            public long OffsetBasedSize { get; set; } = -1;

            /// <summary>Gets the out entry.</summary>
            /// <value>The out entry.</value>
            public ZipEntry OutEntry
            {
                get
                {
                    if (outEntry_ == null)
                    {
                        outEntry_ = (ZipEntry)Entry.Clone();
                    }
                    return outEntry_;
                }
            }

            /// <summary>Gets or sets the size patch offset.</summary>
            /// <value>The size patch offset.</value>
            public long SizePatchOffset { get; set; } = -1;

            /// <summary>Gets the source.</summary>
            /// <returns>The source.</returns>
            public Stream GetSource()
            {
                Stream stream = null;
                if (dataSource_ != null)
                {
                    stream = dataSource_.GetSource();
                }
                return stream;
            }
        }
    }
}
