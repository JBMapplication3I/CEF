// Decompiled with JetBrains decompiler
// Type: Excel.ExcelOpenXmlReader
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using Core;
    using Core.OpenXmlFormat;

    /// <summary>An excel open XML reader.</summary>
    /// <seealso cref="Excel.IExcelDataReader"/>
    /// <seealso cref="System.Data.IDataReader"/>
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="System.Data.IDataRecord"/>
    public class ExcelOpenXmlReader : IExcelDataReader, IDataReader, IDisposable, IDataRecord
    {
        /// <summary>The column.</summary>
        private const string COLUMN = "Column";

        /// <summary>The cells values.</summary>
        private object[] _cellsValues;

        /// <summary>The default date time styles.</summary>
        private readonly List<int> _defaultDateTimeStyles;

        /// <summary>Number of empty rows.</summary>
        private int _emptyRowCount;

        /// <summary>True if this ExcelOpenXmlReader is first read.</summary>
        private bool _isFirstRead;

        /// <summary>URI of the namespace.</summary>
        private string _namespaceUri;

        /// <summary>Zero-based index of the result.</summary>
        private int _resultIndex;

        /// <summary>The saved cells values.</summary>
        private object[] _savedCellsValues;

        /// <summary>The sheet stream.</summary>
        private Stream _sheetStream;

        /// <summary>The workbook.</summary>
        private XlsxWorkbook _workbook;

        /// <summary>The XML reader.</summary>
        private XmlReader _xmlReader;

        /// <summary>The zip worker.</summary>
        private ZipWorker _zipWorker;

        /// <summary>True if disposed.</summary>
        private bool disposed;

        /// <summary>Identifier for the instance.</summary>
        private readonly string instanceId = Guid.NewGuid().ToString();

        /// <summary>Initializes a new instance of the <see cref="Excel.ExcelOpenXmlReader"/> class.</summary>
        internal ExcelOpenXmlReader()
        {
            IsValid = true;
            _isFirstRead = true;
            _defaultDateTimeStyles = new List<int>(new int[12] { 14, 15, 16, 17, 18, 19, 20, 21, 22, 45, 46, 47 });
        }

        /// <summary>Finalizes an instance of the Excel.ExcelOpenXmlReader class.</summary>
        ~ExcelOpenXmlReader()
        {
            Dispose(false);
        }

        /// <inheritdoc/>
        public int Depth { get; private set; }

        /// <inheritdoc/>
        public string ExceptionMessage { get; private set; }

        /// <inheritdoc/>
        public int FieldCount
            => _resultIndex < 0 || _resultIndex >= ResultsCount ? -1 : _workbook.Sheets[_resultIndex].ColumnsCount;

        /// <inheritdoc/>
        public bool IsClosed { get; private set; }

        /// <inheritdoc/>
        public bool IsFirstRowAsColumnNames { get; set; }

        /// <inheritdoc/>
        public bool IsValid { get; private set; }

        /// <inheritdoc/>
        public string Name
            => _resultIndex < 0 || _resultIndex >= ResultsCount ? null : _workbook.Sheets[_resultIndex].Name;

        /// <inheritdoc/>
        public int RecordsAffected => throw new NotSupportedException();

        /// <inheritdoc/>
        public int ResultsCount => _workbook != null ? _workbook.Sheets.Count : -1;

        /// <inheritdoc/>
        public object this[int i] => _cellsValues[i];

        /// <inheritdoc/>
        public object this[string name] => throw new NotSupportedException();

        /// <summary>Converts this ExcelOpenXmlReader to a data set.</summary>
        /// <returns>A DataSet.</returns>
        public DataSet AsDataSet()
        {
            return AsDataSet(true);
        }

        /// <summary>Converts a convertOADateTime to a data set.</summary>
        /// <param name="convertOADateTime">True to convert oa date time.</param>
        /// <returns>A DataSet.</returns>
        public DataSet AsDataSet(bool convertOADateTime)
        {
            if (!IsValid)
            {
                return null;
            }
            var dataset = new DataSet();
            for (var index1 = 0; index1 < _workbook.Sheets.Count; ++index1)
            {
                var table = new DataTable(_workbook.Sheets[index1].Name);
                ReadSheetGlobals(_workbook.Sheets[index1]);
                if (_workbook.Sheets[index1].Dimension != null)
                {
                    Depth = 0;
                    _emptyRowCount = 0;
                    if (!IsFirstRowAsColumnNames)
                    {
                        for (var index2 = 0; index2 < _workbook.Sheets[index1].ColumnsCount; ++index2)
                        {
                            table.Columns.Add(null, typeof(object));
                        }
                    }
                    else if (ReadSheetRow(_workbook.Sheets[index1]))
                    {
                        for (var index2 = 0; index2 < _cellsValues.Length; ++index2)
                        {
                            if (_cellsValues[index2] != null && _cellsValues[index2].ToString().Length > 0)
                            {
                                Helpers.AddColumnHandleDuplicate(table, _cellsValues[index2].ToString());
                            }
                            else
                            {
                                Helpers.AddColumnHandleDuplicate(table, "Column" + index2);
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                    table.BeginLoadData();
                    while (ReadSheetRow(_workbook.Sheets[index1]))
                    {
                        table.Rows.Add(_cellsValues);
                    }
                    if (table.Rows.Count > 0)
                    {
                        dataset.Tables.Add(table);
                    }
                    table.EndLoadData();
                }
            }
            dataset.AcceptChanges();
            Helpers.FixDataTypes(dataset);
            return dataset;
        }

        /// <inheritdoc/>
        public void Close()
        {
            IsClosed = true;
            if (_xmlReader != null)
            {
                _xmlReader.Close();
            }
            if (_sheetStream != null)
            {
                _sheetStream.Close();
            }
            if (_zipWorker == null)
            {
                return;
            }
            _zipWorker.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public bool GetBoolean(int i)
        {
            return !IsDBNull(i) && bool.Parse(_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public byte GetByte(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public char GetChar(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public IDataReader GetData(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public string GetDataTypeName(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public DateTime GetDateTime(int i)
        {
            if (IsDBNull(i))
            {
                return DateTime.MinValue;
            }
            try
            {
                return (DateTime)_cellsValues[i];
            }
            catch (InvalidCastException)
            {
                return DateTime.MinValue;
            }
        }

        /// <inheritdoc/>
        public decimal GetDecimal(int i)
        {
            return IsDBNull(i) ? decimal.MinValue : decimal.Parse(_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public double GetDouble(int i)
        {
            return IsDBNull(i) ? double.MinValue : double.Parse(_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public Type GetFieldType(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public float GetFloat(int i)
        {
            return IsDBNull(i) ? float.MinValue : float.Parse(_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public Guid GetGuid(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public short GetInt16(int i)
        {
            return IsDBNull(i) ? short.MinValue : short.Parse(_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public int GetInt32(int i)
        {
            return IsDBNull(i) ? int.MinValue : int.Parse(_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public long GetInt64(int i)
        {
            return IsDBNull(i) ? long.MinValue : long.Parse(_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public string GetName(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public int GetOrdinal(string name)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public DataTable GetSchemaTable()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public string GetString(int i)
        {
            return IsDBNull(i) ? null : _cellsValues[i].ToString();
        }

        /// <inheritdoc/>
        public object GetValue(int i)
        {
            return _cellsValues[i];
        }

        /// <inheritdoc/>
        public int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        /// <summary>Initializes this ExcelOpenXmlReader.</summary>
        /// <param name="fileStream">The file stream.</param>
        public void Initialize(Stream fileStream)
        {
            _zipWorker = new ZipWorker();
            _zipWorker.Extract(fileStream);
            if (!_zipWorker.IsValid)
            {
                IsValid = false;
                ExceptionMessage = _zipWorker.ExceptionMessage;
                Close();
            }
            else
            {
                ReadGlobals();
            }
        }

        /// <inheritdoc/>
        public bool IsDBNull(int i)
        {
            return _cellsValues[i] == null || DBNull.Value == _cellsValues[i];
        }

        /// <inheritdoc/>
        public bool NextResult()
        {
            if (_resultIndex >= ResultsCount - 1)
            {
                return false;
            }
            ++_resultIndex;
            _isFirstRead = true;
            _savedCellsValues = null;
            return true;
        }

        /// <inheritdoc/>
        public bool Read()
        {
            return IsValid && (!_isFirstRead || InitializeSheetRead()) && ReadSheetRow(_workbook.Sheets[_resultIndex]);
        }

        /// <summary>Check date time number fmts.</summary>
        /// <param name="list">The list.</param>
        private void CheckDateTimeNumFmts(List<XlsxNumFmt> list)
        {
            if (list.Count == 0)
            {
                return;
            }
            foreach (var xlsxNumFmt in list)
            {
                if (!string.IsNullOrEmpty(xlsxNumFmt.FormatCode))
                {
                    var str = xlsxNumFmt.FormatCode.ToLower();
                    int startIndex;
                    while ((startIndex = str.IndexOf('"')) > 0)
                    {
                        var num = str.IndexOf('"', startIndex + 1);
                        if (num > 0)
                        {
                            str = str.Remove(startIndex, num - startIndex + 1);
                        }
                    }
                    if (new FormatReader { FormatString = str }.IsDateFormatString())
                    {
                        _defaultDateTimeStyles.Add(xlsxNumFmt.Id);
                    }
                }
            }
        }

        /// <summary>Releases the unmanaged resources used by the Excel.ExcelOpenXmlReader and optionally releases the
        /// managed resources.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if (_xmlReader != null)
                {
                    _xmlReader.Dispose();
                }
                if (_sheetStream != null)
                {
                    _sheetStream.Dispose();
                }
                if (_zipWorker != null)
                {
                    _zipWorker.Dispose();
                }
            }
            _zipWorker = null;
            _xmlReader = null;
            _sheetStream = null;
            _workbook = null;
            _cellsValues = null;
            _savedCellsValues = null;
            disposed = true;
        }

        /// <summary>Initializes the sheet read.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool InitializeSheetRead()
        {
            if (ResultsCount <= 0)
            {
                return false;
            }
            ReadSheetGlobals(_workbook.Sheets[_resultIndex]);
            if (_workbook.Sheets[_resultIndex].Dimension == null)
            {
                return false;
            }
            _isFirstRead = false;
            Depth = 0;
            _emptyRowCount = 0;
            return true;
        }

        /// <summary>Query if 'styleId' is date time style.</summary>
        /// <param name="styleId">Identifier for the style.</param>
        /// <returns>True if date time style, false if not.</returns>
        private bool IsDateTimeStyle(int styleId)
        {
            return _defaultDateTimeStyles.Contains(styleId);
        }

        /// <summary>Reads the globals.</summary>
        private void ReadGlobals()
        {
            _workbook = new XlsxWorkbook(
                _zipWorker.GetWorkbookStream(),
                _zipWorker.GetWorkbookRelsStream(),
                _zipWorker.GetSharedStringsStream(),
                _zipWorker.GetStylesStream());
            CheckDateTimeNumFmts(_workbook.Styles.NumFmts);
        }

        /// <summary>Reads sheet globals.</summary>
        /// <param name="sheet">The sheet.</param>
        private void ReadSheetGlobals(XlsxWorksheet sheet)
        {
            if (_xmlReader != null)
            {
                _xmlReader.Close();
            }
            if (_sheetStream != null)
            {
                _sheetStream.Close();
            }
            _sheetStream = _zipWorker.GetWorksheetStream(sheet.Path);
            if (_sheetStream == null)
            {
                return;
            }
            _xmlReader = XmlReader.Create(_sheetStream);
            var rows = 0;
            var cols = 0;
            _namespaceUri = null;
            var num = 0;
            while (_xmlReader.Read())
            {
                if (_xmlReader.NodeType == XmlNodeType.Element && _xmlReader.LocalName == "worksheet")
                {
                    _namespaceUri = _xmlReader.NamespaceURI;
                }
                if (_xmlReader.NodeType == XmlNodeType.Element && _xmlReader.LocalName == "dimension")
                {
                    var attribute = _xmlReader.GetAttribute("ref");
                    sheet.Dimension = new XlsxDimension(attribute);
                    break;
                }
                if (_xmlReader.NodeType == XmlNodeType.Element && _xmlReader.LocalName == "row")
                {
                    ++rows;
                }
                if (sheet.Dimension == null
                    && cols == 0
                    && _xmlReader.NodeType == XmlNodeType.Element
                    && _xmlReader.LocalName == "c")
                {
                    var attribute = _xmlReader.GetAttribute("r");
                    if (attribute != null)
                    {
                        var columnAndRow = ReferenceHelper.ReferenceToColumnAndRow(attribute);
                        if (columnAndRow[1] > num)
                        {
                            num = columnAndRow[1];
                        }
                    }
                }
            }
            if (sheet.Dimension == null)
            {
                if (cols == 0)
                {
                    cols = num;
                }
                if (rows == 0 || cols == 0)
                {
                    sheet.IsEmpty = true;
                    return;
                }
                sheet.Dimension = new XlsxDimension(rows, cols);
                _xmlReader.Close();
                _sheetStream.Close();
                _sheetStream = _zipWorker.GetWorksheetStream(sheet.Path);
                _xmlReader = XmlReader.Create(_sheetStream);
            }
            _xmlReader.ReadToFollowing("sheetData", _namespaceUri);
            if (!_xmlReader.IsEmptyElement)
            {
                return;
            }
            sheet.IsEmpty = true;
        }

        /// <summary>Reads sheet row.</summary>
        /// <param name="sheet">The sheet.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool ReadSheetRow(XlsxWorksheet sheet)
        {
            if (_xmlReader == null)
            {
                return false;
            }
            if (_emptyRowCount != 0)
            {
                _cellsValues = new object[sheet.ColumnsCount];
                --_emptyRowCount;
                ++Depth;
                return true;
            }
            if (_savedCellsValues != null)
            {
                _cellsValues = _savedCellsValues;
                _savedCellsValues = null;
                ++Depth;
                return true;
            }
            if (_xmlReader.NodeType == XmlNodeType.Element && _xmlReader.LocalName == "row"
                || _xmlReader.ReadToFollowing("row", _namespaceUri))
            {
                _cellsValues = new object[sheet.ColumnsCount];
                var num = int.Parse(_xmlReader.GetAttribute("r"));
                if (num != Depth + 1 && num != Depth + 1)
                {
                    _emptyRowCount = num - Depth - 1;
                }
                var flag = false;
                var s = string.Empty;
                var str = string.Empty;
                var empty = string.Empty;
                var val1 = 0;
                var val2 = 0;
                while (_xmlReader.Read() && _xmlReader.Depth != 2)
                {
                    if (_xmlReader.NodeType == XmlNodeType.Element)
                    {
                        flag = false;
                        if (_xmlReader.LocalName == "c")
                        {
                            s = _xmlReader.GetAttribute("s");
                            str = _xmlReader.GetAttribute("t");
                            XlsxDimension.XlsxDim(_xmlReader.GetAttribute("r"), out val1, out val2);
                        }
                        else if (_xmlReader.LocalName == "v" || _xmlReader.LocalName == "t")
                        {
                            flag = true;
                        }
                    }
                    if (_xmlReader.NodeType == XmlNodeType.Text && flag)
                    {
                        object obj = _xmlReader.Value;
                        var style = NumberStyles.Any;
                        var invariantCulture = CultureInfo.InvariantCulture;
                        if (double.TryParse(obj.ToString(), style, invariantCulture, out var result))
                        {
                            obj = result;
                        }
                        if (str != null && str == "s")
                        {
                            obj = Helpers.ConvertEscapeChars(_workbook.SST[int.Parse(obj.ToString())]);
                        }
                        else if (str != null && str == "inlineStr")
                        {
                            obj = Helpers.ConvertEscapeChars(obj.ToString());
                        }
                        else if (str == "b")
                        {
                            obj = _xmlReader.Value == "1";
                        }
                        else if (s != null)
                        {
                            var cellXf = _workbook.Styles.CellXfs[int.Parse(s)];
                            if (cellXf.ApplyNumberFormat
                                && obj != null
                                && obj.ToString() != string.Empty
                                && IsDateTimeStyle(cellXf.NumFmtId))
                            {
                                obj = Helpers.ConvertFromOATime(result);
                            }
                            else if (cellXf.NumFmtId == 49)
                            {
                                obj = obj.ToString();
                            }
                        }
                        if (val1 - 1 < _cellsValues.Length)
                        {
                            _cellsValues[val1 - 1] = obj;
                        }
                    }
                }
                if (_emptyRowCount > 0)
                {
                    _savedCellsValues = _cellsValues;
                    return ReadSheetRow(sheet);
                }
                ++Depth;
                return true;
            }
            _xmlReader.Close();
            if (_sheetStream != null)
            {
                _sheetStream.Close();
            }
            return false;
        }
    }
}
