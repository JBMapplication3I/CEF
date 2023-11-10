// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipExtraData
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>A zip extra data. This class cannot be inherited.</summary>
    /// <seealso cref="IDisposable" />
    public sealed class ZipExtraData : IDisposable
    {
        /// <summary>The data.</summary>
        private byte[] _data;

        /// <summary>The new entry.</summary>
        private MemoryStream _newEntry;

        /// <summary>The read value start.</summary>
        private int _readValueStart;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipExtraData" /> class.</summary>
        public ZipExtraData()
        {
            Clear();
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipExtraData" /> class.</summary>
        /// <param name="data">The data.</param>
        public ZipExtraData(byte[] data)
        {
            if (data == null)
            {
                _data = Array.Empty<byte>();
            }
            else
            {
                _data = data;
            }
        }

        /// <summary>Gets the current read index.</summary>
        /// <value>The current read index.</value>
        public int CurrentReadIndex { get; private set; }

        /// <summary>Gets the length.</summary>
        /// <value>The length.</value>
        public int Length => _data.Length;

        /// <summary>Gets the number of unreads.</summary>
        /// <value>The number of unreads.</value>
        public int UnreadCount
        {
            get
            {
                if (_readValueStart > _data.Length || _readValueStart < 4)
                {
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw new ZipException("Find must be called before calling a Read method");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                }
                return _readValueStart + ValueLength - CurrentReadIndex;
            }
        }

        /// <summary>Gets the length of the value.</summary>
        /// <value>The length of the value.</value>
        public int ValueLength { get; private set; }

        /// <summary>Adds a data.</summary>
        /// <param name="data">The data.</param>
        public void AddData(byte data)
        {
            _newEntry.WriteByte(data);
        }

        /// <summary>Adds a data.</summary>
        /// <param name="data">The data.</param>
        public void AddData(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            _newEntry.Write(data, 0, data.Length);
        }

        /// <summary>Adds an entry.</summary>
        /// <param name="taggedData">Information describing the tagged.</param>
        public void AddEntry(ITaggedData taggedData)
        {
            if (taggedData == null)
            {
                throw new ArgumentNullException(nameof(taggedData));
            }
            AddEntry(taggedData.TagID, taggedData.GetData());
        }

        /// <summary>Adds an entry to 'fieldData'.</summary>
        /// <param name="headerID"> Identifier for the header.</param>
        /// <param name="fieldData">Information describing the field.</param>
        public void AddEntry(int headerID, byte[] fieldData)
        {
            if (headerID > ushort.MaxValue || headerID < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(headerID));
            }
            var source = fieldData == null ? 0 : fieldData.Length;
            if (source > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(fieldData), "exceeds maximum length");
            }
            var length1 = _data.Length + source + 4;
            if (Find(headerID))
            {
                length1 -= ValueLength + 4;
            }
            if (length1 > ushort.MaxValue)
            {
                throw new ZipException("Data exceeds maximum length");
            }
            Delete(headerID);
            var numArray = new byte[length1];
            _data.CopyTo(numArray, 0);
            var length2 = _data.Length;
            _data = numArray;
            SetShort(ref length2, headerID);
            SetShort(ref length2, source);
            fieldData?.CopyTo(numArray, length2);
        }

        /// <summary>Adds a le int.</summary>
        /// <param name="toAdd">to add.</param>
        public void AddLeInt(int toAdd)
        {
            AddLeShort((short)toAdd);
            AddLeShort((short)(toAdd >> 16));
        }

        /// <summary>Adds a le long.</summary>
        /// <param name="toAdd">to add.</param>
        public void AddLeLong(long toAdd)
        {
            AddLeInt((int)(toAdd & uint.MaxValue));
            AddLeInt((int)(toAdd >> 32));
        }

        /// <summary>Adds a le short.</summary>
        /// <param name="toAdd">to add.</param>
        public void AddLeShort(int toAdd)
        {
            _newEntry.WriteByte((byte)toAdd);
            _newEntry.WriteByte((byte)(toAdd >> 8));
        }

        /// <summary>Adds a new entry.</summary>
        /// <param name="headerID">Identifier for the header.</param>
        public void AddNewEntry(int headerID)
        {
            var array = _newEntry.ToArray();
            _newEntry = null;
            AddEntry(headerID, array);
        }

        /// <summary>Clears this ZipExtraData to its blank/initial state.</summary>
        public void Clear()
        {
            if (_data != null && _data.Length == 0)
            {
                return;
            }
            _data = Array.Empty<byte>();
        }

        /// <summary>Deletes the given headerID.</summary>
        /// <param name="headerID">The header Identifier to delete.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Delete(int headerID)
        {
            var flag = false;
            if (Find(headerID))
            {
                flag = true;
                var num = _readValueStart - 4;
                var numArray = new byte[_data.Length - (ValueLength + 4)];
                Array.Copy(_data, 0, numArray, 0, num);
                var sourceIndex = num + ValueLength + 4;
                Array.Copy(_data, sourceIndex, numArray, num, _data.Length - sourceIndex);
                _data = numArray;
            }
            return flag;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _newEntry?.Close();
        }

        /// <summary>Searches for the first match for the given header identifier.</summary>
        /// <param name="headerID">Identifier for the header.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public bool Find(int headerID)
        {
            _readValueStart = _data.Length;
            ValueLength = 0;
            CurrentReadIndex = 0;
            var num1 = _readValueStart;
            var num2 = headerID - 1;
            while (num2 != headerID && CurrentReadIndex < _data.Length - 3)
            {
                num2 = ReadShortInternal();
                num1 = ReadShortInternal();
                if (num2 != headerID)
                {
                    CurrentReadIndex += num1;
                }
            }
            var flag = num2 == headerID && CurrentReadIndex + num1 <= _data.Length;
            if (flag)
            {
                _readValueStart = CurrentReadIndex;
                ValueLength = num1;
            }
            return flag;
        }

        /// <summary>Gets entry data.</summary>
        /// <returns>An array of byte.</returns>
        public byte[] GetEntryData()
        {
            if (Length > ushort.MaxValue)
            {
                throw new ZipException("Data exceeds maximum length");
            }
            return (byte[])_data.Clone();
        }

        /// <summary>Gets stream for tag.</summary>
        /// <param name="tag">The tag.</param>
        /// <returns>The stream for tag.</returns>
        public Stream GetStreamForTag(int tag)
        {
            Stream stream = null;
            if (Find(tag))
            {
                stream = new MemoryStream(_data, CurrentReadIndex, ValueLength, false);
            }
            return stream;
        }

        /// <summary>Reads the byte.</summary>
        /// <returns>The byte.</returns>
        public int ReadByte()
        {
            var num = -1;
            if (CurrentReadIndex < _data.Length && _readValueStart + ValueLength > CurrentReadIndex)
            {
                num = _data[CurrentReadIndex];
                ++CurrentReadIndex;
            }
            return num;
        }

        /// <summary>Reads the int.</summary>
        /// <returns>The int.</returns>
        public int ReadInt()
        {
            ReadCheck(4);
            var num = _data[CurrentReadIndex] + (_data[CurrentReadIndex + 1] << 8) + (_data[CurrentReadIndex + 2] << 16) + (_data[CurrentReadIndex + 3] << 24);
            CurrentReadIndex += 4;
            return num;
        }

        /// <summary>Reads the long.</summary>
        /// <returns>The long.</returns>
        public long ReadLong()
        {
            ReadCheck(8);
            return (ReadInt() & uint.MaxValue) | ((long)ReadInt() << 32);
        }

        /// <summary>Reads the short.</summary>
        /// <returns>The short.</returns>
        public int ReadShort()
        {
            ReadCheck(2);
            var num = _data[CurrentReadIndex] + (_data[CurrentReadIndex + 1] << 8);
            CurrentReadIndex += 2;
            return num;
        }

        /// <summary>Skips.</summary>
        /// <param name="amount">The amount.</param>
        public void Skip(int amount)
        {
            ReadCheck(amount);
            CurrentReadIndex += amount;
        }

        /// <summary>Starts new entry.</summary>
        public void StartNewEntry()
        {
            _newEntry = new MemoryStream();
        }

        /// <summary>Creates a new ITaggedData.</summary>
        /// <param name="tag">   The tag.</param>
        /// <param name="data">  The data.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="count"> Number of.</param>
        /// <returns>An ITaggedData.</returns>
        private static ITaggedData Create(short tag, byte[] data, int offset, int count)
        {
            ITaggedData taggedData = tag switch
            {
                10 => new NTTaggedData(),
                21589 => new ExtendedUnixData(),
                _ => new RawTaggedData(tag),
            };
            taggedData.SetData(data, offset, count);
            return taggedData;
        }

        /// <summary>Gets a data.</summary>
        /// <param name="tag">The tag.</param>
        /// <returns>The data.</returns>
        private ITaggedData GetData(short tag)
        {
            ITaggedData taggedData = null;
            if (Find(tag))
            {
                taggedData = Create(tag, _data, _readValueStart, ValueLength);
            }
            return taggedData;
        }

        /// <summary>Reads a check.</summary>
        /// <param name="length">The length.</param>
        private void ReadCheck(int length)
        {
            if (_readValueStart > _data.Length || _readValueStart < 4)
            {
                throw new ZipException("Find must be called before calling a Read method");
            }
            if (CurrentReadIndex > _readValueStart + ValueLength - length)
            {
                throw new ZipException("End of extra data");
            }
            if (CurrentReadIndex + length < 4)
            {
                throw new ZipException("Cannot read before start of tag");
            }
        }

        /// <summary>Reads short internal.</summary>
        /// <returns>The short internal.</returns>
        private int ReadShortInternal()
        {
            if (CurrentReadIndex > _data.Length - 2)
            {
                throw new ZipException("End of extra data");
            }
            var num = _data[CurrentReadIndex] + (_data[CurrentReadIndex + 1] << 8);
            CurrentReadIndex += 2;
            return num;
        }

        /// <summary>Sets a short.</summary>
        /// <param name="index"> Zero-based index of the.</param>
        /// <param name="source">Source for the.</param>
        private void SetShort(ref int index, int source)
        {
            _data[index] = (byte)source;
            _data[index + 1] = (byte)(source >> 8);
            index += 2;
        }
    }
}
