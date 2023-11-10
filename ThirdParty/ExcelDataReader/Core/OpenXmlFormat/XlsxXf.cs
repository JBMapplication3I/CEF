// Decompiled with JetBrains decompiler
// Type: Excel.Core.OpenXmlFormat.XlsxXf
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.OpenXmlFormat
{
    /// <summary>An XLSX xf.</summary>
    internal class XlsxXf
    {
        /// <summary>The apply number format.</summary>
        public const string A_applyNumberFormat = "applyNumberFormat";

        /// <summary>Number of format identifiers.</summary>
        public const string A_numFmtId = "numFmtId";

        /// <summary>Identifier for the xf.</summary>
        public const string A_xfId = "xfId";

        /// <summary>The xf.</summary>
        public const string N_xf = "xf";

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.OpenXmlFormat.XlsxXf"/> class.</summary>
        /// <param name="id">               The identifier.</param>
        /// <param name="numFmtId">         Number of format identifiers.</param>
        /// <param name="applyNumberFormat">The apply number format.</param>
        public XlsxXf(int id, int numFmtId, string applyNumberFormat)
        {
            Id = id;
            NumFmtId = numFmtId;
            ApplyNumberFormat = applyNumberFormat != null && applyNumberFormat == "1";
        }

        /// <summary>Gets or sets a value indicating whether the apply number format.</summary>
        /// <value>True if apply number format, false if not.</value>
        public bool ApplyNumberFormat { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>Gets or sets the number of format identifiers.</summary>
        /// <value>The total number of format identifier.</value>
        public int NumFmtId { get; set; }
    }
}
