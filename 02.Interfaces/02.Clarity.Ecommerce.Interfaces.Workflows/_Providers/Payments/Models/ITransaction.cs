// <copyright file="ITransaction.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITransaction interface</summary>

namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    /// <summary>Interface for transaction model.</summary>
    public interface ITransaction
    {
        /// <summary>Gets or sets the Transaction ID.</summary>
        /// <value>The Transaction ID.</value>
        string? TransactionID { get; set; }

        /// <summary>Gets or sets the Credit Card.</summary>
        /// <value>The Credit Card.</value>
        ICreditCard? CreditCard { get; set; }

        /// <summary>Gets or sets the Transaction Type.</summary>
        /// <value>The Transaction Type.</value>
        string? TransactionType { get; set; }

        /// <summary>Gets or sets the Description.</summary>
        /// <value>The Description.</value>
        string? Description { get; set; }

        /// <summary>Gets or sets the Amount.</summary>
        /// <value>The Amount.</value>
        decimal Amount { get; set; }

        /// <summary>Gets or sets the Invoice ID.</summary>
        /// <value>The Invoice ID.</value>
        string? InvoiceID { get; set; }

        /// <summary>Gets or sets the Shipping Address.</summary>
        /// <value>The Shipping Address.</value>
        IAddress? ShippingAddress { get; set; }

        /// <summary>Gets or sets the Billing Address.</summary>
        /// <value>The Billing Address.</value>
        IAddress? BillingAddress { get; set; }

        /// <summary>Gets or sets the Receipt Emailed To.</summary>
        /// <value>The Receipt Emailed To.</value>
        string? ReceiptEmailedTo { get; set; }

        /// <summary>Gets or sets the Tax Amount.</summary>
        /// <value>The Tax Amount.</value>
        string? TaxAmount { get; set; }

        /// <summary>Gets or sets the Customer Reference ID.</summary>
        /// <value>The Customer Reference ID.</value>
        string? CustomerReferenceID { get; set; }

        /// <summary>Gets or sets the Approval Code.</summary>
        /// <value>The Approval Code.</value>
        string? Approval_code { get; set; }

        /// <summary>Gets or sets the Approval Message.</summary>
        /// <value>The Approval Message.</value>
        string? ApprovalMessage { get; set; }

        /// <summary>Gets or sets the Avs Response.</summary>
        /// <value>The Avs Response.</value>
        string? AvsResponse { get; set; }

        /// <summary>Gets or sets the CSC Response.</summary>
        /// <value>The CSC Response.</value>
        string? CscResponse { get; set; }

        /// <summary>Gets or sets the Status Code.</summary>
        /// <value>The Status Code.</value>
        string? StatusCode { get; set; }

        /// <summary>Gets or sets the Status Message.</summary>
        /// <value>The Status Message.</value>
        string? StatusMessage { get; set; }

        /// <summary>Gets or sets the Created object.</summary>
        /// <value>The Created object.</value>
        ICreated? Created { get; set; }

        /// <summary>Gets or sets the settled date.</summary>
        /// <value>The settled date.</value>
        string? Settled { get; set; }

        /// <summary>Gets or sets the Customer ID.</summary>
        /// <value>The Customer ID.</value>
        string? CustomerID { get; set; }

        /// <summary>Gets or sets the Discretionary Data.</summary>
        /// <value>The Discretionary Data.</value>
        object? DiscretionaryData { get; set; }
    }
}
