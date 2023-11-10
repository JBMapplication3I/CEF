// <copyright file="YRCResponseError.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the yrc response error class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response error.</summary>
    [JsonObject("error")]
    public class YRCResponseError
    {
        /// <summary>Gets or sets the field.</summary>
        /// <value>The field.</value>
        [JsonProperty("field")]
        public string? Field { get; set; }

        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
