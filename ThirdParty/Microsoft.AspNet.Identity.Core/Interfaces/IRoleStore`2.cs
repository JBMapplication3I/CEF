// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IRoleStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Interface that exposes basic role management.</summary>
    /// <typeparam name="TRole">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IRoleStore<TRole, in TKey> : IDisposable
        where TRole : IRole<TKey>
    {
        /// <summary>Create a new role.</summary>
        /// <param name="role">.</param>
        /// <returns>The new asynchronous.</returns>
        Task CreateAsync(TRole role);

        /// <summary>Delete a role.</summary>
        /// <param name="role">.</param>
        /// <returns>A Task.</returns>
        Task DeleteAsync(TRole role);

        /// <summary>Find a role by id.</summary>
        /// <param name="roleId">.</param>
        /// <returns>The found identifier asynchronous.</returns>
        Task<TRole> FindByIdAsync(TKey roleId);

        /// <summary>Find a role by name.</summary>
        /// <param name="roleName">.</param>
        /// <returns>The found name asynchronous.</returns>
        Task<TRole> FindByNameAsync(string roleName);

        /// <summary>Update a role.</summary>
        /// <param name="role">.</param>
        /// <returns>A Task.</returns>
        Task UpdateAsync(TRole role);
    }
}
