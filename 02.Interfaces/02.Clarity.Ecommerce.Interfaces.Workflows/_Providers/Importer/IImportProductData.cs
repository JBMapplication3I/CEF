// <copyright file="IImportProductData.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImportProductData interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for import product data.</summary>
    public interface IImportProductData
    {
        /// <summary>Gets or sets the type of the import.</summary>
        /// <value>The type of the import.</value>
        string ImportType { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        string Manufacturer { get; set; }

        /// <summary>Gets or sets the heater.</summary>
        /// <value>The heater.</value>
        string Heater { get; set; }

        /// <summary>Gets or sets source document.</summary>
        /// <value>The source document.</value>
        string SourceDocument { get; set; }
    }
}
