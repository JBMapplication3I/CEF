// <copyright file="IManufacturerWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IManufacturerWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for manufacturer workflow.</summary>
    public partial interface IManufacturerWorkflow
    {
        /// <summary>Gets products by manufacturer.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="asListing">         True to as listing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The products by manufacturer.</returns>
        Task<(IEnumerable<IManufacturerProductModel> results, int totalPages, int totalCount)> GetProductsByManufacturerAsync(
            IManufacturerProductSearchModel search,
            bool asListing,
            string? contextProfileName);

        /// <summary>Gets manufacturers by product.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The manufacturers by product.</returns>
        Task<List<IManufacturerProductModel>> GetManufacturersByProductAsync(int productID, string? contextProfileName);

        /// <summary>Gets identifier by assigned user identifier.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The identifier by assigned user identifier.</returns>
        Task<CEFActionResponse<int?>> GetIDByAssignedUserIDAsync(int userID, string? contextProfileName);
    }
}
