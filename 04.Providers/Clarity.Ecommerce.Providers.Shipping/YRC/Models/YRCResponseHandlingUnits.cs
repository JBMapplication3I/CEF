// <copyright file="YRCResponseHandlingUnits.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseHandlingUnits class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response handling units.</summary>
    [JsonObject("handlingUnits")]
    public class YRCResponseHandlingUnits
    {
        /// <summary>Gets or sets the number of. </summary>
        /// <value>The count.</value>
        [JsonProperty("count")]
        public int Count { get; set; }

        /// <summary>Gets or sets the package code.</summary>
        /// <value>The package code.</value>
        [JsonProperty("packageCode")]
        public string? PackageCode { get; set; }

        /// <summary>Gets or sets the length.</summary>
        /// <value>The length.</value>
        [JsonProperty("length")]
        public int Length { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>Gets or sets the dimensions uo m.</summary>
        /// <value>The dimensions uo m.</value>
        [JsonProperty("dimensionsUOM")]
        public string? DimensionsUoM { get; set; }
    }
}
