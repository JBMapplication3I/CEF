// <copyright file="RepeatTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the repeat type model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class RepeatTypeModel
    {
        #region RepeatType Properties
        /// <inheritdoc/>
        public int? InitialBonusBillingPeriods { get; set; }

        /// <inheritdoc/>
        public int? RepeatableBillingPeriods { get; set; }
        #endregion

        /// <inheritdoc cref="IRepeatTypeModel.SubscriptionTypeRepeatTypes"/>
        public List<SubscriptionTypeRepeatTypeModel>? SubscriptionTypeRepeatTypes { get; set; }

        /// <inheritdoc/>
        List<ISubscriptionTypeRepeatTypeModel>? IRepeatTypeModel.SubscriptionTypeRepeatTypes { get => SubscriptionTypeRepeatTypes?.ToList<ISubscriptionTypeRepeatTypeModel>(); set => SubscriptionTypeRepeatTypes = value?.Cast<SubscriptionTypeRepeatTypeModel>().ToList(); }
    }
}
