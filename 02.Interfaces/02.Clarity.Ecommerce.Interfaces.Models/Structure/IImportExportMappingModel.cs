// <copyright file="IImportExportMappingModel.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IImportExportMappingModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for import export mapping model.</summary>
    public partial interface IImportExportMappingModel
    {
        /// <summary>Gets or sets the mapping JSON.</summary>
        /// <value>The mapping JSON.</value>
        string? MappingJson { get; set; }

        /// <summary>Gets or sets the mapping JSON hash.</summary>
        /// <value>The mapping JSON hash.</value>
        long? MappingJsonHash { get; set; }
    }
}
