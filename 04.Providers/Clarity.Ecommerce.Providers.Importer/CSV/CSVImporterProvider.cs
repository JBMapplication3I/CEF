// <copyright file="CSVImporterProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CSV importer provider class</summary>
namespace Clarity.Ecommerce.Providers.Importer.CSV
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Interfaces.Models.Import;
    using Models.Import;

    /// <summary>A CSV importer provider.</summary>
    /// <seealso cref="ImporterProviderBase"/>
    public class CSVImporterProvider : ImporterProviderBase
    {
        /// <summary>The CSV stream.</summary>
        private Stream? csvStream;

        /// <inheritdoc/>
        public override string Name => "CSV";

        /// <inheritdoc/>
        public override bool HasValidConfiguration { get; } = true;

        /// <inheritdoc/>
        public override Task<bool> LoadAsync(ISpreadsheetImportModel spsModel)
        {
            csvStream = spsModel.SpreadsheetStream;
            return Task.FromResult(true);
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<IImportItem>?> ParseAsync(string? contextProfileName)
        {
            var items = new List<IImportItem>();
            using (var reader = new StreamReader(csvStream!))
            {
                var doHeaders = true;
                string[]? headers = null;
                var line = await reader.ReadLineAsync().ConfigureAwait(false);
                if (string.IsNullOrEmpty(line))
                {
                    return items;
                }
                do
                {
                    if (doHeaders)
                    {
                        headers = line.Split(';');
                        doHeaders = false;
                        line = await reader.ReadLineAsync().ConfigureAwait(false);
                        continue;
                    }
                    var values = line.Split(';');
                    var item = new ImportItem
                    {
                        Fields = new(),
                    };
                    var count = 0;
                    // Cannot match value and headers if not all columns have data
                    if (values.Length != headers!.Length)
                    {
                        var joinStr = string.Join(";", values);
                        ParsingErrorList.Add($"Error While parsing line : {joinStr}");
                        line = await reader.ReadLineAsync().ConfigureAwait(false);
                        continue;
                    }
                    foreach (var value in values)
                    {
                        if (count < headers.Length)
                        {
                            var columnName = headers[count];
                            switch (columnName)
                            {
                                case "Images":
                                case "Image":
                                case "ImageNew":
                                {
                                    await DownloadImagesAsync(value, item, contextProfileName).ConfigureAwait(false);
                                    continue;
                                }
                                default:
                                {
                                    item.Fields.Add(new ImportField
                                    {
                                        Name = columnName,
                                        Value = value,
                                    });
                                    continue;
                                }
                            }
                        }
                        ++count;
                    }
                    items.Add(item);
                    line = await reader.ReadLineAsync().ConfigureAwait(false);
                }
                while (!string.IsNullOrEmpty(line));
            }
            return items;
        }

        /// <inheritdoc/>
        public override Task<List<string>> ReadWorkbookHeaderInfoAsync(string fileName, string? contextProfileName)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Task ReadCellDataAsync(string fileName, string? contextProfileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
