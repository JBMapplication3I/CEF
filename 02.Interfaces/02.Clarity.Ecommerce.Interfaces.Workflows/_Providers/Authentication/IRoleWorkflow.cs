// <copyright file="IRoleWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRoleWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for authentication workflow.</summary>
    public partial interface IAuthenticationWorkflow
    {
        /// <summary>Gets the roles.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The roles.</returns>
        Task<Dictionary<string, int>> GetRolesAsync(string? contextProfileName);

        /// <summary>Creates a role.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="permissions">       The permissions.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new role.</returns>
        Task<CEFActionResponse<IRoleUserModel>> CreateRoleAsync(
            string name,
            List<string> permissions,
            string? contextProfileName);

        /// <summary>Rename role.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="newName">           Name of the new.</param>
        /// <param name="permissions">       The permissions.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{IRoleUserModel}).</returns>
        Task<CEFActionResponse<IRoleUserModel>> RenameRoleAsync(
            string name,
            string newName,
            List<string> permissions,
            string? contextProfileName);

        /// <summary>Deletes the role.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> DeleteRoleAsync(
            string name,
            string? contextProfileName);

        /// <summary>Gets the permissions.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The permissions.</returns>
        Task<List<IPermissionModel>> GetPermissionsAsync(string? contextProfileName);
    }
}
