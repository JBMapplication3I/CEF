// <copyright file="Subscription.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISubscription
        : INameableBase, IHaveAStatusBase<SubscriptionStatus>, IHaveATypeBase<SubscriptionType>
    {
        #region Subscription Properties
        /// <summary>Gets or sets the Date/Time of the member since.</summary>
        /// <value>The member since.</value>
        DateTime MemberSince { get; set; }

        /// <summary>Gets or sets the Date/Time of the starts on.</summary>
        /// <value>The starts on.</value>
        DateTime StartsOn { get; set; }

        /// <summary>Gets or sets the Date/Time of the ends on.</summary>
        /// <value>The ends on.</value>
        DateTime? EndsOn { get; set; }

        /// <summary>Gets or sets the billing periods total.</summary>
        /// <value>The billing periods total.</value>
        int BillingPeriodsTotal { get; set; }

        /// <summary>Gets or sets the billing periods paid.</summary>
        /// <value>The billing periods paid.</value>
        int BillingPeriodsPaid { get; set; }

        /// <summary>Gets or sets the last paid date.</summary>
        /// <value>The last paid date.</value>
        DateTime? LastPaidDate { get; set; }

        /// <summary>Gets or sets the fee.</summary>
        /// <value>The fee.</value>
        decimal Fee { get; set; }

        /// <summary>Gets or sets the memo.</summary>
        /// <value>The memo.</value>
        string? Memo { get; set; }

        /// <summary>Gets or sets a value indicating whether the automatic renew.</summary>
        /// <value>True if automatic renew, false if not.</value>
        bool AutoRenew { get; set; }

        /// <summary>Gets or sets a value indicating whether we can upgrade.</summary>
        /// <value>True if we can upgrade, false if not.</value>
        bool CanUpgrade { get; set; }

        /// <summary>Gets or sets the credit upon upgrade.</summary>
        /// <value>The credit upon upgrade.</value>
        decimal? CreditUponUpgrade { get; set; }

        /// <summary>Gets or sets a value indicating whether the subscription will be automatically reilled.</summary>
        /// <value>True if automatically refilled, false if not.</value>
        bool IsAutoRefill { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the product membership level.</summary>
        /// <value>The identifier of the product membership level.</value>
        int? ProductMembershipLevelID { get; set; }

        /// <summary>Gets or sets the product membership level.</summary>
        /// <value>The product membership level.</value>
        ProductMembershipLevel? ProductMembershipLevel { get; set; }

        /// <summary>Gets or sets the identifier of the repeat type.</summary>
        /// <value>The identifier of the repeat type.</value>
        int RepeatTypeID { get; set; }

        /// <summary>Gets or sets the type of the repeat.</summary>
        /// <value>The type of the repeat.</value>
        RepeatType? RepeatType { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice.</summary>
        /// <value>The identifier of the sales invoice.</value>
        int? SalesInvoiceID { get; set; }

        /// <summary>Gets or sets the sales invoice.</summary>
        /// <value>The sales invoice.</value>
        SalesInvoice? SalesInvoice { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        Account? Account { get; set; }

        /// <summary>Gets or sets the identifier of the sales group.</summary>
        /// <value>The identifier of the sales group.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the sales group.</summary>
        /// <value>The sales group.</value>
        SalesGroup? SalesGroup { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the sales order.</summary>
        /// <value>The sales order.</value>
        SalesOrder? SalesOrder { get; set; }

        /// <summary>Gets or sets the identifier of the product subscription type.</summary>
        /// <value>The identifier of the product subscription type ID.</value>
        int? ProductSubscriptionTypeID { get; set; }

        /// <summary>Gets or sets the product subscription type.</summary>
        /// <value>The product subscription type.</value>
        ProductSubscriptionType? ProductSubscriptionType { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the subscription histories.</summary>
        /// <value>The subscription histories.</value>
        ICollection<SubscriptionHistory>? SubscriptionHistories { get; set; }

        /// <summary>Gets or sets the store subscriptions.</summary>
        /// <value>The store subscriptions.</value>
        ICollection<StoreSubscription>? StoreSubscriptions { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "Subscription")]
    public class Subscription : NameableBase, ISubscription
    {
        private ICollection<SubscriptionHistory>? subscriptionHistories;
        private ICollection<StoreSubscription>? storeSubscriptions;

        public Subscription()
        {
            subscriptionHistories = new HashSet<SubscriptionHistory>();
            storeSubscriptions = new HashSet<StoreSubscription>();
        }

        #region IHaveATypeBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Type)), DefaultValue(0)]
        public int TypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual SubscriptionType? Type { get; set; }
        #endregion

        #region IHaveAStatusBase Properties
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Status)), DefaultValue(0)]
        public int StatusID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual SubscriptionStatus? Status { get; set; }
        #endregion

        #region Subscription
        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime MemberSince { get; set; }

        /// <inheritdoc/>
        ////[Column(TypeName = "datetime2"), DateTimePrecision(7)]
        public DateTime StartsOn { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? EndsOn { get; set; }

        /// <inheritdoc/>
        [/*Column(TypeName = "datetime2"), DateTimePrecision(7),*/ DefaultValue(null)]
        public DateTime? LastPaidDate { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int BillingPeriodsTotal { get; set; }

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int BillingPeriodsPaid { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(0)]
        public decimal Fee { get; set; }

        /// <inheritdoc/>
        [DecimalPrecision(18, 4), DefaultValue(null)]
        public decimal? CreditUponUpgrade { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool AutoRenew { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool CanUpgrade { get; set; }

        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsAutoRefill { get; set; }

        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(128), DefaultValue(null)]
        public string? Memo { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ProductMembershipLevel)), DefaultValue(null)]
        public int? ProductMembershipLevelID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ProductMembershipLevel? ProductMembershipLevel { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(RepeatType)), DefaultValue(0)]
        public int RepeatTypeID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual RepeatType? RepeatType { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesInvoice)), DefaultValue(null)]
        public int? SalesInvoiceID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual SalesInvoice? SalesInvoice { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(User)), DefaultValue(null)]
        public int? UserID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual User? User { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Account)), DefaultValue(null)]
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual Account? Account { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesGroup)), DefaultValue(null)]
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual SalesGroup? SalesGroup { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(SalesOrder)), DefaultValue(null)]
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual SalesOrder? SalesOrder { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(ProductSubscriptionType)), DefaultValue(null)]
        public int? ProductSubscriptionTypeID { get; set; }

        /// <inheritdoc/>
        [AllowMapInWithRelateWorkflowsButDontAutoGenerate, DefaultValue(null), JsonIgnore]
        public virtual ProductSubscriptionType? ProductSubscriptionType { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<SubscriptionHistory>? SubscriptionHistories { get => subscriptionHistories; set => subscriptionHistories = value; }

        #region Don't map these out
        /// <inheritdoc/>
        [DontMapOutEver, DontMapInEver, DefaultValue(null), JsonIgnore]
        public virtual ICollection<StoreSubscription>? StoreSubscriptions { get => storeSubscriptions; set => storeSubscriptions = value; }
        #endregion
        #endregion
    }
}
