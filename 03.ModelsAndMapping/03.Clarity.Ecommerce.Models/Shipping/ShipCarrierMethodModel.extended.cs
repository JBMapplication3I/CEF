// <copyright file="ShipCarrierMethodModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship carrier method model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the ship carrier method.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IShipCarrierMethodModel"/>
    public partial class ShipCarrierMethodModel
    {
        /// <inheritdoc/>
        public int ShipCarrierID { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierKey { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierName { get; set; }

        /// <inheritdoc cref="IShipCarrierMethodModel.ShipCarrier"/>
        public ShipCarrierModel? ShipCarrier { get; set; }

        /// <inheritdoc/>
        IShipCarrierModel? IShipCarrierMethodModel.ShipCarrier { get => ShipCarrier; set => ShipCarrier = (ShipCarrierModel?)value; }
    }
}
