// <copyright file="CreateCustomerResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CreateCustomerResponseHeader
    {
        [XmlElement(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? Action { get; set; }

        [XmlElement(ElementName = "MessageID", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? MessageID { get; set; }
    }

    [XmlRoot(ElementName = "CustomerAccountSiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class CreateCustomerResponseCustomerAccountSiteUse
    {
        [XmlElement(ElementName = "SiteUseId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? SiteUseId { get; set; }

        [XmlElement(ElementName = "CustomerAccountSiteId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? CustomerAccountSiteId { get; set; }

        [XmlElement(ElementName = "LastUpdateDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? LastUpdateDate { get; set; }

        [XmlElement(ElementName = "LastUpdatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? LastUpdatedBy { get; set; }

        [XmlElement(ElementName = "CreationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? CreationDate { get; set; }

        [XmlElement(ElementName = "CreatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedBy { get; set; }

        [XmlElement(ElementName = "SiteUseCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? SiteUseCode { get; set; }

        [XmlElement(ElementName = "PrimaryFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public bool? PrimaryFlag { get; set; }

        [XmlElement(ElementName = "Status", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? Status { get; set; }

        [XmlElement(ElementName = "Location", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public int? Location { get; set; }

        [XmlElement(ElementName = "LastUpdateLogin", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? LastUpdateLogin { get; set; }

        [XmlElement(ElementName = "BillToSiteUseId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? BillToSiteUseId { get; set; }

        [XmlElement(ElementName = "OrigSystemReference", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? OrigSystemReference { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = "SetId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? SetId { get; set; }

        [XmlElement(ElementName = "EndDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? EndDate { get; set; }

        [XmlElement(ElementName = "StartDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? StartDate { get; set; }
    }

    [XmlRoot(ElementName = "CustomerAccountSite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class CreateCustomerResponseCustomerAccountSite
    {
        [XmlElement(ElementName = "CustomerAccountSiteId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? CustomerAccountSiteId { get; set; }

        [XmlElement(ElementName = "CustomerAccountId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? CustomerAccountId { get; set; }

        [XmlElement(ElementName = "PartySiteId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? PartySiteId { get; set; }

        [XmlElement(ElementName = "LastUpdateDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? LastUpdateDate { get; set; }

        [XmlElement(ElementName = "LastUpdatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? LastUpdatedBy { get; set; }

        [XmlElement(ElementName = "CreationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? CreationDate { get; set; }

        [XmlElement(ElementName = "CreatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedBy { get; set; }

        [XmlElement(ElementName = "LastUpdateLogin", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? LastUpdateLogin { get; set; }

        [XmlElement(ElementName = "OrigSystemReference", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? OrigSystemReference { get; set; }

        [XmlElement(ElementName = "Status", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? Status { get; set; }

        [XmlElement(ElementName = "BillToIndicator", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? BillToIndicator { get; set; }

        [XmlElement(ElementName = "MarketIndicator", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? MarketIndicator { get; set; }

        [XmlElement(ElementName = "ShipToIndicator", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? ShipToIndicator { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = "SetId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? SetId { get; set; }

        [XmlElement(ElementName = "StartDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? StartDate { get; set; }

        [XmlElement(ElementName = "EndDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? EndDate { get; set; }

        [XmlElement(ElementName = "SetCode", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? SetCode { get; set; }

        [XmlElement(ElementName = "CustomerAccountSiteUse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public List<CreateCustomerResponseCustomerAccountSiteUse>? CustomerAccountSiteUse { get; set; }
    }

    [XmlRoot(ElementName = "CustAcctInformation", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class CreateCustomerResponseCustAcctInformation
    {
        [XmlElement(ElementName = "CustAccountId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccount/")]
        public long? CustAccountId { get; set; }

        [XmlElement(ElementName = "jbmClassification", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccount/")]
        public string? JbmClassification { get; set; }

        [XmlElement(ElementName = "CustomerDEANumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccount/")]
        public string? CustomerDEANumber { get; set; }

        [XmlElement(ElementName = "_FLEX_NumOfSegments", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/flex/custAccount/")]
        public int? FLEXNumOfSegments { get; set; }
    }

    [XmlRoot(ElementName = "Value", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class CreateCustomerResponseValue
    {
        [XmlElement(ElementName = "CustomerAccountId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? CustomerAccountId { get; set; }

        [XmlElement(ElementName = "PartyId", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? PartyId { get; set; }

        [XmlElement(ElementName = "LastUpdateDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? LastUpdateDate { get; set; }

        [XmlElement(ElementName = "AccountNumber", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public int? AccountNumber { get; set; }

        [XmlElement(ElementName = "LastUpdatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? LastUpdatedBy { get; set; }

        [XmlElement(ElementName = "CreationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? CreationDate { get; set; }

        [XmlElement(ElementName = "CreatedBy", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedBy { get; set; }

        [XmlElement(ElementName = "LastUpdateLogin", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? LastUpdateLogin { get; set; }

        [XmlElement(ElementName = "OrigSystemReference", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public long? OrigSystemReference { get; set; }

        [XmlElement(ElementName = "Status", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? Status { get; set; }

        [XmlElement(ElementName = "AccountEstablishedDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? AccountEstablishedDate { get; set; }

        [XmlElement(ElementName = "AccountTerminationDate", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public DateTime? AccountTerminationDate { get; set; }

        [XmlElement(ElementName = "HoldBillFlag", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public bool? HoldBillFlag { get; set; }

        [XmlElement(ElementName = "AccountName", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? AccountName { get; set; }

        [XmlElement(ElementName = "CreatedByModule", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public string? CreatedByModule { get; set; }

        [XmlElement(ElementName = "CustomerAccountSite", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public CreateCustomerResponseCustomerAccountSite? CustomerAccountSite { get; set; }

        [XmlElement(ElementName = "CustAcctInformation", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public CreateCustomerResponseCustAcctInformation? CustAcctInformation { get; set; }
    }

    [XmlRoot(ElementName = "CustomerAccountResult", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
    public class CustomerAccountResult
    {
        [XmlElement(ElementName = "Value", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/")]
        public CreateCustomerResponseValue? Value { get; set; }
    }

    [XmlRoot(ElementName = "createCustomerAccountResponse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
    public class CreateCustomerAccountResponse
    {
        [XmlElement(ElementName = "result", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
        public CustomerAccountResult? CustomerAccountResult { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CreateCustomerResponseBody
    {
        [XmlElement(ElementName = "createCustomerAccountResponse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
        public CreateCustomerAccountResponse? CreateCustomerAccountResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class CreateCustomerResponseEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public CreateCustomerResponseHeader? Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public CreateCustomerResponseBody? Body { get; set; }
    }
}