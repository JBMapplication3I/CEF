// <copyright file="SubscriptionType.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface ISubscriptionType : ITypableBase
    {
        #region Associated Objects
        /// <summary>Gets or sets a list of types of the product subscriptions.</summary>
        /// <value>A list of types of the product subscriptions.</value>
        ICollection<ProductSubscriptionType>? ProductSubscriptionTypes { get; set; }

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

    [SqlSchema("Payments", "SubscriptionType")]
    public class SubscriptionType : TypableBase, ISubscriptionType
    {
        private ICollection<ProductSubscriptionType>? productSubscriptionTypes;
        private ICollection<SubscriptionTypeRepeatType>? subscriptionTypeRepeatTypes;

        public SubscriptionType()
        {
            productSubscriptionTypes = new HashSet<ProductSubscriptionType>();
            subscriptionTypeRepeatTypes = new HashSet<SubscriptionTypeRepeatType>();
        }

        #region Associated Objects
        /// <inheritdoc/>
        [DontMapInWithAssociateWorkflows, DefaultValue(null), JsonIgnore]
        public virtual ICollection<ProductSubscriptionType>? ProductSubscriptionTypes { get => productSubscriptionTypes; set => productSubscriptionTypes = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<SubscriptionTypeRepeatType>? SubscriptionTypeRepeatTypes { get => subscriptionTypeRepeatTypes; set => subscriptionTypeRepeatTypes = value; }
        #endregion
    }
}
