// <copyright file="InventoryItemResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the InventoryItems class</summary>
namespace Clarity.Clients.JBM
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject]
    public class FusionTotal
    {
        [JsonProperty("TotalName")]
        public string? TotalName { get; set; }

        [JsonProperty("TotalCode")]
        public string? TotalCode { get; set; }

        [JsonProperty("TotalAmount")]
        public double? TotalAmount { get; set; }
    }

    public class FusionOrderTotals
    {
        [JsonProperty("items")]
        public List<FusionTotal>? Items { get; set; }
    }
}
