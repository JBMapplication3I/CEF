// <copyright file="DataProtectorTokenProvider`2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the data protector token provider` 2 class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.DataProtection;

    /// <summary>Token provider that uses an IDataProtector to generate encrypted tokens based off of the security
    /// stamp.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    /// <seealso cref="IUserTokenProvider{TUser,TKey}"/>
    public class DataProtectorTokenProvider<TUser, TKey> : IUserTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Constructor.</summary>
        /// <param name="protector">.</param>
        public DataProtectorTokenProvider(IDataProtector protector)
        {
            Protector = protector ?? throw new ArgumentNullException(nameof(protector));
            TokenLifespan = TimeSpan.FromDays(1);
        }

        /// <summary>IDataProtector for the token.</summary>
        /// <value>The protector.</value>
        public IDataProtector Protector
        {
            get;
        }

        /// <summary>Lifespan after which the token is considered expired.</summary>
        /// <value>The token lifespan.</value>
        public TimeSpan TokenLifespan
        {
            get;
            set;
        }

        /// <summary>Generate a protected string for a user.</summary>
        /// <param name="purpose">.</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>The asynchronous.</returns>
        public async Task<string> GenerateAsync(string purpose, UserManager<TUser, TKey> manager, TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var ms = new MemoryStream();
            using (var writer = ms.CreateWriter())
            {
                writer.Write(DateTimeOffset.UtcNow);
                writer.Write(Convert.ToString(user.Id, CultureInfo.InvariantCulture));
                writer.Write(purpose ?? string.Empty);
                string str = null;
                if (manager.SupportsUserSecurityStamp)
                {
                    str = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture();
                }
                writer.Write(str ?? string.Empty);
            }
            return Convert.ToBase64String(Protector.Protect(ms.ToArray()));
        }

        /// <summary>Returns true if the provider can be used to generate tokens for this user.</summary>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        public Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(true);
        }

        /// <summary>This provider no-ops by default when asked to notify a user.</summary>
        /// <param name="token">  .</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task.</returns>
        public Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(0);
        }

        /// <summary>Return false if the token is not valid.</summary>
        /// <param name="purpose">.</param>
        /// <param name="token">  .</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        public async Task<bool> ValidateAsync(
            string purpose,
            string token,
            UserManager<TUser, TKey> manager,
            TUser user)
        {
            try
            {
                using var reader = new MemoryStream(Protector.Unprotect(Convert.FromBase64String(token))).CreateReader();
                if (reader.ReadDateTimeOffset() + TokenLifespan < DateTimeOffset.UtcNow
                    || !string.Equals(reader.ReadString(), Convert.ToString(user.Id, CultureInfo.InvariantCulture))
                    || !string.Equals(reader.ReadString(), purpose))
                {
                    return false;
                }
                var stamp = reader.ReadString();
                if (reader.PeekChar() != -1)
                {
                    return false;
                }
                return manager.SupportsUserSecurityStamp
                    ? stamp == await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture()
                    : stamp == string.Empty;
            }
            catch { }
            return false;
        }
    }
}
