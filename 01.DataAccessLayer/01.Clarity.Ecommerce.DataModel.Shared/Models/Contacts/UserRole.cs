// <copyright file="UserRole.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user role class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;
    using Microsoft.AspNet.Identity;

    public interface IUserRole : IRole<int>, IAmExcludedFromT4Generation
    {
        ICollection<RolePermission> Permissions { get; }
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using System.Collections.Generic;
    using Interfaces.DataModel;
    using Microsoft.AspNet.Identity.EntityFramework;

    [SqlSchema("Contacts", "UserRole")]
    public class UserRole : IdentityRole<int, RoleUser>, IUserRole
    {
        public UserRole()
        {
            Permissions = new List<RolePermission>();
        }

        public UserRole(string name)
            : this()
        {
            Name = name;
        }

        public virtual ICollection<RolePermission> Permissions { get; }
    }
}
