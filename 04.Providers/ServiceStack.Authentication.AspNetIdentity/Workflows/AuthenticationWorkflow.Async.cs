// <copyright file="AuthenticationWorkflow.Async.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Utilities;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> ValidateUserNameIsGoodAsync(string userName, string? contextProfileName)
        {
            if (await Workflows.Users.CheckExistsAsync(userName, contextProfileName).ConfigureAwait(false) != null)
            {
                return CEFAR.FailingCEFAR("This username is already taken");
            }
            var censored = Censor.CensorText(userName.ToLower(), contextProfileName);
            return (!censored.Contains("*"))
                .BoolToCEFAR("This username contains filtered language");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ValidateEmailIsUniqueAsync(string email, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (!await context.Users.AnyAsync(x => x.Email == email).ConfigureAwait(false))
                .BoolToCEFAR("This email is already taken");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ValidatePasswordAsync(
            string userName,
            string password,
            string? contextProfileName)
        {
            var userID = await Workflows.Users.CheckExistsAsync(userName, contextProfileName).ConfigureAwait(false);
            Contract.RequiresValidID(userID, "Cannot validate a password for a user that does not exist");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            if (await userManager.CheckPasswordAsync(
                    await userManager.Users.SingleAsync(x => x.ID == userID!.Value).ConfigureAwait(false),
                    password)
                .ConfigureAwait(false))
            {
                return CEFAR.PassingCEFAR();
            }
            await Logger.LogErrorAsync("Authentication", "Invalid Password", contextProfileName).ConfigureAwait(false);
            return CEFAR.FailingCEFAR("Invalid Password");
        }

        /// <inheritdoc/>
        public Task<CEFActionResponse> ValidatePasswordIsGoodAsync(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return Task.FromResult(CEFAR.FailingCEFAR("Empty or Whitespace Password"));
            }
            if (RequiredLength > 0 && password.Length < RequiredLength)
            {
                return Task.FromResult(CEFAR.FailingCEFAR($"Passwords must be at least {RequiredLength} characters"));
            }
            if (RequireDigit && !RequireDigitRegex.IsMatch(password))
            {
                return Task.FromResult(CEFAR.FailingCEFAR("Passwords must contain at least one number."));
            }
            if (RequireLowercase && !RequireLowercaseRegex.IsMatch(password))
            {
                return Task.FromResult(CEFAR.FailingCEFAR("Passwords must contain at least one lower case letter."));
            }
            if (RequireUppercase && !RequireUppercaseRegex.IsMatch(password))
            {
                return Task.FromResult(CEFAR.FailingCEFAR("Passwords must contain at least one upper case letter."));
            }
            if (RequireNonLetterOrDigit && !RequireNonLetterOrDigitRegex.IsMatch(password))
            {
                return Task.FromResult(CEFAR.FailingCEFAR("Passwords must contain at least one symbol."));
            }
            return Task.FromResult(CEFAR.PassingCEFAR());
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ValidateCustomKeyIsUniqueAsync(
            string customKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (!await context.Users.AnyAsync(x => x.CustomKey == customKey).ConfigureAwait(false))
            {
                return CEFAR.PassingCEFAR();
            }
            await Logger.LogErrorAsync("Authentication", "Invalid CustomKey", contextProfileName).ConfigureAwait(false);
            return CEFAR.FailingCEFAR("An entry with that key already exists.");
        }

        /// <inheritdoc/>
        public async Task<string> GenerateEmailVerificationTokenAsync(
            int userID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(userID).ConfigureAwait(false);
            var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(token));
            return encoded;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CheckForcePasswordResetAsync(
            string userName,
            string password,
            string? contextProfileName)
        {
            if (!Contract.CheckValidKey(userName) || !Contract.CheckValidKey(password))
            {
                return CEFAR.FailingCEFAR();
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var result = await ValidatePasswordAsync(userName, password, contextProfileName).ConfigureAwait(false);
            if (!result.ActionSucceeded)
            {
                return result;
            }
            return (await context.Users.Where(x => x.UserName == userName)
                    .Select(x => x.RequirePasswordChangeOnNextLogin)
                    .SingleAsync()
                .ConfigureAwait(false))
                .BoolToCEFAR();
        }

        /// <summary>Gets user manager.</summary>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The user manager.</returns>
        private static CEFUserManager GetUserManager(IDbContext context, string? contextProfileName)
        {
#if NET5_0_OR_GREATER
            return new(RegistryLoaderWrapper.GetInstance<ICEFUserStore>(
                context,
                contextProfileName));
#else
            return new(RegistryLoaderWrapper.GetInstance<ICEFUserStore>(
                new Dictionary<string, object>() { ["context"] = context },
                contextProfileName));
#endif
        }

        /// <summary>Gets role manager.</summary>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The role manager.</returns>
        private static CEFRoleManager GetRoleManager(IDbContext context, string? contextProfileName)
        {
#if NET5_0_OR_GREATER
            return new(RegistryLoaderWrapper.GetInstance<ICEFRoleStore>(
                context,
                contextProfileName));
#else
            return new(RegistryLoaderWrapper.GetInstance<ICEFRoleStore>(
                new Dictionary<string, object>() { ["context"] = context },
                contextProfileName));
#endif
        }

        /// <summary>Gets a role user model using the given role.</summary>
        /// <param name="role">The role to get.</param>
        /// <returns>An IRoleUserModel.</returns>
        private static IRoleUserModel Get(IUserRole role)
        {
            return new RoleUserModel
            {
                ID = role.Id,
                CustomKey = role.Name,
                Active = true,
                Permissions = role.Permissions
                    .Select(x => new PermissionModel { Id = x.Permission!.Id, Name = x.Permission.Name })
                    .ToList(),
            };
        }
    }
}
