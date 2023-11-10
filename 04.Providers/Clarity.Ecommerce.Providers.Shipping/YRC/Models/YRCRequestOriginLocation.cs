// <copyright file="YRCRequestOriginLocation.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequestOriginLocation class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc request origin location.</summary>
    [JsonObject("originLocation")]
    public class YRCRequestOriginLocation
    {
        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city")]
        public string? City { get; set; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [JsonProperty("state")]
        public string? State { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        /// <value>The postal code.</value>
        [JsonProperty("postalCode")]
        public string? PostalCode { get; set; }

        /// <summary>Gets or sets the country.</summary>
        /// <value>The country.</value>
        [JsonProperty("country")]
        public string? Country { get; set; }
    }
}
