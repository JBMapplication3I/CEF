// <copyright file="IProviderPayment.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPayment interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers
{
    /// <summary>Interface for payment.</summary>
    public interface IProviderPayment
    {
        /// <summary>Gets or sets the routing number.</summary>
        /// <value>The routing number.</value>
        string? RoutingNumber { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        string? AccountNumber { get; set; }

        /// <summary>Gets or sets the name of the bank.</summary>
        /// <value>The name of the bank.</value>
        string? BankName { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        decimal? Amount { get; set; }

        /// <summary>Gets or sets the card number.</summary>
        /// <value>The card number.</value>
        string? CardNumber { get; set; }

        /// <summary>Gets or sets the cvv.</summary>
        /// <value>The cvv.</value>
        string? CVV { get; set; }

        /// <summary>Gets or sets the expiration month.</summary>
        /// <value>The expiration month.</value>
        int? ExpirationMonth { get; set; }

        /// <summary>Gets or sets the expiration year.</summary>
        /// <value>The expiration year.</value>
        int? ExpirationYear { get; set; }

        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        string? Token { get; set; }

        /// <summary>Gets or sets the type of the card (or the type of eCheck account).</summary>
        /// <value>The type of the card (or the type of eCheck account).</value>
        string? CardType { get; set; }

        /// <summary>Gets or sets the name of the card/account holder.</summary>
        /// <value>The name of the card/account holder.</value>
        string? CardHolderName { get; set; }

        /// <summary>Gets or sets the zip.</summary>
        /// <value>The zip.</value>
        string? Zip { get; set; }

        /// <summary>Gets or sets the invoice number.</summary>
        /// <value>The invoice number.</value>
        string? InvoiceNumber { get; set; }

        /// <summary>Gets or sets the purchase order number.</summary>
        /// <value>The purchase order number.</value>
        string? PurchaseOrderNumber { get; set; }

        /// <summary>Gets or sets the identifier of the transaction.</summary>
        /// <value>The identifier of the transaction.</value>
        string? TransactionID { get; set; }

        /// <summary>Gets or sets the reference 1.</summary>
        /// <value>The reference 1.</value>
        string? Reference1 { get; set; }

        /// <summary>Gets or sets the reference 2.</summary>
        /// <value>The reference 2.</value>
        string? Reference2 { get; set; }

        /// <summary>Gets or sets the reference 3.</summary>
        /// <value>The reference 3.</value>
        string? Reference3 { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }
    }
}
