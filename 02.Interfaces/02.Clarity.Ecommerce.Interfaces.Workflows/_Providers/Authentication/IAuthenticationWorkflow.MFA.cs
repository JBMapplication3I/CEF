// <copyright file="IAuthenticationWorkflow.MFA.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAuthenticationWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for authentication workflow.</summary>
    public partial interface IAuthenticationWorkflow
    {
        /// <summary>Check for mfa for username.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{MFARequirementsModel}}.</returns>
        Task<CEFActionResponse<MFARequirementsModel>> CheckForMFAForUsernameAsync(
            string username,
            string? contextProfileName);

        /// <summary>Request mfa for username.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="usePhone">          True to use phone.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> RequestMFAForUsernameAsync(
            string username,
            bool usePhone,
            string? contextProfileName);
    }
}
