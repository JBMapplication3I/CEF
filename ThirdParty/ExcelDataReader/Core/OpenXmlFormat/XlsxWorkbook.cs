// Decompiled with JetBrains decompiler
// Type: Excel.Core.OpenXmlFormat.XlsxWorkbook
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel.Core.OpenXmlFormat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    /// <summary>An XLSX workbook.</summary>
    internal class XlsxWorkbook
    {
        /// <summary>The identifier.</summary>
        private const string A_id = "Id";

        /// <summary>The name.</summary>
        private const string A_name = "name";

        /// <summary>The rid.</summary>
        private const string A_rid = "r:id";

        /// <summary>Identifier for the sheet.</summary>
        private const string A_sheetId = "sheetId";

        /// <summary>Target for the.</summary>
        private const string A_target = "Target";

        /// <summary>The cell xfs.</summary>
        private const string N_cellXfs = "cellXfs";

        /// <summary>Number of fmts.</summary>
        private const string N_numFmts = "numFmts";

        /// <summary>The relative.</summary>
        private const string N_rel = "Relationship";

        /// <summary>The sheet.</summary>
        private const string N_sheet = "sheet";

        /// <summary>The SI.</summary>
        private const string N_si = "si";

        /// <summary>The t.</summary>
        private const string N_t = "t";

        /// <summary>Initializes a new instance of the <see cref="Excel.Core.OpenXmlFormat.XlsxWorkbook"/> class.</summary>
        /// <param name="workbookStream">     The workbook stream.</param>
        /// <param name="relsStream">         The rels stream.</param>
        /// <param name="sharedStringsStream">The shared strings stream.</param>
        /// <param name="stylesStream">       The styles stream.</param>
        public XlsxWorkbook(Stream workbookStream, Stream relsStream, Stream sharedStringsStream, Stream stylesStream)
        {
            if (workbookStream == null)
            {
                throw new ArgumentNullException(nameof(workbookStream));
            }
            ReadWorkbook(workbookStream);
            ReadWorkbookRels(relsStream);
            ReadSharedStrings(sharedStringsStream);
            ReadStyles(stylesStream);
        }

        /// <summary>Prevents a default instance of the Excel.Core.OpenXmlFormat.XlsxWorkbook class from being
        /// created.</summary>
        private XlsxWorkbook() { }

        /// <summary>Gets or sets the sheets.</summary>
        /// <value>The sheets.</value>
        public List<XlsxWorksheet> Sheets { get; set; }

        /// <summary>Gets the sst.</summary>
        /// <value>The sst.</value>
        public XlsxSST SST { get; private set; }

        /// <summary>Gets the styles.</summary>
        /// <value>The styles.</value>
        public XlsxStyles Styles { get; private set; }

        /// <summary>Reads shared strings.</summary>
        /// <param name="xmlFileStream">The XML file stream.</param>
        private void ReadSharedStrings(Stream xmlFileStream)
        {
            if (xmlFileStream == null)
            {
                return;
            }
            SST = new XlsxSST();
            using var xmlReader = XmlReader.Create(xmlFileStream);
            var flag = false;
            var str = string.Empty;
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "si")
                {
                    if (flag)
                    {
                        SST.Add(str);
                    }
                    else
                    {
                        flag = true;
                    }
                    str = string.Empty;
                }
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "t")
                {
                    str += xmlReader.ReadElementContentAsString();
                }
            }
            if (flag)
            {
                SST.Add(str);
            }
            xmlFileStream.Close();
        }

        /// <summary>Reads the styles.</summary>
        /// <param name="xmlFileStream">The XML file stream.</param>
        private void ReadStyles(Stream xmlFileStream)
        {
            if (xmlFileStream == null)
            {
                return;
            }
            Styles = new XlsxStyles();
            var flag = false;
            using var xmlReader = XmlReader.Create(xmlFileStream);
            while (xmlReader.Read())
            {
                if (!flag && xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "numFmts")
                {
                    while (xmlReader.Read() && (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Depth != 1))
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "numFmt")
                        {
                            Styles.NumFmts.Add(
                                new XlsxNumFmt(
                                    int.Parse(xmlReader.GetAttribute("numFmtId")),
                                    xmlReader.GetAttribute("formatCode")));
                        }
                    }
                    flag = true;
                }
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "cellXfs")
                {
                    while (xmlReader.Read() && (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Depth != 1))
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "xf")
                        {
                            var attribute1 = xmlReader.GetAttribute("xfId");
                            var attribute2 = xmlReader.GetAttribute("numFmtId");
                            Styles.CellXfs.Add(
                                new XlsxXf(
                                    attribute1 == null ? -1 : int.Parse(attribute1),
                                    attribute2 == null ? -1 : int.Parse(attribute2),
                                    xmlReader.GetAttribute("applyNumberFormat")));
                        }
                    }
                    break;
                }
            }
            xmlFileStream.Close();
        }

        /// <summary>Reads a workbook.</summary>
        /// <param name="xmlFileStream">The XML file stream.</param>
        private void ReadWorkbook(Stream xmlFileStream)
        {
            Sheets = new List<XlsxWorksheet>();
            using var xmlReader = XmlReader.Create(xmlFileStream);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "sheet")
                {
                    Sheets.Add(
                        new XlsxWorksheet(
                            xmlReader.GetAttribute("name"),
                            int.Parse(xmlReader.GetAttribute("sheetId")),
                            xmlReader.GetAttribute("r:id")));
                }
            }
            xmlFileStream.Close();
        }

        /// <summary>Reads workbook rels.</summary>
        /// <param name="xmlFileStream">The XML file stream.</param>
        private void ReadWorkbookRels(Stream xmlFileStream)
        {
            using var xmlReader = XmlReader.Create(xmlFileStream);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Relationship")
                {
                    var attribute = xmlReader.GetAttribute("Id");
                    for (var index = 0; index < Sheets.Count; ++index)
                    {
                        var sheet = Sheets[index];
                        if (sheet.RID == attribute)
                        {
                            sheet.Path = xmlReader.GetAttribute("Target");
                            Sheets[index] = sheet;
                            break;
                        }
                    }
                }
            }
            xmlFileStream.Close();
        }
    }
}
