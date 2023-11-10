// Decompiled with JetBrains decompiler
// Type: Excel.Core.OpenXmlFormat.XlsxDimension
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.OpenXmlFormat
{
    using System;

    /// <summary>An XLSX dimension.</summary>
    internal class XlsxDimension
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.OpenXmlFormat.XlsxDimension"/> class.</summary>
        /// <param name="value">The value.</param>
        public XlsxDimension(string value)
        {
            ParseDimensions(value);
        }

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.OpenXmlFormat.XlsxDimension"/> class.</summary>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public XlsxDimension(int rows, int cols)
        {
            FirstRow = 1;
            LastRow = rows;
            FirstCol = 1;
            LastCol = cols;
        }

        /// <summary>Gets or sets the first col.</summary>
        /// <value>The first col.</value>
        public int FirstCol { get; set; }

        /// <summary>Gets or sets the first row.</summary>
        /// <value>The first row.</value>
        public int FirstRow { get; set; }

        /// <summary>Gets or sets the last col.</summary>
        /// <value>The last col.</value>
        public int LastCol { get; set; }

        /// <summary>Gets or sets the last row.</summary>
        /// <value>The last row.</value>
        public int LastRow { get; set; }

        /// <summary>XLSX dim.</summary>
        /// <param name="value">The value.</param>
        /// <param name="val1"> The first value.</param>
        /// <param name="val2"> The second value.</param>
        public static void XlsxDim(string value, out int val1, out int val2)
        {
            var index1 = 0;
            val1 = 0;
            var numArray = new int[value.Length - 1];
            for (; index1 < value.Length && !char.IsDigit(value[index1]); ++index1)
            {
                numArray[index1] = value[index1] - 65 + 1;
            }
            for (var index2 = 0; index2 < index1; ++index2)
            {
                val1 += (int)(numArray[index2] * Math.Pow(26.0, index1 - index2 - 1));
            }
            val2 = int.Parse(value[index1..]);
        }

        /// <summary>Parse dimensions.</summary>
        /// <param name="value">The value.</param>
        public void ParseDimensions(string value)
        {
            var strArray = value.Split(':');
            XlsxDim(strArray[0], out var val1, out var val2);
            FirstCol = val1;
            FirstRow = val2;
            if (strArray.Length == 1)
            {
                LastCol = FirstCol;
                LastRow = FirstRow;
            }
            else
            {
                XlsxDim(strArray[1], out val1, out val2);
                LastCol = val1;
                LastRow = val2;
            }
        }
    }
}
