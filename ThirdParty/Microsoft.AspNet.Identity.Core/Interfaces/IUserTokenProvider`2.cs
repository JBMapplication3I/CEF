// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.IUserTokenProvider`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Threading.Tasks;

    /// <summary>Interface to generate user tokens.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    public interface IUserTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Generate a token for a user with a specific purpose.</summary>
        /// <param name="purpose">.</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>The asynchronous.</returns>
        Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user);

        /// <summary>Returns true if provider can be used for this user, i.e. could require a user to have an email.</summary>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user);

        /// <summary>Notifies the user that a token has been generated, for example an email or sms could be sent, or
        /// this can be a no-op.</summary>
        /// <param name="token">  .</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task.</returns>
        Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user);

        /// <summary>Validate a token for a user with a specific purpose.</summary>
        /// <param name="purpose">.</param>
        /// <param name="token">  .</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser, TKey> manager, TUser user);
    }
}
