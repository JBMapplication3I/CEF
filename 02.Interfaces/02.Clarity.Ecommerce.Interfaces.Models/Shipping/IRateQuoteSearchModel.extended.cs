// <copyright file="IRateQuoteSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRateQuoteSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for rate quote search model.</summary>
    public partial interface IRateQuoteSearchModel
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

        /// <summary>Gets or sets the ship carrier method key.</summary>
        /// <value>The ship carrier method key.</value>
        string? ShipCarrierMethodKey { get; set; }

        /// <summary>Gets or sets the name of the ship carrier method.</summary>
        /// <value>The name of the ship carrier method.</value>
        string? ShipCarrierMethodName { get; set; }
    }
}
