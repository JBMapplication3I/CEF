// <copyright file="ShipmentRates.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment rates class</summary>
namespace Clarity.Ecommerce.Providers.Shipping
{
    using System.Collections.Generic;

    /// <summary>A shipment rates.</summary>
    public class ShipmentRates
    {
        /// <summary>Gets or sets the domestic.</summary>
        /// <value>The domestic.</value>
        public List<ShipmentRate>? Domestic { get; set; }

        /// <summary>Gets or sets the canada.</summary>
        /// <value>The canada.</value>
        public List<ShipmentRate>? Canada { get; set; }

        /// <summary>Gets or sets the international.</summary>
        /// <value>The international.</value>
        public List<ShipmentRate>? International { get; set; }
    }
}
