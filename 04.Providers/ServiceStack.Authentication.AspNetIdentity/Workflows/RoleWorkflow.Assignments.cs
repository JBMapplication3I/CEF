// <copyright file="RoleWorkflow.Assignments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Models;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse> AssignRoleToUserAsync(IRoleForUserModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            var role = await roleManager.Roles
                .SingleAsync(x => x.Id == model.RoleId || x.Name == model.Name)
                .ConfigureAwait(false); // Will throw if not found
            using var userManager = GetUserManager(context, contextProfileName);
            var (success, message) = await userManager
                .AssignRoleIfNotAssignedAsync(model.UserId, role.Name, model.StartDate, model.EndDate)
                .ConfigureAwait(false);
            return success.BoolToCEFAR(message);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateRoleForUserAsync(IRoleForUserModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            var result = await userManager.UpdateRoleAsync(model).ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors?.ToArray());
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> RemoveRoleFromUserAsync(IRoleForUserModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            var role = await roleManager.Roles
                .SingleAsync(x => x.Id == model.RoleId || x.Name == model.Name)
                .ConfigureAwait(false); // Will throw if not found
            using var userManager = GetUserManager(context, contextProfileName);
            var result = await userManager.RemoveRoleFromUserAsync(model.UserId, role.Name).ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> AssignRoleToAccountAsync(IRoleForAccountModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            var role = await roleManager.Roles
                .SingleAsync(x => x.Id == model.RoleId || x.Name == model.Name)
                .ConfigureAwait(false); // Will throw if not found
            using var userManager = GetUserManager(context, contextProfileName);
            var result = await userManager
                .AddRoleToAccountAsync(model.AccountId, role.Name, model.StartDate, model.EndDate)
                .ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateRoleForAccountAsync(IRoleForAccountModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var userManager = GetUserManager(context, contextProfileName);
            var result = await userManager.UpdateRoleAsync(model).ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> RemoveRoleFromAccountAsync(IRoleForAccountModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            var role = roleManager.Roles
                .Single(x => x.Id == model.RoleId || x.Name == model.Name).Name; // Will throw if not found
            using var userManager = GetUserManager(context, contextProfileName);
            var result = await userManager.RemoveRoleFromAccountAsync(model.AccountId, role).ConfigureAwait(false);
            return result.Succeeded.BoolToCEFAR(result.Errors);
        }
    }
}
