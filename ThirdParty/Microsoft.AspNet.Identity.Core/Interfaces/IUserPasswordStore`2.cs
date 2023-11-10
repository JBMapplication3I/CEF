// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserPasswordStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Threading.Tasks;

    /// <summary>Stores a user's password hash.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserPasswordStore<TUser, in TKey> : IUserStore<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        /// <summary>Get the user password hash.</summary>
        /// <param name="user">.</param>
        /// <returns>The password hash asynchronous.</returns>
        Task<string> GetPasswordHashAsync(TUser user);

        /// <summary>Returns true if a user has a password set.</summary>
        /// <param name="user">.</param>
        /// <returns>A Task{bool}</returns>
        Task<bool> HasPasswordAsync(TUser user);

        /// <summary>Set the user password hash.</summary>
        /// <param name="user">        .</param>
        /// <param name="passwordHash">.</param>
        /// <returns>A Task.</returns>
        Task SetPasswordHashAsync(TUser user, string passwordHash);
    }
}
