// <copyright file="RoleService.Assignments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the authentication service class</summary>
// ReSharper disable AsyncApostle.AsyncMethodNamingHighlighting
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using DataModel;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/User/AssignRole", "PATCH",
            Summary = "Assigns a role to the specified user")]
    public class AssignRoleToUser : RoleForUserModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/User/UpdateRole", "PATCH",
            Summary = "Updates the information about the role assignment to a specific user")]
    public class UpdateRoleForUser : RoleForUserModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInBrandAdmin, UsedInFranchiseAdmin, UsedInStoreAdmin, UsedInAdmin,
        Authenticate,
        Route("/Authentication/User/RemoveRole", "PATCH",
            Summary = "Removes a role from a specified user")]
    public class RemoveRoleFromUser : RoleForUserModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Account/AssignRole", "PATCH",
            Summary = "Assigns a role to a specified account (all users in the account will inherit the role).")]
    public class AssignRoleToAccount : RoleForAccountModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Account/UpdateRole", "PATCH",
            Summary = "Updates the information about a specific role assignment to an account.")]
    public class UpdateRoleForAccount : RoleForAccountModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Authentication/Account/RemoveRole", "PATCH",
            Summary = "Removes a role assigned to a specified account.")]
    public class RemoveRoleFromAccount : RoleForAccountModel, IReturn<CEFActionResponse>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Patch(AssignRoleToUser request)
        {
            return await Workflows.Authentication.AssignRoleToUserAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(UpdateRoleForUser request)
        {
            return await Workflows.Authentication.UpdateRoleForUserAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(RemoveRoleFromUser request)
        {
            return await Workflows.Authentication.RemoveRoleFromUserAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(AssignRoleToAccount request)
        {
            return await Workflows.Authentication.AssignRoleToAccountAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(UpdateRoleForAccount request)
        {
            return await Workflows.Authentication.UpdateRoleForAccountAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(RemoveRoleFromAccount request)
        {
            return await Workflows.Authentication.RemoveRoleFromAccountAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
