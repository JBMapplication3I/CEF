// <copyright file="IMembershipModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IMembershipModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    public partial interface IMembershipModel
    {
        #region Membership Properties
        /// <summary>Gets or sets a value indicating whether this IMembership is contractual.</summary>
        /// <value>True if this IMembership is contractual, false if not.</value>
        bool IsContractual { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the membership levels.</summary>
        /// <value>The membership levels.</value>
        List<IMembershipLevelModel>? MembershipLevels { get; set; }

        /// <summary>Gets or sets a list of types of the membership repeats.</summary>
        /// <value>A list of types of the membership repeats.</value>
        List<IMembershipRepeatTypeModel>? MembershipRepeatTypes { get; set; }
        #endregion
    }
}
