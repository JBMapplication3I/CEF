// <copyright file="Membership.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Membership class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    public interface IMembership : ITypableBase
    {
        #region Membership Properties
        /// <summary>Gets or sets a value indicating whether this IMembership is contractual.</summary>
        /// <value>True if this IMembership is contractual, false if not.</value>
        bool IsContractual { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the membership levels.</summary>
        /// <value>The membership levels.</value>
        ICollection<MembershipLevel>? MembershipLevels { get; set; }

        /// <summary>Gets or sets a list of types of the membership repeats.</summary>
        /// <value>A list of types of the membership repeats.</value>
        ICollection<MembershipRepeatType>? MembershipRepeatTypes { get; set; }
        #endregion
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interfaces.DataModel;
    using Newtonsoft.Json;

    [SqlSchema("Payments", "Membership")]
    public class Membership : TypableBase, IMembership
    {
        private ICollection<MembershipLevel>? membershipLevels;
        private ICollection<MembershipRepeatType>? membershipRepeatTypes;

        public Membership()
        {
            membershipLevels = new HashSet<MembershipLevel>();
            membershipRepeatTypes = new HashSet<MembershipRepeatType>();
        }

        #region Membership Properties
        /// <inheritdoc/>
        [DefaultValue(false)]
        public bool IsContractual { get; set; } = false;
        #endregion

        #region Associated Objects
        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<MembershipLevel>? MembershipLevels { get => membershipLevels; set => membershipLevels = value; }

        /// <inheritdoc/>
        [DefaultValue(null), JsonIgnore]
        public virtual ICollection<MembershipRepeatType>? MembershipRepeatTypes { get => membershipRepeatTypes; set => membershipRepeatTypes = value; }
        #endregion
    }
}
