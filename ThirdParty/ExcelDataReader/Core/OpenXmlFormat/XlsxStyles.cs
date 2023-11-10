// Decompiled with JetBrains decompiler
// Type: Excel.Core.OpenXmlFormat.XlsxStyles
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.OpenXmlFormat
{
    using System.Collections.Generic;

    /// <summary>An XLSX styles.</summary>
    internal class XlsxStyles
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.OpenXmlFormat.XlsxStyles"/> class.</summary>
        public XlsxStyles()
        {
            CellXfs = new List<XlsxXf>();
            NumFmts = new List<XlsxNumFmt>();
        }

        /// <summary>Gets or sets the cell xfs.</summary>
        /// <value>The cell xfs.</value>
        public List<XlsxXf> CellXfs { get; set; }

        /// <summary>Gets or sets the number of fmts.</summary>
        /// <value>The total number of fmts.</value>
        public List<XlsxNumFmt> NumFmts { get; set; }
    }
}
