// <copyright file="MergeCustomerAccountResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Clients.JBM
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeCustomerAccountResponseHeader
    {
        [XmlElement(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? Action { get; set; }

        [XmlElement(ElementName = "MessageID", Namespace = "http://www.w3.org/2005/08/addressing")]
        public string? MessageID { get; set; }
    }

    [XmlRoot(ElementName = "mergeCustomerAccountResponse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
    public class MergeCustomerAccountResponse
    {
        [XmlElement(ElementName = "result", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
        public CustomerAccountResult? CustomerAccountResult { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeCustomerAccountResponseBody
    {
        [XmlElement(ElementName = "mergeCustomerAccountResponse", Namespace = "http://xmlns.oracle.com/apps/cdm/foundation/parties/customerAccountService/applicationModule/types/")]
        public MergeCustomerAccountResponse? MergeCustomerAccountResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class MergeCustomerAccountResponseEnvelope
    {
        [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public MergeCustomerAccountResponseHeader? Header { get; set; }

        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public MergeCustomerAccountResponseBody? Body { get; set; }
    }
}