// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserEmailStore`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System.Threading.Tasks;

    /// <summary>Stores a user's email.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    public interface IUserEmailStore<TUser, in TKey> : IUserStore<TUser, TKey>
        where TUser : class, IUser<TKey>
    {
        /// <summary>Returns the user associated with this email.</summary>
        /// <param name="email">.</param>
        /// <returns>The found email asynchronous.</returns>
        Task<TUser> FindByEmailAsync(string email);

        /// <summary>Get the user email.</summary>
        /// <param name="user">.</param>
        /// <returns>The email asynchronous.</returns>
        Task<string> GetEmailAsync(TUser user);

        /// <summary>Returns true if the user email is confirmed.</summary>
        /// <param name="user">.</param>
        /// <returns>The email confirmed asynchronous.</returns>
        Task<bool> GetEmailConfirmedAsync(TUser user);

        /// <summary>Set the user email.</summary>
        /// <param name="user"> .</param>
        /// <param name="email">.</param>
        /// <returns>A Task.</returns>
        Task SetEmailAsync(TUser user, string email);

        /// <summary>Sets whether the user email is confirmed.</summary>
        /// <param name="user">     .</param>
        /// <param name="confirmed">.</param>
        /// <returns>A Task.</returns>
        Task SetEmailConfirmedAsync(TUser user, bool confirmed);
    }
}
