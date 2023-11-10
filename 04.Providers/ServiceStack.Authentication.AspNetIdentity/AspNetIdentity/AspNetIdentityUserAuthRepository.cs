// <copyright file="AspNetIdentityUserAuthRepository.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the ASP.NET Identity user authentication repository class</summary>
namespace ServiceStack.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using Auth;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Utilities;
    using JetBrains.Annotations;
    using ServiceStack;
    using IUser = Clarity.Ecommerce.Interfaces.DataModel.IUser;

    /// <summary>An ASP.NET Identity user authentication repository.</summary>
    /// <remarks>Based on
    /// https://github.com/ServiceStack/ServiceStack/blob/v4.0.40/src/ServiceStack.Authentication.NHibernate/NHibernateUserAuthRepository.cs .
    /// </remarks>
    /// <seealso cref="ICEFUserAuthRepository"/>
    [PublicAPI]
    public class AspNetIdentityUserAuthRepository : ICEFUserAuthRepository
    {
        private static ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Loads user authentication.</summary>
        /// <param name="session">The session.</param>
        /// <param name="tokens"> The tokens.</param>
        public void LoadUserAuth(IAuthSession session, IAuthTokens tokens)
        {
            session.ThrowIfNull("session");
            var userAuth = GetUserAuth(session, tokens);
            LoadUserAuth(session, userAuth);
        }

        /// <summary>Gets user authentication.</summary>
        /// <param name="authSession">The authentication session.</param>
        /// <param name="tokens">     The tokens.</param>
        /// <returns>The user authentication.</returns>
        public IUserAuth? GetUserAuth(IAuthSession authSession, IAuthTokens? tokens)
        {
            if (!string.IsNullOrEmpty(authSession.UserAuthId))
            {
                var userAuth = GetUserAuth(authSession.UserAuthId);
                if (userAuth != null)
                {
                    return userAuth;
                }
            }
            if (!string.IsNullOrEmpty(authSession.UserAuthName))
            {
                var userAuth = GetUserAuthByUserName(authSession.UserAuthName);
                if (userAuth != null)
                {
                    return userAuth;
                }
            }
            if (string.IsNullOrEmpty(tokens?.Provider) || string.IsNullOrEmpty(tokens!.UserId))
            {
                return null;
            }
            return null;
        }

        /// <summary>Gets user authentication.</summary>
        /// <param name="userAuthId">Identifier for the user authentication.</param>
        /// <returns>The user authentication.</returns>
        public IUserAuth? GetUserAuth(string userAuthId)
        {
            // Parse the ID
            var authId = int.Parse(userAuthId);
            // Get a DB Context
            return RunWrappedInUserManager(userManager =>
            {
                // Search for the user
                var user = userManager.Users
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                    .SingleOrDefault(x => x.ID == authId);
                return user == null ? null : MapUserToUserAuth(user);
            });
        }

        /// <summary>Gets user authentication by user name.</summary>
        /// <param name="userNameOrEmail">The user name or email.</param>
        /// <returns>The user authentication by user name.</returns>
        public IUserAuth? GetUserAuthByUserName(string userNameOrEmail)
        {
            if (!Contract.CheckValidKey(userNameOrEmail))
            {
                return null;
            }
            return RunWrappedInUserManager(userManager =>
            {
                var user = userManager.Users
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                    .FilterByActive(true)
                    .FilterUsersByUserNameOrEmail(userNameOrEmail, true)
                    .SingleOrDefault();
                return user == null ? null : MapUserToUserAuth(user);
            });
        }

        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="userId">  Identifier for the user.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public bool TryAuthenticate(string userName, string password, out string? userId)
        {
            userId = null;
            using (var context = RegistryLoaderWrapper.GetContext(null))
            {
                var userAuthData = context.Users
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterUsersByUserName(userName, true)
                    .Select(x => new
                    {
                        x.IsApproved,
                        x.LockoutEnabled,
                    })
                    .SingleOrDefault();
                if (userAuthData == null)
                {
                    // User not found
                    return false;
                }
                if (!userAuthData.IsApproved || userAuthData.LockoutEnabled)
                {
                    // User is not approved or is locked out, not allowed to log in
                    return false;
                }
            }
            if (!TryAuthenticate(userName, password, out IUserAuth? userAuth))
            {
                return false;
            }
            userId = userAuth!.Id.ToString(CultureInfo.InvariantCulture);
            return true;
        }

        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="digestHeaders">The digest headers.</param>
        /// <param name="privateKey">   The private key.</param>
        /// <param name="nonceTimeOut"> The nonce time out.</param>
        /// <param name="sequence">     The sequence.</param>
        /// <param name="userAuth">     The user authentication.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public bool TryAuthenticate(Dictionary<string, string> digestHeaders, string privateKey, int nonceTimeOut, string sequence, out IUserAuth? userAuth)
        {
            userAuth = GetUserAuthByUserName(digestHeaders["username"]);
            if (userAuth == null)
            {
                return false;
            }
            return new DigestAuthFunctions()
                .ValidateResponse(digestHeaders, privateKey, nonceTimeOut, userAuth.DigestHa1Hash, sequence);
        }

        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="userAuth">The user authentication.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public bool TryAuthenticate(string userName, string password, out IUserAuth? userAuth)
        {
            // Check for MFA
            var mfaToken = string.Empty;
            var mfaEnabled = bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.Enabled"] ?? "false");
            if (mfaEnabled)
            {
                mfaToken = password[^6..];
                password = password[..^6];
            }
            // NOTE: Can't use RunWrapped because of the out param
            // Step 1: Get the Repository of Users
            using var context = RegistryLoaderWrapper.GetContext(null);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            var userAuthData = context.Users
                .AsNoTracking()
                .FilterByActive(true)
                .FilterUsersByUserName(userName, true)
                .Select(x => new
                {
                    x.IsApproved,
                    x.LockoutEnabled,
                })
                .SingleOrDefault();
            if (userAuthData == null)
            {
                // User not found
                userAuth = null;
                return false;
            }
            if (!userAuthData.IsApproved || userAuthData.LockoutEnabled)
            {
                // User is not approved or is locked out, not allowed to log in
                userAuth = null;
                return false;
            }
            // Step 2: Make sure the user exists and the password checks out
            userAuth = GetUserAuthByUserName(userName);
            if (userAuth == null)
            {
                return false;
            }
            var lookupUserName = userName.Trim().ToLower();
            var user = userManager.Users
                .SingleOrDefault(x => x.UserName == lookupUserName);
            if (user == null)
            {
                throw new AuthenticationException(ErrorMessages.UserNotExists);
            }
            var passwordCheckResult = userManager.CheckPasswordAsync(user, password).Result;
            if (!passwordCheckResult)
            {
                return false;
            }
            var mfaCheckResult = !mfaEnabled
                || userManager.VerifyTwoFactorTokenAsync(
                        user.ID,
                        // TODO: Make a setting that swiches between email and phone (per user?)
                        CEFUserManager.DataProtectionProviderNameForEmail,
                        mfaToken)
                    .Result;
            return passwordCheckResult && mfaCheckResult;
        }

        /// <summary>Creates or merge authentication session.</summary>
        /// <param name="authSession">The authentication session.</param>
        /// <param name="tokens">     The tokens.</param>
        /// <returns>The new or merge authentication session.</returns>
        public IUserAuthDetails CreateOrMergeAuthSession(IAuthSession authSession, IAuthTokens tokens)
        {
            var userAuth = GetUserAuth(authSession, tokens) ?? new UserAuth();
            return RunWrappedInUserManager(userManager =>
            {
                var tokenUserId = int.Parse(tokens.UserId);
                var user = userManager.Users
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                    .SingleOrDefault(x => x.ID == tokenUserId);
                var authDetails = user == null
                    ? new UserAuthDetails { Provider = tokens.Provider, UserId = tokens.UserId }
                    : MapUserToUserAuthDetails(user);
                authDetails.PopulateMissing(tokens);
                userAuth.PopulateMissingExtended(authDetails);
                userAuth.ModifiedDate = DateTime.UtcNow;
                authDetails.UserAuthId = userAuth.Id;
                authDetails.ModifiedDate = userAuth.ModifiedDate;
                return authDetails;
            });
        }

        /// <summary>Gets user authentication details.</summary>
        /// <param name="userAuthId">Identifier for the user authentication.</param>
        /// <returns>The user authentication details.</returns>
        public List<IUserAuthDetails> GetUserAuthDetails(string userAuthId)
        {
            return RunWrappedInUserManager(userManager =>
            {
                var authId = int.Parse(userAuthId);
                var user = userManager.Users
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                    .SingleOrDefault(x => x.ID == authId);
                return user == null
                    ? new()
                    : new List<IUserAuthDetails> { MapUserToUserAuthDetails(user) };
            });
        }

        /// <summary>Deletes the user authentication described by userAuthId.</summary>
        /// <param name="userAuthId">Identifier for the user authentication.</param>
        public void DeleteUserAuth(string userAuthId)
        {
            RunWrappedInUserManager(userManager =>
            {
                var authId = int.Parse(userAuthId);
                var user = userManager.Users
                    ////.Include(x => x.Roles)
                    ////.Include(x => x.Roles.Select(y => y.Role))
                    ////.Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    ////.Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    ////.Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                    .SingleOrDefault(x => x.ID == authId);
                if (user != null)
                {
                    userManager.DeleteAsync(user).Wait(10_000);
                }
            });
        }

        /// <summary>Creates user authentication.</summary>
        /// <param name="newUser"> The new user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The new user authentication.</returns>
        public IUserAuth CreateUserAuth(IUserAuth newUser, string password)
        {
            ValidateNewUser(newUser, password);
            AssertNoExistingUser(newUser);
            return RunWrappedInUserManager(userManager =>
            {
                var user = MapAuthUserToUser(newUser);
                var result = userManager.CreateAsync(user, password).Result;
                if (!result.Succeeded)
                {
                    // TODO: Log the error and the inputs that caused it
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                return MapUserToUserAuth(user);
            });
        }

        /// <summary>Updates the user authentication.</summary>
        /// <param name="existingUser">The existing user.</param>
        /// <param name="newUser">     The new user.</param>
        /// <returns>An IUserAuth.</returns>
        public IUserAuth UpdateUserAuth(IUserAuth existingUser, IUserAuth newUser)
        {
            throw new NotImplementedException();
        }

        /// <summary>Updates the user authentication.</summary>
        /// <param name="existingUser">The existing user.</param>
        /// <param name="newUser">     The new user.</param>
        /// <param name="password">    The password.</param>
        /// <returns>An IUserAuth.</returns>
        public IUserAuth UpdateUserAuth(IUserAuth existingUser, IUserAuth newUser, string password)
        {
            ValidateNewUser(newUser, password);
            AssertNoExistingUser(newUser, existingUser);
            return RunWrappedInUserManager(userManager =>
            {
                var user = MapAuthUserToUser(newUser);
                var result = userManager.UpdateAsync(user).Result;
                if (!result.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(AspNetIdentityUserAuthRepository)}.{nameof(UpdateUserAuth)}",
                        result.Errors.Aggregate((c, n) => $"{c}\r\n{n}"),
                        null);
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                var result2 = userManager.RemovePasswordAsync(user.ID).Result;
                if (!result2.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(AspNetIdentityUserAuthRepository)}.{nameof(UpdateUserAuth)}",
                        result2.Errors.Aggregate((c, n) => $"{c}\r\n{n}"),
                        null);
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                var result3 = userManager.AddPasswordAsync(user.ID, password).Result;
                if (!result3.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(AspNetIdentityUserAuthRepository)}.{nameof(UpdateUserAuth)}",
                        result3.Errors.Aggregate((c, n) => $"{c}\r\n{n}"),
                        null);
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                return MapUserToUserAuth(user);
            });
        }

        /// <summary>Saves a user authentication.</summary>
        /// <param name="userAuth">The user authentication.</param>
        public void SaveUserAuth(IUserAuth userAuth)
        {
            RunWrappedInUserManager(userManager =>
            {
                var lookupUsername = userAuth.UserName.Trim().ToLower();
                var user = userManager.Users
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                    .FilterByActive(true)
                    .Single(x => x.UserName == lookupUsername);
                user = MapAuthUserToUser(userAuth, user);
                var result = userManager.UpdateAsync(user).Result;
                if (!result.Succeeded)
                {
                    throw new ArgumentException(result.Errors.First(), nameof(userAuth));
                }
            });
        }

        /// <summary>Saves a user authentication.</summary>
        /// <param name="authSession">The authentication session.</param>
        public void SaveUserAuth(IAuthSession authSession)
        {
            RunWrappedInUserManager(userManager =>
            {
                var lookupUsernameAuthSess = authSession.UserAuthId.Trim().ToLower();
                var userAuth = !string.IsNullOrEmpty(authSession.UserAuthId)
                    ? MapUserToUserAuth(userManager.Users
                        .Include(x => x.Roles)
                        .Include(x => x.Roles.Select(y => y.Role))
                        .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                        .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                        .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                        .FilterByActive(true)
                        .Single(x => x.UserName == lookupUsernameAuthSess))
                    : authSession.ConvertTo<UserAuth>();
                var lookupUsername = userAuth.UserName.Trim().ToLower();
                var user = userManager.Users
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                    .FilterByActive(true)
                    .Single(x => x.UserName == lookupUsername);
                user = MapAuthUserToUser(userAuth, user);
                var result = userManager.UpdateAsync(user).Result;
                if (!result.Succeeded)
                {
                    throw new ArgumentException(result.Errors.First());
                }
            });
        }

        /// <summary>Map user to user authentication.</summary>
        /// <param name="user">The user.</param>
        /// <returns>An IUserAuth.</returns>
        private static IUserAuth MapUserToUserAuth(User user)
        {
            var fullName = $"{user.Contact?.FirstName?.Trim()} {user.Contact?.LastName?.Trim()}".Trim();
            var timestamp = DateExtensions.GenDateTime;
            IUserAuth userAuth = new UserAuth
            {
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.UpdatedDate ?? user.CreatedDate,
                Address = null,
                Address2 = null,
                City = null,
                State = null,
                PostalCode = null,
                Country = null,
                BirthDate = null,
                BirthDateRaw = null,
                DisplayName = !string.IsNullOrWhiteSpace(user.DisplayName)
                    ? user.DisplayName
                    : !string.IsNullOrWhiteSpace(fullName)
                        ? fullName
                        : user.UserName,
                Email = user.Email,
                FirstName = null,
                LastName = null,
                FullName = null,
                InvalidLoginAttempts = user.AccessFailedCount,
                LastLoginAttempt = null,
                LockedDate = user.LockoutEndDateUtc,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PrimaryEmail = user.Email,
                Roles = user.Roles?.Where(x => (x.StartDate == null || x.StartDate < timestamp) && (x.EndDate == null || x.EndDate > timestamp)).Select(x => x.Role!.Name).ToList(),
                Permissions = user.Roles?.Where(x => (x.StartDate == null || x.StartDate < timestamp) && (x.EndDate == null || x.EndDate > timestamp)).SelectMany(x => x.Role?.Permissions?.Select(y => y.Permission!.Name).Distinct()!).ToList(),
                UserName = user.UserName,
                Id = user.ID,
                Company = user.Account?.Name,
            };
            if (user.Account?.AccountUserRoles!.Count(x => (x.StartDate == null || x.StartDate < timestamp) && (x.EndDate == null || x.EndDate > timestamp)) > 0)
            {
                userAuth.Roles.AddRange(user.Account.AccountUserRoles!.Where(x => (x.StartDate == null || x.StartDate < timestamp) && (x.EndDate == null || x.EndDate > timestamp)).Select(x => x.Slave!.Name));
                userAuth.Permissions.AddRange(user.Account.AccountUserRoles!.Where(x => (x.StartDate == null || x.StartDate < timestamp) && (x.EndDate == null || x.EndDate > timestamp)).SelectMany(x => x.Slave!.Permissions.Select(y => y.Permission!.Name).Distinct()));
            }
            if (user.Contact == null)
            {
                return userAuth;
            }
            userAuth.FirstName = user.Contact.FirstName;
            userAuth.LastName = user.Contact.LastName;
            userAuth.FullName = fullName;
            if (user.Contact.Address == null)
            {
                return userAuth;
            }
            userAuth.Address = user.Contact.Address.Street1;
            userAuth.Address2 = user.Contact.Address.Street2;
            userAuth.City = user.Contact.Address.City;
            userAuth.State = user.Contact.Address.Region?.Name ?? user.Contact.Address.RegionCustom;
            userAuth.PostalCode = user.Contact.Address.PostalCode;
            userAuth.Country = user.Contact.Address.Country?.Name ?? user.Contact.Address.CountryCustom;
            return userAuth;
        }

        /// <summary>Map user to user authentication details.</summary>
        /// <param name="user">The user.</param>
        /// <returns>The IUserAuthDetails.</returns>
        private static IUserAuthDetails MapUserToUserAuthDetails(IUser user)
        {
            var fullName = $"{user.Contact?.FirstName?.Trim()} {user.Contact?.LastName?.Trim()}".Trim();
            IUserAuthDetails userAuthDetails = new UserAuthDetails
            {
                CreatedDate = user.CreatedDate,
                ModifiedDate = user.UpdatedDate ?? user.CreatedDate,
                Address = null,
                Address2 = null,
                City = null,
                State = null,
                PostalCode = null,
                Country = null,
                BirthDate = null,
                BirthDateRaw = null,
                DisplayName = !string.IsNullOrWhiteSpace(user.DisplayName)
                    ? user.DisplayName
                    : !string.IsNullOrWhiteSpace(fullName)
                        ? fullName
                        : user.UserName,
                Email = user.Email,
                FirstName = null,
                LastName = null,
                FullName = null,
                ////InvalidLoginAttempts = user.AccessFailedCount,
                ////LastLoginAttempt = null,
                ////LockedDate = user.LockoutEndDateUtc,
                ////PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                ////PrimaryEmail = user.Email,
                ////Roles = user.Roles?.Select(x => x.Role.Name).ToList(),
                UserName = user.UserName,
                Id = user.ID,
                Company = user.Account?.Name,
                UserId = user.ID.ToString(),
                Provider = AspNetIdentityAuthProvider.StaticName,
                UserAuthId = user.ID,
            };
            if (user.Contact == null)
            {
                return userAuthDetails;
            }
            userAuthDetails.FirstName = user.Contact.FirstName;
            userAuthDetails.LastName = user.Contact.LastName;
            userAuthDetails.FullName = fullName;
            if (user.Contact?.Address == null)
            {
                return userAuthDetails;
            }
            userAuthDetails.Address = user.Contact.Address.Street1;
            userAuthDetails.Address2 = user.Contact.Address.Street2;
            userAuthDetails.City = user.Contact.Address.City;
            userAuthDetails.State = user.Contact.Address.Region?.Name ?? user.Contact.Address.RegionCustom;
            userAuthDetails.PostalCode = user.Contact.Address.PostalCode;
            userAuthDetails.Country = user.Contact.Address.Country?.Name ?? user.Contact.Address.CountryCustom;
            return userAuthDetails;
        }

        /// <summary>Map authentication user to user.</summary>
        /// <param name="userAuth">    The user authentication.</param>
        /// <param name="existingUser">The existing user.</param>
        /// <returns>A ServiceStack.Authentication.ClarityUserAuthRepository.DotNetNukeContext.User.</returns>
        private static User MapAuthUserToUser(IUserAuth userAuth, User? existingUser = null)
        {
            var timestamp = DateExtensions.GenDateTime;
            if (existingUser != null)
            {
                existingUser.CustomKey = userAuth.UserName;
                existingUser.UserName = userAuth.UserName;
                existingUser.Email = userAuth.PrimaryEmail;
                existingUser.UpdatedDate = timestamp;
                existingUser.DisplayName = userAuth.DisplayName;
                existingUser.PhoneNumber = userAuth.PhoneNumber;
                existingUser.Contact!.CustomKey = userAuth.UserName + ":Contact";
                existingUser.Contact.UpdatedDate = timestamp;
                existingUser.Contact.Email1 = userAuth.PrimaryEmail;
                existingUser.Contact.Phone1 = userAuth.PhoneNumber;
                existingUser.Contact.FirstName = userAuth.FirstName;
                existingUser.Contact.LastName = userAuth.LastName;
                existingUser.Contact.FullName = userAuth.FullName ?? $"{userAuth.FirstName?.Trim()} {userAuth.LastName?.Trim()}".Trim();
                existingUser.Contact.Address!.CustomKey = userAuth.UserName + ":Contact:Address";
                existingUser.Contact.Address.UpdatedDate = timestamp;
                existingUser.Contact.Address.Street1 = userAuth.Address;
                existingUser.Contact.Address.Street2 = userAuth.Address2;
                existingUser.Contact.Address.City = userAuth.City;
                existingUser.Contact.Address.RegionCustom = userAuth.State; // TODO: Do lookup
                existingUser.Contact.Address.CountryCustom = userAuth.Country; // TODO: Do lookup
                existingUser.Contact.Address.PostalCode = userAuth.PostalCode;
                return existingUser;
            }
            return new()
            {
                CustomKey = userAuth.UserName,
                UserName = userAuth.UserName,
                Email = userAuth.PrimaryEmail,
                CreatedDate = timestamp,
                Active = true,
                DisplayName = userAuth.DisplayName,
                PhoneNumber = userAuth.PhoneNumber,
                StatusID = 1, // TODO: Do Lookup
                TypeID = 1, // TODO: Do Lookup
                Contact = new()
                {
                    CustomKey = userAuth.UserName + ":Contact",
                    CreatedDate = timestamp,
                    Active = true,
                    TypeID = 3, // TODO: Do lookup
                    Email1 = userAuth.PrimaryEmail,
                    Phone1 = userAuth.PhoneNumber,
                    FirstName = userAuth.FirstName,
                    LastName = userAuth.LastName,
                    FullName = userAuth.FullName ?? $"{userAuth.FirstName?.Trim()} {userAuth.LastName?.Trim()}".Trim(),
                    Address = new()
                    {
                        CustomKey = userAuth.UserName + ":Contact:Address",
                        CreatedDate = timestamp,
                        Active = true,
                        Street1 = userAuth.Address,
                        Street2 = userAuth.Address2,
                        City = userAuth.City,
                        RegionCustom = userAuth.State, // TODO: Do lookup
                        CountryCustom = userAuth.Country, // TODO: Do lookup
                        PostalCode = userAuth.PostalCode,
                    },
                },
            };
        }

        /// <summary>Executes the wrapped in user manager operation.</summary>
        /// <param name="action">The action.</param>
        private static void RunWrappedInUserManager(Action<CEFUserManager> action)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            action(userManager);
        }

        /// <summary>Executes the wrapped in user manager operation.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="func">The function.</param>
        /// <returns>A T.</returns>
        private static T RunWrappedInUserManager<T>(Func<CEFUserManager, T> func)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            return func(userManager);
        }
        /// <summary>Validates the new user.</summary>
        /// <param name="newUser"> The new user.</param>
        /// <param name="password">The password.</param>
        private static void ValidateNewUser(IUserAuth newUser, string password)
        {
            RunWrappedInUserManager(userManager =>
            {
                userManager.UserValidator.ValidateAsync(MapAuthUserToUser(newUser));
                userManager.PasswordValidator.ValidateAsync(password);
            });
        }

        /// <summary>Loads user authentication.</summary>
        /// <param name="session"> The session.</param>
        /// <param name="userAuth">The user authentication.</param>
        private void LoadUserAuth(IAuthSession session, IUserAuth? userAuth)
        {
            session.PopulateSession(
                userAuth,
#if NET5_0_OR_GREATER
                this);
#else
                GetUserAuthDetails(session.UserAuthId).ConvertAll(x => (IAuthTokens)x));
#endif
        }

        /// <summary>Assert no existing user.</summary>
        /// <exception cref="ArgumentException">Thrown when one or more arguments have unsupported or
        ///                                     illegal values.</exception>
        /// <param name="newUser">              The new user.</param>
        /// <param name="exceptForExistingUser">The except for existing user.</param>
        private void AssertNoExistingUser(IUserAuth newUser, IUserAuth? exceptForExistingUser = null)
        {
            if (newUser.UserName != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.UserName);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                {
                    throw new ArgumentException($"User {newUser.UserName} already exists");
                }
            }
            if (newUser.Email != null)
            {
                var existingUser = GetUserAuthByUserName(newUser.Email);
                if (existingUser != null
                    && (exceptForExistingUser == null || existingUser.Id != exceptForExistingUser.Id))
                {
                    throw new ArgumentException($"Email {newUser.Email} already exists");
                }
            }
        }
    }
}
