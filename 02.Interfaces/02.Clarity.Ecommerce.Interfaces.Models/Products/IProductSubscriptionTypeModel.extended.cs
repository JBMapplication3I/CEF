// <copyright file="IProductSubscriptionTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductSubscriptionTypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for product subscription type model.</summary>
    /// <seealso cref="IBaseModel"/>
    public partial interface IProductSubscriptionTypeModel
    {
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the identifier of the subscription type repeat type.</summary>
        /// <value>The identifier of the subscription type repeat type.</value>
        int SubscriptionTypeRepeatTypeID { get; set; }

        /// <summary>Gets or sets the subscription type repeat type key.</summary>
        /// <value>The subscription type repeat type key.</value>
        string? SubscriptionTypeRepeatTypeKey { get; set; }

        /// <summary>Gets or sets the type of the subscription type repeat.</summary>
        /// <value>The type of the subscription type repeat.</value>
        ISubscriptionTypeRepeatTypeModel? SubscriptionTypeRepeatType { get; set; }

        /// <summary>Gets or sets the subscriptions.</summary>
        /// <value>The subscriptions.</value>
        List<ISubscriptionModel>? Subscriptions { get; set; }
    }
}
