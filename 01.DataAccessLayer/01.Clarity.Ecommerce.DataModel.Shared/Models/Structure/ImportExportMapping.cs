// <copyright file="ImportExportMapping.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the import export mapping class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface IImportExportMapping : INameableBase
    {
        /// <summary>Gets or sets the mapping JSON.</summary>
        /// <value>The mapping JSON.</value>
        string? MappingJson { get; set; }

        /// <summary>Gets or sets the mapping JSON hash.</summary>
        /// <value>The mapping JSON hash.</value>
        long? MappingJsonHash { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;

    [SqlSchema("System", "ImportExportMapping")]
    public class ImportExportMapping : NameableBase, IImportExportMapping
    {
        /// <inheritdoc/>
        [Required, DefaultValue(null)]
        public string? MappingJson { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public long? MappingJsonHash { get; set; }
    }
}
