// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.EmailTokenProvider`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    /// <summary>TokenProvider that generates tokens from the user's security stamp and notifies a user via their
    /// email.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="Microsoft.AspNet.Identity.TotpSecurityStampBasedTokenProvider{TUser,TKey}"/>
    /// <seealso cref="TotpSecurityStampBasedTokenProvider{TUser,TKey}"/>
    public class EmailTokenProvider<TUser, TKey> : TotpSecurityStampBasedTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>The body.</summary>
        private string body;

        /// <summary>The subject.</summary>
        private string subject;

        /// <summary>Email body which should contain a formatted string which the token will be the only argument.</summary>
        /// <value>The body format.</value>
        public string BodyFormat
        {
            get => body ?? "{0}";
            set => body = value;
        }

        /// <summary>Email subject used when a token notification is received.</summary>
        /// <value>The subject.</value>
        public string Subject
        {
            get => subject ?? string.Empty;
            set => subject = value;
        }

        /// <summary>Returns the email of the user for entropy in the token.</summary>
        /// <param name="purpose">.</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>The user modifier asynchronous.</returns>
        public override async Task<string> GetUserModifierAsync(
            string purpose,
            UserManager<TUser, TKey> manager,
            TUser user)
        {
            return "Email:" + purpose + ":" + await manager.GetEmailAsync(user.Id).WithCurrentCulture();
        }

        /// <summary>True if the user has an email set.</summary>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        public override async Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            var flag = !string.IsNullOrWhiteSpace(await manager.GetEmailAsync(user.Id).WithCurrentCulture());
            if (flag)
            {
                flag = await manager.IsEmailConfirmedAsync(user.Id).WithCurrentCulture();
            }
            return flag;
        }

        /// <summary>Notifies the user with a token via email using the Subject and BodyFormat.</summary>
        /// <param name="token">  .</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task.</returns>
        public override Task NotifyAsync(string token, UserManager<TUser, TKey> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return manager.SendEmailAsync(
                user.Id,
                Subject,
                string.Format(CultureInfo.CurrentCulture, BodyFormat, token));
        }
    }
}
