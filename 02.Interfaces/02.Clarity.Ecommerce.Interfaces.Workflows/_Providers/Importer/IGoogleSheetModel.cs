// <copyright file="IGoogleSheetModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IGoogleSheetModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models.Import
{
    /// <summary>Interface for google sheet model.</summary>
    public interface IGoogleSheetModel
    {
        /// <summary>Gets or sets the identifier of the google client.</summary>
        /// <value>The identifier of the google client.</value>
        string? GoogleClientID { get; set; }

        /// <summary>Gets or sets the google access token.</summary>
        /// <value>The google access token.</value>
        string? GoogleAccessToken { get; set; }

        /// <summary>Gets or sets the identifier of the sheet.</summary>
        /// <value>The identifier of the sheet.</value>
        string? SheetID { get; set; }

        /// <summary>Gets or sets the full pathname of the HTTP image file.</summary>
        /// <value>The full pathname of the HTTP image file.</value>
        string? HttpImagePath { get; set; }
    }
}
