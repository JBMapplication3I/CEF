// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserClaimStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>Stores user specific claims.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserClaimStore<TUser, in TKey> : IUserStore<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        /// <summary>Add a new user claim.</summary>
        /// <param name="user"> .</param>
        /// <param name="claim">.</param>
        /// <returns>A Task.</returns>
        Task AddClaimAsync(TUser user, Claim claim);

        /// <summary>Returns the claims for the user with the issuer set.</summary>
        /// <param name="user">.</param>
        /// <returns>The claims asynchronous.</returns>
        Task<IList<Claim>> GetClaimsAsync(TUser user);

        /// <summary>Remove a user claim.</summary>
        /// <param name="user"> .</param>
        /// <param name="claim">.</param>
        /// <returns>A Task.</returns>
        Task RemoveClaimAsync(TUser user, Claim claim);
    }
}
