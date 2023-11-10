// Decompiled with JetBrains decompiler
// Type: Excel.ExcelBinaryReader
// Assembly: Excel, Version=2.1.2.3, Culture=neutral, PublicKeyToken=93517dbe6a4012fa
// MVID: BAA9F851-C6E3-48A6-A81E-7924C581E3AA
// Assembly location: C:\Users\jotha\.nuget\packages\exceldatareader\2.1.2.3\lib\net45\Excel.dll

namespace Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using Core;
    using Core.BinaryFormat;
    using Excel.Log;
    using Exceptions;

    /// <summary>An excel binary reader.</summary>
    /// <seealso cref="IExcelDataReader"/>
    /// <seealso cref="IDataReader"/>
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="IDataRecord"/>
    public class ExcelBinaryReader : IExcelDataReader, IDataReader, IDisposable, IDataRecord
    {
        /// <summary>The book.</summary>
        private const string BOOK = "Book";

        /// <summary>The column.</summary>
        private const string COLUMN = "Column";

        /// <summary>The wor kB ook.</summary>
        private const string WORKBOOK = "Workbook";

        /// <summary>The default encoding.</summary>
        private readonly Encoding m_Default_Encoding = Encoding.UTF8;

        /// <summary>True if disposed.</summary>
        private bool disposed;

        /// <summary>True if this ExcelBinaryReader can read.</summary>
        private bool m_canRead;

        /// <summary>The cell offset.</summary>
        private int m_cellOffset;

        /// <summary>The cells values.</summary>
        private object[] m_cellsValues;

        /// <summary>The current row record.</summary>
        private XlsBiffRow m_currentRowRecord;

        /// <summary>The database cell addrs.</summary>
        private uint[] m_dbCellAddrs;

        /// <summary>Zero-based index of the database cell addrs.</summary>
        private int m_dbCellAddrsIndex;

        /// <summary>The encoding.</summary>
        private Encoding m_encoding;

        /// <summary>The file.</summary>
        private Stream m_file;

        /// <summary>The globals.</summary>
        private XlsWorkbookGlobals m_globals;

        /// <summary>The header.</summary>
        private XlsHeader m_hdr;

        /// <summary>True if this ExcelBinaryReader is first read.</summary>
        private bool m_IsFirstRead;

        /// <summary>The maximum row.</summary>
        private int m_maxRow;

        /// <summary>True to no index.</summary>
        private bool m_noIndex;

        /// <summary>Zero-based index of the sheet.</summary>
        private int m_SheetIndex;

        /// <summary>The sheets.</summary>
        private List<XlsWorksheet> m_sheets;

        /// <summary>The stream.</summary>
        private XlsBiffStream m_stream;

        /// <summary>The version.</summary>
        private ushort m_version;

        /// <summary>Information describing the workbook.</summary>
        private DataSet m_workbookData;

        /// <summary>Initializes a new instance of the <see cref="Excel.ExcelBinaryReader"/> class.</summary>
        internal ExcelBinaryReader()
        {
            m_encoding = m_Default_Encoding;
            m_version = 1536;
            IsValid = true;
            m_SheetIndex = -1;
            m_IsFirstRead = true;
        }

        /// <summary>Initializes a new instance of the <see cref="Excel.ExcelBinaryReader"/> class.</summary>
        /// <param name="readOption">The read option.</param>
        internal ExcelBinaryReader(ReadOption readOption) : this()
        {
            ReadOption = readOption;
        }

        /// <summary>Finalizes an instance of the Excel.ExcelBinaryReader class.</summary>
        ~ExcelBinaryReader()
        {
            Dispose(false);
        }

        /// <summary>Gets or sets a value indicating whether the convert oa date.</summary>
        /// <value>True if convert oa date, false if not.</value>
        public bool ConvertOaDate { get; set; }

        /// <inheritdoc/>
        public int Depth { get; private set; }

        /// <inheritdoc/>
        public string ExceptionMessage { get; private set; }

        /// <inheritdoc/>
        public int FieldCount { get; private set; }

        /// <inheritdoc/>
        public bool IsClosed { get; private set; }

        /// <inheritdoc/>
        public bool IsFirstRowAsColumnNames { get; set; }

        /// <inheritdoc/>
        public bool IsValid { get; private set; }

        /// <inheritdoc/>
        public string Name => m_sheets != null && m_sheets.Count > 0 ? m_sheets[m_SheetIndex].Name : null;

        /// <summary>Gets the read option.</summary>
        /// <value>The read option.</value>
        public ReadOption ReadOption { get; }

        /// <inheritdoc/>
        public int RecordsAffected => throw new NotSupportedException();

        /// <inheritdoc/>
        public int ResultsCount => m_globals.Sheets.Count;

        /// <inheritdoc/>
        public object this[int i] => m_cellsValues[i];

        /// <inheritdoc/>
        public object this[string name] => throw new NotSupportedException();

        /// <summary>Converts this ExcelBinaryReader to a data set.</summary>
        /// <returns>A DataSet.</returns>
        public DataSet AsDataSet()
        {
            return AsDataSet(false);
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
            if (IsClosed)
            {
                return m_workbookData;
            }
            ConvertOaDate = convertOADateTime;
            m_workbookData = new DataSet();
            for (var index = 0; index < ResultsCount; ++index)
            {
                var table = ReadWholeWorkSheet(m_sheets[index]);
                if (table != null)
                {
                    m_workbookData.Tables.Add(table);
                }
            }
            m_file.Close();
            IsClosed = true;
            m_workbookData.AcceptChanges();
            Helpers.FixDataTypes(m_workbookData);
            return m_workbookData;
        }

        /// <inheritdoc/>
        public void Close()
        {
            m_file.Close();
            IsClosed = true;
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
            return !IsDBNull(i) && bool.Parse(m_cellsValues[i].ToString());
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
            var cellsValue = m_cellsValues[i];
            if (cellsValue is DateTime dateTime)
            {
                return dateTime;
            }
            var s = cellsValue.ToString();
            double d;
            try
            {
                d = double.Parse(s);
            }
            catch (FormatException)
            {
                return DateTime.Parse(s);
            }
            return DateTime.FromOADate(d);
        }

        /// <inheritdoc/>
        public decimal GetDecimal(int i)
        {
            return IsDBNull(i) ? decimal.MinValue : decimal.Parse(m_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public double GetDouble(int i)
        {
            return IsDBNull(i) ? double.MinValue : double.Parse(m_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public Type GetFieldType(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public float GetFloat(int i)
        {
            return IsDBNull(i) ? float.MinValue : float.Parse(m_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public Guid GetGuid(int i)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public short GetInt16(int i)
        {
            return IsDBNull(i) ? short.MinValue : short.Parse(m_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public int GetInt32(int i)
        {
            return IsDBNull(i) ? int.MinValue : int.Parse(m_cellsValues[i].ToString());
        }

        /// <inheritdoc/>
        public long GetInt64(int i)
        {
            return IsDBNull(i) ? long.MinValue : long.Parse(m_cellsValues[i].ToString());
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
            return IsDBNull(i) ? null : m_cellsValues[i].ToString();
        }

        /// <inheritdoc/>
        public object GetValue(int i)
        {
            return m_cellsValues[i];
        }

        /// <inheritdoc/>
        public int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        /// <summary>Initializes this ExcelBinaryReader.</summary>
        /// <param name="fileStream">The file stream.</param>
        public void Initialize(Stream fileStream)
        {
            m_file = fileStream;
            ReadWorkBookGlobals();
            m_SheetIndex = 0;
        }

        /// <inheritdoc/>
        public bool IsDBNull(int i)
        {
            return m_cellsValues[i] == null || DBNull.Value == m_cellsValues[i];
        }

        /// <summary>Query if this ExcelBinaryReader is v 8.</summary>
        /// <returns>True if v 8, false if not.</returns>
        public bool IsV8()
        {
            return m_version >= 1536;
        }

        /// <inheritdoc/>
        public bool NextResult()
        {
            if (m_SheetIndex >= ResultsCount - 1)
            {
                return false;
            }
            ++m_SheetIndex;
            m_IsFirstRead = true;
            return true;
        }

        /// <inheritdoc/>
        public bool Read()
        {
            if (!IsValid)
            {
                return false;
            }
            if (m_IsFirstRead)
            {
                InitializeSheetRead();
            }
            return MoveToNextRecord();
        }

        /// <summary>Releases the unmanaged resources used by the Excel.ExcelBinaryReader and optionally releases the
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
                if (m_workbookData != null)
                {
                    m_workbookData.Dispose();
                }
                if (m_sheets != null)
                {
                    m_sheets.Clear();
                }
            }
            m_workbookData = null;
            m_sheets = null;
            m_stream = null;
            m_globals = null;
            m_encoding = null;
            m_hdr = null;
            disposed = true;
        }

        /// <summary>Dumps the biff records.</summary>
        private void DumpBiffRecords()
        {
            var position = m_stream.Position;
            XlsBiffRecord xlsBiffRecord;
            do
            {
                xlsBiffRecord = m_stream.Read();
                LogManager.Log(this).Debug(xlsBiffRecord.ID.ToString());
            }
            while (xlsBiffRecord != null && m_stream.Position < m_stream.Size);
            m_stream.Seek(position, SeekOrigin.Begin);
        }

        /// <summary>Fails.</summary>
        /// <param name="message">The message.</param>
        private void Fail(string message)
        {
            ExceptionMessage = message;
            IsValid = false;
            m_file.Close();
            IsClosed = true;
            m_workbookData = null;
            m_sheets = null;
            m_stream = null;
            m_globals = null;
            m_encoding = null;
            m_hdr = null;
        }

        /// <summary>Searches for the first data cell offset.</summary>
        /// <param name="startOffset">The start offset.</param>
        /// <returns>The found data cell offset.</returns>
        private int FindFirstDataCellOffset(int startOffset)
        {
            XlsBiffRecord xlsBiffRecord;
            for (xlsBiffRecord = m_stream.ReadAt(startOffset); !(xlsBiffRecord is XlsBiffDbCell); xlsBiffRecord = m_stream.Read())
            {
                if (m_stream.Position >= m_stream.Size || xlsBiffRecord is XlsBiffEOF)
                {
                    return -1;
                }
            }
            var rowAddress = ((XlsBiffDbCell)xlsBiffRecord).RowAddress;
            while (m_stream.ReadAt(rowAddress) is XlsBiffRow xlsBiffRow)
            {
                rowAddress += xlsBiffRow.Size;
                if (xlsBiffRow == null)
                {
                    break;
                }
            }
            return rowAddress;
        }

        /// <summary>Initializes the sheet read.</summary>
        private void InitializeSheetRead()
        {
            if (m_SheetIndex == ResultsCount)
            {
                return;
            }
            m_dbCellAddrs = null;
            m_IsFirstRead = false;
            if (m_SheetIndex == -1)
            {
                m_SheetIndex = 0;
            }
            if (!ReadWorkSheetGlobals(m_sheets[m_SheetIndex], out var idx, out m_currentRowRecord))
            {
                ++m_SheetIndex;
                InitializeSheetRead();
            }
            else if (idx == null)
            {
                m_noIndex = true;
            }
            else
            {
                m_dbCellAddrs = idx.DbCellAddresses;
                m_dbCellAddrsIndex = 0;
                m_cellOffset = FindFirstDataCellOffset((int)m_dbCellAddrs[m_dbCellAddrsIndex]);
                if (m_cellOffset >= 0)
                {
                    return;
                }
                Fail("Badly formed binary file. Has INDEX but no DBCELL");
            }
        }

        /// <summary>Determines if we can move to next record.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool MoveToNextRecord()
        {
            if (m_noIndex)
            {
                LogManager.Log(this).Debug("No index");
                return MoveToNextRecordNoIndex();
            }
            if (m_dbCellAddrs == null || m_dbCellAddrsIndex == m_dbCellAddrs.Length || Depth == m_maxRow)
            {
                return false;
            }
            m_canRead = ReadWorkSheetRow();
            if (!m_canRead && Depth > 0)
            {
                m_canRead = true;
            }
            if (!m_canRead && m_dbCellAddrsIndex < m_dbCellAddrs.Length - 1)
            {
                ++m_dbCellAddrsIndex;
                m_cellOffset = FindFirstDataCellOffset((int)m_dbCellAddrs[m_dbCellAddrsIndex]);
                if (m_cellOffset < 0)
                {
                    return false;
                }
                m_canRead = ReadWorkSheetRow();
            }
            return m_canRead;
        }

        /// <summary>Determines if we can move to next record no index.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool MoveToNextRecordNoIndex()
        {
            var xlsBiffRow = m_currentRowRecord;
            if (xlsBiffRow == null)
            {
                return false;
            }
            if ((int)xlsBiffRow.RowIndex < Depth)
            {
                m_stream.Seek(xlsBiffRow.Offset + xlsBiffRow.Size, SeekOrigin.Begin);
                while (m_stream.Position < m_stream.Size)
                {
                    var xlsBiffRecord = m_stream.Read();
                    if (xlsBiffRecord is XlsBiffEOF)
                    {
                        return false;
                    }
                    if (xlsBiffRecord is XlsBiffRow xlsBiffRow2 && xlsBiffRow2.RowIndex >= Depth)
                    {
                        goto label_9;
                    }
                }
                return false;
            }
        label_9:
            m_currentRowRecord = xlsBiffRow;
            XlsBiffBlankCell xlsBiffBlankCell1 = null;
            while (m_stream.Position < m_stream.Size)
            {
                var xlsBiffRecord = m_stream.Read();
                if (xlsBiffRecord is XlsBiffEOF)
                {
                    return false;
                }
                if (xlsBiffRecord.IsCell
                    && xlsBiffRecord is XlsBiffBlankCell xlsBiffBlankCell2
                    && xlsBiffBlankCell2.RowIndex == m_currentRowRecord.RowIndex)
                {
                    xlsBiffBlankCell1 = xlsBiffBlankCell2;
                }
                if (xlsBiffBlankCell1 != null)
                {
                    m_cellOffset = xlsBiffBlankCell1.Offset;
                    m_canRead = ReadWorkSheetRow();
                    return m_canRead;
                }
            }
            return false;
        }

        /// <summary>Pushes a cell value.</summary>
        /// <param name="cell">The cell.</param>
        private void PushCellValue(XlsBiffBlankCell cell)
        {
            LogManager.Log(this).Debug("pushCellValue {0}", (object)cell.ID);
            var id = cell.ID;
            if ((uint)id <= 214U)
            {
                switch (id)
                {
                    case BIFFRECORDTYPE.BLANK_OLD:
                    {
                        return;
                    }
                    case BIFFRECORDTYPE.INTEGER_OLD:
                    {
                        break;
                    }
                    case BIFFRECORDTYPE.NUMBER_OLD:
                    {
                        goto label_13;
                    }
                    case BIFFRECORDTYPE.LABEL_OLD:
                    case BIFFRECORDTYPE.RSTRING:
                    {
                        goto label_14;
                    }
                    case BIFFRECORDTYPE.BOOLERR_OLD:
                    {
                        if (cell.ReadByte(8) != 0)
                        {
                            return;
                        }
                        m_cellsValues[cell.ColumnIndex] = cell.ReadByte(7) != 0;
                        return;
                    }
                    case BIFFRECORDTYPE.FORMULA_OLD:
                    {
                        goto label_21;
                    }
                    case BIFFRECORDTYPE.MULRK:
                    {
                        var xlsBiffMulRkCell = (XlsBiffMulRKCell)cell;
                        for (var columnIndex = cell.ColumnIndex;
                             (int)columnIndex <= (int)xlsBiffMulRkCell.LastColumnIndex;
                             ++columnIndex)
                        {
                            var num = xlsBiffMulRkCell.GetValue(columnIndex);
                            LogManager.Log(this).Debug("VALUE[{1}]: {0}", (object)num, (object)columnIndex);
                            m_cellsValues[columnIndex] = !ConvertOaDate
                                ? num
                                : TryConvertOADateTime(num, xlsBiffMulRkCell.GetXF(columnIndex));
                        }
                        return;
                    }
                    case BIFFRECORDTYPE.MULBLANK:
                    {
                        return;
                    }
                    default:
                    {
                        return;
                    }
                }
            }
            else if ((uint)id <= 517U)
            {
                switch (id)
                {
                    case BIFFRECORDTYPE.LABELSST:
                    {
                        var str = m_globals.SST.GetString(((XlsBiffLabelSSTCell)cell).SSTIndex);
                        LogManager.Log(this).Debug("VALUE: {0}", (object)str);
                        m_cellsValues[cell.ColumnIndex] = str;
                        return;
                    }
                    case BIFFRECORDTYPE.BLANK:
                    {
                        return;
                    }
                    case BIFFRECORDTYPE.INTEGER:
                    {
                        break;
                    }
                    case BIFFRECORDTYPE.NUMBER:
                    {
                        goto label_13;
                    }
                    case BIFFRECORDTYPE.LABEL:
                    {
                        goto label_14;
                    }
                    case BIFFRECORDTYPE.BOOLERR:
                    {
                        if (cell.ReadByte(7) != 0)
                        {
                            return;
                        }
                        m_cellsValues[cell.ColumnIndex] = cell.ReadByte(6) != 0;
                        return;
                    }
                    default:
                    {
                        return;
                    }
                }
            }
            else
            {
                switch (id)
                {
                    case BIFFRECORDTYPE.RK:
                    {
                        var num1 = ((XlsBiffRKCell)cell).Value;
                        m_cellsValues[cell.ColumnIndex] = !ConvertOaDate ? num1 : TryConvertOADateTime(num1, cell.XFormat);
                        LogManager.Log(this).Debug("VALUE: {0}", (object)num1);
                        return;
                    }
                    case BIFFRECORDTYPE.FORMULA:
                    {
                        goto label_21;
                    }
                    default:
                    {
                        return;
                    }
                }
            }
            m_cellsValues[cell.ColumnIndex] = ((XlsBiffIntegerCell)cell).Value;
            return;
        label_13:
            var num2 = ((XlsBiffNumberCell)cell).Value;
            m_cellsValues[cell.ColumnIndex] = !ConvertOaDate ? num2 : TryConvertOADateTime(num2, cell.XFormat);
            LogManager.Log(this).Debug("VALUE: {0}", (object)num2);
            return;
        label_14:
            m_cellsValues[cell.ColumnIndex] = ((XlsBiffLabelCell)cell).Value;
            LogManager.Log(this).Debug("VALUE: {0}", m_cellsValues[cell.ColumnIndex]);
            return;
        label_21:
            var obj = ((XlsBiffFormulaCell)cell).Value;
            if (obj == null || !(obj is FORMULAERROR))
            {
                m_cellsValues[cell.ColumnIndex] = !ConvertOaDate ? obj : TryConvertOADateTime(obj, cell.XFormat);
            }
        }

        /// <summary>Reads whole work sheet.</summary>
        /// <param name="sheet">The sheet.</param>
        /// <returns>The whole work sheet.</returns>
        private DataTable ReadWholeWorkSheet(XlsWorksheet sheet)
        {
            if (!ReadWorkSheetGlobals(sheet, out var idx, out m_currentRowRecord))
            {
                return null;
            }
            var table = new DataTable(sheet.Name);
            var triggerCreateColumns = true;
            if (idx != null)
            {
                ReadWholeWorkSheetWithIndex(idx, triggerCreateColumns, table);
            }
            else
            {
                ReadWholeWorkSheetNoIndex(triggerCreateColumns, table);
            }
            table.EndLoadData();
            return table;
        }

        /// <summary>Reads whole work sheet no index.</summary>
        /// <param name="triggerCreateColumns">True to trigger create columns.</param>
        /// <param name="table">               The table.</param>
        private void ReadWholeWorkSheetNoIndex(bool triggerCreateColumns, DataTable table)
        {
            while (Read() && Depth != m_maxRow)
            {
                var flag = false;
                if (triggerCreateColumns)
                {
                    if (IsFirstRowAsColumnNames || IsFirstRowAsColumnNames && m_maxRow == 1)
                    {
                        for (var index = 0; index < FieldCount; ++index)
                        {
                            if (m_cellsValues[index] != null && m_cellsValues[index].ToString().Length > 0)
                            {
                                Helpers.AddColumnHandleDuplicate(table, m_cellsValues[index].ToString());
                            }
                            else
                            {
                                Helpers.AddColumnHandleDuplicate(table, "Column" + index);
                            }
                        }
                    }
                    else
                    {
                        for (var index = 0; index < FieldCount; ++index)
                        {
                            table.Columns.Add(null, typeof(object));
                        }
                    }
                    triggerCreateColumns = false;
                    flag = true;
                    table.BeginLoadData();
                }
                if (!flag && Depth > 0 && (!IsFirstRowAsColumnNames || m_maxRow != 1))
                {
                    table.Rows.Add(m_cellsValues);
                }
            }
            if (Depth <= 0 || IsFirstRowAsColumnNames && m_maxRow == 1)
            {
                return;
            }
            table.Rows.Add(m_cellsValues);
        }

        /// <summary>Reads whole work sheet with index.</summary>
        /// <param name="idx">                 The index.</param>
        /// <param name="triggerCreateColumns">True to trigger create columns.</param>
        /// <param name="table">               The table.</param>
        private void ReadWholeWorkSheetWithIndex(XlsBiffIndex idx, bool triggerCreateColumns, DataTable table)
        {
            m_dbCellAddrs = idx.DbCellAddresses;
            for (var index1 = 0; index1 < m_dbCellAddrs.Length && Depth != m_maxRow; ++index1)
            {
                m_cellOffset = FindFirstDataCellOffset((int)m_dbCellAddrs[index1]);
                if (m_cellOffset < 0)
                {
                    break;
                }
                if (triggerCreateColumns)
                {
                    if (IsFirstRowAsColumnNames && ReadWorkSheetRow() || IsFirstRowAsColumnNames && m_maxRow == 1)
                    {
                        for (var index2 = 0; index2 < FieldCount; ++index2)
                        {
                            if (m_cellsValues[index2] != null && m_cellsValues[index2].ToString().Length > 0)
                            {
                                Helpers.AddColumnHandleDuplicate(table, m_cellsValues[index2].ToString());
                            }
                            else
                            {
                                Helpers.AddColumnHandleDuplicate(table, "Column" + index2);
                            }
                        }
                    }
                    else
                    {
                        for (var index2 = 0; index2 < FieldCount; ++index2)
                        {
                            table.Columns.Add(null, typeof(object));
                        }
                    }
                    triggerCreateColumns = false;
                    table.BeginLoadData();
                }
                while (ReadWorkSheetRow())
                {
                    table.Rows.Add(m_cellsValues);
                }
                if (Depth > 0 && (!IsFirstRowAsColumnNames || m_maxRow != 1))
                {
                    table.Rows.Add(m_cellsValues);
                }
            }
        }

        /// <summary>Reads work book globals.</summary>
        private void ReadWorkBookGlobals()
        {
            try
            {
                m_hdr = XlsHeader.ReadHeader(m_file);
            }
            catch (HeaderException ex)
            {
                Fail(ex.Message);
                return;
            }
            catch (FormatException ex)
            {
                Fail(ex.Message);
                return;
            }
            var rootDir = new XlsRootDirectory(m_hdr);
            var xlsDirectoryEntry = rootDir.FindEntry("Workbook") ?? rootDir.FindEntry("Book");
            if (xlsDirectoryEntry == null)
            {
                Fail("Error: Neither stream 'Workbook' nor 'Book' was found in file.");
            }
            else if (xlsDirectoryEntry.EntryType != STGTY.STGTY_STREAM)
            {
                Fail("Error: Workbook directory entry is not a Stream.");
            }
            else
            {
                m_stream = new XlsBiffStream(
                    m_hdr,
                    xlsDirectoryEntry.StreamFirstSector,
                    xlsDirectoryEntry.IsEntryMiniStream,
                    rootDir,
                    this);
                m_globals = new XlsWorkbookGlobals();
                m_stream.Seek(0, SeekOrigin.Begin);
                if (m_stream.Read() is not XlsBiffBOF xlsBiffBof || xlsBiffBof.Type != BIFFTYPE.WorkbookGlobals)
                {
                    Fail("Error reading Workbook Globals - Stream has invalid data.");
                    return;
                }
                var flag = false;
                m_version = xlsBiffBof.Version;
                m_sheets = new List<XlsWorksheet>();
                XlsBiffRecord xlsBiffRecord;
                while ((xlsBiffRecord = m_stream.Read()) != null)
                {
                    var id = xlsBiffRecord.ID;
                    if ((uint)id <= 140U)
                    {
                        if ((uint)id <= 49U)
                        {
                            if ((uint)id <= 19U)
                            {
                                switch (id)
                                {
                                    case BIFFRECORDTYPE.EOF:
                                    {
                                        if (m_globals.SST == null)
                                        {
                                            return;
                                        }
                                        m_globals.SST.ReadStrings();
                                        return;
                                    }
                                    default:
                                    {
                                        continue;
                                    }
                                }
                            }
                            switch (id)
                            {
                                case BIFFRECORDTYPE.FORMAT_V23:
                                {
                                    var biffFormatString1 = (XlsBiffFormatString)xlsBiffRecord;
                                    biffFormatString1.UseEncoding = m_encoding;
                                    m_globals.Formats.Add((ushort)m_globals.Formats.Count, biffFormatString1);
                                    continue;
                                }
                                case BIFFRECORDTYPE.FONT:
                                {
                                    break;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                        else if ((uint)id <= 67U)
                        {
                            switch (id)
                            {
                                case BIFFRECORDTYPE.CONTINUE:
                                {
                                    if (flag)
                                    {
                                        m_globals.SST.Append((XlsBiffContinue)xlsBiffRecord);
                                    }
                                    continue;
                                }
                                case BIFFRECORDTYPE.CODEPAGE:
                                {
                                    m_globals.CodePage = (XlsBiffSimpleValueRecord)xlsBiffRecord;
                                    try
                                    {
                                        m_encoding = Encoding.GetEncoding(m_globals.CodePage.Value);
                                        continue;
                                    }
                                    catch (ArgumentException)
                                    {
                                        continue;
                                    }
                                }
                                case BIFFRECORDTYPE.XF_V2:
                                {
                                    goto label_37;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            switch (id)
                            {
                                case BIFFRECORDTYPE.BOUNDSHEET:
                                {
                                    var refSheet = (XlsBiffBoundSheet)xlsBiffRecord;
                                    if (refSheet.Type == XlsBiffBoundSheet.SheetType.Worksheet)
                                    {
                                        refSheet.IsV8 = IsV8();
                                        refSheet.UseEncoding = m_encoding;
                                        LogManager.Log(this).Debug("BOUNDSHEET IsV8={0}", (object)refSheet.IsV8);
                                        m_sheets.Add(new XlsWorksheet(m_globals.Sheets.Count, refSheet));
                                        m_globals.Sheets.Add(refSheet);
                                    }
                                    continue;
                                }
                                case BIFFRECORDTYPE.COUNTRY:
                                {
                                    m_globals.Country = xlsBiffRecord;
                                    continue;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    else if ((uint)id <= byte.MaxValue)
                    {
                        if ((uint)id <= 225U)
                        {
                            switch (id)
                            {
                                case BIFFRECORDTYPE.MMS:
                                {
                                    m_globals.MMS = xlsBiffRecord;
                                    continue;
                                }
                                case BIFFRECORDTYPE.XF:
                                {
                                    goto label_37;
                                }
                                case BIFFRECORDTYPE.INTERFACEHDR:
                                {
                                    m_globals.InterfaceHdr = (XlsBiffInterfaceHdr)xlsBiffRecord;
                                    continue;
                                }
                                default:
                                {
                                    continue;
                                }
                            }
                        }
                        switch (id)
                        {
                            case BIFFRECORDTYPE.SST:
                            {
                                m_globals.SST = (XlsBiffSST)xlsBiffRecord;
                                flag = true;
                                continue;
                            }
                            case BIFFRECORDTYPE.EXTSST:
                            {
                                m_globals.ExtSST = xlsBiffRecord;
                                flag = false;
                                continue;
                            }
                            default:
                            {
                                continue;
                            }
                        }
                    }
                    else if ((uint)id <= 561U)
                    {
                        if (id == BIFFRECORDTYPE.PROT4REVPASSWORD || id != BIFFRECORDTYPE.FONT_V34)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        switch (id)
                        {
                            case BIFFRECORDTYPE.XF_V3:
                            case BIFFRECORDTYPE.XF_V4:
                            {
                                goto label_37;
                            }
                            case BIFFRECORDTYPE.FORMAT:
                            {
                                var biffFormatString2 = (XlsBiffFormatString)xlsBiffRecord;
                                m_globals.Formats.Add(biffFormatString2.Index, biffFormatString2);
                                continue;
                            }
                            default:
                            {
                                continue;
                            }
                        }
                    }
                    m_globals.Fonts.Add(xlsBiffRecord);
                    continue;
                label_37:
                    m_globals.ExtendedFormats.Add(xlsBiffRecord);
                }
            }
        }

        /// <summary>Reads work sheet globals.</summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="idx">  The index.</param>
        /// <param name="row">  The row.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool ReadWorkSheetGlobals(XlsWorksheet sheet, out XlsBiffIndex idx, out XlsBiffRow row)
        {
            idx = null;
            row = null;
            m_stream.Seek((int)sheet.DataOffset, SeekOrigin.Begin);
            if (m_stream.Read() is not XlsBiffBOF xlsBiffBof || xlsBiffBof.Type != BIFFTYPE.Worksheet)
            {
                return false;
            }
            var xlsBiffRecord1 = m_stream.Read();
            if (xlsBiffRecord1 == null)
            {
                return false;
            }
            if (xlsBiffRecord1 is XlsBiffIndex)
            {
                idx = xlsBiffRecord1 as XlsBiffIndex;
            }
            else if (xlsBiffRecord1 is XlsBiffUncalced)
            {
                idx = m_stream.Read() as XlsBiffIndex;
            }
            if (idx != null)
            {
                idx.IsV8 = IsV8();
                LogManager.Log(this).Debug("INDEX IsV8={0}", (object)idx.IsV8);
            }
            XlsBiffDimensions xlsBiffDimensions = null;
            XlsBiffRecord xlsBiffRecord2;
            do
            {
                xlsBiffRecord2 = m_stream.Read();
                if (xlsBiffRecord2.ID == BIFFRECORDTYPE.DIMENSIONS)
                {
                    xlsBiffDimensions = (XlsBiffDimensions)xlsBiffRecord2;
                    break;
                }
            }
            while (xlsBiffRecord2 != null && xlsBiffRecord2.ID != BIFFRECORDTYPE.ROW);
            if (xlsBiffRecord2.ID == BIFFRECORDTYPE.ROW)
            {
                row = (XlsBiffRow)xlsBiffRecord2;
            }
            XlsBiffRow xlsBiffRow;
            XlsBiffRecord xlsBiffRecord3;
            for (xlsBiffRow = (XlsBiffRow)null;
                 xlsBiffRow == null && m_stream.Position < m_stream.Size;
                 xlsBiffRow = xlsBiffRecord3 as XlsBiffRow)
            {
                xlsBiffRecord3 = m_stream.Read();
                LogManager.Log(this)
                    .Debug(
                        "finding rowRecord offset {0}, rec: {1}",
                        (object)xlsBiffRecord3.Offset,
                        (object)xlsBiffRecord3.ID);
                if (xlsBiffRecord3 is XlsBiffEOF)
                {
                    break;
                }
            }
            if (xlsBiffRow != null)
            {
                LogManager.Log(this).Debug(
                    "Got row {0}, rec: id={1},rowindex={2}, rowColumnStart={3}, rowColumnEnd={4}",
                    (object)xlsBiffRow.Offset,
                    (object)xlsBiffRow.ID,
                    (object)xlsBiffRow.RowIndex,
                    (object)xlsBiffRow.FirstDefinedColumn,
                    (object)xlsBiffRow.LastDefinedColumn);
            }
            row = xlsBiffRow;
            if (xlsBiffDimensions != null)
            {
                xlsBiffDimensions.IsV8 = IsV8();
                LogManager.Log(this).Debug("dims IsV8={0}", (object)xlsBiffDimensions.IsV8);
                FieldCount = xlsBiffDimensions.LastColumn - 1;
                if (FieldCount <= 0 && xlsBiffRow != null)
                {
                    FieldCount = xlsBiffRow.LastDefinedColumn;
                }
                m_maxRow = (int)xlsBiffDimensions.LastRow;
                sheet.Dimensions = xlsBiffDimensions;
            }
            else
            {
                FieldCount = 256;
                m_maxRow = (int)idx.LastExistingRow;
            }
            if (idx != null && idx.LastExistingRow <= idx.FirstExistingRow || row == null)
            {
                return false;
            }
            Depth = 0;
            return true;
        }

        /// <summary>Reads work sheet row.</summary>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private bool ReadWorkSheetRow()
        {
            m_cellsValues = new object[FieldCount];
            while (m_cellOffset < m_stream.Size)
            {
                var xlsBiffRecord = m_stream.ReadAt(m_cellOffset);
                m_cellOffset += xlsBiffRecord.Size;
                switch (xlsBiffRecord)
                {
                    case XlsBiffDbCell _:
                    {
                        goto label_8;
                    }
                    case XlsBiffEOF _:
                    {
                        return false;
                    }
                    case XlsBiffBlankCell cell:
                    {
                        if (cell.ColumnIndex < FieldCount)
                        {
                            if (cell.RowIndex != Depth)
                            {
                                m_cellOffset -= xlsBiffRecord.Size;
                                goto label_8;
                            }
                            PushCellValue(cell);
                            continue;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    default:
                    {
                        continue;
                    }
                }
            }
        label_8:
            ++Depth;
            return Depth < m_maxRow;
        }

        /// <summary>Try convert oa date time.</summary>
        /// <param name="value">  The value.</param>
        /// <param name="XFormat">Describes the format to use.</param>
        /// <returns>An object.</returns>
        private object TryConvertOADateTime(double value, ushort XFormat)
        {
            ushort key;
            if (XFormat >= 0 && XFormat < m_globals.ExtendedFormats.Count)
            {
                var extendedFormat = m_globals.ExtendedFormats[XFormat];
                switch (extendedFormat.ID)
                {
                    case BIFFRECORDTYPE.XF_V2:
                    {
                        key = (ushort)(extendedFormat.ReadByte(2) & 63U);
                        break;
                    }
                    case BIFFRECORDTYPE.XF_V3:
                    {
                        if ((extendedFormat.ReadByte(3) & 4) == 0)
                        {
                            return value;
                        }
                        key = extendedFormat.ReadByte(1);
                        break;
                    }
                    case BIFFRECORDTYPE.XF_V4:
                    {
                        if ((extendedFormat.ReadByte(5) & 4) == 0)
                        {
                            return value;
                        }
                        key = extendedFormat.ReadByte(1);
                        break;
                    }
                    default:
                    {
                        if ((extendedFormat.ReadByte(m_globals.Sheets[^1].IsV8 ? 9 : 7) & 4) == 0)
                        {
                            return value;
                        }
                        key = extendedFormat.ReadUInt16(2);
                        break;
                    }
                }
            }
            else
            {
                key = XFormat;
            }
            switch (key)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 48:
                {
                    return value;
                }
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 45:
                case 46:
                case 47:
                {
                    return Helpers.ConvertFromOATime(value);
                }
                case 49:
                {
                    return value.ToString();
                }
                default:
                {
                    if (m_globals.Formats.TryGetValue(key, out var biffFormatString))
                    {
                        var str = biffFormatString.Value;
                        if (new FormatReader { FormatString = str }.IsDateFormatString())
                        {
                            return Helpers.ConvertFromOATime(value);
                        }
                    }
                    return value;
                }
            }
        }

        /// <summary>Try convert oa date time.</summary>
        /// <param name="value">  The value.</param>
        /// <param name="XFormat">Describes the format to use.</param>
        /// <returns>An object.</returns>
        private object TryConvertOADateTime(object value, ushort XFormat)
        {
            return double.TryParse(value.ToString(), out var result) ? TryConvertOADateTime(result, XFormat) : value;
        }
    }
}
