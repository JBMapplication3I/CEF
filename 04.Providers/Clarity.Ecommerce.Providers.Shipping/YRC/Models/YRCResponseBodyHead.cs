// <copyright file="YRCResponseBodyHead.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseBodyHead class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response body head.</summary>
    [JsonObject("bodyHead")]
    public class YRCResponseBodyHead
    {
        /// <summary>Gets or sets the body title.</summary>
        /// <value>The body title.</value>
        [JsonProperty("bodyTitle")]
        public string? BodyTitle { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
