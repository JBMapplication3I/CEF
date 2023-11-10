// <copyright file="SubscriptionModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class SubscriptionModel
    {
        #region Subscription Properties
        /// <inheritdoc/>
        public DateTime MemberSince { get; set; }

        /// <inheritdoc/>
        public DateTime StartsOn { get; set; }

        /// <inheritdoc/>
        public DateTime? EndsOn { get; set; }

        /// <inheritdoc/>
        public int BillingPeriodsTotal { get; set; }

        /// <inheritdoc/>
        public int BillingPeriodsPaid { get; set; }

        /// <inheritdoc/>
        public DateTime? LastPaidDate { get; set; }

        /// <inheritdoc/>
        public decimal Fee { get; set; }

        /// <inheritdoc/>
        public string? Memo { get; set; }

        /// <inheritdoc/>
        public bool AutoRenew { get; set; }

        /// <inheritdoc/>
        public bool CanUpgrade { get; set; }

        /// <inheritdoc/>
        public decimal? CreditUponUpgrade { get; set; }

        /// <inheritdoc/>
        public bool IsAutoRefill { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int? ProductMembershipLevelID { get; set; }

        /// <inheritdoc/>
        public string? ProductMembershipLevelKey { get; set; }

        /// <inheritdoc cref="ISubscriptionModel.ProductMembershipLevel"/>
        public ProductMembershipLevelModel? ProductMembershipLevel { get; set; }

        /// <inheritdoc/>
        IProductMembershipLevelModel? ISubscriptionModel.ProductMembershipLevel { get => ProductMembershipLevel; set => ProductMembershipLevel = (ProductMembershipLevelModel?)value; }

        /// <inheritdoc/>
        public int RepeatTypeID { get; set; }

        /// <inheritdoc/>
        public string? RepeatTypeKey { get; set; }

        /// <inheritdoc/>
        public string? RepeatTypeName { get; set; }

        /// <inheritdoc cref="ISubscriptionModel.RepeatType"/>
        public RepeatTypeModel? RepeatType { get; set; }

        /// <inheritdoc/>
        IRepeatTypeModel? ISubscriptionModel.RepeatType { get => RepeatType; set => RepeatType = (RepeatTypeModel?)value; }

        /// <inheritdoc/>
        public int? UserID { get; set; }

        /// <inheritdoc/>
        public string? UserKey { get; set; }

        /// <inheritdoc cref="ISubscriptionModel.User"/>
        public UserModel? User { get; set; }

        /// <inheritdoc/>
        IUserModel? ISubscriptionModel.User { get => User; set => User = (UserModel?)value; }

        /// <inheritdoc/>
        public int? AccountID { get; set; }

        /// <inheritdoc/>
        public string? AccountKey { get; set; }

        /// <inheritdoc/>
        public string? AccountName { get; set; }

        /// <inheritdoc cref="ISubscriptionModel.Account"/>
        public AccountModel? Account { get; set; }

        /// <inheritdoc/>
        IAccountModel? ISubscriptionModel.Account { get => Account; set => Account = (AccountModel?)value; }

        /// <inheritdoc/>
        public int? SalesGroupID { get; set; }

        /// <inheritdoc/>
        public string? SalesGroupKey { get; set; }

        /// <inheritdoc/>
        public int? SalesOrderID { get; set; }

        /// <inheritdoc/>
        public string? SalesOrderKey { get; set; }

        /// <inheritdoc/>
        public int? ProductSubscriptionTypeID { get; set; }

        /// <inheritdoc/>
        public string? ProductSubscriptionTypeKey { get; set; }

        /// <inheritdoc cref="ISubscriptionModel.ProductSubscriptionType"/>
        public ProductSubscriptionTypeModel? ProductSubscriptionType { get; set; }

        /// <inheritdoc/>
        IProductSubscriptionTypeModel? ISubscriptionModel.ProductSubscriptionType { get => ProductSubscriptionType; set => ProductSubscriptionType = (ProductSubscriptionTypeModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="ISubscriptionModel.SubscriptionHistories" />
        public List<SubscriptionHistoryModel>? SubscriptionHistories { get; set; }

        /// <inheritdoc/>
        List<ISubscriptionHistoryModel>? ISubscriptionModel.SubscriptionHistories { get => SubscriptionHistories?.ToList<ISubscriptionHistoryModel>(); set => SubscriptionHistories = value?.Cast<SubscriptionHistoryModel>().ToList(); }

        /// <inheritdoc cref="ISubscriptionModel.StoreSubscriptions"/>
        public List<StoreSubscriptionModel>? StoreSubscriptions { get; set; }

        /// <inheritdoc/>
        List<IStoreSubscriptionModel>? ISubscriptionModel.StoreSubscriptions { get => StoreSubscriptions?.Cast<IStoreSubscriptionModel>().ToList(); set => StoreSubscriptions = value?.Cast<StoreSubscriptionModel>().ToList(); }
        #endregion

        #region Convenience Properties
        /// <inheritdoc/>
        public string? MemberNumber { get; set; }
        #endregion
    }
}
