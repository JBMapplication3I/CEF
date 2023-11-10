// <copyright file="IPersonalizationProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPersonalizationProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Personalization
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Interface for personalization provider base.</summary>
    public interface IPersonalizationProviderBase : IProviderBase
    {
        /// <summary>Gets resulting product IDs for user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="minimum">           Minimum number of IDs to return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The resulting product IDs for user identifier.</returns>
        Task<List<int>> GetResultingProductIDsForUserIDAsync(int? userID, int minimum, string? contextProfileName);

        /// <summary>Gets resulting category IDs for user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="minimum">           Minimum number of IDs to return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The resulting category IDs for user identifier.</returns>
        Task<List<int>> GetResultingCategoryIDsForUserIDAsync(int? userID, int minimum, string? contextProfileName);

        /// <summary>Gets resulting feed for user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="minimum">           Minimum number of IDs to return.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The resulting feed: A dictionary of category IDs as the keys with a list of Product IDs as the
        /// values for each key.</returns>
        Task<Dictionary<int/*categoryID*/, List<int> /*productIDs*/>> GetResultingFeedForUserIDAsync(
            int? userID, int minimum, string? contextProfileName);
    }
}
