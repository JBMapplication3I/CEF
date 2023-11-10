// <copyright file="MembershipLevel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the MembershipLevel class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IMembershipLevel : ITypableBase
    {
        #region MembershipLevel Properties
        /// <summary>Gets or sets the roles applied.</summary>
        /// <value>The roles applied.</value>
        string? RolesApplied { get; set; }
        #endregion

        #region Related Obejcts
        /// <summary>Gets or sets the identifier of the membership.</summary>
        /// <value>The identifier of the membership.</value>
        int MembershipID { get; set; }

        /// <summary>Gets or sets the membership.</summary>
        /// <value>The membership.</value>
        Membership? Membership { get; set; }
        #endregion

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
    using System.ComponentModel.DataAnnotations;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "MembershipLevel")]
    public class MembershipLevel : TypableBase, IMembershipLevel
    {
        private ICollection<MembershipAdZoneAccessByLevel>? membershipAdZoneAccessByLevels;

        public MembershipLevel()
        {
            membershipAdZoneAccessByLevels = new HashSet<MembershipAdZoneAccessByLevel>();
        }

        #region MembershipLevel Properties
        /// <inheritdoc/>
        [StringIsUnicode(false), StringLength(512), DefaultValue(null)]
        public string? RolesApplied { get; set; } = null;
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        [/*InverseProperty(nameof(ID)), ForeignKey(nameof(Membership)),*/ DefaultValue(0)] // Relationship handled in model builder
        public int MembershipID { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual Membership? Membership { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<MembershipAdZoneAccessByLevel>? MembershipAdZoneAccessByLevels { get => membershipAdZoneAccessByLevels; set => membershipAdZoneAccessByLevels = value; }
        #endregion
    }
}
