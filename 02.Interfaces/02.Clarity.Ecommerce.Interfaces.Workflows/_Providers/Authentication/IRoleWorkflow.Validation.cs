// <copyright file="IRoleWorkflow.Validation.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRoleWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Threading.Tasks;
    using Ecommerce.DataModel;

    /// <summary>Interface for authentication workflow.</summary>
    public partial interface IAuthenticationWorkflow
    {
        /// <summary>Gets roles for user.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An array of i role for user model.</returns>
        Task<IRoleForUserModel[]> GetRolesForUserAsync(int userID, string? contextProfileName);

        /// <summary>Gets roles for Account.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An array of IRoleForAccountModel.</returns>
        Task<IRoleForAccountModel[]> GetRolesForAccountAsync(int accountID, string? contextProfileName);
    }
}
