// <copyright file="YRCRequestCommodity.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequestCommodity class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc request commodity.</summary>
    [JsonObject("commodity")]
    public class YRCRequestCommodity
    {
        /// <summary>Gets or sets the nmfc class.</summary>
        /// <value>The nmfc class.</value>
        [JsonProperty("nmfcClass")]
        public string? NmfcClass { get; set; }

        /// <summary>Gets or sets the handling units.</summary>
        /// <value>The handling units.</value>
        [JsonProperty("handlingUnits")]
        public int HandlingUnits { get; set; }

        /// <summary>Gets or sets the package code.</summary>
        /// <value>The package code.</value>
        [JsonProperty("packageCode")]
        public string? PackageCode { get; set; }

        /// <summary>Gets or sets the height of the package.</summary>
        /// <value>The height of the package.</value>
        [JsonProperty("packageHeight")]
        public int PackageHeight { get; set; }

        /// <summary>Gets or sets the length of the package.</summary>
        /// <value>The length of the package.</value>
        [JsonProperty("packageLength")]
        public int PackageLength { get; set; }

        /// <summary>Gets or sets the width of the package.</summary>
        /// <value>The width of the package.</value>
        [JsonProperty("packageWidth")]
        public int PackageWidth { get; set; }

        /// <summary>Gets or sets the package weight.</summary>
        /// <value>The package weight.</value>
        [JsonProperty("weight")]
        public int PackageWeight { get; set; }
    }
}
