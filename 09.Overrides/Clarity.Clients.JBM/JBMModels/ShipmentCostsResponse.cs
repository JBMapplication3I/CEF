// <copyright file="ShipmentCostsResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ShipmentCostsResponse class</summary>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class ShipmentCost
    {
        public long? FreightCostId { get; set; }
    }

    public class ShipmentCosts : FusionResponseBase
    {
        [JsonProperty("items")]
        public List<ShipmentCost>? Items { get; set; }
    }

}
