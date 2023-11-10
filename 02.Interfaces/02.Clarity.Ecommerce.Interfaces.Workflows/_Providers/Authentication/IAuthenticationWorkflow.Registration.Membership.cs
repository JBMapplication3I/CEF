// <copyright file="IAuthenticationWorkflow.Registration.Membership.cs" company="clarity-ventures.com">
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
        /// <summary>Validates the invitation.</summary>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ValidateInvitationAsync(string email, string token);

        /// <summary>Validates the email.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="token">             The token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ValidateEmailAsync(string email, string token, string? contextProfileName);

        /// <summary>Creates lite account and send validation email.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> CreateLiteAccountAndSendValidationEmailAsync(
            ICreateLiteAccountAndSendValidationEmail request,
            string? contextProfileName);

        /// <summary>Complete registration.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> CompleteRegistrationAsync(ICompleteRegistration request, string? contextProfileName);

        /// <summary>Approve user registration.</summary>
        /// <param name="token">             The token.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ApproveUserRegistrationAsync(string token, int userID, string? contextProfileName);
    }
}
