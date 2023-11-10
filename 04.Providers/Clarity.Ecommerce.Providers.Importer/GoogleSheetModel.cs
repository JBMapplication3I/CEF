// <copyright file="GoogleSheetModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the google sheet model class</summary>
namespace Clarity.Ecommerce.Models.Import
{
    using Interfaces.Models.Import;

    /// <summary>A data Model for the Google sheet.</summary>
    /// <seealso cref="IGoogleSheetModel"/>
    public class GoogleSheetModel : IGoogleSheetModel
    {
        /// <inheritdoc/>
        public string? GoogleClientID { get; set; }

        /// <inheritdoc/>
        public string? GoogleAccessToken { get; set; }

        /// <inheritdoc/>
        public string? SheetID { get; set; }

        /// <inheritdoc/>
        public string? HttpImagePath { get; set; }
    }
}
