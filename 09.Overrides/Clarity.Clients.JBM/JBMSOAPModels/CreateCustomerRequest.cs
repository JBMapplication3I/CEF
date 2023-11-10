// <copyright file="CreateCustomerRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = @"soapenv:Envelope")]
    public class CreateCustomerEnvelope
    {
        [XmlElement(ElementName = "Header")]
        public object? Header { get; set; }

        [XmlElement(ElementName = @"soapenv:Body")]
        public CreateCustomerBody? Body { get; set; }

        [XmlAttribute(AttributeName = "soapenv")]
        public string? Soapenv { get; set; } = "http://schemas.xmlsoap.org/soap/envelope/";

        [XmlAttribute(AttributeName = "cus")]
        public string? Cus { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/";

        [XmlAttribute(AttributeName = "cus1")]
        public string? Cus1 { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountContactRole/";

        [XmlAttribute(AttributeName = "cus2")]
        public string? Cus2 { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountContact/";

        [XmlAttribute(AttributeName = "cus3")]
        public string? Cus3 { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountRel/";

        [XmlAttribute(AttributeName = "cus4")]
        public string? Cus4 { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountSiteUse/";

        [XmlAttribute(AttributeName = "cus5")]
        public string? Cus5 { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountSite/";

        [XmlAttribute(AttributeName = "cus6")]
        public string? Cus6 { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccount/";

        [XmlAttribute(AttributeName = "par")]
        public string? Par { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/";

        [XmlAttribute(AttributeName = "sour")]
        public string? Sour { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/sourceSystemRef/";

        [XmlAttribute(AttributeName = "typ")]
        public string? Typ { get; set; } = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/";

        [XmlAttribute(AttributeName = "xsi")]
        public string? Xsi { get; set; } = "http://www.w3.org/2001/XMLSchema-instance";

        [XmlText]
        public string? Text { get; set; }
    }

    [XmlRoot(ElementName = @"cus:CustomerAccountSiteUse")]
    public class CreateCustomerCustomerAccountSiteUse
    {
        [XmlElement(ElementName = @"cus:SiteUseCode")]
        public string? SiteUseCode { get; set; }

        [XmlElement(ElementName = @"cus:CreatedByModule")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = @"cus:SetId")]
        public long? SetId { get; set; }
    }

    [XmlRoot(ElementName = @"cus:CustomerAccountSite")]
    public class CreateCustomerCustomerAccountSite
    {
        [XmlElement(ElementName = @"cus:PartySiteId")]
        public long? PartySiteId { get; set; }

        [XmlElement(ElementName = @"cus:CreatedByModule")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = @"cus:SetId")]
        public long? SetId { get; set; }

        [XmlElement(ElementName = @"cus:CustomerAccountSiteUse")]
        public List<CreateCustomerCustomerAccountSiteUse>? CustomerAccountSiteUse { get; set; }
    }

    [XmlRoot(ElementName = @"cus:CustAcctInformation")]
    public class CreateCustomerCustAcctInformation
    {
        [XmlElement(ElementName = @"cus6:CustomerDEANumber")]
        public string? CustomerDEANumber { get; set; }

        [XmlElement(ElementName = @"cus6:jbmClassification")]
        public string? JbmClassification { get; set; }

        [XmlElement(ElementName = @"cus6:__FLEX_Context")]
        public string? FLEXContext { get; set; }
    }

    [XmlRoot(ElementName = @"typ:customerAccount")]
    public class CreateCustomerCustomerAccount
    {
        [XmlElement(ElementName = @"cus:PartyId")]
        public long? PartyId { get; set; }

        [XmlElement(ElementName = @"cus:Status")]
        public string? Status { get; set; }

        [XmlElement(ElementName = @"cus:AccountName")]
        public string? AccountName { get; set; }

        [XmlElement(ElementName = @"cus:CreatedByModule")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = @"cus:CustomerAccountSite")]
        public CreateCustomerCustomerAccountSite? CustomerAccountSite { get; set; }

        [XmlElement(ElementName = @"cus:CustAcctInformation")]
        public CreateCustomerCustAcctInformation? CustAcctInformation { get; set; }
    }

    [XmlRoot(ElementName = @"typ:createCustomerAccount")]
    public class CreateCustomerCreateCustomerAccount
    {
        [XmlElement(ElementName = @"typ:customerAccount")]
        public CreateCustomerCustomerAccount? CustomerAccount { get; set; }
    }

    [XmlRoot(ElementName = @"soapenv:Body")]
    public class CreateCustomerBody
    {
        [XmlElement(ElementName = @"typ:createCustomerAccount")]
        public CreateCustomerCreateCustomerAccount? CreateCustomerAccount { get; set; }
    }
}
