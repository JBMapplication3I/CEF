// <copyright file="CMSAuthUserSession.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CMS authentication user session class</summary>
// ReSharper disable AsyncConverter.AsyncWait
namespace ServiceStack.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Interfaces.Workflow;
    using Clarity.Ecommerce.JSConfigs;
    using Clarity.Ecommerce.Models;
    using Clarity.Ecommerce.Utilities;
    using JetBrains.Annotations;
    using Microsoft.AspNet.Identity;
    using ServiceStack;

    /// <summary>The CMS authentication user session.</summary>
    /// <seealso cref="AuthUserSession"/>
    /// <seealso cref="ICMSAuthUserSession"/>
    [PublicAPI, DataContract]
    public class CMSAuthUserSession : AuthUserSession, ICMSAuthUserSession
    {
        private const string ServiceContextProfileName = null!;

        private const string CacheKeyForAccount = "current-account";

        private const string CacheKeyForUser = "current-user";

        /// <summary>Initializes a new instance of the <see cref="CMSAuthUserSession"/> class.</summary>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Thrown when a Configuration Errors error condition occurs.</exception>
        public CMSAuthUserSession()
        {
            Contract.RequiresValidKey<System.Configuration.ConfigurationErrorsException>(
                CEFConfigDictionary.AuthProviders,
                "AuthProviders isn't set, please contact the administrator to add this app setting to the web config.");
        }

        /// <inheritdoc/>
        [DataMember]
        public string[]? CMSRoles { get; set; }

        /// <inheritdoc/>
        public int? AccountID
        {
            get
            {
                if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
                {
                    return null;
                }
                if (Contract.CheckValidID(SelectedAccountID))
                {
                    return SelectedAccountID;
                }
                using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
                return context.Accounts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterAccountsByUserID(Contract.RequiresValidID(parsed))
                    .Select(x => (int?)x.ID)
                    .SingleOrDefault();
            }
        }

        /// <inheritdoc/>
        [DataMember]
        public int? SelectedAccountID { get; set; }

        /// <inheritdoc/>
        public string? AccountKey
        {
            get
            {
                if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
                {
                    return null;
                }
                using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
                return context.Accounts
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(Contract.RequiresValidID(AccountID))
                    .Select(x => x.CustomKey)
                    .SingleOrDefault();
            }
        }

        /// <inheritdoc/>
        public int? UserID => IsAuthenticated && Contract.CheckValidID(int.Parse(UserAuthId))
            ? int.Parse(UserAuthId)
            : null;

        /// <inheritdoc/>
        public string? UserKey => IsAuthenticated && Contract.CheckValidID(int.Parse(UserAuthId))
            ? UserAuthName
            : null;

        /// <inheritdoc/>
        public string? UserUsername
        {
            get
            {
                if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
                {
                    return null;
                }
                using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
                return context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(parsed)
                    .Select(x => x.CustomKey)
                    .Single();
            }
        }

        private static ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        private static IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <inheritdoc/>
        public async Task<IAccountModel?> AccountAsync()
        {
            if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
            {
                return null;
            }
            var key = $"session:{Id}:{CacheKeyForAccount}";
            try
            {
                //var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
                //if (client is null)
                //{
                //    return await GetAccountManuallyAsync(key, ServiceContextProfileName).ConfigureAwait(false);
                //}
                //var cachedResult = await client.GetAsync<AccountModel?>(key).ConfigureAwait(false);
                //if (cachedResult is null)
                //{
                //    return await GetAccountManuallyAsync(key, ServiceContextProfileName).ConfigureAwait(false);
                //}
                //cachedResult.AccountContacts = (await Workflows.AddressBooks.GetAddressBookAsync(
                //            cachedResult.ID,
                //            ServiceContextProfileName)
                //        .ConfigureAwait(false))
                //    .Cast<AccountContactModel>()
                //    .ToList();
                //return cachedResult;
                return await GetAccountManuallyAsync(key, ServiceContextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(CMSAuthUserSession)}.{nameof(Account)}.{ex.GetType().Name}",
                        message: "Error getting Session Account from Cache:\r\n" + ex.Message,
                        ex: ex,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
                return await GetAccountManuallyAsync(key, ServiceContextProfileName).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<IUserModel?> UserAsync()
        {
            if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
            {
                return null;
            }
            var key = $"session:{Id}:{CacheKeyForUser}";
            try
            {
                var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
                if (client is null)
                {
                    return await GetUserManuallyAsync(key, ServiceContextProfileName).ConfigureAwait(false);
                }
                var cachedResult = await client.GetAsync<UserModel?>(key).ConfigureAwait(false);
                if (cachedResult is not null && cachedResult.ID == parsed)
                {
                    return cachedResult;
                }
                return await GetUserManuallyAsync(key, ServiceContextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(CMSAuthUserSession)}.{nameof(User)}.{ex.GetType().Name}",
                        message: "Error getting Session User from Cache:\r\n" + ex.Message,
                        ex: ex,
                        ServiceContextProfileName)
                    .ConfigureAwait(false);
                return await GetUserManuallyAsync(key, ServiceContextProfileName).ConfigureAwait(false);
            }
        }

        /// <summary>Query if 'role' has role.</summary>
        /// <param name="role">    The role.</param>
        /// <param name="authRepo">The authentication repo.</param>
        /// <returns>true if role, false if not.</returns>
        public override bool HasRole(string role, IAuthRepository authRepo)
        {
            if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
            {
                return false;
            }
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            return userManager.UserHasRoleAsync(parsed, role).Result;
        }

        /// <inheritdoc/>
        public bool HasAnyRole(Regex role)
        {
            if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
            {
                return false;
            }
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            return userManager.GetRoleNamesForUserAsync(parsed)
                .Result
                .Any(role.IsMatch);
        }

        /// <summary>Query if 'permission' has permission.</summary>
        /// <param name="permission">The permission.</param>
        /// <param name="authRepo">  The authentication repo.</param>
        /// <returns>true if permission, false if not.</returns>
        public override bool HasPermission(string permission, IAuthRepository authRepo)
        {
            if (!int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
            {
                return Permissions?.Contains(permission) == true;
            }
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            if (userManager.UserHasPermissionAsync(parsed, permission).Result)
            {
                return true;
            }
            return Permissions?.Contains(permission) == true;
        }

        /// <inheritdoc/>
        public bool HasAnyPermission(Regex permission)
        {
            if (!IsAuthenticated || !int.TryParse(UserAuthId, out var parsed) || !Contract.CheckValidID(parsed))
            {
                return Permissions?.Any(permission.IsMatch) == true;
            }
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            if (userManager.GetPermissionNamesForUserAsync(parsed).Result.Any(permission.IsMatch))
            {
                return true;
            }
            return Permissions?.Any(permission.IsMatch) == true;
        }

        /// <summary>Executes the authenticated action.</summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="session">    The session.</param>
        /// <param name="tokens">     The tokens.</param>
        /// <param name="authInfo">   Information describing the authentication.</param>
        public override void OnAuthenticated(
            IServiceBase authService,
            IAuthSession session,
            IAuthTokens tokens,
            Dictionary<string, string> authInfo)
        {
            var authID = session.UserAuthId;
            if (string.IsNullOrWhiteSpace(authID))
            {
                OnLogout(authService);
                return;
            }
            using (var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName))
            {
                using var userStore = new CEFUserStore(context);
                using var userManager = new UserManager<User, int>(userStore);
                var userID = int.Parse(authID);
                if (!Contract.CheckValidID(userManager.Users.Select(x => x.ID).SingleOrDefault(x => x == userID)))
                {
                    OnLogout(authService);
                    return;
                }
            }
            base.OnAuthenticated(authService, session, tokens, authInfo);
        }

        /// <summary>Executes the logout action.</summary>
        /// <param name="authService">The authentication service.</param>
        public override void OnLogout(IServiceBase authService)
        {
            var session = authService.GetSession();
            ClearSessionUserAsync($"session:{session.Id}:{CacheKeyForUser}").Wait(10_000);
            ClearSessionAccountAsync($"session:{session.Id}:{CacheKeyForAccount}").Wait(10_000);
            var authID = session.UserAuthId;
            if (string.IsNullOrWhiteSpace(authID))
            {
                base.OnLogout(authService);
                return;
            }
            var userID = int.Parse(authID);
            using (var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName))
            {
                using var userStore = new CEFUserStore(context);
                using var userManager = new UserManager<User, int>(userStore);
                var userName = userManager.Users
                    .Where(x => x.ID == userID)
                    .Select(x => x.UserName)
                    .SingleOrDefault();
                if (Contract.CheckValidKey(userName))
                {
                    if (userManager.GetLogins(userID)
                        .Any(x => x.LoginProvider == "Identity"
                               && x.ProviderKey == $"{CEFConfigDictionary.SiteRouteHostUrl.Replace("http://", string.Empty).Replace("https://", string.Empty)}|{userName}"))
                    {
                        var result = userManager.RemoveLogin(
                            userID,
                            new(
                                "Identity",
                                $"{CEFConfigDictionary.SiteRouteHostUrl.Replace("http://", string.Empty).Replace("https://", string.Empty)}|{userName}"));
                        // ReSharper disable once ConvertIfStatementToReturnStatement
                        if (!result.Succeeded)
                        {
                            // TODO: Log Error and data that caused it
                        }
                    }
                    else
                    {
                        // User is already logged out, do the rest in ServiceStack
                    }
                }
            }
            // All good on our end, do the rest in ServiceStack
            authService.RemoveSession();
            base.OnLogout(authService);
        }

        /// <inheritdoc/>
        public async Task ClearSessionUserAsync(string? key = null)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (client is null)
            {
                return;
            }
            await client.RemoveAsync(key ?? $"session:{Id}:{CacheKeyForUser}").ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task ClearSessionAccountAsync(string? key = null)
        {
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(ServiceContextProfileName).ConfigureAwait(false);
            if (client is null)
            {
                return;
            }
            await client.RemoveAsync(key ?? $"session:{Id}:{CacheKeyForAccount}").ConfigureAwait(false);
        }

        private async Task<IAccountModel?> GetAccountManuallyAsync(string key, string? contextProfileName)
        {
            if (Contract.CheckInvalidID(AccountID))
            {
                return null;
            }
            // TODO: Pull more limited data
            var account = await Workflows.Accounts.GetAsync(AccountID!.Value, ServiceContextProfileName).ConfigureAwait(false);
            if (account == null)
            {
                return null;
            }
            //account.AccountContacts ??= await Workflows.AddressBooks.GetAddressBookAsync(
            //        Contract.RequiresValidID(account.ID),
            //        contextProfileName)
            //    .ConfigureAwait(false);
            account.Users = null;
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            if (client is not null)
            {
                await client.AddAsync(key, account, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
            }
            return account;
        }

        /// <summary>Gets user manually.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The user manually.</returns>
        private async Task<IUserModel?> GetUserManuallyAsync(string key, string? contextProfileName)
        {
            var user = await Workflows.Users.GetAsync(int.Parse(UserAuthId), contextProfileName).ConfigureAwait(false);
            var client = await RegistryLoaderWrapper.GetCacheClientAsync(contextProfileName).ConfigureAwait(false);
            if (client is not null)
            {
                await client.AddAsync(key, user, true, CEFConfigDictionary.CachingTimeoutTimeSpan).ConfigureAwait(false);
            }
            return user;
        }
    }
}
