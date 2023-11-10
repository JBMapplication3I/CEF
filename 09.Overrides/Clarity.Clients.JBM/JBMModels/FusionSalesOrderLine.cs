// <copyright file="FusionSalesOrderLine.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionSalesOrderLine class</summary>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject(MissingMemberHandling = MissingMemberHandling.Ignore, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FusionSalesOrderLine
    {
        public string? SourceTransactionLineId { get; set; }

        public string? SourceTransactionLineNumber { get; set; }

        public string? SourceTransactionScheduleId { get; set; }

        public string? SourceScheduleNumber { get; set; }

        public string? TransactionCategoryCode { get; set; }

        public string? ProductNumber { get; set; }

        public decimal? OrderedQuantity { get; set; }

        public string? OrderedUOM { get; set; }

        public long? HeaderId { get; set; }

        public long? LineId { get; set; }

        public long? FulfillLineId { get; set; }

        public string? SourceTransactionId { get; set; }

        public string? SourceTransactionNumber { get; set; }

        public string? SourceTransactionSystem { get; set; }

        public long? RequestingBusinessUnitId { get; set; }

        public string? RequestingBusinessUnitName { get; set; }

        public string? AccountingRuleCode { get; set; }

        public string? AccountingRule { get; set; }

        public string? ActionTypeCode { get; set; }

        public string? ActionType { get; set; }

        public string? ActualCompletionDate { get; set; }

        public string? ActualShipDate { get; set; }

        public long? AppliedPriceListId { get; set; }

        public string? AppliedPriceListName { get; set; }

        public decimal? AssessableValue { get; set; }

        public string? AssetGroupNumber { get; set; }

        public bool? AssetTrackedFlag { get; set; }

        public string? AssetTrackingCode { get; set; }

        public string? AssetTracking { get; set; }

        public long? BillingTransactionTypeId { get; set; }

        public string? BillingTransactionTypeName { get; set; }

        public string? BuyerId { get; set; }

        public string? BuyerName { get; set; }

        public string? BuyerFirstName { get; set; }

        public string? BuyerLastName { get; set; }

        public string? BuyerMiddleName { get; set; }

        public string? CancelReasonCode { get; set; }

        public string? CancelReason { get; set; }

        public bool? CanceledFlag { get; set; }

        public string? CanceledQuantity { get; set; }

        public long? ShippingCarrierId { get; set; }

        public string? ShippingCarrier { get; set; }

        public string? Comments { get; set; }

        public string? ComponentIdPath { get; set; }

        public string? ConfigCreationDate { get; set; }

        public string? ConfigHeaderId { get; set; }

        public string? ConfigInventoryItemId { get; set; }

        public string? ConfigInventoryItemNumber { get; set; }

        public string? ConfigInventoryItemDescription { get; set; }

        public string? ConfigItemReference { get; set; }

        public string? ConfigRevisionNumber { get; set; }

        public string? ConfigTradeComplianceResultCode { get; set; }

        public string? ConfigTradeComplianceResult { get; set; }

        public string? ConfiguratorPath { get; set; }

        public string? ContractEndDate { get; set; }

        public string? ContractStartDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? CreditCheckAuthExpiryDate { get; set; }

        public string? CreditCheckAuthorizationNumber { get; set; }

        public string? CustomerProductId { get; set; }

        public string? CustomerProductNumber { get; set; }

        public string? CustomerProductDescription { get; set; }

        public string? CustomerPOLineNumber { get; set; }

        public string? CustomerPONumber { get; set; }

        public string? CustomerPOScheduleNumber { get; set; }

        public string? DefaultTaxationCountry { get; set; }

        public string? DefaultTaxationCountryShortName { get; set; }

        public string? DemandClassCode { get; set; }

        public string? DemandClass { get; set; }

        public string? DestinationLocationId { get; set; }

        public string? DestinationShippingOrganizationId { get; set; }

        public string? DestinationShippingOrganizationCode { get; set; }

        public string? DestinationShippingOrganizationName { get; set; }

        public string? DocumentSubtype { get; set; }

        public string? DocumentSubtypeName { get; set; }

        public string? EarliestAcceptableShipDate { get; set; }

        public string? EstimateFulfillmentCost { get; set; }

        public string? EstimateMargin { get; set; }

        public string? ExemptionCertificateNumber { get; set; }

        public decimal? ExtendedAmount { get; set; }

        public string? FinalDischargeLocationId { get; set; }

        public string? FinalDischargeLocationAddressLine1 { get; set; }

        public string? FinalDischargeLocationAddressLine2 { get; set; }

        public string? FinalDischargeLocationAddressLine3 { get; set; }

        public string? FinalDischargeLocationAddressLine4 { get; set; }

        public string? FinalDischargeLocationAddressCity { get; set; }

        public string? FinalDischargeLocationAddressPostalCode { get; set; }

        public string? FinalDischargeLocationAddressState { get; set; }

        public string? FinalDischargeLocationAddressProvince { get; set; }

        public string? FinalDischargeLocationAddressCounty { get; set; }

        public string? FinalDischargeLocationAddressCountry { get; set; }

        public string? FirstPartyTaxRegistration { get; set; }

        public string? FirstPartyTaxRegistrationNumber { get; set; }

        public string? FOBPointCode { get; set; }

        public string? FOBPoint { get; set; }

        public string? FreightTermsCode { get; set; }

        public string? FreightTerms { get; set; }

        public string? FulfillInstanceId { get; set; }

        public string? FulfillLineNumber { get; set; }

        public long? RequestedFulfillmentOrganizationId { get; set; }

        public string? RequestedFulfillmentOrganizationCode { get; set; }

        public string? RequestedFulfillmentOrganizationName { get; set; }

        public string? RequestedFulfillmentOrganizationAddress1 { get; set; }

        public string? RequestedFulfillmentOrganizationAddress2 { get; set; }

        public string? RequestedFulfillmentOrganizationAddress3 { get; set; }

        public string? RequestedFulfillmentOrganizationAddress4 { get; set; }

        public string? RequestedFulfillmentOrganizationCity { get; set; }

        public string? RequestedFulfillmentOrganizationState { get; set; }

        public string? RequestedFulfillmentOrganizationPostalCode { get; set; }

        public string? RequestedFulfillmentOrganizationCounty { get; set; }

        public string? RequestedFulfillmentOrganizationProvince { get; set; }

        public string? RequestedFulfillmentOrganizationCountry { get; set; }

        public string? OverFulfillmentTolerance { get; set; }

        public string? UnderFulfillmentTolerance { get; set; }

        public string? FulfilledQuantity { get; set; }

        public string? SecondaryFulfilledQuantity { get; set; }

        public string? FulfillmentDate { get; set; }

        public string? FulfillmentMode { get; set; }

        public string? FulfillmentSplitReferenceId { get; set; }

        public string? GopReferenceId { get; set; }

        public string? GopRequestRegion { get; set; }

        public string? IntendedUseClassificationId { get; set; }

        public string? IntendedUseClassificationName { get; set; }

        public long? ProductId { get; set; }

        public string? ProductDescription { get; set; }

        public long? InventoryOrganizationId { get; set; }

        public string? InventoryOrganizationCode { get; set; }

        public string? InventoryOrganizationName { get; set; }

        public bool? InvoiceEnabledFlag { get; set; }

        public bool? InvoiceInterfacedFlag { get; set; }

        public bool? InvoiceableItemFlag { get; set; }

        public string? InvoicingRuleCode { get; set; }

        public string? InvoicingRule { get; set; }

        public string? ItemSubTypeCode { get; set; }

        public string? ItemSubType { get; set; }

        public string? ItemTypeCode { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? LastUpdateLogin { get; set; }

        public string? LastUpdatedBy { get; set; }

        public string? LatestAcceptableArrivalDate { get; set; }

        public string? LatestAcceptableShipDate { get; set; }

        public string? TransactionLineTypeCode { get; set; }

        public string? TransactionLineType { get; set; }

        public bool? OnHoldFlag { get; set; }

        public bool? OpenFlag { get; set; }

        public string? SecondaryOrderedQuantity { get; set; }

        public string? OrderedUOMCode { get; set; }

        public string? SecondaryUOMCode { get; set; }

        public string? SecondaryUOM { get; set; }

        public long? BusinessUnitId { get; set; }

        public string? BusinessUnitName { get; set; }

        public string? OrigSystemDocumentLineReference { get; set; }

        public string? OrigSystemDocumentReference { get; set; }

        public string? OriginalProductId { get; set; }

        public string? OriginalProductNumber { get; set; }

        public string? OriginalProductDescription { get; set; }

        public bool? OverrideScheduleDateFlag { get; set; }

        public string? OwnerId { get; set; }

        public string? PackingInstructions { get; set; }

        public string? ParentFulfillLineId { get; set; }

        public bool? PartialShipAllowedFlag { get; set; }

        public List<Payment>? payments { get; set; }

        public long? PaymentTermsCode { get; set; }

        public string? PaymentTerms { get; set; }

        public string? POStatusCode { get; set; }

        public DateTime? PricedOn { get; set; }

        public string? ProcessNumber { get; set; }

        public string? ProductFiscalCategoryId { get; set; }

        public string? ProductFiscalCategory { get; set; }

        public string? ProductCategory { get; set; }

        public string? ProductCategoryName { get; set; }

        public string? ProductType { get; set; }

        public string? ProductTypeName { get; set; }

        public string? PromiseArrivalDate { get; set; }

        public string? PromiseShipDate { get; set; }

        public bool? PurchasingEnabledFlag { get; set; }

        public string? PurchasingUOMCode { get; set; }

        public string? PurchasingUOM { get; set; }

        public string? QuantityForEachModel { get; set; }

        public string? ReceivablesOrgId { get; set; }

        public string? ReceivablesOrgName { get; set; }

        public bool? RemnantFlag { get; set; }

        public string? RequestedArrivalDate { get; set; }

        public string? RequestedCancelDate { get; set; }

        public DateTime? RequestedShipDate { get; set; }

        public string? RequiredFulfillmentDate { get; set; }

        public string? RequisitionBusinessUnitId { get; set; }

        public string? RequisitionBusinessUnitName { get; set; }

        public string? RequisitionInventoryOrganizationId { get; set; }

        public string? RequisitionInventoryOrganizationCode { get; set; }

        public string? RequisitionInventoryOrganizationName { get; set; }

        public bool? ReservableFlag { get; set; }

        public string? ReservationId { get; set; }

        public string? ReservedQuantity { get; set; }

        public string? ReturnReasonCode { get; set; }

        public string? ReturnReason { get; set; }

        public bool? ReturnableFlag { get; set; }

        public string? RMADeliveredQuantity { get; set; }

        public string? SecondaryRMADeliveredQuantity { get; set; }

        public string? RootParentFulfillLineId { get; set; }

        public string? SalesProductTypeCode { get; set; }

        public string? SalesProductType { get; set; }

        public long? SalespersonId { get; set; }

        public string? Salesperson { get; set; }

        public DateTime? ScheduleArrivalDate { get; set; }

        public DateTime? ScheduleShipDate { get; set; }

        public string? SchedulingReason { get; set; }

        public string? SchedulingReasonCode { get; set; }

        public string? ServiceDuration { get; set; }

        public string? ServiceDurationPeriodCode { get; set; }

        public string? ServiceDurationPeriodName { get; set; }

        public string? ShippingServiceLevelCode { get; set; }

        public string? ShippingServiceLevel { get; set; }

        public string? ShippingModeCode { get; set; }

        public string? ShippingMode { get; set; }

        public string? ShipSetName { get; set; }

        public string? ShipmentPriorityCode { get; set; }

        public string? ShipmentPriority { get; set; }

        public bool? ShippableFlag { get; set; }

        public string? ShippedQuantity { get; set; }

        public string? SecondaryShippedQuantity { get; set; }

        public string? ShippedUOMCode { get; set; }

        public string? ShippedUOM { get; set; }

        public string? ShippingInstructions { get; set; }

        public string? ShowInSales { get; set; }

        public int? SourceTransactionRevisionNumber { get; set; }

        public string? SplitFromFlineId { get; set; }

        public string? StatusCode { get; set; }

        public string? Status { get; set; }

        public string? SubinventoryCode { get; set; }

        public string? Subinventory { get; set; }

        public bool? SubstitutionAllowedFlag { get; set; }

        public string? SubstitutionReasonCode { get; set; }

        public string? SubstitutionReason { get; set; }

        public string? SupplierId { get; set; }

        public string? SupplierName { get; set; }

        public string? SupplierSiteId { get; set; }

        public string? SupplierSiteName { get; set; }

        public string? SupplyStatusCode { get; set; }

        public string? TaxClassificationCode { get; set; }

        public string? TaxClassification { get; set; }

        public bool? TaxExemptFlag { get; set; }

        public string? TaxExemptCode { get; set; }

        public string? TaxExempt { get; set; }

        public string? TaxExemptReasonCode { get; set; }

        public string? TaxExemptReason { get; set; }

        public string? TaxInvoiceDate { get; set; }

        public string? TaxInvoiceNumber { get; set; }

        public string? ThirdPartyTaxRegistration { get; set; }

        public string? ThirdPartyTaxRegistrationNumber { get; set; }

        public string? TotalContractAmount { get; set; }

        public string? TotalContractQuantity { get; set; }

        public string? TradeComplianceDate { get; set; }

        public string? TradeComplianceResultCode { get; set; }

        public string? TradeComplianceResult { get; set; }

        public bool? TransportationPlannedFlag { get; set; }

        public string? TransportationPlannedStatusCode { get; set; }

        public string? TransactionBusinessCategory { get; set; }

        public string? TransactionBusinessCategoryName { get; set; }

        public decimal? UnitListPrice { get; set; }

        public int? UnitQuantity { get; set; }

        public decimal? UnitSellingPrice { get; set; }

        public string? UserDefinedFiscalClass { get; set; }

        public string? UserDefinedFiscalClassName { get; set; }

        public string? ValidConfigurationFlag { get; set; }

        public int? LineNumber { get; set; }

        public string? DisplayLineNumber { get; set; }

        public string? ParentSourceTransactionLineId { get; set; }

        public string? TransformFromLineId { get; set; }

        public bool? UnreferencedReturnFlag { get; set; }

        public string? ServiceCancelDate { get; set; }

        public string? CoveredProductId { get; set; }

        public string? CoveredProductNumber { get; set; }

        public string? CoveredProductDescription { get; set; }

        public string? CoveredCustomerProductId { get; set; }

        public string? CoveredCustomerProductNumber { get; set; }

        public string? CoveredCustomerProductDescription { get; set; }

        public string? JeopardyPriorityCode { get; set; }

        public string? JeopardyPriority { get; set; }

        public string? AgreementHeaderId { get; set; }

        public string? AgreementLineId { get; set; }

        public string? AgreementLineNumber { get; set; }

        public string? AgreementVersionNumber { get; set; }

        public string? AgreementNumber { get; set; }

        public string? SellingProfitCenterBusinessUnitId { get; set; }

        public string? SellingProfitCenterBusinessUnitName { get; set; }

        public string? TransportationPlanningOrder { get; set; }

        public string? ContractEndDateTime { get; set; }

        public string? ContractStartDateTime { get; set; }

        public string? SubscriptionProfileId { get; set; }

        public string? SubscriptionProfileName { get; set; }

        public string? ExternalPriceBookName { get; set; }

        public string? EndReasonCode { get; set; }

        public string? EndReason { get; set; }

        public string? EndCreditMethodCode { get; set; }

        public string? EndCreditMethod { get; set; }

        public string? EndDate { get; set; }

        public bool? InventoryTransactionFlag { get; set; }

        public bool? InventoryInterfacedFlag { get; set; }

        public string? PriceUsingSecondaryUOMFlag { get; set; }

        public bool? SubscriptionInterfacedFlag { get; set; }

        public string? SupplierAddress1 { get; set; }

        public string? SupplierAddress2 { get; set; }

        public string? SupplierAddress3 { get; set; }

        public string? SupplierAddress4 { get; set; }

        public string? SupplierCity { get; set; }

        public string? SupplierState { get; set; }

        public string? SupplierPostalCode { get; set; }

        public string? SupplierProvince { get; set; }

        public string? SupplierCounty { get; set; }

        public string? SupplierCountry { get; set; }

        public string? RatePlanDocumentId { get; set; }

        public string? OrchestrationProcessName { get; set; }

        public bool? IntegrateSubscriptionFlag { get; set; }

        [JsonProperty("charges")]
        public List<Charge>? Charges { get; set; }

        [JsonProperty("shipToCustomer")]
        public List<ShipToCustomer>? ShipToCustomer { get; set; }
    }

    public class Charge
    {
        public string? SourceChargeId { get; set; }

        public string? PriceType { get; set; }

        public string? ChargeType { get; set; }

        public string? ChargeSubType { get; set; }

        public string? ChargeDefinition { get; set; }

        public int? PricedQuantity { get; set; }

        public string? ChargeCurrency { get; set; }

        public string? PricedQuantityUOM { get; set; }

        public int? SequenceNumber { get; set; }

        public bool? RollupFlag { get; set; }

        public bool? CanAdjustFlag { get; set; }

        public string? ApplyTo { get; set; }

        public string? ApplyToCode { get; set; }

        public string? ChargeTypeCode { get; set; }

        public string? ChargeCurrencyCode { get; set; }

        public string? ChargeDefinitionCode { get; set; }

        public bool? PrimaryFlag { get; set; }

        [JsonProperty("chargeComponents")]
        public List<ChargeComponent>? ChargeComponents { get; set; }
    }

    public class ChargeComponent
    {
        public string? SourceChargeComponentId { get; set; }

        public decimal? ChargeCurrencyUnitPrice { get; set; }

        public decimal? ChargeCurrencyExtendedAmount { get; set; }

        public string? ChargeCurrency { get; set; }

        public string? PriceElement { get; set; }

        public string? PriceElementUsage { get; set; }

        public int? SequenceNumber { get; set; }

        public bool? RollupFlag { get; set; }

        public decimal? HeaderCurrencyUnitPrice { get; set; }

        public decimal? HeaderCurrencyExtendedAmount { get; set; }

        public string? HeaderCurrency { get; set; }

        public string? PriceElementCode { get; set; }

        public string? PriceElementUsageCode { get; set; }
    }

    public class OrderLinesResponse : FusionResponseBase
    {
        public List<FusionSalesOrderLine>? Items { get; set; }
    }
}