// <copyright file="IAccountWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAccountWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for account workflow.</summary>
    public partial interface IAccountWorkflow
    {
        /// <summary>Check exists by CustomKey.</summary>
        /// <param name="customKey">         The custom key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The ID of the Account if the customKey is found.</returns>
        Task<int?> CheckExistsByKeyAsync(string customKey, string? contextProfileName);

        /// <summary>Gets identifier by user identifier.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The identifier by user identifier.</returns>
        Task<int?> GetIDByUserIDAsync(int id, string? contextProfileName);

        /// <summary>Gets identifier by user name.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The identifier by user name.</returns>
        Task<int?> GetIDByUserNameAsync(
            string username,
            string? contextProfileName);

        /// <summary>Gets by user identifier.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by user identifier.</returns>
        Task<IAccountModel?> GetByUserIDAsync(int id, string? contextProfileName);

        /// <summary>Gets by user name.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by user name.</returns>
        Task<IAccountModel?> GetByUserNameAsync(
            string username,
            string? contextProfileName);

        /// <summary>Gets by user name or email.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by user name.</returns>
        Task<IAccountModel?> GetByUserNameOrEmailAsync(
            string username,
            string? contextProfileName);

        /// <summary>Gets for pricing by user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="affAccountID">      Identifier for the aff account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>for pricing by user identifier.</returns>
        Task<IAccountModel?> GetForPricingByUserIDAsync(
            int userID,
            int affAccountID,
            string? contextProfileName);

        /// <summary>Gets for pricing by user name.</summary>
        /// <param name="username">          The username.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>for pricing by user name.</returns>
        Task<IAccountModel?> GetForPricingByUserNameAsync(
            string username,
            string? contextProfileName);

        /// <summary>Gets for taxes.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>for taxes.</returns>
        Task<IAccountModel?> GetForTaxesAsync(int? accountID, string? contextProfileName);

        /// <summary>Gets for cart validator.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>for cart validator.</returns>
        Task<IAccountModel?> GetForCartValidatorAsync(int accountID, string? contextProfileName);

        /// <summary>Gets type name for account.</summary>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The type name for account.</returns>
        Task<string?> GetTypeNameForAccountAsync(int accountID, string? contextProfileName);

        /// <summary>Searches for the first existing account.</summary>
        /// <param name="accountName">       Name of the account.</param>
        /// <param name="city">              The city.</param>
        /// <param name="regionID">          Identifier for the region.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The found existing account.</returns>
        Task<IAccountModel?> FindExistingAccountAsync(
            string? accountName,
            string? city,
            int? regionID,
            string? contextProfileName);

        /// <summary>Gets account identifier by attribute value.</summary>
        /// <param name="attrName">          Name of the attribute.</param>
        /// <param name="value">             The value.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The account identifier by attribute value.</returns>
        Task<int?> GetAccountIDByAttributeValueAsync(
            string attrName,
            string value,
            string? contextProfileName);

        /// <summary>Gets users for current account.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The users for current account.</returns>
        Task<(List<IUserModel> results, int totalPages, int totalCount)> GetUsersForCurrentAccountAsync(
            IUserSearchModel search,
            string? contextProfileName);

        /// <summary>Gets users for current account.</summary>
        /// <param name="userID">            The user ID.</param>
        /// <param name="accountID">         The selected account ID.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The users for current account.</returns>
        Task<bool> CheckCanEmulateAccountForCurrentUserAsync(
            int userID,
            int accountID,
            string? contextProfileName);
    }
}
