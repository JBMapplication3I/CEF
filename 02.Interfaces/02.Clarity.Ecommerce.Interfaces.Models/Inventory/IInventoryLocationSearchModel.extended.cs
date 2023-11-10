// <copyright file="IInventoryLocationSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IInventoryLocationSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for inventory location search model.</summary>
    public partial interface IInventoryLocationSearchModel
    {
        /// <summary>Gets or sets the name of the country.</summary>
        /// <value>The name of the country.</value>
        string? CountryName { get; set; }

        /// <summary>Gets or sets the name of the state.</summary>
        /// <value>The name of the state.</value>
        string? StateName { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        string? PostalCode { get; set; }
    }
}
