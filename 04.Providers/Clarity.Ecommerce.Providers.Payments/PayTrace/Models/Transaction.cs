// <copyright file="Transaction.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace Transaction class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace.Models
{
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;

    /// <summary>Class for Transaction.</summary>
    public class Transaction : ITransaction
    {
        /// <summary>Gets or sets the Transaction ID.</summary>
        /// <value>The Transaction ID.</value>
        [JsonProperty("transaction_id")]
        public string? TransactionID { get; set; }

        /// <summary>Gets or sets the Credit Card.</summary>
        /// <value>The Credit Card.</value>
        [JsonProperty("credit_card")]
        public CreditCard? CreditCard { get; set; }

        /// <inheritdoc/>
        ICreditCard? ITransaction.CreditCard { get => CreditCard; set => CreditCard = (CreditCard?)value; }

        /// <summary>Gets or sets the Transaction Type.</summary>
        /// <value>The Transaction Type.</value>
        [JsonProperty("transaction_type")]
        public string? TransactionType { get; set; }

        /// <summary>Gets or sets the Description.</summary>
        /// <value>The Description.</value>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>Gets or sets the Amount.</summary>
        /// <value>The Amount.</value>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>Gets or sets the Invoice ID.</summary>
        /// <value>The Invoice ID.</value>
        [JsonProperty("invoice_id")]
        public string? InvoiceID { get; set; }

        /// <summary>Gets or sets the Shipping Address.</summary>
        /// <value>The Shipping Address.</value>
        [JsonProperty("shipping_address")]
        public Address? ShippingAddress { get; set; }

        /// <inheritdoc/>
        IAddress? ITransaction.ShippingAddress { get => ShippingAddress; set => ShippingAddress = (Address?)value; }

        /// <summary>Gets or sets the Billing Address.</summary>
        /// <value>The Billing Address.</value>
        [JsonProperty("billing_address")]
        public Address? BillingAddress { get; set; }

        /// <inheritdoc/>
        IAddress? ITransaction.BillingAddress { get => BillingAddress; set => BillingAddress = (Address?)value; }

        /// <summary>Gets or sets the Receipt Emailed To.</summary>
        /// <value>The Receipt Emailed To.</value>
        [JsonProperty("receipt_emailed_to")]
        public string? ReceiptEmailedTo { get; set; }

        /// <summary>Gets or sets the Tax Amount.</summary>
        /// <value>The Tax Amount.</value>
        [JsonProperty("tax_amount")]
        public string? TaxAmount { get; set; }

        /// <summary>Gets or sets the Customer Reference ID.</summary>
        /// <value>The Customer Reference ID.</value>
        [JsonProperty("customer_reference_id")]
        public string? CustomerReferenceID { get; set; }

        /// <summary>Gets or sets the Approval Code.</summary>
        /// <value>The Approval Code.</value>
        [JsonProperty("approval_code")]
        public string? Approval_code { get; set; }

        /// <summary>Gets or sets the Approval Message.</summary>
        /// <value>The Approval Message.</value>
        [JsonProperty("approval_message")]
        public string? ApprovalMessage { get; set; }

        /// <summary>Gets or sets the Avs Response.</summary>
        /// <value>The Avs Response.</value>
        [JsonProperty("avs_response")]
        public string? AvsResponse { get; set; }

        /// <summary>Gets or sets the CSC Response.</summary>
        /// <value>The CSC Response.</value>
        [JsonProperty("csc_response")]
        public string? CscResponse { get; set; }

        /// <summary>Gets or sets the Status Code.</summary>
        /// <value>The Status Code.</value>
        [JsonProperty("status_code")]
        public string? StatusCode { get; set; }

        /// <summary>Gets or sets the Status Message.</summary>
        /// <value>The Status Message.</value>
        [JsonProperty("status_message")]
        public string? StatusMessage { get; set; }

        /// <summary>Gets or sets the Created object.</summary>
        /// <value>The Created object.</value>
        [JsonProperty("created")]
        public Created? Created { get; set; }

        /// <inheritdoc/>
        ICreated? ITransaction.Created { get => Created; set => Created = (Created?)value; }

        /// <summary>Gets or sets the settled date.</summary>
        /// <value>The settled date.</value>
        [JsonProperty("settled")]
        public string? Settled { get; set; }

        /// <summary>Gets or sets the Customer ID.</summary>
        /// <value>The Customer ID.</value>
        [JsonProperty("Customer_id")]
        public string? CustomerID { get; set; }

        /// <summary>Gets or sets the Discretionary Data.</summary>
        /// <value>The Discretionary Data.</value>
        [JsonProperty("discretionary_data")]
        public object? DiscretionaryData { get; set; }
    }
}
