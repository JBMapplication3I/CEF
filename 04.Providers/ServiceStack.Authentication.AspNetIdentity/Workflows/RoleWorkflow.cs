// <copyright file="RoleWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the role workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;

    /// <summary>An authentication workflow.</summary>
    public partial class AuthenticationWorkflow
    {
        /// <inheritdoc/>
        public async Task<Dictionary<string, int>> GetRolesAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            return await roleManager.Roles.ToDictionaryAsync(x => x.Name, x => x.Id).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IRoleUserModel>> CreateRoleAsync(
            string name,
            List<string> permissions,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            var newRole = new UserRole(name);
            var toAdd = new List<int>();
            foreach (var p in permissions)
            {
                toAdd.Add(
                    await context.Permissions
                        .Where(y => y.Name == p)
                        .Select(y => y.Id)
                        .SingleAsync()
                        .ConfigureAwait(false));
            }
            foreach (var add in toAdd)
            {
                newRole.Permissions.Add(new() { PermissionId = add });
            }
            var result = await roleManager.CreateAsync(newRole).ConfigureAwait(false);
            return result.Succeeded
                ? (await GetAsync(name, contextProfileName).ConfigureAwait(false)).WrapInPassingCEFAR()!
                : CEFAR.FailingCEFAR<IRoleUserModel>(result.Errors?.ToArray()); // TODO: Log the error
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<IRoleUserModel>> RenameRoleAsync(
            string oldName,
            string newName,
            List<string> permissions,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            var existing = roleManager.Roles
                .Single(x => x.Name == oldName); // Will throw if not found
            existing.Name = newName;
            var toAdd = permissions
                .ConvertAll(x => new RolePermission
                {
                    PermissionId = context.Permissions.Single(y => y.Name == x).Id,
                });
            existing.Permissions.Clear();
            foreach (var add in toAdd)
            {
                existing.Permissions.Add(add);
            }
            var result = roleManager.Update(existing);
            return result.Succeeded
                ? (await GetAsync(newName, contextProfileName).ConfigureAwait(false)).WrapInPassingCEFAR()!
                : CEFAR.FailingCEFAR<IRoleUserModel>(result.Errors?.ToArray()); // TODO: Log the error
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> DeleteRoleAsync(string name, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            var existing = await roleManager.Roles.SingleAsync(x => x.Name == name || x.Id.ToString() == name).ConfigureAwait(false); // Will throw if not found
            for (var i = 0; i < context.RoleUsers.Count(x => x.RoleId == existing.Id); i++)
            {
                context.RoleUsers.Remove(context.RoleUsers.First(x => x.RoleId == existing.Id));
            }
            var result = roleManager.Delete(existing);
            // TODO: Log the error when false
            return result.Succeeded.BoolToCEFAR(result.Errors?.ToArray());
        }

        /// <inheritdoc/>
        public async Task<List<IPermissionModel>> GetPermissionsAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Permissions.Select(x => new { x.Id, x.Name }).ToListAsync().ConfigureAwait(false))
                .ConvertAll<IPermissionModel>(x => new PermissionModel { Id = x.Id, Name = x.Name });
        }

        /// <inheritdoc/>
        public override async Task<IRoleUserModel?> GetAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            return Get(await roleManager.Roles.FirstAsync(x => x.Id == id).ConfigureAwait(false));
        }

        /// <inheritdoc/>
        public override async Task<IRoleUserModel?> GetAsync(string name, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            using var roleManager = GetRoleManager(context, contextProfileName);
            return Get(await roleManager.Roles.FirstAsync(x => x.Name == name).ConfigureAwait(false));
        }

        /// <inheritdoc/>
        protected override Task RunDefaultAssociateWorkflowsAsync(
            IRoleUser entity,
            IRoleUserModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        protected override Task RunDefaultAssociateWorkflowsAsync(
            IRoleUser entity,
            IRoleUserModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        protected override Task RunDefaultRelateWorkflowsAsync(
            IRoleUser entity,
            IRoleUserModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc/>
        protected override Task RunDefaultRelateWorkflowsAsync(
            IRoleUser entity,
            IRoleUserModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            throw new InvalidOperationException();
        }
    }
}
