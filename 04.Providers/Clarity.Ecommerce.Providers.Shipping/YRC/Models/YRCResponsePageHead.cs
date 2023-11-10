// <copyright file="YRCResponsePageHead.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponsePageHead class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response page head.</summary>
    [JsonObject("pageHead")]
    public class YRCResponsePageHead
    {
        /// <summary>Gets or sets the page title.</summary>
        /// <value>The page title.</value>
        [JsonProperty("pageTitle")]
        public string? PageTitle { get; set; }

        /// <summary>Gets or sets the page sub title.</summary>
        /// <value>The page sub title.</value>
        [JsonProperty("pageSubtitle")]
        public string? PageSubTitle { get; set; }
    }
}
