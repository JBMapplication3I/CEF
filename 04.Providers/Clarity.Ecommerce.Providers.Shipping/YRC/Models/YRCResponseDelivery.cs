// <copyright file="YRCResponseDelivery.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseDelivery class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response delivery.</summary>
    [JsonObject("delivery")]
    public class YRCResponseDelivery
    {
        /// <summary>Gets or sets the type of the requested service.</summary>
        /// <value>The type of the requested service.</value>
        [JsonProperty("requestedServiceType")]
        public YRCResponseRequestedServiceType? RequestedServiceType { get; set; }

        /// <summary>Gets or sets the requested date time.</summary>
        /// <value>The requested date time.</value>
        [JsonProperty("requestedDateTime")]
        public YRCResponseRequestedDateTime? RequestedDateTime { get; set; }

        /// <summary>Gets or sets the standard date.</summary>
        /// <value>The standard date.</value>
        [JsonProperty("standardDate")]
        public string? StandardDate { get; set; } // Format yyyyMMdd

        /// <summary>Gets or sets the standard days.</summary>
        /// <value>The standard days.</value>
        [JsonProperty("standardDays")]
        public string? StandardDays { get; set; }
    }
}
