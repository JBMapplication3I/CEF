// <copyright file="IVendorWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IVendorWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for vendor workflow.</summary>
    public partial interface IVendorWorkflow
    {
        /// <summary>Gets products by vendor.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="asListing">         True to as listing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The products by vendor.</returns>
        Task<(IEnumerable<IVendorProductModel> results, int totalPages, int totalCount)> GetProductsByVendorAsync(
            IVendorProductSearchModel search,
            bool asListing,
            string? contextProfileName);

        /// <summary>Gets vendors by product.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The vendors by product.</returns>
        Task<List<IVendorProductModel>> GetVendorsByProductAsync(int productID, string? contextProfileName);

        /// <summary>Gets by user name.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by user name.</returns>
        Task<IVendorModel?> GetByUserNameAsync(string userName, string? contextProfileName);

        /// <summary>Assign account to user name.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="accountToken">      The account token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> AssignAccountToUserNameAsync(string userName, string accountToken, string? contextProfileName);

        /// <summary>Resets the token.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> ResetTokenAsync(string userName, string? contextProfileName);

        /// <summary>Updates the password.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="password">          The password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A string.</returns>
        Task<string> UpdatePasswordAsync(string userName, string password, string? contextProfileName);

        /// <summary>User must reset password.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> UserMustResetPasswordAsync(string userName, string? contextProfileName);

        /// <summary>Login action.</summary>
        /// <param name="userName">          Name of the user.</param>
        /// <param name="password">          The password.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A string.</returns>
        Task<string> LoginAsync(string userName, string password, string? contextProfileName);

        /// <summary>Gets identifier by assigned user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The identifier by assigned user identifier.</returns>
        Task<CEFActionResponse<int?>> GetIDByAssignedUserIDAsync(int userID, string? contextProfileName);
    }
}
