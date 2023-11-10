// <copyright file="ICheckoutModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICheckoutModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for checkout model.</summary>
    public interface ICheckoutModel
    {
        #region With Taxes
        /// <summary>Gets or sets the with taxes.</summary>
        /// <value>The with taxes.</value>
        ICheckoutWithTaxes? WithTaxes { get; set; }
        #endregion

        #region With Cart Info
        /// <summary>Gets or sets information describing the with cart.</summary>
        /// <value>Information describing the with cart.</value>
        ICheckoutWithCartInfo? WithCartInfo { get; set; }
        #endregion

        #region With User Info
        /// <summary>Gets or sets information describing the with user.</summary>
        /// <value>Information describing the with user.</value>
        ICheckoutWithUserInfo? WithUserInfo { get; set; }
        #endregion

        #region Wallet Entry Payment method
        /// <summary>Gets or sets the pay by wallet entry.</summary>
        /// <value>The pay by wallet entry.</value>
        ICheckoutPayByWalletEntry? PayByWalletEntry { get; set; }
        #endregion

        #region Credit Card Payment Method
        /// <summary>Gets or sets the pay by credit card.</summary>
        /// <value>The pay by credit card.</value>
        ICheckoutPayByCreditCard? PayByCreditCard { get; set; }
        #endregion

        #region eCheck Payment Method
        /// <summary>Gets or sets the pay by e check.</summary>
        /// <value>The pay by e check.</value>
        ICheckoutPayByECheck? PayByECheck { get; set; }
        #endregion

        #region Bill Me Later Method
        /// <summary>Gets or sets the pay by bill me later.</summary>
        /// <value>The pay by bill me later.</value>
        ICheckoutPayByBillMeLater? PayByBillMeLater { get; set; }
        #endregion

        #region PayPal Method
        /// <summary>Gets or sets the pay by pay palette.</summary>
        /// <value>The pay by pay palette.</value>
        ICheckoutPayByPayPal? PayByPayPal { get; set; }
        #endregion

        #region Payoneer method
        /// <summary>Gets or sets the pay by payoneer.</summary>
        /// <value>The pay by payoneer.</value>
        ICheckoutPayByPayoneer? PayByPayoneer { get; set; }
        #endregion

        #region Other Things
        /// <summary>Gets or sets the identifier of the referring store.</summary>
        /// <value>The identifier of the referring store.</value>
        int? ReferringStoreID { get; set; }

        /// <summary>Gets or sets the identifier of the referring brand.</summary>
        /// <value>The identifier of the referring brand.</value>
        int? ReferringBrandID { get; set; }

        /// <summary>Gets or sets the affiliate account key.</summary>
        /// <value>The affiliate account key.</value>
        string? AffiliateAccountKey { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the shipping.</summary>
        /// <value>The shipping.</value>
        IContactModel? Shipping { get; set; }

        /// <summary>Gets or sets the special instructions.</summary>
        /// <value>The special instructions.</value>
        string? SpecialInstructions { get; set; }

        /// <summary>Gets or sets a value indicating whether this ICheckoutModel is same as billing.</summary>
        /// <value>True if this ICheckoutModel is same as billing, false if not.</value>
        bool IsSameAsBilling { get; set; }

        /// <summary>Gets or sets the billing.</summary>
        /// <value>The billing.</value>
        IContactModel? Billing { get; set; }

        /// <summary>Gets or sets the payment style.</summary>
        /// <value>The payment style.</value>
        string? PaymentStyle { get; set; }

        /// <summary>Gets or sets a value indicating whether this ICheckoutModel is partial payment.</summary>
        /// <value>True if this ICheckoutModel is partial payment, false if not.</value>
        bool IsPartialPayment { get; set; }

        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        decimal? Amount { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        string? CurrencyKey { get; set; }
        #endregion

        #region For Quote Submissions
        /// <summary>Gets or sets the category i ds.</summary>
        /// <value>The category i ds.</value>
        int[]? CategoryIDs { get; set; }

        /// <summary>Gets or sets a list of names of the files.</summary>
        /// <value>A list of names of the files.</value>
        List<string>? FileNames { get; set; }
        #endregion

        #region Custom Attributes to apply to the object
        /// <summary>Gets or sets the serializable attributes.</summary>
        /// <value>The serializable attributes.</value>
        SerializableAttributesDictionary? SerializableAttributes { get; set; }
        #endregion
    }

    /// <summary>Interface for checkout with taxes.</summary>
    public interface ICheckoutWithTaxes
    {
        /// <summary>Gets or sets the identifier of the vat.</summary>
        /// <value>The identifier of the vat.</value>
        string? VatID { get; set; }

        /// <summary>Gets or sets the tax exemption number.</summary>
        /// <value>The tax exemption number.</value>
        string? TaxExemptionNumber { get; set; }
    }

    /// <summary>Interface for checkout with cart information.</summary>
    public interface ICheckoutWithCartInfo
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        int? CartID { get; set; }

        /// <summary>Gets or sets the name of the cart type.</summary>
        /// <value>The name of the cart type.</value>
        string? CartTypeName { get; set; }

        /// <summary>Gets or sets the identifier of the cart session.</summary>
        /// <value>The identifier of the cart session.</value>
        Guid? CartSessionID { get; set; }
    }

    /// <summary>Interface for checkout with user information.</summary>
    public interface ICheckoutWithUserInfo
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets a value indicating whether this ICheckoutWithUserInfo is new account.</summary>
        /// <value>True if this ICheckoutWithUserInfo is new account, false if not.</value>
        bool IsNewAccount { get; set; }

        /// <summary>Gets or sets the username.</summary>
        /// <value>The username.</value>
        string? UserName { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        string? Password { get; set; }

        /// <summary>Gets or sets the identifier of the external user.</summary>
        /// <value>The identifier of the external user.</value>
        string? ExternalUserID { get; set; }
    }

    /// <summary>Interface for checkout pay by payoneer.</summary>
    public interface ICheckoutPayByPayoneer
    {
        /// <summary>Gets or sets the identifier of the payoneer account.</summary>
        /// <value>The identifier of the payoneer account.</value>
        long? PayoneerAccountID { get; set; }

        /// <summary>Gets or sets the identifier of the payoneer customer.</summary>
        /// <value>The identifier of the payoneer customer.</value>
        long? PayoneerCustomerID { get; set; }
    }

    /// <summary>Interface for checkout pay by pay palette.</summary>
    public interface ICheckoutPayByPayPal
    {
        /// <summary>Gets or sets URL of the cancel.</summary>
        /// <value>The cancel URL.</value>
        string? CancelUrl { get; set; }

        /// <summary>Gets or sets URL of the return.</summary>
        /// <value>The return URL.</value>
        string? ReturnUrl { get; set; }

        /// <summary>Gets or sets the identifier of the payer.</summary>
        /// <value>The identifier of the payer.</value>
        string? PayerID { get; set; }

        /// <summary>Gets or sets the pay palette token.</summary>
        /// <value>The pay palette token.</value>
        string? PayPalToken { get; set; }
    }

    /// <summary>Interface for checkout pay by wallet entry.</summary>
    public interface ICheckoutPayByWalletEntry
    {
        /// <summary>Gets or sets the identifier of the wallet.</summary>
        /// <value>The identifier of the wallet.</value>
        int? WalletID { get; set; }

        /// <summary>Gets or sets the wallet token.</summary>
        /// <value>The wallet token.</value>
        string? WalletToken { get; set; }

        /// <summary>Gets or sets the wallet cvv.</summary>
        /// <value>The wallet cvv.</value>
        string? WalletCVV { get; set; }
    }

    /// <summary>Interface for checkout pay by credit card.</summary>
    public interface ICheckoutPayByCreditCard
    {
        /// <summary>Gets or sets the type of the card.</summary>
        /// <value>The type of the card.</value>
        string? CardType { get; set; }

        /// <summary>Gets or sets the name of the card reference.</summary>
        /// <value>The name of the card reference.</value>
        string? CardReferenceName { get; set; }

        /// <summary>Gets or sets the name of the card holder.</summary>
        /// <value>The name of the card holder.</value>
        string? CardHolderName { get; set; }

        /// <summary>Gets or sets the card number.</summary>
        /// <value>The card number.</value>
        string? CardNumber { get; set; }

        /// <summary>Gets or sets the card token.</summary>
        /// <value>The card token.</value>
        string? CardToken { get; set; }

        /// <summary>Gets or sets the cvv.</summary>
        /// <value>The cvv.</value>
        string? CVV { get; set; }

        /// <summary>Gets or sets the expiration month.</summary>
        /// <value>The expiration month.</value>
        int? ExpirationMonth { get; set; }

        /// <summary>Gets or sets the expiration year.</summary>
        /// <value>The expiration year.</value>
        int? ExpirationYear { get; set; }
    }

    /// <summary>Interface for checkout pay by e check.</summary>
    public interface ICheckoutPayByECheck
    {
        /// <summary>Gets or sets the name of the account reference.</summary>
        /// <value>The name of the account reference.</value>
        string? AccountReferenceName { get; set; }

        /// <summary>Gets or sets the name of the account holder.</summary>
        /// <value>The name of the account holder.</value>
        string? AccountHolderName { get; set; }

        /// <summary>Gets or sets the account number.</summary>
        /// <value>The account number.</value>
        string? AccountNumber { get; set; }

        /// <summary>Gets or sets the routing number.</summary>
        /// <value>The routing number.</value>
        string? RoutingNumber { get; set; }

        /// <summary>Gets or sets the name of the bank.</summary>
        /// <value>The name of the bank.</value>
        string? BankName { get; set; }

        /// <summary>Gets or sets the type of the account.</summary>
        /// <value>The type of the account.</value>
        string? AccountType { get; set; }
    }

    /// <summary>Interface for checkout pay by bill me later.</summary>
    public interface ICheckoutPayByBillMeLater
    {
        /// <summary>Gets or sets the customer purchase order number.</summary>
        /// <value>The customer purchase order number.</value>
        string? CustomerPurchaseOrderNumber { get; set; }
    }
}
