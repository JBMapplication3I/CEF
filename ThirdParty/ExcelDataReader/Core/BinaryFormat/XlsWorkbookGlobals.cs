// Decompiled with JetBrains decompiler
// Type: Excel.Core.BinaryFormat.XlsWorkbookGlobals
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.BinaryFormat
{
    using System.Collections.Generic;

    /// <summary>The XLS workbook globals.</summary>
    internal class XlsWorkbookGlobals
    {
        /// <summary>Gets or sets the backup.</summary>
        /// <value>The backup.</value>
        public XlsBiffSimpleValueRecord Backup { get; set; }

        /// <summary>Gets or sets the code page.</summary>
        /// <value>The code page.</value>
        public XlsBiffSimpleValueRecord CodePage { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        public XlsBiffRecord Country { get; set; }

        /// <summary>Gets or sets the dsf.</summary>
        /// <value>The dsf.</value>
        public XlsBiffRecord DSF { get; set; }

        /// <summary>Gets the extended formats.</summary>
        /// <value>The extended formats.</value>
        public List<XlsBiffRecord> ExtendedFormats { get; } = new List<XlsBiffRecord>();

        /// <summary>Gets or sets the extent sst.</summary>
        /// <value>The extent sst.</value>
        public XlsBiffRecord ExtSST { get; set; }

        /// <summary>Gets the fonts.</summary>
        /// <value>The fonts.</value>
        public List<XlsBiffRecord> Fonts { get; } = new List<XlsBiffRecord>();

        /// <summary>Gets the formats.</summary>
        /// <value>The formats.</value>
        public Dictionary<ushort, XlsBiffFormatString> Formats { get; } = new Dictionary<ushort, XlsBiffFormatString>();

        /// <summary>Gets or sets the interface header.</summary>
        /// <value>The interface header.</value>
        public XlsBiffInterfaceHdr InterfaceHdr { get; set; }

        /// <summary>Gets or sets the mms.</summary>
        /// <value>The mms.</value>
        public XlsBiffRecord MMS { get; set; }

        /// <summary>Gets the sheets.</summary>
        /// <value>The sheets.</value>
        public List<XlsBiffBoundSheet> Sheets { get; } = new List<XlsBiffBoundSheet>();

        /// <summary>Gets or sets the sst.</summary>
        /// <value>The sst.</value>
        public XlsBiffSST SST { get; set; }

        /// <summary>Gets the styles.</summary>
        /// <value>The styles.</value>
        public List<XlsBiffRecord> Styles { get; } = new List<XlsBiffRecord>();

        /// <summary>Gets or sets the write access.</summary>
        /// <value>The write access.</value>
        public XlsBiffRecord WriteAccess { get; set; }
    }
}
