// <copyright file="GoogleImporter.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the google importer class</summary>
namespace Clarity.Ecommerce.Providers.Importer.GoogleSheet
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using Google.GData.Client;
    using Google.GData.Spreadsheets;
    using Importer;
    using Interfaces.Providers.Importer;

    /// <content>A google sheet importer provider.</content>
    public partial class GoogleSheetImporterProvider
    {
        /// <summary>Identifier for the Google client.</summary>
        private string googleClientId;

        /// <summary>The Google access token.</summary>
        private string googleAccessToken;

        /// <summary>Gets or sets the identifier of the Google client.</summary>
        /// <value>The identifier of the Google client.</value>
        /// <seealso cref="ImporterProviderBase.GoogleClientId"/>
        public override string GoogleClientId
        {
            get => googleClientId;
            set
            {
                googleClientId = value;
                SetupSpreadsheetService();
            }
        }

        /// <summary>Gets or sets the Google access token.</summary>
        /// <value>The Google access token.</value>
        /// <seealso cref="ImporterProviderBase.GoogleAccessToken"/>
        public override string GoogleAccessToken
        {
            get => googleAccessToken;
            set
            {
                googleAccessToken = value;
                SetupSpreadsheetService();
            }
        }

        /// <summary>Gets or sets the sheet.</summary>
        /// <value>The sheet.</value>
        private AtomEntry Sheet { get; set; }

        /// <inheritdoc/>
        public override Task<List<string>> ReadWorkbookHeaderInfoAsync(string fileName, string? contextProfileName)
        {
            OpenReader(fileName);
            var items = new List<string>();
            // Get the data from google
            var cellQuery = new CellQuery(Sheet.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null).HRef.ToString());
            var cellFeed = service.Query(cellQuery);
            for (var i = 0; i < cellFeed.Entries.Count; i++)
            {
                var atomEntryAsCell = (CellEntry)cellFeed.Entries[i];
                // Process Column header row cells only, 1-based index
                if (atomEntryAsCell.Row != 1)
                {
                    continue;
                }
                var cell = new Cell
                {
                    Value = atomEntryAsCell.Value ?? string.Empty,
                    Column = (uint)i,
                };
                if (string.IsNullOrWhiteSpace(atomEntryAsCell.Cell.Value))
                {
                    continue;
                }
                var nextColName = i == cellFeed.Entries.Count - 1
                    ? string.Empty
                    : ((CellEntry)cellFeed.Entries[i + 1]).Cell.Value;
                if (nextColName == $"{cell.Value} UofM")
                {
                    ++i;
                }
                items.Add(atomEntryAsCell.Cell.Value);
            }
            return Task.FromResult(items);
        }

        /// <inheritdoc/>
        protected override Task ReadCellDataAsync(string fileName, string? contextProfileName)
        {
            OpenReader(fileName);
            // Get the data from google
            var cellQuery = new CellQuery(Sheet.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null).HRef.ToString());
            var cellFeed = service.Query(cellQuery);
            // Initialize the array of rows
            var rowList = new Row[cellFeed.RowCount.IntegerValue + 1];
            // Entries come in the order from upper-left to bottom-right.  The upper left cell of a spreadsheet is 1,1.
            // The columns are assumed to be the first row
            var entriesByRow = cellFeed.Entries
                .Cast<CellEntry>()
                .OrderBy(x => x.Row)
                .ThenBy(x => x.Column)
                .GroupBy(x => x.Row)
                .Select(er => er.ToList())
                .Where(er => !er.All(e => string.IsNullOrWhiteSpace(e.Value)));
            foreach (var entryRow in entriesByRow)
            {
                for (var columnIndex = 0; columnIndex <= entryRow.Max(x => x.Column); ++columnIndex)
                {
                    var atomEntryAsCell = entryRow.Find(x => x.Column == columnIndex);
                    if (atomEntryAsCell == null)
                    {
                        continue;
                    }
                    var cell = new Cell
                    {
                        Value = atomEntryAsCell.Value ?? string.Empty,
                        Column = (uint)columnIndex - 1,
                    };
                    // Column header row
                    if (atomEntryAsCell.Row == 1)
                    {
                        if (string.IsNullOrWhiteSpace(atomEntryAsCell.Cell.Value))
                        {
                            continue;
                        }
                        // Make sure this column name is unique
                        if (ColumnByName[fileName].ContainsKey(atomEntryAsCell.Cell.Value))
                        {
                            throw new Exception(
                                $"There are multiple columns with the same name: '{atomEntryAsCell.Cell.Value}' Column is {cell.Column}");
                        }
                        var nextColName = columnIndex == entryRow.Max(x => x.Column)
                            ? string.Empty
                            : entryRow.Find(x => x.Column == columnIndex + 1)?.Cell.Value;
                        if (nextColName == $"{cell.Value} UofM")
                        {
                            if (ColumnByNameUofMs[fileName].ContainsKey(nextColName))
                            {
                                throw new Exception(
                                    $"There are multiple columns with the same name: '{cell.Value}' Column is {cell.Column}");
                            }
                            ColumnByNameUofMs[fileName][nextColName] = cell.Column;
                            ++columnIndex;
                        }
                        ColumnByName[fileName][atomEntryAsCell.Cell.Value] = cell.Column;
                        continue;
                    }
                    // Data row
                    var row = rowList[atomEntryAsCell.Row] ?? new Row(this);
                    if (ColumnByNameUofMs[fileName].ContainsValue(cell.Column))
                    {
                        cell.UofM = entryRow.First(x => x.Column == columnIndex + 1).Cell.Value;
                        row.EntryByColumnUofM[cell.Column] = cell;
                        ++columnIndex;
                    }
                    row.EntryByColumn[cell.Column] = new Cell { Value = cell.Value };
                    rowList[atomEntryAsCell.Row] = row;
                }
            }
            Rows[fileName] = rowList;
            return Task.CompletedTask;
        }

        /// <summary>Sets up the spreadsheet service.</summary>
        /// <param name="googleClientID">   Identifier for the google client.</param>
        /// <param name="googleAccessToken">The google access token.</param>
        /// <returns>A SpreadsheetsService.</returns>
        private static SpreadsheetsService SetupSpreadsheetService(string googleClientID, string googleAccessToken)
        {
            return new SpreadsheetsService("Clarity Ecommerce Platform Importer")
            {
                RequestFactory = new GOAuth2RequestFactory(
                    null,
                    "Clarity Ecommerce Platform Importer",
                    new OAuth2Parameters
                    {
                        ClientId = googleClientID,
                        ClientSecret = ConfigurationManager.AppSettings["google_api:importer:client_secret"],
                        RedirectUri = "urn:ietf:wg:oauth:2.0:oob",
                        Scope = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds",
                        AccessToken = googleAccessToken,
                        ////RefreshToken = google_refresh_token,
                    }),
            };
        }

        /// <summary>Opens a reader.</summary>
        /// <param name="fileName">Filename of the file.</param>
        private void OpenReader(string fileName)
        {
            var query = new WorksheetQuery(fileName);
            var feed = service.Query(query);
            Sheet = feed.Entries[0];
        }

        /// <summary>Sets up the spreadsheet service.</summary>
        private void SetupSpreadsheetService()
        {
            if (string.IsNullOrWhiteSpace(GoogleClientId) || string.IsNullOrWhiteSpace(GoogleAccessToken))
            {
                return;
            }
            service = SetupSpreadsheetService(GoogleClientId, GoogleAccessToken);
        }
    }
}
