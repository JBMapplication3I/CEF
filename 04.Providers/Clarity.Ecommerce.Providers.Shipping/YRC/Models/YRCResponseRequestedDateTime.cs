// <copyright file="YRCResponseRequestedDateTime.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseRequestedDateTime class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response requested date time.</summary>
    [JsonObject("requestedDateTime")]
    public class YRCResponseRequestedDateTime
    {
        /// <summary>Gets or sets the requested date.</summary>
        /// <value>The requested date.</value>
        [JsonProperty("requestedDate")]
        public string? RequestedDate { get; set; } // Format yyyyMMdd

        /// <summary>Gets or sets the requested time.</summary>
        /// <value>The requested time.</value>
        [JsonProperty("requestedTime")]
        public YRCResponseRequestedTime[]? RequestedTime { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}
