// <copyright file="FavoriteShipCarrierSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite ship carrier search model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the favorite ship carrier search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IFavoriteShipCarrierSearchModel"/>
    public partial class FavoriteShipCarrierSearchModel
    {
        /// <inheritdoc/>
        public int? ShipCarrierID { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierKey { get; set; }

        /// <inheritdoc/>
        public string? ShipCarrierName { get; set; }
    }
}
