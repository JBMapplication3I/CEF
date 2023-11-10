// <copyright file="AuthenticationService.Access.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Lock/User/{ID}", "PATCH",
            Summary = "Locks the user, preventing them from logging in")]
    public class LockUser : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Unlock/User/{ID}", "PATCH",
            Summary = "Unlocks a locked user, allowing them to log in")]
    public class UnlockUser : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Approve/User/{ID}", "PATCH",
            Summary = "Approves a user, allowing them to log in")]
    public class ApproveUser : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/UnApprove/User/{ID}", "PATCH",
            Summary = "Removes the approval for a user, preventing them from logging in.")]
    public class UnApproveUser : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/DoRequirePasswordResetOnNextLogin/User/{ID}", "PATCH",
            Summary = "Sets a user to require resetting their password on their next login attempt.")]
    public class DoRequirePasswordResetOnNextLoginForUser : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/DontRequirePasswordResetOnNextLogin/User/{ID}", "PATCH",
            Summary = "Un-sets the setting of a specific user to require resetting their password on next login attempt.")]
    public class DontRequirePasswordResetOnNextLoginForUser : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Patch(LockUser request)
        {
            return await Workflows.Authentication.LockUserAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(UnlockUser request)
        {
            return await Workflows.Authentication.UnlockUserAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(ApproveUser request)
        {
            return await Workflows.Authentication.ApproveUserAccountAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(UnApproveUser request)
        {
            return await Workflows.Authentication.UnApproveUserAccountAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(DoRequirePasswordResetOnNextLoginForUser request)
        {
            return await Workflows.Authentication.DoRequirePasswordResetOnNextLoginForUserAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(DontRequirePasswordResetOnNextLoginForUser request)
        {
            return await Workflows.Authentication.DontRequirePasswordResetOnNextLoginForUserAccountAsync(
                    request.ID,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
