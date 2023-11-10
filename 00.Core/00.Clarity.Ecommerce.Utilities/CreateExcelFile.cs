// <copyright file="CreateExcelFile.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create excel file class</summary>
// ReSharper disable InvertIf, PossiblyMistakenUseOfParamsMethod, UnusedMember.Global
#define INCLUDE_WEB_FUNCTIONS
#if !NET5_0_OR_GREATER
namespace ExportToExcel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Clarity.Ecommerce.Models;
    using Clarity.Ecommerce.Utilities;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Drawing.Spreadsheet;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using ServiceStack.Web;

    /// <summary>
    /// November 2013 http://www.mikesknowledgebase.com
    /// Note: if you plan to use this in an ASP.Net application, remember to add a reference to "System.Web",
    /// and to un-comment the "INCLUDE_WEB_FUNCTIONS" definition at the top of this file.
    /// Release history
    ///  - Nov 2013:
    ///     Changed "CreateExcelDocument(DataTable dt, string xlsxFilePath)" to remove the DataTable from the
    ///     DataSet after creating the Excel file. You can now create an Excel file via a Stream (making it
    ///     more ASP.Net friendly)
    ///  - Jan 2013: Fix: Couldn't open .xlsx files using OLEDB (was missing "WorkbookStylesPart" part)
    ///  - Nov 2012:
    ///     List's with Nullable columns weren't be handled properly. If a value in a numeric column doesn't
    ///     have any data, don't write anything to the Excel file (previously, it'd write a '0')
    ///  - Jul 2012: Fix: Some worksheets weren't exporting their numeric data properly, causing "Excel found
    ///    unreadable content in '___.xlsx'" errors.
    ///  - Mar 2012: Fixed issue, where Microsoft.ACE.OLEDB.12.0 wasn't able to connect to the Excel files created
    ///    using this class.
    /// </summary>
    public static class CreateExcelFile
    {
        private const string InvalidChars = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";

        /// <summary>Creates excel document.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="list">        The list.</param>
        /// <param name="xlsxFilePath">Full pathname of the XLSX file.</param>
        /// <returns>The new excel document.</returns>
        public static CEFActionResponse CreateExcelDocument<T>(List<T> list, string xlsxFilePath)
        {
            var ds = new DataSet();
            ds.Tables.Add(ListToDataTable(list));
            return CreateExcelDocument(ds, xlsxFilePath);
        }

        /// <summary>This function is adapted from: http://www.codeguru.com/forum/showthread.php?t=450171 My thanks to
        /// Carl Quirion, for making it "nullable-friendly".</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="list">The list.</param>
        /// <returns>A DataTable.</returns>
        // ReSharper disable once MemberCanBePrivate.Global
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            var dt = new DataTable();
            foreach (var info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
            }
            foreach (var t in list)
            {
                var row = dt.NewRow();
                foreach (var info in typeof(T).GetProperties())
                {
                    if (!IsNullableType(info.PropertyType))
                    {
                        row[info.Name] = info.GetValue(t, null);
                    }
                    else
                    {
                        row[info.Name] = info.GetValue(t, null) ?? DBNull.Value;
                    }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>Creates excel document.</summary>
        /// <param name="dt">          The dt.</param>
        /// <param name="xlsxFilePath">Full pathname of the XLSX file.</param>
        /// <returns>The new excel document.</returns>
        public static CEFActionResponse CreateExcelDocument(DataTable dt, string xlsxFilePath)
        {
            Contract.RequiresNotNull(dt);
            var ds = new DataSet();
            ds.Tables.Add(dt);
            var result = CreateExcelDocument(ds, xlsxFilePath);
            ds.Tables.Remove(dt);
            return result;
        }

#if INCLUDE_WEB_FUNCTIONS
        /// <summary>Create an Excel file, and write it out to a MemoryStream (rather than directly to a file).</summary>
        /// <param name="dt">      DataTable containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="response">HttpResponse of the current page.</param>
        /// <returns>True if it was created successfully, otherwise false.</returns>
        public static bool CreateExcelDocument(DataTable dt, string filename, IResponse response)
        {
            Contract.RequiresNotNull(dt);
            Contract.RequiresNotNull(response);
            try
            {
                var ds = new DataSet();
                ds.Tables.Add(dt);
                CreateExcelDocumentAsStream(ds, filename, response);
                ds.Tables.Remove(dt);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        /// <summary>Creates excel document.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="list">    The list.</param>
        /// <param name="filename">Filename of the file.</param>
        /// <param name="response">The response.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CreateExcelDocument<T>(List<T> list, string filename, IResponse response)
        {
            Contract.RequiresNotNull(response);
            try
            {
                var ds = new DataSet();
                ds.Tables.Add(ListToDataTable(list));
                CreateExcelDocumentAsStream(ds, filename, response);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                return false;
            }
        }

        /// <summary>Create an Excel file, and write it out to a MemoryStream (rather than directly to a file).</summary>
        /// <param name="ds">      DataSet containing the data to be written to the Excel.</param>
        /// <param name="filename">The filename (without a path) to call the new Excel file.</param>
        /// <param name="response">HttpResponse of the current page.</param>
        public static void CreateExcelDocumentAsStream(DataSet ds, string filename, IResponse response)
        {
            Contract.RequiresNotNull(ds);
            Contract.RequiresNotNull(response);
            try
            {
                var stream = new MemoryStream();
                using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(ds, document);
                }
                stream.Flush();
                stream.Position = 0;
                response.UseBufferedStream = true;
                // NOTE: If you get an "HttpCacheability does not exist" error on the following line, make sure you have
                // manually added System.Web to this project's References.
                ////response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                response.AddHeader("content-disposition", "attachment; filename=" + filename);
                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var data1 = new byte[stream.Length];
                stream.Read(data1, 0, data1.Length);
                stream.Close();
                response.OutputStream.Write(data1, 0, data1.Length);
                response.Flush();
                response.End();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Failed, exception thrown: " + ex.Message);
            }
        }
#endif //  End of "INCLUDE_WEB_FUNCTIONS" section

        /// <summary>Create an Excel file, and write it to a file.</summary>
        /// <param name="ds">           DataSet containing the data to be written to the Excel.</param>
        /// <param name="excelFilename">Name of file to be written.</param>
        /// <returns>True if successful, false if something went wrong.</returns>
        public static CEFActionResponse CreateExcelDocument(DataSet ds, string excelFilename)
        {
            try
            {
                Contract.RequiresNotNull(ds);
                var parts = excelFilename.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                var builtPath = string.Empty;
                foreach (var part in parts)
                {
                    // If extension can be parsed, it is not an extension. Do not break for that case
                    var extension = Path.GetExtension(part);
                    if (!string.IsNullOrEmpty(extension) && !int.TryParse(extension.Replace(".", string.Empty), out _))
                    {
                        break;
                    }
                    builtPath += (builtPath != string.Empty ? "\\" : string.Empty) + part;
                    if (!Directory.Exists(builtPath))
                    {
                        Directory.CreateDirectory(builtPath);
                    }
                }
                using (var document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(ds, document);
                }
                return CEFAR.PassingCEFAR();
            }
            catch
            {
                return CEFAR.FailingCEFAR("Unable to create excel document");
            }
        }

        /// <summary>Writes an excel file.</summary>
        /// <param name="ds">         The ds.</param>
        /// <param name="spreadsheet">The spreadsheet.</param>
        public static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
        {
            // Create the Excel file contents.  This function is used when creating an Excel file either writing
            // to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet!.WorkbookPart!.Workbook = new();
            // My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));
            // If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            var workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            workbookStylesPart.Stylesheet = new();
            // Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                // For each worksheet you want to create
                ////var workSheetID = $"rId{worksheetNumber}";
                ////var worksheetName = dt.TableName;
                var newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new();
                // create sheet data
                newWorksheetPart.Worksheet.AppendChild(new SheetData());
                // save worksheet
                WriteDataTableToExcelWorksheet(dt, newWorksheetPart);
                newWorksheetPart.Worksheet.Save();
                // create the worksheet to workbook relation
                if (worksheetNumber == 1)
                {
                    spreadsheet.WorkbookPart.Workbook.AppendChild(new Sheets());
                }
                spreadsheet.WorkbookPart.Workbook.GetFirstChild<Sheets>()!.AppendChild(new Sheet
                {
                    Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = worksheetNumber,
                    Name = dt.TableName,
                });
                worksheetNumber++;
            }
            spreadsheet.WorkbookPart.Workbook.Save();
        }

        /// <summary>Writes a data table to excel worksheet.</summary>
        /// <param name="dt">           The dt.</param>
        /// <param name="worksheetPart">The worksheet part.</param>
        // ReSharper disable once CognitiveComplexity
        private static void WriteDataTableToExcelWorksheet(DataTable dt, WorksheetPart worksheetPart)
        {
            var worksheet = worksheetPart.Worksheet;
            var sheetData = worksheet.GetFirstChild<SheetData>();
            // Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            // We'll also create an array, showing which type each column of data is (Text or Numeric), so when we come to write the actual
            // cells of data, we'll know if to write Text values or Numeric cell values.
            var numberOfColumns = dt.Columns.Count;
            var isNumericColumn = new bool[numberOfColumns];
            var excelColumnNames = new string[numberOfColumns];
            for (var n = 0; n < numberOfColumns; n++)
            {
                excelColumnNames[n] = GetExcelColumnName(n);
            }
            // Create the Header row in our Excel Worksheet
            uint rowIndex = 1;
            var headerRow = new Row { RowIndex = rowIndex }; // add a row at the top of spreadsheet
            sheetData!.Append(headerRow);
            for (var colInx = 0; colInx < numberOfColumns; colInx++)
            {
                var col = dt.Columns[colInx];
                AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow);
                isNumericColumn[colInx] = col.DataType.FullName is "System.Decimal" or "System.Int32";
            }
            // Now, step through each row of data in our DataTable...
            foreach (DataRow dr in dt.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;
                var newExcelRow = new Row { RowIndex = rowIndex }; // add a row at the top of spreadsheet
                sheetData.Append(newExcelRow);
                for (var colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    var cellValue = dr.ItemArray[colInx]!.ToString()!;
                    // Create cell with data
                    if (isNumericColumn[colInx])
                    {
                        // For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                        // If this numeric value is NULL, then don't write anything to the Excel file.
                        if (double.TryParse(cellValue, out var cellNumericValue))
                        {
                            cellValue = cellNumericValue.ToString(CultureInfo.InvariantCulture);
                            AppendNumericCell(excelColumnNames[colInx] + rowIndex, cellValue, newExcelRow);
                        }
                    }
                    else if (cellValue.StartsWith("SpreadSheetImage\n"))
                    {
                        var parts = cellValue.Split('\n');
                        if (parts.Length < 2)
                        {
                            // No FileName was posted to the cell
                            continue;
                        }
                        if (!File.Exists(parts[1]))
                        {
                            // File isn't actually there, don't try to open it
                            continue;
                        }
                        try
                        {
                            using var fileStream = new FileStream(parts[1], FileMode.Open);
                            // Let's try shoving an Image into a spreadsheet and see what happens.
                            newExcelRow.CustomHeight = true;
                            newExcelRow.Height = 50;
                            AppendImageCell(
                                fileStream,
                                ////newExcelRow,
                                worksheetPart,
                                Convert.ToInt32(rowIndex - 1),
                                colInx);
                        }
                        catch
                        {
                            // nothing
                        }
                    }
                    else
                    {
                        // For text cells, just write the input data straight out to the Excel file.
                        AppendTextCell(excelColumnNames[colInx] + rowIndex, cellValue, newExcelRow);
                    }
                }
            }
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, OpenXmlElement excelRow)
        {
            // Add a new Excel Cell to our Row
            var cell = new Cell { CellReference = cellReference, DataType = CellValues.String };
            var cellValue = new CellValue
            {
                Text = ReplaceInvalidSymbolsInXml(cellStringValue),
            };
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        private static void AppendNumericCell(string cellReference, string cellStringValue, OpenXmlElement excelRow)
        {
            // Add a new Excel Cell to our Row
            var cell = new Cell { CellReference = cellReference };
            var cellValue = new CellValue
            {
                Text = ReplaceInvalidSymbolsInXml(cellStringValue),
            };
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        // http://qjpos.blogspot.com/2014/02/openxml-insert-multiple-images-into-ms.html
        private static void AppendImageCell(/*string cellReference,*/ Stream imageStream, /*OpenXmlElement excelRow,*/ WorksheetPart sheet1, int startRowIndex, int startColumnIndex)
        {
            var endRowIndex = startRowIndex + 0; // Note: the image size goes into the end row and end column
            var endColumnIndex = startColumnIndex + 0;
            var imagePartType = ImagePartType.Jpeg; // TODO: Get the type from the source file
            DrawingsPart drawingsPart1;
            ImagePart imagePart1;
            WorksheetDrawing worksheetDrawing1;
            if (sheet1.DrawingsPart == null)
            {
                drawingsPart1 = sheet1.AddNewPart<DrawingsPart>();
                imagePart1 = drawingsPart1.AddImagePart(imagePartType, sheet1.GetIdOfPart(drawingsPart1));
                worksheetDrawing1 = new();
            }
            else
            {
                drawingsPart1 = sheet1.DrawingsPart;
                imagePart1 = drawingsPart1.AddImagePart(imagePartType);
                drawingsPart1.CreateRelationshipToPart(imagePart1);
                worksheetDrawing1 = drawingsPart1.WorksheetDrawing;
            }
            var imageNumber = drawingsPart1.ImageParts.Count();
            if (imageNumber == 1)
            {
                var drawing = new Drawing { Id = drawingsPart1.GetIdOfPart(imagePart1) };
                sheet1.Worksheet.Append(drawing);
            }
            imagePart1.FeedData(imageStream);
            var twoCellAnchor1 = new TwoCellAnchor { EditAs = EditAsValues.OneCell };
            var fromMarker1 = new DocumentFormat.OpenXml.Drawing.Spreadsheet.FromMarker();
            var columnId1 = new ColumnId { Text = startColumnIndex.ToString() };
            var columnOffset1 = new ColumnOffset { Text = "38100" };
            var rowId1 = new RowId { Text = startRowIndex.ToString() };
            var rowOffset1 = new RowOffset { Text = "0" };
            fromMarker1.Append(columnId1);
            fromMarker1.Append(columnOffset1);
            fromMarker1.Append(rowId1);
            fromMarker1.Append(rowOffset1);
            var toMarker1 = new DocumentFormat.OpenXml.Drawing.Spreadsheet.ToMarker();
            var columnId2 = new ColumnId { Text = endColumnIndex.ToString() };
            var columnOffset2 = new ColumnOffset { Text = "5429250" };
            var rowId2 = new RowId { Text = endRowIndex.ToString() };
            var rowOffset2 = new RowOffset { Text = "1619250" };
            toMarker1.Append(columnId2);
            toMarker1.Append(columnOffset2);
            toMarker1.Append(rowId2);
            toMarker1.Append(rowOffset2);
            var picture1 = new DocumentFormat.OpenXml.Drawing.Spreadsheet.Picture();
            var nonVisualPictureProperties1 = new NonVisualPictureProperties();
            var nonVisualDrawingProperties1 = new NonVisualDrawingProperties
            {
                Id = new((uint)(1024 + imageNumber)),
                Name = "Picture " + imageNumber,
            };
            var nonVisualPictureDrawingProperties1 = new NonVisualPictureDrawingProperties();
            var pictureLocks1 = new DocumentFormat.OpenXml.Drawing.PictureLocks { NoChangeAspect = true };
            nonVisualPictureDrawingProperties1.Append(pictureLocks1);
            nonVisualPictureProperties1.Append(nonVisualDrawingProperties1);
            nonVisualPictureProperties1.Append(nonVisualPictureDrawingProperties1);
            var blipFill1 = new BlipFill();
            var blip1 = new DocumentFormat.OpenXml.Drawing.Blip
            {
                Embed = drawingsPart1.GetIdOfPart(imagePart1),
                CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print,
            };
            blip1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            var blipExtensionList1 = new DocumentFormat.OpenXml.Drawing.BlipExtensionList();
            var blipExtension1 = new DocumentFormat.OpenXml.Drawing.BlipExtension
            {
                Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}",
            };
            var useLocalDpi1 = new DocumentFormat.OpenXml.Office2010.Drawing.UseLocalDpi { Val = false };
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");
            blipExtension1.Append(useLocalDpi1);
            blipExtensionList1.Append(blipExtension1);
            blip1.Append(blipExtensionList1);
            var stretch1 = new DocumentFormat.OpenXml.Drawing.Stretch();
            var fillRectangle1 = new DocumentFormat.OpenXml.Drawing.FillRectangle();
            stretch1.Append(fillRectangle1);
            blipFill1.Append(blip1);
            blipFill1.Append(stretch1);
            var shapeProperties1 = new ShapeProperties();
            var transform2D1 = new DocumentFormat.OpenXml.Drawing.Transform2D();
            var offset1 = new DocumentFormat.OpenXml.Drawing.Offset { X = 1257300L, Y = 762000L };
            var extents1 = new DocumentFormat.OpenXml.Drawing.Extents { Cx = 2943225L, Cy = 2257425L };
            transform2D1.Append(offset1);
            transform2D1.Append(extents1);
            var presetGeometry1 = new DocumentFormat.OpenXml.Drawing.PresetGeometry
            {
                Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle,
            };
            var adjustValueList1 = new DocumentFormat.OpenXml.Drawing.AdjustValueList();
            presetGeometry1.Append(adjustValueList1);
            shapeProperties1.Append(transform2D1);
            shapeProperties1.Append(presetGeometry1);
            picture1.Append(nonVisualPictureProperties1);
            picture1.Append(blipFill1);
            picture1.Append(shapeProperties1);
            var clientData1 = new ClientData();
            twoCellAnchor1.Append(fromMarker1);
            twoCellAnchor1.Append(toMarker1);
            twoCellAnchor1.Append(picture1);
            twoCellAnchor1.Append(clientData1);
            worksheetDrawing1.Append(twoCellAnchor1);
            if (imageNumber == 1)
            {
                drawingsPart1.WorksheetDrawing = worksheetDrawing1;
            }
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            // Convert a zero-based column index into an Excel column reference (A, B, C.. Y, Y, AA, AB, AC... AY, AZ, B1, B2..)
            //
            //  e.g. GetExcelColumnName(00) should return "A"
            //       GetExcelColumnName(01) should return "B"
            //       GetExcelColumnName(25) should return "Z"
            //       GetExcelColumnName(26) should return "AA"
            //       GetExcelColumnName(27) should return "AB"
            //      ..etc..
            if (columnIndex < 26)
            {
                return ((char)('A' + columnIndex)).ToString();
            }
            var firstChar = (char)('A' + columnIndex / 26 - 1);
            var secondChar = (char)('A' + columnIndex % 26);
            return $"{firstChar}{secondChar}";
        }

        /// <summary>Removes invalid characters from strings to be put into excel sheets.</summary>
        /// <param name="text">String going into XML.</param>
        /// <returns>Parsed string.</returns>
        private static string ReplaceInvalidSymbolsInXml(string text)
        {
            return Regex.Replace(text, InvalidChars, string.Empty, RegexOptions.Compiled);
        }

        private static Type GetNullableType(Type t)
        {
            var returnType = t;
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                returnType = Nullable.GetUnderlyingType(t);
            }
            return returnType!;
        }

        private static bool IsNullableType(Type type)
        {
            return type == typeof(string)
                || type.IsArray
                || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
#endif
