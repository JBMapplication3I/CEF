// <copyright file="CEFRoleStore.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF role store class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System.Data.Entity;
    using Interfaces.DataModel;
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>A CEF role store.</summary>
    /// <seealso cref="RoleStore{TRole, TKey, TUserRole}"/>
    public class CEFRoleStore : RoleStore<UserRole, int, RoleUser>, ICEFRoleStore
    {
        /// <summary>Initializes a new instance of the <see cref="CEFRoleStore"/> class.</summary>
        /// <param name="context">The context.</param>
        public CEFRoleStore(IDbContext context)
            : base((DbContext)context)
        {
        }

        /// <inheritdoc/>
        public new IClarityEcommerceEntities Context => (IClarityEcommerceEntities)base.Context;
    }
}
