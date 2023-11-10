// <copyright file="CreateLocationRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CreateLocationEnvelope
    {
        [XmlIgnore]
        public static string URI = "/crmService/FoundationPartiesLocationService?wsdl";

        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public object? Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public CreateLocationBody? Body { get; set; }

        //[XmlAttribute(AttributeName = "soapenv")]
        //public string? Soapenv { get; set; } = "http://schemas.xmlsoap.org/soap/envelope/";

        //[XmlAttribute(AttributeName = "loc")]
        //public string? Loc { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/";

        //[XmlAttribute(AttributeName = "loc1")]
        //public string? Loc1 { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/location/";

        //[XmlAttribute(AttributeName = "par")]
        //public string? Par { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/";

        //[XmlAttribute(AttributeName = "sour")]
        //public string? Sour { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/sourceSystemRef/";

        //[XmlAttribute(AttributeName = "typ")]
        //public string? Typ { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/";

        //[XmlText]
        //public string? Text { get; set; }
    }

    [XmlRoot(ElementName = "location", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/")]
    public class CreateLocationLocation
    {
        [XmlElement(ElementName = "Country", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? Country { get; set; }

        [XmlElement(ElementName = "Address1", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? Address1 { get; set; }

        [XmlElement(ElementName = "Address2", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? Address2 { get; set; }

        [XmlElement(ElementName = "City", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? City { get; set; }

        [XmlElement(ElementName = "PostalCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? PostalCode { get; set; }

        [XmlElement(ElementName = "State", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? State { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/")]
        public string? CreatedByModule { get; set; } = "HZ_WS";
    }

    [XmlRoot(ElementName = "createLocation", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/")]
    public class CreateLocationCreateLocation
    {
        [XmlElement(ElementName = "location", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/")]
        public CreateLocationLocation? Location { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CreateLocationBody
    {
        [XmlElement(ElementName = "createLocation", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/locationService/applicationModule/types/")]
        public CreateLocationCreateLocation? CreateLocation { get; set; }
    }
}
