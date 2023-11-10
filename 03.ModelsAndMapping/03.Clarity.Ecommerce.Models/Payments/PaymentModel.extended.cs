// <copyright file="PaymentModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the payment.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IPaymentModel"/>
    /// <seealso cref="Interfaces.Providers.IProviderPayment"/>
    public partial class PaymentModel
    {
        #region IProviderPayment Properties
        /// <inheritdoc/>
        public string? RoutingNumber { get; set; }

        /// <inheritdoc/>
        public string? AccountNumber { get; set; }

        /// <inheritdoc/>
        public string? BankName { get; set; }

        /// <inheritdoc/>
        public decimal? Amount { get; set; }

        /// <inheritdoc/>
        public string? CardNumber { get; set; }

        /// <inheritdoc/>
        public string? CVV { get; set; }

        /// <inheritdoc/>
        public int? ExpirationMonth { get; set; }

        /// <inheritdoc/>
        public int? ExpirationYear { get; set; }

        /// <inheritdoc/>
        public string? Token { get; set; }

        /// <inheritdoc/>
        public string? CardType { get; set; }

        /// <inheritdoc/>
        public string? CardHolderName { get; set; }

        /// <inheritdoc/>
        public string? Zip { get; set; }

        /// <inheritdoc/>
        public string? InvoiceNumber { get; set; }

        /// <inheritdoc/>
        public string? PurchaseOrderNumber { get; set; }

        /// <inheritdoc/>
        public string? TransactionID { get; set; }

        /// <inheritdoc/>
        public string? Reference1 { get; set; }

        /// <inheritdoc/>
        public string? Reference2 { get; set; }

        /// <inheritdoc/>
        public string? Reference3 { get; set; }
        #endregion

        #region Payment Properties
        /// <inheritdoc/>
        public string? PaymentData { get; set; }

        /// <inheritdoc/>
        public bool? Authorized { get; set; }

        /// <inheritdoc/>
        public string? AuthCode { get; set; }

        /// <inheritdoc/>
        public bool? Received { get; set; }

        /// <inheritdoc/>
        public DateTime? StatusDate { get; set; }

        /// <inheritdoc/>
        public DateTime? AuthDate { get; set; }

        /// <inheritdoc/>
        public DateTime? ReceivedDate { get; set; }

        /// <inheritdoc/>
        public DateTime? PaymentDate { get; set; }

        /// <inheritdoc/>
        public string? ReferenceNo { get; set; }

        /// <inheritdoc/>
        public string? Response { get; set; }

        /// <inheritdoc/>
        public string? ExternalCustomerID { get; set; }

        /// <inheritdoc/>
        public string? ExternalPaymentID { get; set; }

        /// <inheritdoc/>
        public long? PayoneerAccountID { get; set; }

        /// <inheritdoc/>
        public long? PayoneerCustomerID { get; set; }

        /// <inheritdoc/>
        public int? CardTypeID { get; set; }

        /// <inheritdoc/>
        public string? CardMask { get; set; }

        /// <inheritdoc/>
        public string? Last4CardDigits { get; set; }

        /// <inheritdoc/>
        public string? TransactionNumber { get; set; }

        /// <inheritdoc/>
        public string? CheckNumber { get; set; }

        /// <inheritdoc/>
        public string? RoutingNumberLast4 { get; set; }

        /// <inheritdoc/>
        public string? AccountNumberLast4 { get; set; }

        /// <inheritdoc/>
        public int? WalletID { get; set; }

        /// <inheritdoc/>
        public string? ReferenceName { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? BillingContactID { get; set; }

        /// <inheritdoc/>
        public string? BillingContactKey { get; set; }

        /// <inheritdoc cref="IPaymentModel.BillingContact"/>
        public ContactModel? BillingContact { get; set; }

        /// <inheritdoc/>
        IContactModel? IPaymentModel.BillingContact { get => BillingContact; set => BillingContact = (ContactModel?)value; }

        /// <inheritdoc/>
        public int? PaymentMethodID { get; set; }

        /// <inheritdoc/>
        public string? PaymentMethodKey { get; set; }

        /// <inheritdoc cref="IPaymentModel.PaymentMethod"/>
        public PaymentMethodModel? PaymentMethod { get; set; }

        /// <inheritdoc/>
        IPaymentMethodModel? IPaymentModel.PaymentMethod { get => PaymentMethod; set => PaymentMethod = (PaymentMethodModel?)value; }

        /// <inheritdoc/>
        public string? PaymentMethodName { get; set; }

        /// <inheritdoc/>
        public int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency key.</summary>
        /// <value>The currency key.</value>
        public string? CurrencyKey { get; set; }

        /// <inheritdoc/>
        public string? CurrencyName { get; set; }

        /// <inheritdoc cref="IPaymentModel.Currency"/>
        public CurrencyModel? Currency { get; set; }

        /// <inheritdoc/>
        ICurrencyModel? IPaymentModel.Currency { get => Currency; set => Currency = (CurrencyModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IPaymentModel.SalesInvoicePayments"/>
        public List<SalesInvoicePaymentModel>? SalesInvoicePayments { get; set; }

        /// <inheritdoc/>
        List<ISalesInvoicePaymentModel>? IPaymentModel.SalesInvoicePayments { get => SalesInvoicePayments?.ToList<ISalesInvoicePaymentModel>(); set => SalesInvoicePayments = value?.Cast<SalesInvoicePaymentModel>().ToList(); }

        /// <inheritdoc cref="IPaymentModel.SalesOrderPayments"/>
        public List<SalesOrderPaymentModel>? SalesOrderPayments { get; set; }

        /// <inheritdoc/>
        List<ISalesOrderPaymentModel>? IPaymentModel.SalesOrderPayments { get => SalesOrderPayments?.ToList<ISalesOrderPaymentModel>(); set => SalesOrderPayments = value?.Cast<SalesOrderPaymentModel>().ToList(); }

        /// <inheritdoc cref="IPaymentModel.SubscriptionHistories"/>
        public List<SubscriptionHistoryModel>? SubscriptionHistories { get; set; }

        /// <inheritdoc/>
        List<ISubscriptionHistoryModel>? IPaymentModel.SubscriptionHistories { get => SubscriptionHistories?.ToList<ISubscriptionHistoryModel>(); set => SubscriptionHistories = value?.Cast<SubscriptionHistoryModel>().ToList(); }

        /// <inheritdoc cref="IPaymentModel.Subscriptions"/>
        public List<SubscriptionModel>? Subscriptions { get; set; }

        /// <inheritdoc/>
        List<ISubscriptionModel>? IPaymentModel.Subscriptions { get => Subscriptions?.ToList<ISubscriptionModel>(); set => Subscriptions = value?.Cast<SubscriptionModel>().ToList(); }
        #endregion
    }
}
