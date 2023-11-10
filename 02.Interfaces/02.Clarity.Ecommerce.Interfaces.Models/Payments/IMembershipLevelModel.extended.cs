// <copyright file="IMembershipLevelModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMembershipLevelModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IMembershipLevelModel
    {
        #region MembershipLevel Properties
        /// <summary>Gets or sets the roles applied.</summary>
        /// <value>The roles applied.</value>
        string? RolesApplied { get; set; }
        #endregion

        #region Related Objects
        /// <summary>Gets or sets the identifier of the membership.</summary>
        /// <value>The identifier of the membership.</value>
        int MembershipID { get; set; }

        /// <summary>Gets or sets the membership key.</summary>
        /// <value>The membership key.</value>
        string? MembershipKey { get; set; }

        /// <summary>Gets or sets the name of the membership.</summary>
        /// <value>The name of the membership.</value>
        string? MembershipName { get; set; }

        /// <summary>Gets or sets the membership.</summary>
        /// <value>The membership.</value>
        IMembershipModel? Membership { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the membership ad zone access by levels.</summary>
        /// <value>The membership ad zone access by levels.</value>
        List<IMembershipAdZoneAccessByLevelModel>? MembershipAdZoneAccessByLevels { get; set; }
        #endregion
    }
}
