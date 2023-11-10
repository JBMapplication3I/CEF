// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.WindowsPathUtils
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>The windows path utilities.</summary>
    public abstract class WindowsPathUtils
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.WindowsPathUtils" />
        ///     class.
        /// </summary>
        internal WindowsPathUtils() { }

        /// <summary>Drop path root.</summary>
        /// <param name="path">Full pathname of the file.</param>
        /// <returns>A string.</returns>
        public static string DropPathRoot(string path)
        {
            var str = path;
            if (path != null && path.Length > 0)
            {
                if (path[0] == '\\' || path[0] == '/')
                {
                    if (path.Length > 1 && (path[1] == '\\' || path[1] == '/'))
                    {
                        var index = 2;
                        var num = 2;
                        while (index <= path.Length && (path[index] != '\\' && path[index] != '/' || --num > 0))
                        {
                            ++index;
                        }

                        var startIndex = index + 1;
                        str = startIndex >= path.Length ? string.Empty : path[startIndex..];
                    }
                }
                else if (path.Length > 1 && path[1] == ':')
                {
                    var count = 2;
                    if (path.Length > 2 && (path[2] == '\\' || path[2] == '/'))
                    {
                        count = 3;
                    }

                    str = str.Remove(0, count);
                }
            }

            return str;
        }
    }
}