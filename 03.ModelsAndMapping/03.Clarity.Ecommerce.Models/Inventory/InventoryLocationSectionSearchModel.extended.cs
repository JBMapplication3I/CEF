// <copyright file="InventoryLocationSectionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location section search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the inventory location section search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IInventoryLocationSectionSearchModel"/>
    public partial class InventoryLocationSectionSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(InventoryLocationName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Inventory Location Name")]
        public string? InventoryLocationName { get; set; }
    }
}
