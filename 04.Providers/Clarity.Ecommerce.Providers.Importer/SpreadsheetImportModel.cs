// <copyright file="SpreadsheetImportModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the spreadsheet import model class</summary>
namespace Clarity.Ecommerce.Models.Import
{
    using System.IO;
    using Interfaces.Models.Import;

    /// <summary>A data Model for the spreadsheet import.</summary>
    /// <seealso cref="ISpreadsheetImportModel"/>
    public class SpreadsheetImportModel : ISpreadsheetImportModel
    {
        /// <inheritdoc/>
        public IGoogleSheetModel? GoogleSheet { get; set; }

        /// <inheritdoc/>
        public Stream? SpreadsheetStream { get; set; }

        /// <inheritdoc/>
        public Enums.ImportType ImportType { get; set; }
    }
}
