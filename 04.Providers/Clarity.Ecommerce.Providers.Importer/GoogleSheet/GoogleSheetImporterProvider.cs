// <copyright file="GoogleSheetImporterProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the google sheet importer provider class</summary>
namespace Clarity.Ecommerce.Providers.Importer.GoogleSheet
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Google.GData.Spreadsheets;
    using Interfaces.Models.Import;
    using Models.Import;

    /// <summary>A google sheet importer provider.</summary>
    /// <seealso cref="ImporterProviderBase"/>
    public partial class GoogleSheetImporterProvider : ImporterProviderBase
    {
        /// <summary>List of names of the columns.</summary>
        private readonly Dictionary<uint, string> columnNames = new Dictionary<uint, string>();

        /// <summary>The model.</summary>
        private IGoogleSheetModel model;

        /// <summary>The service.</summary>
        private SpreadsheetsService service;

        /// <inheritdoc/>
        public override string Name => "Google";

        /// <inheritdoc/>
        public override bool HasValidConfiguration => true;

        /// <inheritdoc/>
        public override Task<bool> LoadAsync(ISpreadsheetImportModel spsModel)
        {
            try
            {
                model = spsModel.GoogleSheet;
                service = SetupSpreadsheetService(model.GoogleClientID, model.GoogleAccessToken);
                var query = new WorksheetQuery(model.SheetID);
                var feed = service.Query(query);
                Sheet = feed.Entries[0];
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                // TODO: Log
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

        /// <inheritdoc/>
        public override async Task<IEnumerable<IImportItem>> ParseAsync(string? contextProfileName)
        {
            return await ReadCellDataBAsync(contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Reads cell data b.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The cell data b.</returns>
        private async Task<List<IImportItem>> ReadCellDataBAsync(string? contextProfileName)
        {
            // Get the data from google
            var cellQuery = new CellQuery(Sheet.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null).HRef.ToString());
            var cellFeed = service.Query(cellQuery);
            var currentRow = 1u;
            var currentItem = new ImportItem { Fields = new List<IImportField>() };
            var items = new List<IImportItem>();
            foreach (var atomEntry in cellFeed.Entries)
            {
                var cell = (CellEntry)atomEntry;
                // Column header row?
                if (cell.Row == 1)
                {
                    // Make sure this column name is unique
                    if (!columnNames.ContainsKey(cell.Column))
                    {
                        columnNames.Add(cell.Column, cell.Cell.Value);
                    }
                    continue;
                }
                if (cell.Row != currentRow)
                {
                    items.Add(currentItem);
                    currentItem = new ImportItem { Fields = new List<IImportField>() };
                    currentRow = cell.Row;
                }
                var columnName = columnNames[cell.Column];
                var value = cell.Cell.Value;
                switch (columnName)
                {
                    case "Images":
                    case "Image":
                    case "ImageNew":
                    {
                        await DownloadImagesAsync(value, currentItem, contextProfileName).ConfigureAwait(false);
                        continue;
                    }
                    default:
                    {
                        currentItem.Fields.Add(new ImportField { Name = columnName, Value = value });
                        continue;
                    }
                }
            }
            return items;
        }
    }
}
