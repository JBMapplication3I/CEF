// <copyright file="ProductMembershipLevelModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product membership level model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class ProductMembershipLevelModel
    {
        #region Related Objects
        /// <inheritdoc/>
        public int MembershipRepeatTypeID { get; set; }

        /// <inheritdoc/>
        public string? MembershipRepeatTypeKey { get; set; }

        /// <inheritdoc cref="IProductMembershipLevelModel.MembershipRepeatType"/>
        public MembershipRepeatTypeModel? MembershipRepeatType { get; set; }

        /// <inheritdoc/>
        IMembershipRepeatTypeModel? IProductMembershipLevelModel.MembershipRepeatType { get => MembershipRepeatType; set => MembershipRepeatType = (MembershipRepeatTypeModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IProductMembershipLevelModel.Subscriptions"/>
        public List<SubscriptionModel>? Subscriptions { get; set; }

        /// <inheritdoc/>
        List<ISubscriptionModel>? IProductMembershipLevelModel.Subscriptions { get => Subscriptions?.ToList<ISubscriptionModel>(); set => Subscriptions = value?.Cast<SubscriptionModel>().ToList(); }
        #endregion
    }
}
