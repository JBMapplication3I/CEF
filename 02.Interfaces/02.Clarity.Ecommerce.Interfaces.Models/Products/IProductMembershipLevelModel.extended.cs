// <copyright file="IProductMembershipLevelModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductMembershipLevelModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IProductMembershipLevelModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the membership repeat type.</summary>
        /// <value>The identifier of the membership repeat type.</value>
        int MembershipRepeatTypeID { get; set; }

        /// <summary>Gets or sets the membership repeat type key.</summary>
        /// <value>The membership repeat type key.</value>
        string? MembershipRepeatTypeKey { get; set; }

        /// <summary>Gets or sets the type of the membership repeat.</summary>
        /// <value>The type of the membership repeat.</value>
        IMembershipRepeatTypeModel? MembershipRepeatType { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the subscriptions.</summary>
        /// <value>The subscriptions.</value>
        List<ISubscriptionModel>? Subscriptions { get; set; }
        #endregion
    }
}
