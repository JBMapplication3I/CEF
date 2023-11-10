// <copyright file="IRoleWorkflow.Assignments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRoleWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.DataModel;
    using Ecommerce.Models;

    /// <summary>Interface for authentication workflow.</summary>
    public partial interface IAuthenticationWorkflow
    {
        /// <summary>Assign role to user.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> AssignRoleToUserAsync(IRoleForUserModel model, string? contextProfileName);

        /// <summary>Updates the role for user.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UpdateRoleForUserAsync(IRoleForUserModel model, string? contextProfileName);

        /// <summary>Removes the role from user.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> RemoveRoleFromUserAsync(IRoleForUserModel model, string? contextProfileName);

        /// <summary>Assign role to Account.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> AssignRoleToAccountAsync(IRoleForAccountModel model, string? contextProfileName);

        /// <summary>Updates the role for Account.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UpdateRoleForAccountAsync(IRoleForAccountModel model, string? contextProfileName);

        /// <summary>Removes the role from Account.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> RemoveRoleFromAccountAsync(IRoleForAccountModel model, string? contextProfileName);
    }
}
