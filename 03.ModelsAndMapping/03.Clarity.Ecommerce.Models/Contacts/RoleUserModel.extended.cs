// <copyright file="RoleUserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role user model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    public partial class RoleUserModel
    {
        /// <inheritdoc cref="IRoleUserModel.Permissions"/>
        public List<PermissionModel>? Permissions { get; set; }

        /// <inheritdoc/>
        List<IPermissionModel>? IRoleUserModel.Permissions { get => Permissions?.ToList<IPermissionModel>(); set => Permissions = value?.Cast<PermissionModel>().ToList(); }
    }
}
