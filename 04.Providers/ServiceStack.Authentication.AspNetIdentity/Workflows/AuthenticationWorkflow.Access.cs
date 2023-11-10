// <copyright file="AuthenticationWorkflow.Access.cs" company="clarity-ventures.com">
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
    using Models;
    using Providers.Emails;
    using Utilities;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> LockUserAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            if (await userManager.IsLockedOutAsync(id).ConfigureAwait(false))
            {
                // Just say ok
                return CEFAR.PassingCEFAR();
            }
            var result = await userManager.SetLockoutEnabledAsync(id, true).ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors?.ToArray());
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UnlockUserAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            if (!await context.Users.FilterByID(id).Select(x => x.LockoutEnabled).SingleAsync().ConfigureAwait(false))
            {
                // Just say ok
                return CEFAR.PassingCEFAR();
            }
            using var userManager = GetUserManager(context, contextProfileName);
            var result = await userManager.SetLockoutEnabledAsync(id, false).ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors?.ToArray());
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ApproveUserAccountAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = await context.Users.SingleAsync(x => x.ID == userID).ConfigureAwait(false);
            if (user.IsApproved)
            {
                return CEFAR.PassingCEFAR("User already approved.");
            }
            user.UpdatedDate = DateExtensions.GenDateTime;
            user.IsApproved = true;
            var success = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (success)
            {
                try
                {
                    await new AuthenticationUserAccountApprovedNotificationToCustomerEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: user.Email,
                            parameters: new() { ["firstName"] = user.Contact?.FirstName ?? "Customer" })
                        .ConfigureAwait(false);
                }
                catch (Exception ex1)
                {
                    return success.BoolToCEFAR("There was an error sending the customer approval email: " + ex1);
                }
            }
            return success.BoolToCEFAR("Unable to save changes");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UnApproveUserAccountAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = await context.Users.SingleAsync(x => x.ID == userID).ConfigureAwait(false);
            if (!user.IsApproved)
            {
                return CEFAR.PassingCEFAR("User already un-approved.");
            }
            user.UpdatedDate = DateExtensions.GenDateTime;
            user.IsApproved = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("Unable to save changes.");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DoRequirePasswordResetOnNextLoginForUserAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = await context.Users.SingleAsync(x => x.ID == userID).ConfigureAwait(false);
            if (user.RequirePasswordChangeOnNextLogin)
            {
                return CEFAR.PassingCEFAR("User already set.");
            }
            user.UpdatedDate = DateExtensions.GenDateTime;
            user.RequirePasswordChangeOnNextLogin = true;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)).BoolToCEFAR("Unable to save changes");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DontRequirePasswordResetOnNextLoginForUserAccountAsync(int userID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var user = await context.Users.SingleAsync(x => x.ID == userID).ConfigureAwait(false);
            if (!user.RequirePasswordChangeOnNextLogin)
            {
                return CEFAR.PassingCEFAR("User already set.");
            }
            user.UpdatedDate = DateExtensions.GenDateTime;
            user.RequirePasswordChangeOnNextLogin = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false)).BoolToCEFAR("Unable to save changes.");
        }
    }
}
