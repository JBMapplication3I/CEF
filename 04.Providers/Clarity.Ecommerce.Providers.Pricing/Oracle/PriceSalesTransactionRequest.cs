// <copyright file="PriceSalesTransactionRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Oracle pricing provider configuration class</summary>
namespace Clarity.Ecommerce.Providers.Pricing.Oracle.PSTRequest
{
    using System;
    using System.Collections.Generic;

    public class PriceSalesTransactionRequest
    {
        public List<Header>? Header { get; set; }

        public List<PricingHdrEffCustom>? PricingHdrEff_Custom { get; set; }

        public List<Line>? Line { get; set; }

        public PricingServiceParameter? PricingServiceParameter { get; set; }
    }

    public class Header
    {
        public int? HeaderId { get; set; }

        public string? AppliedCurrencyCode { get; set; }

        public bool? CalculatePricingChargesFlag { get; set; }

        public bool? CalculateShippingChargesFlag { get; set; }

        public bool? CalculateTaxFlag { get; set; }

        public long? CustomerId { get; set; }

        public DateTime? TransactionOn { get; set; }

        public string? TransactionTypeCode { get; set; }

        public string? SellingBusinessUnitName { get; set; }
    }

    public class Line
    {
        public int? HeaderId { get; set; }

        public int? LineId { get; set; }

        public bool? AllowPriceListUpdateFlag { get; set; }

        public long? InventoryItemId { get; set; }

        public string? InventoryOrganizationCode { get; set; }

        public string? LineCategoryCode { get; set; }

        public string? LineQuantityUOMCode { get; set; }

        public LineQuantity? LineQuantity { get; set; }

        public string? LineTypeCode { get; set; }

        public bool? SkipShippingChargesFlag { get; set; }

        public DateTime? TransactionOn { get; set; }
    }

    public class LineQuantity
    {
        public int? Value { get; set; }

        public string? UomCode { get; set; }
    }

    public class PricingHdrEffCustom
    {
        public int? HeaderId_Custom { get; set; }

        public string? jnbTier_Custom { get; set; }

        public string? buyingGroup_Custom { get; set; }
    }

    public class PricingServiceParameter
    {
        public string? PricingContext { get; set; }

        public bool? PerformValueIdConversionsFlag { get; set; }
    }
}
