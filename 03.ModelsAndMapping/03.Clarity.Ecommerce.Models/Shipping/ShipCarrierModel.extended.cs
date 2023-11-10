// <copyright file="ShipCarrierModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ship carrier model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the ship carrier.</summary>
    /// <seealso cref="NameableBaseModel"/>
    /// <seealso cref="IShipCarrierModel"/>
    public partial class ShipCarrierModel
    {
        /// <inheritdoc/>
        public string? PointOfContact { get; set; }

        /// <inheritdoc/>
        public bool IsInbound { get; set; }

        /// <inheritdoc/>
        public bool IsOutbound { get; set; }

        /// <inheritdoc/>
        public string? Username { get; set; }

        /// <inheritdoc/>
        public string? Password { get; set; }

        /// <inheritdoc/>
        public string? Authentication { get; set; }

        /// <inheritdoc/>
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        public string? SalesRep { get; set; }

        /// <inheritdoc/>
        public DateTime? PickupTime { get; set; }

        #region Associated Objects
        /// <inheritdoc cref="IShipCarrierModel.ShipCarrierMethods"/>
        public List<ShipCarrierMethodModel>? ShipCarrierMethods { get; set; }

        /// <inheritdoc/>
        List<IShipCarrierMethodModel>? IShipCarrierModel.ShipCarrierMethods { get => ShipCarrierMethods?.ToList<IShipCarrierMethodModel>(); set => ShipCarrierMethods = value?.Cast<ShipCarrierMethodModel>().ToList(); }

        /// <inheritdoc cref="IShipCarrierModel.Shipments"/>
        public List<ShipmentModel>? Shipments { get; set; }

        /// <inheritdoc/>
        List<IShipmentModel>? IShipCarrierModel.Shipments { get => Shipments?.ToList<IShipmentModel>(); set => Shipments = value?.Cast<ShipmentModel>().ToList(); }
        #endregion
    }
}
