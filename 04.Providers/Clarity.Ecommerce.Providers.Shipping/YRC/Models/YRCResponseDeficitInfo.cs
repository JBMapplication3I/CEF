// <copyright file="YRCResponseDeficitInfo.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseDeficitInfo class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>Information about the yrc response deficit.</summary>
    [JsonObject("deficitInfo")]
    public class YRCResponseDeficitInfo
    {
        /// <summary>Gets or sets the deficit weight.</summary>
        /// <value>The deficit weight.</value>
        [JsonProperty("deficitWeight")]
        public string? DeficitWeight { get; set; }

        /// <summary>Gets or sets the deficit rate.</summary>
        /// <value>The deficit rate.</value>
        [JsonProperty("deficitRate")]
        public string? DeficitRate { get; set; }

        /// <summary>Gets or sets the deficit class.</summary>
        /// <value>The deficit class.</value>
        [JsonProperty("deficitClass")]
        public string? DeficitClass { get; set; }

        /// <summary>Gets or sets the deficit amount.</summary>
        /// <value>The deficit amount.</value>
        [JsonProperty("deficitAmount")]
        public string? DeficitAmount { get; set; }
    }
}
