// <copyright file="RolePermission.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the permission class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    public interface IRolePermission : IAmExcludedFromT4Generation
    {
        /// <summary>Gets or sets the identifier of the role.</summary>
        /// <value>The identifier of the role.</value>
        int RoleId { get; set; }

        /// <summary>Gets or sets the role.</summary>
        /// <value>The role.</value>
        UserRole? Role { get; set; }

        /// <summary>Gets or sets the identifier of the permission.</summary>
        /// <value>The identifier of the permission.</value>
        int PermissionId { get; set; }

        /// <summary>Gets or sets the permission.</summary>
        /// <value>The permission.</value>
        Permission? Permission { get; set; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    [SqlSchema("Contacts", "RolePermission")]
    public class RolePermission : IRolePermission
    {
        /// <inheritdoc/>
        [Key, Column(Order = 0), InverseProperty(nameof(IBase.ID)), ForeignKey(nameof(Role)), DefaultValue(0)]
        public int RoleId { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual UserRole? Role { get; set; }

        /// <inheritdoc/>
        [Key, Column(Order = 1), InverseProperty(nameof(IBase.ID)), ForeignKey(nameof(Permission)), DefaultValue(0)]
        public int PermissionId { get; set; }

        /// <inheritdoc/>
        [DefaultValue(null)]
        public virtual Permission? Permission { get; set; }
    }
}
