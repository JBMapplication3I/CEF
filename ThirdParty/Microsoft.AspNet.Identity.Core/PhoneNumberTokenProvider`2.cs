// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.PhoneNumberTokenProvider`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    /// <summary>TokenProvider that generates tokens from the user's security stamp and notifies a user via their
    /// phone number.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="Microsoft.AspNet.Identity.TotpSecurityStampBasedTokenProvider{TUser,TKey}"/>
    /// <seealso cref="TotpSecurityStampBasedTokenProvider{TUser,TKey}"/>
    public class PhoneNumberTokenProvider<TUser, TKey> : TotpSecurityStampBasedTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>The body.</summary>
        private string body;

        /// <summary>Message contents which should contain a format string which the token will be the only argument.</summary>
        /// <value>The message format.</value>
        public string MessageFormat
        {
            get => body ?? "{0}";
            set => body = value;
        }

        /// <summary>Returns the phone number of the user for entropy in the token.</summary>
        /// <param name="purpose">.</param>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>The user modifier asynchronous.</returns>
        public override async Task<string> GetUserModifierAsync(
            string purpose,
            UserManager<TUser, TKey> manager,
            TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return "PhoneNumber:" + purpose + ":" + await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture();
        }

        /// <summary>Returns true if the user has a phone number set.</summary>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>A Task{bool}</returns>
        public override async Task<bool> IsValidProviderForUserAsync(UserManager<TUser, TKey> manager, TUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var flag = !string.IsNullOrWhiteSpace(await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture());
            if (flag)
            {
                flag = await manager.IsPhoneNumberConfirmedAsync(user.Id).WithCurrentCulture();
            }
            return flag;
        }

        /// <summary>Notifies the user with a token via sms using the MessageFormat.</summary>
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
            return manager.SendSmsAsync(user.Id, string.Format(CultureInfo.CurrentCulture, MessageFormat, token));
        }
    }
}
