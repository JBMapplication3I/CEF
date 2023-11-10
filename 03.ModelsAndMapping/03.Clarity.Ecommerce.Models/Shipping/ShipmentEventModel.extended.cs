// <copyright file="ShipmentEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment event model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using Interfaces.Models;

    /// <summary>A data Model for the shipment event.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IShipmentEventModel"/>
    public partial class ShipmentEventModel
    {
        #region ShipmentEvent Properties
        /// <inheritdoc/>
        public string? Note { get; set; }

        /// <inheritdoc/>
        public DateTime EventDate { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int AddressID { get; set; }

        /// <inheritdoc/>
        public string? AddressKey { get; set; }

        /// <inheritdoc cref="IShipmentEventModel.Address" />
        public AddressModel? Location { get; set; }

        /// <inheritdoc/>
        IAddressModel? IShipmentEventModel.Address { get => Location; set => Location = (AddressModel?)value; }

        /// <inheritdoc/>
        public int ShipmentID { get; set; }

        /// <inheritdoc/>
        public string? ShipmentKey { get; set; }

        /// <inheritdoc cref="IShipmentEventModel.Shipment"/>
        public ShipmentModel? Shipment { get; set; }

        /// <inheritdoc/>
        IShipmentModel? IShipmentEventModel.Shipment { get => Shipment; set => Shipment = (ShipmentModel?)value; }
        #endregion
    }
}
