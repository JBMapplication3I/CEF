// <copyright file="AuthenticationManagerExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication manager extensions class</summary>
namespace Microsoft.Owin.Security
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AspNet.Identity;
    using AspNet.Identity.Owin;

    /// <summary>Extensions methods on IAuthenticationManager that add methods for using the default Application and
    /// External authentication type constants.</summary>
    public static class AuthenticationManagerExtensions
    {
        /// <summary>Creates a TwoFactorRememberBrowser cookie for a user.</summary>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The new two factor remember browser identity.</returns>
        public static ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(
            this IAuthenticationManager manager,
            string userId)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var claimsIdentity = new ClaimsIdentity("TwoFactorRememberBrowser");
            claimsIdentity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));
            return claimsIdentity;
        }

        /// <summary>Return the authentication types which are considered external because they have captions.</summary>
        /// <param name="manager">.</param>
        /// <returns>An enumerator that allows foreach to be used to process the external authentication types in this
        /// collection.</returns>
        public static IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes(
            this IAuthenticationManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return manager.GetAuthenticationTypes(
                d =>
                {
                    if (d.Properties == null)
                    {
                        return false;
                    }
                    return d.Properties.ContainsKey("Caption");
                });
        }

        /// <summary>Return the identity associated with the default external authentication type.</summary>
        /// <param name="manager">                   .</param>
        /// <param name="externalAuthenticationType">.</param>
        /// <returns>The external identity.</returns>
        public static ClaimsIdentity GetExternalIdentity(
            this IAuthenticationManager manager,
            string externalAuthenticationType)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => GetExternalIdentityAsync(manager, externalAuthenticationType));
        }

        /// <summary>Return the identity associated with the default external authentication type.</summary>
        /// <param name="manager">                   The manager.</param>
        /// <param name="externalAuthenticationType">Type of the external authentication.</param>
        /// <returns>The external identity asynchronous.</returns>
        public static async Task<ClaimsIdentity> GetExternalIdentityAsync(
            IAuthenticationManager manager,
            string externalAuthenticationType)
        {
            ClaimsIdentity identity;
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var cultureAwaiter = manager.AuthenticateAsync(externalAuthenticationType).WithCurrentCulture();
            var authenticateResult = await cultureAwaiter;
            if (authenticateResult == null
                || authenticateResult.Identity == null
                || authenticateResult.Identity.FindFirst(
                    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                == null)
            {
                identity = null;
            }
            else
            {
                identity = authenticateResult.Identity;
            }
            return identity;
        }

        /// <summary>Extracts login info out of an external identity.</summary>
        /// <param name="manager">.</param>
        /// <returns>The external login information.</returns>
        public static ExternalLoginInfo GetExternalLoginInfo(this IAuthenticationManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => GetExternalLoginInfoAsync(manager));
        }

        /// <summary>Extracts login info out of an external identity.</summary>
        /// <param name="manager">      .</param>
        /// <param name="xsrfKey">      key that will be used to find the userId to verify.</param>
        /// <param name="expectedValue">the value expected to be found using the xsrfKey in the
        ///                             AuthenticationResult.Properties dictionary.</param>
        /// <returns>The external login information.</returns>
        public static ExternalLoginInfo GetExternalLoginInfo(
            this IAuthenticationManager manager,
            string xsrfKey,
            string expectedValue)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => GetExternalLoginInfoAsync(manager, xsrfKey, expectedValue));
        }

        /// <summary>Extracts login info out of an external identity.</summary>
        /// <param name="manager">.</param>
        /// <returns>The external login information asynchronous.</returns>
        public static async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(IAuthenticationManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var cultureAwaiter = manager.AuthenticateAsync("ExternalCookie").WithCurrentCulture();
            return GetExternalLoginInfo(await cultureAwaiter);
        }

        /// <summary>Extracts login info out of an external identity.</summary>
        /// <param name="manager">      .</param>
        /// <param name="xsrfKey">      key that will be used to find the userId to verify.</param>
        /// <param name="expectedValue">the value expected to be found using the xsrfKey in the
        ///                             AuthenticationResult.Properties dictionary.</param>
        /// <returns>The external login information asynchronous.</returns>
        public static async Task<ExternalLoginInfo> GetExternalLoginInfoAsync(
            IAuthenticationManager manager,
            string xsrfKey,
            string expectedValue)
        {
            ExternalLoginInfo externalLoginInfo;
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var cultureAwaiter = manager.AuthenticateAsync("ExternalCookie").WithCurrentCulture();
            var authenticateResult = await cultureAwaiter;
            if (authenticateResult == null
                || authenticateResult.Properties == null
                || authenticateResult.Properties.Dictionary == null
                || !authenticateResult.Properties.Dictionary.ContainsKey(xsrfKey)
                || !(authenticateResult.Properties.Dictionary[xsrfKey] == expectedValue))
            {
                externalLoginInfo = null;
            }
            else
            {
                externalLoginInfo = GetExternalLoginInfo(authenticateResult);
            }
            return externalLoginInfo;
        }

        /// <summary>Returns true if there is a TwoFactorRememberBrowser cookie for a user.</summary>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool TwoFactorBrowserRemembered(this IAuthenticationManager manager, string userId)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => TwoFactorBrowserRememberedAsync(manager, userId));
        }

        /// <summary>Returns true if there is a TwoFactorRememberBrowser cookie for a user.</summary>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>A Task{bool}</returns>
        public static async Task<bool> TwoFactorBrowserRememberedAsync(IAuthenticationManager manager, string userId)
        {
            return manager == null
                ? throw new ArgumentNullException(nameof(manager))
                : (await manager.AuthenticateAsync("TwoFactorRememberBrowser").WithCurrentCulture())?.Identity?.GetUserId() == userId;
        }

        /// <summary>Gets external login information.</summary>
        /// <param name="result">The result.</param>
        /// <returns>The external login information.</returns>
        private static ExternalLoginInfo GetExternalLoginInfo(AuthenticateResult result)
        {
            if (result == null || result.Identity == null)
            {
                return null;
            }
            var claim = result.Identity.FindFirst(
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (claim == null)
            {
                return null;
            }
            var name = result.Identity.Name;
            if (name != null)
            {
                name = name.Replace(" ", string.Empty);
            }
            var str = result.Identity.FindFirstValue(
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            return new ExternalLoginInfo
            {
                ExternalIdentity = result.Identity,
                Login = new UserLoginInfo(claim.Issuer, claim.Value),
                DefaultUserName = name,
                Email = str,
            };
        }
    }
}
