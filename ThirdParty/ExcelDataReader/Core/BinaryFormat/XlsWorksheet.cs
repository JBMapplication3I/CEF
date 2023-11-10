// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsWorksheet
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    /// <summary>The XLS worksheet.</summary>
    internal class XlsWorksheet
    {
        /// <summary>Initializes a new instance of the <see cref="Excel.Core.BinaryFormat.XlsWorksheet"/> class.</summary>
        /// <param name="index">   Zero-based index of the.</param>
        /// <param name="refSheet">The reference sheet.</param>
        public XlsWorksheet(int index, XlsBiffBoundSheet refSheet)
        {
            Index = index;
            Name = refSheet.SheetName;
            DataOffset = refSheet.StartOffset;
        }

        /// <summary>Gets or sets the number of calculates.</summary>
        /// <value>The number of calculates.</value>
        public XlsBiffSimpleValueRecord CalcCount { get; set; }

        /// <summary>Gets or sets the calculate mode.</summary>
        /// <value>The calculate mode.</value>
        public XlsBiffSimpleValueRecord CalcMode { get; set; }

        /// <summary>Gets the data offset.</summary>
        /// <value>The data offset.</value>
        public uint DataOffset { get; }

        /// <summary>Gets or sets the delta.</summary>
        /// <value>The delta.</value>
        public XlsBiffRecord Delta { get; set; }

        /// <summary>Gets or sets the dimensions.</summary>
        /// <value>The dimensions.</value>
        public XlsBiffDimensions Dimensions { get; set; }

        /// <summary>Gets the zero-based index of this XlsWorksheet.</summary>
        /// <value>The index.</value>
        public int Index { get; }

        /// <summary>Gets or sets the iteration.</summary>
        /// <value>The iteration.</value>
        public XlsBiffSimpleValueRecord Iteration { get; set; }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; } = string.Empty;

        /// <summary>Gets or sets the reference mode.</summary>
        /// <value>The reference mode.</value>
        public XlsBiffSimpleValueRecord RefMode { get; set; }

        /// <summary>Gets or sets the window.</summary>
        /// <value>The window.</value>
        public XlsBiffRecord Window { get; set; }
    }
}
