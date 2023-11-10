// <copyright file="IAuthenticationWorkflow.Registration.Normal.cs" company="clarity-ventures.com">
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
        /// <summary>Validates the user name is good.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ValidateUserNameIsGoodAsync(string userName, string? contextProfileName);

        /// <summary>Validates the email is unique.</summary>
        /// <param name="email">             The email.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ValidateEmailIsUniqueAsync(string email, string? contextProfileName);

        /// <summary>Validates the password.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="password">          The password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ValidatePasswordAsync(string userName, string password, string? contextProfileName);

        /// <summary>Validates the password is good.</summary>
        /// <param name="password">The password.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ValidatePasswordIsGoodAsync(string password);

        /// <summary>Validates the custom key isn't already used.</summary>
        /// <param name="customKey">            The custom key.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ValidateCustomKeyIsUniqueAsync(string customKey, string? contextProfileName);

        /// <summary>Creates a verification token and assigns it to the given user.</summary>
        /// <param name="userID">The ID of the user to generate the token for.</param>
        /// <param name="contextProfileName">The context profile name.</param>
        /// <returns>A verification token, assigned to the current user.</returns>
        Task<string> GenerateEmailVerificationTokenAsync(int userID, string? contextProfileName);
    }
}
