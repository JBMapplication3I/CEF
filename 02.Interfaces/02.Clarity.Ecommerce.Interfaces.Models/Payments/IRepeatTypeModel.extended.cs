// <copyright file="IRepeatTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRepeatTypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IRepeatTypeModel
    {
        #region RepeatType Properties
        /// <summary>Gets or sets the initial bonus billing periods.</summary>
        /// <value>The initial bonus billing periods.</value>
        int? InitialBonusBillingPeriods { get; set; }

        /// <summary>Gets or sets the repeatable billing periods.</summary>
        /// <value>The repeatable billing periods.</value>
        int? RepeatableBillingPeriods { get; set; }
        #endregion

        /// <summary>Gets or sets a list of types of the subscription type repeats.</summary>
        /// <value>A list of types of the subscription type repeats.</value>
        List<ISubscriptionTypeRepeatTypeModel>? SubscriptionTypeRepeatTypes { get; set; }
    }
}
