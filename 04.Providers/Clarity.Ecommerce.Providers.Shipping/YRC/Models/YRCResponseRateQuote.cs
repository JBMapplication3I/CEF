// <copyright file="YRCResponseRateQuote.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the YRCResponseRateQuote class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.YRC.Models
{
    using Newtonsoft.Json;

    /// <summary>A yrc response rate quote.</summary>
    [JsonObject("rateQuote")]
    public class YRCResponseRateQuote
    {
        /// <summary>Gets or sets the identifier of the reference.</summary>
        /// <value>The identifier of the reference.</value>
        [JsonProperty("referenceId")]
        public string? ReferenceId { get; set; }

        /// <summary>Gets or sets the identifier of the quote.</summary>
        /// <value>The identifier of the quote.</value>
        [JsonProperty("quoteId")]
        public string? QuoteId { get; set; }

        /// <summary>Gets or sets the payment terms.</summary>
        /// <value>The payment terms.</value>
        [JsonProperty("paymentTerms")]
        public string? PaymentTerms { get; set; }

        /// <summary>Gets or sets the payer role.</summary>
        /// <value>The payer role.</value>
        [JsonProperty("payerRole")]
        public string? PayerRole { get; set; }

        /// <summary>Gets or sets the pickup date.</summary>
        /// <value>The pickup date.</value>
        [JsonProperty("pickupDate")]
        public string? PickupDate { get; set; } // Format yyyyMMdd

        /// <summary>Gets or sets the delivery.</summary>
        /// <value>The delivery.</value>
        [JsonProperty("delivery")]
        public YRCResponseDelivery? Delivery { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        [JsonProperty("currency")]
        public string? Currency { get; set; }

        /// <summary>Gets or sets the locations.</summary>
        /// <value>The locations.</value>
        [JsonProperty("location")]
        public YRCResponseLocation[]? Locations { get; set; }

        /// <summary>Gets or sets the customers.</summary>
        /// <value>The customers.</value>
        [JsonProperty("customer")]
        public YRCResponseCustomer[]? Customers { get; set; }

        /// <summary>Gets or sets the line items.</summary>
        /// <value>The line items.</value>
        [JsonProperty("lineItem")]
        public YRCResponseLineItem[]? LineItems { get; set; }

        /// <summary>Gets or sets the vendor scac.</summary>
        /// <value>The vendor scac.</value>
        [JsonProperty("vendorSCAC")]
        public string? VendorSCAC { get; set; }

        /// <summary>Gets or sets the publish rate flag.</summary>
        /// <value>The publish rate flag.</value>
        [JsonProperty("publishRateFlag")]
        public string? PublishRateFlag { get; set; }

        /// <summary>Gets or sets the minimum charge.</summary>
        /// <value>The minimum charge.</value>
        [JsonProperty("minCharge")]
        public string? MinCharge { get; set; }

        /// <summary>Gets or sets the guarantee flag.</summary>
        /// <value>The guarantee flag.</value>
        [JsonProperty("guaranteeFlag")]
        public string? GuaranteeFlag { get; set; }

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

        /// <summary>Gets or sets the pricing.</summary>
        /// <value>The pricing.</value>
        [JsonProperty("pricing")]
        public YRCResponsePricing[]? Pricing { get; set; }

        /// <summary>Gets or sets information describing the promo code.</summary>
        /// <value>Information describing the promo code.</value>
        [JsonProperty("promoCodeInfo")]
        public YRCResponsePromoCodeInfo? PromoCodeInfo { get; set; }

        /// <summary>Gets or sets the liability.</summary>
        /// <value>The liability.</value>
        [JsonProperty("liability")]
        public YRCResponseLiability? Liability { get; set; }

        /// <summary>Gets or sets the dim factor.</summary>
        /// <value>The dim factor.</value>
        [JsonProperty("dimFactor")]
        public string? DimFactor { get; set; }

        /// <summary>Gets or sets the rated pricing days.</summary>
        /// <value>The rated pricing days.</value>
        [JsonProperty("ratedPricingDays")]
        public string? RatedPricingDays { get; set; }

        /// <summary>Gets or sets the number of account srvcs.</summary>
        /// <value>The number of account srvcs.</value>
        [JsonProperty("accSrvcCount")]
        public string? AccountSrvcCount { get; set; }

        /// <summary>Gets or sets the number of commodities.</summary>
        /// <value>The number of commodities.</value>
        [JsonProperty("commodityCount")]
        public string? CommodityCount { get; set; }

        /// <summary>Gets or sets the minimum charge floor.</summary>
        /// <value>The minimum charge floor.</value>
        [JsonProperty("minChargeFloor")]
        public string? MinChargeFloor { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        [JsonProperty("weight")]
        public decimal Weight { get; set; }

        /// <summary>Gets or sets the rated weight.</summary>
        /// <value>The rated weight.</value>
        [JsonProperty("ratedWeight")]
        public decimal RatedWeight { get; set; }

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

        /// <summary>Gets or sets the full visible capacity.</summary>
        /// <value>The full visible capacity.</value>
        [JsonProperty("fullVisibleCapacity")]
        public string? FullVisibleCapacity { get; set; }

        /// <summary>Gets or sets the position hours.</summary>
        /// <value>The position hours.</value>
        [JsonProperty("positionHours")]
        public string? PositionHours { get; set; }
    }
}
