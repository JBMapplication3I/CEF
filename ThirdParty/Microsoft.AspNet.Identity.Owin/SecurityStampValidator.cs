// <copyright file="SecurityStampValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the security stamp validator class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security.Cookies;

    /// <summary>Static helper class used to configure a CookieAuthenticationProvider to validate a cookie against a
    /// user's security stamp.</summary>
    public static class SecurityStampValidator
    {
        /// <summary>Can be used as the ValidateIdentity method for a CookieAuthenticationProvider which will check a
        /// user's security stamp after validateInterval Rejects the identity if the stamp changes, and otherwise will
        /// call regenerateIdentity to sign in a new ClaimsIdentity.</summary>
        /// <typeparam name="TManager">.</typeparam>
        /// <typeparam name="TUser">   .</typeparam>
        /// <param name="validateInterval">  .</param>
        /// <param name="regenerateIdentity">.</param>
        /// <returns>A Func{CookieValidateIdentityContext,Task}</returns>
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(
            TimeSpan validateInterval,
            Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity)
            where TManager : UserManager<TUser, string>
            where TUser : class, IUser<string>
        {
            return OnValidateIdentity(validateInterval, regenerateIdentity, id => id.GetUserId());
        }

        /// <summary>Can be used as the ValidateIdentity method for a CookieAuthenticationProvider which will check a
        /// user's security stamp after validateInterval Rejects the identity if the stamp changes, and otherwise will
        /// call regenerateIdentity to sign in a new ClaimsIdentity.</summary>
        /// <typeparam name="TManager">.</typeparam>
        /// <typeparam name="TUser">   .</typeparam>
        /// <typeparam name="TKey">    .</typeparam>
        /// <param name="validateInterval">          .</param>
        /// <param name="regenerateIdentityCallback">.</param>
        /// <param name="getUserIdCallback">         .</param>
        /// <returns>A Func{CookieValidateIdentityContext,Task}</returns>
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser, TKey>(
            TimeSpan validateInterval,
            Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentityCallback,
            Func<ClaimsIdentity, TKey> getUserIdCallback)
            where TManager : UserManager<TUser, TKey>
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (getUserIdCallback == null)
            {
                throw new ArgumentNullException(nameof(getUserIdCallback));
            }
            return async context =>
            {
                var utcNow = DateTimeOffset.UtcNow;
                if (context.Options != null && context.Options.SystemClock != null)
                {
                    utcNow = context.Options.SystemClock.UtcNow;
                }
                var issuedUtc = context.Properties.IssuedUtc;
                var hasValue = !issuedUtc.HasValue;
                if (issuedUtc.HasValue)
                {
                    hasValue = utcNow.Subtract(issuedUtc.Value) > validateInterval;
                }
                if (hasValue)
                {
                    var userManager = context.OwinContext.GetUserManager<TManager>();
                    var identity = getUserIdCallback(context.Identity);
                    if (userManager != null && identity != null)
                    {
                        var tUser = await userManager.FindByIdAsync(identity).WithCurrentCulture();
                        var flag = true;
                        if (tUser != null && userManager.SupportsUserSecurityStamp)
                        {
                            var str = context.Identity.FindFirstValue("AspNet.Identity.SecurityStamp");
                            if (str == await userManager.GetSecurityStampAsync(identity).WithCurrentCulture())
                            {
                                str = null;
                                flag = false;
                                if (regenerateIdentityCallback != null)
                                {
                                    var claimsIdentity = await regenerateIdentityCallback(userManager, tUser).WithCurrentCulture();
                                    if (claimsIdentity != null)
                                    {
                                        DateTimeOffset? nullable = null;
                                        context.Properties.IssuedUtc = nullable;
                                        nullable = null;
                                        context.Properties.ExpiresUtc = nullable;
                                        context.OwinContext.Authentication.SignIn(context.Properties, claimsIdentity);
                                    }
                                }
                            }
                        }
                        if (flag)
                        {
                            context.RejectIdentity();
                            context.OwinContext.Authentication.SignOut(context.Options.AuthenticationType);
                        }
                        tUser = default;
                    }
                    userManager = default;
                    identity = default;
                }
            };
        }
    }
}
