// <copyright file="SubscriptionTypeRepeatType.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription type repeat type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface ISubscriptionTypeRepeatType : IAmARelationshipTable<SubscriptionType, RepeatType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "SubscriptionTypeRepeatType")]
    public class SubscriptionTypeRepeatType : Base, ISubscriptionTypeRepeatType
    {
        #region IAmARelationshipTable<SubscriptionType, RepeatType>
        /// <inheritdoc/>
        [/*InverseProperty(nameof(ID)), ForeignKey(nameof(Master)),*/ DefaultValue(0)] // Handled in model builder
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual SubscriptionType? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual RepeatType? Slave { get; set; }
        #endregion
    }
}
