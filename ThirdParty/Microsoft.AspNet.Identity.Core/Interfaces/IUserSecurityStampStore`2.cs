// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserSecurityStampStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Stores a user's security stamp.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserSecurityStampStore<TUser, in TKey> : IUserStore<TUser, TKey>, IDisposable
        where TUser : class, IUser<TKey>
    {
        /// <summary>Get the user security stamp.</summary>
        /// <param name="user">.</param>
        /// <returns>The security stamp asynchronous.</returns>
        Task<string> GetSecurityStampAsync(TUser user);

        /// <summary>Set the security stamp for the user.</summary>
        /// <param name="user"> .</param>
        /// <param name="stamp">.</param>
        /// <returns>A Task.</returns>
        Task SetSecurityStampAsync(TUser user, string stamp);
    }
}
