// <copyright file="MembershipRepeatType.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership repeat type class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IMembershipRepeatType : IAmARelationshipTable<Membership, RepeatType>
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "MembershipRepeatType")]
    public class MembershipRepeatType : Base, IMembershipRepeatType
    {
        #region IAmARelationshipTable<Membership, RepeatType>
        /// <inheritdoc/>
        [/*InverseProperty(nameof(ID)), ForeignKey(nameof(Master)),*/ DefaultValue(0)] // Relationship handled in model builder
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Membership? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave)), DefaultValue(0)]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual RepeatType? Slave { get; set; }
        #endregion
    }
}
