// <copyright file="YRCResponseQuoteMatrix.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseQuoteMatrix class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response quote matrix.</summary>
    [JsonObject("quoteMatrix")]
    public class YRCResponseQuoteMatrix
    {
        /// <summary>Gets or sets the minimum transit days.</summary>
        /// <value>The minimum transit days.</value>
        [JsonProperty("minTransitDays")]
        public string? MinTransitDays { get; set; }

        /// <summary>Gets or sets the minimum transit hours.</summary>
        /// <value>The minimum transit hours.</value>
        [JsonProperty("minTransitHours")]
        public string? MinTransitHours { get; set; }

        /// <summary>Gets or sets the standard transit days.</summary>
        /// <value>The standard transit days.</value>
        [JsonProperty("stdTransitDays")]
        public string? StdTransitDays { get; set; }

        /// <summary>Gets or sets options for controlling the transit.</summary>
        /// <value>Options that control the transit.</value>
        [JsonProperty("transitOptions")]
        public YRCResponseTransitOptions? TransitOptions { get; set; }

        /// <summary>Gets or sets the minimum charge floor.</summary>
        /// <value>The minimum charge floor.</value>
        [JsonProperty("minChargeFloor")]
        public string? MinChargeFloor { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        [JsonProperty("weight")]
        public int Weight { get; set; }

        /// <summary>Gets or sets the rated weight.</summary>
        /// <value>The rated weight.</value>
        [JsonProperty("ratedWeight")]
        public int RatedWeight { get; set; }

        /// <summary>Gets or sets the type of the quote.</summary>
        /// <value>The type of the quote.</value>
        [JsonProperty("quoteType")]
        public string? QuoteType { get; set; }

        /// <summary>Gets or sets the linear feet.</summary>
        /// <value>The linear feet.</value>
        [JsonProperty("linearFeet")]
        public string? LinearFeet { get; set; }

        /// <summary>Gets or sets the cubic feet.</summary>
        /// <value>The cubic feet.</value>
        [JsonProperty("cubicFeet")]
        public string? CubicFeet { get; set; }

        /// <summary>Gets or sets the calculate cubic feet.</summary>
        /// <value>The calculate cubic feet.</value>
        [JsonProperty("calcCubicFeet")]
        public string? CalcCubicFeet { get; set; }

        /// <summary>Gets or sets the full visibility capacity.</summary>
        /// <value>The full visibility capacity.</value>
        [JsonProperty("fullVisibilityCapacity")]
        public string? FullVisibilityCapacity { get; set; }

        /// <summary>Gets or sets the position hours.</summary>
        /// <value>The position hours.</value>
        [JsonProperty("positionHours")]
        public string? PositionHours { get; set; }

        /// <summary>Gets or sets the rated charges.</summary>
        /// <value>The rated charges.</value>
        [JsonProperty("ratedCharges")]
        public YRCResponseRatedCharges? RatedCharges { get; set; }

        /// <summary>Gets or sets the shipment rate unit.</summary>
        /// <value>The shipment rate unit.</value>
        public string? ShipmentRateUnit { get; set; }

        /// <summary>Gets or sets the shipment price per unit.</summary>
        /// <value>The shipment price per unit.</value>
        [JsonProperty("shipmentPricePerUnit")]
        public string? ShipmentPricePerUnit { get; set; }

        /// <summary>Gets or sets information describing the deficit.</summary>
        /// <value>Information describing the deficit.</value>
        [JsonProperty("deficitInfo")]
        public YRCResponseDeficitInfo? DeficitInfo { get; set; }

        /// <summary>Gets or sets information describing the discount.</summary>
        /// <value>Information describing the discount.</value>
        [JsonProperty("discountInfo")]
        public YRCResponseDiscountInfo? DiscountInfo { get; set; }
    }
}
