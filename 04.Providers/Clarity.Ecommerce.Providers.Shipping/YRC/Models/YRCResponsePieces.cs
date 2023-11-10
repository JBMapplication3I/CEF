// <copyright file="YRCResponsePieces.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponsePieces class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response pieces.</summary>
    [JsonObject("pieces")]
    public class YRCResponsePieces
    {
        /// <summary>Gets or sets the number of. </summary>
        /// <value>The count.</value>
        [JsonProperty("count")]
        public string? Count { get; set; }

        /// <summary>Gets or sets the package code.</summary>
        /// <value>The package code.</value>
        [JsonProperty("packageCode")]
        public string? PackageCode { get; set; }
    }
}
