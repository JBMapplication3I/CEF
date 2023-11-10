// <copyright file="IRoleUserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRoleUserModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for role user model.</summary>
    public partial interface IRoleUserModel
    {
        /// <summary>Gets or sets the permissions.</summary>
        /// <value>The permissions.</value>
        List<IPermissionModel>? Permissions { get; set; }
    }
}