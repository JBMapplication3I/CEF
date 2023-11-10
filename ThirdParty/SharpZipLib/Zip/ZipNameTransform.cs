// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipNameTransform
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;
    using System.Text;
    using Core;

    /// <summary>Form for viewing the zip name transaction.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Core.INameTransform" />
    public class ZipNameTransform : INameTransform
    {
        /// <summary>The invalid entry characters.</summary>
        private static readonly char[] InvalidEntryChars;

        /// <summary>The invalid entry characters relaxed.</summary>
        private static readonly char[] InvalidEntryCharsRelaxed;

        /// <summary>The trim prefix.</summary>
        private string trimPrefix_;

        /// <summary>Initializes static members of the ICSharpCode.SharpZipLib.Zip.ZipNameTransform class.</summary>
        static ZipNameTransform()
        {
            var invalidPathChars = Path.GetInvalidPathChars();
            var length1 = invalidPathChars.Length + 2;
            InvalidEntryCharsRelaxed = new char[length1];
            Array.Copy(invalidPathChars, 0, InvalidEntryCharsRelaxed, 0, invalidPathChars.Length);
            InvalidEntryCharsRelaxed[length1 - 1] = '*';
            InvalidEntryCharsRelaxed[length1 - 2] = '?';
            var length2 = invalidPathChars.Length + 4;
            InvalidEntryChars = new char[length2];
            Array.Copy(invalidPathChars, 0, InvalidEntryChars, 0, invalidPathChars.Length);
            InvalidEntryChars[length2 - 1] = ':';
            InvalidEntryChars[length2 - 2] = '\\';
            InvalidEntryChars[length2 - 3] = '*';
            InvalidEntryChars[length2 - 4] = '?';
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipNameTransform" />
        ///     class.
        /// </summary>
        public ZipNameTransform() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Zip.ZipNameTransform" />
        ///     class.
        /// </summary>
        /// <param name="trimPrefix">The trim prefix.</param>
        public ZipNameTransform(string trimPrefix)
        {
            TrimPrefix = trimPrefix;
        }

        /// <summary>Gets or sets the trim prefix.</summary>
        /// <value>The trim prefix.</value>
        public string TrimPrefix
        {
            get => trimPrefix_;
            set
            {
                trimPrefix_ = value;
                if (trimPrefix_ == null)
                {
                    return;
                }
                trimPrefix_ = trimPrefix_.ToLower();
            }
        }

        /// <summary>Query if 'name' is valid name.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="relaxed">True if relaxed.</param>
        /// <returns>True if valid name, false if not.</returns>
        public static bool IsValidName(string name, bool relaxed)
        {
            var flag = name != null;
            if (flag)
            {
                flag = !relaxed
                    ? name.IndexOfAny(InvalidEntryChars) < 0 && name.IndexOf('/') != 0
                    : name.IndexOfAny(InvalidEntryCharsRelaxed) < 0;
            }
            return flag;
        }

        /// <summary>Query if 'name' is valid name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>True if valid name, false if not.</returns>
        public static bool IsValidName(string name)
        {
            return name != null && name.IndexOfAny(InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
        }

        /// <inheritdoc/>
        public string TransformDirectory(string name)
        {
            name = TransformFile(name);
            if (name.Length <= 0)
            {
                throw new ZipException("Cannot have an empty directory name");
            }
            if (!name.EndsWith("/"))
            {
                name += "/";
            }
            return name;
        }

        /// <inheritdoc/>
        public string TransformFile(string name)
        {
            if (name != null)
            {
                var lower = name.ToLower();
                if (trimPrefix_ != null && lower.IndexOf(trimPrefix_) == 0)
                {
                    name = name[trimPrefix_.Length..];
                }
                name = name.Replace("\\", "/");
                name = WindowsPathUtils.DropPathRoot(name);
                while (name.Length > 0 && name[0] == '/')
                {
                    name = name.Remove(0, 1);
                }
                while (name.Length > 0 && name[^1] == '/')
                {
                    name = name.Remove(name.Length - 1, 1);
                }
                for (var startIndex = name.IndexOf("//"); startIndex >= 0; startIndex = name.IndexOf("//"))
                {
                    name = name.Remove(startIndex, 1);
                }
                name = MakeValidName(name, '_');
            }
            else
            {
                name = string.Empty;
            }
            return name;
        }

        /// <summary>Makes valid name.</summary>
        /// <param name="name">       The name.</param>
        /// <param name="replacement">The replacement.</param>
        /// <returns>A string.</returns>
        private static string MakeValidName(string name, char replacement)
        {
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
            return name.Length <= (int)ushort.MaxValue ? name : throw new PathTooLongException();
        }
    }
}
