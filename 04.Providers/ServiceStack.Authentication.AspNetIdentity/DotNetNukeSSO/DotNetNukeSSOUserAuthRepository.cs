// <copyright file="DotNetNukeSSOUserAuthRepository.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the DotNetNuke SSO user authentication repository class</summary>
// ReSharper disable AsyncConverter.AsyncWait
namespace ServiceStack.Auth
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Encryption;
    using Clarity.Ecommerce.Interfaces.Models;
    using Clarity.Ecommerce.Interfaces.Workflow;
    using Clarity.Ecommerce.Models;
    using Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9;
    using Clarity.Ecommerce.Utilities;
    using JetBrains.Annotations;
    using Microsoft.AspNet.Identity;
    using User = Clarity.Ecommerce.DataModel.User;

    /// <summary>A DotNetNuke SSO user authentication repository.</summary>
    /// <remarks>Based on
    /// https://github.com/ServiceStack/ServiceStack/blob/v4.0.40/src/ServiceStack.Authentication.NHibernate/NHibernateUserAuthRepository.cs .
    /// </remarks>
    /// <seealso cref="ICEFUserAuthRepository"/>
    [PublicAPI]
    public class DotNetNukeSSOUserAuthRepository : ICEFUserAuthRepository
    {
        // NOTE: This isn't used yet, but would be forward compatible when 10 comes out, just by adding 10
        private static int? version;

        private static int Version
        {
            get
            {
                if (version.HasValue)
                {
                    return version.Value;
                }
                if (!int.TryParse(ConfigurationManager.AppSettings["Clarity.DotNetNukeSSO.Version"] ?? "9", out var i))
                {
                    throw new ConfigurationErrorsException("Cannot parse the DotNetNukeSSO.Version to a number");
                }
                if (i != 9)
                {
                    throw new ConfigurationErrorsException(
                        "Invalid configuration for DotNetNukeSSO.Version. The only accepted value is 9");
                }
                return (version = i).Value;
            }
        }

        private static ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Loads user authentication.</summary>
        /// <param name="session">The session.</param>
        /// <param name="tokens"> The tokens.</param>
        public void LoadUserAuth(IAuthSession session, IAuthTokens tokens)
        {
            session.ThrowIfNull(nameof(session));
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
            if (string.IsNullOrEmpty(tokens?.Provider) || string.IsNullOrEmpty(tokens?.UserId))
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
            if (!TryAuthenticate(userName, password, out IUserAuth? userAuth))
            {
                return false;
            }
            userId = userAuth?.Id.ToString(CultureInfo.InvariantCulture);
            return true;
        }

        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="digestHeaders">The digest headers.</param>
        /// <param name="privateKey">   The private key.</param>
        /// <param name="nonceTimeOut"> The nonce time out.</param>
        /// <param name="sequence">     The sequence.</param>
        /// <param name="userAuth">     The user authentication.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public bool TryAuthenticate(
            Dictionary<string, string> digestHeaders,
            string privateKey,
            int nonceTimeOut,
            string sequence,
            out IUserAuth? userAuth)
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
            // Validate a username and token (password) was sent
            if (!Contract.CheckAllValidKeys(userName, password))
            {
                throw new AuthenticationException(ErrorMessages.InvalidUsernameOrPassword);
            }
            // Validate the token decrypts
#pragma warning disable 618
            var decryptedToken = CMSApiEncoder.Decrypt(password);
#pragma warning restore 618
            if (!Contract.CheckValidKey(decryptedToken))
            {
                throw new AuthenticationException(ErrorMessages.TokenInvalid);
            }
            // Validate the token hasn't expired
            var data = Convert.FromBase64String(decryptedToken);
            var created = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            var isExpired = created < DateTime.UtcNow.AddMinutes(-30);
            if (isExpired)
            {
                throw new AuthenticationException(ErrorMessages.TokenExpired);
            }
            // Validate the token has 3 parts
            var decoded = data.GetUTF8DecodedString();
            var split = decoded?.Split('|');
            if (split?.Length < 3)
            {
                throw new AuthenticationException(ErrorMessages.TokenInvalid);
            }
            // Validate the username inside the token matches the username sent
            if (!split![^1].Equals(userName, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AuthenticationException(ErrorMessages.TokenInvalid);
            }
            // NOTE: Can't use RunWrapped because of the out param
            // Get the Repository of Users
            using var context = RegistryLoaderWrapper.GetContext(null);
            using var userStore = new CEFUserStore(context);
            using var userManager = new UserManager<User, int>(userStore);
            // See if we already have the user locally
            userAuth = GetUserAuthByUserName(userName);
            if (userAuth != null)
            {
                return true;
            }
            // Make sure the user exists in DNN and pull it
            using var dnnContext = GetDNNContext();
            var dnnUser = GetDNNUserByUserName(dnnContext, userName);
            if (dnnUser == null)
            {
                throw new AuthenticationException(ErrorMessages.UserNotExists);
            }
            var task = UpsertDNNUserToCEFAsync(dnnUser);
            task.Wait(10_000);
            var upsertResult = task.Result;
            if (!upsertResult.ActionSucceeded)
            {
                return false;
            }
            var userID = userManager.Users
                .FilterByActive(true)
                .FilterUsersByUserName(userName, true)
                .Select(x => (int?)x.ID)
                .SingleOrDefault();
            if (userID == null)
            {
                // TODO: Pull the user from DNN to CEF, throw if we can't find it in DNN
                throw new AuthenticationException(ErrorMessages.UserNotExists);
            }
            // Success
            return true;
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
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
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
                var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
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
            throw new InvalidOperationException();
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
                var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(DotNetNukeSSOUserAuthRepository)}.{nameof(UpdateUserAuth)}",
                        result.Errors.Aggregate((c, n) => $"{c}\r\n{n}"),
                        null);
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                var result2 = userManager.RemovePasswordAsync(user.ID).GetAwaiter().GetResult();
                if (!result2.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(DotNetNukeSSOUserAuthRepository)}.{nameof(UpdateUserAuth)}",
                        result2.Errors.Aggregate((c, n) => $"{c}\r\n{n}"),
                        null);
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                var result3 = userManager.AddPasswordAsync(user.ID, password).GetAwaiter().GetResult();
                if (!result3.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(DotNetNukeSSOUserAuthRepository)}.{nameof(UpdateUserAuth)}",
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
                var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();
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
                var search = authSession.UserAuthId.Trim().ToLower();
                var userAuth = !string.IsNullOrEmpty(authSession.UserAuthId)
                    ? MapUserToUserAuth(userManager.Users
                        .Include(x => x.Roles)
                        .Include(x => x.Roles.Select(y => y.Role))
                        .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                        .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                        .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                        .FilterByActive(true)
                        .Single(x => x.UserName == search))
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
                var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    throw new ArgumentException(result.Errors.First());
                }
            });
        }

        /// <summary>Validates the new user.</summary>
        /// <param name="newUser"> The new user.</param>
        /// <param name="password">The password.</param>
#pragma warning disable IDE0060, RCS1163 // Remove unused parameter
        private static void ValidateNewUser(IUserAuth newUser, string password)
#pragma warning restore IDE0060, RCS1163 // Remove unused parameter
        {
            // Do Nothing, DNN is managing if it's valid or not (a good username/password)
        }

        private static void RunWrappedInUserManager(Action<CEFUserManager> action)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            action(userManager);
        }

        private static T RunWrappedInUserManager<T>(Func<CEFUserManager, T> func)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            using var userStore = new CEFUserStore(context);
            using var userManager = new CEFUserManager(userStore);
            return func(userManager);
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
                Permissions = user.Roles?.Where(x => (x.StartDate == null || x.StartDate < timestamp) && (x.EndDate == null || x.EndDate > timestamp)).SelectMany(x => x.Role!.Permissions!.Select(y => y.Permission!.Name).Distinct()).ToList(),
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
        private static IUserAuthDetails MapUserToUserAuthDetails(Clarity.Ecommerce.Interfaces.DataModel.IUser user)
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
                ////PrimaryEmail = user.Email,
                ////Roles = user.Roles?.Select(x => x.Role.Name).ToList(),
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Id = user.ID,
                Company = user.Account?.Name,
                UserId = user.ID.ToString(),
                Provider = DotNetNukeSSOAuthProvider.StaticName,
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
                if (existingUser.Contact is null)
                {
                    return existingUser;
                }
                existingUser.Contact.CustomKey = userAuth.UserName + ":Contact";
                existingUser.Contact.UpdatedDate = timestamp;
                existingUser.Contact.Email1 = userAuth.PrimaryEmail;
                existingUser.Contact.Phone1 = userAuth.PhoneNumber;
                existingUser.Contact.FirstName = userAuth.FirstName;
                existingUser.Contact.LastName = userAuth.LastName;
                existingUser.Contact.FullName = userAuth.FullName ?? $"{userAuth.FirstName?.Trim()} {userAuth.LastName?.Trim()}".Trim();
                if (existingUser.Contact.Address is null)
                {
                    return existingUser;
                }
                existingUser.Contact.Address.CustomKey = userAuth.UserName + ":Contact:Address";
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

        private static Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User? GetDNNUserByEmail(DotNetNuke9Entities dnnContext, string email)
        {
            return GetDNNUserByUserNameOrEmail(dnnContext, null, email);
        }

        private static Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User? GetDNNUserByUserName(DotNetNuke9Entities dnnContext, string userName)
        {
            return GetDNNUserByUserNameOrEmail(dnnContext, userName, null);
        }

        private static Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User? GetDNNUserByUserNameOrEmail(DotNetNuke9Entities dnnContext, string? userName, string? email)
        {
            var now = DateExtensions.GenDateTime;
            return dnnContext.Users
                .AsNoTracking()
                .Include(x => x.UserProfiles)
                .Include(x => x.UserProfiles.Select(y => y.ProfilePropertyDefinition))
                .Include(x => x.UserRoles)
                .Include(x => x.UserRoles.Select(y => y.Role))
                .Where(x => userName != null && x.Username.Equals(userName, StringComparison.OrdinalIgnoreCase)
                    || email != null && x.Email.Equals(email.ToLower(), StringComparison.OrdinalIgnoreCase))
                .Select(x => new
                {
                    x.UserID,
                    x.Username,
                    x.FirstName,
                    x.LastName,
                    x.Email,
                    x.DisplayName,
                    x.IsDeleted,
                    UserRoles = x.UserRoles
                        .Where(y => (y.ExpiryDate == null || y.ExpiryDate > now)
                            && (y.EffectiveDate == null || y.EffectiveDate < now))
                        .Select(y => new
                        {
                            y.RoleID,
                            Role = new { y.Role.RoleName },
                        })
                        .ToList(),
                    UserProfiles = x.UserProfiles
                        .Where(y => !y.ProfilePropertyDefinition.Deleted)
                        .Select(y => new
                        {
                            ProfilePropertyDefinition = new
                            {
                                y.ProfilePropertyDefinition.PropertyCategory,
                                y.ProfilePropertyDefinition.PropertyName,
                            },
                            y.PropertyValue,
                        })
                        .ToList(),
                })
                .ToList()
                .Select(x => new Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User
                {
                    UserID = x.UserID,
                    Username = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    DisplayName = x.DisplayName,
                    IsDeleted = x.IsDeleted,
                    UserRoles = x.UserRoles
                        .ConvertAll(y => new Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.UserRole
                        {
                            RoleID = y.RoleID,
                            Role = new() { RoleName = y.Role.RoleName },
                        }),
                    UserProfiles = x.UserProfiles
                        .ConvertAll(y => new UserProfile
                        {
                            ProfilePropertyDefinition = new()
                            {
                                PropertyCategory = y.ProfilePropertyDefinition.PropertyCategory,
                                PropertyName = y.ProfilePropertyDefinition.PropertyName,
                            },
                            PropertyValue = y.PropertyValue,
                        }),
                })
                .SingleOrDefault();
        }

        private static DotNetNuke9Entities GetDNNContext()
        {
            switch (Version)
            {
                ////case 8: { return new DNN8.DotNetNuke8Entities(); }
                case 9:
                {
                    return new();
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(Version));
                }
            }
        }

        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        private static bool UpsertDNNUserToCEFInner(Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User dnnUser, IUserModel? cefUser)
        {
            if (cefUser is null)
            {
                return false;
            }
            var changed = false;
            if (cefUser.UserName != dnnUser.Username)
            {
                cefUser.UserName = dnnUser.Username;
                changed = true;
            }
            if (cefUser.Email != dnnUser.Email)
            {
                cefUser.Email = dnnUser.Email;
                changed = true;
            }
            if (cefUser.DisplayName != dnnUser.DisplayName)
            {
                cefUser.DisplayName = dnnUser.DisplayName;
                changed = true;
            }
            if (cefUser.IsDeleted != dnnUser.IsDeleted)
            {
                cefUser.IsDeleted = dnnUser.IsDeleted;
                changed = true;
            }
            foreach (var profileValue in dnnUser.UserProfiles)
            {
                if (!Contract.CheckValidKey(profileValue.PropertyValue))
                {
                    // Don't assign blanks
                    continue;
                }
                cefUser.Contact ??= RegistryLoaderWrapper.GetInstance<IContactModel>();
                cefUser.Contact.Address ??= RegistryLoaderWrapper.GetInstance<IAddressModel>();
                switch (profileValue.ProfilePropertyDefinition.PropertyCategory
                        + " "
                        + profileValue.ProfilePropertyDefinition.PropertyName)
                {
                    case "Basic FirstName":
                    {
                        if (cefUser.Contact.FirstName != profileValue.PropertyValue)
                        {
                            cefUser.Contact.FirstName = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Basic MiddleName":
                    {
                        if (cefUser.Contact.MiddleName != profileValue.PropertyValue)
                        {
                            cefUser.Contact.MiddleName = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Basic LastName":
                    {
                        if (cefUser.Contact.LastName != profileValue.PropertyValue)
                        {
                            cefUser.Contact.LastName = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Contact Telephone":
                    {
                        if (cefUser.Contact.Phone1 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Phone1 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Contact Cell":
                    {
                        if (cefUser.Contact.Phone2 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Phone2 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Contact Fax":
                    {
                        if (cefUser.Contact.Fax1 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Fax1 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Street":
                    {
                        if (cefUser.Contact.Address.Street1 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.Street1 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Unit":
                    {
                        if (cefUser.Contact.Address.Street2 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.Street2 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location City":
                    {
                        if (cefUser.Contact.Address.City != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.City = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Region":
                    {
                        if (cefUser.Contact.Address.RegionCode != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.RegionCode = profileValue.PropertyValue;
                            cefUser.Contact.Address.Region = null;
                            cefUser.Contact.Address.RegionID = null;
                            cefUser.Contact.Address.RegionKey
                                = cefUser.Contact.Address.RegionName
                                    = null;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Country":
                    {
                        if (cefUser.Contact.Address.CountryCode != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.CountryCode = profileValue.PropertyValue;
                            cefUser.Contact.Address.CountryCode = profileValue.PropertyValue;
                            cefUser.Contact.Address.Country = null;
                            cefUser.Contact.Address.CountryID = null;
                            cefUser.Contact.Address.CountryKey
                                = cefUser.Contact.Address.CountryName
                                    = null;
                            changed = true;
                        }
                        break;
                    }
                    case "Location PostalCode":
                    {
                        if (cefUser.Contact.Address.PostalCode != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.PostalCode = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    default:
                    {
                        // Don't assign anything else
                        continue;
                    }
                }
            }
            return changed;
        }

        private static async Task<CEFActionResponse> UpsertDNNUserToCEFAsync(Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User dnnUser)
        {
            var userWorkflow = RegistryLoaderWrapper.GetInstance<IUserWorkflow>();
            var existingID = await userWorkflow.CheckExistsAsync(dnnUser.Username, contextProfileName: null).ConfigureAwait(false);
            int cefUserModel;
            if (Contract.CheckValidID(existingID))
            {
                var cefUser = await userWorkflow.GetAsync(existingID!.Value, contextProfileName: null).ConfigureAwait(false);
                if (!UpsertDNNUserToCEFInner(dnnUser, cefUser))
                {
                    return CEFAR.FailingCEFAR("ERROR: Unable to read user");
                }
                cefUserModel = Contract.RequiresValidID(
                    (await userWorkflow.UpdateAsync(cefUser!, contextProfileName: null).ConfigureAwait(false)).Result);
            }
            else
            {
                var cefUser = RegistryLoaderWrapper.GetInstance<IUserModel>();
                cefUser.Contact = RegistryLoaderWrapper.GetInstance<IContactModel>();
                cefUser.Contact.Address = RegistryLoaderWrapper.GetInstance<IAddressModel>();
                if (!UpsertDNNUserToCEFInner(dnnUser, cefUser))
                {
                    return CEFAR.FailingCEFAR("ERROR: Unable to read user");
                }
                cefUserModel = Contract.RequiresValidID(
                    (await userWorkflow.CreateAsync(cefUser, contextProfileName: null).ConfigureAwait(false)).Result);
            }
            var authenticationWorkflow = RegistryLoaderWrapper.GetInstance<IAuthenticationWorkflow>();
            var cefRoles = await authenticationWorkflow.GetRolesAsync(null).ConfigureAwait(false);
            var userDNNRoles = dnnUser.UserRoles.Select(x => x.Role.RoleName).ToList();
            var added = false;
            foreach (var dnnRole in userDNNRoles.Where(x => !cefRoles.ContainsKey(x)))
            {
                var createRoleResult = await authenticationWorkflow.CreateRoleAsync(dnnRole, new(), null).ConfigureAwait(false);
                if (!createRoleResult.ActionSucceeded)
                {
                    return createRoleResult;
                }
                added = true;
            }
            if (added)
            {
                cefRoles = await authenticationWorkflow.GetRolesAsync(null).ConfigureAwait(false);
            }
            var userCEFRoles = await authenticationWorkflow.GetRolesForUserAsync(cefUserModel, null).ConfigureAwait(false);
            var removed = false;
            foreach (var userCEFRole in userCEFRoles.Where(x => !userDNNRoles.Contains(x.Name!)))
            {
                var removeResult = await authenticationWorkflow.RemoveRoleFromUserAsync(userCEFRole, null).ConfigureAwait(false);
                if (!removeResult.ActionSucceeded)
                {
                    return removeResult;
                }
                removed = true;
            }
            if (removed)
            {
                userCEFRoles = await authenticationWorkflow.GetRolesForUserAsync(cefUserModel, null).ConfigureAwait(false);
            }
            foreach (var userDNNRole in userDNNRoles.Where(x => !userCEFRoles.Select(y => y.Name).Contains(x)))
            {
                var roleAssignment = RegistryLoaderWrapper.GetInstance<IRoleForUserModel>();
                roleAssignment.Name = userDNNRole;
                roleAssignment.RoleId = cefRoles.Single(x => x.Key == userDNNRole).Value;
                roleAssignment.UserId = cefUserModel;
                var assignResult = await authenticationWorkflow.AssignRoleToUserAsync(roleAssignment, null).ConfigureAwait(false);
                if (!assignResult.ActionSucceeded)
                {
                    return assignResult;
                }
            }
            return CEFAR.PassingCEFAR();
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
                AssertNoExistingUserByUserName(newUser, exceptForExistingUser);
            }
            if (newUser.Email != null)
            {
                AssertNoExistingUserByEmail(newUser, exceptForExistingUser);
            }
        }

        private void AssertNoExistingUserByEmail(IUserAuth newUser, IUserAuth? exceptForExistingUser)
        {
            using var dnnContext = GetDNNContext();
            var dnnUser = GetDNNUserByEmail(dnnContext, newUser.Email);
            if (dnnUser == null)
            {
                return;
            }
            var task = UpsertDNNUserToCEFAsync(dnnUser);
            task.Wait(10_000);
            var existingUser = GetUserAuthByUserName(newUser.Email);
            if (existingUser == null
                || exceptForExistingUser != null && existingUser.Id == exceptForExistingUser.Id)
            {
                return;
            }
            throw new ArgumentException($"Email {newUser.Email} already exists");
        }

        private void AssertNoExistingUserByUserName(IUserAuth newUser, IUserAuth? exceptForExistingUser)
        {
            using var dnnContext = GetDNNContext();
            var dnnUser = GetDNNUserByUserName(dnnContext, newUser.UserName);
            if (dnnUser == null)
            {
                return;
            }
            var task = UpsertDNNUserToCEFAsync(dnnUser);
            task.Wait(10_000);
            var existingUser = GetUserAuthByUserName(newUser.UserName);
            if (existingUser == null
                || exceptForExistingUser != null && existingUser.Id == exceptForExistingUser.Id)
            {
                return;
            }
            throw new ArgumentException($"User {newUser.UserName} already exists");
        }
    }

    /// <summary>A DotNetNuke SSO user authentication repository.</summary>
    /// <remarks>Based on
    /// https://github.com/ServiceStack/ServiceStack/blob/v4.0.40/src/ServiceStack.Authentication.NHibernate/NHibernateUserAuthRepository.cs .
    /// </remarks>
    /// <seealso cref="ICEFUserAuthRepository"/>
    [PublicAPI]
    public class DotNetNukeSSOUserAuthRepository2 : ICEFUserAuthRepository
    {
        // NOTE: This isn't used yet, but would be forward compatible when 10 comes out, just by adding 10
        private static int? version;

        private static int Version
        {
            get
            {
                if (version.HasValue)
                {
                    return version.Value;
                }
                if (!int.TryParse(ConfigurationManager.AppSettings["Clarity.DotNetNukeSSO.Version"] ?? "9", out var i))
                {
                    throw new ConfigurationErrorsException("Cannot parse the DotNetNukeSSO.Version to a number");
                }
                if (i != 9)
                {
                    throw new ConfigurationErrorsException(
                        "Invalid configuration for DotNetNukeSSO.Version. The only accepted value is 9");
                }
                return (version = i).Value;
            }
        }

        private static ILogger Logger { get; } = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Loads user authentication.</summary>
        /// <param name="session">The session.</param>
        /// <param name="tokens"> The tokens.</param>
        public void LoadUserAuth(IAuthSession session, IAuthTokens tokens)
        {
            session.ThrowIfNull(nameof(session));
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
            if (string.IsNullOrEmpty(tokens?.Provider) || string.IsNullOrEmpty(tokens?.UserId))
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
            if (!TryAuthenticate(userName, password, out IUserAuth? userAuth))
            {
                return false;
            }
            userId = userAuth?.Id.ToString(CultureInfo.InvariantCulture);
            return true;
        }

        /// <summary>Attempts to authenticate from the given data.</summary>
        /// <param name="digestHeaders">The digest headers.</param>
        /// <param name="privateKey">   The private key.</param>
        /// <param name="nonceTimeOut"> The nonce time out.</param>
        /// <param name="sequence">     The sequence.</param>
        /// <param name="userAuth">     The user authentication.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public bool TryAuthenticate(
            Dictionary<string, string> digestHeaders,
            string privateKey,
            int nonceTimeOut,
            string sequence,
            out IUserAuth? userAuth)
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
            // Validate a username and token (password) was sent
            if (!Contract.CheckAllValidKeys(userName, password))
            {
                throw new AuthenticationException(ErrorMessages.InvalidUsernameOrPassword);
            }
            // Validate the token decrypts
#pragma warning disable 618
            var decryptedToken = CMSApiEncoder.Decrypt(password);
#pragma warning restore 618
            if (!Contract.CheckValidKey(decryptedToken))
            {
                throw new AuthenticationException(ErrorMessages.TokenInvalid);
            }
            // Validate the token hasn't expired
            var data = Convert.FromBase64String(decryptedToken);
            var created = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            var isExpired = created < DateTime.UtcNow.AddMinutes(-30);
            if (isExpired)
            {
                throw new AuthenticationException(ErrorMessages.TokenExpired);
            }
            // Validate the token has 3 parts
            var decoded = data.GetUTF8DecodedString();
            var split = decoded?.Split('|');
            if (split?.Length < 3)
            {
                throw new AuthenticationException(ErrorMessages.TokenInvalid);
            }
            // Validate the username inside the token matches the username sent
            if (!split![^1].Equals(userName, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AuthenticationException(ErrorMessages.TokenInvalid);
            }
            // NOTE: Can't use RunWrapped because of the out param
            // Get the Repository of Users
            using var context = RegistryLoaderWrapper.GetContext(null);
            using var userStore = new CEFUserStore(context);
            using var userManager = new UserManager<User, int>(userStore);
            // See if we already have the user locally
            userAuth = GetUserAuthByUserName(userName);
            if (userAuth != null)
            {
                return true;
            }
            // Make sure the user exists in DNN and pull it
            using var dnnContext = GetDNNContext();
            var dnnUser = GetDNNUserByUserName(dnnContext, userName);
            if (dnnUser == null)
            {
                throw new AuthenticationException(ErrorMessages.UserNotExists);
            }
            var task = UpsertDNNUserToCEFAsync(dnnUser);
            task.Wait(10_000);
            var upsertResult = task.Result;
            if (!upsertResult.ActionSucceeded)
            {
                return false;
            }
            var userID = userManager.Users
                .FilterByActive(true)
                .FilterUsersByUserName(userName, true)
                .Select(x => (int?)x.ID)
                .SingleOrDefault();
            if (userID == null)
            {
                // TODO: Pull the user from DNN to CEF, throw if we can't find it in DNN
                throw new AuthenticationException(ErrorMessages.UserNotExists);
            }
            // Success
            return true;
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
                    .Include(x => x.Roles)
                    .Include(x => x.Roles.Select(y => y.Role))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                    .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                    .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
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
                var result = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
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
            throw new InvalidOperationException();
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
                var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(DotNetNukeSSOUserAuthRepository)}.{nameof(UpdateUserAuth)}",
                        result.Errors.Aggregate((c, n) => $"{c}\r\n{n}"),
                        null);
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                var result2 = userManager.RemovePasswordAsync(user.ID).GetAwaiter().GetResult();
                if (!result2.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(DotNetNukeSSOUserAuthRepository)}.{nameof(UpdateUserAuth)}",
                        result2.Errors.Aggregate((c, n) => $"{c}\r\n{n}"),
                        null);
                    throw new AuthenticationException(result.Errors.FirstOrDefault());
                }
                var result3 = userManager.AddPasswordAsync(user.ID, password).GetAwaiter().GetResult();
                if (!result3.Succeeded)
                {
                    Logger.LogError(
                        $"{nameof(DotNetNukeSSOUserAuthRepository)}.{nameof(UpdateUserAuth)}",
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
                var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();
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
                var search = authSession.UserAuthId.Trim().ToLower();
                var userAuth = !string.IsNullOrEmpty(authSession.UserAuthId)
                    ? MapUserToUserAuth(userManager.Users
                        .Include(x => x.Roles)
                        .Include(x => x.Roles.Select(y => y.Role))
                        .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions))
                        .Include(x => x.Roles.Select(y => y.Role).Select(z => z!.Permissions.Select(a => a.Permission)))
                        .Include(x => x.Account!.AccountUserRoles!.Select(z => z.Slave!.Permissions.Select(a => a.Permission)))
                        .FilterByActive(true)
                        .Single(x => x.UserName == search))
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
                var result = userManager.UpdateAsync(user).GetAwaiter().GetResult();
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
                Roles = user.Roles?.Select(x => x.Role!.Name).ToList(),
                Permissions = user.Roles?.SelectMany(x => x.Role!.Permissions!.Select(y => y.Permission!.Name).Distinct()).ToList(),
                UserName = user.UserName,
                Id = user.ID,
                Company = user.Account?.Name,
            };
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
        private static IUserAuthDetails MapUserToUserAuthDetails(Clarity.Ecommerce.Interfaces.DataModel.IUser user)
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
                Provider = DotNetNukeSSOAuthProvider.StaticName,
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
                if (existingUser.Contact is null)
                {
                    return existingUser;
                }
                existingUser.Contact.CustomKey = userAuth.UserName + ":Contact";
                existingUser.Contact.UpdatedDate = timestamp;
                existingUser.Contact.Email1 = userAuth.PrimaryEmail;
                existingUser.Contact.Phone1 = userAuth.PhoneNumber;
                existingUser.Contact.FirstName = userAuth.FirstName;
                existingUser.Contact.LastName = userAuth.LastName;
                existingUser.Contact.FullName = userAuth.FullName ?? $"{userAuth.FirstName?.Trim()} {userAuth.LastName?.Trim()}".Trim();
                if (existingUser.Contact.Address is null)
                {
                    return existingUser;
                }
                existingUser.Contact.Address.CustomKey = userAuth.UserName + ":Contact:Address";
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

        /// <summary>Gets DotNetNuke context.</summary>
        /// <returns>The DotNetNuke context.</returns>
        private static DotNetNuke9Entities GetDNNContext()
        {
            switch (Version)
            {
                ////case 8: { return new DNN8.DotNetNuke8Entities(); }
                case 9:
                {
                    return new();
                }
                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(Version));
                }
            }
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
#pragma warning disable IDE0060, RCS1163 // Remove unused parameter
        private static void ValidateNewUser(IUserAuth newUser, string password)
#pragma warning restore IDE0060, RCS1163 // Remove unused parameter
        {
            // Do Nothing, DNN is managing if it's valid or not (a good username/password)
        }

        /// <summary>Gets DotNetNuke user by email.</summary>
        /// <param name="dnnContext">Context for the DotNetNuke.</param>
        /// <param name="email">     The email.</param>
        /// <returns>The DotNetNuke user by email.</returns>
        private static Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User? GetDNNUserByEmail(DotNetNuke9Entities dnnContext, string email)
        {
            return GetDNNUserByUserNameOrEmail(dnnContext, null, email);
        }

        /// <summary>Gets DotNetNuke user by user name.</summary>
        /// <param name="dnnContext">Context for the DotNetNuke.</param>
        /// <param name="userName">  Name of the user.</param>
        /// <returns>The DotNetNuke user by user name.</returns>
        private static Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User? GetDNNUserByUserName(DotNetNuke9Entities dnnContext, string userName)
        {
            return GetDNNUserByUserNameOrEmail(dnnContext, userName, null);
        }

        /// <summary>Gets DotNetNuke user by user name or email.</summary>
        /// <param name="dnnContext">Context for the DotNetNuke.</param>
        /// <param name="userName">  Name of the user.</param>
        /// <param name="email">     The email.</param>
        /// <returns>The DotNetNuke user by user name or email.</returns>
        private static Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User? GetDNNUserByUserNameOrEmail(DotNetNuke9Entities dnnContext, string? userName, string? email)
        {
            var now = DateExtensions.GenDateTime;
            return dnnContext.Users
                .AsNoTracking()
                .Include(x => x.UserProfiles)
                .Include(x => x.UserProfiles.Select(y => y.ProfilePropertyDefinition))
                .Include(x => x.UserRoles)
                .Include(x => x.UserRoles.Select(y => y.Role))
                .Where(x => userName != null && x.Username.Equals(userName, StringComparison.OrdinalIgnoreCase)
                    || email != null && x.Email.Equals(email.ToLower(), StringComparison.OrdinalIgnoreCase))
                .Select(x => new Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User
                {
                    UserID = x.UserID,
                    Username = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    DisplayName = x.DisplayName,
                    IsDeleted = x.IsDeleted,
                    UserRoles = x.UserRoles
                        .Where(y => (y.ExpiryDate == null || y.ExpiryDate > now)
                            && (y.EffectiveDate == null || y.EffectiveDate < now))
                        .Select(y => new Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.UserRole
                        {
                            RoleID = y.RoleID,
                            Role = new() { RoleName = y.Role.RoleName, },
                        })
                        .ToList(),
                    UserProfiles = x.UserProfiles
                        .Where(y => !y.ProfilePropertyDefinition.Deleted)
                        .Select(y => new UserProfile
                        {
                            ProfilePropertyDefinition = new()
                            {
                                PropertyCategory = y.ProfilePropertyDefinition.PropertyCategory,
                                PropertyName = y.ProfilePropertyDefinition.PropertyName,
                            },
                            PropertyValue = y.PropertyValue,
                        })
                        .ToList(),
                })
                .SingleOrDefault();
        }

        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        private static bool UpsertDNNUserToCEFInner(Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User dnnUser, IUserModel? cefUser)
        {
            if (cefUser is null)
            {
                return false;
            }
            var changed = false;
            if (cefUser.UserName != dnnUser.Username)
            {
                cefUser.UserName = dnnUser.Username;
                changed = true;
            }
            if (cefUser.Email != dnnUser.Email)
            {
                cefUser.Email = dnnUser.Email;
                changed = true;
            }
            if (cefUser.DisplayName != dnnUser.DisplayName)
            {
                cefUser.DisplayName = dnnUser.DisplayName;
                changed = true;
            }
            if (cefUser.IsDeleted != dnnUser.IsDeleted)
            {
                cefUser.IsDeleted = dnnUser.IsDeleted;
                changed = true;
            }
            foreach (var profileValue in dnnUser.UserProfiles)
            {
                if (!Contract.CheckValidKey(profileValue.PropertyValue))
                {
                    // Don't assign blanks
                    continue;
                }
                cefUser.Contact ??= RegistryLoaderWrapper.GetInstance<IContactModel>();
                cefUser.Contact.Address ??= RegistryLoaderWrapper.GetInstance<IAddressModel>();
                switch (profileValue.ProfilePropertyDefinition.PropertyCategory
                        + " "
                        + profileValue.ProfilePropertyDefinition.PropertyName)
                {
                    case "Basic FirstName":
                    {
                        if (cefUser.Contact.FirstName != profileValue.PropertyValue)
                        {
                            cefUser.Contact.FirstName = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Basic MiddleName":
                    {
                        if (cefUser.Contact.MiddleName != profileValue.PropertyValue)
                        {
                            cefUser.Contact.MiddleName = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Basic LastName":
                    {
                        if (cefUser.Contact.LastName != profileValue.PropertyValue)
                        {
                            cefUser.Contact.LastName = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Contact Telephone":
                    {
                        if (cefUser.Contact.Phone1 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Phone1 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Contact Cell":
                    {
                        if (cefUser.Contact.Phone2 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Phone2 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Contact Fax":
                    {
                        if (cefUser.Contact.Fax1 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Fax1 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Street":
                    {
                        if (cefUser.Contact.Address.Street1 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.Street1 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Unit":
                    {
                        if (cefUser.Contact.Address.Street2 != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.Street2 = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location City":
                    {
                        if (cefUser.Contact.Address.City != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.City = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Region":
                    {
                        if (cefUser.Contact.Address.RegionCode != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.RegionCode = profileValue.PropertyValue;
                            cefUser.Contact.Address.Region = null;
                            cefUser.Contact.Address.RegionID = null;
                            cefUser.Contact.Address.RegionKey
                                = cefUser.Contact.Address.RegionName
                                    = null;
                            changed = true;
                        }
                        break;
                    }
                    case "Location Country":
                    {
                        if (cefUser.Contact.Address.CountryCode != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.CountryCode = profileValue.PropertyValue;
                            cefUser.Contact.Address.CountryCode = profileValue.PropertyValue;
                            cefUser.Contact.Address.Country = null;
                            cefUser.Contact.Address.CountryID = null;
                            cefUser.Contact.Address.CountryKey
                                = cefUser.Contact.Address.CountryName
                                    = null;
                            changed = true;
                        }
                        break;
                    }
                    case "Location PostalCode":
                    {
                        if (cefUser.Contact.Address.PostalCode != profileValue.PropertyValue)
                        {
                            cefUser.Contact.Address.PostalCode = profileValue.PropertyValue;
                            changed = true;
                        }
                        break;
                    }
                    default:
                    {
                        // Don't assign anything else
                        continue;
                    }
                }
            }
            return changed;
        }

        private static async Task<CEFActionResponse> UpsertDNNUserToCEFAsync(Clarity.Ecommerce.Providers.Authentication.DotNetNukeSSO.DNN9.User dnnUser)
        {
            var userWorkflow = RegistryLoaderWrapper.GetInstance<IUserWorkflow>();
            var existingID = await userWorkflow.CheckExistsAsync(dnnUser.Username, contextProfileName: null).ConfigureAwait(false);
            int cefUserModel;
            if (Contract.CheckValidID(existingID))
            {
                var cefUser = await userWorkflow.GetAsync(existingID!.Value, contextProfileName: null).ConfigureAwait(false);
                if (!UpsertDNNUserToCEFInner(dnnUser, cefUser))
                {
                    return CEFAR.FailingCEFAR("ERROR: Unable to read user");
                }
                cefUserModel = Contract.RequiresValidID(
                    (await userWorkflow.UpdateAsync(cefUser!, contextProfileName: null).ConfigureAwait(false)).Result);
            }
            else
            {
                var cefUser = RegistryLoaderWrapper.GetInstance<IUserModel>();
                cefUser.Contact = RegistryLoaderWrapper.GetInstance<IContactModel>();
                cefUser.Contact.Address = RegistryLoaderWrapper.GetInstance<IAddressModel>();
                if (!UpsertDNNUserToCEFInner(dnnUser, cefUser))
                {
                    return CEFAR.FailingCEFAR("ERROR: Unable to read user");
                }
                cefUserModel = Contract.RequiresValidID(
                    (await userWorkflow.CreateAsync(cefUser, contextProfileName: null).ConfigureAwait(false)).Result);
            }
            var authenticationWorkflow = RegistryLoaderWrapper.GetInstance<IAuthenticationWorkflow>();
            var cefRoles = await authenticationWorkflow.GetRolesAsync(null).ConfigureAwait(false);
            var userDNNRoles = dnnUser.UserRoles.Select(x => x.Role.RoleName).ToList();
            var added = false;
            foreach (var dnnRole in userDNNRoles.Where(x => !cefRoles.ContainsKey(x)))
            {
                var createRoleResult = await authenticationWorkflow.CreateRoleAsync(dnnRole, new(), null).ConfigureAwait(false);
                if (!createRoleResult.ActionSucceeded)
                {
                    return createRoleResult;
                }
                added = true;
            }
            if (added)
            {
                cefRoles = await authenticationWorkflow.GetRolesAsync(null).ConfigureAwait(false);
            }
            var userCEFRoles = await authenticationWorkflow.GetRolesForUserAsync(cefUserModel, null).ConfigureAwait(false);
            var removed = false;
            foreach (var userCEFRole in userCEFRoles.Where(x => !userDNNRoles.Contains(x.Name!)))
            {
                var removeResult = await authenticationWorkflow.RemoveRoleFromUserAsync(userCEFRole, null).ConfigureAwait(false);
                if (!removeResult.ActionSucceeded)
                {
                    return removeResult;
                }
                removed = true;
            }
            if (removed)
            {
                userCEFRoles = await authenticationWorkflow.GetRolesForUserAsync(cefUserModel, null).ConfigureAwait(false);
            }
            foreach (var userDNNRole in userDNNRoles.Where(x => !userCEFRoles.Select(y => y.Name).Contains(x)))
            {
                var roleAssignment = RegistryLoaderWrapper.GetInstance<IRoleForUserModel>();
                roleAssignment.Name = userDNNRole;
                roleAssignment.RoleId = cefRoles.Single(x => x.Key == userDNNRole).Value;
                roleAssignment.UserId = cefUserModel;
                var assignResult = await authenticationWorkflow.AssignRoleToUserAsync(roleAssignment, null).ConfigureAwait(false);
                if (!assignResult.ActionSucceeded)
                {
                    return assignResult;
                }
            }
            return CEFAR.PassingCEFAR();
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
                AssertNoExistingUserByUserName(newUser, exceptForExistingUser);
            }
            if (newUser.Email != null)
            {
                AssertNoExistingUserByEmail(newUser, exceptForExistingUser);
            }
        }

        /// <summary>Assert no existing user by email.</summary>
        /// <param name="newUser">              The new user.</param>
        /// <param name="exceptForExistingUser">The except for existing user.</param>
        private void AssertNoExistingUserByEmail(IUserAuth newUser, IUserAuth? exceptForExistingUser)
        {
            using var dnnContext = GetDNNContext();
            var dnnUser = GetDNNUserByEmail(dnnContext, newUser.Email);
            if (dnnUser == null)
            {
                return;
            }
            var task = UpsertDNNUserToCEFAsync(dnnUser);
            task.Wait(10_000);
            var existingUser = GetUserAuthByUserName(newUser.Email);
            if (existingUser == null
                || exceptForExistingUser != null && existingUser.Id == exceptForExistingUser.Id)
            {
                return;
            }
            throw new ArgumentException($"Email {newUser.Email} already exists");
        }

        /// <summary>Assert no existing user by user name.</summary>
        /// <param name="newUser">              The new user.</param>
        /// <param name="exceptForExistingUser">The except for existing user.</param>
        private void AssertNoExistingUserByUserName(IUserAuth newUser, IUserAuth? exceptForExistingUser)
        {
            using var dnnContext = GetDNNContext();
            var dnnUser = GetDNNUserByUserName(dnnContext, newUser.UserName);
            if (dnnUser == null)
            {
                return;
            }
            var task = UpsertDNNUserToCEFAsync(dnnUser);
            task.Wait(10_000);
            var existingUser = GetUserAuthByUserName(newUser.UserName);
            if (existingUser == null
                || exceptForExistingUser != null && existingUser.Id == exceptForExistingUser.Id)
            {
                return;
            }
            throw new ArgumentException($"User {newUser.UserName} already exists");
        }
    }
}
