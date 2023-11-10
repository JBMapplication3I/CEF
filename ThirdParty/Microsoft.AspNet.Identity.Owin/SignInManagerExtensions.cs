// <copyright file="SignInManagerExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sign in manager extensions class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using System.Security.Claims;

    /// <summary>Extension methods for SignInManager/&gt;</summary>
    public static class SignInManagerExtensions
    {
        /// <summary>Called to generate the ClaimsIdentity for the user, override to add additional claims before SignIn.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>The new user identity.</returns>
        public static ClaimsIdentity CreateUserIdentity<TUser, TKey>(
            this SignInManager<TUser, TKey> manager,
            TUser user)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.CreateUserIdentityAsync(user));
        }

        /// <summary>Sign the user in using an associated external login.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">     .</param>
        /// <param name="loginInfo">   .</param>
        /// <param name="isPersistent">.</param>
        /// <returns>The SignInStatus.</returns>
        public static SignInStatus ExternalSignIn<TUser, TKey>(
            this SignInManager<TUser, TKey> manager,
            ExternalLoginInfo loginInfo,
            bool isPersistent)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.ExternalSignInAsync(loginInfo, isPersistent));
        }

        /// <summary>Get the user id that has been verified already or null.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <returns>The verified user identifier.</returns>
        public static TKey GetVerifiedUserId<TUser, TKey>(this SignInManager<TUser, TKey> manager)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.GetVerifiedUserIdAsync());
        }

        /// <summary>Has the user been verified (ie either via password or external login)</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <returns>True if been verified, false if not.</returns>
        public static bool HasBeenVerified<TUser, TKey>(this SignInManager<TUser, TKey> manager)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.HasBeenVerifiedAsync());
        }

        /// <summary>Sign in the user in using the user name and password.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">      .</param>
        /// <param name="userName">     .</param>
        /// <param name="password">     .</param>
        /// <param name="isPersistent"> .</param>
        /// <param name="shouldLockout">.</param>
        /// <returns>The SignInStatus.</returns>
        public static SignInStatus PasswordSignIn<TUser, TKey>(
            this SignInManager<TUser, TKey> manager,
            string userName,
            string password,
            bool isPersistent,
            bool shouldLockout)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(
                () => manager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout));
        }

        /// <summary>Send a two factor code to a user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="provider">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool SendTwoFactorCode<TUser, TKey>(this SignInManager<TUser, TKey> manager, string provider)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.SendTwoFactorCodeAsync(provider));
        }

        /// <summary>Creates a user identity and then signs the identity using the AuthenticationManager.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">        .</param>
        /// <param name="user">           .</param>
        /// <param name="isPersistent">   .</param>
        /// <param name="rememberBrowser">.</param>
        public static void SignIn<TUser, TKey>(
            this SignInManager<TUser, TKey> manager,
            TUser user,
            bool isPersistent,
            bool rememberBrowser)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            AsyncHelper.RunSync(() => manager.SignInAsync(user, isPersistent, rememberBrowser));
        }

        /// <summary>Two factor verification step.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">        .</param>
        /// <param name="provider">       .</param>
        /// <param name="code">           .</param>
        /// <param name="isPersistent">   .</param>
        /// <param name="rememberBrowser">.</param>
        /// <returns>The SignInStatus.</returns>
        public static SignInStatus TwoFactorSignIn<TUser, TKey>(
            this SignInManager<TUser, TKey> manager,
            string provider,
            string code,
            bool isPersistent,
            bool rememberBrowser)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(
                () => manager.TwoFactorSignInAsync(provider, code, isPersistent, rememberBrowser));
        }
    }
}
