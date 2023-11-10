// <copyright file="ShipmentZone.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment zone class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Zone
{
    using System.Collections.Generic;

    /// <summary>A shipment zone.</summary>
    public class ShipmentZone
    {
        /// <summary>Gets or sets the name of the zone.</summary>
        /// <value>The name of the zone.</value>
        public string? ZoneName { get; set; }

        /// <summary>Gets or sets the countries.</summary>
        /// <value>The countries.</value>
        public List<string>? Countries { get; set; }

        /// <summary>Gets or sets the shipment rates.</summary>
        /// <value>The shipment rates.</value>
        public List<ShipmentRate>? ShipmentRates { get; set; }
    }
}
