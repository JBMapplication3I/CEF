// <copyright file="YRCResponseRequestedTime.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseRequestedTime class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response requested time.</summary>
    [JsonObject("requestedTime")]
    public class YRCResponseRequestedTime
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}
