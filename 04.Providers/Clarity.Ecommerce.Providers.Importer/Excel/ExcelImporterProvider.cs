// <copyright file="ExcelImporterProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the excel importer provider class</summary>
namespace Clarity.Ecommerce.Providers.Importer.Excel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using global::Excel;
    using Interfaces.Models.Import;
    using Interfaces.Providers.Importer;
    using Models.Import;

    /// <summary>An excel importer provider.</summary>
    /// <seealso cref="ImporterProviderBase"/>
    public class ExcelImporterProvider : ImporterProviderBase
    {
        private IExcelDataReader? excelDataReader;

        /// <inheritdoc/>
        public override bool HasValidConfiguration { get; } = true;

        /// <inheritdoc/>
        public override string Name => "Excel";

        /// <inheritdoc/>
        public override Task<bool> LoadAsync(ISpreadsheetImportModel spsModel)
        {
            switch (spsModel.ImportType)
            {
                case Enums.ImportType.XLS:
                {
                    // 1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    excelDataReader = ExcelReaderFactory.CreateBinaryReader(spsModel.SpreadsheetStream);
                    break;
                }
                case Enums.ImportType.XLSX:
                {
                    // 2. Reading from a OpenXml Excel file (2007+ format; *.xlsx)
                    excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(spsModel.SpreadsheetStream);
                    break;
                }
            }
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<string>?> GetHeadersAsync()
        {
            excelDataReader!.IsFirstRowAsColumnNames = true;
            var columnNames = new List<string>();
            excelDataReader.Read();
            for (var i = 0; i != excelDataReader.FieldCount; ++i)
            {
                columnNames.Add(excelDataReader.GetString(i));
            }
            return Task.FromResult(columnNames?.AsEnumerable());
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<IImportItem>?> ParseAsync(string? contextProfileName)
        {
            // TEST
            var items = new List<IImportItem>();
            excelDataReader!.IsFirstRowAsColumnNames = true;
            excelDataReader.AsDataSet();
            var currentRow = 0;
            // 5. Data Reader methods
            var columnNames = new string[excelDataReader.FieldCount];
            while (excelDataReader.Read())
            {
                var item = new ImportItem
                {
                    Fields = new(),
                };
                if (currentRow == 0)
                {
                    for (var i = 0; i != excelDataReader.FieldCount; ++i)
                    {
                        columnNames[i] = excelDataReader.GetString(i);
                    }
                    ++currentRow;
                    continue;
                }
                // check if it's an empty row by checking the first column
                var firstValue = excelDataReader.GetString(0);
                if (string.IsNullOrWhiteSpace(firstValue))
                {
                    continue;
                }
                var doBreak = false;
                for (var i = 0; i != excelDataReader.FieldCount; ++i)
                {
                    var columnName = columnNames[i];
                    var value = excelDataReader.GetString(i);
                    if (string.IsNullOrEmpty(value))
                    {
                        continue;
                    }
                    switch (columnName)
                    {
                        #region Client Specific
                        case "Primary Image":
                        case "PrimaryImage":
                        case "primary image":
                        case "primaryimage":
                        case "Secondary Image":
                        case "SecondaryImage":
                        case "secondary image":
                        case "secondaryimage":
                        #endregion
                        case "Images":
                        case "Image":
                        case "image":
                        case "ImageNew":
                        case "imagenew":
                        {
                            await DownloadImagesAsync(value, item, contextProfileName).ConfigureAwait(false);
                            continue;
                        }
                        default:
                        {
                            if (columnName == "Name" && value == "My Test Product")
                            {
                                doBreak = true;
                                break;
                            }
                            item.Fields.Add(new ImportField
                            {
                                Name = columnName,
                                Value = value,
                            });
                            continue;
                        }
                    }
                    break;
                }
                if (!doBreak)
                {
                    items.Add(item);
                }
                ++currentRow;
            }
            excelDataReader.Close();
            return items;
        }

        /// <inheritdoc/>
        public override async Task<List<string>> ReadWorkbookHeaderInfoAsync(string fileName, string? contextProfileName)
        {
            await OpenReaderAsync(fileName, contextProfileName).ConfigureAwait(false);
            var items = new List<string>();
            var currentRow = 0;
            while (excelDataReader!.Read())
            {
                // Process Column header row cells only, 0-based index
                if (currentRow != 0)
                {
                    continue;
                }
                for (var i = 0; i < excelDataReader.FieldCount; ++i)
                {
                    var colName = excelDataReader.GetString(i);
                    if (string.IsNullOrWhiteSpace(colName))
                    {
                        continue;
                    }
                    var nextColName = i == excelDataReader.FieldCount - 1
                        ? string.Empty
                        : excelDataReader.GetString(i + 1);
                    if (nextColName == $"{colName} UofM")
                    {
                        ++i;
                    }
                    items.Add(colName);
                }
                ++currentRow;
            }
            excelDataReader.Close();
            return items;
        }

        /// <inheritdoc/>
        protected override async Task ReadCellDataAsync(string fileName, string? contextProfileName)
        {
            var rowList = new List<Row>();
            await OpenReaderAsync(fileName, contextProfileName).ConfigureAwait(false);
            excelDataReader!.IsFirstRowAsColumnNames = true;
            var currentRow = 0;
            // Reset the dictionaries right before the read so we don't accidentally get the same data
            if (!ColumnByName.ContainsKey(fileName))
            {
                ColumnByName[fileName] = new();
            }
            if (!ColumnByNameUofMs.ContainsKey(fileName))
            {
                ColumnByNameUofMs[fileName] = new();
            }
            ColumnByName[fileName].Clear();
            ColumnByNameUofMs[fileName].Clear();
            while (excelDataReader.Read())
            {
                var row = new Row(this);
                for (var i = 0; i < excelDataReader.FieldCount; ++i)
                {
                    var cell = new Cell
                    {
                        Value = excelDataReader.GetString(i) ?? string.Empty,
                        Column = (uint)i,
                    };
                    // Column header row
                    if (currentRow == 0)
                    {
                        if (string.IsNullOrWhiteSpace(cell.Value))
                        {
                            continue;
                        }
                        // Make sure this column name is unique
                        if (ColumnByName[fileName].ContainsKey(cell.Value))
                        {
                            throw new(
                                "There are multiple columns with the same name:"
                                + $" '{cell.Value}' Column is {cell.Column}");
                        }
                        var nextColName = i == excelDataReader.FieldCount - 1
                            ? string.Empty
                            : excelDataReader.GetString(i + 1);
                        if (nextColName == $"{cell.Value} UofM")
                        {
                            if (ColumnByNameUofMs[fileName].ContainsKey(nextColName))
                            {
                                throw new(
                                    "There are multiple columns with the same name:"
                                    + $" '{cell.Value}' Column is {cell.Column}");
                            }
                            ColumnByNameUofMs[fileName].Add(nextColName, cell.Column);
                            ++i;
                        }
                        ColumnByName[fileName].Add(cell.Value, cell.Column);
                    }
                    else if (ColumnByNameUofMs[fileName].ContainsValue(cell.Column))
                    {
                        // Data row
                        cell.UofM = excelDataReader.GetString(i + 1);
                        row.EntryByColumnUofM.Add(cell.Column, cell);
                        ++i;
                    }
                    row.EntryByColumn.Add(cell.Column, cell);
                }
                rowList.Add(row);
                Rows[fileName] = rowList.ToArray();
                ++currentRow;
            }
            excelDataReader.Close();
        }

        /// <summary>Opens a reader.</summary>
        /// <param name="fileName">          Filename of the file.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task OpenReaderAsync(string fileName, string? contextProfileName)
        {
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName);
            if (provider is null)
            {
                throw new InvalidOperationException("Could load files provider");
            }
            var filePath = Path.Combine(
                await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.ImportProduct).ConfigureAwait(false),
                fileName);
            var stream = File.OpenRead(filePath);
            var extension = Path.GetExtension(fileName);
            switch (extension.ToLower())
            {
                case ".xls":
                {
                    excelDataReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    break;
                }
                case ".xlsx":
                {
                    excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    break;
                }
                default:
                {
                    throw new InvalidOperationException("Extension not supported");
                }
            }
            if (excelDataReader == null)
            {
                throw new InvalidOperationException("Could load Excel Sheet importer provider");
            }
            excelDataReader.IsFirstRowAsColumnNames = true;
            // ReSharper disable once UnusedVariable
            _ = excelDataReader.AsDataSet();
        }
    }
}
