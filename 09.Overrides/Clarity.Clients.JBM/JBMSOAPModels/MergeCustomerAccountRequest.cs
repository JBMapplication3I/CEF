// <copyright file="MergeCustomerAccountRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeCustomerAccountEnvelope
    {
        [XmlIgnore]
        public static string? URI { get; set; } = "/crmService/CustomerAccountService?wsdl";

        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public object? Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public MergeCustomerAccountBody? Body { get; set; }
    }

    [XmlRoot(ElementName = "CustomerAccountSiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class CustomerAccountSiteUse
    {
        [XmlElement(ElementName = "SiteUseCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? SiteUseCode { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedByModule { get; set; }
    }

    [XmlRoot(ElementName = "CustAcctSiteInformation", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class CustAcctSiteInformation
    {
        [XmlElement(ElementName = "contactNumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountSite/")]
        public string? contactNumber { get; set; }

        [XmlElement(ElementName = "contactFn", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountSite/")]
        public string? contactFn { get; set; }

        [XmlElement(ElementName = "contactLn", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccountSite/")]
        public string? contactLn { get; set; }
    }

    [XmlRoot(ElementName = "CustomerAccountSite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class MergeCustomerAccountCustomerAccountSite
    {
        [XmlElement(ElementName = "PartySiteId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? PartySiteId { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = "SetId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? SetId { get; set; }

        [XmlElement(ElementName = "CustAcctSiteInformation", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public CustAcctSiteInformation? CustAcctSiteInformation { get; set; }

        [XmlElement(ElementName = "CustomerAccountSiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public CustomerAccountSiteUse? CustomerAccountSiteUse { get; set; }
    }

    [XmlRoot(ElementName = "customerAccount", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
    public class MergeCustomerAccountCustomerAccount
    {
        [XmlElement(ElementName = "PartyId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? PartyId { get; set; }

        [XmlElement(ElementName = "CustomerAccountId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? CustomerAccountId { get; set; }

        [XmlElement(ElementName = "CustomerAccountSite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public MergeCustomerAccountCustomerAccountSite? CustomerAccountSite { get; set; }
    }

    [XmlRoot(ElementName = "mergeCustomerAccount", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
    public class MergeCustomerAccountMergeCustomerAccount
    {
        [XmlElement(ElementName = "customerAccount", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
        public MergeCustomerAccountCustomerAccount? CustomerAccount { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeCustomerAccountBody
    {
        [XmlElement(ElementName = "mergeCustomerAccount", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
        public MergeCustomerAccountMergeCustomerAccount? MergeCustomerAccount { get; set; }
    }
}