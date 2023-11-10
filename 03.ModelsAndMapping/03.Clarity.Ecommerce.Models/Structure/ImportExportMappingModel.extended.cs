// <copyright file="ImportExportMappingModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import export mapping model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class ImportExportMappingModel
    {
        /// <inheritdoc/>
        public string? MappingJson { get; set; }

        /// <inheritdoc/>
        public long? MappingJsonHash { get; set; }
    }
}
