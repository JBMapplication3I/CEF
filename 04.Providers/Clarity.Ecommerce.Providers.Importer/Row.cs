// <copyright file="Row.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the row class</summary>
namespace Clarity.Ecommerce.Providers.Importer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers.Importer;

    /// <summary>A row.</summary>
    /// <seealso cref="IRow"/>
    public class Row : IRow
    {
        /// <summary>Initializes a new instance of the <see cref="Row"/> class.</summary>
        /// <param name="sheet">The sheet.</param>
        public Row(IImporterProviderBase sheet)
        {
            Sheet = sheet;
            EntryByColumn = new();
            EntryByColumnUofM = new();
        }

        /// <inheritdoc/>
        public Dictionary<uint, Cell> EntryByColumn { get; }

        /// <inheritdoc/>
        public Dictionary<uint, Cell> EntryByColumnUofM { get; }

        /// <inheritdoc/>
        public IImporterProviderBase Sheet { get; }

        /// <inheritdoc/>
        public Task<Cell?> EntryByColumnNameAsync(string fileName, string columnName)
        {
            if (Sheet.ColumnByName[fileName].ContainsKey(columnName))
            {
                var col = Sheet.ColumnByName[fileName][columnName];
                if (EntryByColumn.Any(x => x.Key == col))
                {
                    return Task.FromResult<Cell?>(EntryByColumn.First(x => x.Key == col).Value);
                }
            }
            var altColumnName = columnName.Replace("UnitOfMeasure", "UofM").Replace("Unit Of Measure", "UofM").Replace("Unit of Measure", "UofM");
            // ReSharper disable once InvertIf
            if (Sheet.ColumnByNameUofMs[fileName].ContainsKey(altColumnName))
            {
                var col = Sheet.ColumnByNameUofMs[fileName][altColumnName];
                if (EntryByColumnUofM.Any(x => x.Key == col))
                {
                    return Task.FromResult<Cell?>(EntryByColumnUofM.First(x => x.Key == col).Value);
                }
            }
            return Task.FromResult<Cell?>(null);
        }

        /// <inheritdoc/>
        public Task<Cell?> EntryByMappingFieldAsync(string fileName, Enums.ProductImportFieldEnum field)
        {
            var columnName = Sheet.ImportMappings[fileName].Keys.FirstOrDefault(k => Sheet.ImportMappings[fileName][k] == field)
                ?? field.ToString().SplitCamelCase().Replace("Unit Of Measure", "UofM").Replace("Unit of Measure", "UofM");
            return EntryByColumnNameAsync(fileName, columnName);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Cell?>> EntriesByMappingFieldAsync(string fileName, Enums.ProductImportFieldEnum field)
        {
            var columnNames = Sheet.ImportMappings[fileName].Keys.Where(k => Sheet.ImportMappings[fileName][k] == field);
            return await Task.WhenAll(columnNames.Select(x => EntryByColumnNameAsync(fileName, x))).ConfigureAwait(false);
        }
    }
}
