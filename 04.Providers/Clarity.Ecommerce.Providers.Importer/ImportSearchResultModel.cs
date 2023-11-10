// <copyright file="ImportSearchResultModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import search result model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using Interfaces.Models;

    /// <summary>A data Model for the import search result.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IImportSearchResultModel"/>
    public class ImportSearchResultModel : NameableBaseModel, IImportSearchResultModel
    {
        /// <inheritdoc/>
        public string? ManufacturerName { get; set; }

        /// <inheritdoc/>
        public string? HeaterName { get; set; }

        /// <inheritdoc/>
        public string? ModelName { get; set; }

        /// <inheritdoc/>
        public string? Function { get; set; }

        /// <inheritdoc/>
        public string? Callout { get; set; }

        /// <inheritdoc/>
        public string? Use { get; set; }

        /// <inheritdoc/>
        public List<string>? SourceDocuments { get; set; }
    }
}
