// <copyright file="PriceListResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the UOMResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class PriceList
    {
        [JsonProperty("PriceListId")]
        public long PriceListId { get; set; }
    }

    public class PriceListResponse : FusionResponseBase
    {
        [JsonProperty("items")]
        public List<PriceList>? Items { get; set; }
    }
}