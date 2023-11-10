// <copyright file="IGroupModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IGroupModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for group model.</summary>
    public partial interface IGroupModel
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the group owner.</summary>
        /// <value>The identifier of the group owner.</value>
        int? GroupOwnerID { get; set; }

        /// <summary>Gets or sets the group owner key.</summary>
        /// <value>The group owner key.</value>
        string? GroupOwnerKey { get; set; }

        /// <summary>Gets or sets the group that owns this item.</summary>
        /// <value>The group owner.</value>
        IUserModel? GroupOwner { get; set; }
        #endregion

        #region Associated Objects
        /// <summary>Gets or sets the group users.</summary>
        /// <value>The group users.</value>
        List<IGroupUserModel>? GroupUsers { get; set; }
        #endregion
    }
}
