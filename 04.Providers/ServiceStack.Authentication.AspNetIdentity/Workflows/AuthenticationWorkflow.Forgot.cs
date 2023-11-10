// <copyright file="AuthenticationWorkflow.Forgot.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JSConfigs;
    using Models;
    using Providers.Emails;
    using Utilities;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> ForgotUsernameAsync(string email, string? contextProfileName)
        {
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                using var userManager = GetUserManager(context, contextProfileName);
                var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false)
                    ?? await context.Users
                        .FilterByActive(true)
                        .FilterUsersByContactEmail(email)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false);
                if (user == null)
                {
                    return CEFAR.FailingCEFAR("This email is not tied to an existing User.");
                }
                try
                {
                    var subject = CEFConfigDictionary.EmailForgotUsernameSubject;
                    var template = CEFConfigDictionary.EmailForgotUsernameBodyTemplatePath;
                    // TODO: Read the url and load the template using a web client
                    template = template.Replace("{{Username}}", user.UserName);
                    await userManager.SendEmailAsync(user.ID, subject, template).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    await Logger.LogErrorAsync(
                            name: $"{nameof(AuthenticationWorkflow)}.{ex.GetType().Name}",
                            message: "Unable to send Forgot Username email",
                            ex: ex,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    return CEFAR.FailingCEFAR("Unable to send Forgot Username email", ex.Message);
                }
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ForgotPasswordAsync(string email, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false)
                ?? await context.Users
                    .FilterByActive(true)
                    .FilterUsersByContactEmail(email)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR("This email is not tied to an existing User.");
            }
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user.ID).ConfigureAwait(false);
            var result = await new AuthenticationForgotPasswordToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: email,
                    parameters: new()
                    {
                        ["username"] = user.UserName,
                        ["firstName"] = user.Contact?.FirstName,
                        ["lastName"] = user.Contact?.LastName,
                        ["resetToken"] = resetToken,
                    })
                .ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<string>> ForgotPasswordReturnAsync(
            string email,
            string password,
            string token,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            var user = await userManager.FindByEmailAsync(email).ConfigureAwait(false)
                ?? await context.Users
                    .FilterByActive(true)
                    .FilterUsersByContactEmail(email)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR<string>("This email is not tied to an existing User.");
            }
            var result = await userManager.ResetPasswordAsync(user.Id, token, password).ConfigureAwait(false);
            return !result.Succeeded
                // TODO: Log Error
                ? CEFAR.FailingCEFAR<string>(
                    "Unable to Reset Password: " + result.Errors.Aggregate((c, n) => c + " " + n))
                : user.UserName.WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ForcePasswordResetAsync(
            string email,
            string currentPassword,
            string newPassword,
            string? contextProfileName)
        {
            var userID = await Workflows.Users.CheckExistsAsync(email, contextProfileName).ConfigureAwait(false);
            if (!Contract.CheckValidID(userID))
            {
                return CEFAR.FailingCEFAR("Email or password do not match our records.");
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = await context.Users.FilterByID(userID).SingleAsync().ConfigureAwait(false);
            var result = await ChangePasswordInnerAsync(
                    user.ID,
                    currentPassword,
                    newPassword,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!result.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR("Email or password do not match our records.");
            }
            user.RequirePasswordChangeOnNextLogin = false;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR(
                    $"ERROR! Saving RequirePasswordChangeOnNextLogin as false for user ID '{userID}' failed.");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ChangePasswordAsync(
            string userName,
            string currentPassword,
            string newPassword,
            string? contextProfileName)
        {
            var userID = await Workflows.Users.CheckExistsAsync(userName, contextProfileName).ConfigureAwait(false);
            return await ChangePasswordInnerAsync(userID, currentPassword, newPassword, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public CEFActionResponse<string> CreatePasswordHash(string password, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            return userManager.PasswordHasher.HashPassword(Contract.RequiresValidKey(password))
                .WrapInPassingCEFAR()!;
        }

        /// <summary>Change password inner.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="currentPassword">   The current password.</param>
        /// <param name="newPassword">       The new password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        private async Task<CEFActionResponse> ChangePasswordInnerAsync(
            int? userID,
            string currentPassword,
            string newPassword,
            string? contextProfileName)
        {
            Contract.RequiresValidID(userID, "Cannot change a password for a user that does not exist");
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            var result1 = await userManager.ChangePasswordAsync(
                    userID!.Value,
                    currentPassword,
                    newPassword)
                .ConfigureAwait(false);
            if (result1.Succeeded)
            {
                return CEFAR.PassingCEFAR();
            }
            var user = await userManager.FindByIdAsync(userID.Value).ConfigureAwait(false);
            if (user is null)
            {
                await Logger.LogErrorAsync("Authentication", "User not found", contextProfileName).ConfigureAwait(false);
                return CEFAR.FailingCEFAR("User not found");
            }
            if (user.RequirePasswordChangeOnNextLogin)
            {
                user.RequirePasswordChangeOnNextLogin = false;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            var message = result1.Errors?.Aggregate((c, n) => c + "\r\n" + n);
            await Logger.LogErrorAsync("Authentication", message, contextProfileName).ConfigureAwait(false);
            return CEFAR.FailingCEFAR(message);
        }
    }
}
