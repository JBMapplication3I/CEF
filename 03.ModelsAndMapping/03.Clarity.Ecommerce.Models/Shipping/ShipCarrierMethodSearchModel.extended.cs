// <copyright file="ShipCarrierMethodSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship carrier method search model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the ship carrier method search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IShipCarrierMethodSearchModel"/>
    public partial class ShipCarrierMethodSearchModel
    {
        /// <inheritdoc/>
        public string? ShipCarrierName { get; set; }
    }
}
