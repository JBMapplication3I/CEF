// <copyright file="IMembershipAdZoneAccessByLevelModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMembershipAdZoneAccessByLevelModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface IMembershipAdZoneAccessByLevelModel
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
