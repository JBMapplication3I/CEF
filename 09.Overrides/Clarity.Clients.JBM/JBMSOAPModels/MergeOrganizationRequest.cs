// <copyright file="MergeOrganizationRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System.Xml.Serialization;

    //!!!!!!!!!!!!!!! ALL CASE SENSITIVE !!!!!!!!!!!!!!!!!!!!!!!!!
    [XmlRoot(ElementName = "PartySiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
    public class PartySiteUse
    {
        [XmlElement(ElementName = "SiteUseType", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? SiteUseType { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CreatedByModule { get; set; } = "HZ_WS";
    }

    [XmlRoot(ElementName = "PartySite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
    public class PartySite
    {
        [XmlElement(ElementName = "LocationId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public long? LocationId { get; set; }

        [XmlElement(ElementName = "PartySiteName", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? PartySiteName { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public string? CreatedByModule { get; set; } = "HZ_WS";

        [XmlElement(ElementName = "PartySiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
        public PartySiteUse? PartySiteUse { get; set; }
    }

    [XmlRoot(ElementName = "organizationParty", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
    public class OrganizationParty
    {
        [XmlElement(ElementName = "PartyId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public long? PartyId { get; set; }

        [XmlElement(ElementName = "PartySite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
        public PartySite? PartySite { get; set; }
    }

    [XmlRoot(ElementName = "mergeOrganization", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
    public class MergeOrganization
    {
        [XmlElement(ElementName = "organizationParty", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
        public OrganizationParty? OrganizationParty { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
    public class MergeOrganizationBody
    {
        [XmlElement(ElementName = "mergeOrganization", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
        public MergeOrganization? MergeOrganization { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeOrganizationEnvelope
    {
        [XmlIgnore]
        public static string? URI = "/foundationParties/OrganizationService";

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public MergeOrganizationBody? Body { get; set; }
    }

    //[XmlRoot(ElementName = "ns3:PartySiteUse")]
    //public class PartySiteUse
    //{
    //    [XmlElement(ElementName = "ns3:SiteUseType")]
    //    public string? SiteUseType { get; set; }

    //    [XmlElement(ElementName = "ns3:CreatedByModule")]
    //    public string? CreatedByModule { get; set; } = "HZ_WS";
    //}

    //[XmlRoot(ElementName = "ns3:PartySite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/")]
    //public class PartySite
    //{
    //    [XmlElement(ElementName = "ns3:LocationId")]
    //    public long? LocationId { get; set; }

    //    [XmlElement(ElementName = "ns3:CreatedByModule")]
    //    public string? CreatedByModule { get; set; } = "HZ_WS";

    //    [XmlElement(ElementName = "ns3:PartySiteUse")]
    //    public PartySiteUse? PartySiteUse { get; set; }
    //}

    //[XmlRoot(ElementName = "ns1:OrganizationParty", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/")]
    //public class OrganizationParty
    //{
    //    [XmlElement(ElementName = "ns2:PartyId")]
    //    public long? PartyId { get; set; }

    //    [XmlElement(ElementName = "ns2:PartySite")]
    //    public PartySite? PartySite { get; set; }
    //}

    //[XmlRoot(ElementName = "ns1:MergeOrganization", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/")]
    //public class MergeOrganization
    //{
    //    [XmlElement(ElementName = "ns1:OrganizationParty")]
    //    public OrganizationParty? OrganizationParty { get; set; }
    //}

    //[XmlRoot(ElementName = "soap:Body")]
    //public class MergeOrganizationBody
    //{
    //    [XmlElement(ElementName = "ns1:MergeOrganization")]
    //    public MergeOrganization? MergeOrganization { get; set; }
    //}

    //[XmlRoot(ElementName = "soap:Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    //public class MergeOrganizationEnvelope
    //{
    //    [XmlIgnore]
    //    public string? URI = "/foundationParties/OrganizationService";

    //    [XmlElement(ElementName = "soap:Body")]
    //    public MergeOrganizationBody? Body { get; set; }
    //}

    //< soap:Envelope xmlns:soap = "http://schemas.xmlsoap.org/soap/envelope/" >
    //< soap:Body >

    //    < ns1:mergeOrganization xmlns:ns1 = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/" >

    //    < ns1:organizationParty xmlns:ns2 = "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/" >

    //        < ns2:PartyId > 1003 </ ns2:PartyId >

    //        < ns2:PartySite xmlns:ns3 = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/" >

    //            < ns3:LocationId > 300100554888521 </ ns3:LocationId >

    //            < ns3:CreatedByModule > HZ_WS </ ns3:CreatedByModule >

    //            < ns3:PartySiteUse >

    //            < ns3:SiteUseType > BILL_TO </ ns3:SiteUseType >

    //            < ns3:CreatedByModule > HZ_WS </ ns3:CreatedByModule >

    //            </ ns3:PartySiteUse >

    //        </ ns2:PartySite >

    //    </ ns1:organizationParty >

    //    </ ns1:mergeOrganization >
    //</ soap:Body >
    //</ soap:Envelope >
}