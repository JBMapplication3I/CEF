// <copyright file="IAuthenticationWorkflow.Forgot.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuthenticationWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for authentication workflow.</summary>
    public partial interface IAuthenticationWorkflow
        : IWorkflowBaseHasAll<IRoleUserModel, IRoleUserSearchModel, IRoleUser, RoleUser>
    {
        /// <summary>Forgot username.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ForgotUsernameAsync(string email, string? contextProfileName);

        /// <summary>Forgot password.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ForgotPasswordAsync(string email, string? contextProfileName);

        /// <summary>Forgot password return.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="password">          The password.</param>
        /// <param name="token">             The token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{string}}.</returns>
        Task<CEFActionResponse<string>> ForgotPasswordReturnAsync(
            string email,
            string password,
            string token,
            string? contextProfileName);

        /// <summary>Force password reset.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="currentPassword">   The current password.</param>
        /// <param name="newPassword">       The new password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ForcePasswordResetAsync(
            string email,
            string currentPassword,
            string newPassword,
            string? contextProfileName);

        /// <summary>Check for force password reset.</summary>
        /// <param name="userName">          The UserName.</param>
        /// <param name="password">          The password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> CheckForcePasswordResetAsync(
            string userName,
            string password,
            string? contextProfileName);

        /// <summary>Change password.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="currentPassword">   The current password.</param>
        /// <param name="newPassword">       The new password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ChangePasswordAsync(
            string userName,
            string currentPassword,
            string newPassword,
            string? contextProfileName);

        /// <summary>Creates password hash.</summary>
        /// <param name="password">          The password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{string}.</returns>
        CEFActionResponse<string> CreatePasswordHash(string password, string? contextProfileName);
    }
}
