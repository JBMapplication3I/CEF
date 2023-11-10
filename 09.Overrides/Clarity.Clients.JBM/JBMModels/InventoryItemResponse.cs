// <copyright file="InventoryItemResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the InventoryItems class</summary>
namespace Clarity.Clients.JBM
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class Item
    {
        [JsonProperty("InventoryItemId")]
        public long? InventoryItemId { get; set; }

        [JsonProperty("ItemNumber")]
        public string? ItemNumber { get; set; }

        [JsonProperty("PrimaryQuantity")]
        public decimal? PrimaryQuantity { get; set; }
    }

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class InventoryItems : FusionResponseBase
    {
        [JsonProperty("items")]
        public List<Item>? Items { get; set; }
    }
}
