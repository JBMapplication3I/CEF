// <copyright file="CEFRoleManager.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CEF role manager class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System.Linq;
    using JetBrains.Annotations;
    using Microsoft.AspNet.Identity;

    [PublicAPI]
    public interface ICEFRoleManager
    {
        /// <summary>Gets the roles.</summary>
        /// <value>The roles.</value>
        IQueryable<UserRole> Roles { get; }

        /// <summary>Creates a new IdentityResult.</summary>
        /// <param name="role">The role.</param>
        /// <returns>An IdentityResult.</returns>
        IdentityResult Create(UserRole role);

        /// <summary>Updates the given role.</summary>
        /// <param name="role">The role.</param>
        /// <returns>An IdentityResult.</returns>
        IdentityResult Update(UserRole role);

        /// <summary>Deletes the given role.</summary>
        /// <param name="role">The role to delete.</param>
        /// <returns>An IdentityResult.</returns>
        IdentityResult Delete(UserRole role);
    }

    /// <summary>Manager for CEF roles.</summary>
    /// <seealso cref="RoleManager{UserRole,Int32}"/>
    public class CEFRoleManager : RoleManager<UserRole, int>, ICEFRoleManager
    {
        /// <summary>Initializes a new instance of the <see cref="CEFRoleManager"/> class.</summary>
        /// <param name="store">The store.</param>
        public CEFRoleManager(IRoleStore<UserRole, int> store)
            : base(store)
        {
        }

        /// <inheritdoc/>
        public IdentityResult Create(UserRole role)
        {
            // ReSharper disable once AsyncConverter.AsyncWait
            return CreateAsync(role).Result;
        }

        /// <inheritdoc/>
        public IdentityResult Update(UserRole role)
        {
            // ReSharper disable once AsyncConverter.AsyncWait
            return UpdateAsync(role).Result;
        }

        /// <inheritdoc/>
        public IdentityResult Delete(UserRole role)
        {
            // ReSharper disable once AsyncConverter.AsyncWait
            return DeleteAsync(role).Result;
        }
    }
}
