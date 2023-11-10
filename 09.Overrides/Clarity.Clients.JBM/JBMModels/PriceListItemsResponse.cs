// <copyright file="PriceListItemsResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UOMResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class PriceListItem
    {
        [JsonProperty("Item")]
        public string? Item { get; set; }
    }

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore)]
    public class PriceListItemsResponse : FusionResponseBase
    {
        [JsonProperty("items")]
        public List<PriceListItem>? items { get; set; }
    }
}