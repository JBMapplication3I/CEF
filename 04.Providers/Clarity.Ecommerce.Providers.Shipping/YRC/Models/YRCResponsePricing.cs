// <copyright file="YRCResponsePricing.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponsePricing class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response pricing.</summary>
    [JsonObject("pricing")]
    public class YRCResponsePricing
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>Gets or sets the application code.</summary>
        /// <value>The application code.</value>
        [JsonProperty("appCode")]
        public string? AppCode { get; set; }

        /// <summary>Gets or sets the agent.</summary>
        /// <value>The agent.</value>
        [JsonProperty("agent")]
        public string? Agent { get; set; }

        /// <summary>Gets or sets the tariff.</summary>
        /// <value>The tariff.</value>
        [JsonProperty("tariff")]
        public string? Tariff { get; set; }

        /// <summary>Gets or sets the item.</summary>
        /// <value>The item.</value>
        [JsonProperty("item")]
        public string? Item { get; set; }

        /// <summary>Gets or sets the sub item.</summary>
        /// <value>The sub item.</value>
        [JsonProperty("subItem")]
        public string? SubItem { get; set; }

        /// <summary>Gets or sets the apply pay terms.</summary>
        /// <value>The apply pay terms.</value>
        [JsonProperty("applyPayTerms")]
        public string? ApplyPayTerms { get; set; }

        /// <summary>Gets or sets the used flag.</summary>
        /// <value>The used flag.</value>
        [JsonProperty("usedFlag")]
        public string? UsedFlag { get; set; }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        [JsonProperty("role")]
        public string? Role { get; set; }

        /// <summary>Gets or sets the type of the pricing.</summary>
        /// <value>The type of the pricing.</value>
        [JsonProperty("pricingType")]
        public string? PricingType { get; set; }

        /// <summary>Gets or sets the type of the schedule.</summary>
        /// <value>The type of the schedule.</value>
        [JsonProperty("scheduleType")]
        public string? ScheduleType { get; set; }

        /// <summary>Gets or sets the type of the lane.</summary>
        /// <value>The type of the lane.</value>
        [JsonProperty("laneType")]
        public string? LaneType { get; set; }

        /// <summary>Gets or sets information describing the promo code.</summary>
        /// <value>Information describing the promo code.</value>
        [JsonProperty("promoCodeInfo")]
        public string? PromoCodeInfo { get; set; }

        /// <summary>Gets or sets the liability.</summary>
        /// <value>The liability.</value>
        [JsonProperty("liability")]
        public string? Liability { get; set; }

        /// <summary>Gets or sets the dim factor.</summary>
        /// <value>The dim factor.</value>
        [JsonProperty("dimFactor")]
        public string? DimFactor { get; set; }

        /// <summary>Gets or sets the rated pricing days.</summary>
        /// <value>The rated pricing days.</value>
        [JsonProperty("ratedPricingDays")]
        public string? RatedPricingDays { get; set; }

        /// <summary>Gets or sets the minimum rate applied.</summary>
        /// <value>The minimum rate applied.</value>
        [JsonProperty("minRateApplied")]
        public string? MinRateApplied { get; set; }

        /// <summary>Gets or sets the flat charge applied.</summary>
        /// <value>The flat charge applied.</value>
        [JsonProperty("flatChargeApplied")]
        public string? FlatChargeApplied { get; set; }

        /// <summary>Gets or sets the vendor SCAC.</summary>
        /// <value>The vendor SCAC.</value>
        [JsonProperty("vendorSCAC")]
        public string? VendorSCAC { get; set; }

        /// <summary>Gets or sets the publish rate flag.</summary>
        /// <value>The publish rate flag.</value>
        [JsonProperty("publishRateFlag")]
        public string? PublishRateFlag { get; set; }

        /// <summary>Gets or sets the minimum charge.</summary>
        /// <value>The minimum charge.</value>
        [JsonProperty("minCharge")]
        public int MinCharge { get; set; } // In pennies

        /// <summary>Gets or sets the guarantee flag.</summary>
        /// <value>The guarantee flag.</value>
        [JsonProperty("guaranteeFlag")]
        public string? GuaranteeFlag { get; set; }

        /// <summary>Gets or sets the tfq MVC.</summary>
        /// <value>The tfq MVC.</value>
        [JsonProperty("tfqMvc")]
        public string? TfqMvc { get; set; }

        /// <summary>Gets or sets the quote matrix.</summary>
        /// <value>The quote matrix.</value>
        [JsonProperty("quoteMatrix")]
        public YRCResponseQuoteMatrix? QuoteMatrix { get; set; }

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
        [JsonProperty("shipmentRateUnit")]
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
