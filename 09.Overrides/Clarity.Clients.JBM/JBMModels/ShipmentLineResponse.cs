// <copyright file="ShipmentLineResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Shipment Line Response class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject("Item")]
    public class JBMShipmentLine
    {
        
        public int? ShipmentLine { get; set; }
        
        public string? OrderTypeCode { get; set; }
        
        public string? OrderType { get; set; }
        
        public string? Order { get; set; }
        
        public string? OrderLine { get; set; }
        
        public string? OrderSchedule { get; set; }
        
        public long? InventoryItemId { get; set; }
        
        public string? Item { get; set; }
        
        public string? ItemDescription { get; set; }
        
        public int? Revision { get; set; }
        
        public int? UnitPrice { get; set; }
        
        public int? SellingPrice { get; set; }
        
        public int? RequestedQuantity { get; set; }
        
        public string? RequestedQuantityUOMCode { get; set; }
        
        public string? RequestedQuantityUOM { get; set; }
        
        public decimal? SecondaryRequestedQuantity { get; set; }
        
        public string? SecondaryRequestedQuantityUOMCode { get; set; }
        
        public string? SecondaryRequestedQuantityUOM { get; set; }
        
        public string? FOBCode { get; set; }
        
        public string? FOB { get; set; }
        
        public string? FreightTermsCode { get; set; }
        
        public string? FreightTerms { get; set; }
        
        public string? ShippingPriorityCode { get; set; }
        
        public string? ShippingPriority { get; set; }
        
        public string? PreferredGrade { get; set; }
        
        public string? PreferredGradeName { get; set; }
        
        public string? CurrencyCode { get; set; }
        
        public string? CurrencyName { get; set; }
        
        public string? ShipmentSet { get; set; }
        
        public string? ArrivalSet { get; set; }
        
        public string? CustomerPONumber { get; set; }
        
        public string? CustomerItemId { get; set; }
        
        public string? CustomerItem { get; set; }
        
        public string? CustomerItemDescription { get; set; }
        
        public DateTime? RequestedDate { get; set; }
        
        public DateTime? ScheduledShipDate { get; set; }
        
        public string? RequestedDateTypeCode { get; set; }
        
        public string? RequestedDateType { get; set; }
        
        public long? ShipToPartyId { get; set; }
        
        public string? ShipToCustomer { get; set; }
        
        public string? ShipToCustomerNumber { get; set; }
        
        public long? ShipToPartySiteId { get; set; }
        
        public string? ShipToPartySiteNumber { get; set; }
        
        public long? ShipToContactId { get; set; }
        
        public string? ShipToContact { get; set; }
        
        public string? ShipToContactFirstName { get; set; }
        
        public string? ShipToContactLastName { get; set; }
        
        public string? ShipToContactPhone { get; set; }
        
        public string? ShipToURL { get; set; }
        
        public string? ShipToFax { get; set; }
        
        public string? ShipToEmail { get; set; }
        
        public long? ShipToLocationId { get; set; }
        
        public string? ShipToLocationType { get; set; }
        
        public string? ShipToAddress1 { get; set; }
        
        public string? ShipToAddress2 { get; set; }
        
        public string? ShipToAddress3 { get; set; }
        
        public string? ShipToAddress4 { get; set; }
        
        public string? ShipToCity { get; set; }
        
        public string? ShipToCounty { get; set; }
        
        public string? ShipToPostalCode { get; set; }
        
        public string? ShipToRegion { get; set; }
        
        public string? ShipToState { get; set; }
        
        public string? ShipToCountry { get; set; }
        
        public long? SoldToPartyId { get; set; }
        
        public string? SoldToCustomer { get; set; }
        
        public string? SoldToCustomerNumber { get; set; }
        
        public string? Supplier { get; set; }
        
        public string? SupplierPartyNumber { get; set; }
        
        public long? SoldToContactId { get; set; }
        
        public string? SoldToContact { get; set; }
        
        public string? SoldToContactFirstName { get; set; }
        
        public string? SoldToContactLastName { get; set; }
        
        public string? SoldToContactPhone { get; set; }
        
        public string? SoldToURL { get; set; }
        
        public string? SoldToFax { get; set; }
        
        public string? SoldToEmail { get; set; }
        
        public long? BillToPartyId { get; set; }
        
        public string? BillToCustomer { get; set; }
        
        public string? BillToCustomerNumber { get; set; }
        
        public long? BillToPartySiteId { get; set; }
        
        public string? BillToPartySiteNumber { get; set; }
        
        public string? BillToContactId { get; set; }
        
        public string? BillToContact { get; set; }
        
        public string? BillToContactFirstName { get; set; }
        
        public string? BillToContactLastName { get; set; }
        
        public string? BillToContactPhone { get; set; }
        
        public string? BillToURL { get; set; }
        
        public string? BillToFax { get; set; }
        
        public string? BillToEmail { get; set; }
        
        public long? BillToLocationId { get; set; }
        
        public string? BillToAddress1 { get; set; }
        
        public string? BillToAddress2 { get; set; }
        
        public string? BillToAddress3 { get; set; }
        
        public string? BillToAddress4 { get; set; }
        
        public string? BillToCity { get; set; }
        
        public string? BillToCounty { get; set; }
        
        public string? BillToPostalCode { get; set; }
        
        
        public string? BillToRegion { get; set; }
        
        public string? BillToState { get; set; }
        
        public string? BillToCountry { get; set; }
        
        public string? Subinventory { get; set; }
        
        public string? LocatorId { get; set; }
        
        public string? Locator { get; set; }
        
        public int? ShippedQuantity { get; set; }
        
        public int? SecondaryShippedQuantity { get; set; }
        
        public string? NetWeight { get; set; }
        
        public string? GrossWeight { get; set; }
        
        public string? TareWeight { get; set; }
        
        public string? WeightUOMCode { get; set; }
        
        public string? WeightUOM { get; set; }
        
        public string? Volume { get; set; }
        
        public string? VolumeUOMCode { get; set; }
        
        public string? VolumeUOM { get; set; }
        
        public string? LoadingSequence { get; set; }
        
        public string? LotNumber { get; set; }
        
        public string? EndAssemblyItemNumber { get; set; }
        
        public long? OrganizationId { get; set; }
        
        public string? OrganizationCode { get; set; }
        
        public string? OrganizationName { get; set; }
        
        public string? ParentPackingUnitId { get; set; }
        
        public string? ParentPackingUnit { get; set; }
        
        public int? ShipmentId { get; set; }
        
        public string? Shipment { get; set; }
        
        public bool? ProjectSalesOrderFlag { get; set; }
        
        public string? RcvShipmentLineId { get; set; }
        
        public string? LineStatusCode { get; set; }
        
        public string? LineStatus { get; set; }
        
        public string? TrackingNumber { get; set; }
        
        public string? SealNumber { get; set; }
        
        public int? PickWaveId { get; set; }
        
        public string? PickWave { get; set; }
        
        public int? SourceOrderId { get; set; }
        
        public string? SourceOrder { get; set; }
        
        public int? SourceOrderLineId { get; set; }
        
        public string? SourceOrderLine { get; set; }
        
        public long? SourceOrderFulfillmentLineId { get; set; }
        
        public string? SourceOrderFulfillmentLine { get; set; }
        
        public string? TaxationCountryCode { get; set; }
        
        public string? TaxationCountry { get; set; }
        
        public string? FirstPartyTaxRegistrationId { get; set; }
        
        public string? FirstPartyTaxRegistrationNumber { get; set; }
        
        public string? ThirdPartyTaxRegistrationId { get; set; }
        
        public string? ThirdPartyTaxRegistrationNumber { get; set; }
        
        public string? LocationOfFinalDischargeId { get; set; }
        
        public string? LocationOfFinalDischargeCode { get; set; }
        
        public string? LocationOfFinalDischarge { get; set; }
        
        public string? DocumentFiscalClassificationCode { get; set; }
        
        public string? DocumentFiscalClassification { get; set; }
        
        public string? TransactionBusinessCategoryCode { get; set; }
        
        public string? TransactionBusinessCategory { get; set; }
        
        public string? UserDefinedFiscalClassificationCode { get; set; }
        
        public string? UserDefinedFiscalClassification { get; set; }
        
        public string? TaxInvoiceNumber { get; set; }
        
        public string? TaxInvoiceDate { get; set; }
        
        public string? ProductCategoryCode { get; set; }
        
        public string? ProductCategory { get; set; }
        
        public string? IntendedUseClassificationId { get; set; }
        
        public string? IntendedUse { get; set; }
        
        public string? ProductTypeCode { get; set; }
        
        public string? ProductType { get; set; }
        
        public decimal? AssessableValue { get; set; }
        
        public string? TaxClassificationCode { get; set; }
        
        public string? TaxClassification { get; set; }
        
        public string? TaxExemptionCertificateNumber { get; set; }
        
        public string? TaxExemptionReasonCode { get; set; }
        
        public string? TaxExemptionReason { get; set; }
        
        public string? ProductFiscalClassificationId { get; set; }
        
        public string? ProductFiscalClassification { get; set; }
        
        public string? TransportationPlanningStatusCode { get; set; }
        
        public string? TransportationPlanningStatus { get; set; }
        
        public string? TransportationPlanningDate { get; set; }
        
        public string? TransportationShipment { get; set; }
        
        public string? TransportationShipmentLine { get; set; }
        
        public string? InitialDestinationId { get; set; }
        
        public string? InitialDestination { get; set; }
        
        public string? TradeComplianceStatusCode { get; set; }
        
        public string? TradeComplianceStatus { get; set; }
        
        public string? TradeComplianceDate { get; set; }
        
        public string? TradeComplianceReason { get; set; }
        
        public string? TradeComplianceScreeningMethodCode { get; set; }
        
        public string? TradeComplianceScreeningMethod { get; set; }
        
        public long? ShipFromLocationId { get; set; }
        
        public string? ShipFromAddress1 { get; set; }
        
        public string? ShipFromAddress2 { get; set; }
        
        public string? ShipFromAddress3 { get; set; }
        
        public string? ShipFromAddress4 { get; set; }
        
        public string? ShipFromCity { get; set; }
        
        public string? ShipFromCounty { get; set; }
        
        public string? ShipFromPostalCode { get; set; }
        
        public string? ShipFromRegion { get; set; }
        
        public string? ShipFromState { get; set; }
        
        public string? ShipFromCountry { get; set; }
        
        public string? MaximumOvershipmentPercentage { get; set; }
        
        public string? MaximumUndershipmentPercentage { get; set; }
        
        public int? SourceRequestedQuantity { get; set; }
        
        public string? SourceRequestedQuantityUOMCode { get; set; }
        
        public string? SourceRequestedQuantityUOM { get; set; }
        
        public string? SecondarySourceRequestedQuantity { get; set; }
        
        public string? SecondarySourceRequestedQuantityUOMCode { get; set; }
        
        public string? SecondarySourceRequestedQuantityUOM { get; set; }
        
        public string? DeliveredQuantity { get; set; }
        
        public string? SecondaryDeliveredQuantity { get; set; }
        
        public string? CancelledQuantity { get; set; }
        
        public string? SecondaryCancelledQuantity { get; set; }
        
        public int? BackorderedQuantity { get; set; }
        
        public string? SecondaryBackorderedQuantity { get; set; }
        
        public int? PickedQuantity { get; set; }
        
        public string? SecondaryPickedQuantity { get; set; }
        
        public int? ConvertedQuantity { get; set; }
        
        public int? SecondaryConvertedQuantity { get; set; }
        
        public int? StagedQuantity { get; set; }
        
        public int? SecondaryStagedQuantity { get; set; }
        
        public int? PendingQuantity { get; set; }
        
        public bool? PendingQuantityFlag { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? CreatedBy { get; set; }
        
        public DateTime? LastUpdateDate { get; set; }
        
        public string? LastUpdatedBy { get; set; }
        
        public string? SplitFromShipmentLine { get; set; }
        
        public string? SourceSubinventory { get; set; }
        
        public long? CarrierId { get; set; }
        
        public string? Carrier { get; set; }
        
        public string? CarrierNumber { get; set; }
        
        public string? ModeOfTransportCode { get; set; }
        
        public string? ModeOfTransport { get; set; }
        
        public string? ServiceLevelCode { get; set; }
        
        public string? ServiceLevel { get; set; }
        
        public string? ShippingMethodCode { get; set; }
        
        public string? ShippingMethod { get; set; }
        
        public int? SourceDocumentTypeId { get; set; }
        
        public string? UnitWeight { get; set; }
        
        public string? UnitVolume { get; set; }
        
        public string? LogisticsServiceProviderCustomerId { get; set; }
        
        public string? LogisticsServiceProviderCustomer { get; set; }
        
        public string? LogisticsServiceProviderCustomerNumber { get; set; }
        
        public DateTime? SourceLineUpdateDate { get; set; }
        
        public long? SourceSystemId { get; set; }
        
        public string? SourceSystem { get; set; }
        
        public bool? ShipmentAdviceStatusFlag { get; set; }
        
        public string? DoNotShipBeforeDate { get; set; }
        
        public string? DoNotShipAfterDate { get; set; }
        
        public string? IntegrationStatusCode { get; set; }
        
        public string? IntegrationStatus { get; set; }
        
        public string? QuickShipStatus { get; set; }
        
        public long? BusinessUnitId { get; set; }
        
        public string? BusinessUnit { get; set; }
        
        public long? LegalEntityId { get; set; }
        
        public string? LegalEntity { get; set; }
        
        public string? POHeaderId { get; set; }
        
        public string? PONumber { get; set; }
        
        public string? POBillToBusinessUnitId { get; set; }
        
        public string? POBillToBusinessUnit { get; set; }
        
        public string? POSoldToLegalEntityId { get; set; }
        
        public string? POSoldToLegalEntity { get; set; }
        
        public string? ConversionTypeCode { get; set; }
        
        public string? ConversionDate { get; set; }
        
        public string? ConversionRate { get; set; }
        
        public string? ParentItemId { get; set; }
        
        public string? ParentItem { get; set; }
        
        public string? ParentItemDescription { get; set; }
        
        public string? ParentSourceOrderFulfillmentLineId { get; set; }
        
        public string? BaseItemId { get; set; }
        
        public string? BaseItem { get; set; }
        
        public string? BaseItemDescription { get; set; }
        
        public string? SrcAttributeCategory { get; set; }
        
        public string? SrcAttribute1 { get; set; }
        
        public string? SrcAttribute2 { get; set; }
        
        public string? SrcAttribute3 { get; set; }
        
        public string? SrcAttribute4 { get; set; }
        
        public string? SrcAttribute5 { get; set; }
        
        public string? SrcAttribute6 { get; set; }
        
        public string? SrcAttribute7 { get; set; }
        
        public string? SrcAttribute8 { get; set; }
        
        public string? SrcAttribute9 { get; set; }
        
        public string? SrcAttribute10 { get; set; }
        
        public string? SrcAttribute11 { get; set; }
        
        public string? SrcAttribute12 { get; set; }
        
        public string? SrcAttribute13 { get; set; }
        
        public string? SrcAttribute14 { get; set; }
        
        public string? SrcAttribute15 { get; set; }
        
        public string? SrcAttribute16 { get; set; }
        
        public string? SrcAttribute17 { get; set; }
        
        public string? SrcAttribute18 { get; set; }
        
        public string? SrcAttribute19 { get; set; }
        
        public string? SrcAttribute20 { get; set; }
        
        public string? SrcAttributeDate1 { get; set; }
        
        public string? SrcAttributeDate2 { get; set; }
        
        public string? SrcAttributeDate3 { get; set; }
        
        public string? SrcAttributeDate4 { get; set; }
        
        public string? SrcAttributeDate5 { get; set; }
        
        public string? SrcAttributeNumber1 { get; set; }
        
        public string? SrcAttributeNumber2 { get; set; }
        
        public string? SrcAttributeNumber3 { get; set; }
        
        public string? SrcAttributeNumber4 { get; set; }
        
        public string? SrcAttributeNumber5 { get; set; }
        
        public string? SrcAttributeNumber6 { get; set; }
        
        public string? SrcAttributeNumber7 { get; set; }
        
        public string? SrcAttributeNumber8 { get; set; }
        
        public string? SrcAttributeNumber9 { get; set; }
        
        public string? SrcAttributeNumber10 { get; set; }
        
        public string? SrcAttributeTimestamp1 { get; set; }
        
        public string? SrcAttributeTimestamp2 { get; set; }
        
        public string? SrcAttributeTimestamp3 { get; set; }
        
        public string? SrcAttributeTimestamp4 { get; set; }
        
        public string? SrcAttributeTimestamp5 { get; set; }
        
        public string? TransportationShipmentFromEvent { get; set; }
        
        public string? TransportationShipmentLineFromEvent { get; set; }
        
        public string? TransportationShipmentCompleteFromEvent { get; set; }
        
        public string? TradeComplianceScreenedFromEvent { get; set; }
        
        public string? CurrentBackorderedQuantityFromEvent { get; set; }
        
        public string? OriginalShipmentLineFromEvent { get; set; }
        
        public string? ProjectCostingProjectId { get; set; }
        
        public string? ProjectCostingProjectNumber { get; set; }
        
        public string? ProjectCostingProjectName { get; set; }
        
        public string? ProjectCostingTaskId { get; set; }
        
        public string? ProjectCostingTaskNumber { get; set; }
        
        public string? ProjectCostingTaskName { get; set; }
        
        public string? OverShipTolerancePercentage { get; set; }
        
        public string? UnderShipTolerancePercentage { get; set; }
        
        public string? ShippingToleranceBehavior { get; set; }
        
        public int? ConvertedRequestedQuantity { get; set; }
        
        public string? LineNetWeight { get; set; }
        
        public string? LineGrossWeight { get; set; }
        
        public string? SupplierLotNumber { get; set; }
        
        public int? MovementRequestLineId { get; set; }
        
        public string? AllowItemSubstitutionFlag { get; set; }
        
        public string? OriginalItemId { get; set; }
        
        public string? OriginalItemNumber { get; set; }
        
        public string? OriginalDeliveryDetailId { get; set; }
        
        public int? OriginalItemConvertedQuantity { get; set; }
        
        public string? DestinationOrganizationCode { get; set; }
        
        public decimal? LineUnitPrice { get; set; }
        
        public string? LineUnitVolume { get; set; }
        
        public string? LineVolume { get; set; }
        
        public string? LineUnitWeight { get; set; }
        
        public decimal? LineSellingPrice { get; set; }
        
        public string? OriginalSourceOrderFulfillmentLineId { get; set; }
        
        public string? OriginalSourceOrderFulfillmentLine { get; set; }
    }

    public class ShipmentLineResponse : FusionResponseBase
    {
        public List<JBMShipmentLine>? Items { get; set; }
    }
}