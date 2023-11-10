// Decompiled with JetBrains decompiler
// Type: Excel.Core.OpenXmlFormat.XlsxWorksheet
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.OpenXmlFormat
{
    /// <summary>An XLSX worksheet.</summary>
    internal class XlsxWorksheet
    {
        /// <summary>The r.</summary>
        public const string A_r = "r";

        /// <summary>The reference.</summary>
        public const string A_ref = "ref";

        /// <summary>The s.</summary>
        public const string A_s = "s";

        /// <summary>The t.</summary>
        public const string A_t = "t";

        /// <summary>The c.</summary>
        public const string N_c = "c";

        /// <summary>The col.</summary>
        public const string N_col = "col";

        /// <summary>The dimension.</summary>
        public const string N_dimension = "dimension";

        /// <summary>The inline string.</summary>
        public const string N_inlineStr = "inlineStr";

        /// <summary>The row.</summary>
        public const string N_row = "row";

        /// <summary>Information describing the sheet.</summary>
        public const string N_sheetData = "sheetData";

        /// <summary>The t.</summary>
        public const string N_t = "t";

        /// <summary>The v.</summary>
        public const string N_v = "v";

        /// <summary>The worksheet.</summary>
        public const string N_worksheet = "worksheet";

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.OpenXmlFormat.XlsxWorksheet"/> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="id">  The identifier.</param>
        /// <param name="rid"> The rid.</param>
        public XlsxWorksheet(string name, int id, string rid)
        {
            Name = name;
            Id = id;
            RID = rid;
        }

        /// <summary>Gets the number of columns.</summary>
        /// <value>The number of columns.</value>
        public int ColumnsCount
        {
            get
            {
                if (IsEmpty)
                {
                    return 0;
                }
                return Dimension != null ? Dimension.LastCol : -1;
            }
        }

        /// <summary>Gets or sets the dimension.</summary>
        /// <value>The dimension.</value>
        public XlsxDimension Dimension { get; set; }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; }

        /// <summary>Gets or sets a value indicating whether this XlsxWorksheet is empty.</summary>
        /// <value>True if this XlsxWorksheet is empty, false if not.</value>
        public bool IsEmpty { get; set; }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>Gets or sets the full pathname of the file.</summary>
        /// <value>The full pathname of the file.</value>
        public string Path { get; set; }

        /// <summary>Gets or sets the rid.</summary>
        /// <value>The rid.</value>
        public string RID { get; set; }

        /// <summary>Gets the number of rows.</summary>
        /// <value>The number of rows.</value>
        public int RowsCount => Dimension != null ? Dimension.LastRow - Dimension.FirstRow + 1 : -1;
    }
}
