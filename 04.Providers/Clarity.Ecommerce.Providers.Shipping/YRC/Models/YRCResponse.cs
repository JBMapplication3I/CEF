// <copyright file="YRCResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponse class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response.</summary>
    public class YRCResponse
    {
        /// <summary>Gets or sets a value indicating whether this YRCResponse is success.</summary>
        /// <value>True if this YRCResponse is success, false if not.</value>
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        /// <summary>Gets or sets the errors.</summary>
        /// <value>The errors.</value>
        [JsonProperty("errors")]
        public YRCResponseError[]? Errors { get; set; }

        /// <summary>Gets or sets the warnings.</summary>
        /// <value>The warnings.</value>
        [JsonProperty("warnings")]
        public string[]? Warnings { get; set; }

        /// <summary>Gets or sets the page root.</summary>
        /// <value>The page root.</value>
        [JsonProperty("pageRoot")]
        public YRCResponsePageRoot? PageRoot { get; set; }
    }
}
