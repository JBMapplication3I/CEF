// <copyright file="IPaymentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPaymentModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;
    using Providers;

    /// <summary>Interface for payment model.</summary>
    /// <seealso cref="IBaseModel"/>
    /// <seealso cref="IProviderPayment"/>
    public partial interface IPaymentModel : IProviderPayment
    {
        ////// Inherited from IPayment
        //// decimal? Amount { get; set; }
        //// string CVV { get; set; }
        //// string ExpirationMonth { get; set; }
        //// string ExpirationYear { get; set; }
        //// string CardNumber { get; set; }
        //// string Token { get; set; }
        //// string Zip { get; set; }
        //// string InvoiceNumber { get; set; }

        #region Payment Properties
        /// <summary>Gets or sets information describing the payment.</summary>
        /// <value>Information describing the payment.</value>
        string? PaymentData { get; set; }

        /// <summary>Gets or sets the authorized.</summary>
        /// <value>The authorized.</value>
        bool? Authorized { get; set; }

        /// <summary>Gets or sets the authentication code.</summary>
        /// <value>The authentication code.</value>
        string? AuthCode { get; set; }

        /// <summary>Gets or sets the received.</summary>
        /// <value>The received.</value>
        bool? Received { get; set; }

        /// <summary>Gets or sets the status date.</summary>
        /// <value>The status date.</value>
        DateTime? StatusDate { get; set; }

        /// <summary>Gets or sets the authentication date.</summary>
        /// <value>The authentication date.</value>
        DateTime? AuthDate { get; set; }

        /// <summary>Gets or sets the received date.</summary>
        /// <value>The received date.</value>
        DateTime? ReceivedDate { get; set; }

        /// <summary>Gets or sets the payment date.</summary>
        /// <value>The payment date.</value>
        DateTime? PaymentDate { get; set; }

        /// <summary>Gets or sets the reference no.</summary>
        /// <value>The reference no.</value>
        string? ReferenceNo { get; set; }

        /// <summary>Gets or sets the response.</summary>
        /// <value>The response.</value>
        string? Response { get; set; }

        /// <summary>Gets or sets the identifier of the external customer.</summary>
        /// <value>The identifier of the external customer.</value>
        string? ExternalCustomerID { get; set; }

        /// <summary>Gets or sets the identifier of the external payment.</summary>
        /// <value>The identifier of the external payment.</value>
        string? ExternalPaymentID { get; set; }

        /// <summary>Gets or sets the identifier of the card type.</summary>
        /// <value>The identifier of the card type.</value>
        int? CardTypeID { get; set; }

        /// <summary>Gets or sets the card mask.</summary>
        /// <value>The card mask.</value>
        string? CardMask { get; set; }

        /// <summary>Gets or sets the last 4 card digits.</summary>
        /// <value>The last 4 card digits.</value>
        string? Last4CardDigits { get; set; }

        /// <summary>Gets or sets the identifier of the payoneer account.</summary>
        /// <value>The identifier of the payoneer account.</value>
        long? PayoneerAccountID { get; set; }

        /// <summary>Gets or sets the identifier of the payoneer customer.</summary>
        /// <value>The identifier of the payoneer customer.</value>
        long? PayoneerCustomerID { get; set; }

        /// <summary>Gets or sets the transaction number.</summary>
        /// <value>The transaction number.</value>
        string? TransactionNumber { get; set; }

        /// <summary>Gets or sets the check number.</summary>
        /// <value>The check number.</value>
        string? CheckNumber { get; set; }

        /// <summary>Gets or sets the routing number last 4.</summary>
        /// <value>The routing number last 4.</value>
        string? RoutingNumberLast4 { get; set; }

        /// <summary>Gets or sets the account number last 4.</summary>
        /// <value>The account number last 4.</value>
        string? AccountNumberLast4 { get; set; }

        /// <summary>Gets or sets the identifier of the wallet.</summary>
        /// <value>The identifier of the wallet.</value>
        int? WalletID { get; set; }

        /// <summary>Gets or sets the name of the reference.</summary>
        /// <value>The name of the reference.</value>
        string? ReferenceName { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the billing contact.</summary>
        /// <value>The identifier of the billing contact.</value>
        int? BillingContactID { get; set; }

        /// <summary>Gets or sets the billing contact key.</summary>
        /// <value>The billing contact key.</value>
        string? BillingContactKey { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        IContactModel? BillingContact { get; set; }

        /// <summary>Gets or sets the identifier of the payment method.</summary>
        /// <value>The identifier of the payment method.</value>
        int? PaymentMethodID { get; set; }

        /// <summary>Gets or sets the payment method key.</summary>
        /// <value>The payment method key.</value>
        string? PaymentMethodKey { get; set; }

        /// <summary>Gets or sets the name of the payment method.</summary>
        /// <value>The name of the payment method.</value>
        string? PaymentMethodName { get; set; }

        /// <summary>Gets or sets the payment method.</summary>
        /// <value>The payment method.</value>
        IPaymentMethodModel? PaymentMethod { get; set; }

        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the name of the currency.</summary>
        /// <value>The name of the currency.</value>
        string? CurrencyName { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        ICurrencyModel? Currency { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the sales order payments.</summary>
        /// <value>The sales order payments.</value>
        List<ISalesOrderPaymentModel>? SalesOrderPayments { get; set; }

        /// <summary>Gets or sets the sales invoice payments.</summary>
        /// <value>The sales invoice payments.</value>
        List<ISalesInvoicePaymentModel>? SalesInvoicePayments { get; set; }

        /// <summary>Gets or sets the subscription histories.</summary>
        /// <value>The subscription histories.</value>
        List<ISubscriptionHistoryModel>? SubscriptionHistories { get; set; }

        /// <summary>Gets or sets the subscriptions.</summary>
        /// <value>The subscriptions.</value>
        List<ISubscriptionModel>? Subscriptions { get; set; }
        #endregion
    }
}
