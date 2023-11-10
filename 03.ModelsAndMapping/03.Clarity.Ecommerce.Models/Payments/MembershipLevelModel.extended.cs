// <copyright file="MembershipLevelModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership level model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class MembershipLevelModel
    {
        #region MembershipLevel Properties
        /// <inheritdoc/>
        public string? RolesApplied { get; set; }
        #endregion

        #region Related Objects
        /// <inheritdoc/>
        public int MembershipID { get; set; }

        /// <inheritdoc/>
        public string? MembershipKey { get; set; }

        /// <inheritdoc/>
        public string? MembershipName { get; set; }

        /// <inheritdoc cref="IMembershipLevelModel.Membership"/>
        public MembershipModel? Membership { get; set; }

        /// <inheritdoc/>
        IMembershipModel? IMembershipLevelModel.Membership { get => Membership; set => Membership = (MembershipModel?)value; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IMembershipLevelModel.MembershipAdZoneAccessByLevels"/>
        public List<MembershipAdZoneAccessByLevelModel>? MembershipAdZoneAccessByLevels { get; set; }

        /// <inheritdoc/>
        List<IMembershipAdZoneAccessByLevelModel>? IMembershipLevelModel.MembershipAdZoneAccessByLevels { get => MembershipAdZoneAccessByLevels?.ToList<IMembershipAdZoneAccessByLevelModel>(); set => MembershipAdZoneAccessByLevels = value?.Cast<MembershipAdZoneAccessByLevelModel>().ToList(); }
        #endregion
    }
}
