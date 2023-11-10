// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserRoleStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Interface that maps users to their roles.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserRoleStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable
        where TUser : class, IUser<TKey>
    {
        /// <summary>Adds a user to a role.</summary>
        /// <param name="user">    .</param>
        /// <param name="roleName">.</param>
        /// <returns>A Task.</returns>
        Task AddToRoleAsync(TUser user, string roleName);

        /// <summary>Returns the roles for this user.</summary>
        /// <param name="user">.</param>
        /// <returns>The roles asynchronous.</returns>
        Task<IList<string>> GetRolesAsync(TUser user);

        /// <summary>Returns true if a user is in the role.</summary>
        /// <param name="user">    .</param>
        /// <param name="roleName">.</param>
        /// <returns>A Task{bool}</returns>
        Task<bool> IsInRoleAsync(TUser user, string roleName);

        /// <summary>Removes the role for the user.</summary>
        /// <param name="user">    .</param>
        /// <param name="roleName">.</param>
        /// <returns>A Task.</returns>
        Task RemoveFromRoleAsync(TUser user, string roleName);
    }
}
