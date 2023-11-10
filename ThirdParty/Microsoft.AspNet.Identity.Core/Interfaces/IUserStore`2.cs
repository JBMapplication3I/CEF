// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.IUserStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Interface that exposes basic user management apis.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserStore<TUser, in TKey> : IDisposable
        where TUser : class, IUser<TKey>
    {
        /// <summary>Insert a new user.</summary>
        /// <param name="user">.</param>
        /// <returns>The new asynchronous.</returns>
        Task CreateAsync(TUser user);

        /// <summary>Delete a user.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task.</returns>
        Task DeleteAsync(TUser user);

        /// <summary>Finds a user.</summary>
        /// <param name="userId">.</param>
        /// <returns>The found identifier asynchronous.</returns>
        Task<TUser> FindByIdAsync(TKey userId);

        /// <summary>Find a user by name.</summary>
        /// <param name="userName">.</param>
        /// <returns>The found name asynchronous.</returns>
        Task<TUser> FindByNameAsync(string userName);

        /// <summary>Update a user.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task.</returns>
        Task UpdateAsync(TUser user);
    }
}
