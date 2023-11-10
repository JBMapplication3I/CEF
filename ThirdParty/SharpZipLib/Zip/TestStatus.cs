// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.TestStatus
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>A test status.</summary>
    public class TestStatus
    {

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.TestStatus" /> class.</summary>
        /// <param name="file">The file.</param>
        public TestStatus(ZipFile file)
        {
            File = file;
        }

        /// <summary>Gets the bytes tested.</summary>
        /// <value>The bytes tested.</value>
        public long BytesTested { get; private set; }

        /// <summary>Gets the entry.</summary>
        /// <value>The entry.</value>
        public ZipEntry Entry { get; private set; }

        /// <summary>Gets a value indicating whether the entry valid.</summary>
        /// <value>True if entry valid, false if not.</value>
        public bool EntryValid { get; private set; }

        /// <summary>Gets the number of errors.</summary>
        /// <value>The number of errors.</value>
        public int ErrorCount { get; private set; }

        /// <summary>Gets the file.</summary>
        /// <value>The file.</value>
        public ZipFile File { get; }

        /// <summary>Gets the operation.</summary>
        /// <value>The operation.</value>
        public TestOperation Operation { get; private set; }

        /// <summary>Adds error.</summary>
        internal void AddError()
        {
            ++ErrorCount;
            EntryValid = false;
        }

        /// <summary>Sets bytes tested.</summary>
        /// <param name="value">The value.</param>
        internal void SetBytesTested(long value)
        {
            BytesTested = value;
        }

        /// <summary>Sets an entry.</summary>
        /// <param name="entry">The entry.</param>
        internal void SetEntry(ZipEntry entry)
        {
            Entry = entry;
            EntryValid = true;
            BytesTested = 0L;
        }

        /// <summary>Sets an operation.</summary>
        /// <param name="operation">The operation.</param>
        internal void SetOperation(TestOperation operation)
        {
            Operation = operation;
        }
    }
}
