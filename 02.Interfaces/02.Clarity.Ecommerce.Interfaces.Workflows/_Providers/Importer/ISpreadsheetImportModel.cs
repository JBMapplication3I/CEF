// <copyright file="ISpreadsheetImportModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISpreadsheetImportModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models.Import
{
    using System.IO;

    /// <summary>Interface for spreadsheet import model.</summary>
    public interface ISpreadsheetImportModel
    {
        /// <summary>Gets or sets the google sheet.</summary>
        /// <value>The google sheet.</value>
        IGoogleSheetModel? GoogleSheet { get; set; }

        /// <summary>Gets or sets the spreadsheet stream.</summary>
        /// <value>The spreadsheet stream.</value>
        Stream? SpreadsheetStream { get; set; }

        /// <summary>Gets or sets the type of the import.</summary>
        /// <value>The type of the import.</value>
        Enums.ImportType ImportType { get; set; }
    }
}
