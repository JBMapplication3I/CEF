// Decompiled with JetBrains decompiler
// Type: Excel.Core.OpenXmlFormat.XlsxNumFmt
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.OpenXmlFormat
{
    /// <summary>An XLSX number format.</summary>
    internal class XlsxNumFmt
    {
        /// <summary>The format code.</summary>
        public const string A_formatCode = "formatCode";

        /// <summary>Number of format identifiers.</summary>
        public const string A_numFmtId = "numFmtId";

        /// <summary>Number of formats.</summary>
        public const string N_numFmt = "numFmt";

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.OpenXmlFormat.XlsxNumFmt"/> class.</summary>
        /// <param name="id">        The identifier.</param>
        /// <param name="formatCode">The format code.</param>
        public XlsxNumFmt(int id, string formatCode)
        {
            Id = id;
            FormatCode = formatCode;
        }

        /// <summary>Gets or sets the format code.</summary>
        /// <value>The format code.</value>
        public string FormatCode { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
    }
}
