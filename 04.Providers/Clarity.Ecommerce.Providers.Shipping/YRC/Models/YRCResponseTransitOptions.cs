// <copyright file="YRCResponseTransitOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseTransitOptions class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response transit options.</summary>
    [JsonObject("transitOptions")]
    public class YRCResponseTransitOptions
    {
        /// <summary>Gets or sets the transit days.</summary>
        /// <value>The transit days.</value>
        [JsonProperty("transitDays")]
        public string? TransitDays { get; set; }

        /// <summary>Gets or sets the delivery date.</summary>
        /// <value>The delivery date.</value>
        [JsonProperty("deliveryDate")]
        public int DeliveryDate { get; set; }

        /// <summary>Gets or sets the delivery do w.</summary>
        /// <value>The delivery do w.</value>
        [JsonProperty("deliveryDow")]
        public string? DeliveryDoW { get; set; }

        /// <summary>Gets or sets the holiday flag.</summary>
        /// <value>The holiday flag.</value>
        [JsonProperty("holidayFlag")]
        public string? HolidayFlag { get; set; }

        /// <summary>Gets or sets the price option.</summary>
        /// <value>The price option.</value>
        [JsonProperty("priceOption")]
        public YRCResponsePriceOption? PriceOption { get; set; }
    }
}
