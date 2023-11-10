// Decompiled with JetBrains decompiler
// Type: Microsoft.AspNet.Identity.UserManagerExtensions
// Assembly: Microsoft.AspNet.Identity.Core, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 9E2B2C84-E0DA-4554-B416-8EA71B0D085D
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.aspnet.identity.core\2.2.3\lib\net45\Microsoft.AspNet.Identity.Core.dll

namespace Microsoft.AspNet.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>Extension methods for UserManager.</summary>
    public static class UserManagerExtensions
    {
        /// <summary>Increments the access failed count for the user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult AccessFailed<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.AccessFailedAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Add a user claim.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="claim">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult AddClaim<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            Claim claim)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.AddClaimAsync(userId, claim));
        }

        /// <summary>Sync extension.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="login">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult AddLogin<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            UserLoginInfo login)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.AddLoginAsync(userId, login));
        }

        /// <summary>Add a user password only if one does not already exist.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="userId">  .</param>
        /// <param name="password">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult AddPassword<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string password)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.AddPasswordAsync(userId, password));
        }

        /// <summary>Add a user to a role.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="role">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult AddToRole<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string role)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.AddToRoleAsync(userId, role));
        }

        /// <summary>Add a user to several roles.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="roles">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult AddToRoles<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            params string[] roles)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.AddToRolesAsync(userId, roles));
        }

        /// <summary>Change a user password.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">        .</param>
        /// <param name="userId">         .</param>
        /// <param name="currentPassword">.</param>
        /// <param name="newPassword">    .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult ChangePassword<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string currentPassword,
            string newPassword)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.ChangePasswordAsync(userId, currentPassword, newPassword));
        }

        /// <summary>Change a phone number using the verification token.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">    .</param>
        /// <param name="userId">     .</param>
        /// <param name="phoneNumber">.</param>
        /// <param name="token">      .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult ChangePhoneNumber<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string phoneNumber,
            string token)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.ChangePhoneNumberAsync(userId, phoneNumber, token));
        }

        /// <summary>Returns true if the password combination is valid for the user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="user">    .</param>
        /// <param name="password">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool CheckPassword<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TUser user,
            string password)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.CheckPasswordAsync(user, password));
        }

        /// <summary>Confirm the user with confirmation token.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="token">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult ConfirmEmail<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string token)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.ConfirmEmailAsync(userId, token));
        }

        /// <summary>Create a user with no password.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Create<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.CreateAsync(user))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Create a user and associates it with the given password (if one is provided)</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="user">    .</param>
        /// <param name="password">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Create<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TUser user,
            string password)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.CreateAsync(user, password));
        }

        /// <summary>Creates a ClaimsIdentity representing the user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">           .</param>
        /// <param name="user">              .</param>
        /// <param name="authenticationType">.</param>
        /// <returns>The new identity.</returns>
        public static ClaimsIdentity CreateIdentity<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TUser user,
            string authenticationType)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.CreateIdentityAsync(user, authenticationType));
        }

        /// <summary>Delete an user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Delete<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.DeleteAsync(user))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Return a user with the specified username and password or null if there is no match.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="userName">.</param>
        /// <param name="password">.</param>
        /// <returns>A TUser.</returns>
        public static TUser Find<TUser, TKey>(this UserManager<TUser, TKey> manager, string userName, string password)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.FindAsync(userName, password));
        }

        /// <summary>Sync extension.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="login">  .</param>
        /// <returns>A TUser.</returns>
        public static TUser Find<TUser, TKey>(this UserManager<TUser, TKey> manager, UserLoginInfo login)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.FindAsync(login))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Find a user by email.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="email">  .</param>
        /// <returns>The found email.</returns>
        public static TUser FindByEmail<TUser, TKey>(this UserManager<TUser, TKey> manager, string email)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.FindByEmailAsync(email))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Find a user by id.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The found identifier.</returns>
        public static TUser FindById<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.FindByIdAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Find a user by name.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager"> .</param>
        /// <param name="userName">.</param>
        /// <returns>The found name.</returns>
        public static TUser FindByName<TUser, TKey>(this UserManager<TUser, TKey> manager, string userName)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.FindByNameAsync(userName))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Generate a token for using to change to a specific phone number for the user.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">    .</param>
        /// <param name="userId">     .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>The change phone number token.</returns>
        public static string GenerateChangePhoneNumberToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string phoneNumber)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber));
        }

        /// <summary>Get the confirmation token for the user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The email confirmation token.</returns>
        public static string GenerateEmailConfirmationToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GenerateEmailConfirmationTokenAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Get the password reset token for the user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The password reset token.</returns>
        public static string GeneratePasswordResetToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GeneratePasswordResetTokenAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Get a user token for a factor provider.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">   .</param>
        /// <param name="userId">    .</param>
        /// <param name="providerId">.</param>
        /// <returns>The two factor token.</returns>
        public static string GenerateTwoFactorToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string providerId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.GenerateTwoFactorTokenAsync(userId, providerId));
        }

        /// <summary>Get a user token for a specific purpose.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="purpose">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The user token.</returns>
        public static string GenerateUserToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            string purpose,
            TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.GenerateUserTokenAsync(purpose, userId));
        }

        /// <summary>Returns the number of failed access attempts for the user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The access failed count.</returns>
        public static int GetAccessFailedCount<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetAccessFailedCountAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Get a users's claims.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The claims.</returns>
        public static IList<Claim> GetClaims<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetClaimsAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Get an user's email.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The email.</returns>
        public static string GetEmail<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetEmailAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns whether the user allows lockout.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool GetLockoutEnabled<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetLockoutEnabledAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns the user lockout end date.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The lockout end date.</returns>
        public static DateTimeOffset GetLockoutEndDate<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetLockoutEndDateAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Gets the logins for a user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The logins.</returns>
        public static IList<UserLoginInfo> GetLogins<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetLoginsAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Get an user's phoneNumber.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The phone number.</returns>
        public static string GetPhoneNumber<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetPhoneNumberAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Get a users's roles.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The roles.</returns>
        public static IList<string> GetRoles<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetRolesAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Get the current security stamp for a user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The security stamp.</returns>
        public static string GetSecurityStamp<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetSecurityStampAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns true if two factor is enabled for the user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool GetTwoFactorEnabled<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetTwoFactorEnabledAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns a list of valid two factor providers for a user.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>The valid two factor providers.</returns>
        public static IList<string> GetValidTwoFactorProviders<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.GetValidTwoFactorProvidersAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns true if a user has a password set.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>True if password, false if not.</returns>
        public static bool HasPassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.HasPasswordAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns true if the user's email has been confirmed.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>True if email confirmed, false if not.</returns>
        public static bool IsEmailConfirmed<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.IsEmailConfirmedAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns true if the user is in the specified role.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="role">   .</param>
        /// <returns>True if in role, false if not.</returns>
        public static bool IsInRole<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string role)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.IsInRoleAsync(userId, role));
        }

        /// <summary>Returns true if the user is locked out.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>True if locked out, false if not.</returns>
        public static bool IsLockedOut<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.IsLockedOutAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Returns true if the user's phone number has been confirmed.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>True if phone number confirmed, false if not.</returns>
        public static bool IsPhoneNumberConfirmed<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.IsPhoneNumberConfirmedAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Notify a user with a token from a specific user factor provider.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">          .</param>
        /// <param name="userId">           .</param>
        /// <param name="twoFactorProvider">.</param>
        /// <param name="token">            .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult NotifyTwoFactorToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string twoFactorProvider,
            string token)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.NotifyTwoFactorTokenAsync(userId, twoFactorProvider, token));
        }

        /// <summary>Remove a user claim.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="claim">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult RemoveClaim<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            Claim claim)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.RemoveClaimAsync(userId, claim));
        }

        /// <summary>Remove a user from a role.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="role">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult RemoveFromRole<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string role)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.RemoveFromRoleAsync(userId, role));
        }

        /// <summary>Remove a user from the specified roles.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="roles">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult RemoveFromRoles<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            params string[] roles)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.RemoveFromRolesAsync(userId, roles));
        }

        /// <summary>Remove a user login.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="login">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult RemoveLogin<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            UserLoginInfo login)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.RemoveLoginAsync(userId, login));
        }

        /// <summary>Associate a login with a user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult RemovePassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.RemovePasswordAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Resets the access failed count for the user to 0.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult ResetAccessFailedCount<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.ResetAccessFailedCountAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Reset a user's password using a reset password token.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">    .</param>
        /// <param name="userId">     .</param>
        /// <param name="token">      This should be the user's security stamp by default.</param>
        /// <param name="newPassword">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult ResetPassword<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string token,
            string newPassword)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.ResetPasswordAsync(userId, token, newPassword));
        }

        /// <summary>Send email with supplied subject and body.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="subject">.</param>
        /// <param name="body">   .</param>
        public static void SendEmail<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string subject,
            string body)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            AsyncHelper.RunSync(() => manager.SendEmailAsync(userId, subject, body));
        }

        /// <summary>Send text message using the given message.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="message">.</param>
        public static void SendSms<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string message)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            AsyncHelper.RunSync(() => manager.SendSmsAsync(userId, message));
        }

        /// <summary>Set an user's email.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="email">  .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult SetEmail<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string email)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.SetEmailAsync(userId, email));
        }

        /// <summary>Sets whether the user allows lockout.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="enabled">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult SetLockoutEnabled<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            bool enabled)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.SetLockoutEnabledAsync(userId, enabled));
        }

        /// <summary>Sets the user lockout end date.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">   .</param>
        /// <param name="userId">    .</param>
        /// <param name="lockoutEnd">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult SetLockoutEndDate<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            DateTimeOffset lockoutEnd)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.SetLockoutEndDateAsync(userId, lockoutEnd));
        }

        /// <summary>Set an user's phoneNumber.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">    .</param>
        /// <param name="userId">     .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult SetPhoneNumber<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string phoneNumber)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.SetPhoneNumberAsync(userId, phoneNumber));
        }

        /// <summary>Set whether a user's two factor is enabled.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="enabled">.</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult SetTwoFactorEnabled<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            bool enabled)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.SetTwoFactorEnabledAsync(userId, enabled));
        }

        /// <summary>Update an user.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="user">   .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult Update<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.UpdateAsync(user))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Generate a new security stamp for a user, used for SignOutEverywhere functionality.</summary>
        /// <typeparam name="TUser">Type of the user.</typeparam>
        /// <typeparam name="TKey"> Type of the key.</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <returns>An IdentityResult.</returns>
        public static IdentityResult UpdateSecurityStamp<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            return manager != null
                ? AsyncHelper.RunSync(() => manager.UpdateSecurityStampAsync(userId))
                : throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>Verify that a token is valid for changing the user's phone number.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">    .</param>
        /// <param name="userId">     .</param>
        /// <param name="token">      .</param>
        /// <param name="phoneNumber">.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool VerifyChangePhoneNumberToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string token,
            string phoneNumber)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber));
        }

        /// <summary>Verify a user factor token with the specified provider.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">   .</param>
        /// <param name="userId">    .</param>
        /// <param name="providerId">.</param>
        /// <param name="token">     .</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool VerifyTwoFactorToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string providerId,
            string token)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.VerifyTwoFactorTokenAsync(userId, providerId, token));
        }

        /// <summary>Validate a user token.</summary>
        /// <typeparam name="TUser">.</typeparam>
        /// <typeparam name="TKey"> .</typeparam>
        /// <param name="manager">.</param>
        /// <param name="userId"> .</param>
        /// <param name="purpose">.</param>
        /// <param name="token">  .</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        public static bool VerifyUserToken<TUser, TKey>(
            this UserManager<TUser, TKey> manager,
            TKey userId,
            string purpose,
            string token)
            where TUser : class, IUser<TKey>
            where TKey : IEquatable<TKey>
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            return AsyncHelper.RunSync(() => manager.VerifyUserTokenAsync(userId, purpose, token));
        }
    }
}
