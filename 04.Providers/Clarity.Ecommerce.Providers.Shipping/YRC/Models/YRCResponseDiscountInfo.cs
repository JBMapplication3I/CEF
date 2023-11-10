// <copyright file="YRCResponseDiscountInfo.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseDiscountInfo class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>Information about the yrc response discount.</summary>
    [JsonObject("discountInfo")]
    public class YRCResponseDiscountInfo
    {
        /// <summary>Gets or sets the discount floor applied.</summary>
        /// <value>The discount floor applied.</value>
        [JsonProperty("discountFloorApplied")]
        public string? DiscountFloorApplied { get; set; }

        /// <summary>Gets or sets the discount percent rate.</summary>
        /// <value>The discount percent rate.</value>
        [JsonProperty("discountPercentRate")]
        public string? DiscountPercentRate { get; set; }

        /// <summary>Gets or sets the discount amount.</summary>
        /// <value>The discount amount.</value>
        [JsonProperty("discountAmount")]
        public string? DiscountAmount { get; set; }
    }
}
