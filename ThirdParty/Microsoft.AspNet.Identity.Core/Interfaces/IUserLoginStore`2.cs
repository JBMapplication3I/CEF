// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserLoginStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Interface that maps users to login providers, i.e. Google, Facebook, Twitter, Microsoft.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserLoginStore<TUser, in TKey> : IUserStore<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        /// <summary>Adds a user login with the specified provider and key.</summary>
        /// <param name="user"> .</param>
        /// <param name="login">.</param>
        /// <returns>A Task.</returns>
        Task AddLoginAsync(TUser user, UserLoginInfo login);

        /// <summary>Returns the user associated with this login.</summary>
        /// <param name="login">The login.</param>
        /// <returns>The found asynchronous.</returns>
        Task<TUser> FindAsync(UserLoginInfo login);

        /// <summary>Returns the linked accounts for this user.</summary>
        /// <param name="user">.</param>
        /// <returns>The logins asynchronous.</returns>
        Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user);

        /// <summary>Removes the user login with the specified combination if it exists.</summary>
        /// <param name="user"> .</param>
        /// <param name="login">.</param>
        /// <returns>A Task.</returns>
        Task RemoveLoginAsync(TUser user, UserLoginInfo login);
    }
}
