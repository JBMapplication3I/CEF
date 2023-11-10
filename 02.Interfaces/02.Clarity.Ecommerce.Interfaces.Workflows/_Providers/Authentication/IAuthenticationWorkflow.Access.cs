// <copyright file="IAuthenticationWorkflow.Access.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuthenticationWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.Models;

    /// <summary>Interface for authentication workflow.</summary>
    public partial interface IAuthenticationWorkflow
    {
        /// <summary>Locks the user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> LockUserAsync(int userID, string? contextProfileName);

        /// <summary>Unlocks the user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UnlockUserAsync(int userID, string? contextProfileName);

        /// <summary>Approve user account.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ApproveUserAccountAsync(int userID, string? contextProfileName);

        /// <summary>Un-approve user account.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> UnApproveUserAccountAsync(int userID, string? contextProfileName);

        /// <summary>Executes the require password reset on next login for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> DoRequirePasswordResetOnNextLoginForUserAsync(int userID, string? contextProfileName);

        /// <summary>Don't require password reset on next login for user account.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> DontRequirePasswordResetOnNextLoginForUserAccountAsync(int userID, string? contextProfileName);
    }
}
