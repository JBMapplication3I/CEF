// <copyright file="YRCResponseRatedCharges.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseRatedCharges class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response rated charges.</summary>
    [JsonObject("ratedCharges")]
    public class YRCResponseRatedCharges
    {
        /// <summary>Gets or sets the freight charges.</summary>
        /// <value>The freight charges.</value>
        [JsonProperty("freightCharges")]
        public int FreightCharges { get; set; } // In pennies

        /// <summary>Gets or sets the other charges.</summary>
        /// <value>The other charges.</value>
        [JsonProperty("otherCharges")]
        public int OtherCharges { get; set; } // In pennies

        /// <summary>Gets or sets the total number of charges.</summary>
        /// <value>The total number of charges.</value>
        [JsonProperty("totalCharges")]
        public int TotalCharges { get; set; } // In pennies
    }
}
