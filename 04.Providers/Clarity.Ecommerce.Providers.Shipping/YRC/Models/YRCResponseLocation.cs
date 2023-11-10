// <copyright file="YRCResponseLocation.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseLocation class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response location.</summary>
    [JsonObject("location")]
    public class YRCResponseLocation
    {
        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        [JsonProperty("role")]
        public string? Role { get; set; }

        /// <summary>Gets or sets the city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city")]
        public string? City { get; set; }

        /// <summary>Gets or sets the state postal code.</summary>
        /// <value>The state postal code.</value>
        [JsonProperty("statePostalCode")]
        public string? StatePostalCode { get; set; }

        /// <summary>Gets or sets the zip code.</summary>
        /// <value>The zip code.</value>
        [JsonProperty("zipCode")]
        public string? ZipCode { get; set; }

        /// <summary>Gets or sets the nation code.</summary>
        /// <value>The nation code.</value>
        [JsonProperty("nationCode")]
        public string? NationCode { get; set; }

        /// <summary>Gets or sets the terminal.</summary>
        /// <value>The terminal.</value>
        [JsonProperty("terminal")]
        public int Terminal { get; set; }

        /// <summary>Gets or sets the zone.</summary>
        /// <value>The zone.</value>
        [JsonProperty("zone")]
        public string? Zone { get; set; }

        /// <summary>Gets or sets the type service.</summary>
        /// <value>The type service.</value>
        [JsonProperty("typeService")]
        public string? TypeService { get; set; }
    }
}
