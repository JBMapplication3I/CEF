// <copyright file="YRCRequestServiceOpts.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCRequestServiceOpts class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc request service options.</summary>
    [JsonObject("username")]
    public class YRCRequestServiceOpts
    {
        /// <summary>Gets or sets options for controlling the accumulate.</summary>
        /// <value>Options that control the accumulate.</value>
        [JsonProperty("accOptions")]
        public string[]? AccOptions { get; set; }
    }
}
