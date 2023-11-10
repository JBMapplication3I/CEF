// <copyright file="IShipmentEventModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShipmentEventModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for shipment event model.</summary>
    public partial interface IShipmentEventModel
    {
        #region ShipmentEvent Properties
        /// <summary>Gets or sets the note.</summary>
        /// <value>The note.</value>
        string? Note { get; set; }

        /// <summary>Gets or sets the event date.</summary>
        /// <value>The event date.</value>
        DateTime EventDate { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int AddressID { get; set; }

        /// <summary>Gets or sets the address key.</summary>
        /// <value>The address key.</value>
        string? AddressKey { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        IAddressModel? Address { get; set; }

        /// <summary>Gets or sets the identifier of the shipment.</summary>
        /// <value>The identifier of the shipment.</value>
        int ShipmentID { get; set; }

        /// <summary>Gets or sets the shipment key.</summary>
        /// <value>The shipment key.</value>
        string? ShipmentKey { get; set; }

        /// <summary>Gets or sets the shipment.</summary>
        /// <value>The shipment.</value>
        IShipmentModel? Shipment { get; set; }
        #endregion
    }
}
