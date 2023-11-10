// <copyright file="IInventoryLocationWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IInventoryLocationWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>Interface for inventory location workflow.</summary>
    public partial interface IInventoryLocationWorkflow
    {
        /// <summary>Gets inventory locations by product.</summary>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The inventory locations by product.</returns>
        Task<List<IProductInventoryLocationSectionModel>> GetInventoryLocationsByProductAsync(
            int productID,
            string? contextProfileName);

        /// <summary>Searches for the first sections.</summary>
        /// <param name="locationID">        Identifier for the location.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The found sections.</returns>
        Task<List<IInventoryLocationSectionModel>> SearchSectionsAsync(int? locationID, string? contextProfileName);

        /// <summary>Gets a section.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The section.</returns>
        Task<IInventoryLocationSectionModel?> GetSectionAsync(int id, string? contextProfileName);

        /// <summary>Gets a section.</summary>
        /// <param name="customKey">         The custom key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The section.</returns>
        Task<IInventoryLocationSectionModel?> GetSectionAsync(string customKey, string? contextProfileName);
    }
}
