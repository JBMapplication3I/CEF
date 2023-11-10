// <copyright file="IImportSearchResultModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImportSearchResultModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for import search result model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public interface IImportSearchResultModel : INameableBaseModel
    {
        /// <summary>Gets or sets the name of the manufacturer.</summary>
        /// <value>The name of the manufacturer.</value>
        string? ManufacturerName { get; set; }

        /// <summary>Gets or sets the name of the heater.</summary>
        /// <value>The name of the heater.</value>
        string? HeaterName { get; set; }

        /// <summary>Gets or sets the name of the model.</summary>
        /// <value>The name of the model.</value>
        string? ModelName { get; set; }

        /// <summary>Gets or sets the function.</summary>
        /// <value>The function.</value>
        string? Function { get; set; }

        /// <summary>Gets or sets the callout.</summary>
        /// <value>The callout.</value>
        string? Callout { get; set; }

        /// <summary>Gets or sets the use.</summary>
        /// <value>The use.</value>
        string? Use { get; set; }

        /// <summary>Gets or sets source documents.</summary>
        /// <value>The source documents.</value>
        List<string>? SourceDocuments { get; set; }
    }
}
