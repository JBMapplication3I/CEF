// <copyright file="MembershipAdZoneAccess.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership ad zone access class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IMembershipAdZoneAccess : IAmARelationshipTable<Membership, AdZoneAccess>
    {
        #region Associated Objects
        /// <summary>Gets or sets the membership ad zone access by levels.</summary>
        /// <value>The membership ad zone access by levels.</value>
        ICollection<MembershipAdZoneAccessByLevel>? MembershipAdZoneAccessByLevels { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "MembershipAdZoneAccess")]
    public class MembershipAdZoneAccess : Base, IMembershipAdZoneAccess
    {
        private ICollection<MembershipAdZoneAccessByLevel>? membershipAdZoneAccessByLevels;

        public MembershipAdZoneAccess()
        {
            membershipAdZoneAccessByLevels = new HashSet<MembershipAdZoneAccessByLevel>();
        }

        #region IAmARelationshipTable<Membership, AdZoneAccess>
        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Master))]
        public int MasterID { get; set; }

        /// <inheritdoc/>
        [DontMapInWithRelateWorkflows, DontMapOutEver, DefaultValue(null), JsonIgnore]
        public virtual Membership? Master { get; set; }

        /// <inheritdoc/>
        [InverseProperty(nameof(ID)), ForeignKey(nameof(Slave))]
        public int SlaveID { get; set; }

        /// <inheritdoc/>
        [ForceMapOutWithListing, DefaultValue(null), JsonIgnore]
        public virtual AdZoneAccess? Slave { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<MembershipAdZoneAccessByLevel>? MembershipAdZoneAccessByLevels { get => membershipAdZoneAccessByLevels; set => membershipAdZoneAccessByLevels = value; }
        #endregion
    }
}
