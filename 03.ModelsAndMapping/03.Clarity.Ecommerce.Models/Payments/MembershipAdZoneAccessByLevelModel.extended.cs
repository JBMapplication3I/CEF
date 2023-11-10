// <copyright file="MembershipAdZoneAccessByLevelModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership ad zone access by level model class</summary>
namespace Clarity.Ecommerce.Models
{
    public partial class MembershipAdZoneAccessByLevelModel
    {
        #region MembershipAdZoneAccessByLevel Properties
        /// <inheritdoc/>
        public int SubscriberCountThreshold { get; set; }

        /// <inheritdoc/>
        public int UniqueAdLimit { get; set; }
        #endregion
    }
}
