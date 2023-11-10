// <copyright file="YRCResponseBodyMain.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseBodyMain class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response body main.</summary>
    [JsonObject("bodyMain")]
    public class YRCResponseBodyMain
    {
        /// <summary>Gets or sets the rate quote.</summary>
        /// <value>The rate quote.</value>
        [JsonProperty("rateQuote")]
        public YRCResponseRateQuote? RateQuote { get; set; }
    }
}
