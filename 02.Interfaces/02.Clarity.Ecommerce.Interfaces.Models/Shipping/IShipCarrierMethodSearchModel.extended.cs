// <copyright file="IShipCarrierMethodSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShipCarrierMethodSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for ship carrier method search model.</summary>
    /// <seealso cref="INameableBaseSearchModel"/>
    public partial interface IShipCarrierMethodSearchModel
    {
        /// <summary>Gets or sets the name of the ship carrier.</summary>
        /// <value>The name of the ship carrier.</value>
        string? ShipCarrierName { get; set; }
    }
}
