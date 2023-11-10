// <copyright file="ExcelSalesQuoteImporterProvider.Export.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel sales quote importer provider class</summary>
// ReSharper disable PossiblyMistakenUseOfParamsMethod
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Drawing.Spreadsheet;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Interfaces.Models;
    using Models;
    using Utilities;
    using Cell = DocumentFormat.OpenXml.Spreadsheet.Cell;

    /// <summary>An excel sales quote importer provider.</summary>
    /// <seealso cref="SalesQuoteImporterProviderBase"/>
    public partial class ExcelSalesQuoteImporterProvider
    {
        /// <inheritdoc/>
        public override async Task<CEFActionResponse<object>> ExportSalesQuoteAsFileAsync(
            string? contextProfileName,
            int id,
            string format,
            int? mappingID = null,
            string mappingKey = null,
            string mappingName = null)
        {
            if (!Contract.CheckValidKey(format) || format != "xlsx")
            {
                return CEFAR.FailingCEFAR<object>($"ERROR! The excel export cannot export file type {format}");
            }
            if (Contract.CheckInvalidID(id))
            {
                return CEFAR.FailingCEFAR<object>($"ERROR! The excel export cannot use {id} to locate a record for export");
            }
            ////if (context?.SalesQuotes == null)
            ////{
            ////    return CEFAR.FailingCEFAR<object>($"ERROR! Context or DbSet was null, run {nameof(Initialize)} before calling this function.");
            ////}
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            var rootPath = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.ImportSalesQuote).ConfigureAwait(false);
            var mappingConfigResult = LoadMappingConfig(rootPath, mappingID: mappingID, mappingKey: mappingKey, mappingName: mappingName, contextProfileName: contextProfileName);
            if (!mappingConfigResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<object>("ERROR! Loading MappingConfig failed");
            }
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var quoteID = context.SalesQuotes.FilterByID(id).Select(x => x.ID).SingleOrDefault();
                Contract.RequiresValidID<ArgumentException>(quoteID, $"{id} was not found in the quote table.");
            }
            var processedDataResult = ProcessMappingIntoSheetHeadersAndData(
                mappingConfigResult.Result,
                id,
                contextProfileName,
                mappingKey == "mapping.vendor-export");
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!processedDataResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<object>("ERROR! Reading data set failed");
            }
            // We now have what is essentially a two dimensional array (it's actually dictionaries) that can be used
            // to populate a sheet by cell reference
            return CreateExcelFile(id, processedDataResult.Result, contextProfileName);
        }

        private static CEFActionResponse<ExportSheetData> ProcessMappingIntoSheetHeadersAndData(MappingConfig mappingConfig, int id, string? contextProfileName, bool mainOrAlt = true)
        {
            var exportSheetData = new ExportSheetData
            {
                ColumnSkip = mappingConfig.ColumnSkip ?? 0,
                RowSkip = mappingConfig.RowSkip ?? 0,
                HeaderRowCount = mappingConfig.HeaderRowCount ?? 1,
            };
            // Simplify the import mappings to just what we need for Exporting
            var validExportMappings = mappingConfig.Mappings
                .Where(x => x.Assignments
                    .Any(y => Contract.CheckValidKey(y.Entity)
                           && Contract.CheckAnyValidKey(y.To)))
                .Select(x => new MapFromIncomingProperty
                {
                    Header = x.Header,
                    HeaderOccurrence = x.HeaderOccurrence,
                    Ignore = x.Ignore,
                    Assignments = new List<MapToEntityProperties>
                    {
                        x.Assignments
                            .Select(y => new MapToEntityProperties
                            {
                                Entity = y.Entity,
                                Instance = y.Instance,
                                To = new[] { y.To.First(Contract.CheckValidKey) },
                            })
                            .First(y => Contract.CheckAnyValidKey(y.To)),
                    },
                })
                .ToArray();
            exportSheetData.InitializeColumns(validExportMappings);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                if (mainOrAlt)
                {
                    exportSheetData.LoadDataFromContextShort(
                        id,
                        validExportMappings,
                        // ReSharper disable once AccessToDisposedClosure
                        () => context);
                }
                else
                {
                    exportSheetData.LoadDataFromContext(
                        id,
                        validExportMappings,
                        // ReSharper disable once AccessToDisposedClosure
                        () => context);
                }
            }
            return exportSheetData.WrapInPassingCEFAR();
        }

        private static void WriteExcelFile(DataSet ds, SpreadsheetDocument spreadsheet)
        {
            // Create the Excel file contents.  This function is used when creating an Excel file either writing
            // to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new Workbook();
            // My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));
            // If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            var workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");
            var stylesheet = new Stylesheet();
            workbookStylesPart.Stylesheet = stylesheet;
            // Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            foreach (DataTable dt in ds.Tables)
            {
                // For each worksheet you want to create
                ////var workSheetID = $"rId{worksheetNumber}";
                ////var worksheetName = dt.TableName;
                var newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet();
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
                spreadsheet.WorkbookPart.Workbook.GetFirstChild<Sheets>()
                    .AppendChild(new Sheet
                    {
                        Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart),
                        SheetId = worksheetNumber,
                        Name = dt.TableName,
                    });
                worksheetNumber++;
            }
            spreadsheet.WorkbookPart.Workbook.Save();
        }

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
                excelColumnNames[n] = GenColumnReference(n);
            }
            // Create the Header row in our Excel Worksheet
            uint rowIndex = 1;
            var headerRow = new Row { RowIndex = rowIndex }; // add a row at the top of spreadsheet
            sheetData.Append(headerRow);
            for (var colInx = 0; colInx < numberOfColumns; colInx++)
            {
                var col = dt.Columns[colInx];
                AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, headerRow);
                isNumericColumn[colInx] = col.DataType.FullName == "System.Decimal" || col.DataType.FullName == "System.Int32";
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
                    var cellValue = dr.ItemArray[colInx].ToString();
                    // Create cell with data
                    if (isNumericColumn[colInx])
                    {
                        // For numeric cells, make sure our input data IS a number, then write it out to the Excel file.
                        // If this numeric value is NULL, then don't write anything to the Excel file.
                        // ReSharper disable once InvertIf
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
                            continue;
                        } // No FileName was posted to the cell
                        if (!File.Exists(parts[1]))
                        {
                            continue;
                        } // File isn't actually there, don't try to open it
                        try
                        {
                            using var fileStream = new FileStream(parts[1], FileMode.Open);
                            // Let's try shoving an Image into a spreadsheet and see what happens.
                            newExcelRow.CustomHeight = true;
                            newExcelRow.Height = 50;
                            AppendImageCell(
                                fileStream,
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
                Text = cellStringValue,
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
                Text = cellStringValue,
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
                worksheetDrawing1 = new WorksheetDrawing();
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
                Id = new UInt32Value((uint)(1024 + imageNumber)),
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
            transform2D1.Append(offset1, extents1);
            transform2D1.Append();
            var presetGeometry1 = new DocumentFormat.OpenXml.Drawing.PresetGeometry
            {
                Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle,
            };
            var adjustValueList1 = new DocumentFormat.OpenXml.Drawing.AdjustValueList();
            presetGeometry1.Append(adjustValueList1);
            shapeProperties1.Append(transform2D1, presetGeometry1);
            picture1.Append(nonVisualPictureProperties1, blipFill1, shapeProperties1);
            var clientData1 = new ClientData();
            twoCellAnchor1.Append(fromMarker1, toMarker1, picture1);
            twoCellAnchor1.Append(clientData1);
            worksheetDrawing1.Append(twoCellAnchor1);
            if (imageNumber == 1)
            {
                drawingsPart1.WorksheetDrawing = worksheetDrawing1;
            }
        }

        private CEFActionResponse<object> CreateExcelFile(int id, ExportSheetData exportSheetData, string? contextProfileName)
        {
            try
            {
                var fileName = $"SalesQuoteExport-No{id}-{DateExtensions.GenDateTime:yyyy-MM-dd-HH.mm.ss}.xlsx";
                using var dataSet = new DataSet();
                // Transcribe the data into the data-set
                using var dataTable = new DataTable();
                // Set up the ColumnSkip Columns
                for (var c = 0; c < exportSheetData.ColumnSkip; c++)
                {
                    var dataColumn = new DataColumn
                    {
                        DefaultValue = null,
                        DataType = typeof(string),
                        AllowDBNull = true,
                        ColumnName = $"Skip{c}",
                    };
                    dataTable.Columns.Add(dataColumn);
                }
                // Set up the Data Columns
                foreach (var kvp in exportSheetData.Data)
                {
                    dataTable.Columns.Add(new DataColumn
                    {
                        ColumnName = $"{kvp.Key}|{kvp.Value.ToList().OrderBy(x => x.Key).First().Value}",
                        DataType = typeof(string),
                        AllowDBNull = true,
                        DefaultValue = null,
                    });
                }
                // Set up the RowSkip Rows
                /*var emptyValues = new List<string>();
                foreach (var _ in dataTable.Columns) { emptyValues.Add(string.Empty); }
                for (var r = 0; r < exportSheetData.RowSkip; r++)
                {
                    var row = dataTable.NewRow();
                    row.ItemArray = emptyValues.ToArray<object>();
                    dataTable.Rows.Add(row);
                }*/
                // Set up the HeaderRowCount Rows
                /*for (var r = exportSheetData.RowSkip; r < exportSheetData.RowSkip + exportSheetData.HeaderRowCount; r++)
                {
                    var headers = new List<string>();
                    for (var c = 0; c < exportSheetData.ColumnSkip; c++)
                    {
                        headers.Add(null);
                    }
                    foreach (var key in exportSheetData.Data.Keys)
                    {
                        headers.Add(exportSheetData.Data[key][(uint)r] as string);
                    }
                    var row = dataTable.NewRow();
                    row.ItemArray = headers.ToArray<object>();
                    dataTable.Rows.Add(row);
                }*/
                // Add the data rows to the DataTable
                for (var r = 0 /*exportSheetData.RowSkip + exportSheetData.HeaderRowCount*/;
                     r < exportSheetData.Data.First().Value.Count;
                     r++)
                {
                    var values = new List<string>();
                    for (var c = 0; c < exportSheetData.ColumnSkip; c++)
                    {
                        values.Add(null);
                    }
                    foreach (var key in exportSheetData.Data.Keys)
                    {
                        values.Add(exportSheetData.Data[key][(uint)(r + exportSheetData.RowSkip /* - exportSheetData.HeaderRowCount*/)] as string);
                    }
                    var row = dataTable.NewRow();
                    row.ItemArray = values.ToArray<object>();
                    dataTable.Rows.Add(row);
                }
                // Add the table to the data set
                dataSet.Tables.Add(dataTable);
                // Push the data-set into the file
                var stream = new MemoryStream();
                using (var document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook, true))
                {
                    WriteExcelFile(dataSet, document);
                }
                stream.Flush();
                stream.Position = 0;
                var result = new ExcelFileResult(stream, fileName);
                return ((object)result).WrapInPassingCEFAR();
            }
            catch (Exception ex)
            {
                Logger.LogError("Export Quote to Excel File", ex.Message, ex, contextProfileName);
                return CEFAR.FailingCEFAR<object>($"ERROR! {ex.Message}");
            }
        }
    }
}
