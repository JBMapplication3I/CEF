// <copyright file="FusionInvoiceResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionInvoiceResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ServiceStack;

    public class FusionInvoiceHeader
    {
        public int? CustomerTransactionId { get; set; }

        public string? TransactionType { get; set; }

        public string? PaymentTerms { get; set; }

        public DateTime? DueDate { get; set; }

        public string? ConversionDate { get; set; }

        public string? ConversionRate { get; set; }

        public string? InvoiceCurrencyCode { get; set; }

        public string? SpecialInstructions { get; set; }

        public string? CrossReference { get; set; }

        public string? DocumentNumber { get; set; }

        public string? TransactionNumber { get; set; }

        public string? TransactionDate { get; set; }

        public string? TransactionSource { get; set; }

        public string? BillToCustomerNumber { get; set; }

        public string? BillToSite { get; set; }

        public string? Comments { get; set; }

        public string? InternalNotes { get; set; }

        public string? LegalEntityIdentifier { get; set; }

        public string? ConversionRateType { get; set; }

        public string? PurchaseOrder { get; set; }

        public string? PurchaseOrderDate { get; set; }

        public string? PurchaseOrderRevision { get; set; }

        public string? FirstPartyRegistrationNumber { get; set; }

        public string? ThirdPartyRegistrationNumber { get; set; }

        public string? InvoicingRule { get; set; }

        public string? ShipToCustomerName { get; set; }

        public string? ShipToCustomerNumber { get; set; }

        public long? BillToPartyId { get; set; }

        public string? BusinessUnit { get; set; }

        public string? InvoiceStatus { get; set; }

        public string? AccountingDate { get; set; }

        public string? ShipToSite { get; set; }

        public string? PayingCustomerName { get; set; }

        public string? PayingCustomerSite { get; set; }

        public string? BillToCustomerName { get; set; }

        public decimal? FreightAmount { get; set; }

        public string? Carrier { get; set; }

        public string? ShipDate { get; set; }

        public string? ShippingReference { get; set; }

        public string? BillToContact { get; set; }

        public string? ShipToContact { get; set; }

        public string? PrintOption { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string? LastUpdatedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? PayingCustomerAccount { get; set; }

        public string? SoldToPartyNumber { get; set; }

        public string? RemitToAddress { get; set; }

        public string? DefaultTaxationCountry { get; set; }

        public decimal? EnteredAmount { get; set; }

        public decimal? InvoiceBalanceAmount { get; set; }

        public string? Prepayment { get; set; }

        public string? Intercompany { get; set; }

        public string? DocumentFiscalClassification { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? CreditCardAuthorizationRequestIdentifier { get; set; }

        public string? CreditCardExpirationDate { get; set; }

        public string? CreditCardIssuerCode { get; set; }

        public string? CreditCardTokenNumber { get; set; }

        public string? CreditCardVoiceAuthorizationCode { get; set; }

        public string? CreditCardErrorCode { get; set; }

        public string? CreditCardErrorText { get; set; }

        public string? CardHolderLastName { get; set; }

        public string? CardHolderFirstName { get; set; }

        public string? ReceiptMethod { get; set; }

        public string? SalesPersonNumber { get; set; }

        public string? StructuredPaymentReference { get; set; }

        public string? InvoicePrinted { get; set; }

        public string? LastPrintDate { get; set; }

        public string? OriginalPrintDate { get; set; }

        public string? DeliveryMethod { get; set; }

        public string? Email { get; set; }

        public string? AllowCompletion { get; set; }

        public string? ControlCompletionReason { get; set; }

        public List<Link>? Links { get; set; }
    }

    public class FusionInvoiceResponse : FusionResponseBase
    {
        [ApiMember(Name = nameof(Items), DataType = "List<FusionInvoiceHeader>?", ParameterType = "body", IsRequired = true)]
        [JsonProperty("items")]
        public List<FusionInvoiceHeader>? Items { get; set; }
    }
}