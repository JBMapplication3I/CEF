// <copyright file="IFavoriteShipCarrierSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IFavoriteShipCarrierSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for favorite ship carrier search model.</summary>
    public partial interface IFavoriteShipCarrierSearchModel
    {
        /// <summary>Gets or sets the identifier of the ship carrier.</summary>
        /// <value>The identifier of the ship carrier.</value>
        int? ShipCarrierID { get; set; }

        /// <summary>Gets or sets the ship carrier key.</summary>
        /// <value>The ship carrier key.</value>
        string? ShipCarrierKey { get; set; }

        /// <summary>Gets or sets the name of the ship carrier.</summary>
        /// <value>The name of the ship carrier.</value>
        string? ShipCarrierName { get; set; }
    }
}
