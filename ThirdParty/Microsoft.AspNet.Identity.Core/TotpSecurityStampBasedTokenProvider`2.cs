// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.TotpSecurityStampBasedTokenProvider`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    /// <summary>TokenProvider that generates time based codes using the user's security stamp.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="IUserTokenProvider{TUser,TKey}"/>
    /// <seealso cref="IUserTokenProvider{TUser,TKey}"/>
    public class TotpSecurityStampBasedTokenProvider<TUser, TKey> : IUserTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Generate a token for the user using their security stamp.</summary>
        /// <param name="purpose">.</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>The asynchronous.</returns>
        public virtual async Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            var token = await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture();
            return Rfc6238AuthenticationService
                .GenerateCode(token, await GetUserModifierAsync(purpose, manager, user).WithCurrentCulture())
                .ToString("D6", CultureInfo.InvariantCulture);
        }

        /// <summary>Used for entropy in the token, uses the user.Id by default.</summary>
        /// <param name="purpose">.</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>The user modifier asynchronous.</returns>
        public virtual Task<string> GetUserModifierAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult("Totp:" + purpose + ":" + user.Id);
        }

        /// <summary>Returns true if the provider can generate tokens for the user, by default this is equal to
        /// manager.SupportsUserSecurityStamp.</summary>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        public virtual Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            return manager != null
                ? Task.FromResult(manager.SupportsUserSecurityStamp)
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>This token provider does not notify the user by default.</summary>
        /// <param name="token">  .</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task.</returns>
        public virtual Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(0);
        }

        /// <summary>Validate the token for the user.</summary>
        /// <param name="purpose">.</param>
        /// <param name="token">  .</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> ValidateAsync(
            string purpose,
            string token,
            UserManager<TUser, TKey> manager,
            TUser user)
        {
            if (!int.TryParse(token, out var code))
            {
                return false;
            }
            var securityToken = await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture();
            var modifier = await GetUserModifierAsync(purpose, manager, user).WithCurrentCulture();
            return securityToken != null && Rfc6238AuthenticationService.ValidateCode(securityToken, code, modifier);
        }
    }
}
