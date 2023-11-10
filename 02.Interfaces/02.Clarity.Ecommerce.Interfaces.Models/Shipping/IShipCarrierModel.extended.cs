// <copyright file="IShipCarrierModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShipCarrierModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for ship carrier model.</summary>
    public partial interface IShipCarrierModel
    {
        #region ShipCarrier Properties
        /// <summary>Gets or sets the point of contact.</summary>
        /// <value>The point of contact.</value>
        string? PointOfContact { get; set; }

        /// <summary>Gets or sets a value indicating whether this IShipCarrierModel is inbound.</summary>
        /// <value>True if this IShipCarrierModel is inbound, false if not.</value>
        bool IsInbound { get; set; }

        /// <summary>Gets or sets a value indicating whether this IShipCarrierModel is outbound.</summary>
        /// <value>True if this IShipCarrierModel is outbound, false if not.</value>
        bool IsOutbound { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        string? AccountNumber { get; set; }

        /// <summary>Gets or sets the authentication.</summary>
        /// <value>The authentication.</value>
        string? Authentication { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        string? Password { get; set; }

        /// <summary>Gets or sets the pickup time.</summary>
        /// <value>The pickup time.</value>
        DateTime? PickupTime { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        string? Username { get; set; }

        /// <summary>Gets or sets the sales rep.</summary>
        /// <value>The sales rep.</value>
        string? SalesRep { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the ship carrier methods.</summary>
        /// <value>The ship carrier methods.</value>
        List<IShipCarrierMethodModel>? ShipCarrierMethods { get; set; }

        /// <summary>Gets or sets the shipments.</summary>
        /// <value>The shipments.</value>
        List<IShipmentModel>? Shipments { get; set; }
        #endregion
    }
}
