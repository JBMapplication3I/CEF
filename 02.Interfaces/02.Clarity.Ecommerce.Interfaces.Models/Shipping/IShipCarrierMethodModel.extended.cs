// <copyright file="IShipCarrierMethodModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShipCarrierMethodModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for ship carrier method model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface IShipCarrierMethodModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the ship carrier.</summary>
        /// <value>The identifier of the ship carrier.</value>
        int ShipCarrierID { get; set; }

        /// <summary>Gets or sets the ship carrier key.</summary>
        /// <value>The ship carrier key.</value>
        string? ShipCarrierKey { get; set; }

        /// <summary>Gets or sets the name of the ship carrier.</summary>
        /// <value>The name of the ship carrier.</value>
        string? ShipCarrierName { get; set; }

        /// <summary>Gets or sets the ship carrier.</summary>
        /// <value>The ship carrier.</value>
        IShipCarrierModel? ShipCarrier { get; set; }
        #endregion
    }
}
