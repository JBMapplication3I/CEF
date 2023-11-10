// <copyright file="ISubscriptionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISubscriptionModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>Interface for subscription model.</summary>
    public partial interface ISubscriptionModel
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

        /// <summary>Gets or sets the product membership level key.</summary>
        /// <value>The product membership level key.</value>
        string? ProductMembershipLevelKey { get; set; }

        /// <summary>Gets or sets the product membership level.</summary>
        /// <value>The product membership level.</value>
        IProductMembershipLevelModel? ProductMembershipLevel { get; set; }

        /// <summary>Gets or sets the identifier of the repeat type.</summary>
        /// <value>The identifier of the repeat type.</value>
        int RepeatTypeID { get; set; }

        /// <summary>Gets or sets the repeat type key.</summary>
        /// <value>The repeat type key.</value>
        string? RepeatTypeKey { get; set; }

        /// <summary>Gets or sets the name of the repeat type.</summary>
        /// <value>The name of the repeat type.</value>
        string? RepeatTypeName { get; set; }

        /// <summary>Gets or sets the type of the repeat.</summary>
        /// <value>The type of the repeat.</value>
        IRepeatTypeModel? RepeatType { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        int? AccountID { get; set; }

        /// <summary>Gets or sets the account key.</summary>
        /// <value>The account key.</value>
        string? AccountKey { get; set; }

        /// <summary>Gets or sets the name of the account.</summary>
        /// <value>The name of the account.</value>
        string? AccountName { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        IAccountModel? Account { get; set; }

        /// <summary>Gets or sets the identifier of the sales invoice.</summary>
        /// <value>The identifier of the sales invoice.</value>
        int? SalesGroupID { get; set; }

        /// <summary>Gets or sets the sales invoice key.</summary>
        /// <value>The sales invoice key.</value>
        string? SalesGroupKey { get; set; }

        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }

        /// <summary>Gets or sets the sales order key.</summary>
        /// <value>The sales order key.</value>
        string? SalesOrderKey { get; set; }

        /// <summary>Gets or sets the identifier of the product subscription type.</summary>
        /// <value>The identifier of the sales group.</value>
        int? ProductSubscriptionTypeID { get; set; }

        /// <summary>Gets or sets the product subscription type key.</summary>
        /// <value>The sales invoice key.</value>
        string? ProductSubscriptionTypeKey { get; set; }

        /// <summary>Gets or sets the product subscription type.</summary>
        /// <value>The sales group.</value>
        IProductSubscriptionTypeModel? ProductSubscriptionType { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the subscription histories.</summary>
        /// <value>The subscription histories.</value>
        List<ISubscriptionHistoryModel>? SubscriptionHistories { get; set; }

        /// <summary>Gets or sets the store subscriptions.</summary>
        /// <value>The store subscriptions.</value>
        List<IStoreSubscriptionModel>? StoreSubscriptions { get; set; }
        #endregion

        #region Convenience Properties
        /// <summary>Gets or sets the member number.</summary>
        /// <value>The member number.</value>
        string? MemberNumber { get; set; }
        #endregion
    }
}
