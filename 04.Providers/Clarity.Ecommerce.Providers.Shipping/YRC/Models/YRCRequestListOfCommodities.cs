// <copyright file="YRCRequestListOfCommodities.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequestListOfCommodities class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc request list of commodities.</summary>
    [JsonObject("listOfCommodities")]
    public class YRCRequestListOfCommodities
    {
        /// <summary>Gets or sets the commodities.</summary>
        /// <value>The commodities.</value>
        [JsonProperty("commodity")]
        public YRCRequestCommodity[]? Commodities { get; set; }
    }
}
