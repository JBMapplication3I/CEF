// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.WindowsNameTransform
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;
    using System.Text;
    using Core;

    /// <summary>Form for viewing the windows name transaction.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Core.INameTransform" />
    public class WindowsNameTransform : INameTransform
    {
        /// <summary>Full pathname of the maximum file.</summary>
        private const int MaxPath = 260;

        /// <summary>The invalid entry characters.</summary>
        private static readonly char[] InvalidEntryChars;

        /// <summary>Pathname of the base directory.</summary>
        private string _baseDirectory;

        /// <summary>The replacement character.</summary>
        private char _replacementChar = '_';

        /// <summary>Initializes static members of the ICSharpCode.SharpZipLib.Zip.WindowsNameTransform class.</summary>
        static WindowsNameTransform()
        {
            var invalidPathChars = Path.GetInvalidPathChars();
            var length = invalidPathChars.Length + 3;
            InvalidEntryChars = new char[length];
            Array.Copy(invalidPathChars, 0, InvalidEntryChars, 0, invalidPathChars.Length);
            InvalidEntryChars[length - 1] = '*';
            InvalidEntryChars[length - 2] = '?';
            InvalidEntryChars[length - 3] = ':';
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.WindowsNameTransform" />
        ///     class.
        /// </summary>
        /// <param name="baseDirectory">Pathname of the base directory.</param>
        public WindowsNameTransform(string baseDirectory)
        {
            BaseDirectory = baseDirectory ?? throw new ArgumentNullException(nameof(baseDirectory), "Directory name is invalid");
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.WindowsNameTransform" />
        ///     class.
        /// </summary>
        public WindowsNameTransform() { }

        /// <summary>Gets or sets the pathname of the base directory.</summary>
        /// <value>The pathname of the base directory.</value>
        public string BaseDirectory
        {
            get => _baseDirectory;
            set
                => _baseDirectory =
                    value != null ? Path.GetFullPath(value) : throw new ArgumentNullException(nameof(value));
        }

        /// <summary>Gets or sets the replacement.</summary>
        /// <value>The replacement.</value>
        public char Replacement
        {
            get => _replacementChar;
            set
            {
                for (var index = 0; index < InvalidEntryChars.Length; ++index)
                {
                    if (InvalidEntryChars[index] == value)
                    {
                        throw new ArgumentException("invalid path character");
                    }
                }
                _replacementChar = value != '\\' && value != '/'
                    ? value
                    : throw new ArgumentException("invalid replacement character");
            }
        }

        /// <summary>Gets or sets a value indicating whether the trim incoming paths.</summary>
        /// <value>True if trim incoming paths, false if not.</value>
        public bool TrimIncomingPaths { get; set; }

        /// <summary>Query if 'name' is valid name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>True if valid name, false if not.</returns>
        public static bool IsValidName(string name)
        {
            return name != null && name.Length <= 260 && string.Compare(name, MakeValidName(name, '_')) == 0;
        }

        /// <summary>Makes valid name.</summary>
        /// <param name="name">       The name.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns>A string.</returns>
        public static string MakeValidName(string name, char replacement)
        {
            name = name != null
                ? WindowsPathUtils.DropPathRoot(name.Replace("/", "\\"))
                : throw new ArgumentNullException(nameof(name));
            while (name.Length > 0 && name[0] == '\\')
            {
                name = name.Remove(0, 1);
            }
            while (name.Length > 0 && name[^1] == '\\')
            {
                name = name.Remove(name.Length - 1, 1);
            }
            for (var startIndex = name.IndexOf("\\\\"); startIndex >= 0; startIndex = name.IndexOf("\\\\"))
            {
                name = name.Remove(startIndex, 1);
            }
            var index = name.IndexOfAny(InvalidEntryChars);
            if (index >= 0)
            {
                var stringBuilder = new StringBuilder(name);
                for (; index >= 0; index = index < name.Length ? name.IndexOfAny(InvalidEntryChars, index + 1) : -1)
                {
                    stringBuilder[index] = replacement;
                }
                name = stringBuilder.ToString();
            }
            return name.Length <= 260 ? name : throw new PathTooLongException();
        }

        /// <inheritdoc/>
        public string TransformDirectory(string name)
        {
            name = TransformFile(name);
            if (name.Length <= 0)
            {
                throw new ZipException("Cannot have an empty directory name");
            }
            while (name.EndsWith("\\"))
            {
                name = name.Remove(name.Length - 1, 1);
            }
            return name;
        }

        /// <inheritdoc/>
        public string TransformFile(string name)
        {
            if (name != null)
            {
                name = MakeValidName(name, _replacementChar);
                if (TrimIncomingPaths)
                {
                    name = Path.GetFileName(name);
                }
                if (_baseDirectory != null)
                {
                    name = Path.Combine(_baseDirectory, name);
                }
            }
            else
            {
                name = string.Empty;
            }
            return name;
        }
    }
}
