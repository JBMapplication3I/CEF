// <copyright file="SignInManager`2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sign in manager` 2 class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using System.Globalization;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security;

    /// <summary>Manages Sign In operations for users.</summary>
    /// <typeparam name="TUser">.</typeparam>
    /// <typeparam name="TKey"> .</typeparam>
    /// <seealso cref="IDisposable"/>
    public class SignInManager<TUser, TKey> : IDisposable
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Type of the authentication.</summary>
        private string _authType;

        /// <summary>Constructor.</summary>
        /// <param name="userManager">          .</param>
        /// <param name="authenticationManager">.</param>
        public SignInManager(UserManager<TUser, TKey> userManager, IAuthenticationManager authenticationManager)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            AuthenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager));
        }

        /// <summary>Used to sign in identities.</summary>
        /// <value>The authentication manager.</value>
        public IAuthenticationManager AuthenticationManager
        {
            get;
            set;
        }

        /// <summary>AuthenticationType that will be used by sign in, defaults to
        /// DefaultAuthenticationTypes.ApplicationCookie.</summary>
        /// <value>The type of the authentication.</value>
        public string AuthenticationType
        {
            get => _authType ?? "ApplicationCookie";
            set => _authType = value;
        }

        /// <summary>Used to operate on users.</summary>
        /// <value>The user manager.</value>
        public UserManager<TUser, TKey> UserManager
        {
            get;
            set;
        }

        /// <summary>Convert a string id to the proper TKey using Convert.ChangeType.</summary>
        /// <param name="id">.</param>
        /// <returns>The identifier converted from string.</returns>
        public virtual TKey ConvertIdFromString(string id)
        {
            if (id == null)
            {
                return default;
            }
            return (TKey)Convert.ChangeType(id, typeof(TKey), CultureInfo.InvariantCulture);
        }

        /// <summary>Convert a TKey userId to a string, by default this just calls ToString()</summary>
        /// <param name="id">.</param>
        /// <returns>The identifier converted to string.</returns>
        public virtual string ConvertIdToString(TKey id)
        {
            return Convert.ToString(id, CultureInfo.InvariantCulture);
        }

        /// <summary>Called to generate the ClaimsIdentity for the user, override to add additional claims before SignIn.</summary>
        /// <param name="user">.</param>
        /// <returns>The new user identity asynchronous.</returns>
        public virtual Task<ClaimsIdentity> CreateUserIdentityAsync(TUser user)
        {
            return UserManager.CreateIdentityAsync(user, AuthenticationType);
        }

        /// <summary>Dispose.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Sign the user in using an associated external login.</summary>
        /// <param name="loginInfo">   .</param>
        /// <param name="isPersistent">.</param>
        /// <returns>A Task{SignInStatus}</returns>
        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            SignInStatus signInStatu;
            var tUser = await UserManager.FindAsync(loginInfo.Login).WithCurrentCulture();
            if (tUser != null)
            {
                if (!await UserManager.IsLockedOutAsync(tUser.Id).WithCurrentCulture())
                {
                    signInStatu = await SignInOrTwoFactor(tUser, isPersistent).WithCurrentCulture();
                }
                else
                {
                    signInStatu = SignInStatus.LockedOut;
                }
            }
            else
            {
                signInStatu = SignInStatus.Failure;
            }
            return signInStatu;
        }

        /// <summary>Get the user id that has been verified already or null.</summary>
        /// <returns>The verified user identifier asynchronous.</returns>
        public async Task<TKey> GetVerifiedUserIdAsync()
        {
            var authenticateResult = await AuthenticationManager.AuthenticateAsync("TwoFactorCookie").WithCurrentCulture();
            var tKey = authenticateResult == null
                || authenticateResult.Identity == null
                || string.IsNullOrEmpty(authenticateResult.Identity.GetUserId())
                    ? default
                    : ConvertIdFromString(authenticateResult.Identity.GetUserId());
            return tKey;
        }

        /// <summary>Has the user been verified (ie either via password or external login)</summary>
        /// <returns>A Task{bool}</returns>
        public async Task<bool> HasBeenVerifiedAsync()
        {
            return (await GetVerifiedUserIdAsync().WithCurrentCulture()) != null;
        }

        /// <summary>Sign in the user in using the user name and password.</summary>
        /// <param name="userName">     .</param>
        /// <param name="password">     .</param>
        /// <param name="isPersistent"> .</param>
        /// <param name="shouldLockout">.</param>
        /// <returns>A Task{SignInStatus}</returns>
        public virtual async Task<SignInStatus> PasswordSignInAsync(
            string userName,
            string password,
            bool isPersistent,
            bool shouldLockout)
        {
            if (UserManager == null)
            {
                return SignInStatus.Failure;
            }
            var user = await UserManager.FindByNameAsync(userName).WithCurrentCulture();
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            {
                return SignInStatus.LockedOut;
            }
            if (await UserManager.CheckPasswordAsync(user, password).WithCurrentCulture())
            {
                if (!await IsTwoFactorEnabled(user))
                {
                    await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                }
                return await SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
            }
            if (!shouldLockout)
            {
                return SignInStatus.Failure;
            }
            await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
            if (await UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
            {
                return SignInStatus.LockedOut;
            }
            return SignInStatus.Failure;
        }

        /// <summary>Send a two factor code to a user.</summary>
        /// <param name="provider">.</param>
        /// <returns>A Task{bool}</returns>
        public virtual async Task<bool> SendTwoFactorCodeAsync(string provider)
        {
            bool flag;
            var tKey = await GetVerifiedUserIdAsync().WithCurrentCulture();
            if (tKey != null)
            {
                var cultureAwaiter = UserManager.GenerateTwoFactorTokenAsync(tKey, provider).WithCurrentCulture();
                var str = await cultureAwaiter;
                var cultureAwaiter1 = UserManager.NotifyTwoFactorTokenAsync(tKey, provider, str).WithCurrentCulture();
                await cultureAwaiter1;
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>Creates a user identity and then signs the identity using the AuthenticationManager.</summary>
        /// <param name="user">           .</param>
        /// <param name="isPersistent">   .</param>
        /// <param name="rememberBrowser">.</param>
        /// <returns>A Task.</returns>
        public virtual async Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser)
        {
            var cultureAwaiter = CreateUserIdentityAsync(user).WithCurrentCulture();
            var claimsIdentity = await cultureAwaiter;
            AuthenticationManager.SignOut("ExternalCookie", "TwoFactorCookie");
            if (!rememberBrowser)
            {
                var authenticationManager = AuthenticationManager;
                var authenticationProperty = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                authenticationManager.SignIn(authenticationProperty, claimsIdentity);
            }
            else
            {
                var claimsIdentity1 =
                    AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(ConvertIdToString(user.Id));
                var authenticationManager1 = AuthenticationManager;
                var authenticationProperty1 = new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                };
                authenticationManager1.SignIn(authenticationProperty1, claimsIdentity, claimsIdentity1);
            }
        }

        /// <summary>Two factor verification step.</summary>
        /// <param name="provider">       .</param>
        /// <param name="code">           .</param>
        /// <param name="isPersistent">   .</param>
        /// <param name="rememberBrowser">.</param>
        /// <returns>A Task{SignInStatus}</returns>
        public virtual async Task<SignInStatus> TwoFactorSignInAsync(
            string provider,
            string code,
            bool isPersistent,
            bool rememberBrowser)
        {
            SignInStatus signInStatu;
            var key = await GetVerifiedUserIdAsync().WithCurrentCulture();
            if (key != null)
            {
                var user = await UserManager.FindByIdAsync(key).WithCurrentCulture();
                if (user != null)
                {
                    var cultureAwaiter2 = UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture();
                    if (!await cultureAwaiter2)
                    {
                        cultureAwaiter2 = UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).WithCurrentCulture();
                        if (!await cultureAwaiter2)
                        {
                            await UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
                            signInStatu = SignInStatus.Failure;
                        }
                        else
                        {
                            await UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                            var cultureAwaiter3 = SignInAsync(user, isPersistent, rememberBrowser).WithCurrentCulture();
                            await cultureAwaiter3;
                            signInStatu = SignInStatus.Success;
                        }
                    }
                    else
                    {
                        signInStatu = SignInStatus.LockedOut;
                    }
                }
                else
                {
                    signInStatu = SignInStatus.Failure;
                }
            }
            else
            {
                signInStatu = SignInStatus.Failure;
            }
            return signInStatu;
        }

        /// <summary>If disposing, calls dispose on the Context.  Always nulls out the Context.</summary>
        /// <param name="disposing">.</param>
        protected virtual void Dispose(bool disposing) { }

        /// <summary>Is two factor enabled.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task{bool}</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task<bool> IsTwoFactorEnabled(TUser user)
#pragma warning restore IDE1006 // Naming Styles
        {
            var count = await UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture();
            if (count)
            {
                count = (await UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0;
            }
            return count;
        }

        /// <summary>Sign in or two factor.</summary>
        /// <param name="user">        The user.</param>
        /// <param name="isPersistent">True if this SignInManager{TUser, TKey} is persistent.</param>
        /// <returns>A Task{SignInStatus}</returns>
#pragma warning disable IDE1006 // Naming Styles
        private async Task<SignInStatus> SignInOrTwoFactor(TUser user, bool isPersistent)
#pragma warning restore IDE1006 // Naming Styles
        {
            SignInStatus signInStatu;
            var str = Convert.ToString(user.Id);
            var flag = await IsTwoFactorEnabled(user);
            if (flag)
            {
                var cultureAwaiter = AuthenticationManagerExtensions.TwoFactorBrowserRememberedAsync(AuthenticationManager, str).WithCurrentCulture();
                flag = !await cultureAwaiter;
            }
            if (!flag)
            {
                await SignInAsync(user, isPersistent, false).WithCurrentCulture();
                signInStatu = SignInStatus.Success;
            }
            else
            {
                var claimsIdentity = new ClaimsIdentity("TwoFactorCookie");
                claimsIdentity.AddClaim(
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", str));
                AuthenticationManager.SignIn(claimsIdentity);
                signInStatu = SignInStatus.RequiresVerification;
            }
            return signInStatu;
        }
    }
}
