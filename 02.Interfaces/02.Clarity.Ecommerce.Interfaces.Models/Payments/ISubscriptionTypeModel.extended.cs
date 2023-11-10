// <copyright file="ISubscriptionTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISubscriptionTypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for subscription type model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public partial interface ISubscriptionTypeModel
    {
        #region Associated Objects
        /// <summary>Gets or sets a list of types of the product subscriptions.</summary>
        /// <value>A list of types of the product subscriptions.</value>
        List<IProductSubscriptionTypeModel>? ProductSubscriptionTypes { get; set; }

        /// <summary>Gets or sets a list of types of the subscription type repeats.</summary>
        /// <value>A list of types of the subscription type repeats.</value>
        List<ISubscriptionTypeRepeatTypeModel>? SubscriptionTypeRepeatTypes { get; set; }
        #endregion
    }
}
