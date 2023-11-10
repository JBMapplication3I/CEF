// <copyright file="RepeatType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the RepeatType class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IRepeatType : ITypableBase
    {
        #region Repeat Type Properties
        /// <summary>Gets or sets the repeatable billing periods.</summary>
        /// <value>The repeatable billing periods.</value>
        int? RepeatableBillingPeriods { get; set; }

        /// <summary>Gets or sets the initial bonus billing periods.</summary>
        /// <value>The initial bonus billing periods.</value>
        int? InitialBonusBillingPeriods { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets a list of types of the subscription type repeats.</summary>
        /// <value>A list of types of the subscription type repeats.</value>
        ICollection<SubscriptionTypeRepeatType>? SubscriptionTypeRepeatTypes { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "RepeatType")]
    public class RepeatType : TypableBase, IRepeatType
    {
        private ICollection<SubscriptionTypeRepeatType>? subscriptionTypeRepeatTypes;

        public RepeatType()
        {
            subscriptionTypeRepeatTypes = new HashSet<SubscriptionTypeRepeatType>();
        }

        #region Repeat Type Properties
        /// <inheritdoc/>
        [DefaultValue(1)]
        public int? RepeatableBillingPeriods { get; set; } = 1;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int? InitialBonusBillingPeriods { get; set; } = 0;
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<SubscriptionTypeRepeatType>? SubscriptionTypeRepeatTypes { get => subscriptionTypeRepeatTypes; set => subscriptionTypeRepeatTypes = value; }
        #endregion
    }
}
