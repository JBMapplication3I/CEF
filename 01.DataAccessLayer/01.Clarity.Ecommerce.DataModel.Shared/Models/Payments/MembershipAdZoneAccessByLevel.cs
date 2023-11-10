// <copyright file="MembershipAdZoneAccessByLevel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership ad zone access by level class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IMembershipAdZoneAccessByLevel : IAmARelationshipTable<MembershipAdZoneAccess, MembershipLevel>
    {
        #region MembershipAdZoneAccessByLevel Properties
        /// <summary>Gets or sets the subscriber count threshold.</summary>
        /// <value>The subscriber count threshold.</value>
        int SubscriberCountThreshold { get; set; }

        /// <summary>Gets or sets the unique ad limit.</summary>
        /// <value>The unique ad limit.</value>
        int UniqueAdLimit { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "MembershipAdZoneAccessByLevel")]
    public class MembershipAdZoneAccessByLevel : Base, IMembershipAdZoneAccessByLevel
    {
        #region IAmARelationshipTable<MembershipAdZoneAccess, MembershipLevel>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual MembershipAdZoneAccess? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave))]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual MembershipLevel? Slave { get; set; }
        #endregion

        #region MembershipAdZoneAccessByLevel Properties
        /// <inheritdoc/>
        [DefaultValue(0)]
        public int SubscriberCountThreshold { get; set; } = 0;

        /// <inheritdoc/>
        [DefaultValue(0)]
        public int UniqueAdLimit { get; set; } = 0;
        #endregion
    }
}
