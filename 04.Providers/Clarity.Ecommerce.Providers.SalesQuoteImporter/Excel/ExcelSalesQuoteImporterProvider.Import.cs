// <copyright file="ExcelSalesQuoteImporterProvider.Import.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel sales quote importer provider class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteImporter.Excel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Interfaces.Models;
    using Models;
    using Utilities;
    using Cell = DocumentFormat.OpenXml.Spreadsheet.Cell;
    using File = System.IO.File;

    public partial class ExcelSalesQuoteImporterProvider
    {
        /// <summary>Generates a column reference.</summary>
        /// <param name="col">The col.</param>
        /// <returns>The column reference.</returns>
        public static string GenColumnReference(int col)
        {
            if (col < 26)
            {
                return Alphabet[col].ToString();
            }
            if (col <= 26 * 26)
            {
                return $"{Alphabet[col / 26 - 1]}{Alphabet[col % 26]}";
            }
            if (col <= 26 * 26 * 26)
            {
                return $"{Alphabet[col / 26 / 26 - 1]}{Alphabet[col / 26 % 26]}{Alphabet[col % 26]}";
            }
            // Not going to handle this unless we actually need to
            throw new InvalidOperationException("ERROR! Too many columns in the excel file to process");
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<ISalesQuoteModel>> ImportFileAsSalesQuoteAsync(
            string? contextProfileName,
            string fileName,
            int? mappingID = null,
            string mappingKey = null,
            string mappingName = null)
        {
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            var rootPath = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(
                    Enums.FileEntityType.ImportSalesQuote)
                .ConfigureAwait(false);
            var mappingConfigResult = LoadMappingConfig(
                root: rootPath, contextProfileName: contextProfileName, mappingID: mappingID, mappingKey: mappingKey, mappingName: mappingName);
            if (!mappingConfigResult.ActionSucceeded)
            {
                return mappingConfigResult.ChangeFailingCEFARType<ISalesQuoteModel>();
            }
            var memoryContentsResult = await ProcessFileContentsToCellDatasAsync(
                    fileName,
                    mappingConfigResult.Result,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!memoryContentsResult.ActionSucceeded)
            {
                return memoryContentsResult.ChangeFailingCEFARType<ISalesQuoteModel>();
            }
            if (memoryContentsResult.Result.Count == 0)
            {
                return CEFAR.FailingCEFAR<ISalesQuoteModel>("ERROR! There was no data to load into records");
            }
            var timestamp = DateExtensions.GenDateTime;
            var firstCustomKeyMapping = mappingConfigResult.Result.Mappings
                .FirstOrDefault(x => x.Assignments
                    .Any(y => y.Entity.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Length == 1
                           && y.To.Any(z => z == "CustomKey")));
            ISalesQuoteModel coreModel = null;
            if (firstCustomKeyMapping != null)
            {
                var firstRecord = memoryContentsResult.Result.First();
                var firstCoreCustomKey = firstRecord
                    .First(x => DefaultAggregate(x.Header) == DefaultAggregate(firstCustomKeyMapping.Header))
                    .Value;
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var exists = context.SalesQuotes.FilterByActive(true).FilterByCustomKey(firstCoreCustomKey, true).Any();
                if (exists)
                {
                    coreModel = await Workflows.SalesQuotes.GetAsync(firstCoreCustomKey, contextProfileName).ConfigureAwait(false);
                }
            }
            if (coreModel == null)
            {
                coreModel = RegistryLoaderWrapper.GetInstance<ISalesQuoteModel>();
                coreModel.Active = true;
                coreModel.CreatedDate = timestamp;
            }
            var entityResult = ProcessCellDataToEntityModel(
                coreModel,
                memoryContentsResult.Result,
                mappingConfigResult.Result,
                timestamp,
                contextProfileName);
            if (!entityResult.ActionSucceeded)
            {
                return entityResult.ChangeFailingCEFARType<ISalesQuoteModel>();
            }
            var saveEntityResult = await AddAndSaveEntityAsync(entityResult.Result, contextProfileName).ConfigureAwait(false);
            if (!saveEntityResult.ActionSucceeded)
            {
                return saveEntityResult;
            }
            // ReSharper disable once InvertIf
            if (ExcelSalesQuoteImporterProviderConfig.CreateSalesGroup
                && !Contract.CheckValidID(saveEntityResult.Result.SalesGroupAsMasterID)
                && !Contract.CheckValidID(saveEntityResult.Result.SalesGroupAsResponseID))
            {
                // ReSharper disable once PossibleInvalidOperationException
                var groupResult = CreateSalesGroupForQuote(saveEntityResult.Result.ID.Value, contextProfileName);
                if (!groupResult.ActionSucceeded)
                {
                    return groupResult.ChangeFailingCEFARType<ISalesQuoteModel>();
                }
            }
            return saveEntityResult;
        }

        /// <summary>Reads and simplify sheet data.</summary>
        /// <param name="sheetData">    Information describing the sheet.</param>
        /// <param name="workbookPart"> The workbook part.</param>
        /// <param name="worksheetPart">The worksheet part.</param>
        /// <returns>The and simplify sheet data.</returns>
        private static Dictionary<int /*row*/, Dictionary<int /*col*/, CellData /*data*/>> ReadAndSimplifySheetData(
            OpenXmlElement sheetData,
            OpenXmlPartContainer workbookPart,
            WorksheetPart worksheetPart)
        {
            var rowCount = sheetData.Elements<Row>().Count();
            var maxCellCount = 0;
            var results = new Dictionary<int /*row*/, Dictionary<int /*col*/, CellData /*data*/>>();
            for (var row = 0; row < rowCount; row++)
            {
                maxCellCount = Math.Max(
                    maxCellCount,
                    sheetData.Elements<Row>().ToArray()[row].Elements<Cell>().Count());
                results[row] = new Dictionary<int, CellData>();
                for (var col = 0; col < maxCellCount; col++)
                {
                    var cellReference = $"{GenColumnReference(col)}{row + 1}";
                    results[row][col] = new CellData
                    {
                        Row = row,
                        Column = col,
                        CellReference = cellReference,
                        Value = GetCellValue(workbookPart, worksheetPart, cellReference),
                    };
                }
            }
            // Back-fill any rows that were shorter before we got the true column max
            for (var row = 0; row < rowCount; row++)
            {
                for (var col = 0; col < maxCellCount; col++)
                {
                    if (results[row].ContainsKey(col))
                    {
                        continue;
                    }
                    var cellReference = $"{GenColumnReference(col)}{row + 1}";
                    results[row][col] = new CellData
                    {
                        Row = row,
                        Column = col,
                        CellReference = cellReference,
                        Value = GetCellValue(workbookPart, worksheetPart, cellReference),
                    };
                }
            }
            return results;
        }

        /// <summary>Gets cell value.</summary>
        /// <param name="workbookPart"> The workbook part.</param>
        /// <param name="worksheetPart">The worksheet part.</param>
        /// <param name="addressName">  Name of the address.</param>
        /// <returns>The cell value.</returns>
        private static string GetCellValue(
            OpenXmlPartContainer workbookPart,
            WorksheetPart worksheetPart,
            string addressName)
        {
            // Use its Worksheet property to get a reference to the cell whose address matches the address you supplied
            var theCell = worksheetPart.Worksheet.Descendants<Cell>()
                .FirstOrDefault(c => c.CellReference == addressName);
            // If the cell does not exist, return an empty string.
            if (theCell == null)
            {
                return null;
            }
            var value = !string.IsNullOrWhiteSpace(theCell.CellFormula?.InnerText)
                ? theCell.CellValue.InnerText
                : theCell.InnerText;
            // If the cell represents an integer number, you are done. For dates, this code returns the serialized
            // value that represents the date. The code handles strings and Booleans individually. For shared strings,
            // the code looks up the corresponding value in the shared string table. For Booleans, the code converts
            // the value into the words TRUE or FALSE.
            if (theCell.DataType == null)
            {
                return value;
            }
            switch (theCell.DataType.Value)
            {
                case CellValues.SharedString:
                {
                    // For shared strings, look up the value in the shared strings table.
                    var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                    // If the shared string table is missing, something is wrong. Return the index that is in the cell.
                    // Otherwise, look up the correct text in the table.
                    if (stringTable != null)
                    {
                        value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
                    }
                    break;
                }
                case CellValues.Date:
                {
                    var dt = DateTime.FromOADate(double.Parse(value));
                    value = dt.ToString("O");
                    break;
                }
                case CellValues.Boolean:
                {
                    switch (value)
                    {
                        case "0":
                        {
                            value = "FALSE";
                            break;
                        }
                        default:
                        {
                            value = "TRUE";
                            break;
                        }
                    }
                    break;
                }
            }
            return value;
        }

        /// <summary>Gets sheet from work sheet.</summary>
        /// <param name="workbookPart"> The workbook part.</param>
        /// <param name="worksheetPart">The worksheet part.</param>
        /// <returns>The sheet from work sheet.</returns>
        private static Sheet GetSheetFromWorkSheet(WorkbookPart workbookPart, OpenXmlPart worksheetPart)
        {
            var relationshipId = workbookPart.GetIdOfPart(worksheetPart);
            return workbookPart.Workbook.Sheets.Elements<Sheet>()
                .FirstOrDefault(s => s.Id.HasValue && s.Id.Value == relationshipId);
        }

        /// <summary>Gets sheet data.</summary>
        /// <param name="config">       The configuration.</param>
        /// <param name="workbookPart"> The workbook part.</param>
        /// <param name="worksheetPart">The worksheet part.</param>
        /// <returns>The sheet data.</returns>
        private static SheetData GetSheetData(
            MappingConfig config,
            WorkbookPart workbookPart,
            out WorksheetPart worksheetPart)
        {
            worksheetPart = null;
            if (Contract.CheckValidKey(config.SheetName))
            {
                worksheetPart = workbookPart.WorksheetParts
                    .FirstOrDefault(x => GetSheetFromWorkSheet(workbookPart, x).Name == config.SheetName);
            }
            // ReSharper disable once InvertIf
            if (worksheetPart == null)
            {
                var index = config.SheetIndex.HasValue && config.SheetIndex.Value >= 0
                    ? config.SheetIndex.Value
                    : 0;
                if (index < workbookPart.WorksheetParts.Count())
                {
                    worksheetPart = workbookPart.WorksheetParts.ElementAt(index);
                }
            }
            return worksheetPart?.Worksheet.Elements<SheetData>().FirstOrDefault();
        }

        /// <summary>Process the headers.</summary>
        /// <param name="config">        The configuration.</param>
        /// <param name="rowSkip">       The row skip.</param>
        /// <param name="data">          The data.</param>
        /// <param name="columnSkip">    The column skip.</param>
        /// <param name="headerRowCount">Number of header rows.</param>
        /// <returns>A Dictionary{int,string[]}.</returns>
        private static Dictionary<int, string[]> ProcessHeaders(
            MappingConfig config,
            int rowSkip,
            IReadOnlyDictionary<int /*row*/, Dictionary<int /*col*/, CellData /*data*/>> data,
            int columnSkip,
            out int headerRowCount)
        {
            var headerDict = new Dictionary<int /* column */, string[] /* headerRow1, headerRow2, ... */>();
            headerRowCount = 1;
            if (Contract.CheckValidID(config.HeaderRowCount))
            {
                // ReSharper disable once PossibleInvalidOperationException
                headerRowCount = config.HeaderRowCount.Value;
            }
            for (var row = rowSkip;
                 row < data.Count && row - rowSkip < headerRowCount;
                 row++)
            {
                for (var column = columnSkip; column < data[row].Count; column++)
                {
                    if (!headerDict.ContainsKey(column) || headerDict[column] == null)
                    {
                        headerDict[column] = new string[headerRowCount];
                    }
                    headerDict[column][row - rowSkip] = data[row][column].Value;
                }
                ////if (row > 5) { break; }
            }
            return headerDict;
        }

        /// <summary>Merge header data to record data.</summary>
        /// <param name="rowSkip">       The row skip.</param>
        /// <param name="headerRowCount">Number of header rows.</param>
        /// <param name="data">          The data.</param>
        /// <param name="headerDict">    Dictionary of headers.</param>
        /// <returns>A List{CellData[]}.</returns>
        private static List<CellData[]> MergeHeaderDataToRecordData(
            int rowSkip,
            int headerRowCount,
            IReadOnlyDictionary<int /*row*/, Dictionary<int /*col*/, CellData /*data*/>> data,
            IReadOnlyDictionary<int, string[]> headerDict)
        {
            var results = new List<CellData[]>();
            for (var row = rowSkip + headerRowCount; row < data.Count; row++)
            {
                var i = 0;
                var cellDatas = new List<CellData>();
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var header in headerDict)
                {
                    var column = header.Key;
                    var cellData = new CellData
                    {
                        Row = row,
                        Column = column,
                        Header = headerDict[column],
                        HeaderOccurrence = headerDict
                            .Skip(0)
                            .Take(++i)
                            .Count(x => x.Value == headerDict[column])
                            - 1,
                    };
                    var origCellData = data[row]
                        .Single(x => x.Value.Row == cellData.Row
                                  && x.Value.Column == cellData.Column);
                    cellData.CellReference = origCellData.Value.CellReference;
                    cellData.Value = origCellData.Value.Value;
                    cellDatas.Add(cellData);
                }
                results.Add(cellDatas.ToArray());
                ////if (row > 5) { break; }
            }
            return results;
        }

        /// <summary>Adds an and save entity to 'contextProfileName'.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ISalesQuoteModel}.</returns>
        private async Task<CEFActionResponse<ISalesQuoteModel>> AddAndSaveEntityAsync(
            ISalesQuoteModel model, string? contextProfileName)
        {
            var result = await Workflows.SalesQuotes.UpsertAsync(model, contextProfileName).ConfigureAwait(false);
            return result == null
                ? CEFAR.FailingCEFAR<ISalesQuoteModel>("ERROR! Could not Upsert the Sales Quote")
                : result.WrapInPassingCEFAR();
        }

        /// <summary>Process the file contents to cell datas.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="config">            The configuration.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A list of.</returns>
        private async Task<CEFActionResponse<List<CellData[]>>> ProcessFileContentsToCellDatasAsync(
            string fileName, MappingConfig config, string? contextProfileName)
        {
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            var rootPath = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.ImportSalesQuote).ConfigureAwait(false);
            var root = EnsureRootFolder(rootPath);
            var fullPath = Path.Combine(root, fileName);
            if (!File.Exists(fullPath))
            {
                return CEFAR.FailingCEFAR<List<CellData[]>>($"ERROR! The file '{fileName}' does not exist at the expected location");
            }
            using var document = SpreadsheetDocument.Open(fullPath, false);
            var workbookPart = document.WorkbookPart;
            var sheetData = GetSheetData(config, workbookPart, out var worksheetPart);
            if (sheetData == null)
            {
                return CEFAR.FailingCEFAR<List<CellData[]>>("ERROR! Could not load the Sheet Data");
            }
            var rowSkip = CheckRowSkip(config);
            var columnSkip = CheckColumnSkip(config);
            var data = ReadAndSimplifySheetData(sheetData, workbookPart, worksheetPart);
            var headerDict = ProcessHeaders(config, rowSkip, data, columnSkip, out var headerRowCount);
            var results = MergeHeaderDataToRecordData(rowSkip, headerRowCount, data, headerDict);
            return results.Count > 0
                ? results.WrapInPassingCEFAR()
                : CEFAR.FailingCEFAR<List<CellData[]>>("ERROR! There were no records to load");
        }
    }
}
