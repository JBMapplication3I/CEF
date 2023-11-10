// <copyright file="IMembershipAdZoneAccessModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMembershipAdZoneAccessModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IMembershipAdZoneAccessModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the membership ad zone access by levels.</summary>
        /// <value>The membership ad zone access by levels.</value>
        List<IMembershipAdZoneAccessByLevelModel>? MembershipAdZoneAccessByLevels { get; set; }
        #endregion
    }
}
