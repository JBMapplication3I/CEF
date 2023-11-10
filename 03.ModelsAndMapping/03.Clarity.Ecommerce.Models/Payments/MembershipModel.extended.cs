// <copyright file="MembershipModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class MembershipModel
    {
        #region Membership Properties
        /// <inheritdoc/>
        public bool IsContractual { get; set; }
        #endregion

        #region Associated Objects
        /// <inheritdoc cref="IMembershipModel.MembershipLevels"/>
        public List<MembershipLevelModel>? MembershipLevels { get; set; }

        /// <inheritdoc/>
        List<IMembershipLevelModel>? IMembershipModel.MembershipLevels { get => MembershipLevels?.ToList<IMembershipLevelModel>(); set => MembershipLevels = value?.Cast<MembershipLevelModel>().ToList(); }

        /// <inheritdoc cref="IMembershipModel.MembershipRepeatTypes"/>
        public List<MembershipRepeatTypeModel>? MembershipRepeatTypes { get; set; }

        /// <inheritdoc/>
        List<IMembershipRepeatTypeModel>? IMembershipModel.MembershipRepeatTypes { get => MembershipRepeatTypes?.ToList<IMembershipRepeatTypeModel>(); set => MembershipRepeatTypes = value?.Cast<MembershipRepeatTypeModel>().ToList(); }
        #endregion
    }
}
