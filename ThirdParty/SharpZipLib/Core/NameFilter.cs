// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.NameFilter
// Assembly: ICSharpCode.SharpZipLib, Version=0.86.0.518, Culture=neutral, PublicKeyToken=1b03e6acf1164f73
// MVID: 8E8FA28D-216A-43EC-8DCB-2258D1F7BF00
// Assembly location: C:\Users\jotha\.nuget\packages\sharpziplib\0.86.0\lib\20\ICSharpCode.SharpZipLib.dll

namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>A name filter.</summary>
    /// <seealso cref="ICSharpCode.SharpZipLib.Core.IScanFilter" />
    public class NameFilter : IScanFilter
    {
        /// <summary>The exclusions.</summary>
        private readonly ArrayList exclusions_;

        /// <summary>Specifies the filter.</summary>
        private readonly string filter_;

        /// <summary>The inclusions.</summary>
        private readonly ArrayList inclusions_;

        /// <summary>Initializes a new instance of the <see cref="ICSharpCode.SharpZipLib.Core.NameFilter" /> class.</summary>
        /// <param name="filter">Specifies the filter.</param>
        public NameFilter(string filter)
        {
            this.filter_ = filter;
            this.inclusions_ = new ArrayList();
            this.exclusions_ = new ArrayList();
            this.Compile();
        }

        /// <summary>Query if 'expression' is valid expression.</summary>
        /// <param name="expression">The expression.</param>
        /// <returns>True if valid expression, false if not.</returns>
        public static bool IsValidExpression(string expression)
        {
            var flag = true;
            try
            {
                var regex = new Regex(expression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            catch (ArgumentException)
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>Query if 'toTest' is valid filter expression.</summary>
        /// <param name="toTest">to test.</param>
        /// <returns>True if valid filter expression, false if not.</returns>
        public static bool IsValidFilterExpression(string toTest)
        {
            if (toTest == null)
            {
                throw new ArgumentNullException(nameof(toTest));
            }

            var flag = true;
            try
            {
                var strArray = SplitQuoted(toTest);
                for (var index = 0; index < strArray.Length; ++index)
                {
                    if (strArray[index] != null && strArray[index].Length > 0)
                    {
                        var regex = new Regex(
                            strArray[index][0] != '+'
                                ? strArray[index][0] != '-' ? strArray[index] :
                                  strArray[index][1..]
                                : strArray[index][1..],
                            RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    }
                }
            }
            catch (ArgumentException)
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>Splits a quoted.</summary>
        /// <param name="original">The original.</param>
        /// <returns>A string[].</returns>
        public static string[] SplitQuoted(string original)
        {
            var ch = '\\';
            var array = new char[1] { ';' };
            var arrayList = new ArrayList();
            if (original != null && original.Length > 0)
            {
                var index = -1;
                var stringBuilder = new StringBuilder();
                while (index < original.Length)
                {
                    ++index;
                    if (index >= original.Length)
                    {
                        arrayList.Add(stringBuilder.ToString());
                    }
                    else if (original[index] == ch)
                    {
                        ++index;
                        if (index >= original.Length)
                        {
                            throw new ArgumentException("Missing terminating escape character", nameof(original));
                        }

                        if (Array.IndexOf(array, original[index]) < 0)
                        {
                            stringBuilder.Append(ch);
                        }

                        stringBuilder.Append(original[index]);
                    }
                    else if (Array.IndexOf(array, original[index]) >= 0)
                    {
                        arrayList.Add(stringBuilder.ToString());
                        stringBuilder.Length = 0;
                    }
                    else
                    {
                        stringBuilder.Append(original[index]);
                    }
                }
            }

            return (string[])arrayList.ToArray(typeof(string));
        }

        /// <summary>Query if 'name' is excluded.</summary>
        /// <param name="name">The name.</param>
        /// <returns>True if excluded, false if not.</returns>
        public bool IsExcluded(string name)
        {
            var flag = false;
            foreach (Regex exclusion in this.exclusions_)
            {
                if (exclusion.IsMatch(name))
                {
                    flag = true;
                    break;
                }
            }

            return flag;
        }

        /// <summary>Query if 'name' is included.</summary>
        /// <param name="name">The name.</param>
        /// <returns>True if included, false if not.</returns>
        public bool IsIncluded(string name)
        {
            var flag = false;
            if (this.inclusions_.Count == 0)
            {
                flag = true;
            }
            else
            {
                foreach (Regex inclusion in this.inclusions_)
                {
                    if (inclusion.IsMatch(name))
                    {
                        flag = true;
                        break;
                    }
                }
            }

            return flag;
        }

        /// <inheritdoc/>
        public bool IsMatch(string name)
        {
            return this.IsIncluded(name) && !this.IsExcluded(name);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.filter_;
        }

        /// <summary>Compiles this NameFilter.</summary>
        private void Compile()
        {
            if (this.filter_ == null)
            {
                return;
            }

            var strArray = SplitQuoted(this.filter_);
            for (var index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index] != null && strArray[index].Length > 0)
                {
                    var flag = strArray[index][0] != '-';
                    var pattern = strArray[index][0] != '+'
                                      ? strArray[index][0] != '-' ? strArray[index] :
                                        strArray[index][1..]
                                      : strArray[index][1..];
                    if (flag)
                    {
                        this.inclusions_.Add(
                            new Regex(
                                pattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
                    }
                    else
                    {
                        this.exclusions_.Add(
                            new Regex(
                                pattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline));
                    }
                }
            }
        }
    }
}