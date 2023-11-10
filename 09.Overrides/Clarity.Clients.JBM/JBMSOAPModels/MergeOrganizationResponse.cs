// <copyright file="MergeOrganizationResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Header
    {
        [XmlElement(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? Action { get; set; }

        [XmlElement(ElementName = "MessageID", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? MessageID { get; set; }
    }

    [XmlRoot(ElementName = "PartySiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
    public class MergeOrganizationResponsePartySiteUse
    {
        [XmlElement(ElementName = "PartySiteUseId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public long PartySiteUseId { get; set; }

        [XmlElement(ElementName = "BeginDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime BeginDate { get; set; }

        [XmlElement(ElementName = "EndDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime EndDate { get; set; }

        [XmlElement(ElementName = "PartySiteId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public long PartySiteId { get; set; }

        [XmlElement(ElementName = "LastUpdateDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime LastUpdateDate { get; set; }

        [XmlElement(ElementName = "LastUpdatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? LastUpdatedBy { get; set; }

        [XmlElement(ElementName = "CreationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime CreationDate { get; set; }

        [XmlElement(ElementName = "CreatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CreatedBy { get; set; }

        [XmlElement(ElementName = "LastUpdateLogin", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? LastUpdateLogin { get; set; }

        [XmlElement(ElementName = "IntegrationKey", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? IntegrationKey { get; set; }

        [XmlElement(ElementName = "SiteUseType", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? SiteUseType { get; set; }

        [XmlElement(ElementName = "PrimaryPerType", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? PrimaryPerType { get; set; }

        [XmlElement(ElementName = "Status", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? Status { get; set; }

        [XmlElement(ElementName = "ObjectVersionNumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public int ObjectVersionNumber { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CreatedByModule { get; set; }
    }

    [XmlRoot(ElementName = "PartySite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
    public class MergeOrganizationResponsePartySite
    {
        [XmlElement(ElementName = "PartySiteId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public long? PartySiteId { get; set; }

        [XmlElement(ElementName = "PartyId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public long? PartyId { get; set; }

        [XmlElement(ElementName = "LocationId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public long? LocationId { get; set; }

        [XmlElement(ElementName = "LastUpdateDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime LastUpdateDate { get; set; }

        [XmlElement(ElementName = "PartySiteNumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? PartySiteNumber { get; set; }

        [XmlElement(ElementName = "LastUpdatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? LastUpdatedBy { get; set; }

        [XmlElement(ElementName = "CreationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime CreationDate { get; set; }

        [XmlElement(ElementName = "CreatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CreatedBy { get; set; }

        [XmlElement(ElementName = "LastUpdateLogin", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? LastUpdateLogin { get; set; }

        [XmlElement(ElementName = "OrigSystemReference", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public long? OrigSystemReference { get; set; }

        [XmlElement(ElementName = "StartDateActive", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime StartDateActive { get; set; }

        [XmlElement(ElementName = "EndDateActive", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public DateTime EndDateActive { get; set; }

        [XmlElement(ElementName = "IdentifyingAddressFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public bool IdentifyingAddressFlag { get; set; }

        [XmlElement(ElementName = "Status", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? Status { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = "UsageCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? UsageCode { get; set; }

        [XmlElement(ElementName = "FormattedAddress", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? FormattedAddress { get; set; }

        [XmlElement(ElementName = "Country", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? Country { get; set; }

        [XmlElement(ElementName = "Address1", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? Address1 { get; set; }

        [XmlElement(ElementName = "Address2", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? Address2 { get; set; }

        [XmlElement(ElementName = "City", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? City { get; set; }

        [XmlElement(ElementName = "PostalCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public int PostalCode { get; set; }

        [XmlElement(ElementName = "State", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? State { get; set; }

        [XmlElement(ElementName = "ValidatedFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public bool ValidatedFlag { get; set; }

        [XmlElement(ElementName = "FormattedMultilineAddress", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? FormattedMultilineAddress { get; set; }

        [XmlElement(ElementName = "Country1", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? Country1 { get; set; }

        [XmlElement(ElementName = "ObjectVersionNumber1", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public int ObjectVersionNumber1 { get; set; }

        [XmlElement(ElementName = "ContactPreferenceExistFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public bool ContactPreferenceExistFlag { get; set; }

        [XmlElement(ElementName = "CurrencyCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CurrencyCode { get; set; }

        [XmlElement(ElementName = "CorpCurrencyCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CorpCurrencyCode { get; set; }

        [XmlElement(ElementName = "CurcyConvRateType", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CurcyConvRateType { get; set; }

        [XmlElement(ElementName = "InternalFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public bool InternalFlag { get; set; }

        [XmlElement(ElementName = "OverallPrimaryFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public bool OverallPrimaryFlag { get; set; }

        [XmlElement(ElementName = "EmailAddress", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? EmailAddress { get; set; }

        [XmlElement(ElementName = "PartySiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public MergeOrganizationResponsePartySiteUse? PartySiteUse { get; set; }
    }

    [XmlRoot(ElementName = "Value", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
    public class MergeOrganizationResponseValue
    {
        [XmlElement(ElementName = "PartyNumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public int PartyNumber { get; set; }

        [XmlElement(ElementName = "PartyId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public long? PartyId { get; set; }

        [XmlElement(ElementName = "PartyType", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? PartyType { get; set; }

        [XmlElement(ElementName = "PartyName", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? PartyName { get; set; }

        [XmlElement(ElementName = "LastUpdatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? LastUpdatedBy { get; set; }

        [XmlElement(ElementName = "ValidatedFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public bool ValidatedFlag { get; set; }

        [XmlElement(ElementName = "LastUpdateLogin", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? LastUpdateLogin { get; set; }

        [XmlElement(ElementName = "CreationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public DateTime CreationDate { get; set; }

        [XmlElement(ElementName = "RequestId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public long? RequestId { get; set; }

        [XmlElement(ElementName = "LastUpdateDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public DateTime LastUpdateDate { get; set; }

        [XmlElement(ElementName = "CreatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? CreatedBy { get; set; }

        [XmlElement(ElementName = "OrigSystemReference", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public int OrigSystemReference { get; set; }

        [XmlElement(ElementName = "Address1", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? Address1 { get; set; }

        [XmlElement(ElementName = "Country", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? Country { get; set; }

        [XmlElement(ElementName = "Status", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? Status { get; set; }

        [XmlElement(ElementName = "City", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? City { get; set; }

        [XmlElement(ElementName = "PostalCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public int PostalCode { get; set; }

        [XmlElement(ElementName = "State", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? State { get; set; }

        [XmlElement(ElementName = "EmailAddress", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? EmailAddress { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = "ObjectVersionNumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public int ObjectVersionNumber { get; set; }

        [XmlElement(ElementName = "IdenAddrLocationId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public long? IdenAddrLocationId { get; set; }

        [XmlElement(ElementName = "PrimaryEmailContactPointId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public long? PrimaryEmailContactPointId { get; set; }

        [XmlElement(ElementName = "IdenAddrPartySiteId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public long? IdenAddrPartySiteId { get; set; }

        [XmlElement(ElementName = "PreferredContactPersonId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public long? PreferredContactPersonId { get; set; }

        [XmlElement(ElementName = "InternalFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public bool InternalFlag { get; set; }

        [XmlElement(ElementName = "PartyUniqueName", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public string? PartyUniqueName { get; set; }

        [XmlElement(ElementName = "PartySite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public MergeOrganizationResponsePartySite? PartySite { get; set; }
    }

    [XmlRoot(ElementName = "result", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
    public class MergeOrganizationResponseResult
    {
        [XmlElement(ElementName = "Value", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public MergeOrganizationResponseValue? Value { get; set; }
    }

    [XmlRoot(ElementName = "mergeOrganizationResponse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
    public class MergeOrganizationResponse
    {
        [XmlElement(ElementName = "result", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
        public MergeOrganizationResponseResult? Result { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeOrganizationResponseBody
    {
        [XmlElement(ElementName = "mergeOrganizationResponse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
        public MergeOrganizationResponse? MergeOrganizationResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeOrganizationResponseEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Header? Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public MergeOrganizationResponseBody? Body { get; set; }
    }
}