// <copyright file="FusionSalesOrder.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionSalesOrder class</summary>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using DocuSign.eSign.Model;
    using Newtonsoft.Json;

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FusionSalesOrder
    {
        public string? HeaderId { get; set; }

        public string? OrderNumber { get; set; }

        public string? TransactionType { get; set; }

        public string? PaymentTerms { get; set; }

        public string? SourceTransactionNumber { get; set; }

        public string? SourceTransactionSystem { get; set; }

        public string? SourceTransactionId { get; set; }

        public long? BusinessUnitId { get; set; }

        public string? BusinessUnitName { get; set; }

        public DateTime? TransactionOn { get; set; }

        public long? BuyingPartyId { get; set; }

        public string? BuyingPartyName { get; set; }

        public string? BuyingPartyNumber { get; set; }

        public string? BuyingPartyPersonFirstName { get; set; }

        public string? BuyingPartyPersonLastName { get; set; }

        public string? BuyingPartyPersonMiddleName { get; set; }

        public string? BuyingPartyPersonNameSuffix { get; set; }

        public string? BuyingPartyPersonTitle { get; set; }

        public long? BuyingPartyContactId { get; set; }

        public string? BuyingPartyContactName { get; set; }

        public string? BuyingPartyContactNumber { get; set; }

        public string? BuyingPartyContactFirstName { get; set; }

        public string? BuyingPartyContactLastName { get; set; }

        public string? BuyingPartyContactMiddleName { get; set; }

        public string? BuyingPartyContactNameSuffix { get; set; }

        public string? BuyingPartyContactTitle { get; set; }

        public long? PreferredSoldToContactPointId { get; set; }

        public DateTime? RequestedShipDate { get; set; }

        public string? RequestedFulfillmentOrganizationName { get; set; }

        public string? CustomerPONumber { get; set; }

        public string? TransactionalCurrencyName { get; set; }

        public string? RequestingBusinessUnitName { get; set; }

        public string? FreightTerms { get; set; }

        public bool? FreezePriceFlag { get; set; }

        public bool? FreezeShippingChargeFlag { get; set; }

        public bool? FreezeTaxFlag { get; set; }

        public bool? SubmittedFlag { get; set; }

        public int? SourceTransactionRevisionNumber { get; set; }

        public string? TransactionTypeCode { get; set; }

        public bool? SubstituteAllowedFlag { get; set; }

        public string? PackingInstructions { get; set; }

        public string? ShippingInstructions { get; set; }

        public bool? ShipsetFlag { get; set; }

        public bool? PartialShipAllowedFlag { get; set; }

        public string? RequestedArrivalDate { get; set; }

        public string? LatestAcceptableShipDate { get; set; }

        public string? LatestAcceptableArrivalDate { get; set; }

        public string? EarliestAcceptableShipDate { get; set; }

        public string? ShipmentPriorityCode { get; set; }

        public string? ShipmentPriority { get; set; }

        public long? ShippingCarrierId { get; set; }

        public string? ShippingCarrier { get; set; }

        public string? ShippingServiceLevelCode { get; set; }

        public string? ShippingServiceLevel { get; set; }

        public string? ShippingModeCode { get; set; }

        public string? ShippingMode { get; set; }

        public string? FOBPointCode { get; set; }

        public string? FOBPoint { get; set; }

        public string? DemandClassCode { get; set; }

        public string? DemandClass { get; set; }

        public string? FreightTermsCode { get; set; }

        public long? RequestedFulfillmentOrganizationId { get; set; }

        public string? RequestedFulfillmentOrganizationCode { get; set; }

        public string? SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public string? SupplierSiteId { get; set; }

        public long? PaymentTermsCode { get; set; }

        public long? SalespersonId { get; set; }

        public string? Salesperson { get; set; }

        public string? PricedOn { get; set; }

        public string? TransactionalCurrencyCode { get; set; }

        public string? AppliedCurrencyCode { get; set; }

        public string? CurrencyConversionDate { get; set; }

        public string? CurrencyConversionRate { get; set; }

        public string? CurrencyConversionType { get; set; }

        public string? PricingSegmentCode { get; set; }

        public string? PricingSegment { get; set; }

        public long? PricingStrategyId { get; set; }

        public string? PricingStrategyName { get; set; }

        public bool? AllowCurrencyOverrideFlag { get; set; }

        public string? SegmentExplanationMessageName { get; set; }

        public string? PricingSegmentExplanation { get; set; }

        public string? StrategyExplanationMessageName { get; set; }

        public string? PricingStrategyExplanation { get; set; }

        public string? SalesChannelCode { get; set; }

        public string? SalesChannel { get; set; }

        public string? Comments { get; set; }

        public string? StatusCode { get; set; }

        public string? Status { get; set; }

        public bool? OnHoldFlag { get; set; }

        public bool? CanceledFlag { get; set; }

        public string? CancelReasonCode { get; set; }

        public string? CancelReason { get; set; }

        public string? RequestedCancelDate { get; set; }

        public long? RequestingBusinessUnitId { get; set; }

        public long? RequestingLegalEntityId { get; set; }

        public string? RequestingLegalEntity { get; set; }

        public string? SubmittedBy { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public string? TransactionDocumentTypeCode { get; set; }

        public bool? PreCreditCheckedFlag { get; set; }

        public string? RevisionSourceSystem { get; set; }

        public string? TradeComplianceResultCode { get; set; }

        public string? TradeComplianceResult { get; set; }

        public string? LastUpdatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public bool? OpenFlag { get; set; }

        public string? OrigSystemDocumentReference { get; set; }

        public string? OrderKey { get; set; }

        public string? AppliedCurrencyName { get; set; }

        public string? SupplierSiteName { get; set; }

        public string? MessageText { get; set; }

        public string? AgreementHeaderId { get; set; }

        public string? AgreementNumber { get; set; }

        public string? AgreementVersionNumber { get; set; }

        public FusionOrderTotals? Totals { get; set; }

        [JsonProperty("billToCustomer")]
        public List<BillToCustomer>? BillToCustomer { get; set; }

        [JsonProperty("shipToCustomer")]
        public List<ShipToCustomer>? ShipToCustomer { get; set; }

        [JsonProperty("lines")]
        public List<FusionSalesOrderLine>? Lines { get; set; }

        [JsonProperty("payments")]
        public List<Payment>? Payments { get; set; }

        [JsonProperty("links")]
        public List<Link>? Links { get; set; }

        /*{
    "OriginalSystemPaymentReference": "PaymentRef1",
    "Amount": 100,
    "PaymentType": "Credit Card",
    "AuthorizationStatus": "Success",
    "PaymentSetId": 1,
    "PaymentMethod": "SSC Manual 200",
    "TransactionExtensionId": 1
}*/
    }

    public class BillToCustomer
    {
        public string? CustomerAccountId { get; set; }

        public string? SiteUseId { get; set; }
    }

    public class ShipToCustomer
    {
        public string? PartyId { get; set; }

        public string? SiteId { get; set; }
    }

    public class Payment
    {
        public string? OriginalSystemPaymentReference { get; set; }

        public decimal? Amount { get; set; }

        public string PaymentType => "Credit Card";

        public string AuthorizationStatus => "Success";

        public string? AuthorizationStatusCode { get; set; }

        //public int PaymentSetId => 1;
        public string PaymentTypeCode => "ORA_CREDIT_CARD";
        //public int TransactionExtensionId => 1;

        // public string PaymentMethod => "Credit Card";

        public long? AuthorizationRequestId { get; set; }

        public decimal? AuthorizedAmount { get; set; }

        public DateTime? AuthorizedOn { get; set; }

        public string? CardTokenNumber { get; set; }

        public string? CardExpirationDate { get; set; }

        public string? CardIssuerCode { get; set; }

        public string? CardFirstName { get; set; }

        public string? CardLastName { get; set; }

        public string? MaskedCardNumber { get; set; }
    }
}