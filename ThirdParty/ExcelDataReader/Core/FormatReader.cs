// Decompiled with JetBrains decompiler
// Type: Excel.Core.FormatReader
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core
{
    /// <summary>A format reader.</summary>
    public class FormatReader
    {
        /// <summary>.</summary>
        private const char escapeChar = '\\';

        /// <summary>Gets or sets the format string.</summary>
        /// <value>The format string.</value>
        public string FormatString { get; set; }

        /// <summary>Query if this FormatReader is date format string.</summary>
        /// <returns>True if date format string, false if not.</returns>
        public bool IsDateFormatString()
        {
            var anyOf = new char[10] { 'y', 'm', 'd', 's', 'h', 'Y', 'M', 'D', 'S', 'H' };
            if (FormatString.IndexOfAny(anyOf) < 0)
            {
                return false;
            }
            foreach (var dateChar in anyOf)
            {
                for (var pos = FormatString.IndexOf(dateChar);
                     pos > -1;
                     pos = FormatString.IndexOf(dateChar, pos + 1))
                {
                    if (!IsSurroundedByBracket(dateChar, pos)
                        && !IsPrecededByBackSlash(dateChar, pos)
                        && !IsSurroundedByQuotes(dateChar, pos))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>Query if 'dateChar' is preceded by back slash.</summary>
        /// <param name="dateChar">The date character.</param>
        /// <param name="pos">     The position.</param>
        /// <returns>True if preceded by back slash, false if not.</returns>
        private bool IsPrecededByBackSlash(char dateChar, int pos)
        {
            return pos != 0 && FormatString[pos - 1].CompareTo('\\') == 0;
        }

        /// <summary>Query if 'dateChar' is surrounded by bracket.</summary>
        /// <param name="dateChar">The date character.</param>
        /// <param name="pos">     The position.</param>
        /// <returns>True if surrounded by bracket, false if not.</returns>
        private bool IsSurroundedByBracket(char dateChar, int pos)
        {
            if (pos == FormatString.Length - 1)
            {
                return false;
            }
            var num1 = NumberOfUnescapedOccurances('[', FormatString.Substring(0, pos))
                - NumberOfUnescapedOccurances(']', FormatString.Substring(0, pos));
            var num2 = NumberOfUnescapedOccurances('[', FormatString[(pos + 1)..]);
            var num3 = NumberOfUnescapedOccurances(']', FormatString[(pos + 1)..]) - num2;
            return num1 % 2 == 1 && num3 % 2 == 1;
        }

        /// <summary>Query if 'dateChar' is surrounded by quotes.</summary>
        /// <param name="dateChar">The date character.</param>
        /// <param name="pos">     The position.</param>
        /// <returns>True if surrounded by quotes, false if not.</returns>
        private bool IsSurroundedByQuotes(char dateChar, int pos)
        {
            if (pos == FormatString.Length - 1)
            {
                return false;
            }
            var num1 = NumberOfUnescapedOccurances('"', FormatString[(pos + 1)..]);
            var num2 = NumberOfUnescapedOccurances('"', FormatString.Substring(0, pos));
            return num1 % 2 == 1 && num2 % 2 == 1;
        }

        /// <summary>Number of unescaped occurances.</summary>
        /// <param name="value">The value.</param>
        /// <param name="src">  Source for the.</param>
        /// <returns>The total number of unescaped occurances.</returns>
        private int NumberOfUnescapedOccurances(char value, string src)
        {
            var num = 0;
            var ch1 = char.MinValue;
            foreach (var ch2 in src)
            {
                if (ch2 == value && (ch1 == char.MinValue || ch1.CompareTo('\\') != 0))
                {
                    ++num;
                    ch1 = ch2;
                }
            }
            return num;
        }
    }
}
