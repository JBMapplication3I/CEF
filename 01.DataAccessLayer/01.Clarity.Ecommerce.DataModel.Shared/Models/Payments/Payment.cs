// <copyright file="Payment.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payment class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    public interface IPayment
        : IHaveATypeBase<PaymentType>,
            IHaveAStatusBase<PaymentStatus>,
            IAmFilterableByNullableStore,
            IAmFilterableByNullableBrand
    {
        #region Payment Properties
        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        decimal? Amount { get; set; }

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

        /// <summary>Gets or sets the cvv.</summary>
        /// <value>The cvv.</value>
        string? CVV { get; set; }

        /// <summary>Gets or sets the last 4 card digits.</summary>
        /// <value>The last 4 card digits.</value>
        string? Last4CardDigits { get; set; }

        /// <summary>Gets or sets the expiration month.</summary>
        /// <value>The expiration month.</value>
        int? ExpirationMonth { get; set; }

        /// <summary>Gets or sets the expiration year.</summary>
        /// <value>The expiration year.</value>
        int? ExpirationYear { get; set; }

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

        /// <summary>Gets or sets the name of the bank.</summary>
        /// <value>The name of the bank.</value>
        string? BankName { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the billing contact.</summary>
        /// <value>The identifier of the billing contact.</value>
        int? BillingContactID { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        Contact? BillingContact { get; set; }

        /// <summary>Gets or sets the identifier of the payment method.</summary>
        /// <value>The identifier of the payment method.</value>
        int? PaymentMethodID { get; set; }

        /// <summary>Gets or sets the payment method.</summary>
        /// <value>The payment method.</value>
        PaymentMethod? PaymentMethod { get; set; }

        /// <summary>Gets or sets the identifier of the currency.</summary>
        /// <value>The identifier of the currency.</value>
        int? CurrencyID { get; set; }

        /// <summary>Gets or sets the currency.</summary>
        /// <value>The currency.</value>
        Currency? Currency { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "Payment")]
    public class Payment : Base, IPayment
    {
        #region IAmFilterableByNullableStore Properties
        // Related Objects
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Store))]
        public int? StoreID { get; set; }

        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Store? Store { get; set; }
        #endregion

        #region IAmFilterableByNullableBrand Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Brand)), DefaultValue(null)]
        public int? BrandID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Brand? Brand { get; set; }
        #endregion

        #region Payment Properties
        /// <inheritdoc/>
        [DecimalPrecision(18, 5), DefaultValue(null)]
        public decimal? Amount { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? AuthCode { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? ReferenceNo { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? TransactionNumber { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? StatusDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? Authorized { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? AuthDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public bool? Received { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? ReceivedDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? Response { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? ExternalCustomerID { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? ExternalPaymentID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public string? PaymentData { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? CardTypeID { get; set; }

        /// <inheritdoc/>
        [StringLength(50), DefaultValue("X")]
        public string? CardMask { get; set; } = "X";

        /// <inheritdoc/>
        [StringLength(50), DefaultValue(null)]
        public string? CVV { get; set; }

        /// <inheritdoc/>
        [StringLength(4), DefaultValue(null)]
        public string? Last4CardDigits { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ExpirationMonth { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public int? ExpirationYear { get; set; }

        /// <inheritdoc/>
        [StringLength(8), DefaultValue(null)]
        public string? CheckNumber { get; set; }

        /// <inheritdoc/>
        [StringLength(4), DefaultValue(null)]
        public string? RoutingNumberLast4 { get; set; }

        /// <inheritdoc/>
        [StringLength(4), DefaultValue(null)]
        public string? AccountNumberLast4 { get; set; }

        /// <inheritdoc/>
        [StringLength(100), DefaultValue(null)]
        public string? BankName { get; set; }
        #endregion

        #region IHaveAType Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual PaymentType? Type { get; set; }
        #endregion

        #region IHaveAStatus Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual PaymentStatus? Status { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(BillingContact)), DefaultValue(null)]
        public int? BillingContactID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Contact? BillingContact { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(PaymentMethod)), DefaultValue(null)]
        public int? PaymentMethodID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual PaymentMethod? PaymentMethod { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Currency)), DefaultValue(null)]
        public int? CurrencyID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Currency? Currency { get; set; }
        #endregion
    }
}
