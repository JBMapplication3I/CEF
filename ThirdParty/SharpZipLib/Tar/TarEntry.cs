// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarEntry
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;

    /// <summary>A tar entry.</summary>
    /// <seealso cref="ICloneable" />
    public class TarEntry : ICloneable
    {

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarEntry" /> class.</summary>
        /// <param name="headerBuffer">Buffer for header data.</param>
        public TarEntry(byte[] headerBuffer)
        {
            TarHeader = new TarHeader();
            TarHeader.ParseBuffer(headerBuffer);
        }

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Tar.TarEntry" /> class.</summary>
        /// <param name="header">The header.</param>
        public TarEntry(TarHeader header)
        {
            this.TarHeader = header != null ? (TarHeader)header.Clone() : throw new ArgumentNullException(nameof(header));
        }

        /// <summary>Prevents a default instance of the ICSharpCode.SharpZipLib.Tar.TarEntry class from being created.</summary>
        private TarEntry()
        {
            TarHeader = new TarHeader();
        }

        /// <summary>Gets the file.</summary>
        /// <value>The file.</value>
        public string File { get; private set; }

        /// <summary>Gets or sets the identifier of the group.</summary>
        /// <value>The identifier of the group.</value>
        public int GroupId
        {
            get => TarHeader.GroupId;
            set => TarHeader.GroupId = value;
        }

        /// <summary>Gets or sets the name of the group.</summary>
        /// <value>The name of the group.</value>
        public string GroupName
        {
            get => TarHeader.GroupName;
            set => TarHeader.GroupName = value;
        }

        /// <summary>Gets a value indicating whether this TarEntry is directory.</summary>
        /// <value>True if this TarEntry is directory, false if not.</value>
        public bool IsDirectory
        {
            get
            {
                if (File != null)
                {
                    return Directory.Exists(File);
                }
                return TarHeader != null && (TarHeader.TypeFlag == 53 || Name.EndsWith("/"));
            }
        }

        /// <summary>Gets or sets the modifier time.</summary>
        /// <value>The modifier time.</value>
        public DateTime ModTime
        {
            get => TarHeader.ModTime;
            set => TarHeader.ModTime = value;
        }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name
        {
            get => TarHeader.Name;
            set => TarHeader.Name = value;
        }

        /// <summary>Gets or sets the size.</summary>
        /// <value>The size.</value>
        public long Size
        {
            get => TarHeader.Size;
            set => TarHeader.Size = value;
        }

        /// <summary>Gets the tar header.</summary>
        /// <value>The tar header.</value>
        public TarHeader TarHeader { get; private set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        public int UserId
        {
            get => TarHeader.UserId;
            set => TarHeader.UserId = value;
        }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get => TarHeader.UserName;
            set => TarHeader.UserName = value;
        }

        /// <summary>Adjust entry name.</summary>
        /// <param name="buffer"> The buffer.</param>
        /// <param name="newName">Name of the new.</param>
        public static void AdjustEntryName(byte[] buffer, string newName)
        {
            TarHeader.GetNameBytes(newName, buffer, 0, 100);
        }

        /// <summary>Creates entry from file.</summary>
        /// <param name="fileName">Filename of the file.</param>
        /// <returns>The new entry from file.</returns>
        public static TarEntry CreateEntryFromFile(string fileName)
        {
            var tarEntry = new TarEntry();
            tarEntry.GetFileTarHeader(tarEntry.TarHeader, fileName);
            return tarEntry;
        }

        /// <summary>Creates tar entry.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The new tar entry.</returns>
        public static TarEntry CreateTarEntry(string name)
        {
            var tarEntry = new TarEntry();
            NameTarHeader(tarEntry.TarHeader, name);
            return tarEntry;
        }

        /// <summary>Name tar header.</summary>
        /// <param name="header">The header.</param>
        /// <param name="name">  The name.</param>
        public static void NameTarHeader(TarHeader header, string name)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }
            var flag = name != null ? name.EndsWith("/") : throw new ArgumentNullException(nameof(name));
            header.Name = name;
            header.Mode = flag ? 1003 : 33216;
            header.UserId = 0;
            header.GroupId = 0;
            header.Size = 0L;
            header.ModTime = DateTime.UtcNow;
            header.TypeFlag = flag ? (byte)53 : (byte)48;
            header.LinkName = string.Empty;
            header.UserName = string.Empty;
            header.GroupName = string.Empty;
            header.DevMajor = 0;
            header.DevMinor = 0;
        }

        /// <inheritdoc/>
        public object Clone()
        {
            return new TarEntry { File = File, TarHeader = (TarHeader)TarHeader.Clone(), Name = Name };
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is TarEntry tarEntry && Name.Equals(tarEntry.Name);
        }

        /// <summary>Gets directory entries.</summary>
        /// <returns>An array of tar entry.</returns>
        public TarEntry[] GetDirectoryEntries()
        {
            if (File == null || !Directory.Exists(File))
            {
                return Array.Empty<TarEntry>();
            }
            var fileSystemEntries = Directory.GetFileSystemEntries(File);
            var tarEntryArray = new TarEntry[fileSystemEntries.Length];
            for (var index = 0; index < fileSystemEntries.Length; ++index)
            {
                tarEntryArray[index] = CreateEntryFromFile(fileSystemEntries[index]);
            }
            return tarEntryArray;
        }

        /// <summary>Gets file tar header.</summary>
        /// <param name="header">The header.</param>
        /// <param name="file">  The file.</param>
        public void GetFileTarHeader(TarHeader header, string file)
        {
            if (header == null)
            {
                throw new ArgumentNullException(nameof(header));
            }
            this.File = file ?? throw new ArgumentNullException(nameof(file));
            var str1 = file;
            if (str1.IndexOf(Environment.CurrentDirectory) == 0)
            {
                str1 = str1[Environment.CurrentDirectory.Length..];
            }
            var str2 = str1.Replace(Path.DirectorySeparatorChar, '/');
            while (str2.StartsWith("/"))
            {
                str2 = str2[1..];
            }
            header.LinkName = string.Empty;
            header.Name = str2;
            if (Directory.Exists(file))
            {
                header.Mode = 1003;
                header.TypeFlag = 53;
                if (header.Name.Length == 0 || header.Name[^1] != '/')
                {
                    header.Name += "/";
                }
                header.Size = 0L;
            }
            else
            {
                header.Mode = 33216;
                header.TypeFlag = 48;
                header.Size = new FileInfo(file.Replace('/', Path.DirectorySeparatorChar)).Length;
            }
            header.ModTime = System.IO.File.GetLastWriteTime(file.Replace('/', Path.DirectorySeparatorChar))
                .ToUniversalTime();
            header.DevMajor = 0;
            header.DevMinor = 0;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>Query if 'toTest' is descendent.</summary>
        /// <param name="toTest">to test.</param>
        /// <returns>True if descendent, false if not.</returns>
        public bool IsDescendent(TarEntry toTest)
        {
            return toTest != null ? toTest.Name.StartsWith(Name) : throw new ArgumentNullException(nameof(toTest));
        }

        /// <summary>Sets the identifiers.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="groupId">Identifier for the group.</param>
        public void SetIds(int userId, int groupId)
        {
            UserId = userId;
            GroupId = groupId;
        }

        /// <summary>Sets the names.</summary>
        /// <param name="userName"> Name of the user.</param>
        /// <param name="groupName">Name of the group.</param>
        public void SetNames(string userName, string groupName)
        {
            UserName = userName;
            GroupName = groupName;
        }

        /// <summary>Writes an entry header.</summary>
        /// <param name="outBuffer">Buffer for out data.</param>
        public void WriteEntryHeader(byte[] outBuffer)
        {
            TarHeader.WriteHeader(outBuffer);
        }
    }
}
