// <copyright file="YRCResponseNMFC.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRC response NMFC class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A YRC response NMFC.</summary>
    [JsonObject("nmfc")]
    public class YRCResponseNMFC
    {
        /// <summary>Gets or sets the item.</summary>
        /// <value>The item.</value>
        [JsonProperty("item")]
        public string? Item { get; set; }

        /// <summary>Gets or sets the sub item.</summary>
        /// <value>The sub item.</value>
        [JsonProperty("subItem")]
        public string? SubItem { get; set; }

        /// <summary>Gets or sets the class.</summary>
        /// <value>The class.</value>
        [JsonProperty("class")]
        public string? Class { get; set; }
    }
}
