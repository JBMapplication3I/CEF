// <copyright file="IInventoryLocationSectionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IInventoryLocationSectionSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for inventory location section search model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    public partial interface IInventoryLocationSectionSearchModel
    {
        /// <summary>Gets or sets the name of the inventory location.</summary>
        /// <value>The name of the inventory location.</value>
        string? InventoryLocationName { get; set; }
    }
}
