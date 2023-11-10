// <copyright file="PriceSalesTransactionResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Oracle pricing provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Oracle.PSTResponse
{
    using System;
    using System.Collections.Generic;

    public class PriceSalesTransactionResponse
    {
        public List<Header>? Header { get; set; }

        public List<PricingServiceParameter>? PricingServiceParameter { get; set; }

        public List<PricingHdrEffCustom>? PricingHdrEff_Custom { get; set; }

        public List<Line>? Line { get; set; }

        public List<Charge>? Charge { get; set; }

        public List<ChargeComponent>? ChargeComponent { get; set; }
    }

    public class Charge
    {
        public bool? CanAdjustFlag { get; set; }

        public string? ChargeAppliesTo { get; set; }

        public string? ChargeDefinitionCode { get; set; }

        public long? ChargeDefinitionId { get; set; }

        public int? ChargeId { get; set; }

        public string? ChargeSubtypeCode { get; set; }

        public string? ChargeTypeCode { get; set; }

        public string? CurrencyCode { get; set; }

        public bool? EstimatedPricedQuantityFlag { get; set; }

        public bool? EstimatedUnitPriceFlag { get; set; }

        public int? LineId { get; set; }

        public string? ParentEntityCode { get; set; }

        public int? ParentEntityId { get; set; }

        public string? PriceTypeCode { get; set; }

        public PricedQuantity? PricedQuantity { get; set; }

        public string? PricedQuantityUOMCode { get; set; }

        public bool? PrimaryFlag { get; set; }

        public bool? RollupFlag { get; set; }

        public int? SequenceNumber { get; set; }

        public bool? TaxIncludedFlag { get; set; }
    }

    public class ChargeComponent
    {
        public int? ChargeComponentId { get; set; }

        public int? ChargeId { get; set; }

        public string? CurrencyCode { get; set; }

        public string? Explanation { get; set; }

        public string? ExplanationMessageName { get; set; }

        public ExtendedAmount? ExtendedAmount { get; set; }

        public string? HeaderCurrencyCode { get; set; }

        public HeaderCurrencyExtendedAmount? HeaderCurrencyExtendedAmount { get; set; }

        public HeaderCurrencyUnitPrice? HeaderCurrencyUnitPrice { get; set; }

        public decimal? PercentOfComparisonElement { get; set; }

        public string? PriceElementCode { get; set; }

        public bool? RollupFlag { get; set; }

        public int? SequenceNumber { get; set; }

        public long? SourceId { get; set; }

        public string? SourceTypeCode { get; set; }

        public UnitPrice? UnitPrice { get; set; }

        public string? PriceElementUsageCode { get; set; }

        public bool? TaxIncludedFlag { get; set; }
    }

    public class ExtendedAmount
    {
        public decimal? Value { get; set; }

        public string? CurrencyCode { get; set; }
    }

    public class Header
    {
        public bool? AllowCurrencyOverrideFlag { get; set; }

        public string? AppliedCurrencyCode { get; set; }

        public bool? CalculatePricingChargesFlag { get; set; }

        public bool? CalculateShippingChargesFlag { get; set; }

        public bool? CalculateTaxFlag { get; set; }

        public long? CustomerId { get; set; }

        public string? DefaultCurrencyCode { get; set; }

        public int? HeaderId { get; set; }

        public DateTime? PriceAsOf { get; set; }

        public DateTime? PriceValidFrom { get; set; }

        public DateTime? PricedOn { get; set; }

        public string? PricingSegmentCode { get; set; }

        public string? PricingSegmentExplanation { get; set; }

        public string? PricingStrategyExplanation { get; set; }

        public long? PricingStrategyId { get; set; }

        public long? SellingBusinessUnitId { get; set; }

        public string? SellingBusinessUnitName { get; set; }

        public DateTime? TransactionOn { get; set; }

        public string? TransactionTypeCode { get; set; }
    }

    public class HeaderCurrencyExtendedAmount
    {
        public decimal? Value { get; set; }

        public string? CurrencyCode { get; set; }
    }

    public class HeaderCurrencyUnitPrice
    {
        public decimal? Value { get; set; }

        public string? CurrencyCode { get; set; }
    }

    public class Line
    {
        public bool? AllowCurrencyOverrideFlag { get; set; }

        public bool? AllowPriceListUpdateFlag { get; set; }

        public long? AppliedPriceListId { get; set; }

        public string? DefaultCurrencyCode { get; set; }

        public long? DefaultPriceListId { get; set; }

        public int? HeaderId { get; set; }

        public long? InventoryItemId { get; set; }

        public long? InventoryOrganizationId { get; set; }

        public string? InventoryOrganizationCode { get; set; }

        public string? LineCategoryCode { get; set; }

        public int? LineId { get; set; }

        public LineQuantity? LineQuantity { get; set; }

        public string? LineQuantityUOMCode { get; set; }

        public string? LineTypeCode { get; set; }

        public DateTime? PriceAsOf { get; set; }

        public DateTime? PriceValidFrom { get; set; }

        public DateTime? PricedOn { get; set; }

        public long? PricingStrategyId { get; set; }

        public bool? SkipShippingChargesFlag { get; set; }

        public DateTime? TransactionOn { get; set; }
    }

    public class LineQuantity
    {
        public int? Value { get; set; }

        public string? UomCode { get; set; }
    }

    public class PricedQuantity
    {
        public int? Value { get; set; }

        public string? UomCode { get; set; }
    }

    public class PricingHdrEffCustom
    {
        public int? HeaderId_Custom { get; set; }

        public string? buyingGroup_Custom { get; set; }

        public string? jnbTier_Custom { get; set; }
    }

    public class PricingServiceParameter
    {
        public string? OutputStatus { get; set; }

        public bool? PerformValueIdConversionsFlag { get; set; }

        public string? PricingContext { get; set; }
    }

    public class UnitPrice
    {
        public decimal? Value { get; set; }

        public string? CurrencyCode { get; set; }
    }
}
