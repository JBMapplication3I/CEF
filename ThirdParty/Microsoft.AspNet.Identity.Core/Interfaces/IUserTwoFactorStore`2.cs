// Decompiled with Telerik decompiler
// Type: Microsoft.AspNet.Identity.IUserTwoFactorStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Stores whether two factor authentication is enabled for a user.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserTwoFactorStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable
        where TUser : class, IUser<TKey>
    {
        /// <summary>Returns whether two factor authentication is enabled for the user.</summary>
        /// <param name="user">.</param>
        /// <returns>The two factor enabled asynchronous.</returns>
        Task<bool> GetTwoFactorEnabledAsync(TUser user);

        /// <summary>Sets whether two factor authentication is enabled for the user.</summary>
        /// <param name="user">   .</param>
        /// <param name="enabled">.</param>
        /// <returns>A Task.</returns>
        Task SetTwoFactorEnabledAsync(TUser user, bool enabled);
    }
}
