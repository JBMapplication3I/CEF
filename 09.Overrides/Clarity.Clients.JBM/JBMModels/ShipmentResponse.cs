// <copyright file="ShipmentResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Shipment Response class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    [JsonObject("Item")]
    public class JBMShipment
    {
        
        public string? Shipment { get; set; }
        
        public int? DeliveryId { get; set; }
        
        public string? ShipmentDescription { get; set; }
        
        public string? InitialPickupDate { get; set; }
        
        public DateTime? InitialPickupDateTime { get; set; }
        
        public string? BillOfLading { get; set; }
        
        public string? GrossWeight { get; set; }
        
        public string? NetWeight { get; set; }
        
        public string? TareWeight { get; set; }
        
        public string? WeightUOMCode { get; set; }
        
        public string? WeightUOM { get; set; }
        
        public string? EquipmentTypeId { get; set; }
        
        public string? EquipmentType { get; set; }
        
        public string? Equipment { get; set; }
        
        public string? SealNumber { get; set; }
        
        public string? Volume { get; set; }
        
        public string? VolumeUOMCode { get; set; }
        
        public string? VolumeUOM { get; set; }
        
        public string? CarrierId { get; set; }
        
        public string? Carrier { get; set; }
        
        public string? CarrierNumber { get; set; }
        
        public string? ModeOfTransportCode { get; set; }
        
        public string? ModeOfTransport { get; set; }
        
        public string? ServiceLevelCode { get; set; }
        
        public string? ServiceLevel { get; set; }
        
        public string? ShippingMethodCode { get; set; }
        
        public string? ShippingMethod { get; set; }
        
        public string? Waybill { get; set; }
        
        public string? PackingSlipNumber { get; set; }
        
        public string? ActualShipDate { get; set; }
        
        public DateTime? ActualShipDateTime { get; set; }
        
        public string? UltimateDropoffDate { get; set; }
        
        public DateTime? UltimateDropoffDateTime { get; set; }
        
        public string? FreightTermsCode { get; set; }
        
        public string? FreightTerms { get; set; }
        
        public string? FOBCode { get; set; }
        
        public string? FOB { get; set; }
        
        public string? FOBSiteId { get; set; }
        
        public string? FOBSiteNumber { get; set; }
        
        public string? FOBLocationId { get; set; }
        
        public string? FOBAddress1 { get; set; }
        
        public string? FOBAddress2 { get; set; }
        
        public string? FOBAddress3 { get; set; }
        
        public string? FOBAddress4 { get; set; }
        
        public string? FOBCity { get; set; }
        
        public string? FOBCounty { get; set; }
        
        public string? FOBPostalCode { get; set; }
        
        public string? FOBRegion { get; set; }
        
        public string? FOBState { get; set; }
        
        public string? FOBCountry { get; set; }
        
        public string? DockCode { get; set; }
        
        public string? CODAmount { get; set; }
        
        public string? CODPaidBy { get; set; }
        
        public string? CODCurrencyCode { get; set; }
        
        public string? CODRemitTo { get; set; }
        
        public string? ASNDateSent { get; set; }
        
        public DateTime? ASNDateTimeSent { get; set; }
        
        public string? ConfirmedDate { get; set; }
        
        public DateTime? ConfirmedDateTime { get; set; }
        
        public string? ConfirmedBy { get; set; }
        
        public string? LoadingSequenceRule { get; set; }
        
        public string? LoadingOrderRuleCode { get; set; }
        
        public string? LoadingOrderRule { get; set; }
        
        public string? ProblemContactReference { get; set; }
        
        public string? OrganizationId { get; set; }
        
        public string? OrganizationCode { get; set; }
        
        public string? OrganizationName { get; set; }
        
        public string? RcvShipmentNumber { get; set; }
        
        public string? RcvShipmentHeaderId { get; set; }
        
        public string? ShipmentStatusCode { get; set; }
        
        public string? ShipmentStatus { get; set; }
        
        public string? CreatedBy { get; set; }
        
        public DateTime? CreationDate { get; set; }
        
        public string? LastUpdatedBy { get; set; }
        
        public DateTime? LastUpdateDate { get; set; }
        
        public string? SourceSystemId { get; set; }
        
        public string? SourceSystem { get; set; }
        
        public string? ShipFromLocationId { get; set; }
        
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
        
        public string? ShipToLocationId { get; set; }
        
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
        
        public string? TransportationReasonCode { get; set; }
        
        public string? TransportationReason { get; set; }
        
        public string? PortOfLoading { get; set; }
        
        public string? PortOfDischarge { get; set; }
        
        public string? NumberOfOuterPackingUnits { get; set; }
        
        public string? EarliestPickupDate { get; set; }
        
        public string? LatestPickupDate { get; set; }
        
        public string? EarliestDropoffDate { get; set; }
        
        public string? LatestDropoffDate { get; set; }
        
        public string? ShipToPartyId { get; set; }
        
        public string? ShipToCustomer { get; set; }
        
        public string? ShipToCustomerNumber { get; set; }
        
        public string? ShipToPartySiteId { get; set; }
        
        public string? ShipToPartySiteNumber { get; set; }
        
        public string? SoldToPartyId { get; set; }
        
        public string? SoldToCustomer { get; set; }
        
        public string? SoldToCustomerNumber { get; set; }
        
        public string? Supplier { get; set; }
        
        public string? SupplierPartyNumber { get; set; }
        
        public string? LogisticsServiceProviderCustomerId { get; set; }
        
        public string? LogisticsServiceProviderCustomer { get; set; }
        
        public string? LogisticsServiceProviderCustomerNumber { get; set; }
        
        public string? TransportationShipment { get; set; }
        
        public string? DistributedOrganizationFlag { get; set; }
        
        public string? CommercialInvoice { get; set; }
        
        public bool? PlannedFlag { get; set; }
        
        public bool? AutomaticallyPackFlag { get; set; }
        
        public bool? EnableAutoshipFlag { get; set; }
        
        public string? TransportationShipmentFromEvent { get; set; }
        
        public string? TransportationShipmentCompleteFromEvent { get; set; }
        
        public string? TradeComplianceScreenedFromEvent { get; set; }
        
        public string? ExternalSystemTransactionReference { get; set; }
    }

    public class ShipmentResponse : FusionResponseBase
    {
        public List<JBMShipment>? Items { get; set; }
    }
}