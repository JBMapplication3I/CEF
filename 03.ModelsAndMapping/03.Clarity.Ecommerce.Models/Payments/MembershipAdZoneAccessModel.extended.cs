// <copyright file="MembershipAdZoneAccessModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership ad zone access model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class MembershipAdZoneAccessModel
    {
        #region Associated Objects
        /// <inheritdoc cref="IMembershipAdZoneAccessModel.MembershipAdZoneAccessByLevels"/>
        public List<MembershipAdZoneAccessByLevelModel>? MembershipAdZoneAccessByLevels { get; set; }

        /// <inheritdoc/>
        List<IMembershipAdZoneAccessByLevelModel>? IMembershipAdZoneAccessModel.MembershipAdZoneAccessByLevels { get => MembershipAdZoneAccessByLevels?.ToList<IMembershipAdZoneAccessByLevelModel>(); set => MembershipAdZoneAccessByLevels = value?.Cast<MembershipAdZoneAccessByLevelModel>().ToList(); }
        #endregion
    }
}
