// <copyright file="CEFUserManager.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cef user manager class</summary>
namespace Clarity.Ecommerce.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.DataProtection;
    using Utilities;

    [PublicAPI]
    public interface ICEFUserManager : IDisposable
    {
        #region Properties
        /// <summary>Gets the CEF user store.</summary>
        /// <value>The CEF user store.</value>
        ICEFUserStore CEFStore { get; }

        /// <summary>Gets the users.</summary>
        /// <value>The users.</value>
        IQueryable<User> Users { get; }

        /// <summary>Gets the password hasher.</summary>
        /// <value>The password hasher.</value>
        IPasswordHasher PasswordHasher { get; }
        #endregion

        #region User Management
        /// <summary>Creates a new IdentityResult.</summary>
        /// <param name="user">The user.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> CreateAsync(User user);

        /// <summary>Creates a new IdentityResult.</summary>
        /// <param name="user">    The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> CreateAsync(User user, string password);

        /// <summary>Searches for the first email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>The found email.</returns>
        Task<User?> FindByEmailAsync(string email);

        /// <summary>Searches for the first name.</summary>
        /// <param name="userUserName">Name of the user.</param>
        /// <returns>The found name.</returns>
        Task<User?> FindByNameAsync(string userUserName);

        /// <summary>Sends an email.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">   The body.</param>
        /// <returns>A Task.</returns>
        Task SendEmailAsync(int userId, string subject, string body);

        /// <summary>Confirm email.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="token"> The token.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> ConfirmEmailAsync(int userId, string token);
        #endregion

        #region Passwords
        /// <summary>Check password.</summary>
        /// <param name="user">    The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> CheckPasswordAsync(User user, string password);

        /// <summary>Generates a password reset token.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The password reset token.</returns>
        Task<string> GeneratePasswordResetTokenAsync(int userId);

        /// <summary>Resets the password.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="token">      The token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> ResetPasswordAsync(int userId, string token, string newPassword);

        /// <summary>Change password.</summary>
        /// <param name="userId">         Identifier for the user.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">    The new password.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        /// <summary>Generates a one time pass-code.</summary>
        /// <param name="userID">  Identifier for the user.</param>
        /// <param name="usePhone">True to use phone.</param>
        /// <returns>The one time pass-code.</returns>
        Task<string> GenOTPAsync(int userID, bool usePhone);

        /// <summary>Gets two factor enabled.</summary>
        /// <param name="username">The username.</param>
        /// <returns>The two factor enabled.</returns>
        Task<bool> GetTwoFactorEnabledAsync(string username);

        /// <summary>Notifies a two factor token.</summary>
        /// <param name="username">The username.</param>
        /// <param name="usePhone">True to use phone.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> NotifyTwoFactorTokenAsync(string username, bool usePhone);
        #endregion

        #region Lockouts
        /// <summary>Query if 'userId' is locked out.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>True if locked out, false if not.</returns>
        Task<bool> IsLockedOutAsync(int userId);

        /// <summary>Sets lockout enabled.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="enabled">True to enable, false to disable.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> SetLockoutEnabledAsync(int userId, bool enabled);
        #endregion

        #region Roles Management
        /// <summary>Adds a role to user.</summary>
        /// <param name="userId">   Identifier for the user.</param>
        /// <param name="role">     The role.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">  The end date.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> AddRoleToUserAsync(int userId, string role, DateTime? startDate, DateTime? endDate);

        /// <summary>Adds a role to account.</summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <param name="role">     The role.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">  The end date.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> AddRoleToAccountAsync(int accountId, string role, DateTime? startDate, DateTime? endDate);

        /// <summary>Gets the roles.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The roles.</returns>
        Task<List<string>> GetUserRolesAsync(int userId);

        /// <summary>Gets account roles.</summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <returns>The account roles.</returns>
        Task<List<string>> GetAccountRolesAsync(int accountId);

        /// <summary>Gets role names for user.</summary>
        /// <param name="userID">Identifier for the user.</param>
        /// <returns>An array of string.</returns>
        Task<string[]> GetRoleNamesForUserAsync(int userID);

        /// <summary>Gets role names for account.</summary>
        /// <param name="accountID">Identifier for the account.</param>
        /// <returns>An array of string.</returns>
        Task<string[]> GetRoleNamesForAccountAsync(int accountID);

        /// <summary>Gets roles for user.</summary>
        /// <param name="userID">Identifier for the user.</param>
        /// <returns>An array of IRoleForUserModel.</returns>
        Task<IRoleForUserModel[]> GetRolesForUserAsync(int userID);

        /// <summary>Gets roles for account.</summary>
        /// <param name="accountID">Identifier for the account.</param>
        /// <returns>An array of IRoleForAccountModel.</returns>
        Task<IRoleForAccountModel[]> GetRolesForAccountAsync(int accountID);

        /// <summary>User has role.</summary>
        /// <param name="userID">Identifier for the user.</param>
        /// <param name="name">  The name.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> UserHasRoleAsync(int userID, string name);

        /// <summary>Account has role.</summary>
        /// <param name="accountID">Identifier for the account.</param>
        /// <param name="name">     The name.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> AccountHasRoleAsync(int accountID, string name);

        /// <summary>Updates the role described by model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> UpdateRoleAsync(IRoleForUserModel model);

        /// <summary>Updates the role described by model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> UpdateRoleAsync(IRoleForAccountModel model);

        /// <summary>Removes the role from user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="role">  The role.</param>
        /// <returns>An IdentityResult.</returns>
        Task<IdentityResult> RemoveRoleFromUserAsync(int userId, string role);

        /// <summary>Removes the role from account.</summary>
        /// <param name="accountId">Identifier for the account.</param>
        /// <param name="role">     The role.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> RemoveRoleFromAccountAsync(int accountId, string role);
        #endregion

        #region Permissions Management
        /// <summary>Gets permission names for user.</summary>
        /// <param name="userID">Identifier for the user.</param>
        /// <returns>An array of string.</returns>
        Task<string[]> GetPermissionNamesForUserAsync(int userID);

        /// <summary>Gets permission names for account.</summary>
        /// <param name="accountID">Identifier for the account.</param>
        /// <returns>An array of string.</returns>
        Task<string[]> GetPermissionNamesForAccountAsync(int accountID);

        /// <summary>User has permission.</summary>
        /// <param name="userID">Identifier for the user.</param>
        /// <param name="name">  The name.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> UserHasPermissionAsync(int userID, string name);

        /// <summary>Account has permission.</summary>
        /// <param name="accountID">Identifier for the account.</param>
        /// <param name="name">     The name.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> AccountHasPermissionAsync(int accountID, string name);
        #endregion

        /// <summary>Gets email store.</summary>
        /// <returns>The email store.</returns>
        IUserEmailStore<User, int> GetEmailStore();
    }

    /// <summary>Manager for CEF users.</summary>
    /// <seealso cref="UserManager{User,Int32}"/>
    public class CEFUserManager : Microsoft.AspNet.Identity2.UserManager<User, int>, ICEFUserManager
    {
        public const string DataProtectionProviderNameForEmail = "ASP.NET Identity (By Email)";

        public const string DataProtectionProviderNameForSMS = "ASP.NET Identity (By SMS)";

        /// <summary>Initializes a new instance of the <see cref="CEFUserManager"/> class.</summary>
        /// <param name="store">The store.</param>
        public CEFUserManager(ICEFUserStore store)
            : base(store)
        {
            var dataProtectionProvider = new DpapiDataProtectionProvider("CEF");
            UserTokenProvider = new Microsoft.AspNet.Identity2.Owin2.DataProtectorTokenProvider<User, int>(
                dataProtectionProvider.Create(DataProtectionProviderNameForEmail));
            UserTokenProvider = new Microsoft.AspNet.Identity2.Owin2.DataProtectorTokenProvider<User, int>(
                dataProtectionProvider.Create("ASP.NET Identity"));
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = int.Parse(ConfigurationManager.AppSettings["Clarity.API.Auth.Providers.Identity.RequiredLength"] ?? "6"),
                RequireDigit = bool.Parse(ConfigurationManager.AppSettings["Clarity.API.Auth.Providers.Identity.RequireDigit"] ?? "false"),
                RequireLowercase = bool.Parse(ConfigurationManager.AppSettings["Clarity.API.Auth.Providers.Identity.RequireLowercase"] ?? "false"),
                RequireUppercase = bool.Parse(ConfigurationManager.AppSettings["Clarity.API.Auth.Providers.Identity.RequireUppercase"] ?? "false"),
                RequireNonLetterOrDigit = bool.Parse(ConfigurationManager.AppSettings["Clarity.API.Auth.Providers.Identity.RequireNonLetterOrDigit"] ?? "false"),
            };
            UserValidator = new Microsoft.AspNet.Identity2.UserValidator<User, int>(this)
            {
                AllowOnlyAlphanumericUserNames = !bool.Parse(ConfigurationManager.AppSettings["Clarity.API.Auth.Providers.Identity.UserNameIsEmail"] ?? "true"),
                RequireUniqueEmail = true,
            };
            if (!bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.Enabled"] ?? "false"))
            {
                return;
            }
            if (bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.ByEmail.Enabled"] ?? "false"))
            {
                var emailSubject = ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.ByEmail.Subject"];
                var emailBody = ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.ByEmail.Body"];
                _ = Contract.RequiresAllValidKeys(emailSubject, emailBody);
                EmailService = new CEFEmailService();
                // ReSharper disable once VirtualMemberCallInConstructor
                RegisterTwoFactorProvider(
                    DataProtectionProviderNameForEmail,
                    new Microsoft.AspNet.Identity2.EmailTokenProvider<User, int>
                    {
                        Subject = emailSubject!,
                        BodyFormat = emailBody!,
                    });
            }
            if (!bool.Parse(ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.BySMS.Enabled"] ?? "false"))
            {
                return;
            }
            var smsBody = Contract.RequiresValidKey(
                ConfigurationManager.AppSettings["Clarity.Login.TwoFactor.BySMS.Body"]);
            SmsService = new CEFSMSService();
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterTwoFactorProvider(
                DataProtectionProviderNameForSMS,
                new Microsoft.AspNet.Identity2.PhoneNumberTokenProvider<User, int>
                {
                    MessageFormat = smsBody,
                });
        }

        /// <inheritdoc/>
        public ICEFUserStore CEFStore => (ICEFUserStore)Store;

        /* Custom OTP Maker:
         * string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
         * var code = GenerateRandomOTP(6, saAllowedCharacters);
         * string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
         * {
         *     string sOTP = string.Empty;
         *     string sTempChars = string.Empty;
         *     Random rand = new Random();
         *     for (int i = 0; i < iOTPLength; i++)
         *     {
         *         int p = rand.Next(0, saAllowedCharacters.Length);
         *         sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
         *         sOTP += sTempChars;
         *     }
         *     return sOTP;
         * }
         */

        /// <inheritdoc/>
        public async Task<bool> GetTwoFactorEnabledAsync(string username)
        {
            var userID = await CEFStore.Context.Users
                .Where(x => x.Active && x.UserName == username)
                .Select(x => x.ID)
                .SingleAsync()
                .ConfigureAwait(false);
            return await GetTwoFactorEnabledAsync(userID).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<bool> NotifyTwoFactorTokenAsync(string username, bool usePhone)
        {
            var userID = await CEFStore.Context.Users
                .Where(x => x.Active && x.UserName == username)
                .Select(x => x.ID)
                .SingleAsync()
                .ConfigureAwait(false);
            var token = await GenOTPAsync(userID, usePhone).ConfigureAwait(false);
            if (usePhone)
            {
                return (await NotifyTwoFactorTokenAsync(
                            userID,
                            DataProtectionProviderNameForSMS,
                            token)
                        .ConfigureAwait(false))
                    .Succeeded;
            }
            return (await NotifyTwoFactorTokenAsync(
                        userID,
                        DataProtectionProviderNameForEmail,
                        token)
                    .ConfigureAwait(false))
                .Succeeded;
        }

        /// <inheritdoc/>
        public Task<string> GenOTPAsync(int userID, bool usePhone)
        {
            return GenerateTwoFactorTokenAsync(
                Contract.RequiresValidID(userID),
                usePhone ? DataProtectionProviderNameForSMS : DataProtectionProviderNameForEmail);
        }

        #region Roles Management
        /// <inheritdoc/>
        public async Task<IdentityResult> AddRoleToUserAsync(
            int userId,
            string role,
            DateTime? startDate,
            DateTime? endDate)
        {
            var roleForUser = CEFStore.Context.Users
                .Single(x => x.ID == userId)
                .Roles
                .FirstOrDefault(x => x.Role?.Name == role);
            if (roleForUser is null)
            {
                var entity = CEFStore.Context.RoleUsers.Create();
                entity.UserId = userId;
                entity.RoleId = await CEFStore.Context.Roles
                    .Where(x => x.Name == role)
                    .Select(x => x.Id)
                    .SingleAsync()
                    .ConfigureAwait(false);
                entity.StartDate = startDate ?? DateExtensions.GenDateTime;
                entity.EndDate = endDate;
                CEFStore.Context.RoleUsers.Add(entity);
                await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                return IdentityResult.Success; // valid
            }
            var dirty = false;
            if (startDate.HasValue)
            {
                if (roleForUser.StartDate != startDate.Value)
                {
                    // Change to provided value
                    roleForUser.StartDate = startDate;
                    dirty = true;
                }
            }
            else if (roleForUser.StartDate.HasValue)
            {
                // Change to no value to match incoming
                roleForUser.StartDate = null;
                dirty = true;
            }
            if (endDate.HasValue)
            {
                if (roleForUser.EndDate != endDate.Value)
                {
                    // Change to provided value
                    roleForUser.EndDate = endDate;
                    dirty = true;
                }
            }
            else if (roleForUser.EndDate.HasValue)
            {
                // Change to no value to match incoming
                roleForUser.EndDate = null;
                dirty = true;
            }
            if (dirty)
            {
                await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> AddRoleToAccountAsync(
            int accountId,
            string role,
            DateTime? startDate,
            DateTime? endDate)
        {
            var roleForAccount = CEFStore.Context.Accounts
                .Single(x => x.ID == accountId)
                .AccountUserRoles!
                .FirstOrDefault(x => x.Slave!.Name == role);
            if (roleForAccount is null)
            {
                var entity = CEFStore.Context.AccountUserRoles.Create();
                entity.Active = true;
                entity.CreatedDate = DateExtensions.GenDateTime;
                entity.MasterID = accountId;
                entity.SlaveID = await CEFStore.Context.Roles
                    .Where(x => x.Name == role)
                    .Select(x => x.Id)
                    .SingleAsync()
                    .ConfigureAwait(false);
                entity.StartDate = startDate ?? DateExtensions.GenDateTime;
                entity.EndDate = endDate;
                CEFStore.Context.AccountUserRoles.Add(entity);
                await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                return IdentityResult.Success; // valid
            }
            var dirty = false;
            if (!roleForAccount.Active)
            {
                roleForAccount.Active = true;
                dirty = true;
            }
            if (startDate.HasValue)
            {
                if (roleForAccount.StartDate != startDate.Value)
                {
                    // Change to provided value
                    roleForAccount.StartDate = startDate;
                    dirty = true;
                }
            }
            else if (roleForAccount.StartDate.HasValue)
            {
                // Change to no value to match incoming
                roleForAccount.StartDate = null;
                dirty = true;
            }
            if (endDate.HasValue)
            {
                if (roleForAccount.EndDate != endDate.Value)
                {
                    // Change to provided value
                    roleForAccount.EndDate = endDate;
                    dirty = true;
                }
            }
            else if (roleForAccount.EndDate.HasValue)
            {
                // Change to no value to match incoming
                roleForAccount.EndDate = null;
                dirty = true;
            }
            if (dirty)
            {
                await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetUserRolesAsync(int userId)
        {
            return (await GetRolesAsync(userId).ConfigureAwait(false)).ToList();
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetAccountRolesAsync(int accountId)
        {
            return (await GetRoleNamesForAccountAsync(accountId).ConfigureAwait(false)).ToList();
        }

        /// <inheritdoc/>
        public Task<string[]> GetRoleNamesForUserAsync(int userID)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.RoleUsers
                .Where(x => x.UserId == userID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .Select(x => x.Role!.Name)
                .Distinct()
                .ToArrayAsync();
        }

        /// <inheritdoc/>
        public Task<string[]> GetRoleNamesForAccountAsync(int accountID)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.AccountUserRoles
                .Where(x => x.MasterID == accountID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .Select(x => x.Slave!.Name)
                .Distinct()
                .ToArrayAsync();
        }

        /// <inheritdoc/>
        public async Task<IRoleForUserModel[]> GetRolesForUserAsync(int userID)
        {
            return (await CEFStore.Context.RoleUsers
                .Where(x => x.UserId == userID)
                .Select(x => new
                {
                    x.RoleId,
                    x.Role!.Name,
                    x.UserId,
                    x.StartDate,
                    x.EndDate,
                })
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(x => new RoleForUserModel
                {
                    RoleId = x.RoleId,
                    Name = x.Name,
                    UserId = x.UserId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                })
                .ToArray<IRoleForUserModel>();
        }

        /// <inheritdoc/>
        public async Task<IRoleForAccountModel[]> GetRolesForAccountAsync(int accountID)
        {
            return (await CEFStore.Context.AccountUserRoles
                .Where(x => x.MasterID == accountID)
                .Select(x => new
                {
                    x.SlaveID,
                    x.Slave!.Name,
                    x.MasterID,
                    x.StartDate,
                    x.EndDate,
                })
                .ToListAsync()
                .ConfigureAwait(false))
                .Select(x => new RoleForAccountModel
                {
                    RoleId = x.SlaveID,
                    Name = x.Name,
                    AccountId = x.MasterID,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                })
                .ToArray<IRoleForAccountModel>();
        }

        /// <inheritdoc/>
        public Task<bool> UserHasRoleAsync(int userID, string name)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.RoleUsers
                .Where(x => x.UserId == userID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .Select(x => x.Role!.Name)
                .ContainsAsync(name);
        }

        /// <inheritdoc/>
        public Task<bool> AccountHasRoleAsync(int accountID, string name)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.AccountUserRoles
                .Where(x => x.MasterID == accountID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .Select(x => x.Slave!.Name)
                .ContainsAsync(name);
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> UpdateRoleAsync(IRoleForUserModel model)
        {
            var roleForUser = CEFStore.Context.Users
                .Single(x => x.ID == model.UserId)
                .Roles
                .FirstOrDefault(x => x.RoleId == model.RoleId);
            if (roleForUser is null)
            {
                return new("Unable to locate role to update it");
            }
            var dirty = false;
            if (roleForUser.StartDate != model.StartDate)
            {
                roleForUser.StartDate = model.StartDate;
                dirty = true;
            }
            if (roleForUser.EndDate != model.EndDate)
            {
                roleForUser.EndDate = model.EndDate;
                dirty = true;
            }
            if (dirty)
            {
                await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> UpdateRoleAsync(IRoleForAccountModel model)
        {
            var roleForAccount = CEFStore.Context.Accounts
                .Single(x => x.ID == model.AccountId)
                .AccountUserRoles!
                .FirstOrDefault(x => x.SlaveID == model.RoleId);
            if (roleForAccount is null)
            {
                return new("Unable to locate role to update it");
            }
            var dirty = false;
            if (roleForAccount.StartDate != model.StartDate)
            {
                roleForAccount.StartDate = model.StartDate;
                dirty = true;
            }
            if (roleForAccount.EndDate != model.EndDate)
            {
                roleForAccount.EndDate = model.EndDate;
                dirty = true;
            }
            if (dirty)
            {
                await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> RemoveRoleFromUserAsync(int userId, string role)
        {
            var roleForUser = await CEFStore.Context.Set<RoleUser>()
                .SingleOrDefaultAsync(x => x.UserId == userId && x.Role!.Name == role)
                .ConfigureAwait(false);
            if (roleForUser is null)
            {
                return new("Unable to locate role to update it");
            }
            // Set the end date to now so it no longer shows up in queries but keeps a history that they did have the role
            roleForUser.EndDate = DateExtensions.GenDateTime;
            await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            return IdentityResult.Success;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> RemoveRoleFromAccountAsync(int accountId, string role)
        {
            var roleForAccount = CEFStore.Context.Accounts
                .Single(x => x.ID == accountId)
                .AccountUserRoles!
                .FirstOrDefault(x => x.Slave!.Name == role);
            if (roleForAccount is null)
            {
                return new("Unable to locate role to update it");
            }
            // Set the end date to now so it no longer shows up in queries but keeps a history that they did have the role
            roleForAccount.EndDate = DateExtensions.GenDateTime;
            await CEFStore.Context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            return IdentityResult.Success;
        }
        #endregion

        #region Permissions Management
        /// <inheritdoc/>
        public Task<string[]> GetPermissionNamesForUserAsync(int userID)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.RoleUsers
                .Where(x => x.UserId == userID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .SelectMany(x => x.Role!.Permissions.Select(y => y.Permission!.Name))
                .Distinct()
                .ToArrayAsync()!;
        }

        /// <inheritdoc/>
        public Task<string[]> GetPermissionNamesForAccountAsync(int accountID)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.AccountUserRoles
                .Where(x => x.MasterID == accountID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .SelectMany(x => x.Slave!.Permissions.Select(y => y.Permission!.Name))
                .Distinct()
                .ToArrayAsync()!;
        }

        /// <inheritdoc/>
        public Task<bool> UserHasPermissionAsync(int userID, string name)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.RoleUsers
                .Where(x => x.UserId == userID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .SelectMany(x => x.Role!.Permissions.Select(y => y.Permission!.Name))
                .AnyAsync(x => x == name);
        }

        /// <inheritdoc/>
        public Task<bool> AccountHasPermissionAsync(int accountID, string name)
        {
            var now = DateExtensions.GenDateTime;
            return CEFStore.Context.AccountUserRoles
                .Where(x => x.MasterID == accountID
                    && (!x.StartDate.HasValue || x.StartDate < now)
                    && (!x.EndDate.HasValue || x.EndDate > now))
                .SelectMany(x => x.Slave!.Permissions.Select(y => y.Permission!.Name))
                .AnyAsync(x => x == name);
        }
        #endregion
    }
}

#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Utilities;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Identity;

    /// <summary>Interface for user manager.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    public interface IUserManager<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Gets or sets the claims identity factory.</summary>
        /// <value>The claims identity factory.</value>
        IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory { get; set; }

        /// <summary>Gets or sets the default account lockout time span.</summary>
        /// <value>The default account lockout time span.</value>
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }

        /// <summary>Gets or sets the email service.</summary>
        /// <value>The email service.</value>
        IIdentityMessageService? EmailService { get; set; }

        /// <summary>Gets or sets the maximum failed access attempts before lockout.</summary>
        /// <value>The maximum failed access attempts before lockout.</value>
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        /// <summary>Gets or sets the password hasher.</summary>
        /// <value>The password hasher.</value>
        IPasswordHasher PasswordHasher { get; set; }

        /// <summary>Gets or sets the password validator.</summary>
        /// <value>The password validator.</value>
        IIdentityValidator<string> PasswordValidator { get; set; }

        /// <summary>Gets or sets the SMS service.</summary>
        /// <value>The SMS service.</value>
        IIdentityMessageService? SmsService { get; set; }

        /// <summary>Gets a value indicating whether the supports queryable users.</summary>
        /// <value>True if supports queryable users, false if not.</value>
        bool SupportsQueryableUsers { get; }

        /// <summary>Gets a value indicating whether the supports user claim.</summary>
        /// <value>True if supports user claim, false if not.</value>
        bool SupportsUserClaim { get; }

        /// <summary>Gets a value indicating whether the supports user email.</summary>
        /// <value>True if supports user email, false if not.</value>
        bool SupportsUserEmail { get; }

        /// <summary>Gets a value indicating whether the supports user lockout.</summary>
        /// <value>True if supports user lockout, false if not.</value>
        bool SupportsUserLockout { get; }

        /// <summary>Gets a value indicating whether the supports user login.</summary>
        /// <value>True if supports user login, false if not.</value>
        bool SupportsUserLogin { get; }

        /// <summary>Gets a value indicating whether the supports user password.</summary>
        /// <value>True if supports user password, false if not.</value>
        bool SupportsUserPassword { get; }

        /// <summary>Gets a value indicating whether the supports user phone number.</summary>
        /// <value>True if supports user phone number, false if not.</value>
        bool SupportsUserPhoneNumber { get; }

        /// <summary>Gets a value indicating whether the supports user role.</summary>
        /// <value>True if supports user role, false if not.</value>
        bool SupportsUserRole { get; }

        /// <summary>Gets a value indicating whether the supports user security stamp.</summary>
        /// <value>True if supports user security stamp, false if not.</value>
        bool SupportsUserSecurityStamp { get; }

        /// <summary>Gets a value indicating whether the supports user two factor.</summary>
        /// <value>True if supports user two factor, false if not.</value>
        bool SupportsUserTwoFactor { get; }

        /// <summary>Gets the two factor providers.</summary>
        /// <value>The two factor providers.</value>
        IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders { get; }

        /// <summary>Gets or sets a value indicating whether the user lockout enabled by default.</summary>
        /// <value>True if user lockout enabled by default, false if not.</value>
        bool UserLockoutEnabledByDefault { get; set; }

        /// <summary>Gets the users.</summary>
        /// <value>The users.</value>
        IQueryable<TUser> Users { get; }

        /// <summary>Gets or sets the user token provider.</summary>
        /// <value>The user token provider.</value>
        IUserTokenProvider<TUser, TKey>? UserTokenProvider { get; set; }

        /// <summary>Gets or sets the user validator.</summary>
        /// <value>The user validator.</value>
        IIdentityValidator<TUser> UserValidator { get; set; }

        /// <summary>Access failed.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> AccessFailedAsync(TKey userId);

        /// <summary>Adds a claim to 'claim'.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="claim"> The claim.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim);

        /// <summary>Adds a login to 'login'.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="login"> The login.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login);

        /// <summary>Adds a password to 'password'.</summary>
        /// <param name="userId">  Identifier for the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> AddPasswordAsync(TKey userId, string password);

        /// <summary>Adds to the role.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="role">  The role.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> AddToRoleAsync(TKey userId, string role);

        /// <summary>Adds to the roles.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="roles"> A variable-length parameters list containing roles.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> AddToRolesAsync(TKey userId, params string[] roles);

        /// <summary>Change password.</summary>
        /// <param name="userId">         Identifier for the user.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">    The new password.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword);

        /// <summary>Change phone number.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="token">      The token.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token);

        /// <summary>Check password.</summary>
        /// <param name="user">    The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> CheckPasswordAsync(TUser? user, string password);

        /// <summary>Confirm email.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="token"> The token.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> ConfirmEmailAsync(TKey userId, string token);

        /// <summary>Creates an.</summary>
        /// <param name="user">The user.</param>
        /// <returns>The new.</returns>
        Task<IdentityResult> CreateAsync(TUser user);

        /// <summary>Creates an.</summary>
        /// <param name="user">    The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The new.</returns>
        Task<IdentityResult> CreateAsync(TUser? user, string? password);

        /// <summary>Creates identity.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="authenticationType">Type of the authentication.</param>
        /// <returns>The new identity.</returns>
        Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType);

        /// <summary>Deletes the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> DeleteAsync(TUser user);

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        /// resources.</summary>
        void Dispose();

        /// <summary>Searches for the first.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The found.</returns>
        Task<TUser?> FindAsync(string userName, string password);

        /// <summary>Searches for the first.</summary>
        /// <param name="login">The login.</param>
        /// <returns>The found.</returns>
        Task<TUser?> FindAsync(UserLoginInfo login);

        /// <summary>Searches for the first email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>The found email.</returns>
        Task<TUser?> FindByEmailAsync(string email);

        /// <summary>Searches for the first identifier.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The found identifier.</returns>
        Task<TUser?> FindByIdAsync(TKey userId);

        /// <summary>Searches for the first name.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The found name.</returns>
        Task<TUser?> FindByNameAsync(string userName);

        /// <summary>Generates a change phone number token.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The change phone number token.</returns>
        Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber);

        /// <summary>Generates an email confirmation token.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The email confirmation token.</returns>
        Task<string> GenerateEmailConfirmationTokenAsync(TKey userId);

        /// <summary>Generates a password reset token.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The password reset token.</returns>
        Task<string> GeneratePasswordResetTokenAsync(TKey userId);

        /// <summary>Generates a two factor token.</summary>
        /// <param name="userId">           Identifier for the user.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <returns>The two factor token.</returns>
        Task<string> GenerateTwoFactorTokenAsync(TKey userId, string twoFactorProvider);

        /// <summary>Generates a user token.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="userId"> Identifier for the user.</param>
        /// <returns>The user token.</returns>
        Task<string> GenerateUserTokenAsync(string purpose, TKey userId);

        /// <summary>Gets access failed count.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The access failed count.</returns>
        Task<int> GetAccessFailedCountAsync(TKey userId);

        /// <summary>Gets claims.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The claims.</returns>
        Task<IList<Claim>> GetClaimsAsync(TKey userId);

        /// <summary>Gets email.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The email.</returns>
        Task<string> GetEmailAsync(TKey userId);

        /// <summary>Gets email store.</summary>
        /// <returns>The email store.</returns>
        IUserEmailStore<TUser, TKey> GetEmailStore();

        /// <summary>Gets lockout enabled.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The lockout enabled.</returns>
        Task<bool> GetLockoutEnabledAsync(TKey userId);

        /// <summary>Gets lockout end date.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The lockout end date.</returns>
        Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId);

        /// <summary>Gets logins.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The logins.</returns>
        Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId);

        /// <summary>Gets phone number.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The phone number.</returns>
        Task<string> GetPhoneNumberAsync(TKey userId);

        /// <summary>Gets roles.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The roles.</returns>
        Task<IList<string>> GetRolesAsync(TKey userId);

        /// <summary>Gets security stamp.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The security stamp.</returns>
        Task<string> GetSecurityStampAsync(TKey userId);

        /// <summary>Gets two factor enabled.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The two factor enabled.</returns>
        Task<bool> GetTwoFactorEnabledAsync(TKey userId);

        /// <summary>Gets valid two factor providers.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The valid two factor providers.</returns>
        Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId);

        /// <summary>Has password.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> HasPasswordAsync(TKey userId);

        /// <summary>Is email confirmed.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> IsEmailConfirmedAsync(TKey userId);

        /// <summary>Is in role.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="role">  The role.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> IsInRoleAsync(TKey userId, string role);

        /// <summary>Is locked out.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> IsLockedOutAsync(TKey userId);

        /// <summary>Is phone number confirmed.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> IsPhoneNumberConfirmedAsync(TKey userId);

        /// <summary>Notifies a two factor token.</summary>
        /// <param name="userId">           Identifier for the user.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="token">            The token.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token);

        /// <summary>Registers the two factor provider.</summary>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="provider">         The provider.</param>
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<TUser, TKey> provider);

        /// <summary>Removes the claim.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="claim"> The claim.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> RemoveClaimAsync(TKey userId, Claim claim);

        /// <summary>Removes from role.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="role">  The role.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role);

        /// <summary>Removes from roles.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="roles"> A variable-length parameters list containing roles.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> RemoveFromRolesAsync(TKey userId, params string[] roles);

        /// <summary>Removes the login.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="login"> The login.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login);

        /// <summary>Removes the password described by userId.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> RemovePasswordAsync(TKey userId);

        /// <summary>Resets the access failed count described by userId.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId);

        /// <summary>Resets the password.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="token">      The token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword);

        /// <summary>Sends an email.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">   The body.</param>
        /// <returns>A Task.</returns>
        Task SendEmailAsync(TKey userId, string subject, string body);

        /// <summary>Sends the SMS.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="message">The message.</param>
        /// <returns>A Task.</returns>
        Task SendSmsAsync(TKey userId, string message);

        /// <summary>Sets email.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="email"> The email.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> SetEmailAsync(TKey userId, string email);

        /// <summary>Sets lockout enabled.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="enabled">True to enable, false to disable.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled);

        /// <summary>Sets lockout end date.</summary>
        /// <param name="userId">    Identifier for the user.</param>
        /// <param name="lockoutEnd">The lockout end.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd);

        /// <summary>Sets phone number.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber);

        /// <summary>Sets two factor enabled.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="enabled">True to enable, false to disable.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled);

        /// <summary>Updates the described by user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> UpdateAsync(TUser user);

        /// <summary>Updates the security stamp described by userId.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        Task<IdentityResult> UpdateSecurityStampAsync(TKey userId);

        /// <summary>Verify change phone number token.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="token">      The token.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber);

        /// <summary>Verify two factor token.</summary>
        /// <param name="userId">           Identifier for the user.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="token">            The token.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token);

        /// <summary>Verify user token.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="purpose">The purpose.</param>
        /// <param name="token">  The token.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string token);

        /// <summary>Creates security token.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The new security token.</returns>
        Task<SecurityToken> CreateSecurityTokenAsync(TKey userId);
    }

    /// <summary>Exposes user related api which will automatically save changes to the UserStore.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    /// <seealso cref="IDisposable"/>
    /// <seealso cref="Microsoft.AspNet.Identity2.IUserManager{TUser,TKey}"/>
    public class UserManager<TUser, TKey> : IDisposable, IUserManager<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>(Immutable) The factors.</summary>
        private readonly Dictionary<string, IUserTokenProvider<TUser, TKey>> factors = new();

        /// <summary>The claims factory.</summary>
        private IClaimsIdentityFactory<TUser, TKey> claimsFactory;

        /// <summary>True if disposed.</summary>
        private bool disposed;

        /// <summary>The password hasher.</summary>
        private IPasswordHasher passwordHasher;

        /// <summary>The password validator.</summary>
        private IIdentityValidator<string> passwordValidator;

        /// <summary>The user validator.</summary>
        private IIdentityValidator<TUser> userValidator;

        /// <summary>Initializes a new instance of the <see cref="UserManager{TUser, TKey}"/> class.</summary>
        /// <param name="store">The IUserStore is responsible for commiting changes via the UpdateAsync/CreateAsync
        ///                     methods.</param>
        public UserManager(IUserStore<TUser, TKey> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            userValidator = new UserValidator<TUser, TKey>(this);
            passwordValidator = new MinimumLengthValidator(6);
            passwordHasher = new PasswordHasher();
            claimsFactory = new ClaimsIdentityFactory<TUser, TKey>();
        }

        /// <summary>Gets or sets the hasher used to hash/verify passwords.</summary>
        /// <value>The password hasher.</value>
        public IPasswordHasher PasswordHasher
        {
            get
            {
                ThrowIfDisposed();
                return passwordHasher;
            }

            set
            {
                ThrowIfDisposed();
                passwordHasher = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Gets or sets the validator used to validate users before changes are saved.</summary>
        /// <value>The user validator.</value>
        public IIdentityValidator<TUser> UserValidator
        {
            get
            {
                ThrowIfDisposed();
                return userValidator;
            }

            set
            {
                ThrowIfDisposed();
                userValidator = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Gets or sets the validator used to validate passwords before persisting changes.</summary>
        /// <value>The password validator.</value>
        public IIdentityValidator<string> PasswordValidator
        {
            get
            {
                ThrowIfDisposed();
                return passwordValidator;
            }

            set
            {
                ThrowIfDisposed();
                passwordValidator = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Gets or sets the factory used to create claims identities from users.</summary>
        /// <value>The claims identity factory.</value>
        public IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory
        {
            get
            {
                ThrowIfDisposed();
                return claimsFactory;
            }

            set
            {
                ThrowIfDisposed();
                claimsFactory = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>Gets or sets the service used to send emails.</summary>
        /// <value>The email service.</value>
        public IIdentityMessageService? EmailService { get; set; }

        /// <summary>Gets or sets the used to send SMS messages.</summary>
        /// <value>The SMS service.</value>
        public IIdentityMessageService? SmsService { get; set; }

        /// <summary>Gets or sets the provider used for generating reset password and confirmation tokens.</summary>
        /// <value>The user token provider.</value>
        public IUserTokenProvider<TUser, TKey>? UserTokenProvider { get; set; }

        /// <summary>Gets or sets a value indicating whether this user manager will enable user lockout when users are
        /// created.</summary>
        /// <value>True if user lockout enabled by default, false if not.</value>
        public bool UserLockoutEnabledByDefault { get; set; }

        /// <summary>Gets or sets the number of access attempts allowed before a user is locked out (if lockout is
        /// enabled).</summary>
        /// <value>The maximum failed access attempts before lockout.</value>
        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }

        /// <summary>Gets or sets the default amount of time that a user is locked out for after
        /// MaxFailedAccessAttemptsBeforeLockout is reached.</summary>
        /// <value>The default account lockout time span.</value>
        public TimeSpan DefaultAccountLockoutTimeSpan { get; set; } = TimeSpan.Zero;

        /// <summary>Gets a value indicating whether the store is an IUserTwoFactorStore.</summary>
        /// <value>True if supports user two factor, false if not.</value>
        public virtual bool SupportsUserTwoFactor
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserTwoFactorStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserPasswordStore.</summary>
        /// <value>True if supports user password, false if not.</value>
        public virtual bool SupportsUserPassword
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserPasswordStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserSecurityStore.</summary>
        /// <value>True if supports user security stamp, false if not.</value>
        public virtual bool SupportsUserSecurityStamp
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserSecurityStampStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserRoleStore.</summary>
        /// <value>True if supports user role, false if not.</value>
        public virtual bool SupportsUserRole
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserRoleStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserLoginStore.</summary>
        /// <value>True if supports user login, false if not.</value>
        public virtual bool SupportsUserLogin
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserLoginStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserEmailStore.</summary>
        /// <value>True if supports user email, false if not.</value>
        public virtual bool SupportsUserEmail
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserEmailStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserPhoneNumberStore.</summary>
        /// <value>True if supports user phone number, false if not.</value>
        public virtual bool SupportsUserPhoneNumber
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserPhoneNumberStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserClaimStore.</summary>
        /// <value>True if supports user claim, false if not.</value>
        public virtual bool SupportsUserClaim
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserClaimStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IUserLockoutStore.</summary>
        /// <value>True if supports user lockout, false if not.</value>
        public virtual bool SupportsUserLockout
        {
            get
            {
                ThrowIfDisposed();
                return Store is IUserLockoutStore<TUser, TKey>;
            }
        }

        /// <summary>Gets a value indicating whether the store is an IQueryableUserStore.</summary>
        /// <value>True if supports queryable users, false if not.</value>
        public virtual bool SupportsQueryableUsers
        {
            get
            {
                ThrowIfDisposed();
                return Store is IQueryableUserStore<TUser, TKey>;
            }
        }

        /// <summary>Gets an IQueryable of users if the store is an IQueryableUserStore.</summary>
        /// <value>The users.</value>
        public virtual IQueryable<TUser> Users => (Store as IQueryableUserStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IQueryableUserStore"/*Resources.StoreNotIQueryableUserStore*/))
            .Users;

        /// <summary>Gets the registered two-factor authentication providers for users by their id.</summary>
        /// <value>The two factor providers.</value>
        public IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders => factors;

        /// <summary>Gets the persistence abstraction that the UserManager operates against.</summary>
        /// <value>The store.</value>
        protected internal IUserStore<TUser, TKey> Store { get; }

        /// <summary>Dispose this object.</summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Creates a ClaimsIdentity representing the user.</summary>
        /// <param name="user">              The user.</param>
        /// <param name="authenticationType">Type of the authentication.</param>
        /// <returns>The new identity.</returns>
        public virtual Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType)
        {
            ThrowIfDisposed();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            throw new NotSupportedException();
            /*
            return ClaimsIdentityFactory.CreateAsync(this, user, authenticationType);
            */
        }

        /// <summary>Create a user with no password.</summary>
        /// <param name="user">The user.</param>
        /// <returns>The new.</returns>
        public virtual async Task<IdentityResult> CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            var identityResult = await UserValidator.ValidateAsync(user).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            if (UserLockoutEnabledByDefault && SupportsUserLockout)
            {
                await GetUserLockoutStore().SetLockoutEnabledAsync(user, enabled: true).WithCurrentCulture();
            }
            await Store.CreateAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Update a user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var identityResult = await UserValidator.ValidateAsync(user).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            await Store.UpdateAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Delete a user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            await Store.DeleteAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Find a user by id.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The found identifier.</returns>
        public virtual Task<TUser?> FindByIdAsync(TKey userId)
        {
            ThrowIfDisposed();
            return Store.FindByIdAsync(userId)!;
        }

        /// <summary>Find a user by user name.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>The found name.</returns>
        public virtual Task<TUser?> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            if (userName is null)
            {
                throw new ArgumentNullException(nameof(userName));
            }
            return Store.FindByNameAsync(userName)!;
        }

        /// <summary>Find a user by his email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>The found email.</returns>
        public virtual Task<TUser?> FindByEmailAsync(string email)
        {
            ThrowIfDisposed();
            var emailStore = GetEmailStore();
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            return emailStore.FindByEmailAsync(email)!;
        }

        /// <summary>Return a user with the specified username and password or null if there is no match.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The found.</returns>
        public virtual async Task<TUser?> FindAsync(string userName, string password)
        {
            ThrowIfDisposed();
            var user = await FindByNameAsync(userName).WithCurrentCulture();
            if (user is null)
            {
                return null;
            }
            return await CheckPasswordAsync(user, password).WithCurrentCulture() ? user : null;
        }

        /// <summary>Returns the user associated with this login.</summary>
        /// <param name="login">The login.</param>
        /// <returns>The found.</returns>
        public virtual Task<TUser?> FindAsync(UserLoginInfo login)
        {
            ThrowIfDisposed();
            return GetLoginStore().FindAsync(login)!;
        }

        /// <summary>Create a user with the given password.</summary>
        /// <param name="user">    The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>The new.</returns>
        public virtual async Task<IdentityResult> CreateAsync(TUser? user, string? password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            var identityResult = await UpdatePasswordAsync(passwordStore, user, password).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            return await CreateAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns true if the password is valid for the user.</summary>
        /// <param name="user">    The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> CheckPasswordAsync(TUser? user, string password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            if (user is null)
            {
                return false;
            }
            return await VerifyPasswordAsync(passwordStore, user, password).WithCurrentCulture();
        }

        /// <summary>Returns true if the user has a password.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> HasPasswordAsync(TKey userId)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await passwordStore.HasPasswordAsync(val).WithCurrentCulture();
        }

        /// <summary>Add a user password only if one does not already exist.</summary>
        /// <param name="userId">  Identifier for the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> AddPasswordAsync(TKey userId, string password)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (await passwordStore.GetPasswordHashAsync(user).WithCurrentCulture() != null)
            {
                return new("User already has a password"/*Resources.UserAlreadyHasPassword*/);
            }
            var identityResult = await UpdatePasswordAsync(passwordStore, user, password).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Change a user password.</summary>
        /// <param name="userId">         Identifier for the user.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">    The new password.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!await VerifyPasswordAsync(passwordStore, user, currentPassword).WithCurrentCulture())
            {
                return IdentityResult.Failed("Password mismatch" /*Resources.PasswordMismatch*/);
            }
            var identityResult = await UpdatePasswordAsync(passwordStore, user, newPassword).WithCurrentCulture();
            if (identityResult.Succeeded)
            {
                return await UpdateAsync(user).WithCurrentCulture();
            }
            return identityResult;
        }

        /// <summary>Remove a user's password.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> RemovePasswordAsync(TKey userId)
        {
            ThrowIfDisposed();
            var passwordStore = GetPasswordStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await passwordStore.SetPasswordHashAsync(user, null).WithCurrentCulture();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns the current security stamp for a user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The security stamp.</returns>
        public virtual async Task<string> GetSecurityStampAsync(TKey userId)
        {
            ThrowIfDisposed();
            var securityStore = GetSecurityStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await securityStore.GetSecurityStampAsync(val).WithCurrentCulture();
        }

        /// <summary>Generate a new security stamp for a user, used for SignOutEverywhere functionality.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> UpdateSecurityStampAsync(TKey userId)
        {
            ThrowIfDisposed();
            var securityStore = GetSecurityStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await securityStore.SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Generate a password reset token for the user using the UserTokenProvider.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The password reset token.</returns>
        public virtual Task<string> GeneratePasswordResetTokenAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync("ResetPassword", userId);
        }

        /// <summary>Reset a user's password using a reset password token.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="token">      The token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword)
        {
            ThrowIfDisposed();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!await VerifyUserTokenAsync(userId, "ResetPassword", token).WithCurrentCulture())
            {
                return IdentityResult.Failed("Invalid token"/*Resources.InvalidToken*/);
            }
            var passwordStore = GetPasswordStore();
            var identityResult = await UpdatePasswordAsync(passwordStore, user, newPassword).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove a user login.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="login"> The login.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            if (login is null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await loginStore.RemoveLoginAsync(user, login).WithCurrentCulture();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Associate a login with a user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="login"> The login.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            if (login is null)
            {
                throw new ArgumentNullException(nameof(login));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (await FindAsync(login).WithCurrentCulture() != null)
            {
                return IdentityResult.Failed("External Login Exists"/*Resources.ExternalLoginExists*/);
            }
            await loginStore.AddLoginAsync(user, login).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Gets the logins for a user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The logins.</returns>
        public virtual async Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId)
        {
            ThrowIfDisposed();
            var loginStore = GetLoginStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await loginStore.GetLoginsAsync(val).WithCurrentCulture();
        }

        /// <summary>Add a user claim.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="claim"> The claim.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            if (claim is null)
            {
                throw new ArgumentNullException(nameof(claim));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await claimStore.AddClaimAsync(user, claim).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove a user claim.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="claim"> The claim.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> RemoveClaimAsync(TKey userId, Claim claim)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await claimStore.RemoveClaimAsync(user, claim).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Get a user's claims.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The claims.</returns>
        public virtual async Task<IList<Claim>> GetClaimsAsync(TKey userId)
        {
            ThrowIfDisposed();
            var claimStore = GetClaimStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await claimStore.GetClaimsAsync(val).WithCurrentCulture();
        }

        /// <summary>Add a user to a role.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="role">  The role.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> AddToRoleAsync(TKey userId, string role)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if ((await userRoleStore.GetRolesAsync(user).WithCurrentCulture()).Contains(role))
            {
                return new("User already in role"/*Resources.UserAlreadyInRole*/);
            }
            await userRoleStore.AddToRoleAsync(user, role).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Method to add user to multiple roles.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="roles"> A variable-length parameters list containing roles.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> AddToRolesAsync(TKey userId, params string[] roles)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            if (roles is null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            IList<string> userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
            foreach (var text in roles)
            {
                if (userRoles.Contains(text))
                {
                    return new("User already in role"/*Resources.UserAlreadyInRole*/);
                }
                await userRoleStore.AddToRoleAsync(user, text).WithCurrentCulture();
            }
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove user from multiple roles.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="roles"> A variable-length parameters list containing roles.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> RemoveFromRolesAsync(TKey userId, params string[] roles)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            if (roles is null)
            {
                throw new ArgumentNullException(nameof(roles));
            }
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            IList<string> userRoles = await userRoleStore.GetRolesAsync(user).WithCurrentCulture();
            foreach (var text in roles)
            {
                if (!userRoles.Contains(text))
                {
                    return new("User not in role"/*Resources.UserNotInRole*/);
                }
                await userRoleStore.RemoveFromRoleAsync(user, text).WithCurrentCulture();
            }
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Remove a user from a role.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="role">  The role.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!await userRoleStore.IsInRoleAsync(user, role).WithCurrentCulture())
            {
                return new("User not in role"/*Resources.UserNotInRole*/);
            }
            await userRoleStore.RemoveFromRoleAsync(user, role).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns the roles for the user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The roles.</returns>
        public virtual async Task<IList<string>> GetRolesAsync(TKey userId)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await userRoleStore.GetRolesAsync(val).WithCurrentCulture();
        }

        /// <summary>Returns true if the user is in the specified role.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="role">  The role.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> IsInRoleAsync(TKey userId, string role)
        {
            ThrowIfDisposed();
            var userRoleStore = GetUserRoleStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await userRoleStore.IsInRoleAsync(val, role).WithCurrentCulture();
        }

        /// <summary>Get a user's email.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The email.</returns>
        public virtual async Task<string> GetEmailAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await store.GetEmailAsync(val).WithCurrentCulture();
        }

        /// <summary>Set a user's email.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="email"> The email.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> SetEmailAsync(TKey userId, string email)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await store.SetEmailAsync(user, email).WithCurrentCulture();
            await store.SetEmailConfirmedAsync(user, confirmed: false).WithCurrentCulture();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Get the email confirmation token for the user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The email confirmation token.</returns>
        public virtual Task<string> GenerateEmailConfirmationTokenAsync(TKey userId)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync("Confirmation", userId);
        }

        /// <summary>Confirm the user's email with confirmation token.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <param name="token"> The token.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> ConfirmEmailAsync(TKey userId, string? token)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!await VerifyUserTokenAsync(userId, "Confirmation", token).WithCurrentCulture())
            {
                return IdentityResult.Failed("Invalid token"/*Resources.InvalidToken*/);
            }
            await store.SetEmailConfirmedAsync(user, confirmed: true).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns true if the user's email has been confirmed.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> IsEmailConfirmedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await store.GetEmailConfirmedAsync(val).WithCurrentCulture();
        }

        /// <summary>Get a user's phoneNumber.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The phone number.</returns>
        public virtual async Task<string> GetPhoneNumberAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await store.GetPhoneNumberAsync(val).WithCurrentCulture();
        }

        /// <summary>Set a user's phoneNumber.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
            await store.SetPhoneNumberConfirmedAsync(user, confirmed: false).WithCurrentCulture();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Set a user's phoneNumber with the verification token.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="token">      The token.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!await VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber).WithCurrentCulture())
            {
                return IdentityResult.Failed("Invalid token" /*Resources.InvalidToken*/);
            }
            await store.SetPhoneNumberAsync(user, phoneNumber).WithCurrentCulture();
            await store.SetPhoneNumberConfirmedAsync(user, confirmed: true).WithCurrentCulture();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns true if the user's phone number has been confirmed.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> IsPhoneNumberConfirmedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetPhoneNumberStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await store.GetPhoneNumberConfirmedAsync(val).WithCurrentCulture();
        }

        /// <summary>Generate a code that the user can use to change their phone number to a specific number.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The change phone number token.</returns>
        public virtual async Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber)
        {
            ThrowIfDisposed();
            return Rfc6238AuthenticationService.GenerateCode(
                    await CreateSecurityTokenAsync(userId).WithCurrentCulture(),
                    phoneNumber)
                .ToString("D6", CultureInfo.InvariantCulture);
        }

        /// <summary>Verify the code is valid for a specific user and for a specific phone number.</summary>
        /// <param name="userId">     Identifier for the user.</param>
        /// <param name="token">      The token.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber)
        {
            ThrowIfDisposed();
            var securityToken = await CreateSecurityTokenAsync(userId).WithCurrentCulture();
            if (securityToken != null && int.TryParse(token, out var result))
            {
                return Rfc6238AuthenticationService.ValidateCode(securityToken, result, phoneNumber);
            }
            return false;
        }

        /// <summary>Verify a user token with the specified purpose.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="purpose">The purpose.</param>
        /// <param name="token">  The token.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string? token)
        {
            ThrowIfDisposed();
            if (UserTokenProvider is null)
            {
                throw new NotSupportedException("No token provider"/*Resources.NoTokenProvider*/);
            }
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await UserTokenProvider.ValidateAsync(purpose, token, this, val).WithCurrentCulture();
        }

        /// <summary>Get a user token for a specific purpose.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="userId"> Identifier for the user.</param>
        /// <returns>The user token.</returns>
        public virtual async Task<string> GenerateUserTokenAsync(string purpose, TKey userId)
        {
            ThrowIfDisposed();
            if (UserTokenProvider is null)
            {
                throw new NotSupportedException("No token provider"/*Resources.NoTokenProvider*/);
            }
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await UserTokenProvider.GenerateAsync(purpose, this, val).WithCurrentCulture();
        }

        /// <summary>Register a two factor authentication provider with the TwoFactorProviders mapping.</summary>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="provider">         The provider.</param>
        public virtual void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<TUser, TKey> provider)
        {
            ThrowIfDisposed();
            if (twoFactorProvider is null)
            {
                throw new ArgumentNullException(nameof(twoFactorProvider));
            }
            TwoFactorProviders[twoFactorProvider] = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>Returns a list of valid two factor providers for a user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The valid two factor providers.</returns>
        public virtual async Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId)
        {
            ThrowIfDisposed();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            var results = new List<string>();
            foreach (var f in TwoFactorProviders)
            {
                if (await f.Value.IsValidProviderForUserAsync(this, user).WithCurrentCulture())
                {
                    results.Add(f.Key);
                }
            }
            return results;
        }

        /// <summary>Verify a two factor token with the specified provider.</summary>
        /// <param name="userId">           Identifier for the user.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="token">            The token.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
        {
            ThrowIfDisposed();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "No two factor provider '{0}' found"/*Resources.NoTwoFactorProvider*/,
                        twoFactorProvider));
            }
            return await factors[twoFactorProvider].ValidateAsync(twoFactorProvider, token, this, val).WithCurrentCulture();
        }

        /// <summary>Get a token for a specific two factor provider.</summary>
        /// <param name="userId">           Identifier for the user.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <returns>The two factor token.</returns>
        public virtual async Task<string> GenerateTwoFactorTokenAsync(TKey userId, string twoFactorProvider)
        {
            ThrowIfDisposed();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "No two factor provider '{0}' found"/*Resources.NoTwoFactorProvider*/,
                        twoFactorProvider));
            }
            return await factors[twoFactorProvider].GenerateAsync(twoFactorProvider, this, val).WithCurrentCulture();
        }

        /// <summary>Notify a user with a token using a specific two-factor authentication provider's Notify method.</summary>
        /// <param name="userId">           Identifier for the user.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="token">            The token.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token)
        {
            ThrowIfDisposed();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!factors.ContainsKey(twoFactorProvider))
            {
                throw new NotSupportedException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "No two factor provider '{0}' found"/*Resources.NoTwoFactorProvider*/,
                        twoFactorProvider));
            }
            await factors[twoFactorProvider].NotifyAsync(token, this, val).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>Get whether two factor authentication is enabled for a user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The two factor enabled.</returns>
        public virtual async Task<bool> GetTwoFactorEnabledAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserTwoFactorStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await store.GetTwoFactorEnabledAsync(val).WithCurrentCulture();
        }

        /// <summary>Set whether a user has two factor authentication enabled.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="enabled">True to enable, false to disable.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled)
        {
            ThrowIfDisposed();
            var store = GetUserTwoFactorStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            await store.SetTwoFactorEnabledAsync(user, enabled).WithCurrentCulture();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Send an email to the user.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">   The body.</param>
        /// <returns>A Task.</returns>
        public virtual async Task SendEmailAsync(TKey userId, string subject, string body)
        {
            ThrowIfDisposed();
            if (EmailService is null)
            {
                return;
            }
            var identityMessage = new IdentityMessage();
            var identityMessage2 = identityMessage;
            identityMessage2.Destination = await GetEmailAsync(userId).WithCurrentCulture();
            identityMessage.Subject = subject;
            identityMessage.Body = body;
            await EmailService.SendAsync(identityMessage).WithCurrentCulture();
        }

        /// <summary>Send a user a sms message.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="message">The message.</param>
        /// <returns>A Task.</returns>
        public virtual async Task SendSmsAsync(TKey userId, string message)
        {
            ThrowIfDisposed();
            if (SmsService != null)
            {
                var identityMessage = new IdentityMessage();
                var identityMessage2 = identityMessage;
                identityMessage2.Destination = await GetPhoneNumberAsync(userId).WithCurrentCulture();
                identityMessage.Body = message;
                await SmsService.SendAsync(identityMessage).WithCurrentCulture();
            }
        }

        /// <summary>Returns true if the user is locked out.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> IsLockedOutAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!await store.GetLockoutEnabledAsync(user).WithCurrentCulture())
            {
                return false;
            }
            return await store.GetLockoutEndDateAsync(user).WithCurrentCulture() >= DateTimeOffset.UtcNow;
        }

        /// <summary>Sets whether lockout is enabled for this user.</summary>
        /// <param name="userId"> Identifier for the user.</param>
        /// <param name="enabled">True to enable, false to disable.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }

            await store.SetLockoutEnabledAsync(user, enabled).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns whether lockout is enabled for the user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The lockout enabled.</returns>
        public virtual async Task<bool> GetLockoutEnabledAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }

            return await store.GetLockoutEnabledAsync(val).WithCurrentCulture();
        }

        /// <summary>Returns when the user is no longer locked out, dates in the past are considered as not being locked
        /// out.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The lockout end date.</returns>
        public virtual async Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }

            return await store.GetLockoutEndDateAsync(val).WithCurrentCulture();
        }

        /// <summary>Sets the when a user lockout ends.</summary>
        /// <param name="userId">    Identifier for the user.</param>
        /// <param name="lockoutEnd">The lockout end.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (!await store.GetLockoutEnabledAsync(user).WithCurrentCulture())
            {
                return IdentityResult.Failed("Lockout not enabled"/*Resources.LockoutNotEnabled*/);
            }
            await store.SetLockoutEndDateAsync(user, lockoutEnd).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Increments the access failed count for the user and if the failed access account is greater than or
        /// equal to the MaxFailedAccessAttemptsBeforeLockout, the user will be locked out for the next
        /// DefaultAccountLockoutTimeSpan and the AccessFailedCount will be reset to 0. This is used for locking out the
        /// user account.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> AccessFailedAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            // ReSharper disable once InvertIf
            if (await store.IncrementAccessFailedCountAsync(user).WithCurrentCulture() >= MaxFailedAccessAttemptsBeforeLockout)
            {
                await store.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.Add(DefaultAccountLockoutTimeSpan)).WithCurrentCulture();
                await store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
            }
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Resets the access failed count for the user to 0.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var user = await FindByIdAsync(userId).WithCurrentCulture();
            if (user is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            if (await GetAccessFailedCountAsync(user.Id).WithCurrentCulture() == 0)
            {
                return IdentityResult.Success;
            }
            await store.ResetAccessFailedCountAsync(user).WithCurrentCulture();
            return await UpdateAsync(user).WithCurrentCulture();
        }

        /// <summary>Returns the number of failed access attempts for the user.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The access failed count.</returns>
        public virtual async Task<int> GetAccessFailedCountAsync(TKey userId)
        {
            ThrowIfDisposed();
            var store = GetUserLockoutStore();
            var val = await FindByIdAsync(userId).WithCurrentCulture();
            if (val is null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "User Id '{0}' Not Found"/*Resources.UserIdNotFound*/,
                        userId));
            }
            return await store.GetAccessFailedCountAsync(val).WithCurrentCulture();
        }

        /// <summary>Gets email store.</summary>
        /// <returns>The email store.</returns>
        public IUserEmailStore<TUser, TKey> GetEmailStore()
        {
            return Store as IUserEmailStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserEmailStore"/*Resources.StoreNotIUserEmailStore*/);
        }

        /// <summary>Creates security token.</summary>
        /// <param name="userId">Identifier for the user.</param>
        /// <returns>The new security token.</returns>
        public async Task<SecurityToken> CreateSecurityTokenAsync(TKey userId)
        {
            var unicode = Encoding.Unicode;
            return new(unicode.GetBytes(await GetSecurityStampAsync(userId).WithCurrentCulture()));
        }

        /// <summary>Updates the security stamp internal described by user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>A Task.</returns>
        internal async Task UpdateSecurityStampInternalAsync(TUser user)
        {
            if (SupportsUserSecurityStamp)
            {
                await GetSecurityStore().SetSecurityStampAsync(user, NewSecurityStamp()).WithCurrentCulture();
            }
        }

        /// <summary>Gets phone number store.</summary>
        /// <returns>The phone number store.</returns>
        internal IUserPhoneNumberStore<TUser, TKey> GetPhoneNumberStore()
        {
            return Store as IUserPhoneNumberStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserPhoneNumberStore"/*Resources.StoreNotIUserPhoneNumberStore*/);
        }

        /// <summary>Gets user two factor store.</summary>
        /// <returns>The user two factor store.</returns>
        internal IUserTwoFactorStore<TUser, TKey> GetUserTwoFactorStore()
        {
            return Store as IUserTwoFactorStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserTwoFactorStore"/*Resources.StoreNotIUserTwoFactorStore*/);
        }

        /// <summary>Gets user lockout store.</summary>
        /// <returns>The user lockout store.</returns>
        internal IUserLockoutStore<TUser, TKey> GetUserLockoutStore()
        {
            return Store as IUserLockoutStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserLockoutStore"/*Resources.StoreNotIUserLockoutStore*/);
        }

        /// <summary>Updates the password.</summary>
        /// <param name="passwordStore">The password store.</param>
        /// <param name="user">         The user.</param>
        /// <param name="newPassword">  The new password.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        protected virtual async Task<IdentityResult> UpdatePasswordAsync(IUserPasswordStore<TUser, TKey> passwordStore, TUser user, string newPassword)
        {
            var identityResult = await PasswordValidator.ValidateAsync(newPassword).WithCurrentCulture();
            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            await passwordStore.SetPasswordHashAsync(user, PasswordHasher.HashPassword(newPassword)).WithCurrentCulture();
            await UpdateSecurityStampInternalAsync(user).WithCurrentCulture();
            return IdentityResult.Success;
        }

        /// <summary>By default, retrieves the hashed password from the user store and calls
        /// PasswordHasher.VerifyHashPassword.</summary>
        /// <param name="store">   The store.</param>
        /// <param name="user">    The user.</param>
        /// <param name="password">The password.</param>
        /// <returns>A Task{bool}.</returns>
        protected virtual async Task<bool> VerifyPasswordAsync(IUserPasswordStore<TUser, TKey> store, TUser user, string password)
        {
            var hashedPassword = await store.GetPasswordHashAsync(user).WithCurrentCulture();
            return PasswordHasher.VerifyHashedPassword(hashedPassword, password) != PasswordVerificationResult.Failed;
        }

        /// <summary>When disposing, actually dispose the store.</summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only
        ///                         unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || disposed)
            {
                return;
            }
            Store.Dispose();
            disposed = true;
        }

        /// <summary>Creates a new security stamp.</summary>
        /// <returns>A string.</returns>
        private static string NewSecurityStamp()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>Gets password store.</summary>
        /// <returns>The password store.</returns>
        private IUserPasswordStore<TUser, TKey> GetPasswordStore()
        {
            return Store as IUserPasswordStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserPasswordStore"/*Resources.StoreNotIUserPasswordStore*/);
        }

        /// <summary>Gets security store.</summary>
        /// <returns>The security store.</returns>
        private IUserSecurityStampStore<TUser, TKey> GetSecurityStore()
        {
            return Store as IUserSecurityStampStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserSecurityStampStore"/*Resources.StoreNotIUserSecurityStampStore*/);
        }

        /// <summary>Gets login store.</summary>
        /// <returns>The login store.</returns>
        private IUserLoginStore<TUser, TKey> GetLoginStore()
        {
            return Store as IUserLoginStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserLoginStore"/*Resources.StoreNotIUserLoginStore*/);
        }

        /// <summary>Gets claim store.</summary>
        /// <returns>The claim store.</returns>
        private IUserClaimStore<TUser, TKey> GetClaimStore()
        {
            return Store as IUserClaimStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserClaimStore"/*Resources.StoreNotIUserClaimStore*/);
        }

        /// <summary>Gets user role store.</summary>
        /// <returns>The user role store.</returns>
        private IUserRoleStore<TUser, TKey> GetUserRoleStore()
        {
            return Store as IUserRoleStore<TUser, TKey>
                ?? throw new NotSupportedException("Store Not IUserRoleStore"/*Resources.StoreNotIUserRoleStore*/);
        }

        /// <summary>Throw if disposed.</summary>
        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Utilities;
    using System.Globalization;
    using System.Net.Mail;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Identity;

    /// <summary>Validates users before they are saved.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    /// <seealso cref="Microsoft.AspNet.Identity.IIdentityValidator{TUser}"/>
    public class UserValidator<TUser, TKey>
        : IIdentityValidator<TUser>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Initializes a new instance of the <see cref="UserValidator{TUser, TKey}"/> class.</summary>
        /// <param name="manager">The manager.</param>
        public UserValidator(IUserManager<TUser, TKey> manager)
        {
            Manager = manager ?? throw new ArgumentNullException(nameof(manager));
            AllowOnlyAlphanumericUserNames = true;
        }

        /// <summary>Gets or sets a value indicating whether this validator enforces [A-Za-z0-9@_] in UserNames.</summary>
        /// <value>True if allow only alphanumeric user names, false if not.</value>
        public bool AllowOnlyAlphanumericUserNames { get; set; }

        /// <summary>Gets or sets a value indicating whether this validator enforces that emails are non empty, valid,
        /// and unique.</summary>
        /// <value>True if require unique email, false if not.</value>
        public bool RequireUniqueEmail { get; set; }

        /// <summary>Gets the manager.</summary>
        /// <value>The manager.</value>
        private IUserManager<TUser, TKey> Manager { get; }

        /// <summary>Validates a user before saving.</summary>
        /// <param name="item">The item.</param>
        /// <returns>A Task{IdentityResult}.</returns>
        public virtual async Task<IdentityResult> ValidateAsync(TUser item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var errors = new List<string>();
            await ValidateUserNameAsync(item, errors).WithCurrentCulture();
            if (RequireUniqueEmail)
            {
                await ValidateEmailAsync(item, errors).WithCurrentCulture();
            }
            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }
            return IdentityResult.Success;
        }

        /// <summary>Validates the user name.</summary>
        /// <param name="user">  The user.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>A Task.</returns>
        private async Task ValidateUserNameAsync(TUser user, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                errors.Add(string.Format(
                    CultureInfo.CurrentCulture,
                    "Property '{0}' is too short"/*Resources.PropertyTooShort*/,
                    nameof(user.UserName)));
                return;
            }
            if (AllowOnlyAlphanumericUserNames && !Regex.IsMatch(user.UserName, "^[A-Za-z0-9@_\\.]+$"))
            {
                errors.Add(string.Format(
                    CultureInfo.CurrentCulture,
                    "UserName '{0}' is invalid"/*Resources.InvalidUserName*/,
                    user.UserName));
                return;
            }
            var val = await Manager.FindByNameAsync(user.UserName).WithCurrentCulture();
            if (val != null && !EqualityComparer<TKey>.Default.Equals(val.Id, user.Id))
            {
                errors.Add(string.Format(
                    CultureInfo.CurrentCulture,
                    "UserName '{0}' is a duplicate"/*Resources.DuplicateName*/,
                    user.UserName));
            }
        }

        /// <summary>Validates the email.</summary>
        /// <param name="user">  The user.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>A Task.</returns>
        private async Task ValidateEmailAsync(TUser user, List<string> errors)
        {
            var email = await Manager.GetEmailStore().GetEmailAsync(user).WithCurrentCulture();
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(string.Format(
                    CultureInfo.CurrentCulture,
                    "Property '{0}' is too short"/*Resources.PropertyTooShort*/,
                    "Email"));
                return;
            }
            try
            {
                _ = new MailAddress(email);
            }
            catch (FormatException)
            {
                errors.Add(string.Format(
                    CultureInfo.CurrentCulture,
                    "Email '{0}' is invalid"/*Resources.InvalidEmail*/,
                    email));
                return;
            }
            var val = await Manager.FindByEmailAsync(email).WithCurrentCulture();
            if (val != null && !EqualityComparer<TKey>.Default.Equals(val.Id, user.Id))
            {
                errors.Add(string.Format(
                    CultureInfo.CurrentCulture,
                    "Email '{0}' is a duplicate"/*Resources.DuplicateEmail*/,
                    email));
            }
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2
{
    /// <summary>A security token. This class cannot be inherited.</summary>
    public sealed class SecurityToken
    {
        /// <summary>(Immutable) The data.</summary>
        private readonly byte[] data;

        /// <summary>Initializes a new instance of the <see cref="SecurityToken"/> class.</summary>
        /// <param name="data">The data.</param>
        public SecurityToken(byte[] data)
        {
            this.data = (byte[])data.Clone();
        }

        /// <summary>Gets data no clone.</summary>
        /// <returns>An array of byte.</returns>
        internal byte[] GetDataNoClone()
        {
            return data;
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2
{
    using System;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>A rfc 6238 authentication service.</summary>
    internal static class Rfc6238AuthenticationService
    {
        private static readonly DateTime UnixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>(Immutable) The timestep.</summary>
        private static readonly TimeSpan Timestep = TimeSpan.FromMinutes(3.0);

        /// <summary>(Immutable) The encoding.</summary>
        private static readonly Encoding Encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        /// <summary>Generates a code.</summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="modifier">     The modifier.</param>
        /// <returns>The code.</returns>
        public static int GenerateCode(SecurityToken securityToken, string? modifier = null)
        {
            if (securityToken is null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }
            var currentTimeStepNumber = GetCurrentTimeStepNumber();
            using var hashAlgorithm = new HMACSHA1(securityToken.GetDataNoClone());
            return ComputeTotp(hashAlgorithm, currentTimeStepNumber, modifier);
        }

        /// <summary>Validates the code.</summary>
        /// <param name="securityToken">The security token.</param>
        /// <param name="code">         The code.</param>
        /// <param name="modifier">     The modifier.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool ValidateCode(SecurityToken securityToken, int code, string? modifier = null)
        {
            if (securityToken is null)
            {
                throw new ArgumentNullException(nameof(securityToken));
            }
            var currentTimeStepNumber = GetCurrentTimeStepNumber();
            using var hashAlgorithm = new HMACSHA1(securityToken.GetDataNoClone());
            for (var i = -2; i <= 2; i++)
            {
                // ReSharper disable once IntVariableOverflowInUncheckedContext
                if (ComputeTotp(hashAlgorithm, currentTimeStepNumber + (ulong)i, modifier) == code)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Calculates the totp.</summary>
        /// <param name="hashAlgorithm"> The hash algorithm.</param>
        /// <param name="timestepNumber">The timestep number.</param>
        /// <param name="modifier">      The modifier.</param>
        /// <returns>The calculated totp.</returns>
        private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timestepNumber, string? modifier)
        {
            var bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timestepNumber));
            var array = hashAlgorithm.ComputeHash(ApplyModifier(bytes, modifier));
            var num = array[^1] & 0xF;
            return (((array[num] & 0x7F) << 24) | ((array[num + 1] & 0xFF) << 16) | ((array[num + 2] & 0xFF) << 8) | (array[num + 3] & 0xFF)) % 1000000;
        }

        /// <summary>Applies the modifier.</summary>
        /// <param name="input">   The input.</param>
        /// <param name="modifier">The modifier.</param>
        /// <returns>A byte[].</returns>
        private static byte[] ApplyModifier(byte[] input, string? modifier)
        {
            if (string.IsNullOrEmpty(modifier))
            {
                return input;
            }
            var bytes = Encoding.GetBytes(modifier);
            var array = new byte[checked(input.Length + bytes.Length)];
            Buffer.BlockCopy(input, 0, array, 0, input.Length);
            Buffer.BlockCopy(bytes, 0, array, input.Length, bytes.Length);
            return array;
        }

        /// <summary>Gets current time step number.</summary>
        /// <returns>The current time step number.</returns>
        private static ulong GetCurrentTimeStepNumber()
        {
            return (ulong)((DateTime.UtcNow - UnixEpoch).Ticks / Timestep.Ticks);
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2
{
    using System;
    using System.Threading.Tasks;
    using Identity;

    /// <summary>Interface to generate user tokens.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    public interface IUserTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Generate a token for a user with a specific purpose.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>The.</returns>
        Task<string> GenerateAsync(string purpose, IUserManager<TUser, TKey> manager, TUser user);

        /// <summary>Validate a token for a user with a specific purpose.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> ValidateAsync(string purpose, string? token, IUserManager<TUser, TKey> manager, TUser user);

        /// <summary>Notifies the user that a token has been generated, for example an email or sms could be sent, or
        /// this can be a no-op.</summary>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task.</returns>
        Task NotifyAsync(string token, IUserManager<TUser, TKey> manager, TUser user);

        /// <summary>Returns true if provider can be used for this user, i.e. could require a user to have an email.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        Task<bool> IsValidProviderForUserAsync(IUserManager<TUser, TKey> manager, TUser user);
    }
}

#region Assembly Microsoft.AspNet.Identity.Owin, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// location unknown
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2.Owin2
{
    using System;
    using System.Data.Entity.Utilities;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using Identity;
    using Owin.Security.DataProtection;

    /// <summary>Token provider that uses an IDataProtector to generate encrypted tokens based off of the security
    /// stamp.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    /// <seealso cref="IUserTokenProvider{TUser,TKey}"/>
    public class DataProtectorTokenProvider<TUser, TKey>
        : Identity2.IUserTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>Initializes a new instance of the <see cref="DataProtectorTokenProvider{TUser, TKey}"/> class.</summary>
        /// <param name="protector">The protector.</param>
        public DataProtectorTokenProvider(IDataProtector protector)
        {
            Protector = protector ?? throw new ArgumentNullException(nameof(protector));
            TokenLifespan = TimeSpan.FromDays(1.0);
        }

        /// <summary>Gets the IDataProtector for the token.</summary>
        /// <value>The protector.</value>
        public IDataProtector Protector { get; }

        /// <summary>Gets the lifespan after which the token is considered expired.</summary>
        /// <value>The token lifespan.</value>
        public TimeSpan TokenLifespan { get; }

        /// <summary>Generate a protected string for a user.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>The.</returns>
        public async Task<string> GenerateAsync(string? purpose, IUserManager<TUser, TKey> manager, TUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var ms = new MemoryStream();
            using (var writer = ms.CreateWriter())
            {
                writer.Write(DateTimeOffset.UtcNow);
                writer.Write(Convert.ToString(user.Id, CultureInfo.InvariantCulture)!);
                writer.Write(purpose ?? string.Empty);
                string? text = null;
                if (manager.SupportsUserSecurityStamp)
                {
                    text = await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture();
                }
                writer.Write(text ?? string.Empty);
            }
            return Convert.ToBase64String(Protector.Protect(ms.ToArray()));
        }

        /// <summary>Return false if the token is not valid.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        public async Task<bool> ValidateAsync(string purpose, string? token, IUserManager<TUser, TKey> manager, TUser user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return false;
                }
                var stream = new MemoryStream(Protector.Unprotect(Convert.FromBase64String(token)));
                using var reader = stream.CreateReader();
                if (reader.ReadDateTimeOffset() + TokenLifespan < DateTimeOffset.UtcNow)
                {
                    return false;
                }
                if (!string.Equals(reader.ReadString(), Convert.ToString(user.Id, CultureInfo.InvariantCulture)))
                {
                    return false;
                }
                if (!string.Equals(reader.ReadString(), purpose))
                {
                    return false;
                }
                var stamp = reader.ReadString();
                if (reader.PeekChar() != -1)
                {
                    return false;
                }
                if (manager.SupportsUserSecurityStamp)
                {
                    return stamp == await manager.GetSecurityStampAsync(user.Id).WithCurrentCulture();
                }
                return stamp.Length == 0;
            }
            catch
            {
                // Do Nothing
            }
            return false;
        }

        /// <summary>Returns true if the provider can be used to generate tokens for this user.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        public Task<bool> IsValidProviderForUserAsync(IUserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(result: true);
        }

        /// <summary>This provider no-ops by default when asked to notify a user.</summary>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task.</returns>
        public Task NotifyAsync(string token, IUserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(0);
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.Owin, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.owin\2.2.3\lib\net45\Microsoft.AspNet.Identity.Owin.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2.Owin2
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>A stream extensions.</summary>
    internal static class StreamExtensions
    {
        /// <summary>(Immutable) The default encoding.</summary>
        internal static readonly Encoding DefaultEncoding
            = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);

        /// <summary>A Stream extension method that creates a reader.</summary>
        /// <param name="stream">The stream to act on.</param>
        /// <returns>The new reader.</returns>
        public static BinaryReader CreateReader(this Stream stream)
        {
            return new(stream, DefaultEncoding, leaveOpen: true);
        }

        /// <summary>A Stream extension method that creates a writer.</summary>
        /// <param name="stream">The stream to act on.</param>
        /// <returns>The new writer.</returns>
        public static BinaryWriter CreateWriter(this Stream stream)
        {
            return new(stream, DefaultEncoding, leaveOpen: true);
        }

        /// <summary>A BinaryReader extension method that reads date time offset.</summary>
        /// <param name="reader">The reader to act on.</param>
        /// <returns>The date time offset.</returns>
        public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
        {
            return new(reader.ReadInt64(), TimeSpan.Zero);
        }

        /// <summary>A BinaryWriter extension method that writes.</summary>
        /// <param name="writer">The writer to act on.</param>
        /// <param name="value"> The value.</param>
        public static void Write(this BinaryWriter writer, DateTimeOffset value)
        {
            writer.Write(value.UtcTicks);
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2
{
    using System;
    using System.Data.Entity.Utilities;
    using System.Globalization;
    using System.Threading.Tasks;
    using Identity;

    /// <summary>TokenProvider that generates tokens from the user's security stamp and notifies a user via their
    /// email.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    /// <seealso cref="TotpSecurityStampBasedTokenProvider{TUser,TKey}"/>
    public class EmailTokenProvider<TUser, TKey>
        : TotpSecurityStampBasedTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>The body.</summary>
        private string? body;

        /// <summary>The subject.</summary>
        private string? subject;

        /// <summary>Gets or sets the Email subject used when a token notification is received.</summary>
        /// <value>The subject.</value>
        public string Subject
        {
            get => subject ?? string.Empty;
            set => subject = value;
        }

        /// <summary>Gets or sets the Email body which should contain a formatted string which the token will be the only argument.</summary>
        /// <value>The body format.</value>
        public string BodyFormat
        {
            get => body ?? "{0}";
            set => body = value;
        }

        /// <summary>True if the user has an email set.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        public override async Task<bool> IsValidProviderForUserAsync(IUserManager<TUser, TKey> manager, TUser user)
        {
            var flag = !string.IsNullOrWhiteSpace(await manager.GetEmailAsync(user.Id).WithCurrentCulture());
            if (flag)
            {
                flag = await manager.IsEmailConfirmedAsync(user.Id).WithCurrentCulture();
            }
            return flag;
        }

        /// <summary>Returns the email of the user for entropy in the token.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>The user modifier.</returns>
        public override async Task<string> GetUserModifierAsync(string purpose, IUserManager<TUser, TKey> manager, TUser user)
        {
            return "Email:" + purpose + ":" + await manager.GetEmailAsync(user.Id).WithCurrentCulture();
        }

        /// <summary>Notifies the user with a token via email using the Subject and BodyFormat.</summary>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task.</returns>
        public override Task NotifyAsync(string token, IUserManager<TUser, TKey> manager, TUser user)
        {
            if (manager is null)
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

// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.PhoneNumberTokenProvider`2
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
namespace Microsoft.AspNet.Identity2
{
    using System;
    using System.Data.Entity.Utilities;
    using System.Globalization;
    using System.Threading.Tasks;
    using Identity;

    /// <summary>TokenProvider that generates tokens from the user's security stamp and notifies a user via their
    /// phone number.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    /// <seealso cref="TotpSecurityStampBasedTokenProvider{TUser,TKey}"/>
    public class PhoneNumberTokenProvider<TUser, TKey>
        : TotpSecurityStampBasedTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private string? body;

        /// <summary>Gets or sets the message contents which should contain a format string which the token will be the
        /// only argument.</summary>
        /// <value>The message format.</value>
        public string MessageFormat
        {
            get => this.body ?? "{0}";
            set => this.body = value;
        }

        /// <summary>Returns true if the user has a phone number set.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        public override async Task<bool> IsValidProviderForUserAsync(
            IUserManager<TUser, TKey> manager,
            TUser user)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            var isValid = !string.IsNullOrWhiteSpace(await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture());
            if (isValid)
            {
                isValid = await manager.IsPhoneNumberConfirmedAsync(user.Id).WithCurrentCulture();
            }
            return isValid;
        }

        /// <summary>Returns the phone number of the user for entropy in the token.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>The user modifier.</returns>
        public override async Task<string> GetUserModifierAsync(
            string purpose,
            IUserManager<TUser, TKey> manager,
            TUser user)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return "PhoneNumber:"
                + purpose
                + ":"
                + await manager.GetPhoneNumberAsync(user.Id).WithCurrentCulture();
        }

        /// <summary>Notifies the user with a token via sms using the MessageFormat.</summary>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task.</returns>
        public override Task NotifyAsync(
            string token,
            IUserManager<TUser, TKey> manager,
            TUser user)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return manager.SendSmsAsync(
                user.Id,
                string.Format(CultureInfo.CurrentCulture, MessageFormat, token));
        }
    }
}

#region Assembly Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// C:\Users\JJJ\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll
// Decompiled with ICSharpCode.Decompiler 6.1.0.5902
#endregion

namespace Microsoft.AspNet.Identity2
{
    using System;
    using System.Data.Entity.Utilities;
    using System.Globalization;
    using System.Threading.Tasks;
    using Identity;

    /// <summary>TokenProvider that generates time based codes using the user's security stamp.</summary>
    /// <typeparam name="TUser">Type of the user.</typeparam>
    /// <typeparam name="TKey"> Type of the key.</typeparam>
    /// <seealso cref="IUserTokenProvider{TUser,TKey}"/>
    public class TotpSecurityStampBasedTokenProvider<TUser, TKey>
        : IUserTokenProvider<TUser, TKey>
        where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>This token provider does not notify the user by default.</summary>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task.</returns>
        public virtual Task NotifyAsync(string token, IUserManager<TUser, TKey> manager, TUser user)
        {
            return Task.FromResult(0);
        }

        /// <summary>Returns true if the provider can generate tokens for the user, by default this is equal to
        /// manager.SupportsUserSecurityStamp.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual Task<bool> IsValidProviderForUserAsync(IUserManager<TUser, TKey> manager, TUser user)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return Task.FromResult(manager.SupportsUserSecurityStamp);
        }

        /// <summary>Generate a token for the user using their security stamp.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>The token.</returns>
        public virtual async Task<string> GenerateAsync(
            string purpose,
            IUserManager<TUser, TKey> manager,
            TUser user)
        {
            return Rfc6238AuthenticationService.GenerateCode(
                    await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture(),
                    await GetUserModifierAsync(purpose, manager, user).WithCurrentCulture())
                .ToString("D6", CultureInfo.InvariantCulture);
        }

        /// <summary>Validate the token for the user.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="token">  The token.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>A Task{bool}.</returns>
        public virtual async Task<bool> ValidateAsync(
            string purpose,
            string? token,
            IUserManager<TUser, TKey> manager,
            TUser user)
        {
            if (!int.TryParse(token, out var code))
            {
                return false;
            }
            var securityToken = await manager.CreateSecurityTokenAsync(user.Id).WithCurrentCulture();
            var modifier = await GetUserModifierAsync(purpose, manager, user).WithCurrentCulture();
            return securityToken != null
                && Rfc6238AuthenticationService.ValidateCode(securityToken, code, modifier);
        }

        /// <summary>Used for entropy in the token, uses the user.Id by default.</summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="manager">The manager.</param>
        /// <param name="user">   The user.</param>
        /// <returns>The user modifier.</returns>
        public virtual Task<string> GetUserModifierAsync(
            string purpose,
            IUserManager<TUser, TKey> manager,
            TUser user)
        {
            return Task.FromResult("Totp:" + purpose + ":" + user.Id);
        }
    }
}
