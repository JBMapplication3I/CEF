// <copyright file="CreateLocationResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CreateLocationResponseHeader
    {
        [XmlElement(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? Action { get; set; }

        [XmlElement(ElementName = "MessageID", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? MessageID { get; set; }
    }

    [XmlRoot(ElementName = "Value", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
    public class CreateLocationResponseValue
    {
        [XmlElement(ElementName = "LocationId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public long? LocationId { get; set; }

        [XmlElement(ElementName = "LastUpdateDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public DateTime? LastUpdateDate { get; set; }

        [XmlElement(ElementName = "LastUpdatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? LastUpdatedBy { get; set; }

        [XmlElement(ElementName = "CreationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public DateTime? CreationDate { get; set; }

        [XmlElement(ElementName = "CreatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? CreatedBy { get; set; }

        [XmlElement(ElementName = "LastUpdateLogin", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? LastUpdateLogin { get; set; }

        [XmlElement(ElementName = "OrigSystemReference", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public long? OrigSystemReference { get; set; }

        [XmlElement(ElementName = "Country", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? Country { get; set; }

        [XmlElement(ElementName = "Address1", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? Address1 { get; set; }

        [XmlElement(ElementName = "City", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? City { get; set; }

        [XmlElement(ElementName = "PostalCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public int PostalCode { get; set; }

        [XmlElement(ElementName = "State", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? State { get; set; }

        [XmlElement(ElementName = "ValidatedFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public bool ValidatedFlag { get; set; }

        [XmlElement(ElementName = "SalesTaxInsideCityLimits", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public int SalesTaxInsideCityLimits { get; set; }

        [XmlElement(ElementName = "ObjectVersionNumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public int ObjectVersionNumber { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = "GeometryStatusCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? GeometryStatusCode { get; set; }

        [XmlElement(ElementName = "EffectiveDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public DateTime? EffectiveDate { get; set; }

        [XmlElement(ElementName = "StatusFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public bool StatusFlag { get; set; }

        [XmlElement(ElementName = "InternalFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public bool InternalFlag { get; set; }
    }

    [XmlRoot(ElementName = "LocationResult", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
    public class LocationResult
    {
        [XmlElement(ElementName = "Value", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public CreateLocationResponseValue? Value { get; set; }
    }

    [XmlRoot(ElementName = "createLocationResponse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/")]
    public class CreateLocationResponse
    {
        [XmlElement(ElementName = "result", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/")]
        public LocationResult? LocationResult { get; set; }
    }

    [XmlRoot(ElementName = "Body"/*, Namespace = "http://schemas.xmlsoap.org/soap/envelope/"*/)]
    public class CreateLocationResponseBody
    {
        [XmlElement(ElementName = "createLocationResponse"/*, Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/"*/)]
        public CreateLocationResponse? CreateLocationResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CreateLocationResponseEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public CreateLocationResponseHeader? Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public CreateLocationResponseBody? Body { get; set; }
    }
}