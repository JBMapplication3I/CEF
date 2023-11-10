// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.RoleManagerExtensions
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;

    /// <summary>Extension methods for RoleManager.</summary>
    public static class RoleManagerExtensions
    {
        /// <summary>Create a role.</summary>
        /// <typeparam name="TRole">Type of the role.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="role">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Create<TRole, TKey>(this RoleManager<TRole, TKey> manager, TRole role)
            where TRole : class, IRole<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.CreateAsync(role))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Delete a role.</summary>
        /// <typeparam name="TRole">Type of the role.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="role">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Delete<TRole, TKey>(this RoleManager<TRole, TKey> manager, TRole role)
            where TRole : class, IRole<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.DeleteAsync(role))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Find a role by id.</summary>
        /// <typeparam name="TRole">Type of the role.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="roleId"> .</param>
        /// <returns>The found identifier.</returns>
        public static TRole FindById<TRole, TKey>(this RoleManager<TRole, TKey> manager, TKey roleId)
            where TRole : class, IRole<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.FindByIdAsync(roleId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Find a role by name.</summary>
        /// <typeparam name="TRole">Type of the role.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="roleName">.</param>
        /// <returns>The found name.</returns>
        public static TRole FindByName<TRole, TKey>(this RoleManager<TRole, TKey> manager, string roleName)
            where TRole : class, IRole<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.FindByNameAsync(roleName))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns true if the role exists.</summary>
        /// <typeparam name="TRole">Type of the role.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="roleName">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool RoleExists<TRole, TKey>(this RoleManager<TRole, TKey> manager, string roleName)
            where TRole : class, IRole<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.RoleExistsAsync(roleName))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Update an existing role.</summary>
        /// <typeparam name="TRole">Type of the role.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="role">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Update<TRole, TKey>(this RoleManager<TRole, TKey> manager, TRole role)
            where TRole : class, IRole<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.UpdateAsync(role))
                : throw new ArgumentNullException(nameof(manager));
        }
    }
}
